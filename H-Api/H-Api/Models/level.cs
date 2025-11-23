using System.ComponentModel.DataAnnotations.Schema;

namespace H_Api.Models
{
    public class Level
    {
        public Level()
        {
            Rooms = new HashSet<Room>();
        }
        [Column("id")]
        public int Id { get; set; }

        [Column("level_name")]
        public string LevelName { get; set; }

        [Column("description")]
        public string Description { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
