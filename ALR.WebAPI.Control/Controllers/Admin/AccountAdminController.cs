using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Admin
{
    [Route("api/[controller]/Admin")]
    [ApiController]
    //[Authorize(Roles = EnumBase.ROLE.Administrator)]
    public class AccountAdminController : Controller
    {
        private readonly IAdminAccountService _service;
        private readonly IHttpContextAccessor _context;

        public AccountAdminController(IAdminAccountService service, IHttpContextAccessor context)
        {
            _service = service;
            _context = context;
        }

        [Route("createAccount")]
        [HttpPost]
        public async Task<IActionResult> CreateAccountByAdminAsync(AdminCreateAccountDto dto)
        {
            var account = await _service.CreateAccountByAdmin(dto);
            if(account == null)
            {
                return BadRequest();
            }
            return Ok(); 
        }
        [Route("GetListUser")]
        [HttpGet]
        public async Task<IActionResult> GetListUser(int startIndex, int pageSize)
        {
            var result = await _service.GetListUser(startIndex,pageSize);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Route("GetCurrentUser")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var id = _context.HttpContext.GetUserId();
            var result = await _service.GetCurrentUser(Guid.Parse(id));
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("unactiveAccount")]
        [Authorize]
        public async Task<IActionResult> UnactiveAccount(Guid accountId, int status)
        {
            var result = await _service.UnactiveAccount(accountId, status);
            if(result.Equals(AlrResult.NullObject))
            {
                return StatusCode(StatusCodes.Status404NotFound, "Không thể khóa tài khoản này");
            }
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
