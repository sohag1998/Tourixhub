using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Dtos
{
    public class FriendDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
