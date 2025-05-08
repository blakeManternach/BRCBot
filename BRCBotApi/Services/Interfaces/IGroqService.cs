namespace BRCBotApi.Services.Interfaces
{
    public interface IGroqService
    {
        Task<string?> SendGroqRequest(string userMessage);
    }
}
