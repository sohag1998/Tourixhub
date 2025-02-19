using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;

namespace Tourixhub.Application.Interfaces
{
    public interface IPostHubService
    {
        Task SendLikeUpdate(string postId, int likeCount);
        Task SendCommentUpdateAsync(string postId, List<CommentDto> comments);

    }
}
