using System.Text.Json.Serialization;

namespace BRCBotApi.Models
{
    [Serializable]
    public class GroupMeMessage
    {
        public List<object> Attachments { get; set; }
        [JsonPropertyName("avater_url")]
        public string AvatarUrl { get; set; }
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }
        [JsonPropertyName("group_id")]
        public string GroupId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("sender_id")]
        public string SenderId { get; set; }
        [JsonPropertyName("sender_type")]
        public string SenderType { get; set; }
        [JsonPropertyName("source_guid")]
        public string SourceGuid { get; set; }
        public bool System { get; set; }
        public string Text { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
