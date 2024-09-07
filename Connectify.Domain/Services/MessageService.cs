using Connectify.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Services
{
    public class MessageService : IMessageService
    {
        public string ValidateMessage(Dictionary<string, string> message)
        {
            string content = message["Content"];
            if (content == null || content.Length == 0)
                return "";

            return CheckForSlangInMessageContent(content);
        }


        // simple AI model ---> LMAO XD
        private string CheckForSlangInMessageContent(string content)
        {
            List<string> slangs = new List<string>()
            {
                "Screw", "Screw", "Crap", "Stupid", "Moron", "Pig"
            };

            foreach (string slang in slangs)
            {
                if (content.Contains(slang, StringComparison.OrdinalIgnoreCase))
                {
                    content.Replace(slang, new String('*', slang.Length));
                }
            }
            return content;
        }
    }
}
