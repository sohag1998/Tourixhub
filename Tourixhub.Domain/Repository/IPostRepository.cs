using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Domain.Repository
{
    public interface IPostRepository: IRepository<Post, Guid>
    {
        Task<List<Post>> GetAllPostAsync(Guid loggedInUserId);
        Task<List<Post>> GetAllFavoritePostAsync(Guid loggedInUserId);
        Task<bool> RemovePostAsync(Guid postId);
        Task<int?> TogglePostLikeAsync(Guid postId, Guid loggedInUserId);
        Task<bool> TogglePostFavoriteAsync(Guid postId, Guid loggedInUserId);
        Task<bool> TogglePostVisibilityAsync(Guid postId, Guid loggedInUserId);
        Task<bool> CreatePostReportAsync(Guid postId, Guid loggedInUserId);
        
    }
}
