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

        
        public Guid UserOneId { get; set; }
        

        
        public Guid UserTwoId {  get; set; }


        [ForeignKey("UserOneId")]
        public AppUser UserOne { get; set; }

        [ForeignKey("UserTwoId")]
        public AppUser UserTwo { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    }
}
