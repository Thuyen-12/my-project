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
    public interface ILandlordRoomServices
    {
        Task<(RoomEntity, EnumBase.AlrResult)> AddTenantIntoRoom(string phoneNumber, Guid roomId);
        Task<(RoomEntity, EnumBase.AlrResult)> CreateNewRoom(Guid motelId, RoomDto dto);
        Task<EnumBase.AlrResult> DeleteRoom(Guid roomId);
        Task<List<MotelEntity>> GetAllListMotel(Guid id);
        Task<PagingListDto<MotelEntity>> GetListMotelByLandlordId(Guid id, int startIndex, int pageSize);
        Task<PagingListDto<RoomEntity>> GetListRoomByMotelID(Guid motelId, int startIndex, int pageSize);
        Task<List<RoomEntity>> GetListRoomByMotelIDNoPaging(Guid motelId);
        Task<(List<RoomEntity>, EnumBase.AlrResult)> GetRoomByCondition(Guid motelId, string roomNumber, float minPrice, float maxPrice, int status);
        Task<(RoomEntity, EnumBase.AlrResult)> GetRoomByID(Guid roomId);
        Task<(RoomEntity, EnumBase.AlrResult)> GetRoomDetailByID(Guid roomId);
        Task<EnumBase.AlrResult> UpdateRoom(RoomDto dto, Guid guid);
        
    }
}
