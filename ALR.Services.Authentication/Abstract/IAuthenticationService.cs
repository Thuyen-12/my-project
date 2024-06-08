using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ALR.Services.Authentication.Abstract
{
    public interface IAuthenticationService
    {
        Task<UserEntity> CheckLoginAsync(AccountDto account);
        Task<(string, DateTime)> CreateRefreshToken(UserEntity account);
        Task<(string, DateTime)> CreateAccessToken(UserEntity account);
        Task<UserEntity> FindByUserID(string userID);

        Task ValidateToken(TokenValidatedContext tokenValidatedContext);
        Task<(UserEntity, EnumBase.AlrResult)> changePass(Guid userId, string newPass, string confirmPass, string oldPass);
        Task<UserEntity> getAccountByName(string accountName);
        Task<int> CheckActiveAccount(UserEntity user);
    }
}
