using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Interfaces
{
    public interface IChatService
    {
        void ValidateChat(Dictionary<string, string> chat);
    }
}
