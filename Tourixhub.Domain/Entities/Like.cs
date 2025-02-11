using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Entities
{
    public class Like: IEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AppUserId { get; set; }
        public Guid PostId { get; set; }

        // Navigation property
        public AppUser AppUser { get; set; }
        public Post Post { get; set; }
    }
}
