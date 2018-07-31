namespace SimpleChat.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using SimpleChat.Messaging.Entities;

    public class SignInModel : PageModel
    {
        private readonly UserManager<User> userManager;

        public SignInModel(UserManager<User> userManager)
            => this.userManager = userManager;

        [Required(ErrorMessage = "Не указан e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = $"Ошибка аутентификации: {string.Join(',', ModelState.Keys.Select(x => $"<li>{x}</li>"))}";
                return;
            }

            var user = await userManager.FindByEmailAsync(Email);
            if (user == null || !(await userManager.CheckPasswordAsync(user, Password)))
            {
                Message = $"Ошибка аутентификации: неправильный пароль или e-mail полльзователя";
                return;
            }

            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity));

            RedirectToPage("Index");
        }
    }
}