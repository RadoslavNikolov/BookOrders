using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using Microsoft.EntityFrameworkCore;

namespace BookOrders.Areas.Admin.Pages.Category
{
    [Authorize(Roles = "Admin")]
    public class NewModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<BookOrdersUser> _userManager;

        public NewModel(ICategoryService categoryService, UserManager<BookOrdersUser> userManager)
        {
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public CategoryInputModel Input { get; set; }

        public IList<SelectListItem> Categories { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await _categoryService.CategoriesListItemsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _categoryService.CategoriesListItemsAsync();
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                StatusMessage = $"Error. {Constants.UnindentIfied_User}";
                Categories = await _categoryService.CategoriesListItemsAsync();

                return Page();
            }

            var result = await _categoryService.AddCategoryAsync(Input, user.Id);
            if (!result.success)
            {
                StatusMessage = $"Error. {result.msg ?? ""}";
                Categories = await _categoryService.CategoriesListItemsAsync();

                return Page();
            }

            StatusMessage = result.msg;
            Categories = await _categoryService.CategoriesListItemsAsync();

            return Page();
        }
    }
}