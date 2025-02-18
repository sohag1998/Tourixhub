using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Infrastructure.SignalR
{
    public class LikeHubService : ILikeHubService
    {
        private readonly IHubContext<LikeHub> _hubContext;

        public LikeHubService(IHubContext<LikeHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendLikeUpdate(string postId, int likeCount)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveLikeUpdate", postId, likeCount);
        }
    }
}
