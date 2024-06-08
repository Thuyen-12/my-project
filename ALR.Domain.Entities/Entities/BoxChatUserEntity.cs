using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    public class BoxChatUserEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid BoxChatId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }
        [ForeignKey(nameof(BoxChatId))]
        public virtual BoxChatEntity BoxChat { get; set; }
    }
}
