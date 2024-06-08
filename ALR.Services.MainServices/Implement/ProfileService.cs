using ALR.Data.Database.Abstract;
using ALR.Domain.Entities.Entities;
using ALR.Domain.Entities;
using ALR.Services.MainServices.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALR.Data.Dto;

namespace ALR.Services.MainServices.Implement
{
    public class ProfileService : IProfileService

    {
        private readonly IRepository<ProfileEntity> _repository;
        private readonly IRepository<UserEntity> _userrepository;

        public ProfileService(IRepository<ProfileEntity> repository, IRepository<UserEntity> userrepository)
        {
            _repository = repository;
            _userrepository = userrepository;
        }

        public async Task<ProfileEntity> EditProfile(ProfileDto profile,Guid id)
        {
            var obj = await _repository.GetByConditionAsync(x => x.userEntity.UserEntityID.Equals(id)) as ProfileEntity;
            obj.UserName = profile.UserName;
            obj.Gender = profile.Gender;
            obj.DOB = profile.DOB;
            obj.Address = profile.Address;
            obj.PhoneNumber = profile.PhoneNumber;
             _repository.UpdateAsync(obj);
            await _repository.CommitChangeAsync();
            return obj;
        }


        public async Task<ProfileEntity> GetProfileById(Guid userID)
        {
            if (userID == Guid.Empty)
            {
                return null;
            }
            var profileresult = await _repository.GetByConditionAsync(x => x.userEntity.UserEntityID.Equals(userID));
            return profileresult;
        }
    }
}
