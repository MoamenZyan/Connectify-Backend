using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Application.Interfaces.UtilitesInterfaces;
using Connectify.Application.Services.EntitiesApplicationServices;
using Connectify.Domain.Entities;
using Connectify.Domain.Interfaces;
using Connectify.Domain.Services;
using Connectify.Infrastructure.Authentication;
using Connectify.Infrastructure.Configurations.JWTConfigurations;
using Connectify.Infrastructure.Data;
using Connectify.Infrastructure.Repositories;
using Connectify.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database registeration
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Entities Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IOTPRepository, OTPRepository>();

            // Notification Repositories
            services.AddScoped<INotificationRepository<AssociatedInfoNotification>, AssociatedInfoNotificationRepository>();
            services.AddScoped<INotificationRepository<InfoNotification>, InfoNotificationRepository>();
            services.AddScoped<IUserNotificationRepository<UserAssociatedInfoNotification>, UserAssosicatedInfoNotificationRepository>();
            services.AddScoped<IUserNotificationRepository<UserInfoNotification>, UserInfoNotificationRepository>();


            // Junction Entities Repositories
            services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
            services.AddScoped<IUserBlocksRepository, UserBlocksRepository>();
            services.AddScoped<IUserChatRepository, UserChatRepository>();
            services.AddScoped<IUserSeenMessageRepository, UserSeenMessageRepository>();
            services.AddScoped<IUserFriendRepository, UserFriendRepository>();

            // Domain Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotificationService, NotificationService>();

            // Application Services
            services.AddScoped<IChatApplicationService, ChatApplicationService>();
            services.AddScoped<IMessageApplicationService, MessageApplicationService>();
            services.AddScoped<IUserApplicationService, UserApplicationService>();


            // Authentication
            services.Configure<JWTConfigurations>(configuration.GetSection("JWTConfigurations"));
            services.AddSingleton<JWTConfigurations>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    JWTConfigurations jwtConfigs = services.BuildServiceProvider().GetRequiredService<IOptions<JWTConfigurations>>().Value;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfigs.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtConfigs.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.SigningKey)),

                        ValidateLifetime = true,
                    };
                });

            return services;
        }
    }
}
