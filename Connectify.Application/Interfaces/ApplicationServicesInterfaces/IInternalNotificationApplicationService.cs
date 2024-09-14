using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.ApplicationServicesInterfaces
{
    public interface IInternalNotificationApplicationService
    {
        Task WelcomeNotification(User receiver);
        Task ReceivedFriendRequestNotification(User sender, User receiver);
    }
}
