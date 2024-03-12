using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMenager.Components.dbcontroll;

namespace TaskMenagerV2.Pages
{
    public class AddNewModel : PageModel
    {
        private readonly MyDbContext _dbContext;

        public AddNewModel(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            newProject = new Project();
            newTask = new Tasks();
        }

        [BindProperty]
        public Project newProject { get; set; }

        [BindProperty]
        public Tasks newTask { get; set; }

        [BindProperty]
        public string Action { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine($"Action: {Action}");
            //fix this
            //if (!ModelState.IsValid)
            //{
            //    Console.WriteLine("Model is not valid!");
            //    return Page();
            //}
            Console.WriteLine("Model valid");
            if (Action == "AddProject")
            {
                Console.WriteLine("Toggeled AddProject");
                Console.WriteLine($"Project: {newProject.Name}, {newProject.Description}");
                _dbContext.Projects.Add(newProject);
                await _dbContext.SaveChangesAsync();
            }
            else if (Action == "AddTask")
            {
                if (newTask.DateOfCompletion.Date < DateTime.Today)
                {
                    ModelState.AddModelError("newTask.DateOfCompletion", "Date is not valid");
                    return Page();
                }

                _dbContext.Tasks.Add(newTask);
                Console.WriteLine($"Task: {newTask.ProjectId}, {newTask.DateOfCreation}, {newTask.Description}");
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}