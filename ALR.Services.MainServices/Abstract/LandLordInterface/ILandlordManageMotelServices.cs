using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract.LandLordInterface
{
    public interface ILandlordManageMotelServices
    {
        Task<EnumBase.AlrResult> CreateNewMotel(CreateMotelDto dto, Guid lanlordId);
        Task<EnumBase.AlrResult> DeleteMotel(Guid motelId);
        Task<PagingListDto<MotelEntity>> GetAllMotelByLandlordID(Guid lanflordId, int startIndex, int pageSize);
        Task<MotelEntity> GetMotelById(Guid motelId);
        Task<EnumBase.AlrResult> UpdateMotel(Guid motelId, CreateMotelDto dto);
    }
}
