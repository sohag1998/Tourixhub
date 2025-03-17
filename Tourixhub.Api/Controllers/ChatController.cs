using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        private readonly Guid _currentUserId;

        public ChatController(IChatService chatService, IUserContextService userContextService)
        {
            _chatService = chatService;
            _currentUserId = userContextService.AppUserId;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] AddMessageDto messageDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var isSend = await _chatService.AddMessage(_currentUserId, messageDto);

            if (!isSend) return BadRequest(new { error = isSend, message = "Message is not send"});

            return Ok(new {success = isSend, message = "Message is send"});
        }

        [HttpGet("allreceived-messages")]
        public async Task<IActionResult> GetAllReceivedMessages(string senderId)
        {
            if(string.IsNullOrWhiteSpace(senderId)) return BadRequest(ModelState);

            var messages = await _chatService.GetAllReceivedMessageByAppUserId(_currentUserId, Guid.Parse(senderId));
            
            return Ok(new {success = true, messages = messages});
        }

        [HttpGet("allsend-messages")]
        public async Task<IActionResult> GetAllSendMessages(string receiverId)
        {
            if(string.IsNullOrWhiteSpace(receiverId)) return BadRequest(ModelState);

            var messages = await _chatService.GetAllSendMessageByAppUserId(_currentUserId, Guid.Parse(receiverId));
            return Ok(new { success = true, messages = messages });
        }
    }
}
