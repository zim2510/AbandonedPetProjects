using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TalkOTC.Services.Interfaces;

namespace TalkOTC.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IAccountService _accountService;

        public RoomController(IRoomService roomService, IAccountService accountService)
        {
            _roomService = roomService;
            _accountService = accountService;
        }

        [Route("/OpenRoom/{roomIdentifier}")]
        public async Task<IActionResult> Index(string roomIdentifier)
        {
            if (string.IsNullOrWhiteSpace(roomIdentifier))
            {
                return RedirectToAction("Create");
            }

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var hasAccess = await _roomService.HasRoomAccessAsync(roomIdentifier, userId);

            if (!hasAccess)
            {
                return Unauthorized();
            }

            ViewBag.RoomIdentifier = roomIdentifier;
            return View();
        }

        [Route("/CreateRoom")]
        public async Task<IActionResult> CreateAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roomDto = await _roomService.CreateRoomAsync(userId);
            return Redirect($"/OpenRoom/{roomDto.RoomIdentifier}");
        }

        [Route("/JoinRoom/{roomIdentifier}")]
        public async Task<IActionResult> JoinAsync(string roomIdentifier)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _roomService.JoinRoomAsync(roomIdentifier, userId);
            return Redirect($"/OpenRoom/{roomIdentifier}");
        }

        [Route("/LeaveRoom/{roomIdentifier}")]
        public async Task<IActionResult> LeaveAsync(string roomIdentifier)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _roomService.LeaveRoomAsync(roomIdentifier, userId);
            return RedirectToAction("Index", "Home");
        }
    }
}
