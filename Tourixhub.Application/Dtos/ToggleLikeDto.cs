using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Application.Dtos
{
    public class ToggleLikeDto
    {
        [Required]
        public string PostId { get; set; }
    }
}
