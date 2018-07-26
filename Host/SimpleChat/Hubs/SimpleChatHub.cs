﻿namespace SimpleChat.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class SimpleChatHub : Hub
    {
        public async Task SendMessageAsync(string user, string message)
            => await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
