using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tourixhub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly string _uploadFolder = "wwwroot/uploads";

        [HttpPost("upload")]
        [Consumes("multipart/form-data")] // Ensure the API consumes multipart/form-data
        [Produces("application/json")]
        public async Task<IActionResult> UploadFiles([FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No file received.");
            try
            {
                if(!Directory.Exists(_uploadFolder))
                    Directory.CreateDirectory(_uploadFolder);

                var fileUrls = new List<string>();
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(_uploadFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        string fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
                        fileUrls.Add(fileUrl);
                    }
                }

                return Ok(new { Message = "Files uploaded successfully", FileUrls = fileUrls });



            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }
    }
}
