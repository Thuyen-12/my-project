using ALR.Data.Database.Abstract;
using ALR.Data.Dto;
using ALR.Domain.Entities.Entities;
using ALR.Services.MainServices.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.MainServices.Implement
{
    public class MessageService: IMessageService
    {
        public IRepository<MessageEntity> _messageRepository;
        public IMapper _mapper;
        public MessageService(IRepository<MessageEntity> messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<List<MessageViewDto>> GetListMessage(Guid boxChatId)
        {
            var listMessageEntity = await _messageRepository.GetDataIncludeAsync(message => message.BoxChatId.Equals(boxChatId), msg => msg.Sender, sender => sender.Profile);
            var listMessageView = _mapper.Map<List<MessageViewDto>>(listMessageEntity);
            return listMessageView;
        }

        public async Task<MessageEntity> CreateNewMessage(CreateMessageDto createMsgDto)
        {
            var msgEntity = _mapper.Map<MessageEntity>(createMsgDto);
            _messageRepository.InsertAsync(msgEntity);
            await _messageRepository.CommitChangeAsync();
            return msgEntity;
        }
    }
}
