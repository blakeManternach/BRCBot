namespace BRCBotApi.Models.DBEntities
{
    public class User
    {
        public int UserID { get; set; }
        public string GroupMeUserID { get; set; }
        public string Name { get; set; }
        public List<Roll> Rolls { get; set; } = new();
    }
}
