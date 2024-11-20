using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TalkOTC.Data;
using TalkOTC.Data.Entities;
using TalkOTC.Infrastructure;
using TalkOTC.Models.DTOs;
using TalkOTC.Services.Interfaces;
using TalkOTC.Utilities;

namespace TalkOTC.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public RoomService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<RoomDto> CreateRoomAsync(string userId)
        {
            var isAnotherRoomMember = await _appDbContext.UserRooms.AnyAsync(x => x.UserId == userId);

            if (isAnotherRoomMember)
            {
                throw new AppException("You must leave your current room to create a new room!", 401);
            }

            var room = new Room { RoomIdentifier = Utility.GenerateRoomId(), RoomAdminId = userId };

            room.UserRooms.Add(new UserRoom { UserId = userId });
            await _appDbContext.Rooms.AddAsync(room);
            await _appDbContext.SaveChangesAsync();

            var roomDto = _mapper.Map<RoomDto>(room);
            return roomDto;
        }

        public async Task JoinRoomAsync(string roomIdentifier, string userId)
        {
            var isAnotherRoomMember = await _appDbContext.UserRooms.AnyAsync(x => x.UserId == userId);

            if (isAnotherRoomMember)
            {
                throw new AppException("You must leave your current room to join a new room!", 401);
            }

            var room = await _appDbContext.Rooms.FirstOrDefaultAsync(x => x.RoomIdentifier == roomIdentifier);

            if (room != null)
            {
                var userRoom = new UserRoom { RoomId = room.Id, UserId = userId };
                await _appDbContext.UserRooms.AddAsync(userRoom);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new AppException("Room doesn't exist", 400);
            }
        }

        public async Task LeaveRoomAsync(string roomIdentifier, string userId)
        {
            var room = await _appDbContext.Rooms.FirstOrDefaultAsync(x => x.RoomIdentifier == roomIdentifier);
            if (room != null)
            {
                var userRoom = await _appDbContext.UserRooms.FirstOrDefaultAsync(x => x.RoomId == room.Id && x.UserId == userId);
                if (userRoom != null)
                {
                    _appDbContext.Remove(userRoom);
                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    throw new AppException("User is not a member of the room!", 400);
                }
            }
            else
            {
                throw new AppException("Room doesn't exist!", 400);
            }
        }

        public async Task<bool> HasRoomAccessAsync(string roomIdentifier, string userId)
        {
            var room = await _appDbContext.Rooms.FirstOrDefaultAsync(x => x.RoomIdentifier == roomIdentifier);

            if (room != null)
            {
                bool hasAccess = room.UserRooms.Any(x => x.UserId == userId);
                return hasAccess;
            }

            throw new AppException("Room doesn't exist", 400);
        }
    }
}
