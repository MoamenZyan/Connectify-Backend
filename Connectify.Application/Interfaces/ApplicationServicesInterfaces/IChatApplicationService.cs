﻿using Connectify.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.ApplicationServicesInterfaces
{
    public interface IChatApplicationService
    {
        Task<bool> CreateChat(IFormCollection chatData);
        Task<bool> DeleteChat(int chatId);
        Task<bool> UserJoinChat(int chatId, int userId, UserRole role);
        Task<bool> UserLeaveChat(int chatId, int userId);
    }
}
