using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement.LandLordImplement
{
    public class LandlordPaymentService : ILanlordPaymentService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<ServiceEntity> _serviceRepository;
        private readonly IRepository<ServicesPackageEntity> _spRepository;
        private readonly IRepository<BillHistoryEntity> _billRepository;

        public LandlordPaymentService(IRepository<UserEntity> userRepository, IRepository<ServiceEntity> serviceRepository, IRepository<ServicesPackageEntity> spRepository, IRepository<BillHistoryEntity> billRepository)
        {
            _userRepository = userRepository;
            _serviceRepository = serviceRepository;
            _spRepository = spRepository;
            _billRepository = billRepository;
        }

        public async Task<AlrResult> BuyServiceAction(Guid landlordID, Guid serviceID)
        {
            var landlord = await _userRepository.GetByConditionAsync(x => x.UserEntityID.Equals(landlordID));
            var service = await _serviceRepository.GetByConditionAsync(x => x.serviceId.Equals(serviceID));
            if(service == null || landlord == null) {
                return AlrResult.NullObject;
            }
            if(landlord.AccountBalance < (service.price - service.price*service.discout / 100))
            {
                return AlrResult.Failed;
            }

            var exsitSP = await _spRepository.GetByConditionAsync(x=> x.userId.Equals(landlordID));
            if(exsitSP != null)
            {
                exsitSP.CreatedDate = DateTime.Now;
                exsitSP.AvailableSlot = service.Slot;
                exsitSP.serviceId = serviceID;
                exsitSP.status = 1;
                exsitSP.userId = landlord.UserEntityID;
                exsitSP.Service = service;
                exsitSP.User = landlord;
                _spRepository.UpdateAsync(exsitSP);
                await _spRepository.CommitChangeAsync();
                var billSpExist = new BillHistoryEntity()
                {
                    billId = Guid.NewGuid(),
                    billType = 0,
                    cost = service.price,
                    paymentDate = DateTime.Now,
                    UserEntityID = landlord.UserEntityID,
                    UserEntity = landlord,
                    status = 1,
                    billDescription = service.serviceName,
                };
                _billRepository.InsertAsync(billSpExist);
                await _billRepository.CommitChangeAsync();
                return AlrResult.Success;
            }
            var servicePackage = new ServicesPackageEntity()
            {
                servicePackageId = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                AvailableSlot = service.Slot,
                serviceId = serviceID,
                status = 1,
                userId = landlord.UserEntityID,
                Service = service,
                User = landlord
            };
            _spRepository.InsertAsync(servicePackage);
            await _spRepository.CommitChangeAsync();
            landlord.AccountBalance -= service.price - service.discout / 100;
            _userRepository.UpdateAsync(landlord);
            await _userRepository.CommitChangeAsync();

            var bill = new BillHistoryEntity()
            {
                billId = Guid.NewGuid(),
                billType = 0,
                cost = service.price,
                paymentDate = DateTime.Now,
                UserEntityID = landlord.UserEntityID,
                UserEntity = landlord,
                status = 1,
                billDescription = service.serviceName,
            };
            _billRepository.InsertAsync(bill);
            await _billRepository.CommitChangeAsync();
            return AlrResult.Success;
        }


        public async Task<PagingListDto<ServiceEntity>> GetAllService(int pageIndex, int pageSize)
        {
            try
            {
                var listService = await _serviceRepository.GetDataAsync(x => x.isActived == true );
                if(listService == null)
                {
                    return null;
                }
                PagingListDto<ServiceEntity> result = new PagingListDto<ServiceEntity>()
                {
                    Data = listService.Skip(pageIndex).Take(pageSize).ToList(),
                    TotalCount = listService.Count()
                };
                return result;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<CheckServiceDto> CheckServicesUnExpired(Guid lanlordId, Guid serviceId)
        {
            var landlord = await _userRepository.GetByConditionAsync(x => x.UserEntityID.Equals(lanlordId));
            var service = await _serviceRepository.GetByConditionAsync(x => x.serviceId.Equals(serviceId));
            var exsitSP = await _spRepository.GetByConditionAsync(x => x.serviceId.Equals(serviceId) && x.userId.Equals(lanlordId));
            if(exsitSP == null || exsitSP.AvailableSlot == 0)
            {
                return null;
            }
            CheckServiceDto result = new CheckServiceDto()
            {
                Service = service,
                availableSlot = exsitSP.AvailableSlot,
            };
            return result;
        }

        


    }
}
