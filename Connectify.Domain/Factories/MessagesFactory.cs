using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Ganss.Xss;

namespace Connectify.Domain.Factories
{
    public class MessagesFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Message CreateMessage(Guid senderId, Guid chatId, string content, string attachmentUrl, Guid messageGuid, MessageStatus status)
        {
            Message message = new Message()
            {
                Id = messageGuid,
                Content = sanitizer.Sanitize(content),
                SenderId = senderId,
                CreatedAt = DateTime.Now,
                Status = status,
                ChatId = chatId,
                AttachmentPath = attachmentUrl
            };
            return message;
        }
    }
}
