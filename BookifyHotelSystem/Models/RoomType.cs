namespace BookifyHotelSystem.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();


    }
}
