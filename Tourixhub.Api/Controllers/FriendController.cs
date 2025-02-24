using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _friendService;
        private readonly IUserContextService _userContextService;
        private readonly Guid _signinUserId;
        public FriendController(IFriendService friendService, IUserContextService userContextService)
        {
            _friendService = friendService;
            _userContextService = userContextService;
            _signinUserId = _userContextService.AppUserId;
        }

        [HttpPost("sendfriendrequest")]
        public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequstDto requstDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, error = "Invalid request" });
            var isRequestSuccess = await _friendService.SendFriendRequestAsync(requstDto, _signinUserId);
            if (isRequestSuccess)
            {
                return Ok(new { success = true, message = "Request is send"});
            }

            return BadRequest(new { success = false, message = "Invalid User or already a friend" });
            
        }
    }
}
