namespace ReviewsSystem.DAL.Entities
{
    public class WorkOut
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }
        public string TrainingDuration { get; set; }
        public decimal Price { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
