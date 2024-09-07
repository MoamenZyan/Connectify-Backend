using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Connectify.Domain.Enums;
using Connectify.Domain.Factories;
using Connectify.Application.DTOs;
using Connectify.Domain.Entities;
using Connectify.Application.Interfaces.UtilitesInterfaces;

namespace Connectify.Application.Services.EntitiesApplicationServices
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IUserFriendRepository _userFriendRepository;
        private readonly IUserBlocksRepository _userBlocksRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserNotificationRepository<UserInfoNotification> _userInfoNotificationRepository;
        private readonly IUserNotificationRepository<UserAssociatedInfoNotification> _userAssociatedInfoNotificationRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTService _jwtService;
        public UserApplicationService(IUserRepository userRepository,
                                    IUserService userService,
                                    IFriendRequestRepository friendRequestRepository,
                                    IUserFriendRepository userFriendRepository,
                                    IUnitOfWork unitOfWork,
                                    IUserBlocksRepository userBlocksRepository,
                                    INotificationService notificationService,
                                    IUserNotificationRepository<UserAssociatedInfoNotification> userAssociatedInfoNotificationRepository,
                                    IUserNotificationRepository<UserInfoNotification> userInfoNotificationRepository,
                                    IJWTService jwtService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _friendRequestRepository = friendRequestRepository;
            _userFriendRepository = userFriendRepository;
            _unitOfWork = unitOfWork;
            _userBlocksRepository = userBlocksRepository;
            _notificationService = notificationService;
            _userAssociatedInfoNotificationRepository = userAssociatedInfoNotificationRepository;
            _userInfoNotificationRepository = userInfoNotificationRepository;
            _jwtService = jwtService;
        }

        // Accept Friend Request
        public async Task<bool> AcceptFriendRequest(Guid currentUserId, Guid senderId)
        {
            var friendRequest = await _friendRequestRepository.GetFriendRequest(senderId, currentUserId);
            if (friendRequest == null)
                throw new Exception("friend request doesn't exists");

            friendRequest.Status = FriendRequestStatus.Accepted;
            var userFriend = UserFriendFactory.CreateFriend(senderId, currentUserId);
            try
            {
                _friendRequestRepository.UpdateFriendRequest(friendRequest);
                await _userFriendRepository.AddAsync(userFriend);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> BlockUser(Guid currentUserId, Guid blockedId)
        {
            var userBlock = UserBlocksFactory.CreateUserBlock(currentUserId, blockedId);
            try
            {
                await _userBlocksRepository.AddAsync(userBlock);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

        public async Task<bool> DeclineFriendRequest(Guid currentUserId, Guid senderId)
        {
            try
            {
                await _friendRequestRepository.DeleteFriendRequest(senderId, currentUserId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> DeleteUser(Guid currentUserId)
        {
            try
            {
                await _userRepository.DeleteUserAsync(currentUserId);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<List<UserDto>?> GetAllUsers(Guid currentUserId)
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return users.Where(x => x.Id != currentUserId).Select(x => new UserDto(x)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<string?> LoginUser(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return null;

            var token = _jwtService.GenerateToken(userId, user.Email, user.Phone);
            return token;
        }

        public async Task<(bool, string)> RegisterUser(IFormCollection form)
        {
            try
            {
                var user = await _userService.CreateUser(form, _userRepository.GetUserByEmailAsync, _userRepository.GetUserByPhoneAsync);
                await _userRepository.AddAsync(user);
                var notification = _notificationService.CreateInfoNotification($"{user.Fname}, Welcome on board!");
                var userNotification = UserNotificationFactory.CreateUserInfoNotification(notification.Id, user.Id);
                await _unitOfWork.SaveChangesAsync();
                return (true, "user created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (false, ex.Message);
            }
        }

        public async Task<bool> RemoveFriendRequest(Guid currentUserId, Guid senderId)
        {
            try
            {
                await _friendRequestRepository.DeleteFriendRequest(senderId, currentUserId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> SendFriendRequest(Guid currentUserId, Guid receiverId)
        {
            var friendRequest = FriendRequestFactory.CreateFriendRequest(currentUserId, receiverId);
            try
            {
                await _friendRequestRepository.AddAsync(friendRequest);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> UnblockUser(Guid currentUserId, Guid blockedId)
        {
            try
            {
                await _userBlocksRepository.RemoveBlockFromSpecificUserAsync(currentUserId, blockedId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<(bool, string)> UpdateUser(Guid currentUserId, IFormCollection form)
        {
            var originalUser = await _userRepository.GetUserByIdAsync(currentUserId);
            if (originalUser == null)
                return (false, "user isn't exists to update");
            
            try
            {
                var updatedUser = await _userService.CreateUser(form, _userRepository.GetUserByEmailAsync, _userRepository.GetUserByPhoneAsync);
                _userService.UpdateUser(originalUser, updatedUser);

                _userRepository.UpdateUser(originalUser);
                await _unitOfWork.SaveChangesAsync();
                return (true, "user updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (false, ex.Message);
            }
        }
    }
}
