using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using ALR.Services.MainServices.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALR.WebAPI.Control.Controllers.Admin
{
    [Route("api/[controller]/Admin")]
    [ApiController]
    public class RequestAdminController : Controller
    {
        private readonly IRequestServices _services;
        private readonly IHttpContextAccessor _context;
        public RequestAdminController(IRequestServices services, IHttpContextAccessor context)
        {
            _services = services;
            _context = context;

        }
        [Route("approveRequest")]
        [HttpPost]
        public async Task<IActionResult> ApproveRequestAsync(Guid requestId, int requestStatus)
        {
            var result = await _services.UpdateRequestStatus(requestId, requestStatus);
            return Ok(result.ToString());
        }

        [Route("getlistrequest")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetListRequestAsync(int startIndex, int pageSize)
        {
            var currentUserId = Guid.Parse(_context.HttpContext.GetUserId());
            var result = await _services.GetListRequest(startIndex,pageSize, currentUserId);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("SearchByTitle")]
        public async Task<IActionResult> GetRequestByTitle(string name)
        {
            var getrequestByTitle = await _services.GetRequestByTitle(name);
            return Ok(getrequestByTitle);

        }
    }
}
