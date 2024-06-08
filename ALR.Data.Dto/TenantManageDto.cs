using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class TenantManageDto
    {
        public Guid UserEntityID { get; set; }

        public string Email { get; set; }


        public int UserRole { get; set; }

  
        public bool Active { get; set; }


        public Guid ProfileID { get; set; }


        public Guid? roomId { get; set; }

        public Guid? landlordId { get; set; }

        public Guid? motelId { get; set; }

    }
}
