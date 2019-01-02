using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookOrders.Areas.Admin.Pages.Role
{
    [Authorize(Roles = "Admin")]
    public class NewModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public NewModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "'{0}' е задължително.")]
            [DataType(DataType.Text)]
            [Display(Name = "Role")]
            //[RegularExpression(@"^[a-zA-Zа-яА-я0-9''-'\s]$", ErrorMessage = "Непозволени символи. Моля, използвайте малки, големи букви или цифри.")]
            [StringLength(40, MinimumLength = 3, ErrorMessage = "'{0}' трябва да е не по-малко от {2} и не повече от {1} букви или цифри.")]
            public string Name { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await _roleManager.RoleExistsAsync(Input.Name))
                {
                    StatusMessage = $"Warning. Роля '{Input.Name}' вече съществува.";

                    return Page();
                }

                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = Input.Name,
                    NormalizedName = Input.Name.ToUpper()
                });

                StatusMessage = $"Успешно създадена роля '{Input.Name}.'";
            }
            return Page();
        }
    }
}