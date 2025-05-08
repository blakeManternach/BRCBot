using System.ComponentModel.DataAnnotations.Schema;

namespace BRCBotApi.Models.DBEntities
{
    public class ChatMessageHistory
    {
        public int ChatMessageHistoryID { get; set; }
        public DateTime ChatMessageHistoryDateTime { get; set; }
        public string ChatMessageText { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
