namespace TalkOTC.Utilities
{
    public static class Utility
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GenerateRoomId()
        {
            var roomId = GenerateGuid().Substring(0, 6);
            return roomId;
        }
    }
}
