using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Aut3.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Aut3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Aut3.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender; 
            _context = context;
        }

        public IEnumerable<SelectListItem> Units { get; set; }

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nazwa użytkownika")]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0}Hasło musi mieć co najmniej {2}, ale nie więcej niź {1} znaków.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potwierdż hasło")]
            [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
            public string ConfirmPassword { get; set; }
            
            [Required]
            [Display(Name = "Jednostka")]
            public string UnitName { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            List<MilitaryUnit> availableUnits = _context.MilitaryUnit.ToList();
            // convert to selectlistitems
            Units = availableUnits.Select(x => new SelectListItem() { Text = x.Name, Value = x.Name.ToString() }).ToList();
            Units = Units.Where(u => u.Text != "").ToList();

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            List<MilitaryUnit> availableUnits = _context.MilitaryUnit.ToList();
            Units = availableUnits.Select(x => new SelectListItem() { Text = x.Name, Value = x.Name.ToString() }).ToList();
            Units = Units.Where(u => u.Text != "").ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                  //  Unit = _context.MilitaryUnit.Where(x => x.MilitaryUnitId == Input.UnitId).FirstOrDefault()

                    
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var res2 = await _userManager.AddToRoleAsync(user, "User");
                    if (res2.Succeeded)
                        _logger.LogInformation("User has role assigned as: User");
                    else
                    {
                        _logger.LogInformation("ERROR CHOLERA");
                    }
                    var res3 = await _userManager.AddToRoleAsync(user, Input.UnitName);
                    if (res3.Succeeded)
                        _logger.LogInformation("User has role assigned as:" + Input.UnitName);
                    else
                    {
                        _logger.LogInformation("ERROR CHOLERA");
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new {area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl},
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new {email = Input.Email, returnUrl = returnUrl});
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}