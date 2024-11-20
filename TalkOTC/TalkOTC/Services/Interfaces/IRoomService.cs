using TalkOTC.Models.DTOs;

namespace TalkOTC.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDto> CreateRoomAsync(string userId);
        Task JoinRoomAsync(string roomId, string userId);
        Task LeaveRoomAsync(string roomId, string userId);
        Task<bool> HasRoomAccessAsync(string roomId, string userId);
    }
}
