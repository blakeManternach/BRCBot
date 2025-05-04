using BRCBotApi.Models;

namespace BRCBotApi.Services.Interfaces
{
    public interface IBotService
    {
        Task<string> ProcessGroupMeMessageAsync(GroupMeMessage message);
    }
}
