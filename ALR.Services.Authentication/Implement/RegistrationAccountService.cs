using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Services.Authentication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.Authentication.Implement
{
    public class RegistrationAccountService : IRegistrationAccountService
    {
        private readonly IRepository<UserEntity> _repository;

        public RegistrationAccountService(IRepository<UserEntity> repository)
        {
            _repository = repository;
        }

        public async Task<string> RegisterAccount(AccountResigterDTO dto)
        {
            try
            {

                string status = CheckConfirmPasswords(dto);
                if (!status.Equals("OK"))
                {
                    return status;
                }
                var pid = Guid.NewGuid();
                var userEntity = new UserEntity()
                {

                    Account = dto.account,
                    UserEntityID = Guid.NewGuid(),
                    Password = dto.password,
                    Email = dto.email,
                    Active = 0,
                    UserRole = dto.role,
                    ProfileID = pid,
                    AccountBalance = 0,
                    Profile = new ProfileEntity()
                    {
                        ProfileID = pid,
                        UserName = dto.userName,
                        Address = dto.address,
                        Gender = dto.gender,
                        DOB = dto.DOB,
                        PhoneNumber = dto.phoneNumber,

                    }
                };
                 _repository.InsertAsync(userEntity);
                await _repository.CommitChangeAsync();

                return status;
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public string CheckConfirmPasswords(AccountResigterDTO dto)
        {
            if (!dto.password.Equals(dto.confirmPassword))
            {
                return "Password not match";
            }
            return "OK";
        }

        public async Task<string> GetEmailFromUserId(Guid userId)
        {
            var user = await _repository.GetByConditionAsync(x => x.UserEntityID.Equals(userId));
            if (user == null)
            {
                return null;
            }
            return user.Email;
        }

        public async Task<AlrResult> ActiveAccount(UserEntity user)
        {
            if(user == null)
            {
                return AlrResult.Failed;
            }
            user.Active = 1;
            _repository.UpdateAsync(user);
           await _repository.CommitChangeAsync();
            return AlrResult.Success;

        }

        public async Task<UserEntity> GetUserByAccountName(string accountName)
        {
            var account = await _repository.GetByConditionAsync(x => x.Account.Equals(accountName));
            if(account == null)
            {
                return null;
            }
            return account;
        }
        public async Task<UserEntity> GetUserById(Guid id)
        {
            var account = await _repository.GetByConditionAsync(x => x.Account.Equals(id));
            if (account == null)
            {
                return null;
            }
            return account;
        }


    }
}
