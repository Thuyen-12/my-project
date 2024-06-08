using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class ProfileDto
    {
        public string UserName { get; set; }

        public int Gender { get; set; }

        public DateTime DOB { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
