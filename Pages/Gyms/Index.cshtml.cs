using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Migrations;
using Proiect.Models;
using Gym = Proiect.Models.Gym;

namespace Proiect.Pages.Gyms
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }


        public IList<Gym> Gym { get; set; } = default!;
        public GymData GymD { get; set; }
        public int GymID { get; set; }
        public string CurrentFilter { get; set; }
        public string NameSort { get; set; }

        public async Task OnGetAsync(int? id,string sortOrder,string searchString)
        {
            GymD = new GymData();

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CurrentFilter = searchString;

            GymD.Gyms = await _context.Gym
                .Include(b => b.Trainer)
                .AsNoTracking()
                .OrderBy(b => b.Name)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                GymD.Gyms = GymD.Gyms.Where(s => s.Name.Contains(searchString));
            }


            if (id != null)
            {
                GymID = id.Value;
                Gym gym = GymD.Gyms
                    .Where(i => i.ID == id.Value).Single();
                
            }
            switch (sortOrder)
            {
                case "name_desc":
                    GymD.Gyms = GymD.Gyms.OrderByDescending(s => s.Name);
                    break;
            }
        }
    }
}
