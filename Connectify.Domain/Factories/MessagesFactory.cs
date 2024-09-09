using Connectify.Domain.Entities;
using Ganss.Xss;

namespace Connectify.Domain.Factories
{
    public class MessagesFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Message CreateMessage(Guid senderId, Guid chatId, string content, string attachmentUrl)
        {
            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                Content = sanitizer.Sanitize(content),
                SenderId = senderId,
                CreatedAt = DateTime.Now,
                Status = Enums.MessageStatus.Sending,
                ChatId = chatId,
                AttachmentPath = attachmentUrl
            };
            return message;
        }
    }
}
