using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.Common.AuthorizationFilter;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using ALR.Services.MainServices.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Tenant
{
    [Route("api/[controller]/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedBackService _feedbackService;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _context;

        public FeedbackController(IFeedBackService feedbackService, IWebHostEnvironment environment, IHttpContextAccessor context)
        {
            _feedbackService = feedbackService;
            _environment = environment;
            _context = context;
        }

        [HttpGet]
        [Route("getallfeedbackbypost")]

        public async Task<IActionResult> GetAllFeedBackByPost(Guid postid, int startIndex, int pageSize)
        {

            var allPost = await _feedbackService.GetAllFeedBackByPost(postid,startIndex,pageSize);
            return Ok(allPost);
        }

        [HttpDelete]
        [Route("deletefeedback")]
        [Authorize]
        [SecurityPermission((int)UserRoleEnum.Tenant)]
        public async Task<IActionResult> Deletefeedback(Guid id)
        {
            var tenantId = Guid.Parse(_context.HttpContext.GetUserId());
            var deleteStatus = await _feedbackService.DeleteFeedback(id, tenantId);
            return Ok(deleteStatus);
        }
        [Authorize]
        [SecurityPermission((int)UserRoleEnum.Tenant)]
        [HttpPost]
        [Route("createnewfeedback")]

        public async Task<IActionResult> CreateNewFeedBack(FeedBackDto dto, Guid postid)
        {
            var tenantId = Guid.Parse(_context.HttpContext.GetUserId());
            var result = await _feedbackService.CreateFeedback(dto, postid, tenantId);
            if (result.Equals(AlrResult.Failed))
            {
                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost]
        [Route("updatefeedback")]
        [Authorize]
        [SecurityPermission((int)UserRoleEnum.Tenant)]
        public async Task<IActionResult> UpdateFeedback(EditFeedBackDto feedback)
        {
            var tenantId = Guid.Parse(_context.HttpContext.GetUserId());
            var updateResult = await _feedbackService.EditFeedback(feedback, tenantId);
            return Ok(updateResult);
        }

        [HttpPost]
        [Route("CheckFeedbackUserExist")]
        [Authorize]
        public async Task<bool> CheckFeedbackUserExist(Guid postId)
        {
            var tenantid =Guid.Parse( _context.HttpContext.GetUserId());
            var result = await _feedbackService.CheckUserFeedback(postId, tenantid);
            return result;
        }
    }
}
