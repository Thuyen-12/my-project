using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities.Entities
{
    public class FeedbackEntity
    {

        [Key]
        public Guid CommentId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid userId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }


        public Guid PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual PostEntity postEntity { get; set; }

        [ForeignKey("userId")]
        public virtual UserEntity User { get; set; }
    }
}
