static public class RoomStatus
{
    public const string JOINED_ROOM = "JOINED_ROOM";
    public const string HOSTED_ROOM = "JOINED_ROOM";
    public const string FREE = "FREE";

    static public string Status = RoomStatus.FREE;


    static public void SetStatus(string Status) {
        RoomStatus.Status = Status;
    }
}
