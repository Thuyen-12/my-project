using ALR.Domain.Entities;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Landlord
{
    [Route("api/[controller]/landlord")]
    [ApiController]
    [Authorize]
    public class TenantManageController : ControllerBase
    {
        private readonly IManagerTenantService _request;
        private readonly IManageTenantService _manageTenantService;

        private readonly IHttpContextAccessor _context;
        public TenantManageController(IManagerTenantService request, IManageTenantService manageTenantService, IHttpContextAccessor context)
        {
            _request = request;
            _manageTenantService = manageTenantService;
            _context = context;
        }

        //[HttpGet]
        //[Route("viewlisttenant")]
        //public async Task<IActionResult> GetListTenant(Guid landlordId, int pageNumber, int pageSize)
        //{
        //   var result = await _request.GetListTenant(landlordId);
        //    return Ok(result);
        //}

        [HttpPost]
        [Route("addtenant")]
        public async Task<IActionResult> AddTenant(Guid motelId, Guid roomid, UserEntity dto)
        {
            var result = await _request.AddTenant(Guid.Parse(_context.HttpContext.GetUserId()), motelId, roomid, dto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("Deletetenant")]
        public async Task<IActionResult> DeleteTenant(Guid motelId, Guid roomid, UserEntity dto)
        {
            var result = await _request.DeleteTenant(Guid.Parse(_context.HttpContext.GetUserId()), motelId, roomid, dto);
            return Ok(result);
        }

        [HttpGet]
        [Route("getlisttenantbylandlordId")]
        public async Task<IActionResult> GetListTenantByLandLordId(int startIndex, int pageSize)
        {
            var result = await _manageTenantService.GetUserInMotel(Guid.Parse(_context.HttpContext.GetUserId()), startIndex, pageSize);
            return StatusCode(StatusCodes.Status200OK, result.Item1);
        }

       
    }
}
