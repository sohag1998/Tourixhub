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
        public PostService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
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
    }
}
