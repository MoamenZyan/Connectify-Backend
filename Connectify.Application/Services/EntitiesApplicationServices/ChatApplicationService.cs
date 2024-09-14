using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Connectify.Domain.Factories;
using Connectify.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Services.EntitiesApplicationServices
{
    public class ChatApplicationService : IChatApplicationService
    {
        private readonly IChatService _chatService;
        private readonly IChatRepository _chatRepository;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserPrivateChatRepository _userPrivateChatRepository;
        public ChatApplicationService(IChatService chatService,
                                        IUserChatRepository userChatRepository,
                                        IChatRepository chatRepository,
                                        IUnitOfWork untiOfWork,
                                        IUserPrivateChatRepository userPrivateChatRepository)
        {
            _chatService = chatService;
            _userChatRepository = userChatRepository;
            _chatRepository = chatRepository;
            _unitOfWork = untiOfWork;
            _userPrivateChatRepository = userPrivateChatRepository;
        }
        public Task<(bool, Guid)> CreateGroupChat(IFormCollection chatData)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, Guid)> CreateNormalChat(Guid currentUserId, Guid chatUserId)
        {
           try
           {
                Chat chat = new Chat()
                {
                    Id = Guid.NewGuid(),
                    Name = "",
                    Description = "",
                    CreatedAt = DateTime.UtcNow,
                };

                UserChat userChat1 = UserChatsFactory.CreateUserChat(currentUserId, chat.Id, UserRole.Member);
                UserChat userChat2 = UserChatsFactory.CreateUserChat(chatUserId, chat.Id, UserRole.Member);
                UserPrivateChat userPrivateChat = UserPrivateChatFactory.CreateUserPrivateChat(currentUserId, chatUserId, chat.Id);
                await _chatRepository.AddAsync(chat);
                await _userChatRepository.AddAsync(userChat1);
                await _userChatRepository.AddAsync(userChat2);
                await _userPrivateChatRepository.AddAsync(userPrivateChat);

                await _unitOfWork.SaveChangesAsync();
                
                return (true, chat.Id);
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex);
               return (false, default);
           }
        }


        public Task<bool> DeleteChat(int chatId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatDto?> GetPrivateChat(Guid currentUserId, Guid receiverId)
        {
            var chat = await _userPrivateChatRepository.GetPrivateChat(currentUserId, receiverId);
            if (chat == null)
                return null;

            return new ChatDto(chat, currentUserId);
        }

        public Task<bool> UserJoinGroupChat(int chatId, int userId, UserRole role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserLeaveGroupChat(int chatId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
