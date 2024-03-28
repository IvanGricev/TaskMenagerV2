using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskMenagerV2.Pages.dbcontroll;
using TaskMenagerV2.Pages.Services;

namespace TaskMenagerV2.Pages
{
    public class RestorePasswordModel : PageModel
    {
        private readonly EmailService _emailService;
        private readonly IUserService _userService;
        private readonly MyDbContext _dbContext;

        public RestorePasswordModel(IUserService userService, MyDbContext dbContext, EmailService emailService)
        {
            _userService = userService;
            _dbContext = dbContext;
            _emailService = emailService;
        }

        [BindProperty]
        public User UserC { get; set; }

        [HttpPost]
        public async Task<IActionResult> OnPostSendEmailAsync(string Email)
        {
            var confirmationNumber = new Random().Next(1000, 9999);
            HttpContext.Session.SetInt32("ConfirmationNumber", confirmationNumber);

            await _emailService.SendEmailAsync(Email, "Confirmation", $"Your confirmation number is {confirmationNumber}.");

            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpdatePasswordAsync(int userId, string Email, int Conf)
        {
            var savedConfirmationNumber = HttpContext.Session.GetInt32("ConfirmationNumber");
            if (Conf != savedConfirmationNumber)
            {
                return Page();
            }

            var users = await _userService.GetUsersByEmailAsync(Email);
            var user = users.FirstOrDefault();
            user.Password = UserC.Password;
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/Account");
        }

    }
}
