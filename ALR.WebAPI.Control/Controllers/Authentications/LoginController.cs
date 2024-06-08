using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Services.Authentication.Abstract;
using ALR.Services.Common.Abstract;
using ALR.Services.Common.Extentions;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Authentications
{
    [Route("api/[controller]/Authentications")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHttpContextAccessor _context;

        IAuthenticationService _authenticationService;
        ISaveUserToken _saveUserToken;
        private readonly IEmailServices _emailServices;

        public LoginController(IAuthenticationService authenticationService, ISaveUserToken saveUserToken, IEmailServices emailServices, IHttpContextAccessor context)
        {
            _authenticationService = authenticationService;
            _saveUserToken = saveUserToken;
            _emailServices = emailServices;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] AccountDto account)
        {
            try
            {
                if (account == null)
                {
                    return BadRequest();
                }
                var acc = await _authenticationService.CheckLoginAsync(account);
                if (acc != null)
                {
                    if(await _authenticationService.CheckActiveAccount(acc) == 1) {
                        (string accessToken, DateTime expiredAccessToken) = await _authenticationService.CreateAccessToken(acc);
                        (string refreshToken, DateTime expiredRereshToken) = await _authenticationService.CreateRefreshToken(acc);

                        long check1 = _saveUserToken.Encode(accessToken);
                        await _saveUserToken.SaveTokenAsync(new UserTokenEntity
                        {
                            AccessToken = check1.ToString(),
                            RefreshToken = refreshToken,
                            UserID = acc.UserEntityID,
                            ExpireDateAccessToken = expiredAccessToken,
                            ExpireDateRefreshToken = expiredRereshToken,
                            CreationTime = DateTime.Now,
                            IsActive = true
                        });
                        return Ok(
                            new JwtDto
                            {
                                status = "OK",
                                AccessToken = accessToken,
                                RefreshToken = refreshToken,
                                Account = account.Account,
                                UserID = acc.UserEntityID
                            });
                    }
                    else if(await _authenticationService.CheckActiveAccount(acc) == 0)
                    {
                        return StatusCode(StatusCodes.Status406NotAcceptable);

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status423Locked, "Tài khoản của bạn đã bị khóa");
                    }

                }

                return Unauthorized();
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
        [HttpPost]
        [Route("changePass")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string oldPass, string newPass)
        {
            var result = await _authenticationService.changePass(Guid.Parse(_context.HttpContext.GetUserId()), newPass, newPass, oldPass);
            if (result.Item1 == null || result.Item2.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, AlrResult.Success);
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string accountName ,string email)
        {
            if(await _emailServices.CanSendMail(email))
            {
                var account = await _authenticationService.getAccountByName(accountName);
                if (account == null)
                {
                    return BadRequest();
                }
                if (!account.Email.Equals(email))
                {
                    return BadRequest();
                }
                var newPass = await _emailServices.randomPassWord(15);
                var message = new EmailMessage(new string[] { email }, "Advanced Lodging Room", $"Your new password : {newPass}.");

                _emailServices.SendMail(message);
                _emailServices.AddToCache(account.Email, newPass);
                var result = await _authenticationService.changePass(account.UserEntityID, newPass, newPass, account.Password);
                if (result.Item1 == null || result.Item2.Equals(AlrResult.Failed))
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                return StatusCode(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
            
        }
    }


}
