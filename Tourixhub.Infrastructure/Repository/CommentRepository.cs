using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Entities;
using Tourixhub.Domain.Repository;
using Tourixhub.Infrastructure.Persistence;

namespace Tourixhub.Infrastructure.Repository
{
    public class CommentRepository: Repository<Comment, Guid>, ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context):base(context)
        {
            _context = context;   
        }

        public async Task<List<Comment>> GetAllCommentByPostId(Guid postId)
        {
            var comments = await _context.Comments
                            .Where(c => c.PostId == postId)
                            .Include(c => c.AppUser)
                            .OrderByDescending(c => c.CreateAt)
                            .ToListAsync();
            return comments;
        }
    }
}
