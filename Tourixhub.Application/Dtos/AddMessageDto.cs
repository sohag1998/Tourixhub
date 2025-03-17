using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Application.Dtos
{
    public class AddMessageDto
    {
        public Guid ReceiverId { get; set; }
        public string Message { get; set; }
    }
}
