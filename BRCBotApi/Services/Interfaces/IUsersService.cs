using BRCBotApi.Models.DBEntities;

namespace BRCBotApi.Services.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetOrCreateGroupMeUser(string groupMeUserName, string groupMeUserID);
    }
}
