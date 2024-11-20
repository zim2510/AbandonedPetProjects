namespace TalkOTC.Data.Entities
{
    public class UserRoom
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public virtual Room Room { get; set; }
    }
}
