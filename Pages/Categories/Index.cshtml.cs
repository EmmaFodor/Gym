using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;
using Proiect.Models.ViewModels;
using System.Data;

namespace Proiect.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        //public IList<Gym> Gym { get; set; } = default!;
        //public IList<Trainer> Trainer { get; set; } = default!;
        public IList<Category> Category { get; set; } = default!;
        public CategoriesIndexData CategoriesData { get; set; }
        public int CategoryID { get; set; }
        public int TrainerID { get; set; }
        public async Task OnGetAsync(int? id, int? trainerID)
        {
            CategoriesData = new CategoriesIndexData();
            CategoriesData.Categories = await _context.Category
                .Include(i => i.TrainerCategories)
                .ThenInclude(c => c.Trainer)
                .OrderBy(i => i.CategoryName)
                .ToListAsync();


            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoriesData.Categories
                    .Where(i => i.ID == id.Value).Single();
                ICollection<TrainerCategory>? trainerCategories = category.TrainerCategories;
            }
        }
    }
}
