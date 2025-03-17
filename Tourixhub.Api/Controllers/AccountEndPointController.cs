using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourixhub.Application.Interfaces;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountEndPointController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserContextService _userContextService;
        private readonly Guid _currentUserId;
        public AccountEndPointController(IUserService userService, IUserContextService userContextService)
        {
            _userService = userService;
            _userContextService = userContextService;
            _currentUserId = _userContextService.AppUserId;
        }

    

        [HttpGet("receivedrequest")]
        public async Task<IActionResult> GetReceivedRequest()
        {
            var users = await _userService.GetWhoSentRequest(_currentUserId);

            return Ok( new { receivedRequest = users });
        }
    }
}
