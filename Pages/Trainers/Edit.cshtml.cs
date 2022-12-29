using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Trainers
{
    [Authorize(Roles = "Admin")]
    public class EditModel :TrainerCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public EditModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Trainer == null)
            {
                return NotFound();
            }

            Trainer = await _context.Trainer
                .Include(b => b.TrainerCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            var trainer =  await _context.Trainer.FirstOrDefaultAsync(m => m.ID == id);
            if (trainer == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Trainer);

            var trainerList = _context.Trainer.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });

            Trainer = trainer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerToUpdate = await _context.Trainer
                .Include(i => i.TrainerCategories).ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);
            if(trainerToUpdate == null)
            {
                return NotFound();
            }

            if(await TryUpdateModelAsync<Trainer>(
                trainerToUpdate, "Trainer", i=>i.FullName, i=>i.Phone))
            {
                UpdateTrainerCategories(_context, selectedCategories, trainerToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateTrainerCategories(_context, selectedCategories, trainerToUpdate);
            PopulateAssignedCategoryData(_context, trainerToUpdate);
            return RedirectToPage("./Index");
        }

        private bool TrainerExists(int id)
        {
          return _context.Trainer.Any(e => e.ID == id);
        }
    }
}
