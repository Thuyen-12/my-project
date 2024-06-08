using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ALR.Domain.Entities.Entities
{
    [Table("Motel")]
    public class MotelEntity
    {
        [Key]
        public Guid motelID { get; set; }
        [Required]
        public Guid UserId {  get; set; }
        [Required]
        public string motelName { get; set; }
        [Required]
        public Guid AddressId { get; set; }
        [Required]
        public string description { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserEntity? Landlord { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual MotelAddress? MotelAddress { get; set; }
    }
}
