namespace H_Api.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
