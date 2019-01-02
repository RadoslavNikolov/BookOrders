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

namespace BookOrders.Areas.Admin.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<BookOrdersUser> _userManager;
        private readonly BookOrdersContext _context;

        public IndexModel(UserManager<BookOrdersUser> userManager, BookOrdersContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public List<SelectListItem> Roles { get; set; }

        [BindProperty]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
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
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Role")]
            public string Role { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound($"Не може да бъде намерен потребител '{username}'.");
            }

            Roles = await _context.Roles.AsNoTracking()
               .Select(r => new SelectListItem
               {
                   Value = r.Name,
                   Text = r.Name
               }).ToListAsync();
            Roles.Insert(0, new SelectListItem { Value = "", Text = "--Избери роля--" });

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            Username = username;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Role = userRoles.FirstOrDefault()
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

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

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user  '{Username}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user '{Username}'.");
                }
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles.Where(r => !r.Equals(Input.Role, StringComparison.OrdinalIgnoreCase)));
            if (!string.IsNullOrWhiteSpace(Input.Role))
            {
                await _userManager.AddToRoleAsync(user, Input.Role);
            }

            await _userManager.UpdateAsync(user);

            StatusMessage = "Профилът е променен";
            return RedirectToPage(new { username = Username });
        }
    }
}