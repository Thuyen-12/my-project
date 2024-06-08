using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Landlord
{
    [Route("api/Lanlord/[controller]")]
    [ApiController]
    [Authorize]
    public class LandlordBuyServicesController : ControllerBase
    {
        private readonly ILanlordPaymentService _lanlordService;
        private readonly IHttpContextAccessor _context;

        public LandlordBuyServicesController(ILanlordPaymentService lanlordService, IHttpContextAccessor context)
        {
            _lanlordService = lanlordService;
            _context = context;
        }
        [HttpPost]
        [Route("LanlordBuyService")]
        public async Task<IActionResult> LanlordBuyServices(Guid serviceId)
        {
            var lanlordId = Guid.Parse(_context.HttpContext.GetUserId());
            var result = await _lanlordService.BuyServiceAction(lanlordId, serviceId);
            if (result.Equals(AlrResult.NullObject))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        [Route("GetAllService")]
        public async Task<IActionResult> LandlordGetAllService(int pageIndex, int pageSize)
        {
            var result = await _lanlordService.GetAllService(pageIndex, pageSize);
            if(result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            
            return StatusCode(StatusCodes.Status200OK, result) ;
        }

        [HttpGet]
        [Route("CheckServiceUnexpired")]
        public async Task<IActionResult> CheckServiceUnexpired(Guid serviceId)
        {
            var lanlordId = Guid.Parse(_context.HttpContext.GetUserId());
            var result = await _lanlordService.CheckServicesUnExpired(lanlordId, serviceId);
            
            if(result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, result) ;
        }

        
    }
}
