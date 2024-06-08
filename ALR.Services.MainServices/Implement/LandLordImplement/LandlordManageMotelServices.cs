using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement.LandLordImplement
{
    public class LandlordManageMotelServices : ILandlordManageMotelServices
    {
        private readonly IRepository<MotelEntity> _motelRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<MotelAddress> _addRepository;

        public LandlordManageMotelServices(IRepository<MotelEntity> motelRepository, IRepository<UserEntity> userRepository, IRepository<MotelAddress> addRepository)
        {
            _motelRepository = motelRepository;
            _userRepository = userRepository;
            _addRepository = addRepository;
        }

        public async Task<PagingListDto<MotelEntity>> GetAllMotelByLandlordID(Guid lanflordId, int startIndex, int pageSize)
        {
            var listMotel = await _motelRepository.GetDataIncludeAsync(x => x.UserId.Equals(lanflordId), y => y.MotelAddress);
            if (listMotel == null)
            {
                return null;
            }
            PagingListDto<MotelEntity> result = new PagingListDto<MotelEntity>()
            {
                Data = listMotel.Skip(startIndex).Take(pageSize).ToList(),
                TotalCount = listMotel.Count()
            };
            return result;
        }

        public async Task<AlrResult> CreateNewMotel(CreateMotelDto dto, Guid lanlordId)
        {
            try
            {
                var motelAddress = new MotelAddress()
                {
                    AddressId = Guid.NewGuid(),
                    City = dto.City,
                    Commune = dto.Commune,
                    District = dto.District,
                    MoreDetails = dto.MoreDetails,
                };
                var landLord = await _userRepository.GetByConditionAsync(x => x.UserEntityID.Equals(lanlordId));
                if (landLord == null)
                {
                    return AlrResult.Failed;
                }
                 
                
                var motel = new MotelEntity()
                {
                    motelID = Guid.NewGuid(),
                    UserId = lanlordId,
                    MotelAddress = motelAddress,
                    Landlord = landLord,
                    description = dto.description,
                    AddressId = motelAddress.AddressId,
                    motelName = dto.motelName


                };
                _addRepository.InsertAsync(motelAddress);
                _motelRepository.InsertAsync(motel);
                await _motelRepository.CommitChangeAsync();
                return AlrResult.Success;

            }
            catch (Exception)
            {

                return AlrResult.Failed;
            }

            
            
        }

        public async Task<MotelEntity> GetMotelById(Guid motelId)
        {
            var motel = await _motelRepository.GetByConditionAsync(x => x.motelID.Equals(motelId));
            if(motel == null)
            {
                return null;
            }
            return motel;
        }

        public async Task<AlrResult> DeleteMotel(Guid motelId)
        {
            try
            {
                var motel = await _motelRepository.GetByConditionAsync(x => x.motelID.Equals(motelId));
                if (motel == null)
                {
                    return AlrResult.Failed;
                }
                _motelRepository.DeleteEntityAsync(motel);
               await _motelRepository.CommitChangeAsync();
                return AlrResult.Success;
            }
            catch (Exception)
            {

                return AlrResult.Failed;
            }

        }

        public async Task<AlrResult> UpdateMotel(Guid motelId, CreateMotelDto dto)
        {
            try
            {
                var motel = await _motelRepository.GetByConditionIncludeAsync(x => x.MotelAddress,null,x => x.motelID.Equals(motelId));
                if(motel == null)
                {
                    return AlrResult.Failed;
                }
                motel.motelName = dto.motelName;
                motel.description = dto.description;
                motel.MotelAddress.City = dto.City;
                motel.MotelAddress.District = dto.District;
                motel.MotelAddress.MoreDetails = dto.MoreDetails;
                motel.MotelAddress.Commune = dto.Commune;

                _motelRepository.UpdateAsync(motel);
                await _motelRepository.CommitChangeAsync();
                return AlrResult.Success;
            }
            catch (Exception)
            {
                return AlrResult.Failed;
            }
           

        }
    }
}
