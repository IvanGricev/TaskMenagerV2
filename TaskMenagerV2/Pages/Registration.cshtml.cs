using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using TaskMenagerV2.Pages.dbcontroll;
using TaskMenagerV2.Pages.Services;

namespace TaskMenagerV2.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly EmailService _emailService;
        private readonly UserService _userService;

        public RegistrationModel(MyDbContext context, EmailService emailService, UserService userService)
        {
            _context = context;
            _emailService = emailService;
            _userService = userService;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public bool Consent { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingUser = await _userService.GetUsersByEmailAsync(User.Email);
            if (existingUser.Any())
            {
                ModelState.AddModelError("User.Email", "User with this email already exists.");
                return Page();
            }

            if (!Consent)
            {
                ModelState.AddModelError("User.Name", "You must agree to the privacy policy.");
                return Page();
            }

            if (!Regex.IsMatch(User.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$%*?&])[A-Za-z\d@$%*?&]{8,}$"))
            {
                ModelState.AddModelError("User.Password", "Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character.");
                return Page();
            }

            await _userService.AddUserAsync(User);

            return RedirectToPage("/Account");
        }
    }
}
