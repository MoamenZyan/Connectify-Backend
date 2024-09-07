using Connectify.Domain.Entities;
using Ganss.Xss;

namespace Connectify.Domain.Factories
{
    public class MessagesFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Message CreateMessage(Dictionary<string, string> data, Guid senderId)
        {
            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                Content = sanitizer.Sanitize(data["Content"]),
                SenderId = senderId,
                CreatedAt = DateTime.Now,
                Status = Enums.MessageStatus.Sending,
                ChatId = new Guid(data["chatId"]),
                AttachmentPath = data["AttachmentPath"]
            };
            return message;
        }
    }
}
