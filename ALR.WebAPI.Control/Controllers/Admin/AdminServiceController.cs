using ALR.Data.Dto;
using ALR.Services.MainServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Admin
{
    [Route("api/[controller]/Admin")]
    [ApiController]
    [Authorize]
    public class AdminServiceController : Controller
    {
        private readonly IAdminServiceAdvanceServices _service;

        public AdminServiceController(IAdminServiceAdvanceServices service)
        {
            _service = service;
        }
        [Route("createservice")]
        [HttpPost]
        public async Task<IActionResult> CreateNewServices(CreateServiceDto dto)
        {
            var result = await _service.CreateNewService(dto);
            if (result == null)
            {
                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [Route("getlistservice")]
        [HttpGet]
        public async Task<IActionResult> GetListService(int startIndex, int pageSize)
        {
            var result = await _service.GetListService(startIndex, pageSize);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("AdminUpdateService")]
        public async Task<IActionResult> AdminUpdateService(CreateServiceDto dto, Guid serviceId)
        {
            if (serviceId == Guid.Empty)
            {
                return NotFound();
            }
            var result = await _service.UpdateService(dto, serviceId);
            if (result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            if (result.Equals(AlrResult.NullObject))
            {
                return NotFound("Không thể tìm thấy dịch vụ này");
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }

        [HttpDelete]
        [Route("AdminDeleteService")]
        public async Task<IActionResult> AdminDeleteService(Guid servicesId)
        {
            if (servicesId == Guid.Empty)
            {
                return NotFound("Không tìm thấy dịch vụ này");
            }
            var result = await _service.DeleteService(servicesId);
            if (result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest,"Không thể xóa dịch vụ này");
            }
            if (result.Equals(AlrResult.NullObject))
            {
                return NotFound("Không tìm thấy dịch vụ này");
            }
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}