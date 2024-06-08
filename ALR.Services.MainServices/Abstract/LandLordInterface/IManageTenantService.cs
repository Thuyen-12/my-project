using ALR.Data.Base;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract.LandLordInterface
{
    public interface IManageTenantService
    {
        Task<List<UserEntity>> GetUserByName(string name);
        Task<(PagingListDto<UserEntity>, EnumBase.AlrResult)> GetUserInMotel(Guid landLordId, int startIndex, int pageSize);
    }
}
