using AngleSharp.Common;
using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.HubInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Connectify.Application.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub, IChatHub
    {
        private static ConcurrentDictionary<string, HashSet<string>> userIdToConnectionId = new ConcurrentDictionary<string, HashSet<string>>();

        private readonly IChatApplicationService _chatApplicationService;
        private readonly IUserApplicationService _userApplicationService;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserPrivateChatRepository _userPrivateChatRepository;
        private readonly IMessageApplicationService _messageApplicationService;
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChatHub(IChatApplicationService chatApplicationService,
                        IUserChatRepository userChatRepository,
                        IUserPrivateChatRepository userPrivateChatRepository,
                        IMessageApplicationService messageApplicationService,
                        IMessageRepository messageRepository,
                        IUnitOfWork unitOfWork,
                        IUserApplicationService userApplicationService,
                        IUserRepository userRepository)
        {
            _userChatRepository = userChatRepository;
            _chatApplicationService = chatApplicationService;
            _userPrivateChatRepository = userPrivateChatRepository;
            _messageApplicationService = messageApplicationService;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
            _userApplicationService = userApplicationService;
            _userRepository = userRepository;
        }

        private async Task AddUserToChatsGroups(string connectionId, List<string> chatIds)
        {
            foreach (var chatId in chatIds)
                await Groups.AddToGroupAsync(connectionId, chatId);
        }
        public async Task UserIsTyping(string chatId)
        {
            await Clients.OthersInGroup(chatId).SendAsync("ReceivingTypingStatus", Context.UserIdentifier, true);
        }
        public async Task UserStoppedTyping(string chatId)
        {
            await Clients.OthersInGroup(chatId).SendAsync("ReceivingTypingStatus", Context.UserIdentifier, false);
        }
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var currentUserId = Context.UserIdentifier;
            Console.WriteLine("-------------------> " + currentUserId);
            userIdToConnectionId.AddOrUpdate(currentUserId!, 
                new HashSet<string> { connectionId }, (key, oldValue) => 
                { 
                    oldValue.Add(connectionId);
                    return oldValue;
                });

            var user = await _userRepository.GetFullUserByIdAsync(new Guid(currentUserId!));
            var userDto = new UserDto(user!);
            var messages = userDto?.PrivateChats.SelectMany(x => x.Messages).ToDictionary(x => x.Key, x => x.Value).Values.Where(x => x.SenderId != new Guid(currentUserId!)).ToList();
            user!.IsOnline = true;
            foreach (var message in messages!)
            {
                if (message.Status == MessageStatus.Saved)
                    message.Status = MessageStatus.Sent;
            }
            var chatsIds = user?.UserJoinedChats.Select(x => Convert.ToString(x.ChatId)).ToList();
            await _userRepository.UserIsOnline(new Guid(currentUserId!));
            await _unitOfWork.SaveChangesAsync();
            if (messages != null && messages!.Count > 0)
            {
                var senderId = messages[0].SenderId;
                await Clients.User(Convert.ToString(senderId)!).SendAsync("ReceiveUpdatedMessages", messages);
            }
            var userIsOnlineStatusTask = Clients.All.SendAsync("OnUserOnline", Context.UserIdentifier!);
            var addingUserToGroupsTask = AddUserToChatsGroups(connectionId, chatsIds!);
            await Task.WhenAll(userIsOnlineStatusTask, addingUserToGroupsTask);
            return;
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            var key = Context.UserIdentifier;

            if (userIdToConnectionId.TryGetValue(key!, out var connectionsIds))
            {
                connectionsIds.Remove(connectionId);
                if (connectionsIds.Count == 0)
                {
                    userIdToConnectionId.TryRemove(key!, out _);
                }
            }
            await _userApplicationService.UserIsOffline(new Guid(key!));
            await Clients.All.SendAsync("OnUserOffline", Context.UserIdentifier!);
            return;
        }

        public async Task UserSeenMessages(string receiverGuid, string[] messagesGuid)
        {
            Console.WriteLine(receiverGuid);
            foreach (var message in messagesGuid)
                Console.WriteLine(message);
            var updatedMessages = await _messageRepository.UpdateMessagesSeenAsync(messagesGuid);

            await Clients.User(receiverGuid).SendAsync("ReceiveUpdatedMessages", updatedMessages?.Select(x => new MessageDto(x)).ToList()); ;
        }

        public async Task SendMessageToSpecificUser(string message, string receiverGuid, string messageGuid)
        {
            try
            {
                var receiverConnectionIds = userIdToConnectionId.GetOrDefault(receiverGuid, new HashSet<string>());
                var currentUserId = new Guid(Context.UserIdentifier!);
                if (currentUserId == default)
                    return;

                var chat = await _userPrivateChatRepository.GetPrivateChat(currentUserId, new Guid(receiverGuid));

                if (chat == null)
                    return;

                var messageObj = await _messageApplicationService.CreateMessage(currentUserId, chat.Id, message, "", new Guid(messageGuid), receiverConnectionIds.Count == 0 ? MessageStatus.Saved : MessageStatus.Sent);
                chat = await _userPrivateChatRepository.GetPrivateChat(currentUserId, new Guid(receiverGuid));

                await Clients.User(receiverGuid).SendAsync("ReceiveNewMessage", new
                {
                    Id = messageObj.Id,
                    Content = messageObj.Content,
                    ChatId = messageObj.ChatId,
                    CreatedAt = messageObj.CreatedAt,
                    SenderId = currentUserId,
                    Chat = new ChatDto(chat!, new Guid(Context.UserIdentifier!))
                });

                await Clients.User(Context.UserIdentifier!).SendAsync("ReceiveUpdatedMessages", new List<dynamic>
                {
                    new
                    {
                        Id = messageObj.Id,
                        Content = messageObj.Content,
                        ChatId = messageObj.ChatId,
                        CreatedAt = messageObj.CreatedAt,
                        SenderId = currentUserId,
                        Status = messageObj.Status,
                        Chat = new ChatDto(chat!, new Guid(Context.UserIdentifier!))
                    }
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

    }
}
