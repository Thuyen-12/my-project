using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement
{
    public class AdminServiceAdvanceServices : IAdminServiceAdvanceServices
    {
        private readonly IRepository<ServiceEntity> _repository;
        private readonly IMapper _mapper;

        public AdminServiceAdvanceServices(IRepository<ServiceEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceEntity> CreateNewService(CreateServiceDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var newService = _mapper.Map<ServiceEntity>(dto);
            newService.serviceId = Guid.NewGuid();
            _repository.InsertAsync(newService);
            await _repository.CommitChangeAsync();
            return newService;
        }

        public async Task<PagingListDto<ServiceEntity>> GetListService(int startIndex, int pageSize)
        {
            try
            {
                var listService = await _repository.GetDataAsync();

                PagingListDto<ServiceEntity> result = new PagingListDto<ServiceEntity>
                {
                    Data = listService.Skip(startIndex).Take(pageSize).ToList(),
                    TotalCount = listService.Count()
                };
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<AlrResult> UpdateService(CreateServiceDto dto, Guid ServiceId)
        {
            try
            {
                var service = await _repository.GetByConditionAsync(x => x.serviceId.Equals(ServiceId));
                if(service == null)
                {
                    return AlrResult.NullObject;
                }
                service.price = dto.price;
                service.discout = dto.discout;
                service.isActived = dto.isActived;
                service.serviceLevel = dto.serviceLevel;
                service.description = dto.description;
                service.expiredDate = dto.expiredDate;
                service.serviceName = dto.serviceName;
                if (service == null) {
                    return AlrResult.Failed;
                }
                _repository.UpdateAsync(service);
                await _repository.CommitChangeAsync();
                return AlrResult.Success;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
            
        }

        public async Task<AlrResult> DeleteService(Guid serviceId)
        {
            try
            {
                if(serviceId == null)
                {
                    return AlrResult.Failed;
                }
                var service = await _repository.GetByConditionAsync(x => x.serviceId.Equals(serviceId));
                if(service == null)
                {
                    return AlrResult.NullObject;
                }
                _repository.DeleteAsync(x => x.serviceId.Equals(serviceId));
                await _repository.CommitChangeAsync();
                return AlrResult.Success;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }
    }
}
