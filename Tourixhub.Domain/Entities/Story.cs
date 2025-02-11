using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Entities
{
    public class Story: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreateAt { get; set; }

        // FK
        public Guid AppUserId { get; set; }

        // Navigation
        public AppUser AppUser { get; set; }
    }
}
