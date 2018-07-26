namespace SimpleChat.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using SimpleChat.Messaging.Entities;

    public class SignUpModel : PageModel
    {
        private readonly UserManager<User> userManager;

        public SignUpModel(UserManager<User> userManager)
            => this.userManager = userManager;

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = $"Ошибка регистрации: {string.Join(',', ModelState.Keys.Select(x => $"<li>{x}</li>"))}";
                return Page();
            }

            var user = await userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                Message = $"Ошибка регистрации: пользователь с e-mail {Email} уже существует";
                return Page();
            }

            user = new User { Name = Name, Email = Email };
            var userCreateResult = await userManager.CreateAsync(user, Password);

            if (!userCreateResult.Succeeded)
            {
                Message = $"Ошибка регистрации: {string.Join(',', userCreateResult.Errors.Select(x => $"<li>{x.Description}</li>"))}";
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}