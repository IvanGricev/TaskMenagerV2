using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMenagerV2.Pages.Services;

namespace TaskMenagerV2.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }
        private readonly IUserService _userService;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //var users = await _userService.GetUsersByEmailAsync(Email);
            //foreach(var user in users)
            //{
            //    if (user != null && user.Password == Password)
            //    {
            //        HttpContext.Session.SetString("UserId", user.UserId.ToString());
            //        return RedirectToPage("/Account");
            //    }
            //}

            //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //return Page();
            var users = await _userService.GetUsersByEmailAsync(Email);
            if (users.Count > 0)
            {
                var user = users.First(); 
                if (user.Password == Password) 
                {
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    return RedirectToPage("/Account");
                }
                else
                {
                    ModelState.AddModelError("Password", "Incorrect password.");
                    return Page();
                }
            }
            else
            {
                ModelState.AddModelError("Email", "User not found.");
                return Page();
            }
        }
    }
}
