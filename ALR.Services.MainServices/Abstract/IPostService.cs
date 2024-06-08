using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using System.Collections;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Abstract
{
    public interface IPostService
    {
        Task<PostEntity> CreateNewPost(CreateNewPostDto dto, string urlImage, Guid userId);
        Task<bool> DeletePost(Guid id);
        Task<PagingListDto<PostEntity>> GetAllPost(int startIndex, int pageSize);
        Task<List<MotelEntity>> GetListMotel(Guid landlordId);
        Task<PagingListDto<PostEntity>> GetListPostByLanlordId(Guid landlordId, int pageIndex, int pageSize);
        Task<MotelEntity> GetMotelByName(string motelName);

        //Task<List<PostDto>> GetAllPostByService();
        //Task<(AlrResult, PostEntity)> GetPostById(Guid id);
        Task<(AlrResult, PostEntity, UserEntity)> GetPostById(Guid id);
        Task<IEnumerable> GetPostByTitle(string name);
        Task<EnumBase.AlrResult> HidePost(Guid id, int status);
        Task<PagingListDto<PostEntity>> SearchPostByCondition(string? motelName, string? postTitle, float minPrice, float maxPrice, int commune, int district, int city, int pageIndex, int pageSize);
        Task<PostEntity> UpdatePost(EditPostDto post, string urlImage);
    }
}
