using ALR.Data.Database.Abstract;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Implement.LandLordImplement
{
    public class LanlordServicePackageServices :ILandlordServicePackageServices
    {
        private readonly IRepository<ServicesPackageEntity> _spRepository;
        private readonly IRepository<ServiceEntity> _serviceRepository;

        public LanlordServicePackageServices(IRepository<ServicesPackageEntity> spRepository, IRepository<ServiceEntity> serviceRepository)
        {
            _spRepository = spRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<PagingListDto<ServicesPackageEntity>> GetAllServicePackage(Guid landLordId, int startIndex, int pageSize)
        {
            var listSP = await _spRepository.GetDataAsync(x => x.userId.Equals(landLordId));
            if (listSP == null)
            {
                return null;
            }
            PagingListDto<ServicesPackageEntity> result = new PagingListDto<ServicesPackageEntity>()
            {
                Data = listSP.Skip(startIndex).Take(pageSize).ToList(),
                TotalCount = listSP.Count()
            };
            return result;
        }

        public async Task<PagingListDto<ServiceEntity>> GetAllService(int startIndex, int pageSize)
        {
            var listService = await _serviceRepository.GetDataAsync(x => x.isActived == true);
            if (listService == null)
            {
                return null;
            }

            PagingListDto<ServiceEntity> result = new PagingListDto<ServiceEntity>()
            {
                Data = listService.Skip(startIndex).Take(pageSize).ToList(),
                TotalCount = listService.Count()
            };
            return result;
        }

        public async Task<ServiceEntity> GetServiceByID(Guid servicesId)
        {
            var service = await _serviceRepository.GetByConditionAsync(x => x.serviceId.Equals(servicesId));
            if (service == null)
            {
                return null;
            }
            return service;
        }
    }
}
