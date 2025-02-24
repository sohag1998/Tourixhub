using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Entities
{
    public class FriendRequest: IEntity<Guid>
    {
        public Guid Id { get; set; }

        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }
        public AppUser Sender { get; set; }

        [ForeignKey("Receiver")]
        public Guid ReceiverId { get; set; }
        public AppUser Receiver { get; set; }


        public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;

        public DateTime CreatAt { get; set; } = DateTime.UtcNow;
    }

    public enum FriendRequestStatus
    {
        Pending,
        Accepted,
        Declined
    }
}
