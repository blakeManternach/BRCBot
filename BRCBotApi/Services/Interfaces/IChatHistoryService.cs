using BRCBotApi.Models;
using BRCBotApi.Models.DBEntities;

namespace BRCBotApi.Services.Interfaces
{
    public interface IChatHistoryService
    {
        Task<List<ChatMessageHistory>> GetMostRecentMessages(int numberOfMessages);
        Task SaveChatMessage(int userID, string userName, string message);
        Task SaveBotChatMessage(string message);
    }
}
