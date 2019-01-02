using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookOrders.Areas.Identity.Data;
using BookOrders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookOrders.Areas.Admin.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class NewModel : PageModel
    {
        private readonly UserManager<BookOrdersUser> _userManager;
        private readonly ILogger<NewModel> _logger;
        private readonly BookOrdersContext _context;

        public NewModel(UserManager<BookOrdersUser> userManager,
            ILogger<NewModel> logger,
            BookOrdersContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        public List<SelectListItem> Roles { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "'{0}' е задължително.")]
            [DataType(DataType.Text)]
            [Display(Name = "Username")]
            [StringLength(40, MinimumLength = 3, ErrorMessage = "'{0}' трябва да е не по-малко от {2} и не повече от {1} букви или цифри.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "'{0}' е задължително.")]
            [DataType(DataType.Text)]
            [Display(Name = "First name")]
            [StringLength(40, MinimumLength = 3, ErrorMessage = "'{0}' трябва да е не по-малко от {2} и не повече от {1} букви или цифри.")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "'{0}' е задължително.")]
            [DataType(DataType.Text)]
            [Display(Name = "Last name")]
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

            [Display(Name = "Role")]
            public string Role { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            Roles = await _context.Roles.AsNoTracking()
                .Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToListAsync();
            Roles.Insert(0, new SelectListItem { Value = "", Text = "--Избери роля--" });

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new BookOrdersUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    EmailConfirmed = true
                };

                var msg = "";
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(Input.Role))
                    {
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }

                    msg = $"Успешно създаден потребител '{user.UserName}'";
                    _logger.LogInformation(msg);

                    StatusMessage = msg;
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