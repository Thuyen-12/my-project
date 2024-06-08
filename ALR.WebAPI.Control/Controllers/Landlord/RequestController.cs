using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using ALR.Services.MainServices.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Landlord
{
    [Route("api/[controller]/landlord")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestServices _request;
        private readonly IHttpContextAccessor _context;
        public RequestController(IRequestServices request, IHttpContextAccessor context)
        {
            _request = request;
            _context = context;
        }

        [HttpPost]
        [Route("createnewrequest")]
        public async Task<IActionResult> CreateNewRequest(RequestEntity requestEntity)
        {
            var result = await _request.CreateNewRequest(requestEntity);
            if (result.Equals(AlrResult.Failed))
            {
                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status200OK,result);
        }

        [HttpPut]
        [Route("editrequest")]
        public async Task<IActionResult> EditRequest(RequestEntity request)
        {
            var updateResult = await _request.EditRequest(request);
            if(updateResult.Equals(AlrResult.Failed))
            {
                return BadRequest(updateResult);
            }
            return StatusCode(StatusCodes.Status200OK,updateResult);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            var deleteStatus = await _request.DeleteRequest(id);
            return Ok(deleteStatus);
        }

        [HttpGet]
        [Route("viewlistrequest")]
        [Authorize]
        public async Task<IActionResult> GetListRequest(int startIndex, int pageSize)
        {
            var currentUserId = Guid.Parse(_context.HttpContext.GetUserId());
            var result = await _request.GetListRequest(startIndex, pageSize, currentUserId);
            return Ok(result);
        }
    }
}
