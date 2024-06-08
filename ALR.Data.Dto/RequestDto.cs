using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class RequestDto
    {

        public Guid requestID { get; set; }
        [Required]
        public Guid userID { get; set; }
        [Required]
        public int requestType { get; set; }
        [Required]
        public DateTime requestDate { get; set; }
        [Required]
        public string requestDescription { get; set; }
        [Required]
        public int requestStatus { get; set; }

        public string UserName { get; set; }
    }
}
