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
    public class LandlordManageTenantBookingServices : ILandlordManageTenantBookingServices
    {
        private readonly IRepository<BookingScheduleEntity> _bookingRepository;
        private readonly IRepository<UserEntity> _userRepository;

        public LandlordManageTenantBookingServices(IRepository<BookingScheduleEntity> bookingRepository, IRepository<UserEntity> userRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
        }

        public async Task<PagingListDto<BookingScheduleEntity>> GetListBooking(Guid landlordId, int startIndex, int pageSize)
        {
            var listbooking = await _bookingRepository.GetDataDoubleIncludeAsync(x => x.tenant.Profile,x => x.landlord.Profile, x => x.landlordId.Equals(landlordId));
            foreach (var item in listbooking)
            {
                if (item.bookingDate < DateTime.Now && item.bookingStatus == 0)
                {
                    item.bookingStatus = 2;
                    _bookingRepository.UpdateAsync(item);
                    await _bookingRepository.CommitChangeAsync();
                }
            }
            PagingListDto<BookingScheduleEntity> result = new PagingListDto<BookingScheduleEntity>()
            {
                Data = listbooking.Skip(startIndex).Take(pageSize).ToList(),
                TotalCount = listbooking.Count()
            };
            return result;
        }

        public async Task<AlrResult> ConsiderBookingSchedule(Guid bookingId, int bookingStatus)
        {
            try
            {
                var booking = await _bookingRepository.GetByConditionAsync(x => x.scheduleID.Equals(bookingId));
                if (booking == null)
                {
                    return AlrResult.Failed;
                }
                booking.bookingStatus = bookingStatus;
                _bookingRepository.UpdateAsync(booking);
                await _bookingRepository.CommitChangeAsync();
                return AlrResult.Success;
            }
            catch (Exception)
            {

                return AlrResult.Failed;
            }

        }

        public async Task<UserEntity> GetUser(Guid id)
        {
            var user = await _userRepository.GetByConditionAsync(x => x.UserEntityID.Equals(id));
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<BookingScheduleEntity> GetBooking(Guid id)
        {
            var booking = await _bookingRepository.GetByConditionAsync(x => x.scheduleID.Equals(id));
            if (booking == null)
            {
                return null;
            }
            return booking;
        }
    }
}
