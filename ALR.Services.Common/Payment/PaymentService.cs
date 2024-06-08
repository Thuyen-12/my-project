using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Services.MainServices.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Common.Payment
{
    public class PaymentService: IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBillManageService _billService;
        private readonly IAdminAccountService _userService;
        private readonly IRepository<UserEntity> _userRepository;

        public PaymentService(IConfiguration configuration, IBillManageService billService, IAdminAccountService userService, IRepository<UserEntity> userRepository)
        {
            _configuration = configuration;
            _billService = billService;
            _userService = userService;
            _userRepository = userRepository;
        }
        public string CreateUrlPayMoney(float money, Guid userId)
        {
            string url = _configuration["PaymentConfiguration:Url"];
            string returnUrl = _configuration["PaymentConfiguration:ReturnUrl"];
            string tmnCode = _configuration["PaymentConfiguration:TmnCode"];
            string hashSecret = _configuration["PaymentConfiguration:HashSecret"];

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", (money * 100).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", "NCB"); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", "127.0.0.1"); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", $"{userId}_Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "billpayment"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn
            //pay.AddRequestData("vnp_CardType", "9704198526191432198");

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return paymentUrl;
        }

        public async Task<bool> SaveResponsePayment(ResponsePaymentDto responsePaymentDto)
        {
            if(!responsePaymentDto.ResponseCode.Equals("00")) return false;
            var userId = responsePaymentDto.OrderInfo.Split('_')[0];
            var userEntity = await _userService.GetCurrentUser(Guid.Parse(userId));
            if(userEntity == null) return false;
            var billHistoryDto = new BillHistoryDto()
            {
                billType = 1,
                paymentDate = DateTime.Now,
                cost = responsePaymentDto.Amount / 100,
                status = 0,
                userId = Guid.Parse(userId),
                billDescription = "Nạp tiền vào hệ thống"
            };
            userEntity.AccountBalance += responsePaymentDto.Amount/100;
            _userRepository.UpdateAsync(userEntity);
            await _userRepository.CommitChangeAsync();
            
            var result = await _billService.CreateNewBill(billHistoryDto);
            return true;
        }
    }
}
