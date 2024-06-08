using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class AccountResigterDTO
    {
        public string status;
        public string account { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string userName { get; set; }
        public int gender { get; set; }
        public DateTime DOB { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public int role { get; set; }
       
    }
}
