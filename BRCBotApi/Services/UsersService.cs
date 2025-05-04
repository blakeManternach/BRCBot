using BRCBotApi.Models.DBEntities;
using BRCBotApi.Services.Interfaces;

namespace BRCBotApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly BRCDbContext _dbContext;
        public UsersService(BRCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetOrCreateGroupMeUser(string groupMeUserName, string groupMeUserID)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.GroupMeUserID == groupMeUserID); 
            if (user is null)
            {
                // If null, create the user then return it
                user = new User
                {
                    GroupMeUserID = groupMeUserID,
                    Name = groupMeUserName
                };
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                // Else see if their username has changed and if so update it
                if (user.Name != groupMeUserName)
                {
                    user.Name = groupMeUserName;
                    _dbContext.Users.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return user;
        }
    }
}
