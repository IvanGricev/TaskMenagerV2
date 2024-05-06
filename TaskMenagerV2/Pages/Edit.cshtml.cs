using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMenager.Components.dbcontroll;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskMenager.Components.services;

namespace TaskMenagerV2.Pages
{
    public class EditModel : PageModel
    {
        private readonly MyDbContext _dbContext;
        private readonly ProjectService _projectService;
        private readonly Taskservice _taskService;
        public EditModel(MyDbContext dbContext, ProjectService projectService, Taskservice Taskservice)
        {
            _dbContext = dbContext;
            _projectService = projectService;
            editProject = new Project();
            editTask = new Tasks();
            _taskService = Taskservice;
        }

        [BindProperty]
        public Project editProject { get; set; }

        [BindProperty]
        public Tasks editTask { get; set; }

        [BindProperty]
        public string Action { get; set; }

        public List<Project> Projects { get; set; }

        public IActionResult OnGet()
        {
            Projects = _dbContext.Projects.Include(p => p.Tasks).ToList();
            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (Action == "EditProject")
            {
                var project = await _dbContext.Projects.FindAsync(editProject.ProjectId);
                if (project != null)
                {
                    project.Name = editProject.Name;
                    project.Description = editProject.Description;
                    await _dbContext.SaveChangesAsync();
                }
            }
            else if (Action == "EditTask")
            {
                var task = await _dbContext.Tasks.FindAsync(editTask.Id);
                if (task != null)
                {
                    task.ProjectId = editTask.ProjectId;
                    task.Description = editTask.Description;
                    await _dbContext.SaveChangesAsync();
                }
            }
            else if (Action == "DeleteProject")
            {
                var projectD = await _dbContext.Projects.FindAsync(editProject.ProjectId);
                if (projectD != null)
                {
                    await _projectService.DeleteProjectAsync(projectD.ProjectId);
                }
            }
            else if (Action == "DeleteTask")
            {
                var taskD = await _dbContext.Tasks.FindAsync(editTask.Id);
                if (taskD != null)
                {
                    await _taskService.DeleteTaskAsync(taskD.Id);
                }
            }
            return RedirectToPage("Index");
        }
    }
}
