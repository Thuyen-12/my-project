
using ALR.Data.Base;
using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Services.Authentication.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.Authentication.Implement
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<UserEntity> _repository;
        public AuthenticationService(IConfiguration configuration, IRepository<UserEntity> repository)
        {
            _configuration = configuration;
            _repository = repository;
        }
        public async Task<(string, DateTime)> CreateAccessToken(UserEntity account)
        {
            DateTime expiredToken = DateTime.Now.AddMinutes(15);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Account, ClaimValueTypes.String, _configuration["TokenBearer:Issuer"]),
                new Claim(BaseConstants.USER_CLAIM_ROLE,account.UserRole.ToString(),ClaimValueTypes.String, _configuration["TokenBearer:Issuer"]),
                new Claim(BaseConstants.USER_CLAIM_ID,account.UserEntityID.ToString(),ClaimValueTypes.String, _configuration["TokenBearer:Issuer"])
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBearer:SignatureKey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var info = new JwtSecurityToken(
                issuer: _configuration["TokenBearer:Issuer"],
                claims: claims,
                audience: _configuration["TokenBearer:Audience"],
                notBefore:DateTime.Now,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credential

                );
            string token = new JwtSecurityTokenHandler().WriteToken(info);
            return await Task.FromResult((token, expiredToken));
        }

        public async Task<(string, DateTime)> CreateRefreshToken(UserEntity account)
        {
            DateTime expiredToken = DateTime.Now.AddHours(1);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBearer:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iss,ClaimValueTypes.String, _configuration["TokenBearer:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.Integer64, _configuration["TokenBearer:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp,DateTime.Now.AddHours(1).ToString("dd/MM/yyyy"), ClaimValueTypes.String, _configuration["TokenBearer:Issuer"]),
                new Claim(ClaimTypes.SerialNumber, Guid.NewGuid().ToString(), ClaimValueTypes.String,_configuration["TokenBearer:Issuer"])
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBearer:SignatureKey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var info = new JwtSecurityToken(
                    issuer: _configuration["TokenBearer:Issuer"],
                    audience: _configuration["TokenBearer:Audience"],
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    credential
                );
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(info);
            return await Task.FromResult((refreshToken, expiredToken));
        }

        public async Task<UserEntity> CheckLoginAsync(AccountDto account)
        {
            return await _repository.GetByConditionAsync(x => x.Account.Equals(account.Account) && x.Password.Equals(account.Password));

        }
        public async Task<int> CheckActiveAccount(UserEntity user)
        {
            return user.Active;
        }
        public async Task<UserEntity> FindByUserID(string userID)
        {
            return await _repository.GetByConditionAsync(x => x.UserEntityID.Equals(userID));
        }

        public async Task ValidateToken(TokenValidatedContext tokenValidatedContext)
        {
            var claims = tokenValidatedContext.Principal.Claims.ToList();
            if (claims.Count == 0)
            {
                tokenValidatedContext.Fail("This token contains no information");
                return;
            }
            var identity = tokenValidatedContext.Principal.Identity as ClaimsIdentity;
            if (identity.FindFirst(JwtRegisteredClaimNames.Iss) == null)
            {
                tokenValidatedContext.Fail("This token is not issued");
                return;
            }
            if (identity.FindFirst(BaseConstants.USER_CLAIM_ID) != null)
            {
                var userid = identity.FindFirst(BaseConstants.USER_CLAIM_ID).Value;
                var user = await FindByUserID(userid);
                if (user == null)
                {
                    tokenValidatedContext.Fail($"This token invalid for userID : {userid}");
                    return;
                }
            }
            if (identity.FindFirst(JwtRegisteredClaimNames.Exp) != null)
            {
                var dateEXP = identity.FindFirst(JwtRegisteredClaimNames.Exp).Value;
                long ticks = long.Parse(dateEXP);
                var date = DateTimeOffset.FromUnixTimeSeconds(ticks).DateTime;
                var time = date.Subtract(DateTime.Now).TotalSeconds;
                if (time < 0)
                {
                    tokenValidatedContext.Fail("This token is expired");
                    return;
                }
            }


        }

        public async Task<(UserEntity, AlrResult)> changePass(Guid userId, string newPass, string confirmPass, string oldPass)
        {
            if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass)||string.IsNullOrEmpty(oldPass))
            {
                return (null, AlrResult.Failed);
            }
            if(!newPass.Equals(confirmPass))
            {
                return (null, AlrResult.Failed);
            }
            var Account = await _repository.GetByConditionAsync(x => x.UserEntityID.Equals(userId)) as UserEntity;
            if (Account == null|| !Account.Password.Equals(oldPass))
            {
                return (null, AlrResult.Failed);
            }
            Account.Password = newPass;
            _repository.UpdateAsync(Account);
            await _repository.CommitChangeAsync();
            return (Account, AlrResult.Success);
        }

        public async Task<UserEntity> getAccountByName(string accountName)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                return null;
            }
            var account = await _repository.GetByConditionAsync(x => x.Account.Equals(accountName));
            if (account == null)
            {
                return null;
            }
            return account;
        }

        
    }
}
