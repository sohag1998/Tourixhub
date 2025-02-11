using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountEndPointController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountEndPointController(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("getprofile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return Unauthorized();  // If no user or not authenticated
            }
            string userId = user.Claims.First(x => x.Type == "UserId").Value;

            var userDetalis = await _userManager.FindByIdAsync(userId);

            return Ok(new
            {
                userDetalis.Email,
                userDetalis.FullName
            });
        }
    }
}
