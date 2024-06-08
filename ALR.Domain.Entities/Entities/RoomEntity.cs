using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    [Table("Room")]
    public class RoomEntity
    {
        [Key]
        public Guid roomId { get; set; }
        [Required]
        public Guid motelId { get; set; }
        [Required]
        public string roomNumber { get; set; }
        [Required]
        public string roomDescription { get; set; }
        [Required]
        public float roomPrice { get; set; }
        [Required]
        public int availableSlot { get; set; }
        [Required]
        public int roomStatus { get; set; }

        [ForeignKey(nameof(motelId))]
        public virtual MotelEntity? Motel { get; set; }

        public virtual ICollection<UserEntity>? Tenants { get; set; }

    }
}
