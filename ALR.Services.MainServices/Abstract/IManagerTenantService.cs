using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract
{
    public interface IManagerTenantService
    {
        Task<(List<UserEntity>, EnumBase.AlrResult)> AddTenant(Guid landlordId, Guid motelId, Guid roomid, UserEntity dto);
        Task<bool> DeleteTenant(Guid landlordId, Guid motelId, Guid roomid, UserEntity dto);
        //Task<PagingListDto<UserEntity>> GetListTenant(Guid landlordId);
    }
}
