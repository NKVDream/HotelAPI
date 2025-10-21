namespace H_Api.Models
{
    public class Level
    {
        public int Id { get; set; }
        public string LevelName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
