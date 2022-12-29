namespace Proiect.Models
{
    public class Category
    {
        public int ID { get; set; }

        public string CategoryName { get; set; }

        //public ICollection<TrainerCategory>? TrainerCategories { get; set; }
        public List<TrainerCategory>? TrainerCategories { get; set; }
    }

}
