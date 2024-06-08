using ALR.Data.Dto;
using ALR.Services.Common.Abstract;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Admin
{
    [Route("api/[controller]/Admin")]
    [ApiController]
    [Authorize]



    public class AdminBillHistoryController : Controller
    {
        private readonly IGenarateFileToExcel _sevices;
        private readonly IAdminBillServices _billServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminBillHistoryController(IGenarateFileToExcel sevices, IAdminBillServices billServices, IHttpContextAccessor httpContextAccessor)
        {
            _sevices = sevices;
            _billServices = billServices;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("BillListHistory")]
        public async Task<IActionResult> GetAllBill(int starIndex, int pageSize)
        {
            //var userrole = HttpContext.GetUserRole();
            ////if (ROLE.Administrator.Equals(userrole))
            ////    {
                var obj = await _billServices.GetAllBill(starIndex,pageSize);

                if (obj == null)
                {
                    return BadRequest(obj);
                }
            return Ok(obj);
            //}
            //return StatusCode(StatusCodes.Status400BadRequest);
        }
        [Route("genaratetoexcel")]
        [HttpPost]
        public async Task<IActionResult> GenarateToExcel()
        {
            var tableData = await _sevices.GetTableData();
            string base64String = string.Empty; 
            using(XLWorkbook wb = new XLWorkbook())
            {
                var sheet = wb.AddWorksheet(tableData, "Bill Record");
                sheet.Columns(1, 5).Style.Font.FontColor = XLColor.Black;
                using(MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    base64String = Convert.ToBase64String(ms.ToArray());
                }
            }
            return new CreatedResult(string.Empty, new {Code=200,Status =true, Message ="", Data = base64String});
        }

        [Route("createbill")]
        [HttpPost]
        public async Task<IActionResult> CreateNewBill(BillHistoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var billHistory = await _billServices.CreateNewBill(dto);
            if (billHistory.Equals(AlrResult.Failed))
            {
                return BadRequest(billHistory);
            }
            return StatusCode(StatusCodes.Status200OK, billHistory);
        }

        [HttpGet]
        [Route("GetBillByYear")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBillByYear(int year)
        {
            var result = await _billServices.GetBillByYear(year);
            if(result == null)
            {
                return BadRequest("Lỗi không thể lấy danh sách lịch sử thanh toán");
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("GetListUserByYear")]
        public async Task<IActionResult> GetListBillByYear(int year)
        {
            var result = await _billServices.GetListBillByYear(year);
            if(result.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, result);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

    }
}
