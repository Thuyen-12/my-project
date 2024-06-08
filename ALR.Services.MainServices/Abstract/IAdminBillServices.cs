using ALR.Data.Dto;
using ALR.Data.Dto.BillDto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract
{
    public interface IAdminBillServices
    {
        Task<BillHistoryEntity> CreateNewBill(BillHistoryDto dto);
        Task<PagingListDto<BillHistoryEntity>> GetAllBill(int startIndex, int pageSize);
        Task<List<BillDasboardHistoryDto>> GetBillByYear(int year);
        Task<List<BillHistoryEntity>> GetListBillByYear(int year);
    }
}
