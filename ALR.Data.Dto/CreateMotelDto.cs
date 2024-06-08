using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class CreateMotelDto
    {
        public string motelName { get; set; }
        public string description { get; set; }
        public string MoreDetails { get; set; }
        public int Commune { get; set; }
        public int District { get; set; }
        public int City { get; set; }
    }
}
