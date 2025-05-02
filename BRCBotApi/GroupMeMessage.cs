namespace BRCBotApi
{
    [Serializable]
    public class GroupMeMessage
    {
        public List<object> Attachments { get; set; }
        public string AvatarUrl { get; set; }
        public long CreatedAt { get; set; }
        public string GroupId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string SenderId { get; set; }
        public string SenderType { get; set; }
        public string SourceGuid { get; set; }
        public bool System { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
    }
}
