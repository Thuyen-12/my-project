using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using AutoMapper;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement
{
    public class BillManageService : IBillManageService
    {
        private readonly IRepository<BillHistoryEntity> _repository;
        private readonly IMapper _mapper;

        public BillManageService(IRepository<BillHistoryEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BillHistoryEntity> CreateNewBill(BillHistoryDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var billEntity = _mapper.Map<BillHistoryEntity>(dto);
            billEntity.billId = Guid.NewGuid();
            billEntity.UserEntityID = dto.userId;
            _repository.InsertAsync(billEntity);
            await _repository.CommitChangeAsync();
            return billEntity;
        }



        public async Task<AlrResult> UpdateBillStatus(Guid billId, int status)
        {
            var billHistory = await _repository.GetByConditionAsync(x => x.billId.Equals(billId));
            if (billHistory == null)
            {
                return AlrResult.Failed;
            }
            billHistory.status = status;
            _repository.UpdateAsync(billHistory);
            await _repository.CommitChangeAsync();
            return AlrResult.Success;

        }

        public async Task<Boolean> DeleteBill(Guid id)
        {
            try
            {
                var obj = await _repository.GetByIDAsync(id) as BillHistoryEntity;
                _repository.DeleteEntityAsync(obj);
                await _repository.CommitChangeAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<PagingListDto<BillHistoryEntity>> GetOwnLandlordBill(Guid lanlordId, int pageIndex, int pageSize)
        {
            var listBill = await _repository.GetOnlyDataIncludeAsync(x => x.UserEntity, x => x.UserEntityID.Equals(lanlordId));
            if (listBill == null)
            {
                return null;
            }
            var result = new PagingListDto<BillHistoryEntity>()
            {
                Data = listBill.OrderByDescending(x => x.paymentDate).Skip(pageIndex).Take(pageSize).ToList(),
                TotalCount = listBill.Count()
            };
            return result;
        }
    }
}
