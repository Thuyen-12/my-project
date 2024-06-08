using ALR.Data.Base;
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
    public interface IRequestServices
    {
        Task<(EnumBase.AlrResult, RequestEntity)> EditRequest(RequestEntity request);
        Task<EnumBase.AlrResult> UpdateRequestStatus(Guid id, int status);
        Task<bool> DeleteRequest(Guid id);
        Task<(EnumBase.AlrResult, RequestEntity)> CreateNewRequest(RequestEntity request);
        Task<RequestDto> GetRequestByTitle(string name);
        Task<PagingListDto<RequestEntity>> GetListRequest(int startIndex, int pageSize, Guid id);
    }
}
