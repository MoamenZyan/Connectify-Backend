using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Interfaces
{
    public interface IMessageService
    {
        string ValidateMessage(Dictionary<string, string> message);
    }
}
