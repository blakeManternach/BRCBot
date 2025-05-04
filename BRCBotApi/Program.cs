using BRCBotApi;
using BRCBotApi.Models;
using BRCBotApi.Services;
using BRCBotApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

// Only for local non-docker environment
DotNetEnv.Env.Load("test.env");
var CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_PATH");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BRCDbContext>(options =>
    options.UseSqlite(CONNECTION_STRING));
builder.Services.AddScoped<IBotService, BotService>();
builder.Services.AddScoped<IGroupMeService, GroupMeService>();
builder.Services.AddScoped<IRollService, RollService>();
builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

// Apply Migrations on startup if they exist
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BRCDbContext>();
    db.Database.Migrate();
}

// Map a POST route for receiving GroupMe callback data
app.MapPost("/GroupMePost", async (HttpContext context, IBotService botService, IGroupMeService groupMeService) =>
{
    // Read the request body and deserialize it to the GroupMeMessage object
    var groupMeMessage = await context.Request.ReadFromJsonAsync<GroupMeMessage>();

    string json = JsonSerializer.Serialize(groupMeMessage, new JsonSerializerOptions
    {
        WriteIndented = true
    });

    // TODO: may want to take this out or add a debug flag to env file
    Console.WriteLine(json);

    if (groupMeMessage != null && groupMeMessage.SenderType.ToLower().Trim() != "bot")
    {
        if (groupMeMessage.Text.ToLower().Contains("@brcbot"))
        {
            var response = await botService.ProcessGroupMeMessageAsync(groupMeMessage);
            await groupMeService.SendGroupMeMessage(response);
        }
    }
});

app.Run();
