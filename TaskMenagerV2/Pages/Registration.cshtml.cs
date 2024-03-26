using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMenagerV2.Pages.dbcontroll;
using TaskMenagerV2.Pages.Services;

namespace TaskMenagerV2.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly EmailService _emailService;

        public RegistrationModel(MyDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (!Consent)
            {
                ModelState.AddModelError("User.Name", "You must agree to the privacy policy.");
                return Page();
            }

            // Use the EmailService instance to send an email
            await _emailService.SendEmailAsync(User.Email, "Congrats on creating account", "Thank you for registering. Your account is now active.");

            // Save
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Account");
        }
    }
}
