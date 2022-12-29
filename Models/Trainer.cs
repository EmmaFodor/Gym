using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proiect.Models
{
    public class Trainer
    {
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Trainer")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string Phone { get; set; }
        public ICollection<TrainerCategory>? TrainerCategories { get; set; }
        public ICollection<Gym>? Gyms { get; set; } //navigation property

    }
}
