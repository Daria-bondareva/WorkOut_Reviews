namespace ReviewsSystem.DAL.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime DatePosted { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int WorkOutId { get; set; }
        public User? User { get; set; }
        public WorkOut? WorkOut { get; set; }

    }
}
