using BRCBotApi.Models;
using BRCBotApi.Models.DBEntities;
using BRCBotApi.Services.Interfaces;
using System.Text.RegularExpressions;

namespace BRCBotApi.Services
{
    public class BotService : IBotService
    {
        private readonly IUsersService _usersService;
        private readonly IRollService _rollService;

        public BotService(IUsersService usersService, IRollService rollService, BRCDbContext dbContext)
        {
            _usersService = usersService;
            _rollService = rollService;
        }

        public async Task<string> ProcessGroupMeMessageAsync(GroupMeMessage message)
        {
            var messageParts = message.Text.Split(" ", 3);
            var command = messageParts.Length > 1 ? messageParts[1] : null;
            var text = messageParts.Length > 2 ? messageParts[2] : null;

            var user = await _usersService.GetOrCreateGroupMeUser(message.Name, message.SenderId);

            if (messageParts[0] != "@brcbot")
            {
                return "Usage: @brcbot {command} {text}\n" + 
                       "Example: @brcbot roll d20";
            }

            switch (command?.ToLower())
            {
                case "roll":
                    return await RollAsync(text, user);
                case "rollstats":
                    return await RollStats(text, user);
                case "help":
                    return Help();
                default:
                    return "Unce! Unknown command.\n" +
                           "Type '@brcbot help' for a list of commands.";
            };
        }


        private string Help()
        {
            return "Available commands:\n" +
                   "🎲 roll — Roll a die (e.g. @brcbot roll d20)\n" +
                   "ℹ️ help — Show this help message (@brcbot help)\n";
        }
        private async Task<string> RollStats(string? text, User user)
        {
            var rollsForUser = await _rollService.GetRollsForUser(user.UserID);
            var average = rollsForUser.Select(x => x.Result).Average();
            return     "📊 Stats:\n" +
                      $"    - Rolls: {rollsForUser.Count}\n" +
                      $"    - Average: {average}";
        }
        private async Task<string> RollAsync(string? text, User user)
        {
            string defaultText = "Usage: @brcbot roll d{number}\n" + 
                                 "Example: @brcbot roll d20";
            if (text is null) return defaultText;
            var match = Regex.Match(text, @"^d(\d+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var range = int.Parse(match.Groups[1].Value);
                if (range > 0)
                {
                    var roll = await _rollService.CreateRoll(user.UserID, range);
                    return $"🎲 {user.Name} rolls a d{roll.DiceSides}...\n" +
                           $"🎯 Result: {roll.Result}";
                }
            }
            return defaultText;
        }
    }
}
