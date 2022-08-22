using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using TcOpen.Inxton.Local.Security.Blazor.Users;
using TcOpen.Inxton.Local.Security.Blazor.Stores;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace TcOpen.Inxton.Local.Security.Blazor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private BlazorAlertManager _alertManager { get; set; }

        public LoginModel(ILogger<LoginModel> logger,
            BlazorAlertManager _alertManager)
        {
            _logger = logger;
            this._alertManager = _alertManager;
        }

        [BindProperty]
        public LoginUserModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public IActionResult OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

           
            if (ModelState.IsValid)
            {
                try
                {
                    SecurityManager.Manager.Service.AuthenticateUser(Input.Username, Input.Password);
                    _logger.LogInformation("User logged in.");
                    TcoAppDomain.Current.Logger.Information($"User '{Input.Username}' logged in. {{@sender}}", new { UserName = Input.Username });
                    //_alertManager.addAlert("success", "User logged in.");
                    return LocalRedirect(returnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
