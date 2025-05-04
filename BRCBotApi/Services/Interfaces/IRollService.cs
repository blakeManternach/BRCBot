using BRCBotApi.Models.DBEntities;

namespace BRCBotApi.Services.Interfaces
{
    public interface IRollService
    {
        Task<List<Roll>> GetRollsForUser(int userID);
        Task<Roll> CreateRoll(int userID, int diceSides);
    }
}
