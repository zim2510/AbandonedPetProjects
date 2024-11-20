namespace TalkOTC.Models.DTOs
{
    public class MessageDto
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
        public string RoomIdentifier { get; set; }
        public string MessageBody { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
