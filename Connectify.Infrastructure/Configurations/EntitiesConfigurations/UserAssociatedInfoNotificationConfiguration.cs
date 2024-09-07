﻿using Connectify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Configurations.EntitiesConfigurations
{
    public class UserAssociatedInfoNotificationConfiguration : IEntityTypeConfiguration<UserAssociatedInfoNotification>
    {
        public void Configure(EntityTypeBuilder<UserAssociatedInfoNotification> builder)
        {
            builder.ToTable("UserAssociatedNotifications");
            builder.HasKey(x => new { x.UserId, x.NotificationId });

            // Relations
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserAssociatedInfoNotifications)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Notification)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.NotificationId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
