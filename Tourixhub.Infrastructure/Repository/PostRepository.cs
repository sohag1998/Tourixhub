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
    public class PostRepository: Repository<Post, Guid>, IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<bool> CreatePostReportAsync(Guid postId, Guid loggedInUserId)
        {
            var post = await _context.Posts
                    .Where(p => p.Id == postId).FirstOrDefaultAsync();
            if(post != null)
            {
                var report = await _context.Reports
                    .FirstOrDefaultAsync(r =>
                    r.AppUserId == loggedInUserId && r.PostId == postId);

                if (report == null)
                {
                    var newReport = new Report
                    {
                        PostId = postId,
                        AppUserId = loggedInUserId,
                        ReportAt = DateTime.UtcNow,
                    };
                    await _context.Reports.AddAsync(newReport);
                    return true;
                }
            }
            return false;
            

        }

        public async Task<List<Post>> GetAllFavoritePostAsync(Guid loggedInUserId)
        {
            var allFavoritedPosts = await _context.Favorites
                        .Include(f => f.Post) // Include Post first
                        .Include(f => f.Post.AppUser)
                        .Include(f => f.Post.Reports)
                        .Include(f => f.Post.Likes)
                        .Include(f => f.Post.Comments).ThenInclude(c => c.AppUser)
                        .Include(f => f.Post.Favorites)
                        .Where(n => n.AppUserId == loggedInUserId &&
                                    !n.Post.IsDeleted &&
                                    n.Post.Reports.Count < 5) // Filter before projection
                        .Select(n => n.Post) // Project to Posts
                        .ToListAsync();


            return allFavoritedPosts;
        }

        public async Task<List<Post>> GetAllPostAsync(Guid loggedInUserId)
        {
            var allPosts = await _context.Posts
                    .Where(n => (n.IsPrivate == false || n.AppUserId == loggedInUserId) && n.Reports.Count < 5 && n.IsDeleted != true)
                    .Include(n => n.AppUser)
                    .Include(n => n.Likes)
                    .Include(n => n.Favorites)
                    .Include(n => n.Reports)
                    .Include(n => n.Comments).ThenInclude(c => c.AppUser)
                    .OrderByDescending(p => p.CreateAt)
                    .ToListAsync();
            return allPosts;
        }

        public async Task<bool> RemovePostAsync(Guid postId)
        {
            var post = await _context.Posts
                    .FirstOrDefaultAsync(p => p.Id == postId && p.IsDeleted != true);
            if (post != null)
            {
                post.IsDeleted = true;

                _context.Posts.Update(post);
                return true;
            }
            return false;
        }

        public async Task<bool> TogglePostFavoriteAsync(Guid postId, Guid loggedInUserId)
        {
            var post = await _context.Posts
                    .Where(p => p.Id == postId).FirstOrDefaultAsync();
            if (post != null)
            {
                var favorite = await _context.Favorites
                    .Where(f => f.AppUserId == loggedInUserId && f.PostId == postId)
                    .FirstOrDefaultAsync();
                if (favorite != null)
                {
                    _context.Remove(favorite);

                }
                else
                {
                    var newFavorite = new Favorite
                    {
                        AppUserId = loggedInUserId,
                        PostId = postId,
                        CreateAt = DateTime.UtcNow
                    };
                    await _context.Favorites.AddAsync(newFavorite);

                }
                return true;
            }
            return false; 
        }

        public async Task<bool> TogglePostLikeAsync(Guid postId, Guid loggedInUserId)
        {
            var post = await _context.Posts
                    .Where(p => p.Id == postId).FirstOrDefaultAsync();
            if (post != null)
            {
                var like = await _context.Likes
                    .Where(f => f.AppUserId == loggedInUserId && f.PostId == postId)
                    .FirstOrDefaultAsync();
                if (like != null)
                {
                    _context.Remove(like);

                }
                else
                {
                    var newLike = new Like
                    {
                        AppUserId = loggedInUserId,
                        PostId = postId,
                        
                    };
                    await _context.Likes.AddAsync(newLike);

                }
                return true;
            }
            return false;

        }

        public async Task<bool> TogglePostVisibilityAsync(Guid postId, Guid loggedInUserId)
        {
            var post = await _context.Posts
                 .Where(p => p.Id == postId && p.AppUserId == loggedInUserId)
                 .FirstOrDefaultAsync();
            if (post != null)
            {
                post.IsPrivate = !post.IsPrivate;
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
