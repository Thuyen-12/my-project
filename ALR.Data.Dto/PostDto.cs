using Microsoft.AspNetCore.Http;

namespace ALR.Data.Dto
{
    public class PostDto
    {
        public string title { get; set; }


        public string content { get; set; }


        public string Image { get; set; }


        public Guid userId { get; set; }


        public DateTime publicDate { get; set; }


        public int active { get; set; }

        public Guid levelId { get; set; }

        public float roomprice { get; set; }
        public int level { get; set; }

        public long viewer { get; set; }
        public List<IFormFile> imageCollections { get; set; }
    }

    public class CreateNewPostDto
    {
        public string title { get; set; }
        public float roomPrice { get; set; }
        public string content { get; set; }
        public List<IFormFile> imageCollections { get; set; }
        public Guid motelId { get; set; }
    }

}
