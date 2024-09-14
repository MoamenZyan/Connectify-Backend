﻿using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connectify.Domain.Enums;

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
        public bool IsOnline { get; set; } = false;


        public List<ChatDto> PrivateChats { get; set; } = new List<ChatDto>();
        public List<ChatDto> GroupChats { get; set; } = new List<ChatDto>();
        public List<UserMinimalDto> BlockedUsers { get; set; } = new List<UserMinimalDto>();
        public List<UserMinimalDto> Friends { get; set; } = new List<UserMinimalDto>();
        public List<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public List<FriendRequest> ReceivedFriendRequests { get; set; } = new List<FriendRequest>();
        public List<AssociatedInfoNotificationDto> AssociatedInfoNotifications { get; set; } = new List<AssociatedInfoNotificationDto>();
        public List<InfoNotificationDto> InfoNotifications { get; set; } = new List<InfoNotificationDto>();
        public UserDto(User user)
        {
            Id = user.Id;
            FullName = $"{user.Fname} {user.Lname}";
            Email = user.Email;
            Phone = user.Phone;
            Photo = user.Photo;
            IsVerified = user.IsVerified;
            IsOnline = user.IsOnline;

            PrivateChats = user.UserJoinedChats.Where(x => x.Chat.Type == ChatType.Normal)
                                                .Select(x => new ChatDto(x.Chat, user.Id)).ToList();

            GroupChats = user.UserJoinedChats.Where(x => x.Chat.Type == ChatType.Group)
                                                .Select(x => new ChatDto(x.Chat, user.Id)).ToList();

            BlockedUsers = user.BlockedUsers.Select(x => new UserMinimalDto(x.BlockerUser)).ToList();

            Friends = user.Friends.Select(x => new UserMinimalDto(x.User2))
                    .Concat(user.FriendOf.Select(x => new UserMinimalDto(x.User1))).ToList();

            SentFriendRequests = user.SentFriendRequests;
            ReceivedFriendRequests = user.ReceivedFriendRequests;
            AssociatedInfoNotifications = user.AssociatedInfoNotifications.Select(x => new AssociatedInfoNotificationDto(x.AssoicatedUser, x)).ToList();
            InfoNotifications = user.UserInfoNotifications.Select(x => new InfoNotificationDto(x.Notification)).ToList();
        }
    }
    public class UserMinimalDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public bool IsOnline { get; set; }
        public UserMinimalDto(User user)
        {
            Id = user.Id;
            FullName = $"{user.Fname} {user.Lname}";
            Email = user.Email;
            Phone = user.Phone;
            Photo = user.Photo;
            IsOnline = user.IsOnline;
        }
    }
}
