namespace Proiect.Models
{
    public class GymData
    {
        public IEnumerable<Gym> Gyms { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<GymCategory> GymCategories { get; set; }
    }
}
