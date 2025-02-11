using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Helpers;

namespace Tourixhub.Application.Dtos
{
    public class AddPostDto
    {
        [AtLeastOneRequired("ImageUrl", ErrorMessage = "Either Content or ImageUrl must be provided.")]
        public string? Content { get; set; }

        [AtLeastOneRequired("Content", ErrorMessage = "Either Content or ImageUrl must be provided.")]
        public string? ImageUrl { get; set; }
    }
}
