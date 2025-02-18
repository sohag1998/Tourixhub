using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Services
{
    public class PostService: IPostService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ILikeHubService _likeHubService;
        public PostService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper, ILikeHubService likeHubService)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
            _likeHubService = likeHubService;
        }
        public async Task<List<PostDto>> GetAllPostAsync(Guid loggedInUserId)
        {
            var allPosts = await _applicationUnitOfWork.PostRepository.GetAllPostAsync(loggedInUserId);
            var postDtos = _mapper.Map<List<PostDto>>(allPosts);
            return postDtos;
        }
        public async Task<bool> AddPost(AddPostDto postDto, Guid appUserId)
        {
            var post = _mapper.Map<Post>(postDto);
            post.AppUserId = appUserId;
            post.CreateAt = DateTime.UtcNow;
            post.UpdateAt = DateTime.UtcNow;
            await _applicationUnitOfWork.PostRepository.AddAsync(post);
            return await _applicationUnitOfWork.SaveAsync();
        }

        public async Task<int?> TogglePostLikeAsync(ToggleLikeDto likeDto, Guid loggedInUserId)
        {
            try
            {
                var result = await _applicationUnitOfWork.PostRepository.TogglePostLikeAsync(Guid.Parse(likeDto.PostId), loggedInUserId);
                if (result != null)
                {
                    await _likeHubService.SendLikeUpdate(likeDto.PostId, result.Value);
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> AddCommentAsync(AddCommentDto commentDto, Guid loggedInUserId)
        {
            try
            {
                var post = await _applicationUnitOfWork.PostRepository.GetByIdAsync(Guid.Parse(commentDto.PostId));
                if(post != null)
                {
                    var comment = new Comment
                    {
                        PostId = Guid.Parse(commentDto.PostId),
                        Content = commentDto.Content,
                        AppUserId = loggedInUserId,
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };
                    await _applicationUnitOfWork.CommentRepository.AddAsync(comment);
                    await _applicationUnitOfWork.SaveAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


    }
}
