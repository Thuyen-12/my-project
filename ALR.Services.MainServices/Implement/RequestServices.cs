using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement
{
    public class RequestServices: IRequestServices
    {
        private readonly IRepository<RequestEntity> _repository;
        private readonly IMapper _mapper;

        public RequestServices(IRepository<RequestEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AlrResult> UpdateRequestStatus(Guid id, int status)
        {
            var request = await _repository.GetByConditionAsync(x => x.requestID.Equals(id));
            if (request == null)
            {
                return AlrResult.Failed;
            }
            request.requestStatus = status;
             _repository.UpdateAsync(request);
            var result =  _repository.CommitChangeAsync();
            if(result == null)
            {
                return AlrResult.Failed;
            }
            return AlrResult.Success;
        }

        public async Task<PagingListDto<RequestEntity>> GetListRequest(int startIndex, int pageSize, Guid id)
        {
            try
            {
                var listRequest = await _repository.GetDataAsync(x => x.userID.Equals(id));
                PagingListDto<RequestEntity> result = new PagingListDto<RequestEntity>()
                {
                    Data = listRequest.Skip(startIndex).Take(pageSize).ToList(),
                    TotalCount = listRequest.Count()
                };
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Đã có lỗi khi nhận dữ liệu");
            }
        }

        public async Task<(AlrResult,RequestEntity)> CreateNewRequest(RequestEntity request)
        {
            var result = new RequestEntity()
            {
                requestID = Guid.NewGuid(),
                userID = Guid.NewGuid(),
                requestType = 1,
                requestDescription = request.requestDescription,
                requestDate = DateTime.Now,
                requestStatus = 1
            };
            _repository.InsertAsync(result);
            await _repository.CommitChangeAsync();
            return (AlrResult.Success,result);
        }

        public async Task<(AlrResult,RequestEntity)> EditRequest(RequestEntity request)
        {
            var obj = await _repository.GetByIDAsync(request.requestID) as RequestEntity;
            obj.requestType = request.requestType;
            obj.requestDate = request.requestDate;
            obj.requestDescription = request.requestDescription;
            obj.requestStatus = request.requestStatus;

            _repository.UpdateAsync(obj);
            await _repository.CommitChangeAsync();
            if(obj == null)
            {
                return (AlrResult.Failed,null);
            }
            return (AlrResult.Success,obj);
        }

        public async Task<Boolean> DeleteRequest(Guid id)
        {
            try
            {
                var obj = await _repository.GetByConditionAsync(x => x.requestID.Equals(id)) as RequestEntity;
                _repository.DeleteEntityAsync(obj);
                _repository.CommitChangeAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<RequestDto> GetRequestByTitle(string name)
        {
            var obj = await _repository.GetByConditionAsync(x => x.userEntity.Profile.UserName.Contains(name));
            var dto = _mapper.Map<RequestDto>(obj);
            return dto;
        }
    }
}
