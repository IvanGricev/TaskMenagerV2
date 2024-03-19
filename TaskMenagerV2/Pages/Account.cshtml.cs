using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskMenagerV2.Pages.dbcontroll;
using TaskMenagerV2.Pages.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskMenagerV2.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly MyDbContext _dbContext;

        public AccountModel(IUserService userService, MyDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                int userId = int.Parse(HttpContext.Session.GetString("UserId"));
                var user = await _userService.GetUserByIdAsync(userId);
                ViewData["User"] = user;

                var achievementsString = user.Achivements;
                var achievementsList = achievementsString.Split(',').Select(int.Parse).ToList();

                var tasks = _dbContext.Tasks.ToList();
                var projects = _dbContext.Projects.ToList();

                for (int i = 0; i < achievementsList.Count; i++)
                {
                    if (achievementsList[i] == 0)
                    {
                        switch (i + 1)
                        {
                            case 1:
                                foreach (var task in tasks)
                                {
                                    if (task.Completion == 1 && task.DateOfCreation == DateTime.Today)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 2:
                                foreach (var task in tasks)
                                {
                                    if (task.Completion == 1)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 3:
                                foreach (var project in projects)
                                {
                                    if (project.ProjectId >= 0)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 4:
                                int counter = 0;
                                foreach (var task in tasks)
                                {
                                    if (task.Completion == 1)
                                    {
                                        counter++;
                                    }
                                    if (counter >= 10)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 5:
                                counter = 0;
                                foreach (var task in tasks)
                                {
                                    if (task.Completion == 1)
                                    {
                                        counter++;
                                    }
                                    if (counter >= 50)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 6:
                                counter = 0;
                                foreach (var task in tasks)
                                {
                                    if (task.Completion == 1)
                                    {
                                        counter++;
                                    }
                                    if (counter >= 100)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 7:
                                counter = 0;
                                foreach (var project in projects)
                                {
                                    foreach (var task in project.Tasks)
                                    {
                                        if (task.Completion == 1)
                                        {
                                            counter++;
                                        }
                                    }

                                    if (counter == project.Tasks.Count)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 8:
                                foreach (var task in tasks)
                                {
                                    if (task.DateOfCompletion < DateTime.Now)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 9:
                                counter = 0;
                                int counterP = 0;
                                foreach (var project in projects)
                                {
                                    foreach (var task in project.Tasks)
                                    {
                                        if (task.Completion == 1)
                                        {
                                            counter++;
                                        }
                                    }
                                    if (counter == project.Tasks.Count)
                                    {
                                        counterP++;
                                    }
                                }

                                if (counterP >= 10)
                                {
                                    achievementsList[i] = 1;
                                    break;
                                }
                                break;

                            case 10:
                                foreach (var task in tasks)
                                {
                                    var oneWeekAgo = DateTime.Now.AddDays(-7);

                                    if (task.DateOfCompletion <= oneWeekAgo)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 11:
                                achievementsList[i] = 1;
                                break;

                            case 12:
                                foreach (var task in tasks)
                                {
                                    if (task.Description.Length >= 1000)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;
                        }
                    } 
                }

                user.Achivements = string.Join(",", achievementsList);
                await _userService.UpdateUserAsync(user);
            }
            else
            {
                ViewData["User"] = null;
            }

            return Page();
        }
    }
}
