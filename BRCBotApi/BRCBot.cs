
using System.Text.RegularExpressions;

namespace BRCBotApi
{
    public static class BRCBot
    {
        public static string InteractWithMessage(string command, string text, string userName)
        {
            switch (command?.ToLower())
            {
                case "roll":
                    return Roll(text, userName);
                case "help":
                    return Help();
                default:
                    return "Unce! Unknown command.\n" +
                           "Type '@brcbot help' for a list of commands.";
            };
        }

        private static string Help()
        {
            return "Available commands:\n" +
                   "🎲 roll — Roll a die (e.g. @brcbot roll d20)\n" +
                   "ℹ️ help — Show this help message (@brcbot help)\n";
        }

        private static string Roll(string text, string userName)
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
                    var rand = new Random();
                    var result = rand.Next(range + 1).ToString();
                    return $"🎲 {userName} rolls a d{range}...\n" +
                           $"🎯 Result: {result}";
                }
            }
            return defaultText;
        }
    }
}
