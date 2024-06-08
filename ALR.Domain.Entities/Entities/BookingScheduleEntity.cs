using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    [Table("BookingSchedule")]
    public class BookingScheduleEntity
    {
        [Key]
        public Guid scheduleID { get; set; }
        [Required]
        public Guid landlordId { get; set; }
        [Required]
        public Guid tenantId { get; set; }
        [Required]
        public DateTime createdDate { get; set; }
        [Required]
        public DateTime bookingDate { get; set; }
        [Required]
        public int bookingStatus { get; set; }

        [ForeignKey(nameof(tenantId))]
        public virtual UserEntity? tenant { get; set; }

        [ForeignKey(nameof(landlordId))]
        public virtual UserEntity? landlord { get; set; }
    }
}
