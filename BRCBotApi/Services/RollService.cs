using BRCBotApi.Models.DBEntities;
using BRCBotApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BRCBotApi.Services
{
    public class RollService : IRollService
    {
        private readonly BRCDbContext _dbContext;
        public RollService(BRCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Roll> CreateRoll(int userID, int diceSides)
        {
            var rand = new Random();
            var result = rand.Next(1, diceSides + 1);
            var roll = new Roll()
            {
                DiceSides = diceSides,
                Result = result,
                RollDateTime = DateTime.UtcNow,
                UserID = userID
            };

            await _dbContext.Rolls.AddAsync(roll);
            await _dbContext.SaveChangesAsync();

            return roll;
        }

        public async Task<List<Roll>> GetRollsForUser(int userID)
        {
            return await _dbContext.Rolls
                .Where(x => x.UserID == userID)
                .OrderByDescending(x => x.RollDateTime)
                .ToListAsync(); 
        }
    }
}
