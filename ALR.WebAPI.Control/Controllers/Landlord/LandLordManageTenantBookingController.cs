using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Services.Common.Abstract;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Landlord
{
    [Route("api/lanlord/[controller]")]
    [ApiController]
    [Authorize]
    public class LandLordManageTenantBookingController : ControllerBase
    {
        private readonly ILandlordManageTenantBookingServices _bookingService;
        private readonly IHttpContextAccessor _context;
        private readonly IEmailServices _emailServices;

        public LandLordManageTenantBookingController(ILandlordManageTenantBookingServices bookingService, IHttpContextAccessor context, IEmailServices emailServices)
        {
            _bookingService = bookingService;
            _context = context;
            _emailServices = emailServices;
        }
        [HttpGet]
        [Route("GetListBooking")]
        public async Task<IActionResult> GetListBooking(int startIndex, int pageSize)
        {
            var landlordId = Guid.Parse(_context.HttpContext.GetUserId());
            var result = await _bookingService.GetListBooking(landlordId, startIndex, pageSize);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut]
        [Route("LanlordConsiderBooking")]
        public async Task<IActionResult> LandlordConsiderBooking(Guid bookingId, int bookingStatus)
        {
            if (bookingStatus == 0)
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }
            if (bookingId.Equals(Guid.Empty))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            var result = await _bookingService.ConsiderBookingSchedule(bookingId, bookingStatus);
            if (result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var mes = "";
            if(bookingStatus == 1)
            {
                mes = $"Yêu cầu xem trọ của bạn đã được chấp nhận";
            }
            else
            {
                mes = $"Yêu cầu xem trọ của bạn đã bị từ chối";
            }
            var booking = await _bookingService.GetBooking(bookingId);
            var user = await _bookingService.GetUser(booking.tenantId);
            var message = new EmailMessage(new string[] { user.Email }, "Advanced Lodging Room verify email", $"{mes}.");
            _emailServices.SendMail(message);
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
