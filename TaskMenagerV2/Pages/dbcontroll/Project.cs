
using Microsoft.AspNetCore.Mvc;

namespace TaskMenager.Components.dbcontroll
{
    public class Project
    {
        public int ProjectId { get; set; }
        
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Description { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
        public Project() { }
    }
}