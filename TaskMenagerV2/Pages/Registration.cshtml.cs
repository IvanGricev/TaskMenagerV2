using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMenagerV2.Pages.dbcontroll;

namespace TaskMenagerV2.Pages
{
    public class RegistrationModel : PageModel
    {
        public void OnGet()
        {
        }
        private readonly MyDbContext _context;

        [BindProperty]
        public User User { get; set; }

        public RegistrationModel(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add the new user to the database
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Account");
        }
    }
}
