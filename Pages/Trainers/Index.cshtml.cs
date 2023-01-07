using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Trainers
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }
        
        public IList<Trainer> Trainer { get; set; }
        public IList<TrainerCategory> TrainerCategories { get; set; }
        public TrainerData TrainerD { get; set; }
        public int TrainerID { get; set; }
        public string CategorySort { get; set; }
        public int CategoryID { get; set; }
        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            TrainerD = new TrainerData();

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CurrentFilter = searchString;


            TrainerD.Trainers = await _context.Trainer
                .Include(b => b.TrainerCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.FirstName)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                TrainerD.Trainers = TrainerD.Trainers.Where(s => s.FullName.Contains(searchString));
            }

            if(id !=null)
            {
                TrainerID = id.Value;
                Trainer trainer = TrainerD.Trainers
                    .Where(i => i.ID == id.Value).Single();
                TrainerD.Categories = trainer.TrainerCategories.Select(s => s.Category);
            }

            switch(sortOrder)
            {
                case "name_desc":
                    TrainerD.Trainers = TrainerD.Trainers.OrderByDescending(s => s.FirstName);
                    break;
            }
        }
    }
}
