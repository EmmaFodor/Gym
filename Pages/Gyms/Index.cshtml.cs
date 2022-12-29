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
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IList<Gym> Gym { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Gym != null)
            {
                Gym = await _context.Gym
                    .Include(b => b.Trainer)
                    .ToListAsync();
            }
        }
    }
}
