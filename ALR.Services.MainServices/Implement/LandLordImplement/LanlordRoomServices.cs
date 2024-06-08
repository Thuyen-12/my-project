using ALR.Data.Database.Abstract;
using ALR.Domain.Entities.Entities;
using ALR.Domain.Entities;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALR.Data.Dto;
using static ALR.Data.Base.EnumBase;
using ALR.Data.Database.Repositories;
using ALR.Data.Dto.PagingDto;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2010.Excel;
using ALR.Services.MainServices.Abstract;

namespace ALR.Services.MainServices.Implement.LandLordImplement
{
    public class LanlordRoomServices : ILandlordRoomServices
    {
        private readonly IRepository<RoomEntity> _roomRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<MotelEntity> _motelRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IBoxChatService _boxChatService;
        private readonly IRepository<BoxChatUserEntity> _boxChatUserRepository;

        public LanlordRoomServices(IRepository<RoomEntity> roomRepository, 
            IMapper mapper, IRepository<MotelEntity> motelRepository, 
            IRepository<UserEntity> userRepository, 
            IBoxChatService boxChatService,
            IRepository<BoxChatUserEntity> boxChatUserRepository)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _motelRepository = motelRepository;
            _userRepository = userRepository;
            _boxChatService = boxChatService;
            _boxChatUserRepository = boxChatUserRepository;
        }

        public async Task<(RoomEntity, AlrResult)> CreateNewRoom(Guid motelId, RoomDto dto)
        {
            var room = _mapper.Map<RoomEntity>(dto);
            var motel = await _motelRepository.GetByConditionAsync(x => x.motelID.Equals(motelId));
            room.roomId = Guid.NewGuid();
            room.motelId = motelId;

            if (room == null || motel == null)
            {
                return (null, AlrResult.Failed);
            }
            _roomRepository.InsertAsync(room);
            await _roomRepository.CommitChangeAsync();
            return (room, AlrResult.Success);
        }

        public async Task<AlrResult> UpdateRoom(RoomDto dto, Guid id)
        {
            var room = await _roomRepository.GetByConditionAsync(x => x.roomId.Equals(id)) as RoomEntity;
            room.roomNumber = dto.roomNumber;
            room.roomDescription = dto.roomDescription;
            room.roomPrice = dto.roomPrice;
            if(dto.roomStatus == 0)
            {
                dto.availableSlot = 0;
            }
            room.availableSlot = dto.availableSlot;
            //room.roomStatus = dto.roomStatus;
            if (room == null)
            {
                return (AlrResult.Failed);
            }
            _roomRepository.UpdateAsync(room);
            await _roomRepository.CommitChangeAsync();
            return (AlrResult.Success);
        }

        public async Task<(RoomEntity, AlrResult)> AddTenantIntoRoom(string phoneNumber, Guid roomId)
        {
            var tenant = await _userRepository.GetByConditionAsync(x => x.Profile.PhoneNumber.Equals(phoneNumber) && x.UserRole == 2);
            var room = await _roomRepository.GetByConditionIncludeAsync(x => x.Motel, expression: x => x.roomId == roomId);


            if (room == null || tenant == null)
            {
                return (null, AlrResult.Failed);
            }

            tenant.roomId = room.roomId;
             _userRepository.UpdateAsync(tenant);
            await _userRepository.CommitChangeAsync();

            room.Tenants.Add(tenant);
             _roomRepository.UpdateAsync(room);
            await _roomRepository.CommitChangeAsync();

            var boxChatEntity = await _boxChatService.SearchBoxChat($"{room.Motel.motelName}_{room.Motel.motelID}");
            if (boxChatEntity == null)
            {
                var newBoxChatDto = new CreateBoxChatDto()
                {
                    Name = $"{room.Motel.motelName}_{room.Motel.motelID}",
                    UserIds = new List<Guid>() { room.Motel.UserId, tenant.UserEntityID }
                };
                _ = await _boxChatService.CreateBoxChat(newBoxChatDto);
            }
            else
            {
                var boxChatUserEntity = new BoxChatUserEntity()
                {
                    Id = Guid.NewGuid(),
                    UserId = tenant.UserEntityID,
                    BoxChatId = boxChatEntity.Id
                };
                _boxChatUserRepository.InsertAsync(boxChatUserEntity);
                await _boxChatUserRepository.CommitChangeAsync();
            }

            return (room, AlrResult.Success);
        }

        public async Task<AlrResult> DeleteRoom(Guid roomId)
        {
            var room = await _roomRepository.GetByConditionAsync(x => x.roomId.Equals(roomId));
            if (room == null)
            {
                return AlrResult.Failed;
            }
            _roomRepository.DeleteEntityAsync(room);
            await _roomRepository.CommitChangeAsync();
            return AlrResult.Success;
        }

        public async Task<PagingListDto<RoomEntity>> GetListRoomByMotelID(Guid motelId, int startIndex, int pageSize)
        {
            var listRoom = await _roomRepository.GetDataIncludeAsync(x => x.motelId.Equals(motelId), x => x.Motel, x => x.MotelAddress);
            return new PagingListDto<RoomEntity>()
            {
                Data = listRoom.Skip(startIndex).Take(pageSize),
                TotalCount = listRoom.Count()
            };
        }

        public async Task<List<RoomEntity>> GetListRoomByMotelIDNoPaging(Guid motelId)
        {
            var listRoom = await _roomRepository.GetDataIncludeAsync(x => x.motelId.Equals(motelId), x => x.Motel, x => x.MotelAddress);
            return listRoom.ToList();
        }


        public async Task<(RoomEntity, AlrResult)> GetRoomByID(Guid roomId)
        {
            var room = await _roomRepository.GetByConditionAsync(x => x.roomId.Equals(roomId));
            if (room == null)
            {
                return (null, AlrResult.Failed);
            }
            return (room, AlrResult.Success);
        }
        public async Task<PagingListDto<MotelEntity>> GetListMotelByLandlordId(Guid id, int startIndex, int pageSize)
        {
            var listUserEntity = await _motelRepository.GetDataIncludeAsync(x => x.UserId.Equals(id), x=>x.MotelAddress);
            return new PagingListDto<MotelEntity>()
            {
                Data = listUserEntity.Skip(startIndex).Take(pageSize).ToList(),
                TotalCount = listUserEntity.Count()
            };
        }

        public async Task<List<MotelEntity>> GetAllListMotel(Guid id)
        {
            var listUserEntity = await _motelRepository.GetDataAsync(x => x.UserId.Equals(id));
            return listUserEntity.ToList();
        }



        public async Task<(RoomEntity, AlrResult)> GetRoomDetailByID(Guid roomId)
        {
            var room = await _roomRepository.GetByConditionAsync(x => x.roomId.Equals(roomId));
            if (room == null)
            {
                return (null, AlrResult.Failed);
            }
            return (room, AlrResult.Success);
        }


        public async Task<(List<RoomEntity>, AlrResult)> GetRoomByCondition(Guid motelId, string roomNumber, float minPrice, float maxPrice, int status)
        {
            if (minPrice < 0 || maxPrice < 0)
            {
                return (null, AlrResult.Failed);
            }
            var listRoom = new List<RoomEntity>();
            if (motelId != null)
            {
                listRoom = await _roomRepository.GetDataAsync(x => x.motelId.Equals(motelId)) as List<RoomEntity>;

                if (roomNumber != null)
                {
                    listRoom = await _roomRepository.GetDataAsync(x => x.roomNumber == roomNumber && x.motelId.Equals(motelId)) as List<RoomEntity>;

                    if (minPrice > 0)
                    {
                        listRoom = await _roomRepository.GetDataAsync(x => x.roomNumber == roomNumber && x.motelId.Equals(motelId) && x.roomPrice >= minPrice) as List<RoomEntity>;

                        if (maxPrice > 0)
                        {
                            listRoom = await _roomRepository.GetDataAsync(x => x.roomNumber == roomNumber && x.motelId.Equals(motelId) && x.roomPrice >= minPrice && x.roomPrice <= maxPrice) as List<RoomEntity>;

                            if (status > 0)
                            {
                                listRoom = await _roomRepository.GetDataAsync(x => x.roomNumber.Contains(roomNumber) && x.motelId.Equals(motelId) && x.roomPrice >= minPrice && x.roomPrice <= maxPrice) as List<RoomEntity>;
                            }

                        }
                    }
                }
                return (listRoom.ToList(), AlrResult.Success);

            }
            return (null, AlrResult.Failed);
        }

    }
}
