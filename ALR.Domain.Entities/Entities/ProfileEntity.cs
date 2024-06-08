using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALR.Domain.Entities
{
    [Table("Profile")]
    public class ProfileEntity
    {
        [Key]
        public Guid ProfileID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string? Image {  get; set; }
        [Required]

        public virtual UserEntity userEntity { get; set; }
    }
}
