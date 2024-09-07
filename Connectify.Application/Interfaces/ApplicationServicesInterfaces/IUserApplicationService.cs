using Connectify.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.ApplicationServicesInterfaces
{
    public interface IUserApplicationService
    {
        Task<(bool, string)> RegisterUser(IFormCollection form);
        Task<(bool, string)> UpdateUser(Guid currentUserId, IFormCollection form);
        Task<bool> DeleteUser(Guid currentUserId);
        Task<bool> SendFriendRequest(Guid currentUserId, Guid receiverId);
        Task<bool> RemoveFriendRequest(Guid currentUserId, Guid senderId);
        Task<bool> AcceptFriendRequest(Guid currentUserId, Guid senderId);
        Task<bool> DeclineFriendRequest(Guid currentUserId, Guid senderId);
        Task<bool> BlockUser(Guid currentUserId, Guid blockedId);
        Task<bool> UnblockUser(Guid currentUserId, Guid blockedId);
        Task<List<UserDto>?> GetAllUsers(Guid currentUserId);
        Task<string?> LoginUser(Guid userId);
    }
}
