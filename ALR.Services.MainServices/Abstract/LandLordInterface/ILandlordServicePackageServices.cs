using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract.LandLordInterface
{
    public interface ILandlordServicePackageServices
    {
        Task<PagingListDto<ServiceEntity>> GetAllService(int startIndex, int pageSize);
        Task<PagingListDto<ServicesPackageEntity>> GetAllServicePackage(Guid landLordId, int startIndex, int pageSize);
        Task<ServiceEntity> GetServiceByID(Guid servicesId);
    }
}
