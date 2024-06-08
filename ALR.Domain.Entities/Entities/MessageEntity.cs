using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    public class MessageEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public Guid BoxChatId { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [ForeignKey(nameof(BoxChatId))]
        public virtual BoxChatEntity BoxChat { get; set; }
        [ForeignKey(nameof(SenderId))]
        public virtual UserEntity Sender { get; set; }
    }
}
