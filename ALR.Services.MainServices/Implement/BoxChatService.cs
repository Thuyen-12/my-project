using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using AutoMapper;
using Azure.Identity;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Implement
{
    public class BoxChatService: IBoxChatService
    {
        public IRepository<BoxChatUserEntity> _boxChatUserRepository;
        public IAdminAccountService _adminAccountService;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<BoxChatEntity> _boxChatRepository;

        public BoxChatService(IRepository<BoxChatUserEntity> boxChatUserRepository, 
            IMapper mapper,
            IRepository<BoxChatEntity> boxChatRepository,
            IAdminAccountService adminAccountService,
            IRepository<UserEntity> userRepository)
        {
            _boxChatUserRepository = boxChatUserRepository;
            _mapper = mapper;
            _boxChatRepository = boxChatRepository;
            _adminAccountService = adminAccountService;
            _userRepository = userRepository;
        }

        public async Task<List<BoxChatEntity>> GetListBoxChat(Guid userId, string userName)
        {
            var listBoxChatUserModel = await _boxChatUserRepository.GetDataIncludeAsync(model => model.UserId.Equals(userId), model => model.BoxChat);
            var listBoxChatEntity = listBoxChatUserModel.Select(x => x.BoxChat).ToList();
            var listBoxChatDto = listBoxChatEntity.Select(boxChat =>
            {
                var nameBoxChat = boxChat.BoxChatName.Split(",").Where(name => !name.Equals(userName));
                boxChat.BoxChatName = String.Join(", ", nameBoxChat);
                return boxChat;
            }).ToList();
            return listBoxChatDto;
        }

        public async Task<int> CreateBoxChat(CreateBoxChatDto createBoxChatDto)
        {
            var listUser = await _adminAccountService.GetListUser(createBoxChatDto.UserIds);
            string boxChatName = string.Empty;
            if (string.IsNullOrEmpty(createBoxChatDto.Name))
            {
                var listUserName = listUser.Select(x => x.Profile.UserName);
                boxChatName = string.Join(",", listUserName);
            }
            else
            {
                boxChatName = createBoxChatDto.Name;
            }
            var boxChatEntity = new BoxChatEntity()
            {
                Id = Guid.NewGuid(),
                BoxChatName = boxChatName
            };
            _boxChatRepository.InsertAsync(boxChatEntity);
            await _boxChatRepository.CommitChangeAsync();
            var listBoxChatUserEntity = createBoxChatDto.UserIds.Select(userId => new BoxChatUserEntity()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                BoxChatId = boxChatEntity.Id
            });
            await _boxChatUserRepository.InsertRangeAsync(listBoxChatUserEntity);
            await _boxChatUserRepository.CommitChangeAsync();
            return 1;
        }

        public async Task<BoxChatEntity> SearchBoxChat(string nameBoxChat)
        {
            var boxChatEntity = await _boxChatRepository.GetByConditionAsync(x => x.BoxChatName.Equals(nameBoxChat));
            return boxChatEntity;
        }

        public async Task<List<UserEntity>> GetUser()
        {
            var listuser = await _userRepository.GetDataAsync();
            return listuser.ToList();
        }
    }
}
