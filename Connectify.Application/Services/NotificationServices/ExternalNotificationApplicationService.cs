using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.ExternalNotificationsInterfaces.EmailStrategies;
using Connectify.Application.Interfaces.ExternalNotificationsInterfaces;
using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Services.NotificationServices
{
    public class ExternalNotificationApplicationService : IExternalNotificationApplicationService
    {
        private readonly IWelcomeEmailStrategy _welcomeEmailStrategy;
        private readonly IReceivedFriendRequestEmailStrategy _receivedFriendRequestEmailStrategy;
        private readonly IExternalNotificationContext _notificationContext;
        public ExternalNotificationApplicationService(IWelcomeEmailStrategy welcomeEmailStrategy,
                                                IReceivedFriendRequestEmailStrategy receivedFriendRequestEmailStrategy,
                                                IExternalNotificationContext notificationContext)
        {
            _welcomeEmailStrategy = welcomeEmailStrategy;
            _receivedFriendRequestEmailStrategy = receivedFriendRequestEmailStrategy;
            _notificationContext = notificationContext;
        }
        public async Task ReceivedFriendRequestEmailNotification(User sender, User receiver)
        {
            _notificationContext.SetStrategy(_receivedFriendRequestEmailStrategy);
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                {"SenderName", sender.Fname }
            };
            await _notificationContext.Send(receiver.Fname, receiver.Email, data);
        }

        public async Task WelcomeEmailNotification(User receiver)
        {
            _notificationContext.SetStrategy(_welcomeEmailStrategy);
            await _notificationContext.Send(receiver.Fname, receiver.Email, null!);
        }
    }
}
