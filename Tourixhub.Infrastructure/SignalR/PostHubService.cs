using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Infrastructure.SignalR
{
    public class PostHubService: IPostHubService
    {
        private readonly IHubContext<ApplicationHub> _hubContext;

        public PostHubService(IHubContext<ApplicationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendLikeUpdate(string postId, int likeCount)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveLikeUpdate", postId, likeCount);
        }
        public async Task SendCommentUpdateAsync(string postId, List<CommentDto> comments)
        {
            await _hubContext.Clients.All.SendAsync("ReceivedCommentUpdate", postId, comments);     
        }
    }
}
