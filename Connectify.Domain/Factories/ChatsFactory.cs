using Connectify.Domain.Entities;
using Ganss.Xss;

namespace Connectify.Domain.Factories
{
    public class ChatsFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Chat CreateChat(Dictionary<string, string> data)
        {
            Chat chat = new Chat()
            {
                Id = Guid.NewGuid(),
                Name = sanitizer.Sanitize(data["name"]),
                Description = sanitizer.Sanitize(data["description"]),
                CreatedAt = DateTime.Now,
            };
            return chat;
        }
    }
}
