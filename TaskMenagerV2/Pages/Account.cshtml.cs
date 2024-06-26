﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMenagerV2.Pages.dbcontroll;
using TaskMenagerV2.Pages.Services;


namespace TaskMenagerV2.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly MyDbContext _dbContext;
        private readonly EmailService _emailService;

        public AccountModel(IUserService userService, MyDbContext dbContext, EmailService emailService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _emailService = emailService;
        }

        //password
        [BindProperty]
        public User UserC { get; set; }

        public async Task<IActionResult> OnPostUpdatePasswordAsync(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if(UserC.Name != null)
            {
                user.Name = UserC.Name;
            }
            if(UserC.Password != null)
            {
                user.Password = UserC.Password;
            }
            if(UserC.Email != null)
            {
                await _emailService.SendEmailAsync(user.Email, "Account data changing!", "You have changed your email.");
                user.Email = UserC.Email;
                await _emailService.SendEmailAsync(user.Email, "Account data changing!", "This is now your email.");
            }
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/Account");
        }


        //logout
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Account");
        }

        public async Task<IActionResult> OnPostDeleteAccountAsync(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(userId);
            HttpContext.Session.Clear(); 
            return RedirectToPage("/Account");
        }

        //achivements
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {

                //achivements
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
                                foreach (var task in tasks.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
                                {
                                    if (task.Completion == 1 && task.DateOfCreation == DateTime.Today)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 2:
                                foreach (var task in tasks.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
                                {
                                    if (task.Completion == 1)
                                    {
                                        achievementsList[i] = 1;
                                        break;
                                    }
                                }
                                break;

                            case 3:
                                foreach (var project in projects.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
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
                                foreach (var task in tasks.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
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
                                foreach (var task in tasks.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
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
                                foreach (var task in tasks.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
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
                                foreach (var project in projects.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
                                {
                                    if(project.Tasks != null)
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
                                }
                                break;

                            case 8:
                                foreach (var task in tasks.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
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
                                foreach (var project in projects.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
                                {
                                    if (project.Tasks != null)
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
                                }

                                if (counterP >= 10)
                                {
                                    achievementsList[i] = 1;
                                    break;
                                }
                                break;

                            case 10:
                                foreach (var task in tasks.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
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
                                foreach (var task in tasks.Where(t => t.UserId == int.Parse(HttpContext.Session.GetString("UserId"))))
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
