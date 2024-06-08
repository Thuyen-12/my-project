using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALR.WebAPI.Control.Controllers.Tenant
{
    [Route("api/[controller]/Tenant")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IHttpContextAccessor _context;

        public PostController(IPostService service, IHttpContextAccessor context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        [Route("viewpostdetail")]
        public async Task<IActionResult> GetPostDetail(Guid postId)
        {
            var result = await _service.GetPostById(postId);
            return Ok(result.Item2);
        }

        [HttpGet]
        [Route("viewAuthorOfPost")]
        public async Task<IActionResult> GetAuthorOfPost(Guid postId)
        {
            var result = await _service.GetPostById(postId);
            return Ok(result.Item3);
        }

        [HttpGet]
        [Route("GetPostById")]
        public async Task<IActionResult> GetPostById(Guid postId, int startIndex, int pageSize)
        {
            var result = await _service.GetPostById(postId);
            return Ok(result);
        }

        
    }
}
