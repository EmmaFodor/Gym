using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Proiect.Models
{
    public class Borrowing
    {
        public int ID { get; set; }
        public int? ClientID { get; set; }
        public Client? Client { get; set; } //navigation property
        public int? GymID { get; set; }
        public Gym? Gym { get; set; }  //navigation property
        [DataType(DataType.Date)]

        [Display(Name= "Subscription expiration date")]
        public DateTime ReturnDate { get; set; }

        //public int? TrainerID { get; set; }
        //public Trainer? Trainer { get; set; }  //navigation property
    }
}
