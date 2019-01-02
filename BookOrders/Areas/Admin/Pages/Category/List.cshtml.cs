using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookOrders.Models;
using BookOrders.Models.Category;
using BookOrders.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookOrders.Areas.Admin.Pages.Category
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public ListModel(BookOrdersContext context, ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IOrderedQueryable<CategoryViewModel> Categories { get; set; }

        public void OnGet()
        {
            Categories = _categoryService.GetCategiories();
        }
    }
}