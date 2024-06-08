using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ALR.Services.MainServices.Abstract.LandLordInterface
{
    public interface ILanlordPaymentService
    {
        Task<EnumBase.AlrResult> BuyServiceAction(Guid landlordID, Guid serviceID);
        Task<CheckServiceDto> CheckServicesUnExpired(Guid lanlordId, Guid serviceId);
        Task<PagingListDto<ServiceEntity>> GetAllService(int pageIndex, int PageSize);
    }
}
