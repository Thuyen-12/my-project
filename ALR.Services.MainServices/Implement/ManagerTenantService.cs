using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement
{
    public class ManagerTenantService : IManagerTenantService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<UserEntity> _repository;
        private readonly IRepository<MotelEntity> _motelrepository;
        private readonly IRepository<RoomEntity> _roomrepository;
        private readonly IRepository<UserEntity> _userRepository;

        public ManagerTenantService(IConfiguration configuration, IRepository<UserEntity> repository
            , IRepository<MotelEntity> motelrepository,  IRepository<RoomEntity> roomrepository, IRepository<UserEntity> userRepository)
        {
            _configuration = configuration;
            _repository = repository;
            _motelrepository = motelrepository;
            _roomrepository = roomrepository;
            _userRepository = userRepository;
        }

        //public async Task<PagingListDto<UserEntity>> GetListTenant(Guid landlordId)
        //{
        //    try
        //    {
        //        var motel = new List<MotelEntity>();
                
        //       var users = await _motelrepository.GetDataAsync(x => x.UserId.Equals(landlordId)) as List<TenantManageDto>;

        //        return (user, AlrResult.Success);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
            
        //}

        public async Task<(List<UserEntity>, AlrResult)> AddTenant(Guid landlordId,Guid motelId,Guid roomid, UserEntity dto)
        {
            var motel = new List<MotelEntity>();
            var user = new List<UserEntity>();
            motel = await _motelrepository.GetDataAsync(x => x.UserId.Equals(landlordId)) as List<MotelEntity>;
            foreach (var item in motel)
            {
                List<RoomEntity> room = await _roomrepository.GetDataAsync(x => x.motelId.Equals(motelId)) as List<RoomEntity>;
                foreach(var item1 in room)
                {
                    user = await _repository.GetDataAsync(x =>x.roomId.Equals(roomid)) as List<UserEntity>;
                    user.Add(dto);
                }
            }
            return (user, AlrResult.Success);
        }

        public async Task<bool> DeleteTenant(Guid landlordId, Guid motelId, Guid roomid, UserEntity dto)
        {
            try
            {
                var motel = new List<MotelEntity>();
                motel = await _motelrepository.GetDataAsync(x => x.UserId.Equals(landlordId)) as List<MotelEntity>;
                foreach (var item in motel)
                {
                    List<RoomEntity> room = await _roomrepository.GetDataAsync(x => x.motelId.Equals(motelId)) as List<RoomEntity>;
                    foreach (var item1 in room)
                    {
                        var user = await _repository.GetByConditionAsync(x => x == dto);
                        _repository.DeleteEntityAsync(user);
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

  


    }
}
