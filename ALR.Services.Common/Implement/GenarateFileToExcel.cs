using ALR.Data.Database.Abstract;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ALR.Services.Common.Implement
{
    public class GenarateFileToExcel : IGenarateFileToExcel
    {
        private readonly IRepository<BillHistoryEntity> _repository;
        private readonly IRepository<UserEntity> _userRepository;

        public GenarateFileToExcel(IRepository<BillHistoryEntity> repository, IRepository<UserEntity> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<DataTable> GetTableData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BillId", typeof(Guid));
            table.Columns.Add("Account", typeof(string));
            table.Columns.Add("Bill Type", typeof(int));
            table.Columns.Add("Payment Date", typeof(DateTime));
            table.Columns.Add("Bill Description", typeof(string));
            table.Columns.Add("Cost", typeof(float));

            var listBill = await _repository.GetDataAsync();
            if(listBill.ToList().Count > 0)
            {
                foreach (var item in listBill.ToList())
                {
                    var user = await _userRepository.GetByConditionAsync(x => x.UserEntityID.Equals(item.UserEntityID));
                    table.Rows.Add(item.billId, user.Account, item.billType, item.paymentDate, item.billDescription, item.cost);
                }
            }
            return table;
        }
    }
}
