using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Authentication.Abstract
{
    public interface IRegistrationAccountService
    {
        Task<EnumBase.AlrResult> ActiveAccount(UserEntity user);
        string CheckConfirmPasswords(AccountResigterDTO dto);
        Task<string> GetEmailFromUserId(Guid userId);
        Task<UserEntity> GetUserByAccountName(string accountName);
        Task<UserEntity> GetUserById(Guid id);
        Task<string> RegisterAccount(AccountResigterDTO dto);
    }
}
