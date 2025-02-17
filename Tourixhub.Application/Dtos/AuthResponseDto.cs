using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Application.Dtos
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public IEnumerable<ErrorDto>? Errors { get; set; } = new List<ErrorDto>();
    }
    public class ErrorDto
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}
