
using System.Text.RegularExpressions;

namespace BRCBotApi
{
    public static class BRCBot
    {
        public static string InteractWithMessage(string command, string text)
        {
            switch (command?.ToLower())
            {
                case "roll":
                    return Roll(text);
                case "info":
                    return Info();
                default:
                    return "Unce! I don't recognize that command";
            };
        }

        private static string Info()
        {
            return "I support the following commands: \n" +
                "roll (e.g. '@brcbot roll d20' or '@brcbot roll d6' \n" +
                "info (return all possible commands, @brcbot info)";
        }

        private static string Roll(string text)
        {
            var match = Regex.Match(text, @"^d(\d+)$", RegexOptions.IgnoreCase);
            string defaultText = "roll command must be in this format: @brcbot roll d{positiveNumber}";
            if (match.Success)
            {
                var range = int.Parse(match.Groups[1].Value);
                if (range > 0)
                {
                    var rand = new Random();
                    return rand.Next(range + 1).ToString();
                }
            }
            return defaultText;
        }
    }
}
