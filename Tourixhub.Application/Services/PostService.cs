using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Services
{
    public class PostService: IPostService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IPostHubService _postHubService;
        public PostService(
            IApplicationUnitOfWork applicationUnitOfWork, 
            IMapper mapper,
            IPostHubService postHubService
            )
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
            _postHubService = postHubService;
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
                    //await _applicationUnitOfWork.SaveAsync();
                    await _postHubService.SendLikeUpdate(likeDto.PostId, result.Value);

                }
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<CommentDto?> AddCommentAsync(AddCommentDto commentDto, Guid loggedInUserId)
        {
            try
            {
                var postId = Guid.Parse(commentDto.PostId);
                var post = await _applicationUnitOfWork.PostRepository.GetByIdAsync(postId);
                if (post == null) return null;
                var comment = new Comment
                {
                    PostId = postId,
                    Content = commentDto.Content,
                    AppUserId = loggedInUserId,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                await _applicationUnitOfWork.CommentRepository.AddAsync(comment);
                await _applicationUnitOfWork.SaveAsync();
                var comments = await this.GetAllCommentByPostId(postId);
                var latestcomment = comments.First();
                await _postHubService.SendCommentUpdateAsync(commentDto.PostId, comments);
                return latestcomment;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CommentDto>> GetAllCommentByPostId(Guid postId)
        {
            var comments = await _applicationUnitOfWork.CommentRepository.GetAllCommentByPostId(postId);

            var mappedComments = _mapper.Map<List<CommentDto>>(comments);

            return mappedComments;
        }


    }
}
