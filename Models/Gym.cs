using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using NuGet.DependencyResolver;

namespace Proiect.Models
{
    public class Gym
    {
        public int ID { get; set; }

        [Display(Name = "Gym's Name")]
        public string Name { get; set; }

        public string Adress { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 500)]
        public decimal Price { get; set; }

        public int? TrainerID { get; set; }
        public Trainer? Trainer { get; set; } //navigation property
        public Borrowing? Borrowing { get; set; } //navigation property
    }
}
