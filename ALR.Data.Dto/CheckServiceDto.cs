using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class CheckServiceDto
    {
        public ServiceEntity Service { get; set; }
        public int availableSlot {  get; set; }
    }
}
