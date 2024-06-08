using ALR.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Common.Abstract
{
    public interface IEmailServices
    {
        void AddToCache(string key, string value);
        Task<bool> CanSendMail(string key);
        Task<string> GetValueFromCache(string key);
        Task<string> RandomNumber(int length);
        Task<string> randomPassWord(int length);
        void SendMail(EmailMessage message);
    }
}
