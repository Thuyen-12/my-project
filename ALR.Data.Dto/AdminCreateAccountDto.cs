using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class AdminCreateAccountDto
    {

        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int UserRole { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
