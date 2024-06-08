using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALR.Domain.Entities.Entities
{
    [Table("BillHistory")]
    public class BillHistoryEntity
    {
        [Key]
        public Guid billId { get; set; }
        [Required]
        public Guid UserEntityID { get; set; }
        [Required]
        public int billType { get; set; }
        [Required]
        public DateTime paymentDate { get; set; }
        [Required]
        public float cost { get; set; }
        [Required]
        public string billDescription {  get; set; }
        [Required]
        public int status {  get; set; }

        [ForeignKey(nameof(UserEntityID))]
        public virtual UserEntity? UserEntity { get; set; }
    }
}
