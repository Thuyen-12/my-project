using ALR.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Common.Payment
{
    public interface IPaymentService
    {
        public string CreateUrlPayMoney(float money, Guid userId);
        Task<bool> SaveResponsePayment(ResponsePaymentDto responsePaymentDto);
    }
}
