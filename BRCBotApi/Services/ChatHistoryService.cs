using BRCBotApi.Models;
using BRCBotApi.Models.DBEntities;
using BRCBotApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BRCBotApi.Services
{
    public class ChatHistoryService : IChatHistoryService
    {
        private readonly BRCDbContext _dbContext;

        public ChatHistoryService(BRCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ChatMessageHistory>> GetMostRecentMessages(int numberOfMessages)
        {
            return await _dbContext.ChatMessageHistories
                .OrderByDescending(x => x.ChatMessageHistoryDateTime)
                .Take(numberOfMessages)
                .ToListAsync();
        }

        public async Task SaveBotChatMessage(string message)
        {
            await SaveChatMessage(0, "BRCBot", message);
        }

        public async Task SaveChatMessage(int userID, string userName, string message)
        {
            await _dbContext.ChatMessageHistories
                .AddAsync(new ChatMessageHistory
                {
                    ChatMessageHistoryDateTime = DateTime.UtcNow,
                    ChatMessageText = $"{userName}: {message}",
                    UserID = userID,
                    UserName = userName
                });

            await _dbContext.SaveChangesAsync();    
        }
    }
}
