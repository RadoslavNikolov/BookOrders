using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookOrders.Infrastructure;
using BookOrders.Models;
using BookOrders.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookOrders.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return Redirect("/Admin/Category/Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> DisableCategory(string name)
        {
            var result = await _categoryService.DisableCategoryAsync(name);

            return Json(new { result.status, result.msg });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> EnableCategory(string name)
        {
            var result = await _categoryService.EnableCategoryAsync(name);

            return Json(new { result.status, result.msg});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_categoryService != null)
                {
                    _categoryService.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}