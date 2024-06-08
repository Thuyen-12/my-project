using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Services.Common.Abstract;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using static ALR.Data.Base.EnumBase;

namespace ALR.WebAPI.Control.Controllers.Landlord
{
    [Route("api/[controller]/landlord")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostService _postService;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _context;
        private readonly IDeserializerAddress _addRepository;

        public PostController(IPostService postService, IWebHostEnvironment environment, IHttpContextAccessor context, IDeserializerAddress addRepository)
        {
            _postService = postService;
            _context = context;
            _addRepository = addRepository;
            _environment = environment;
        }


        [HttpGet]
        [Route("getAll")]

        public async Task<IActionResult> GetAllPost(int startIndex, int pageSize)
        {
            var allPost = await _postService.GetAllPost(startIndex, pageSize);
            if (allPost == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return Ok(allPost);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdatePost([FromForm] EditPostDto post)
        {
            var listPath = new List<string>();
            foreach (var fileInfo in post.imageCollections)
            {
                var file = fileInfo;
                string webRootPath = _environment.WebRootPath;
                string imagePath = Path.Combine(webRootPath, file.FileName);
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                listPath.Add(file.FileName);
            }

            var urlImage = string.Join(";", listPath);
            var updateResult = await _postService.UpdatePost(post, urlImage);
            return Ok(updateResult);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var deleteStatus = await _postService.DeletePost(id);
            return Ok(deleteStatus);
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNewPost([FromForm] CreateNewPostDto dto)
        {
            try
            {
                var userId = Guid.Parse(_context.HttpContext.GetUserId());
                var listPath = new List<string>();
                foreach (var fileInfo in dto.imageCollections)
                {
                    var file = fileInfo;
                    string webRootPath = _environment.WebRootPath;
                    string imagePath = Path.Combine(webRootPath, file.FileName);
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    listPath.Add(file.FileName);
                }

                var urlImage = string.Join(";", listPath);
                var result = await _postService.CreateNewPost(dto, urlImage, userId);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,"Bạn không còn lượt đăng tin");
                }
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("hidepost")]
        public async Task<IActionResult> HidePost(Guid id, int status)
        {
            var result = await _postService.HidePost(id, status);
            if (result.Equals(AlrResult.Failed))
            {
                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var getpostById = await _postService.GetPostById(id);
            return Ok(getpostById.Item2);
        }

        [HttpGet]
        [Route("SearchByTitle")]
        public async Task<IActionResult> GetPostByTitle(string name)
        {
            var getpostByTitle = await _postService.GetPostByTitle(name);
            return Ok(getpostByTitle);

        }

        [HttpGet]
        [Route("getpostbycondition")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchPostByCondition(string? motelName, string? postTitle, float minPrice,
            float maxPrice, int commune, int district, int city, int startIndex, int pageSize)
        {
            var listpost = await _postService.SearchPostByCondition(motelName, postTitle, minPrice, maxPrice, commune, district, city, startIndex, pageSize);
            return StatusCode(StatusCodes.Status200OK, listpost);
        }

        [HttpGet]
        [Route("getlistpostbylanlordId")]
        [Authorize]
        public async Task<IActionResult> GetListPostByLandlordId(int startIndex, int pageSize)
        {
            var currentUserId = _context.HttpContext.GetUserId();
            var listPost = await _postService.GetListPostByLanlordId(Guid.Parse(currentUserId), startIndex, pageSize);
            if (listPost == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status200OK, listPost);
        }
    }
}
