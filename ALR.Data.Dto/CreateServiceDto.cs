using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class CreateServiceDto
    {
        public string serviceName { get; set; }
        public int serviceLevel { get; set; }

        public DateTime expiredDate { get; set; }
        public float price { get; set; }
        public string description { get; set; } 
        public float discout { get; set; }
        public Boolean isActived { get; set; }
    }
}
