using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Application.Dtos
{
    public class ChatDto
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
