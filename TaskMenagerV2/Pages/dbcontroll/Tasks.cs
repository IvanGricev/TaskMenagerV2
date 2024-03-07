
using Microsoft.AspNetCore.Mvc;

namespace TaskMenager.Components.dbcontroll
{
    public class Tasks
    {
        public int Id { get; set; }
        
        [BindProperty]
        public int ProjectId { get; set; }
        
        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public DateTime DateOfCreation { get; set; }

        [BindProperty]
        public DateTime DateOfCompletion { get; set; }

        [BindProperty]
        public int Completion { get; set; }

        public Project Project { get; set; }

        public Tasks() { }

    }
}