using ALR.Data.Database.Abstract;
using ALR.Data.Database.Repositories;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Implement
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IRepository<PostEntity> _postrepository;
        private readonly IRepository<UserEntity> _userrepository;
        private readonly IRepository<FeedbackEntity> _repository;

        public FeedBackService(IRepository<PostEntity> postrepository, IRepository<UserEntity> userrepository, IRepository<FeedbackEntity> repository) 
        {
            _postrepository = postrepository;
            _userrepository = userrepository;
            _repository = repository;
        }

        public async Task<PagingListDto<FeedbackEntity>> GetAllFeedBackByPost(Guid id, int startIndex, int pageSize)
        {
            var listFeedback = await _repository.GetDataIncludeAsync(x => x.PostId.Equals(id), x => x.User, x=>x.Profile);
            PagingListDto<FeedbackEntity> result = new PagingListDto<FeedbackEntity>()
            {
                Data = listFeedback.Skip(startIndex).Take(pageSize).ToList(),
                TotalCount = listFeedback.Count()
            };
            return result;
        }

        public async Task<bool> CheckUserFeedback(Guid postId, Guid tenantId)
        {
            var feedback = await _repository.GetByConditionAsync(x => x.PostId.Equals(postId) && x.userId.Equals(tenantId));
            if(feedback == null)
            {
                return false;
            }
            return true;
        }

        public async Task<FeedbackEntity> CreateFeedback (FeedBackDto dto, Guid postid, Guid userid)
        {
            var post = await _postrepository.GetByConditionIncludeThenIncludeAsync(x => x.postId.Equals(postid), // Default predicate that always evaluates to true
                  includeBuilder: query => query.Include(x => x.motel).ThenInclude(x => x.MotelAddress));
            var user = await _userrepository.GetByConditionIncludeThenIncludeAsync(x => x.UserEntityID.Equals(userid),
                 includeBuilder: query => query.Include(x => x.Room).ThenInclude(x => x.Motel));
            var result = new FeedbackEntity()
            {
                CommentId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Description = dto.Description,
                postEntity = post,
                User = user
            };
            _repository.InsertAsync(result);
            await Task.Delay(100);
            await _repository.CommitChangeAsync();
            return result;
        }

        public async Task<FeedbackEntity> EditFeedback(EditFeedBackDto feedback, Guid tenantid)
        {
            var obj = await _repository.GetByConditionAsync(x => x.CommentId.Equals(feedback.CommentId) && x.userId.Equals(tenantid)) as FeedbackEntity;
           obj.Description = feedback.Description;


            _repository.UpdateAsync(obj);
            await _repository.CommitChangeAsync();
            return obj;
        }

        public async Task<Boolean> DeleteFeedback(Guid id, Guid userid)
        {
            try
            {
                var obj = await _repository.GetByConditionAsync(x => x.CommentId.Equals(id) && x.userId.Equals(userid)) as FeedbackEntity;
                _repository.DeleteEntityAsync(obj);
                await _repository.CommitChangeAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
