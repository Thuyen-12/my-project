using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    [Table("Service")]
    public class ServiceEntity
    {
        [Key]
        public Guid serviceId { get; set; }
        [Required]
        public string serviceName { get; set; }
        [Required]
        public int serviceLevel { get; set; }
        [Required]
        public DateTime expiredDate { get; set; }
        [Required]  
        public float price { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public float discout {  get; set; }
        [Required]
        public int Slot {  get; set; }
        [Required]
        public Boolean isActived { get; set; }

    }
}
