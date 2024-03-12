using Microsoft.AspNetCore.Mvc;

namespace TaskMenagerV2.Pages.dbcontroll
{
    public class Achievements
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [BindProperty]
        public int Completed { get; set; }

    }
}
