using BRCBotApi;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

DotNetEnv.Env.Load();
var GROUPME_BOT_TOKEN = Environment.GetEnvironmentVariable("GROUPME_BOT_TOKEN");

// Map a POST route for receiving GroupMe callback data
app.MapPost("/GroupMePost", async (HttpContext context) =>
{
    // Read the request body and deserialize it to the GroupMeMessage object
    var groupMeMessage = await context.Request.ReadFromJsonAsync<GroupMeMessage>();

    string json = JsonSerializer.Serialize(groupMeMessage, new JsonSerializerOptions
    {
        WriteIndented = true
    });

    Console.WriteLine(json);

    // Here you can work with the deserialized groupMeMessage object
    if (groupMeMessage != null && groupMeMessage.SenderType.ToLower().Trim() != "bot")
    {
        var message = groupMeMessage.Text.ToLower();
        if (message.Contains("@brcbot"))
        {
            var messageParts = message.Split(" ", 3);
            var command = messageParts.Length > 1 ? messageParts[1] : null;
            var text = messageParts.Length > 2 ? messageParts[2] : null;

            string response = messageParts[0] == "@brcbot" ? 
                BRCBot.InteractWithMessage(command, text) :
                "Message must be in format: @brcbot {command} {text}.  (e.g. @brcbot roll d20)";

            await SendGroupMeMessage(response);
        }
    }
});

app.Run();

async Task SendGroupMeMessage(string message)
{
    var url = "https://api.groupme.com/v3/bots/post"; // Replace with your API URL

    if (String.IsNullOrEmpty(GROUPME_BOT_TOKEN))
    {
        throw new Exception("Bot token not found or set");
    }

    var data = new { bot_id = "4c91f451b08fcce91bbef2b40e", text = message }; 
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
