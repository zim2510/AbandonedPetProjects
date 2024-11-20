namespace TalkOTC.Data.Entities
{
    public class Room
    {
        public Room()
        {
            UserRooms = new HashSet<UserRoom>();
        }

        public int Id { get; set; }
        public string RoomIdentifier { get; set; }
        public string RoomAdminId { get; set; }
        public virtual ICollection<UserRoom> UserRooms { get; set; }
    }
}
