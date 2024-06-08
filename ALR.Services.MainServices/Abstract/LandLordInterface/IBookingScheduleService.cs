using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract.LandLordInterface
{
    public interface IBookingScheduleService
    {
        //Task<(EnumBase.AlrResult, BookingScheduleEntity)> CreateSchedule(BookingScheduleDto dto, Guid id);
        Task<(EnumBase.AlrResult, BookingScheduleEntity)> CreateSchedule(BookingScheduleDto dto, Guid landlordid, Guid tenantid);
        Task<EnumBase.AlrResult> DeleteSchedule(Guid scheduleId);
        Task<List<BookingScheduleEntity>> GetListBookingSchedule();
        Task<BookingScheduleEntity> GetScheduleById(Guid scheduleId);
        Task<(EnumBase.AlrResult, BookingScheduleEntity)> UpdateSchedule(BookingScheduleDto dto, Guid id);
    }
}
