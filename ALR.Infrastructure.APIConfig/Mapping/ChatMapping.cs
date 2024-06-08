using ALR.Data.Dto;
using ALR.Domain.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Infrastructure.APIConfig.Mapping
{
    public class ChatMapping: Profile
    {
        public ChatMapping()
        {
            CreateMap<CreateMessageDto, MessageEntity>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<MessageEntity, MessageViewDto>()
                .ForMember(des => des.SenderName, opt => opt.MapFrom(src => src.Sender.Profile.UserName));
        }
    }
}
