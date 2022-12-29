using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Data;

namespace Proiect.Models
{
    public class TrainerCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(ProiectContext context, Trainer trainer)
        {
            var allCategories = context.Category;
            var trainerCategories = new HashSet<int>(
                trainer.TrainerCategories.Select(c => c.CategoryID));
            AssignedCategoryDataList= new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = trainerCategories.Contains(cat.ID)
                });
            }

        }
        public void UpdateTrainerCategories(ProiectContext context, string[] selectedCategories, Trainer trainerToUpdate)
        {
            if(selectedCategories == null)
            {
                trainerToUpdate.TrainerCategories = new List<TrainerCategory>();
                return;
            }

            var selectedCategoriesHs = new HashSet<string>(selectedCategories);
            var trainerCategories = new HashSet<int>(trainerToUpdate.TrainerCategories.Select(c => c.CategoryID));
            foreach (var cat in context.Category)
            {
                if(selectedCategoriesHs.Contains(cat.ID.ToString()))
                {
                    if(!trainerCategories.Contains(cat.ID))
                    {
                        trainerToUpdate.TrainerCategories.Add(
                           new TrainerCategory
                           {
                               TrainerID = trainerToUpdate.ID,
                               CategoryID = cat.ID

                           });
                    }
                }
                else
                {
                    if(trainerCategories.Contains(cat.ID))
                    {
                        TrainerCategory courseToRemove = trainerToUpdate.TrainerCategories.SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
