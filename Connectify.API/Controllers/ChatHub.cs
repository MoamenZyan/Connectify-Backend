using AngleSharp.Common;
using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace Connectify.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, string> userIdToConnectionId = new ConcurrentDictionary<string, string>();
        private readonly IChatApplicationService _chatApplicationService;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IUserPrivateChatRepository _userPrivateChatRepository;
        private readonly IMessageApplicationService _messageApplicationService;
        public ChatHub(IChatApplicationService chatApplicationService,
                        IUserChatRepository userChatRepository,
                        IUserPrivateChatRepository userPrivateChatRepository,
                        IMessageApplicationService messageApplicationService)
        {
            _userChatRepository = userChatRepository;
            _chatApplicationService = chatApplicationService;
            _userPrivateChatRepository = userPrivateChatRepository;
            _messageApplicationService = messageApplicationService;
        }
        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var currentUserId = new Guid(Context.User!.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            userIdToConnectionId[Convert.ToString(currentUserId)!] = connectionId;

            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            var key = userIdToConnectionId.FirstOrDefault(x => x.Value == connectionId).Key;
            userIdToConnectionId.TryRemove(key, out _);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToSpecificUser(string message, string receiverGuid)
        {
            try
            {
                var receiverConnectionId = userIdToConnectionId.GetOrDefault(receiverGuid, null);
                if (receiverConnectionId == null)
                    return;

                var currentUserId = new Guid(Context.User!.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                if (currentUserId == default)
                    return;

                var checkChat = await _userPrivateChatRepository.CheckTwoUsersChat(currentUserId, new Guid(receiverGuid));
                if (checkChat == default(Guid))
                {
                    var result = await _chatApplicationService.CreateNormalChat(currentUserId, new Guid(receiverGuid));
                    checkChat = result.Item2;
                }


                var messageObj = await _messageApplicationService.CreateMessage(currentUserId, checkChat, message, "");
                await Clients.Client(receiverConnectionId).SendAsync("MessageReceive", new {Id = messageObj.Id, Content = messageObj.Content, ChatId = messageObj.ChatId, CreatedAt = messageObj.CreatedAt, SenderId = currentUserId});
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

    }
}
