using Core.App.Chat.Command;
using Core.App.Chat.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chatAppBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send-request")]
        public async Task<IActionResult> SendFriendRequest([FromBody] SendFriendRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { message = result });
        }

        [HttpPost("accept-request")]
        public async Task<IActionResult> AcceptFriendRequest([FromBody] AcceptFriendRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { message = result });
        }

        [HttpGet("Friendslist/{userId}")]
        public async Task<IActionResult> GetFriendsList(Guid userId)
        {
            var result = await _mediator.Send(new GetFriendsListQuery { UserId = userId });
            return Ok(result);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { message = result });
        }

        //[HttpGet("{chatId}")]
        //public async Task<IActionResult> GetChatMessages(Guid chatId)
        //{
        //    var result = await _mediator.Send(new GetChatMessagesQuery { ChatId = chatId });
        //    return Ok(result);
        //}
        [HttpGet("{chatId}/messages")]
        public async Task<IActionResult> GetChatMessages(Guid chatId)
        {
            var result = await _mediator.Send(new GetChatMessagesQuery { ChatId = chatId });
            return Ok(result);
        }
    }
}
