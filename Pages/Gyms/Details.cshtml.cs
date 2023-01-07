using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Gyms
{
    public class DetailsModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public DetailsModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

      public Gym Gym { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Gym == null)
            {
                return NotFound();
            }

            var gym = await _context.Gym.Include("Trainer").FirstOrDefaultAsync(m => m.ID == id);
            if (gym == null)
            {
                return NotFound();
            }
            else 
            {
                Gym = gym;
            }
            return Page();
        }
    }
}
