using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookOrders.Areas.Identity.Data;
using BookOrders.Infrastructure;
using BookOrders.Models;
using BookOrders.Models.Category;
using BookOrders.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookOrders.Areas.Admin.Pages.Category
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<BookOrdersUser> _userManager;

        public EditModel(ICategoryService categoryService, UserManager<BookOrdersUser> userManager)
        {
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public CategoryInputModel Input { get; set; }

        public IList<SelectListItem> Categories { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Input = await _categoryService.GetByIdAsync(id);
            if (Input == null)
            {
                return BadRequest("Не може да бъде намерена категория.");
            }

            Categories = await _categoryService.CategoriesListItemsAsync(Input.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _categoryService.CategoriesListItemsAsync(Input.Id);
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                StatusMessage = $"Error. {Constants.UnindentIfied_User}";
                Categories = await _categoryService.CategoriesListItemsAsync(Input.Id);

                return Page();
            }

            var result = await _categoryService.EditCategoryAsync(Input, user.Id);
            if (!result.success)
            {
                StatusMessage = $"Error. {result.msg}'.";
                Categories = await _categoryService.CategoriesListItemsAsync(Input.Id);

                return Page();
            }

            StatusMessage = result.msg;
            Categories = await _categoryService.CategoriesListItemsAsync(Input.Id);

            return Page();   
        }
    }
}