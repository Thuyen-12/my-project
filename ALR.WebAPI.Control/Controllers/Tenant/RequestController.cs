using ALR.Domain.Entities.Entities;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALR.WebAPI.Control.Controllers.Tenant
{
    [Route("api/[controller]/tenant")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestServices _service;
        private readonly IHttpContextAccessor _context;
        public RequestController(IRequestServices service, IHttpContextAccessor context)
        {
            _service = service;
            _context = context;
        }
        [HttpGet]
        [Route("viewlistrequest")]
        [Authorize]
        public async Task<IActionResult> GetListRequest(int startIndex, int pageSize)
        {
            var currentUserId =Guid.Parse( _context.HttpContext.GetUserId());
            var result = await _service.GetListRequest(startIndex,pageSize, currentUserId);
            return Ok(result);
        }
        [HttpPut]
        [Route("editrequest")]
        public async Task<IActionResult> EditRequest(RequestEntity request)
        {
            var updateResult = await _service.EditRequest(request);
            return Ok(updateResult);
        }

        [HttpPost]
        [Route("createnewrequest")]
        public async Task<IActionResult> CreateNewRequest(RequestEntity requestEntity)
        {
            var result = await _service.CreateNewRequest(requestEntity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            var deleteStatus = await _service.DeleteRequest(id);
            return Ok(deleteStatus);
        }
    }
}
