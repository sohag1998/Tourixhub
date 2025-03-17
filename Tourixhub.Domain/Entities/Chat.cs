using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Entities
{
    public class Chat : IEntity<Guid>
    {
        public Guid Id { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }

        [ForeignKey("Receiver")]
        public Guid ReceiverId { get; set; }

        public AppUser Sender { get; set; }

        public AppUser Receiver { get; set; }
    }
}
