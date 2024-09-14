﻿// <auto-generated />
using System;
using Connectify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Connectify.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Connectify.Domain.Entities.AssociatedInfoNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("AssoicatedUserId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssoicatedUserId");

                    b.ToTable("AssociatedInfoNotifications", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Chats", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.FriendRequest", b =>
                {
                    b.Property<Guid>("SenderId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SenderId", "ReceiverId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("FriendRequests", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.InfoNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InfoNotifications", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("AttachmentPath")
                        .HasMaxLength(4000)
                        .HasColumnType("VARCHAR");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("NVARCHAR");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME2");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.OTP", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<Guid>("UserId")
                        .HasMaxLength(256)
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("OTPs", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("BIT");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("BIT");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(528)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(528)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(528)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserAssociatedInfoNotification", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("NotificationId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("UserId", "NotificationId");

                    b.HasIndex("NotificationId");

                    b.ToTable("UserAssociatedNotifications", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserBlocks", b =>
                {
                    b.Property<Guid>("BlockerId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("BlockedId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("BlockerId", "BlockedId");

                    b.HasIndex("BlockedId");

                    b.ToTable("UserBlocks", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserChat", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "ChatId");

                    b.HasIndex("ChatId");

                    b.ToTable("UserChats", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserFriend", b =>
                {
                    b.Property<Guid>("UserId1")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("UserId2")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("UserId1", "UserId2");

                    b.HasIndex("UserId2");

                    b.ToTable("UserFriends", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserInfoNotification", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("NotificationId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("UserId", "NotificationId");

                    b.HasIndex("NotificationId");

                    b.ToTable("UserInfoNotifications", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserPrivateChat", b =>
                {
                    b.Property<Guid>("User1Id")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("User2Id")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("User1Id", "User2Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("User2Id");

                    b.ToTable("UserPrivateChats", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserSeenMessage", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<DateTime>("SeenAt")
                        .HasColumnType("DATETIME2");

                    b.HasKey("UserId", "MessageId");

                    b.HasIndex("MessageId");

                    b.ToTable("UserSeenMessages", (string)null);
                });

            modelBuilder.Entity("Connectify.Domain.Entities.AssociatedInfoNotification", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.User", "AssoicatedUser")
                        .WithMany("AssociatedInfoNotifications")
                        .HasForeignKey("AssoicatedUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AssoicatedUser");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.FriendRequest", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.User", "Receiver")
                        .WithMany("ReceivedFriendRequests")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "Sender")
                        .WithMany("SentFriendRequests")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.Message", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserAssociatedInfoNotification", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.AssociatedInfoNotification", "Notification")
                        .WithMany("Users")
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "User")
                        .WithMany("UserAssociatedInfoNotifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Notification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserBlocks", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.User", "BlockedUser")
                        .WithMany("BlockedFrom")
                        .HasForeignKey("BlockedId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "BlockerUser")
                        .WithMany("BlockedUsers")
                        .HasForeignKey("BlockerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BlockedUser");

                    b.Navigation("BlockerUser");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserChat", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.Chat", "Chat")
                        .WithMany("Users")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "User")
                        .WithMany("UserJoinedChats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserFriend", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.User", "User1")
                        .WithMany("Friends")
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "User2")
                        .WithMany("FriendOf")
                        .HasForeignKey("UserId2")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserInfoNotification", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.InfoNotification", "Notification")
                        .WithMany("Users")
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "User")
                        .WithMany("UserInfoNotifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Notification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserPrivateChat", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.Chat", "Chat")
                        .WithMany("UserPrivateChats")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "User1")
                        .WithMany("AsSender")
                        .HasForeignKey("User1Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "User2")
                        .WithMany("AsReceiver")
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.UserSeenMessage", b =>
                {
                    b.HasOne("Connectify.Domain.Entities.Message", "Message")
                        .WithMany("MessageViewers")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Connectify.Domain.Entities.User", "User")
                        .WithMany("SeenMessages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.AssociatedInfoNotification", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.Chat", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserPrivateChats");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.InfoNotification", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.Message", b =>
                {
                    b.Navigation("MessageViewers");
                });

            modelBuilder.Entity("Connectify.Domain.Entities.User", b =>
                {
                    b.Navigation("AsReceiver");

                    b.Navigation("AsSender");

                    b.Navigation("AssociatedInfoNotifications");

                    b.Navigation("BlockedFrom");

                    b.Navigation("BlockedUsers");

                    b.Navigation("FriendOf");

                    b.Navigation("Friends");

                    b.Navigation("Messages");

                    b.Navigation("ReceivedFriendRequests");

                    b.Navigation("SeenMessages");

                    b.Navigation("SentFriendRequests");

                    b.Navigation("UserAssociatedInfoNotifications");

                    b.Navigation("UserInfoNotifications");

                    b.Navigation("UserJoinedChats");
                });
#pragma warning restore 612, 618
        }
    }
}
