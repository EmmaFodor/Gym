namespace Proiect.Models
{
    public class TrainerData
    {
        public IEnumerable<Trainer> Trainers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<TrainerCategory> TrainerCategories { get; set; }
    }
}
