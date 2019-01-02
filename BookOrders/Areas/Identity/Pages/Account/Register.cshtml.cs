using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BookOrders.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using reCAPTCHA.AspNetCore;
using BookOrders.Infrastructure;

namespace BookOrders.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<BookOrdersUser> _signInManager;
        private readonly UserManager<BookOrdersUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private IRecaptchaService _recaptcha;

        public RegisterModel(
            UserManager<BookOrdersUser> userManager,
            SignInManager<BookOrdersUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IRecaptchaService recaptcha)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _recaptcha = recaptcha;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "'{0}' е задължително.")]
            [DataType(DataType.Text)]
            [Display(Name = "Username")]
            //[RegularExpression(@"^[a-zA-Zа-яА-я0-9''-'\s]$", ErrorMessage = "Непозволени символи. Моля, използвайте малки, големи букви или цифри.")]
            [StringLength(40, MinimumLength = 3, ErrorMessage = "'{0}' трябва да е не по-малко от {2} и не повече от {1} букви или цифри.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "'{0}' е задължително.")]
            [DataType(DataType.Text)]
            [Display(Name = "First name")]
            //[RegularExpression(@"^[a-zA-Zа-яА-я0-9''-'\s]$", ErrorMessage = "Непозволени символи. Моля, използвайте малки, големи букви или цифри.")]
            [StringLength(40, MinimumLength = 3, ErrorMessage = "'{0}' трябва да е не по-малко от {2} и не повече от {1} букви или цифри.")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "'{0}' е задължително.")]
            [DataType(DataType.Text)]
            [Display(Name = "Last name")]
            //[RegularExpression(@"^[a-zA-Zа-яА-я0-9''-'\s]$", ErrorMessage = "Непозволени символи. Моля, използвайте малки, големи букви или цифри.")]
            [StringLength(40, MinimumLength = 3, ErrorMessage = "'{0}' трябва да е не по-малко от {2} и не повече от {1} букви или цифри.")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "'{0}' е задължително.")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "'{0}' е задължително.")]
            [StringLength(100, ErrorMessage = "'{0}' трябва да е не по-малко от {2} и не повече от {1} символи.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Парола и потвърди парола не съвпадат.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var recaptcha = await _recaptcha.Validate(Request);
            if (!recaptcha.success)
            {
                ModelState.AddModelError("Recaptcha", Constants.ReCaptcha_Error);
                return Page();
            }

            if (ModelState.IsValid)
            {
                var user = new BookOrdersUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Потвърждение на електронна поща",
                        $"Моля, потвърдете вашето потребителско име чрез <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>натисни тук</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
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
