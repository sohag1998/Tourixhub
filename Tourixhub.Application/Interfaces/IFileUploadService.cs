using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Application.Interfaces
{
    public interface IFileUploadService
    {
        Task<List<string>> UploadFiles(List<IFormFile> files, string token);
    }
}
