using ALR.Data.Dto;
using ALR.Services.Authentication.Abstract;
using ALR.Services.Common.Abstract;
using ALR.Services.Common.Extentions;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Authentications
{
    [Route("api/[controller]/Authentications")]
    [ApiController]
    [AllowAnonymous]
    public class RegistrationAccountController : Controller
    {
        private readonly IRegistrationAccountService _registration;
        private readonly IEmailServices _emailServices;
        private readonly IHttpContextAccessor _context;

        public RegistrationAccountController(IRegistrationAccountService registration, IEmailServices emailServices, IHttpContextAccessor context)
        {
            _registration = registration;
            _emailServices = emailServices;
            _context = context;
        }
        [HttpPost]
        [Route("RegisterAccount")]
        public async Task<IActionResult> RegisterAccount([FromBody] AccountResigterDTO account)
        {
            try
            {
                if (account == null)
                {
                    return BadRequest();
                }


                var registerAccount = await _registration.RegisterAccount(account);
                if (registerAccount == null)
                {
                    return BadRequest();
                }
                else
                {
                    if (await _emailServices.CanSendMail(account.email))
                    {
                        var randomNumber = await _emailServices.RandomNumber(6);
                        var message = new EmailMessage(new string[] { account.email }, "Advanced Lodging Room verify email", $"Your verify code : {randomNumber}.");
                        _emailServices.AddToCache(account.email, randomNumber);

                        _emailServices.SendMail(message);
                        return Ok(registerAccount);
                    }
                    return StatusCode(StatusCodes.Status406NotAcceptable);
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }

        }

        [HttpPost]
        [Route("send-verify-email")]
        public async Task<IActionResult> SendVerifyEmail(string email)
        {
            if (await _emailServices.CanSendMail(email))
            {
                var randomNumber = await _emailServices.RandomNumber(6);
                var message = new EmailMessage(new string[] { email }, "Advanced Lodging Room verify email", $"Your verify code : {randomNumber}.");
                _emailServices.AddToCache(email, randomNumber);

                _emailServices.SendMail(message);
                return Ok(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status406NotAcceptable,"Hãy thử lại sau 1 phút");
        }

        [HttpPost]
        [Route("send-verify-email-for-exist-account")]
        [Authorize]
        public async Task<IActionResult> SendVerifyEmailForExistedAccount()
        {

            var userId = Guid.Parse(_context.HttpContext.GetUserId());
            var email = await _registration.GetEmailFromUserId(userId);
            if (await _emailServices.CanSendMail(email))
            {
                var randomNumber = await _emailServices.RandomNumber(6);
                var message = new EmailMessage(new string[] { email }, "Advanced Lodging Room verify email", $"Your verify code : {randomNumber}.");
                _emailServices.AddToCache(email, randomNumber);

                _emailServices.SendMail(message);
                return Ok(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);

        }

        [HttpPost]
        [Route("Verify-Account")]
        [Authorize]
        public async Task<IActionResult> VerifyExistedAccount(string verifyCode)
        {
            var userId = Guid.Parse(_context.HttpContext.GetUserId());
            var email = await _registration.GetEmailFromUserId(userId);
            var user = await _registration.GetUserById(userId);
            var verifyEmailCode = await _emailServices.GetValueFromCache(email);
            if (!verifyEmailCode.Equals(verifyCode))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var result = await _registration.ActiveAccount(user);
            if (result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost]
        [Route("verify-regis-account")]
        public async Task<IActionResult> VerifyRegisAccount(string account, string email, string verifyCode)
        {
            var verifyEmailCode = await _emailServices.GetValueFromCache(email);
            if (!verifyEmailCode.Equals(verifyCode))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var user = await _registration.GetUserByAccountName(account);
            var result = await _registration.ActiveAccount(user);
            if (result.Equals(AlrResult.Failed))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
