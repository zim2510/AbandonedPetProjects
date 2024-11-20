using Microsoft.AspNetCore.SignalR;
using TalkOTC.SignalR.Clients;

namespace TalkOTC.SignalR.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {

    }
}
