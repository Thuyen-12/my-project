using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;

namespace ALR.Services.MainServices.Abstract
{
    public interface IBillManageService
    {
        Task<BillHistoryEntity> CreateNewBill(BillHistoryDto dto);
        Task<bool> DeleteBill(Guid id);
        Task<PagingListDto<BillHistoryEntity>> GetOwnLandlordBill(Guid lanlordId, int pageIndex, int pageSize);
        Task<EnumBase.AlrResult> UpdateBillStatus(Guid billId, int status);
    }
}
