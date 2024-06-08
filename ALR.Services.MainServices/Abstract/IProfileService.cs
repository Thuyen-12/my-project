using ALR.Data.Dto;
using ALR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract
{
    public interface IProfileService
    { 
        Task<ProfileEntity> EditProfile(ProfileDto profile, Guid id);
        Task<ProfileEntity> GetProfileById(Guid userID);
    }
}
