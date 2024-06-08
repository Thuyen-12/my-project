using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Data.Dto.BillDto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Implement
{
    public class AdminBillServices : IAdminBillServices
    {
        private readonly IRepository<BillHistoryEntity> _repository;
        private readonly IMapper _mapper;
        public AdminBillServices(IRepository<BillHistoryEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagingListDto<BillHistoryEntity>> GetAllBill(int startIndex, int pageSize)
        {
            try
            {
                var obj = await _repository.GetOnlyDataIncludeAsync(x => x.UserEntity);
                int totalCount = obj.Count();
                PagingListDto<BillHistoryEntity> result = new PagingListDto<BillHistoryEntity>()
                {
                    Data = obj.OrderByDescending(x => x.paymentDate).Skip(startIndex).Take(pageSize).ToList(),
                    TotalCount = totalCount
                };
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BillHistoryEntity> CreateNewBill(BillHistoryDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = _mapper.Map<BillHistoryEntity>(dto);
            result.billId = Guid.NewGuid();
            result.UserEntityID = Guid.NewGuid();
            return result;
        }

        public async Task<List<BillDasboardHistoryDto>> GetBillByYear(int year)
        {
            try
            {
                if (year != 0)
                {
                   
                    var billHistory = await _repository.GetDataAsync(x => x.billType == 1 && x.paymentDate.Year == year);
                    var monthlySummary = billHistory
                            .Where(b => b.paymentDate.Year == year)
                            .GroupBy(b => new { Month = b.paymentDate.Month })
                            .Select(g => new BillDasboardHistoryDto
                            {
                                    Month = g.Key.Month,
                                    TotalMoney = g.Sum(b => b.cost)
                                })
                            .ToList();
                    return monthlySummary;
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<List<BillHistoryEntity>> GetListBillByYear(int year)
        {
            var listBill = await _repository.GetOnlyDataIncludeAsync(x => x.UserEntity, x => x.paymentDate.Year == year && x.billType == 1);
            if(listBill == null)
            {
                return new List<BillHistoryEntity>();
            }
            return listBill.ToList();
        }
       
    }
}
