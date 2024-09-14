using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Domain.Factories;
using Connectify.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Services.NotificationServices
{
    public class InternalNotificationApplicationService : IInternalNotificationApplicationService
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationRepository<InfoNotification> _infoNotificationRepository;
        private readonly INotificationRepository<AssociatedInfoNotification> _associateInfoNotificationRepository;
        private readonly IUserNotificationRepository<UserInfoNotification> _userInfoNotificationRepository;
        private readonly IUserNotificationRepository<UserAssociatedInfoNotification> _userAssociatedInfoNotificationRepository;
        public InternalNotificationApplicationService(INotificationService notificationService,
                                                INotificationRepository<InfoNotification> infoNotificationRepository,
                                                INotificationRepository<AssociatedInfoNotification> associateInfoNotificationRepository,
                                                IUserNotificationRepository<UserInfoNotification> userInfoNotificationRepository,
                                                IUserNotificationRepository<UserAssociatedInfoNotification> userAssociatedInfoNotificationRepository)
        {
            _notificationService = notificationService;
            _infoNotificationRepository = infoNotificationRepository;
            _associateInfoNotificationRepository = associateInfoNotificationRepository;
            _userAssociatedInfoNotificationRepository = userAssociatedInfoNotificationRepository;
            _userInfoNotificationRepository = userInfoNotificationRepository;
        }
        public async Task ReceivedFriendRequestNotification(User sender, User receiver)
        {
            var notification = _notificationService.CreateAssociatedUserNotification(sender, $"{receiver.Fname}, you got friend request from {sender.Fname}");
            var userNotification = new UserAssociatedInfoNotification()
            {
                UserId = receiver.Id,
                NotificationId = notification.Id,
            };
            
            await _associateInfoNotificationRepository.AddAsync(notification);
            await _userAssociatedInfoNotificationRepository.AddAsync(userNotification);
        }

        public async Task WelcomeNotification(User receiver)
        {
            var notification = _notificationService.CreateInfoNotification($"{receiver.Fname}, Welcome on board!");
            var userNotification = UserNotificationFactory.CreateUserInfoNotification(notification.Id, receiver.Id);

            await _infoNotificationRepository.AddAsync(notification);
            await _userInfoNotificationRepository.AddAsync(userNotification);
        }
    }
}
