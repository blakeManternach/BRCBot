using BRCBotApi.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace BRCBotApi.Services
{
    public class GroupMeService : IGroupMeService
    {
        private readonly string GROUPME_BOT_TOKEN;

        public GroupMeService()
        {
            GROUPME_BOT_TOKEN = Environment.GetEnvironmentVariable("GROUPME_BOT_TOKEN");
        }

        public async Task SendGroupMeMessage(string message)
        {
            var url = "https://api.groupme.com/v3/bots/post"; // Replace with your API URL

            if (String.IsNullOrEmpty(GROUPME_BOT_TOKEN))
            {
                throw new Exception("Bot token not found or set");
            }

            var data = new { bot_id = GROUPME_BOT_TOKEN, text = message }; 
            var jsonData = JsonSerializer.Serialize(data);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Failed to send groupme message: " + responseBody);
                }
            }
        }
    }
}
