
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskMenager.Components.dbcontroll;


namespace TaskMenagerV2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _dbContext;


        public IndexModel(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            editTask = new Tasks();
        }

        [BindProperty]
        public Tasks editTask { get; set; }

        public async Task<IActionResult> OnPostUpdateCompletion(int taskId)
        {
            var task = await _dbContext.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.Completion = task.Completion == 0 ? 1 : 0;
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }


        public void OnGet()
        {

        }
    }
}
