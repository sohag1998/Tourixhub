using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tourixhub.Infrastructure.SignalR
{
    
    public class ApplicationHub: Hub
    {
        private static readonly ConcurrentDictionary<string, string> _connections = new();

        public override Task OnConnectedAsync()
        {
            string userId = Context.GetHttpContext()?.Request.Query["userId"]!;
            if (!string.IsNullOrEmpty(userId) )
            {
                _connections[userId] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var user = _connections.FirstOrDefault(x => x.Value == Context.ConnectionId);
            if (!string.IsNullOrEmpty(user.Key))
            {
                _connections.TryRemove(user.Key, out _);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public static string? GetConnectionId(string userId)
        {
            return _connections.TryGetValue(userId, out var connectionId) ? connectionId : null;
        }
    }
}
