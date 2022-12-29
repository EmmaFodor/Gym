using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Trainers
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : TrainerCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            /*var trainerList = _context.Trainer.Select(x >= new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });*/

            ViewData["TrainerID"] = new SelectList(_context.Set<Trainer>(), "ID", "FullName");

            var trainer = new Trainer();
            trainer.TrainerCategories = new List<TrainerCategory>();
            PopulateAssignedCategoryData(_context, trainer);
            return Page();
        }

        [BindProperty]
        public Trainer Trainer { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newTrainer = new Trainer();
          if (selectedCategories != null)
            {
                newTrainer.TrainerCategories = new List<TrainerCategory>();
                foreach(var cat in selectedCategories)
                {
                    var catToAdd = new TrainerCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newTrainer.TrainerCategories.Add(catToAdd);
                }
                Trainer.TrainerCategories = newTrainer.TrainerCategories;
            _context.Trainer.Add(Trainer);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            }

             PopulateAssignedCategoryData(_context, newTrainer);
        return Page();
        }
    }
}
