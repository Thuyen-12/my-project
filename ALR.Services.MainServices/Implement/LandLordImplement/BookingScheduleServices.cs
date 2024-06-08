using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract.LandLordInterface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement.LandLordImplement
{
    public class BookingScheduleServices : IBookingScheduleService
    {
        private readonly IRepository<BookingScheduleEntity> _bookingScheduleRepository;
        private readonly IRepository<MotelEntity> _motelRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public BookingScheduleServices(IRepository<BookingScheduleEntity> bookingScheduleRepository,
                                        IRepository<MotelEntity> motelRepository, 
                                        IRepository<UserEntity> userRepository,
                                        IMapper mapper)
        {
            _bookingScheduleRepository = bookingScheduleRepository;
            _motelRepository = motelRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<BookingScheduleEntity>> GetListBookingSchedule()
        {
            var listSchedule = await _bookingScheduleRepository.GetDataAsync();
            return listSchedule.ToList();
        }
        public async Task<BookingScheduleEntity> GetScheduleById(Guid scheduleId) {
            var schedule = await _bookingScheduleRepository.GetByConditionAsync(x => x.scheduleID.Equals(scheduleId));
            return schedule;
        }
        public async Task<(AlrResult,BookingScheduleEntity)> CreateSchedule(BookingScheduleDto dto, Guid landlordid, Guid tenantid)
        {
            if (dto == null)
            {
                return (AlrResult.Failed,null);
            }
            var tenant = await _userRepository.GetByConditionAsync(x => x.UserEntityID.Equals(tenantid));
            var landlord = await _userRepository.GetByConditionAsync(x => x.UserEntityID.Equals(landlordid));
            var schedule = _mapper.Map<BookingScheduleEntity>(dto);
            schedule.tenantId = tenant.UserEntityID;
            schedule.landlordId = landlord.UserEntityID;

            _bookingScheduleRepository.InsertAsync(schedule);
            await _bookingScheduleRepository.CommitChangeAsync();
            return (AlrResult.Success,schedule);
        }

        public async Task<(AlrResult,BookingScheduleEntity)> UpdateSchedule(BookingScheduleDto dto, Guid id)
        {
            if (dto == null)
            {
                return (AlrResult.Failed,null);
            }
            var tenant = await _userRepository.GetByConditionAsync(x => x.UserEntityID.Equals(id));
            var schedule = _mapper.Map<BookingScheduleEntity>(dto);
            schedule.tenant = tenant;
            _bookingScheduleRepository.UpdateAsync(schedule);
            await _bookingScheduleRepository.CommitChangeAsync();
            return (AlrResult.Success,schedule);
        }
        public async Task<AlrResult> DeleteSchedule(Guid scheduleId)
        {
            if(scheduleId == null)
            {
                return AlrResult.Failed;
            }
             var schedule =await _bookingScheduleRepository.GetByConditionAsync(x => x.scheduleID.Equals(scheduleId));
            _bookingScheduleRepository.DeleteEntityAsync(schedule);
             await _bookingScheduleRepository.CommitChangeAsync();
            return AlrResult.Success;
        }
    }
}
