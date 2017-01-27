using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace SignalRCoreTest
{
    public class NotificationHub : Hub
    {
        private IHttpContextAccessor _contextAccessor;
        private HttpContext _context { get { return _contextAccessor.HttpContext; } }

        public NotificationHub(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task JoinGroup()
        {
            await Groups.AddAsync($"notification_{_context.User.Identity.Name}");
        }

        public void Send(string notification)
        {
            Clients.Client(Context.ConnectionId).InvokeAsync("sendNotification", notification);
        }
    }
}
