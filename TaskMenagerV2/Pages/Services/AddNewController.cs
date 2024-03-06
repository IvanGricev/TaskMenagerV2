//using Microsoft.AspNetCore.Mvc;
//using TaskMenager.Components.dbcontroll;
//using TaskMenager.Components.services;
//using TaskMenager.Components.Services;

//namespace TaskMenagerV2.Pages.Services
//{
//    public class AddNewController : Controller
//    {
//        private readonly MyDbContext _dbContext;
//        private readonly IProjectService _projectService;
//        private readonly ITaskService _taskService;

//        public AddNewController(MyDbContext dbContext, IProjectService projectService, ITaskService taskService)
//        {
//            _dbContext = dbContext;
//            _projectService = projectService;
//            _taskService = taskService;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult AddProject(Project newProject)
//        {
//            if (ModelState.IsValid)
//            {
//                _projectService.AddProjectAsync(newProject);
//                newProject = new Project();
//            }

//            return View("Index", newProject);
//        }

//        [HttpPost]
//        public IActionResult AddTask(Tasks newTask)
//        {
//            if (ModelState.IsValid)
//            {
//                _taskService.AddTaskAsync(newTask);
//                newTask = new Tasks();
//            }

//            return View("Index", newTask);
//        }
//    }
//}
