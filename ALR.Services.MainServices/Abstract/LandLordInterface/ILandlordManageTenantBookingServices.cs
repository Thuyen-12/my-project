using ALR.Data.Base;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract.LandLordInterface
{
    public interface ILandlordManageTenantBookingServices
    {
        Task<EnumBase.AlrResult> ConsiderBookingSchedule(Guid bookingId, int bookingStatus);
        Task<BookingScheduleEntity> GetBooking(Guid id);
        Task<PagingListDto<BookingScheduleEntity>> GetListBooking(Guid landlordId, int startIndex, int pageSize);
        Task<UserEntity> GetUser(Guid id);
    }
}
