using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookOrders.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookOrders.Areas.Admin.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class SetPasswordModel : PageModel
    {
        private readonly UserManager<BookOrdersUser> _userManager;

        public SetPasswordModel(UserManager<BookOrdersUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "'{0}' е задължително.")]
            [StringLength(100, ErrorMessage = "'{0}' трябва да е не по-малко от {2} и не повече от {1} символи.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "'Парола' и 'Потвърди новата парола' не съвпадат.")]
            public string ConfirmPassword { get; set; }


        }

        public async Task<IActionResult> OnGetAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound($"Не може да бъде намерен потребител '{username}'.");
            }

            Username = await _userManager.GetUserNameAsync(user);
            TempData["Code"] = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Username);
            if (user == null)
            {
                return NotFound($"Не може да бъде намерен потребител '{Username}'.");
            }

            var code = TempData.ContainsKey("Code") ? (string)TempData["Code"] : "";
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest("За промяна на паролата трябва да бъде предоставен код.");
            }
            var addPasswordResult = await _userManager.ResetPasswordAsync(user, code, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["Code"] = code;
                return Page();
            }

            StatusMessage = "Паролата е променен";
            return RedirectToPage(new { username = Username });
        }
    }
}