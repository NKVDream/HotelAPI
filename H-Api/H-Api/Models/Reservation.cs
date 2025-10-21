namespace H_Api.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual Room Room { get; set; }
    }
}
