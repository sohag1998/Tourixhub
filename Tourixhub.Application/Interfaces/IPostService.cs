using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDto>> GetAllPostAsync(Guid loggedInUserId);
        Task<bool> AddPost(AddPostDto postDto, Guid appUserId);
        Task<int?> TogglePostLikeAsync(ToggleLikeDto likeDto, Guid loggedInUserId);
        Task<CommentDto?> AddCommentAsync(AddCommentDto commentDto, Guid loggedInUserId);
        Task<List<CommentDto>> GetAllCommentByPostId(Guid postId);
    }
}
