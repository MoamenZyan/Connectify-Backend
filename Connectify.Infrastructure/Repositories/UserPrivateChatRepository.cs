using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Repositories
{
    public class UserPrivateChatRepository : IUserPrivateChatRepository
    {
        private readonly AppDbContext _context;
        public UserPrivateChatRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserPrivateChat item)
        {
            await _context.AddAsync(item);
        }

        public async Task<Guid> CheckTwoUsersChat(Guid senderId, Guid receiverId)
        {
            var chat = await _context.UserPrivateChats.FirstOrDefaultAsync(x => (x.User1Id == senderId && x.User2Id == receiverId) 
                                                        || (x.User2Id == senderId && x.User1Id == receiverId));
            if (chat == null)
                return default;
            return chat.ChatId;
        }

        public async Task DeleteTwoUsersChat(Guid senderId, Guid receiverId)
        {
            await _context.UserPrivateChats.Where(x => x.User1Id == senderId && x.User2Id == receiverId)
                                    .ExecuteDeleteAsync();
        }

        public IEnumerable<UserPrivateChat> Filter(Func<UserPrivateChat, bool> predicate)
        {
            return _context.UserPrivateChats.Where(predicate);
        }

        public async Task<IEnumerable<UserPrivateChat>> GetAllAsync()
        {
            return await _context.UserPrivateChats.ToListAsync();
        }
    }
}
