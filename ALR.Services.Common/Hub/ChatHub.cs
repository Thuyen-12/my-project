using ALR.Data.Dto;
using ALR.Services.MainServices.Abstract;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Common
{
    public class ChatHub: Hub
    {
        private readonly IMessageService _messageService;
        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<int> SendMessage(CreateMessageDto messageDto)
        {
            var response = await _messageService.CreateNewMessage(messageDto);
            Console.WriteLine(response);
            await Clients.All.SendAsync($"ReceiveMessage_{messageDto.BoxChatId}", messageDto);
            return 1;
        }

    }
}
