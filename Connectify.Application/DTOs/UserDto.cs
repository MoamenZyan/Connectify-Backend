using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public bool IsVerified { get; set; } = false;


        public List<ChatDto> Chats { get; set; } = new List<ChatDto>();
        public List<UserMinimalDto> BlockedUsers { get; set; } = new List<UserMinimalDto>();
        public List<UserMinimalDto> Friends { get; set; } = new List<UserMinimalDto>();
        public List<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public List<FriendRequest> ReceivedFriendRequests { get; set; } = new List<FriendRequest>();
        public List<AssociatedInfoNotificationDto> AssociatedInfoNotifications { get; set; } = new List<AssociatedInfoNotificationDto>();
        public List<InfoNotification> InfoNotifications { get; set; } = new List<InfoNotification>();
        public UserDto(User user)
        {
            Id = user.Id;
            FullName = $"{user.Fname} {user.Lname}";
            Email = user.Email;
            Phone = user.Phone;
            Photo = user.Photo;
            IsVerified = user.IsVerified;

            Chats = user.UserJoinedChats.Select(x => new ChatDto(x.Chat)).ToList();
            BlockedUsers = user.BlockedUsers.Select(x => new UserMinimalDto(x.BlockerUser)).ToList();
            Friends = user.Friends.Select(x => new UserMinimalDto(x.User2))
                    .Concat(user.FriendOf.Select(x => new UserMinimalDto(x.User1))).ToList();
            SentFriendRequests = user.SentFriendRequests;
            ReceivedFriendRequests = user.ReceivedFriendRequests;
            AssociatedInfoNotifications = user.AssociatedInfoNotifications.Select(x => new AssociatedInfoNotificationDto(x.AssoicatedUser, x)).ToList();
            InfoNotifications = user.UserInfoNotifications.Select(x => x.Notification).ToList();
        }
    }
    public class UserMinimalDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public UserMinimalDto(User user)
        {
            Id = user.Id;
            FullName = $"{user.Fname} {user.Lname}";
            Email = user.Email;
            Phone = user.Phone;
            Photo = user.Photo;
        }
    }
}
