using ALR.Data.Dto;
using ALR.Services.MainServices.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ALR.WebAPI.Control.Controllers.Admin
{
    [Route("api/[controller]/Admin")]
    [ApiController]
    public class PackageServiceController : Controller
    {
        private readonly IAdminPackageAdvanceServices _services;

        public PackageServiceController(IAdminPackageAdvanceServices services)
        {
            _services = services;
        }
        [Route("updateservicepackage")]
        [HttpPut]
        public async Task<IActionResult> UpdatePackageServiceStatus(Guid id, PackageServiceDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var result = await _services.UpdateServicePackageStatus(id, dto);
            if (result == null)
            {
                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
