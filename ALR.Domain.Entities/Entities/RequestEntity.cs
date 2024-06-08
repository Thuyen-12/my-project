using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    [Table("Request")]
    public class RequestEntity
    {
        [Key]
        public Guid requestID { get; set; }
        [Required]
        public Guid userID { get; set; }
        [Required]
        public int requestType {  get; set; }
        [Required]
        public DateTime requestDate { get; set; }
        [Required]
        public string requestDescription { get; set; }
        [Required]
        public int requestStatus { get; set; }

        [ForeignKey(nameof(userID))]
        public virtual UserEntity userEntity { get; set; }
    }
}
