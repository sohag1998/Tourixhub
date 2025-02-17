using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;
using Tourixhub.Application.Services;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Api.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserContextService _userContextService;
        private readonly Guid _singinUserId;
        public PostController(IPostService postService, IUserContextService userContextService)
        {
            _postService = postService;
            _userContextService = userContextService;
            _singinUserId = _userContextService.AppUserId;
        }


        
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
       
        
        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPost()
        {
            var posts = await _postService.GetAllPostAsync(_singinUserId);
            //return Ok(posts);
            if (posts != null)
            {
                return Ok(posts);
            }
            else
                return NotFound();
            
        }
       
        [HttpPost("togglelike")]
        public async Task<IActionResult> ToggleLike([FromBody] ToggleLikeDto likeDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _postService.TogglePostLikeAsync(likeDto, _singinUserId));
            }
            return Ok(new { message = "Invaid data" });
        }

        [HttpPost("addcomment")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentDto commentDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _postService.AddCommentAsync(commentDto, _singinUserId));
            }
            return NotFound();
        }
    }
}
