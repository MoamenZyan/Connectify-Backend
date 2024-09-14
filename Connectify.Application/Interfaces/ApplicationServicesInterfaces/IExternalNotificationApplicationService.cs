using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.ApplicationServicesInterfaces
{
    public interface IExternalNotificationApplicationService
    {
        Task WelcomeEmailNotification(User receiver);
        Task ReceivedFriendRequestEmailNotification(User sender, User receiver);
    }
}
