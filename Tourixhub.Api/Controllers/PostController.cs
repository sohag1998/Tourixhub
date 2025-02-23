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
        [Consumes("multipart/form-data")] // Specifies the content type to accept
        [Produces("application/json")]
        public async Task<IActionResult> AddPost([FromForm] AddPostDto postDto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            //if (string.IsNullOrWhiteSpace(postDto.Content) || (postDto.Images == null || postDto.Images.Count < 0))
            //{
            //    return BadRequest("Either Content or at least one Image must be provided.");
            //}
            if (!ModelState.IsValid)
            {
                return BadRequest("Either Content or at least one Image must be provided.");
            }
            try
            {
                var appUserId = _userContextService.AppUserId;
                
                var post = await _postService.AddPost(postDto, appUserId, token);

                if(post != null)
                {
                    return Ok(new { message = "Post created successfully", post = post });
                }

                return BadRequest(new { message = "Failed to create post" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
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
                var updatedLikeCount = await _postService.TogglePostLikeAsync(likeDto, _singinUserId);
                if(updatedLikeCount != null)
                {
                    return Ok(new { likeCout = updatedLikeCount });
                }
                return BadRequest("Failed to toggle like");
            }

            return BadRequest("Invalid data");
        }

        [HttpPost("addcomment")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentDto commentDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { postId = commentDto.PostId, comment = await _postService.AddCommentAsync(commentDto, _singinUserId)});
            }
            return BadRequest(new {error = "Invalid form"});
        }

        [HttpGet("commentsbypostId")]
        public async Task<IActionResult> GetCommentsByPostId(string postId)
        {
            if (!ModelState.IsValid) return BadRequest(new { isSuccess = false, error = "Invalid post id" });
            var result = await _postService.GetAllCommentByPostId(Guid.Parse(postId));
            return Ok(result);
        }
    }
}
