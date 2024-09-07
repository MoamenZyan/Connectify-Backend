using Connectify.Domain.Entities;
using Connectify.Domain.Interfaces;
using System.Text.RegularExpressions;


namespace Connectify.Domain.Services
{
    public class ChatService : IChatService
    {
        public void ValidateChat(Dictionary<string, string> chat)
        {
            var pattern = @"^[^\d][a-zA-Z0-9_ ]*$";
            Regex chatNameRegex = new Regex(pattern);

            if (chat["Name"] != null || chat["Name"].Length == 0)
                throw new ArgumentNullException("chat name doesn't exist");

            if (!chatNameRegex.IsMatch(chat["Name"]))
                throw new Exception("chat name isn't correct");

            return;
        }
    }
}
