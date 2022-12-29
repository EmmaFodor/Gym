using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Gyms
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public DeleteModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Gym Gym { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Gym == null)
            {
                return NotFound();
            }

            var gym = await _context.Gym.FirstOrDefaultAsync(m => m.ID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Gym == null)
            {
                return NotFound();
            }
            var gym = await _context.Gym.FindAsync(id);

            if (gym != null)
            {
                Gym = gym;
                _context.Gym.Remove(Gym);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
