using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.DTOs
{
    public class ChatDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual List<MessageDto> Messages { get; set; } = new List<MessageDto>();
        public virtual List<UserMinimalDto> Users { get; set; } = new List<UserMinimalDto>();

        public ChatDto(Chat chat)
        {
            Id = chat.Id;
            Name = chat.Name;
            Description = chat.Description;
            CreatedAt = chat.CreatedAt;

            Messages = chat.Messages.Select(x => new MessageDto(x)).ToList();
            Users = chat.Users.Select(x => new UserMinimalDto(x.User)).ToList();
        }
    }
}
