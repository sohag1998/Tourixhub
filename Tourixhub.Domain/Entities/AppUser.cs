using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>, IEntity<Guid>
    {
        [Required]
        public string FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool IsDeleted { get; set; }
        // Navigation Property
        public ICollection<Post> Posts { get; set; } = [];
        public ICollection<Like> Likes { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Favorite> Favorites { get; set; } = [];
        public ICollection<Report> Reports { get; set; } = [];
        public ICollection<Story> Stories { get; set; } = [];
    }
}
