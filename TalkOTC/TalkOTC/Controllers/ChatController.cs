using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TalkOTC.Models.DTOs;
using TalkOTC.Services.Interfaces;

namespace TalkOTC.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task SendMessageAsync([FromBody]MessageDto dto)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _chatService.SendChatMessageAsync(userId, dto);
        }
    }
}
