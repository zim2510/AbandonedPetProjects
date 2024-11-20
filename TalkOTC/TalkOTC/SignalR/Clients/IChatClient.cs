using TalkOTC.Models.DTOs;

namespace TalkOTC.SignalR.Clients
{
    public interface IChatClient
    {
        Task SendChatMessageAsync(MessageDto dto);
    }
}
