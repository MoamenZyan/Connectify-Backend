using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.HubInterfaces;
using Connectify.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Connectify.Application.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationHub : Hub, INotificationHub
    {
        public async Task SendInfoNotificationToSpecificUser(IHubContext<NotificationHub> context, InfoNotification infoNotification, Guid receiverId)
        {
            await context.Clients.User(Convert.ToString(receiverId)!).SendAsync("InfoNotificationReceive", new InfoNotificationDto(infoNotification));
        }

        public async Task SendAssociatedNotificationToSpecificUser(IHubContext<NotificationHub> context, AssociatedInfoNotificationDto associatedInfoNotificationDto, Guid receiverId)
        {
            await context.Clients.User(Convert.ToString(receiverId)!).SendAsync("AssociatedNotificationReceive", associatedInfoNotificationDto);
        }


        public async Task SendNotificationToAll(InfoNotification infoNotification)
        {
            await Clients.All.SendAsync("InfoNotificationReceive", infoNotification);
        }
    }
}
