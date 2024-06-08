using ALR.Data.Dto;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using ALR.Services.MainServices.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Landlord
{
    [Route("api/[controller]/landlord")]
    [ApiController]
    [Authorize]
    public class BillManageController : ControllerBase
    {
        private readonly IBillManageService _services;
        private readonly IHttpContextAccessor _context;

        public BillManageController(IBillManageService services, IHttpContextAccessor context )
        {
            _services = services;
            _context = context;
        }

        [Route("createbill")]
        [HttpPost]
        public async Task<IActionResult> CreateNewBill(BillHistoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var billHistory = await _services.CreateNewBill(dto);
            if (billHistory == null)
            {
                return BadRequest();
            }
            return StatusCode(StatusCodes.Status200OK, billHistory);
        }

        [Route("updatebillstatus")]
        [HttpPut]
        public async Task<IActionResult> UpdateBillStatus(Guid billId, int status)
        {
            var result = _services.UpdateBillStatus(billId, status);
            if (result.Equals(AlrResult.Failed))
            {
                return BadRequest();
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [Route("delete")]
        [HttpDelete]      
        public async Task<IActionResult> DeleteBill(Guid id)
        {
            var deleteStatus = await _services.DeleteBill(id);
            return Ok(deleteStatus);
        }

        [HttpGet]
        [Route("GetOwnLanlordBill")]
        public async Task<IActionResult> GetOwnLanlordBill(int pageIndex, int PageSize)
        {
            var lanlordId = Guid.Parse(_context.HttpContext.GetUserId());
            var result = await _services.GetOwnLandlordBill(lanlordId, pageIndex, PageSize);
            if(result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

    }
}
