using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Data.Dto.PagingDto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections;
using static ALR.Data.Base.EnumBase;

namespace ALR.Services.MainServices.Implement
{
    public class PostService : IPostService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<PostEntity> _repository;
        private readonly IRepository<ServiceEntity> _servicerepository;
        private readonly IMapper _mapper;
        private readonly IRepository<UserEntity> _userrepository;
        private readonly IRepository<MotelEntity> _motelRepository;
        private readonly IRepository<ServicesPackageEntity> _spRepository;

        public PostService(IConfiguration configuration,
            IRepository<PostEntity> repository,
            IRepository<ServiceEntity> servicerepository,
            IMapper mapper,
            IRepository<UserEntity> userrepository,
            IRepository<MotelEntity> motelRepository,
            IRepository<ServicesPackageEntity> spRepository)
        {
            _configuration = configuration;
            _repository = repository;
            _servicerepository = servicerepository;
            _mapper = mapper;
            _userrepository = userrepository;
            _motelRepository = motelRepository;
            _spRepository = spRepository;
        }
        public async Task<PagingListDto<PostEntity>> GetAllPost(int startIndex, int pageSize)
        {
            var listPost = await _repository.GetDataAsync();

            if (listPost == null)
            {
                return null;
            }
            PagingListDto<PostEntity> result = new PagingListDto<PostEntity>()
            {
                Data = listPost.Skip(startIndex).Take(pageSize).ToList(),
                TotalCount = listPost.Count()
            };

            return result;
        }


        public async Task<(AlrResult, PostEntity, UserEntity)> GetPostById(Guid id)
        {
            try
            {
                var result = await _repository.GetByConditionIncludeAsync(expressionInclude1: x => x.motel.MotelAddress, expression: x => x.postId.Equals(id)) as PostEntity;
                var resul2 = await _userrepository.GetByConditionAsync(x => x.UserEntityID.Equals(result.userId)) as UserEntity;

                return (AlrResult.Success, result, resul2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PostEntity> CreateNewPost(CreateNewPostDto dto, string urlImage, Guid userId)
        {
            try
            {
                var motel = await _motelRepository.GetByConditionIncludeAsync(x => x.Landlord, y => y.MotelAddress, z => z.motelID.Equals(dto.motelId));
                var servicePackgae = await _spRepository.GetByConditionIncludeAsync(y => y.Service, y => y.User, x => x.userId.Equals(userId)
                );
                
                    var postlevel = 0;
                    if (servicePackgae == null || servicePackgae.AvailableSlot <=0)
                    {
                        postlevel = 0;
                    }
                    else
                    {
                        postlevel = servicePackgae.Service.serviceLevel;

                    }

                    if (motel == null)
                    {
                        return null;
                    }
                    var result = new PostEntity()
                    {
                        postId = Guid.NewGuid(),
                        userId = userId,
                        title = dto.title,
                        content = dto.content,
                        publicDate = DateTime.Now,
                        active = 1,
                        Image = urlImage,
                        roomPrice = dto.roomPrice,
                        motel = motel,
                        postLevel = postlevel,

                    };

                    _repository.InsertAsync(result);
                    await Task.Delay(100);
                    await _repository.CommitChangeAsync();
                    return result;
               
                
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<PostEntity> UpdatePost(EditPostDto post, string urlImage)
        {
            var obj = await _repository.GetByConditionAsync(x => x.postId.Equals(post.postId)) as PostEntity;
            obj.title = post.title;
            obj.content = post.content;
            obj.active = post.active;
            obj.Image = urlImage;
            obj.motelId = post.motelId;
            _repository.UpdateAsync(obj);
            await _repository.CommitChangeAsync();
            return obj;
        }

        public async Task<Boolean> DeletePost(Guid id)
        {
            try
            {
                var obj = await _repository.GetByConditionAsync(x => x.postId.Equals(id)) as PostEntity;
                _repository.DeleteEntityAsync(obj);
                await _repository.CommitChangeAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<AlrResult> HidePost(Guid id, int status)
        {
            try
            {
                var obj = await _repository.GetByConditionAsync(x => x.postId.Equals(id));
                if (obj == null)
                {
                    return AlrResult.Failed;
                }
                obj.active = status;
                _repository.UpdateAsync(obj);
                await _repository.CommitChangeAsync();
                return AlrResult.Success;
            }
            catch (Exception)
            {

                return AlrResult.Failed;
            }

        }

        public async Task<IEnumerable> GetPostByTitle(string name)
        {
            var obj = await _repository.GetDataAsync(x => x.title.Contains(name));
            return obj;
        }

        public async Task<List<MotelEntity>> GetListMotel(Guid landlordId)
        {
            var listMotel = await _motelRepository.GetDataAsync(x => x.UserId.Equals(landlordId));
            if (listMotel == null)
            {
                return null;
            }
            return listMotel.ToList();
        }

        public async Task<MotelEntity> GetMotelByName(string motelName)
        {
            var motel = await _motelRepository.GetByConditionAsync(x => x.motelName.Equals(motelName));
            if (motel == null)
            {
                return null;
            }
            return motel;
        }

        public async Task<PagingListDto<PostEntity>> SearchPostByCondition(string? motelName, string? postTitle, float minPrice, float maxPrice, int commune, int district, int city, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _repository.GetListByConditionIncludeThenIncludeAsync(
                   x => x.active == 1, // Default predicate that always evaluates to true
                  includeBuilder: query => query.Include(x => x.motel).ThenInclude(x => x.MotelAddress));


                if (!string.IsNullOrEmpty(postTitle))
                {
                    result = result.Where(x => x.title.ToLower().Contains(postTitle.ToLower())).ToList();
                }
                if (minPrice > 0)
                {
                    result = result.Where(x => x.roomPrice >= minPrice).ToList();
                }
                if (maxPrice > 0)
                {
                    result = result.Where(x => x.roomPrice <= maxPrice).ToList();
                }
                if (!string.IsNullOrEmpty(motelName))
                {
                    result = result.Where(x => x.motel != null && x.motel.motelName.ToLower().Contains(motelName.ToLower()) || x.motel.motelName.ToLower().Equals(motelName.ToLower())).ToList();
                }
                if (commune != 0)
                {
                    result = result.Where(x => x.motel != null && x.motel.MotelAddress != null &&
                                               x.motel.MotelAddress.Commune == commune).ToList();
                }
                if (district != 0)
                {
                    result = result.Where(x => x.motel != null && x.motel.MotelAddress != null &&
                                               x.motel.MotelAddress.District == district).ToList();
                }
                if (city != 0)
                {
                    result = result.Where(x => x.motel != null && x.motel.MotelAddress != null &&
                                               x.motel.MotelAddress.City == city).ToList();
                }
                result = result.OrderByDescending(x => x.postLevel).ThenByDescending(x => x.publicDate).ToList();
                PagingListDto<PostEntity> postresult = new PagingListDto<PostEntity>()
                {
                    Data = result.Skip(pageIndex).Take(pageSize).ToList(),
                    TotalCount = result.Count()
                };
                return postresult;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public async Task<PagingListDto<PostEntity>> GetListPostByLanlordId(Guid landlordId, int pageIndex, int pageSize)
        {
            var listPost = await _repository.GetDataIncludeAsync(x => x.userId.Equals(landlordId), x => x.motel, x => x.MotelAddress);
            if (listPost == null)
            {
                return null;
            }
            PagingListDto<PostEntity> postresult = new PagingListDto<PostEntity>()
            {
                Data = listPost.Skip(pageIndex).Take(pageSize).ToList(),
                TotalCount = listPost.Count()
            };
            return postresult;
        }



    }
}
