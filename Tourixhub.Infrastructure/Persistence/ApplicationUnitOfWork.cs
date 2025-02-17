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
            IPostRepository postRepository, ICommentRepository commentRepository) 
            : base(context)
        {
            PostRepository = postRepository;
            CommentRepository = commentRepository;
        }

        public IPostRepository PostRepository { get; }
        public ICommentRepository CommentRepository { get; }
    }
}
