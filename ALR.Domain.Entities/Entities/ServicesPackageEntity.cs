using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    [Table("ServicePackage")]
    public class ServicesPackageEntity
    {
        [Key]
        public Guid servicePackageId { get; set; }
        [Required]
        public Guid userId { get; set; }
        [Required]
        public Guid serviceId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]  
        public int  AvailableSlot { get; set; }
        [Required]
        public int status { get; set; }

        [ForeignKey(nameof(userId))]
        public virtual UserEntity? User { get; set; }

        [ForeignKey(nameof(serviceId))]

        public virtual ServiceEntity? Service { get; set; }
    }
}
