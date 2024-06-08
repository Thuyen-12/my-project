using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Services.Common.Abstract;
using ALR.Services.Common.AuthorizationFilter;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using ALR.Services.MainServices.Abstract.TenantInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;


namespace ALR.WebAPI.Control.Controllers.Tenant
{
    [Route("api/[controller]/tenant")]
    [ApiController]
    [Authorize]
    public class BookingScheduleController : ControllerBase
    {
        private readonly IBookingScheduleService _service;
        private readonly IHttpContextAccessor _context;
        private readonly ITenantBookingScheduleService _tenantBookingService;
        private readonly IEmailServices _emailService;

        public BookingScheduleController(IBookingScheduleService service, IHttpContextAccessor context, ITenantBookingScheduleService tenantBookingService, IEmailServices emailService)
        {
            _service = service;
            _context = context;
            _tenantBookingService = tenantBookingService;
            _emailService = emailService;
        }


        [HttpPost]
        [Route("TenantCreateBooking")]
        public async Task<IActionResult> TenantCreateBooking(CreateBookingScheduleDto dto, Guid postId)
        {
            if(dto.BookingDate < DateTime.Now) {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            var tenantId =Guid.Parse( _context.HttpContext.GetUserId());
            var result = await _tenantBookingService.TenantBookingSchedule(dto, postId, tenantId);
            if(result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var author =await _tenantBookingService.GetPostAuthor(result.landlordId);
            var tenant =await _tenantBookingService.GetPostAuthor(tenantId);
            var message = new EmailMessage(new string[] { author.Email }, "Advanced Lodging Room notification email", $"Bạn có yêu cầu lịch hẹn của  : {tenant.Profile.UserName} vào {result.bookingDate}.");

            _emailService.SendMail(message);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet]
        [Route("TenantGetOwnBooking")]
        public async Task<IActionResult> TenatGetOwnBooking(int startIndex, int pageSize)
        {
            var tenantId = Guid.Parse(_context.HttpContext.GetUserId());
            var result = await _tenantBookingService.TenantGetOwnRequest(tenantId, startIndex,pageSize);
            return StatusCode(StatusCodes.Status200OK,result);
        }

        [HttpPut]
        [Route("TenantEditBookingSchedule")]
        public async Task<IActionResult> TenantEditBookingSchedule(CreateBookingScheduleDto dto, Guid bookingId)
        {
            var result = await _tenantBookingService.TenantEditOwnRequest(dto, bookingId);
            if(result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete]
        [Route("TenantDeleteBookingSchedule")]
        public async Task<IActionResult> TenantDeleteBookingSchedule( Guid bookingId)
        {
            var result = await _tenantBookingService.TenantDeleteOwnBooking(bookingId);
            if (result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
