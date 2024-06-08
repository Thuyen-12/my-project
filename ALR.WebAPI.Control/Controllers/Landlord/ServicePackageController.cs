using ALR.Services.Common.AuthorizationFilter;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALR.Domain.Entities;
using ALR.Services.Common.Extentions;

namespace ALR.WebAPI.Control.Controllers.Landlord
{
    [Route("api/Lanlord/[controller]")]
    [ApiController]
    [Authorize]
    [SecurityPermission((int) UserRoleEnum.Landlord)]
    public class ServicePackageController : ControllerBase
    {
        private readonly ILandlordServicePackageServices _spService;
        private readonly IHttpContextAccessor _context;

        public ServicePackageController(ILandlordServicePackageServices spService,  IHttpContextAccessor context)
        {
            _spService = spService;
            _context = context;
        }
        [HttpGet]
        [Route("getallservicepackage")]
        [Authorize]
        public async Task<IActionResult> GetAllServicesPackage(int startIndex, int pageSize)
        {
            var landlordId = Guid.Parse( _context.HttpContext.GetUserId());
            var result = await _spService.GetAllServicePackage(landlordId,startIndex,pageSize);
            if(result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("getallservices")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllService(int startIndex, int pageSize)
        {
            var result = await _spService.GetAllService(startIndex,pageSize);
            if(result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return Ok(result);  
        }

        [HttpGet]
        [Route("getservicesbyId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            var result = await _spService.GetServiceByID(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return Ok(result);
        }
    }
}
