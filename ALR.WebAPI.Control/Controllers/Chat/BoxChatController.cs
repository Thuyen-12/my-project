using ALR.Data.Base;
using ALR.Data.Dto;
using ALR.Domain.Entities;
using ALR.Domain.Entities.Entities;
using ALR.Services.Common.AuthorizationFilter;
using ALR.Services.Common.Extentions;
using ALR.Services.MainServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALR.WebAPI.Control.Controllers.Chat
{
    [Route("api/[controller]/BoxChat")]
    [ApiController]   
    public class BoxChatController
    {
        public readonly IBoxChatService _boxChatService;
        public readonly IHttpContextAccessor _context;
        public readonly IMessageService _messageService;
        public BoxChatController(IBoxChatService boxChatService, IMessageService messageService, IHttpContextAccessor context)
        {
            _boxChatService = boxChatService;
            _messageService = messageService;
            _context = context;
        }
        [Authorize]
        [HttpGet("getlistboxchat")]
        public async Task<List<BoxChatEntity>> GetListBoxChat()
        {
            var userId = _context.HttpContext.GetUserId();
            var result = await _boxChatService.GetListBoxChat(Guid.Parse(userId), "test");
            return result;
        }

        [HttpPost("createboxchat")]
        public async Task<int> CreateBoxChat(CreateBoxChatDto createBoxChatDto)
        {
            var userId = _context.HttpContext.GetUserId();
            createBoxChatDto.UserIds.Add(Guid.Parse(userId));
            var result = await _boxChatService.CreateBoxChat(createBoxChatDto);

            return result;
        }

        [HttpGet("getlistboxchat/{boxChatId}")]
        public async Task<List<MessageViewDto>> GetListMessageInBoxChat(Guid boxChatId)
        {
            var result = await _messageService.GetListMessage(boxChatId);
            return result;
        }

        [HttpGet]
        [Route("getListUser")]
        public async Task<List<UserEntity>> GetListUser()
        {
            var result = await _boxChatService.GetUser();
            return result;
        }
    }
}
