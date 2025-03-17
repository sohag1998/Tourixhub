using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;
using Tourixhub.Application.Services;

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
        public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequestDto requstDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, error = "Invalid request" });
            if(requstDto.ReceiverId == _signinUserId)
            {
                return BadRequest(new {success = false, error="You can't send request to yourself"});
            }

            var isRequestSuccess = await _friendService.SendFriendRequestAsync(requstDto, _signinUserId);
            if (isRequestSuccess)
            {
                return Ok(new { success = true, message = "Request is send"});
            }

            return BadRequest(new { success = false, error = "Invalid User or already a friend" });
            
        }

        [HttpGet("receivedrequests")]
        public async Task<IActionResult> GetReceivedRequest()
        {
            var users = await _friendService.GetReceivedReuests(_signinUserId);

            return Ok(new { receivedRequest = users });
        }

        [HttpDelete("declinedrequest")]
        public async Task<IActionResult> DeclinedRequest([FromBody] FriendAcceptDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest(new { success = false, error = "Invalid user" });

            var isDeleted = await _friendService.DeclinedRequest(_signinUserId, requestDto.SenderId);

            if (isDeleted) return Ok(new { success = isDeleted, message = "Request is deleted" });

            return NotFound(new { success = false, error="User not found"});
        }

        [HttpPost("acceptrequest")]
        public async Task<IActionResult> AcceptRequest([FromBody] FriendAcceptDto acceptRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(new { error = true, errorMessage= "Invalid Request"});

            var isAccepted = await _friendService.AcceptRequest(_signinUserId, acceptRequestDto.SenderId);
            if (isAccepted) return Ok(new { success = isAccepted, message="Friendship is created" });

            return NotFound(new { success = false, message="Already friend or something else"});

        }

        [HttpDelete("remove-friend")]
        public async Task<IActionResult> RemoveFriendship([FromBody] FriendAcceptDto userTwo)
        {
            if (!ModelState.IsValid) return BadRequest(new { success = false, error = "Invalid user" });

            var isDeleted = await _friendService.RemoveFriend(_signinUserId, userTwo.SenderId);

            if (isDeleted) return Ok(new { success = isDeleted, message = "Friend is removed" });

            return NotFound(new { success = false, error = "User not found" });
        }

        [HttpGet("getfriends")]
        public async Task<IActionResult> GetAllFriends()
        {
            try
            {
                var allfriends = await _friendService.GetAllFriends(_signinUserId);

                return Ok(new { success = true, friends = allfriends });
            }
            catch (Exception ex)
            {
                return BadRequest( new {success = false, error=true, errors = ex.Message });
            }

        }

        [HttpGet("nonfriendusers")]
        public async Task<IActionResult> GetNonFriendUsers()
        {
            var users = await _friendService.GetAllNonFriends(_signinUserId);
            if (users == null)
            {
                return NotFound(new { message = "No user is found" });
            }

            return Ok(new { nonFriendUsers = users });
        }
    }
}
