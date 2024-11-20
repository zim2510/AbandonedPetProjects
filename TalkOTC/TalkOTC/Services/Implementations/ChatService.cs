using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TalkOTC.Data;
using TalkOTC.Infrastructure;
using TalkOTC.Models.DTOs;
using TalkOTC.Services.Interfaces;
using TalkOTC.SignalR.Clients;
using TalkOTC.SignalR.Hubs;

namespace TalkOTC.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHubContext;
        private readonly AppDbContext _appDbContext;

        public ChatService(IHubContext<ChatHub, IChatClient> chatHubContext, AppDbContext appDbContext)
        {
            _chatHubContext = chatHubContext;
            _appDbContext = appDbContext;
        }

        public async Task SendChatMessageAsync(string userId, MessageDto dto)
        {
            var room = await _appDbContext.Rooms.FirstOrDefaultAsync(x => x.RoomIdentifier == dto.RoomIdentifier);

            if (room == null)
            {
                throw new AppException("Room doesn't exist!", 400);
            }

            if (room.UserRooms.Any(x => x.UserId == userId))
            {
                var otherUserIds = room.UserRooms.Where(x => x.UserId != userId).Select(x => x.UserId).ToList();

                foreach(var otherUserId in otherUserIds)
                {
                    await _chatHubContext.Clients.User(otherUserId).SendChatMessageAsync(dto);
                }
            }
            else
            {
                throw new AppException("You don't have access to the requested room!", 403);
            }
        }
    }
}
