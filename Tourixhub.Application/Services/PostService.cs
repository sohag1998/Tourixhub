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
        private readonly IFileUploadService _fileUploadService;
        public PostService(
            IApplicationUnitOfWork applicationUnitOfWork, 
            IMapper mapper,
            IPostHubService postHubService,
            IFileUploadService fileUploadService
            )
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
            _postHubService = postHubService;
            _fileUploadService = fileUploadService;
        }
        public async Task<List<PostDto>> GetAllPostAsync(Guid loggedInUserId)
        {
            var allPosts = await _applicationUnitOfWork.PostRepository.GetAllPostAsync(loggedInUserId);
            var postDtos = _mapper.Map<List<PostDto>>(allPosts);
            return postDtos;
        }
        public async Task<PostDto?> AddPost(AddPostDto postDto, Guid appUserId, string token)
        {
            try
            {
                var post = new Post
                {
                    AppUserId = appUserId,
                    Content = postDto.Content !=null? postDto.Content : string.Empty,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                await _applicationUnitOfWork.PostRepository.AddAsync(post);

                var postImages = new List<PostImage>();

                if (postDto.Images != null && postDto.Images.Count > 0)
                {
                   var fileUrls = await _fileUploadService.UploadFiles(postDto.Images, token);
                    foreach ( var fileUrl in fileUrls )
                    {
                        postImages.Add(new PostImage
                        {
                            PostId = post.Id,
                            ImageUrl = fileUrl,
                        });
                    }
                    await _applicationUnitOfWork.PostImageRepository.AddRangeAsync(postImages);
                }

                await _applicationUnitOfWork.SaveAsync();
                var newPost = await _applicationUnitOfWork.PostRepository.GetByIdAsync(post.Id);


                return _mapper.Map<PostDto>(newPost);

                
            }
            catch
            {
                return null;
            }
            
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
