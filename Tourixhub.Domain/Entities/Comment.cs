using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Entities
{
    public class Comment: IEntity<Guid>
    {
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Guid AppUserId { get; set; }
        public Guid PostId { get; set; }

        // Navigation property
        public AppUser AppUser { get; set; } = new AppUser();
        public Post Post { get; set; } = new Post();
    }
}
