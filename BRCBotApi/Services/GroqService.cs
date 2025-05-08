using BRCBotApi.Models;
using BRCBotApi.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BRCBotApi.Services
{
    public class GroqService : IGroqService
    {
        private string GROQ_API_KEY;
        private const string GROQ_PROMPT = "You are a chat bot in a group chat - your name is @brcbot (short for blue room court," +
            " a basketball court we all grew up playing on). Be humorous, witty, insulting, etc.  You are in a group with a bunch" +
            "of bro guys that have known each other since they were kids - lean into the fact that you are a bot and don't hold back.  " +
            "You'll be fed a list of messages for history/context and you respond with a response to the most recent message. " +
            "Jokes about sexuality, penis size, and other typical guy humor stuff is encouraged. Topics of interest are DND, NFL, NBA, memes, humor, current events. " +
            "The messages you'll see will be formatted as {userName}: {message} and you just respond with your message, just a string." +
            "DO NOT RESPOND like this: @brcbot: this is a test message.....instead, just respond like: this is a test message";
        private string GROQ_MODEL;
        private static readonly HttpClient _client = new HttpClient();
        private const string GroqApiUrl = "https://api.groq.com/openai/v1/chat/completions";


        public GroqService()
        {
            GROQ_API_KEY = Environment.GetEnvironmentVariable("GROQ_API_KEY");
            GROQ_MODEL = Environment.GetEnvironmentVariable("GROQ_MODEL");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GROQ_API_KEY);
        }

        public async Task<string?> SendGroqRequest(string text)
        {
            if (string.IsNullOrEmpty(GROQ_API_KEY))
            {
                throw new Exception("Groq API key not found or set");
            }
            if (string.IsNullOrEmpty(GROQ_PROMPT))
            {
                throw new Exception("Groq PROMPT not found or set");
            }
            if (string.IsNullOrEmpty(GROQ_MODEL))
            {
                throw new Exception("Groq MODEL key not found or set");
            }

            var chatHistory = new List<GroqMessage>
            {
                new("system", GROQ_PROMPT),
                new("user", text),
            };

            var payload = new
            {
                model = GROQ_MODEL,
                temperature = 0.7,
                messages = chatHistory
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(GroqApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Groq API call failed: {response.StatusCode} - {error}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            return doc?.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString();
        }
    }
}
