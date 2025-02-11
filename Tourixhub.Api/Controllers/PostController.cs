using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserContextService _userContextService;
        public PostController(IPostService postService, IUserContextService userContextService)
        {
            _postService = postService;
            _userContextService = userContextService;
        }


        [Authorize]
        [HttpPost("addpost")]
        public async Task<IActionResult> AddPost([FromBody] AddPostDto postDto)
        {
            if (ModelState.IsValid)
            {
                var appUserId = _userContextService.AppUserId;
                
                bool isCreated = await _postService.AddPost(postDto, appUserId);

                if(isCreated)
                {
                    return Ok(new { message = "Post created successfully" });
                }

                return BadRequest(new { message = "Failed to create post" });
            }

            return BadRequest(new {message = "Invalid Request"});
        }
    }
}
