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
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;
        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Message item)
        {
            await _context.Messages.AddAsync(item);
        }

        public IEnumerable<Message> Filter(Func<Message, bool> predicate)
        {
            return _context.Messages
                            .AsNoTracking()
                            .Where(predicate);
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public IEnumerable<Message> GetAllUserMessagesInChat(Guid chatId, Guid userId)
        {
            return _context.Messages
                            .AsNoTracking()
                            .Where(x => x.ChatId == chatId && x.SenderId == userId);
        }

        public async Task<Message?> GetMessageByIdAsync(Guid messageId)
        {
            return await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
        }

        public async Task RemoveMessageAsync(Guid messageId)
        {
            await _context.Messages.Where(x => x.Id == messageId).ExecuteDeleteAsync();
        }

        public void UpdateMessageAsync(Message message)
        {
            _context.Messages.Remove(message);
        }
    }
}
