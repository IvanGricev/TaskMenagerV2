using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskMenager.Components.dbcontroll;
using TaskMenagerV2.Pages.dbcontroll;

namespace TaskMenagerV2.Pages
{
    public class AchievementsModel : PageModel
    {
        private readonly MyDbContext _dbContext;
        public AchievementsModel(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task OnGetAsync()
        {
            //var achievementsToUpdate = new List<Achievements>();
            //var taskss = new List<Tasks>();
            //var projectss = new List<Project>();
            var achievementsToUpdate = _dbContext.Achievements.ToList();
            var taskss = _dbContext.Tasks.ToList();
            var projectss = _dbContext.Projects.ToList();

            var achievementsToAdd = new List<Achievements>();


            foreach (var achivement in achievementsToUpdate)
            {
                if(achivement.Completed == 1)
                {
                    break;
                }
                switch (achivement.Id)
                {
                    case 1:
                        foreach(var task in taskss)
                        {
                            if(task.Completion == 1 && task.DateOfCreation == DateTime.Today)
                            {
                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;

                    case 2:
                        foreach (var task in taskss)
                        {
                            if (task.Completion == 1)
                            {
                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;
                    
                    case 3:
                        foreach (var project in projectss)
                        {
                            if (project.ProjectId >= 0)
                            {
                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;

                    case 4:
                        int counter = 0;
                        foreach (var task in taskss)
                        {
                            if (task.Completion == 1)
                            {
                                counter++;
                            }
                            if(counter >= 10)
                            {

                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;
                    
                    case 5:
                        counter = 0;
                        foreach (var task in taskss)
                        {
                            if (task.Completion == 1)
                            {
                                counter++;
                            }
                            if (counter >= 50)
                            {

                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;

                    case 6:
                        counter = 0;
                        foreach (var task in taskss)
                        {
                            if (task.Completion == 1)
                            {
                                counter++;
                            }
                            if (counter >= 100)
                            {

                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;
                    
                    case 7:
                        counter = 0;
                        foreach (var project in projectss)
                        {
                            foreach(var task in project.Tasks)
                            {
                                if(task.Completion == 1)
                                {
                                    counter++;
                                }
                            }

                            if (counter == project.Tasks.Count)
                            {
                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;

                    case 8:
                        foreach (var task in taskss)
                        {
                            if (task.DateOfCompletion < DateTime.Now)
                            {
                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;

                    case 9:
                        counter = 0;
                        int counterP = 0;
                        foreach (var project in projectss)
                        {
                            foreach (var task in project.Tasks)
                            {
                                if (task.Completion == 1)
                                {
                                    counter++;
                                }
                            }
                            if(counter == project.Tasks.Count)
                            {
                                counterP++;
                            }
                        }

                        if (counterP >= 10)
                        {
                            achivement.Completed = 1;
                            achievementsToAdd.Add(achivement);
                            break;
                        }
                        break;

                    case 10:
                        foreach (var task in taskss)
                        {
                            var oneWeekAgo = DateTime.Now.AddDays(-7);

                            if (task.DateOfCompletion <= oneWeekAgo)
                            {
                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;

                    case 11:

                        break;

                    case 12:
                        foreach (var task in taskss)
                        {
                            if(task.Description.Length >= 1000)
                            {
                                achivement.Completed = 1;
                                achievementsToAdd.Add(achivement);
                                break;
                            }
                        }
                        break;
                }
            }

            achievementsToUpdate.AddRange(achievementsToAdd);

            if (achievementsToUpdate.Any())
            {
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
