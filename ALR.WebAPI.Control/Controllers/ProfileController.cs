using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using ALR.Services.MainServices.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ALR.WebAPI.Control.Controllers
{
    [Route("api/[controller]/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileservice;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _context;
        private readonly IAdminAccountService _accountservice;

        public ProfileController(IProfileService profileservice, IWebHostEnvironment environment, IHttpContextAccessor context, IAdminAccountService accountservice)
        {
            _profileservice = profileservice;
            _environment = environment;
            _context = context;
            _accountservice = accountservice;
        }

        [HttpPost]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile( ProfileDto profile)
        {
            var id = Guid.Parse(_context.HttpContext.GetUserId());
            var updateResult = await _profileservice.EditProfile(profile, id);

            return Ok(updateResult);
        }


        [HttpGet]
        [Route("GetProfileById")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var id = Guid.Parse(_context.HttpContext.GetUserId());
            var result = await _profileservice.GetProfileById(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
