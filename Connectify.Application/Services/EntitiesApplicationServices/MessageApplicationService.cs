using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Services.EntitiesApplicationServices
{
    public class MessageApplicationService : IMessageApplicationService
    {
        public Task<bool> CreateMessage(string content, IFormFile photo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMessage(int messageId, string content)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ViewMessage(int messageId, int viewerId)
        {
            throw new NotImplementedException();
        }
    }
}
