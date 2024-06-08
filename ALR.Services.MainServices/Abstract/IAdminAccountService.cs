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
    public interface IAdminAccountService
    {
        Task<UserEntity> CreateAccountByAdmin(AdminCreateAccountDto dto);
        
        Task<UserEntity> GetCurrentUser(Guid userID);
        Task<List<UserEntity>> GetListUser(List<Guid> userIds);
        Task<PagingListDto<UserEntity>> GetListUser(int startIndex, int pageSize);
        Task<EnumBase.AlrResult> UnactiveAccount(Guid accountId, int status);
    }
}
