using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract.TenantInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement.TenantImplement
{
    public class TenantBookingScheduleService : ITenantBookingScheduleService
    {
        private readonly IRepository<BookingScheduleEntity> _bookingRepository;
        private readonly IRepository<PostEntity> _postRepository;
        private readonly IRepository<UserEntity> _userRepository;

        public TenantBookingScheduleService(IRepository<BookingScheduleEntity> bookingRepository, IRepository<PostEntity> postRepository, IRepository<UserEntity> userRepository)
        {
            _bookingRepository = bookingRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        public async Task<BookingScheduleEntity> TenantBookingSchedule(CreateBookingScheduleDto dto, Guid postId, Guid tenantId)
        {
            var post = await _postRepository.GetByConditionIncludeThenIncludeAsync(x => x.postId.Equals(postId), includeBuilder: query => query.Include(x => x.motel).ThenInclude(x => x.MotelAddress));
            var tenant = await _userRepository.GetByConditionAsync(x => x.UserEntityID.Equals(tenantId));
            var bookingSchedule = new BookingScheduleEntity()
            {
                scheduleID = Guid.NewGuid(),
                createdDate = DateTime.Now,
                tenantId = tenantId,
                tenant = tenant,
                bookingDate = dto.BookingDate,
                landlordId = post.userId,
                bookingStatus = 0
            };

            if(bookingSchedule == null)
            {
                return null;
            }
            _bookingRepository.InsertAsync(bookingSchedule);
            await _bookingRepository.CommitChangeAsync();
            return bookingSchedule;
        }

        public async Task<PagingListDto<BookingScheduleEntity>> TenantGetOwnRequest(Guid tenantId, int startIndex, int pageSize)
        {
            var listBooking = await _bookingRepository.GetDataDoubleIncludeAsync(x => x.landlord.Profile, x => x.tenant.Profile, x => x.tenantId.Equals(tenantId));
            foreach(var item in listBooking)
            {
                if(item.bookingDate < DateTime.Now && item.bookingStatus == 0)
                {
                    item.bookingStatus = 2;
                    _bookingRepository.UpdateAsync(item);
                    await _bookingRepository.CommitChangeAsync();
                }
            }
            PagingListDto<BookingScheduleEntity> result = new PagingListDto<BookingScheduleEntity>() { 
                Data = listBooking.Skip(startIndex).Take(pageSize).ToList(),
                TotalCount = listBooking.Count()
            };
            return result;
        }

        public async Task<AlrResult> TenantEditOwnRequest(CreateBookingScheduleDto dto,Guid bookingId)
        {
            try
            {
                var booking = await _bookingRepository.GetByConditionAsync(x => x.scheduleID.Equals(bookingId));
                if (booking == null)
                {
                    return AlrResult.Failed;
                }
                booking.bookingDate = dto.BookingDate;
                _bookingRepository.UpdateAsync(booking);
                await _bookingRepository.CommitChangeAsync();
                return AlrResult.Success;
            }
            catch (Exception)
            {

                return AlrResult.Failed;
            }
        }

        public async Task<AlrResult> TenantDeleteOwnBooking(Guid bookingId)
        {
            try
            {
                if(bookingId == null || bookingId.Equals(Guid.Empty))
                {
                    return AlrResult.Failed;
                }
                _bookingRepository.DeleteAsync(x => x.scheduleID.Equals(bookingId));
               await _bookingRepository.CommitChangeAsync();
                return AlrResult.Success;
            }
            catch (Exception)
            {

                return AlrResult.Failed;
            }
        }

        public async Task<UserEntity> GetPostAuthor(Guid userId)
        {
            var user = await _userRepository.GetByConditionIncludeThenIncludeAsync(x => x.UserEntityID.Equals(userId), includeBuilder: query => query.Include(x => x.Profile));
            return user;
        }
    }
}
