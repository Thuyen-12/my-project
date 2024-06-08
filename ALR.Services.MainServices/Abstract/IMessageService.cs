using ALR.Data.Dto;
using ALR.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Abstract
{
    public interface IMessageService
    {
        Task<List<MessageViewDto>> GetListMessage(Guid boxChatId);
        Task<MessageEntity> CreateNewMessage(CreateMessageDto createMsgDto);
    }
}
