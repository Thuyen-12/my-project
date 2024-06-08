using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class AccountProfileDto
    {
        public Guid UserEntityID { get; set; }

        public Guid ProfileID { get; set; }
        public string UserName { get; set; }

        public int Gender { get; set; }

        public DateTime DOB { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
