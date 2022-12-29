namespace Proiect.Models
{
    public class TrainerCategory
    {
        public int ID { get; set; }
        public int TrainerID { get; set; }
        public Trainer Trainer { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
