using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    public class MotelAddress
    {
        [Key]
        public Guid AddressId { get; set; }
        [Required]
        public string MoreDetails { get; set; }
        [Required]
        public int Commune {  get; set; }
        [Required]
        public int District { get; set; }
        [Required]
        public int City { get; set; }
    }
}
