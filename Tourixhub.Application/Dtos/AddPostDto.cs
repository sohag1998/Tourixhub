using Microsoft.AspNetCore.Http;
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
        [AtLeastOneRequired("Images", ErrorMessage = "Either Content or at least one Image must be provided.")]
        public string? Content { get; set; }

        [AtLeastOneRequired("Content", ErrorMessage = "Either Content or at least one Image must be provided.")]
        public List<IFormFile>? Images { get; set; }
    }
}
