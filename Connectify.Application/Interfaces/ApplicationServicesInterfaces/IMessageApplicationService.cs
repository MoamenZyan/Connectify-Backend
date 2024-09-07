using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.ApplicationServicesInterfaces
{
    public interface IMessageApplicationService
    {
        Task<bool> CreateMessage(string content, IFormFile photo);
        Task<bool> DeleteMessage(int messageId);
        Task<bool> UpdateMessage(int messageId, string content);
        Task<bool> ViewMessage(int messageId, int viewerId);
    }
}
