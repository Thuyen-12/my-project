using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract
{
    public interface IAdminServiceAdvanceServices
    {
        Task<ServiceEntity> CreateNewService(CreateServiceDto dto);
        Task<EnumBase.AlrResult> DeleteService(Guid serviceId);
        Task<PagingListDto<ServiceEntity>> GetListService(int startIndex, int pageSize);
        Task<EnumBase.AlrResult> UpdateService(CreateServiceDto dto, Guid ServiceId);
    }
}
