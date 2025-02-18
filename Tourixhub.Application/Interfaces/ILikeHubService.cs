using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Application.Interfaces
{
    public interface ILikeHubService
    {
        Task SendLikeUpdate(string postId, int likeCount);
    }
}
