using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract
{
    public interface IFeedBackService
    {
        Task<bool> CheckUserFeedback(Guid postId, Guid tenantId);
        Task<FeedbackEntity> CreateFeedback(FeedBackDto dto, Guid postid, Guid userid);
        Task<bool> DeleteFeedback(Guid id, Guid userid);
        Task<FeedbackEntity> EditFeedback(EditFeedBackDto feedback, Guid tenantid);
        Task<PagingListDto<FeedbackEntity>> GetAllFeedBackByPost(Guid id, int startIndex, int pageSize);
    }
}
