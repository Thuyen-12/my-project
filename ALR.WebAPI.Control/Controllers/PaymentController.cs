using ALR.Data.Dto;
using ALR.Services.Common.Extentions;
using ALR.Services.Common.Payment;
using DocumentFormat.OpenXml.Bibliography;
using Irony.Parsing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALR.WebAPI.Control.Controllers
{
    [Route("api/[controller]/payment")]
    [ApiController]
    public class PaymentController: ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IHttpContextAccessor _context;
        public PaymentController(IPaymentService paymentService, IHttpContextAccessor context)
        {
            _paymentService = paymentService;
            _context = context;
        }

        [Authorize]
        [HttpGet("geturlpayment")]
        public string GetUrlPayment(float money)
        {
            var userId = _context.HttpContext.GetUserId();
            var result = _paymentService.CreateUrlPayMoney(money, Guid.Parse(userId));
            return result;
        }

        [HttpGet("test")]
        public ResponsePayment TestResponse([FromQuery] ResponsePayment test)
        {
            return test;
        }

        [Authorize]
        [HttpPost("saveresponsepayment")]
        public async Task<bool> SaveResponse(ResponsePaymentDto responsePayment)
        {
            var result = await _paymentService.SaveResponsePayment(responsePayment);
            return result ;
        }

    }
}
