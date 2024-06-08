using ALR.Domain.Entities.Entities;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Domain.Entities
{
    [Table("Post")]
    public class PostEntity
    {

        [Key]
        public Guid postId { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string content { get; set; }

        [Required]
        public Guid userId { get; set; }
        [Required]
        public string Image { get; set; }

        [Required]
        public float roomPrice { get; set; }

        [Required]
        public DateTime publicDate { get; set; }

        [Required]
        public int active { get; set; }
        [Required]
        public int postLevel { get; set; }

        [Required]
        public Guid motelId { get; set; }

        [ForeignKey(nameof(motelId))]
        public virtual MotelEntity motel { get; set; }
        
    }
}
