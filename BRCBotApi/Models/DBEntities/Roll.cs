namespace BRCBotApi.Models.DBEntities
{
    public class Roll
    {
        public int RollID { get; set; }
        public int DiceSides { get; set; }
        public int Result { get; set; }
        public DateTime RollDateTime { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
