using TalkOTC.Models.DTOs;

namespace TalkOTC.Services.Interfaces
{
    public interface IChatService
    {
        Task SendChatMessageAsync(string userId, MessageDto dto);
    }
}
