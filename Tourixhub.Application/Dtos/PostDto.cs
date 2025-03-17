using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Application.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreateAt { get; set; }

        // Embed AppUser (Post Owner)
        public AppUserDto AppUser { get; set; }

        // Count data
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int FavoriteCount { get; set; }
        public int ReportCount { get; set; }

        // Users who liked, favorited, or reported this post
        public List<Guid> LikedByUserIds { get; set; } = new List<Guid>();
        public List<Guid> FavoritedByUserIds { get; set; } = new List<Guid>();
        public List<Guid> ReportedByUserIds { get; set; } = new List<Guid>();

        // Comments
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public List<string> Images { get; set; } = new List<string>();
    }

    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }

        // Use AppUserDto instead of raw user info
        public AppUserDto AppUser { get; set; }
    }
    public class AppUserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }

}
