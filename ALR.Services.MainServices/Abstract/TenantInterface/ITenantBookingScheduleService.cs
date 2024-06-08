using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract.TenantInterface
{
    public interface ITenantBookingScheduleService
    {
        Task<UserEntity> GetPostAuthor(Guid userId);
        Task<BookingScheduleEntity> TenantBookingSchedule(CreateBookingScheduleDto dto, Guid postId, Guid tenantId);
        Task<EnumBase.AlrResult> TenantDeleteOwnBooking(Guid bookingId);
        Task<EnumBase.AlrResult> TenantEditOwnRequest(CreateBookingScheduleDto dto, Guid bookingId);
        Task<PagingListDto<BookingScheduleEntity>> TenantGetOwnRequest(Guid tenantId, int startIndex, int pageSize);
    }
}
