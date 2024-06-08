using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Implement
{
    public class AdminPackageAdvanceServices : IAdminPackageAdvanceServices
    {
        private readonly IRepository<ServicesPackageEntity> _repository;
        private readonly IMapper _mapper;

        public AdminPackageAdvanceServices(IRepository<ServicesPackageEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServicesPackageEntity> UpdateServicePackageStatus(Guid packageId, PackageServiceDto dto)
        {
            var servicepakage = _repository.GetByConditionIncludeAsync(a => a.Service, b => b.User, x => x.servicePackageId.Equals(packageId)).Result;
            servicepakage.status = dto.status;
            //servicepakage.endDate = dto.endDate;
            servicepakage.CreatedDate = dto.CreatedDate;
            _repository.UpdateAsync(servicepakage);
            await _repository.CommitChangeAsync();
            return servicepakage;
        }
    }
}
