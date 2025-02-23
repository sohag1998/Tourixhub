using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Application.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly HttpClient _httpClient;

        public FileUploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<string>> UploadFiles(List<IFormFile> files, string token)
        {
            var fileUrls = new List<string>();
            using(var content = new MultipartFormDataContent())
            {
                foreach(var file in files)
                {
                    var stream = file.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    content.Add(fileContent, "files", file.FileName);
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
               var response = await _httpClient.PostAsync("https://localhost:7025/api/fileupload/upload", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<FileUploadResponse>();
                    if (result?.FileUrls != null)
                    {
                        fileUrls = result.FileUrls;
                    }
                }
            }

            return fileUrls;
        }
    }

    // Helper class to deserialize API response
    public class FileUploadResponse
    {
        public List<string> FileUrls { get; set; }
    }
}
