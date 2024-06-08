using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class EditPostDto
    {
        public Guid postId { get; set; }
        public string title { get; set; }
        public string content { get; set; }

        public int active { get; set; }
        public List<IFormFile> imageCollections { get; set; }
        public Guid motelId { get; set; }
    }
}
