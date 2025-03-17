using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Entities
{
    public class Friendship: IEntity<Guid>
    {
        public Guid Id { get; set; }

        [ForeignKey("UserOne")]
        public Guid UserOneId { get; set; }
        public AppUser UserOne { get; set; }

        [ForeignKey("UserTwo")]
        public Guid UserTwoId { get; set; }
        public AppUser UserTwo { get; set; }
        

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    }
}
