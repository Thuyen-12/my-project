using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract
{
    public interface IBoxChatService
    {
        Task<List<BoxChatEntity>> GetListBoxChat(Guid userId, string userName);
        Task<int> CreateBoxChat(CreateBoxChatDto createBoxChatDto);
        Task<BoxChatEntity> SearchBoxChat(string nameBoxChat);
        Task<List<UserEntity>> GetUser();
    }
}
