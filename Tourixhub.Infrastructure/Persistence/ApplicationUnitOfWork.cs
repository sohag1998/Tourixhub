using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Interfaces;
using Tourixhub.Domain.Repository;

namespace Tourixhub.Infrastructure.Persistence
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(
            ApplicationDbContext context, 
            IPostRepository postRepository, 
            ICommentRepository commentRepository,
            IPostImageRepository postImageRepository,
            IFriendRepository friendRepository,
            IFriendRequestReopsitory friendRequestReopsitory,
            IAppUserRepository appUserRepository,
            IChatRepository chatRepository)
            : base(context)
        {
            AppUserRepository = appUserRepository;
            PostRepository = postRepository;
            CommentRepository = commentRepository;
            PostImageRepository = postImageRepository;
            FriendRepository = friendRepository;
            FriendRequestReopsitory = friendRequestReopsitory;
            ChatRepository = chatRepository;

        }
        public IAppUserRepository AppUserRepository { get;}

        public IPostRepository PostRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public IPostImageRepository PostImageRepository { get; }

        public IFriendRepository FriendRepository { get; }
        public IFriendRequestReopsitory FriendRequestReopsitory { get; }

        public IChatRepository ChatRepository { get; }
    }
}
