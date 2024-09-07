using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public bool IsVerified { get; set; } = false;

        public virtual List<UserChat> UserJoinedChats { get; set; } = new List<UserChat>();
        public virtual List<Message> Messages { get; set; } = new List<Message>();
        public virtual List<UserBlocks> BlockedUsers { get; set; } = new List<UserBlocks>();
        public virtual List<UserBlocks> BlockedFrom { get; set; } = new List<UserBlocks>();
        public virtual List<UserSeenMessage> SeenMessages { get; set; } = new List<UserSeenMessage>();
        public virtual List<UserFriend> Friends { get; set; } = new List<UserFriend>();
        public virtual List<UserFriend> FriendOf { get; set; } = new List<UserFriend>();
        public virtual List<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual List<FriendRequest> ReceivedFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual List<AssociatedInfoNotification> AssociatedInfoNotifications { get; set; } = new List<AssociatedInfoNotification>();
        public virtual List<UserInfoNotification> UserInfoNotifications { get; set; } = new List<UserInfoNotification>();
        public virtual List<UserAssociatedInfoNotification> UserAssociatedInfoNotifications { get; set; } = new List<UserAssociatedInfoNotification>();
    }
}
