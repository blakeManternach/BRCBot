namespace BRCBotApi.Models
{
    public class GroqMessage
    {
        public string role { get; set; }
        public string content { get; set; }
        public GroqMessage(string role, string content)
        {
            this.role = role;
            this.content = content;
        }
    }
}
