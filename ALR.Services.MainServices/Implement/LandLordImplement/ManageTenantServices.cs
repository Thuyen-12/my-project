using ALR.Data.Database.Abstract;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement.LandLordImplement
{
    public class ManageTenantServices : IManageTenantService
    {
        private readonly IRepository<RoomEntity> _roomRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<MotelEntity> _motelRepository;

        public ManageTenantServices(IRepository<RoomEntity> roomRepository, IRepository<UserEntity> userRepository, IRepository<MotelEntity> motelRepository)
        {
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _motelRepository = motelRepository;
        }

        public async Task<(PagingListDto<UserEntity>, AlrResult)> GetUserInMotel(Guid landLordId, int startIndex, int pageSize)
        {
            List<RoomEntity> listRoom = new List<RoomEntity>();
            List<UserEntity> listTenant = new List<UserEntity>();
            var listMotel = await _motelRepository.GetOnlyDataIncludeAsync(x => x.Landlord, y => y.UserId.Equals(landLordId));
            foreach (var motel in listMotel.ToList())
            {
                var rooms = await _roomRepository.GetDataAsync(x => x.motelId.Equals(motel.motelID));
                listRoom.AddRange(rooms.ToList());
            }

            foreach (var room in listRoom.ToList())
            {
                var tenant = await _userRepository.GetDataIncludeAsync(x => x.roomId.Equals(room.roomId), x => x.Profile);
                listTenant.AddRange(tenant.ToList());
            }
            PagingListDto<UserEntity> PagingListDto = new PagingListDto<UserEntity>()
            {
                Data = listTenant.Skip(startIndex).Take(pageSize),
                TotalCount = listTenant.Count()
            };
            return (PagingListDto, AlrResult.Success);

        }

        public async Task<List<UserEntity>> GetUserByName(string name)
        {
            var listuser = await _userRepository.GetDataAsync(x => x.Account.ToLower().Contains(name.ToLower()));
            List<UserEntity> filteredUsers = new List<UserEntity>();

            foreach (var user in listuser)
            {
                if (user.UserRole == 1 || user.UserRole == 2)
                {
                    filteredUsers.Add(user);
                }
            }

            return filteredUsers;

        }
    }
}
