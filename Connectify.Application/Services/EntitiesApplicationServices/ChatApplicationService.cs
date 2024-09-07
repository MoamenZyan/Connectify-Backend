using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Domain.Enums;
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
        public Task<bool> CreateChat(IFormCollection chatData)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteChat(int chatId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserJoinChat(int chatId, int userId, UserRole role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserLeaveChat(int chatId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
