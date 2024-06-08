using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    public class BoxChatEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string BoxChatName { get; set; }
    }
}
