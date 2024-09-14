using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Connectify.API.Controllers
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/chats")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatController : ControllerBase
    {
        private readonly IChatApplicationService _chatApplicationService;
        public ChatController(IChatApplicationService chatApplicationService)
        {
            _chatApplicationService = chatApplicationService;
        }

        [HttpGet]
        [Route("{receiverId}")]
        public async Task<IActionResult> GetPrivateChat(Guid receiverId)
        {
            Guid currentUserId = new Guid(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var chat = await _chatApplicationService.GetPrivateChat(currentUserId, receiverId);
            if (chat == null)
                return NotFound(new { status = false, message = "chat not found" });
            return Ok(new {status=true, chat = chat});
        }

    }
}
