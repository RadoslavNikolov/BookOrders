using BookOrders.Models;
using BookOrders.Models.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrders.Services.Interfaces
{
    public interface ICategoryService : IDisposable
    {
        CategoryInputModel GetById(int? id);

        Task<CategoryInputModel> GetByIdAsync(int? id);

        IOrderedQueryable<CategoryViewModel> GetCategiories();

        IList<SelectListItem> CategoriesListItems(int? excludedCategory = null);

        Task<IList<SelectListItem>> CategoriesListItemsAsync(int? excludedCategory = null);

        (bool success, string msg) AddCategory(CategoryInputModel model, string userId);

        Task<(bool success, string msg)> AddCategoryAsync(CategoryInputModel model, string userId);

        (bool success, string msg) EditCategory(CategoryInputModel model, string userId);

        Task<(bool success, string msg)> EditCategoryAsync(CategoryInputModel model, string userId);

        (string status, string msg) DisableCategory(string name);

        Task<(string status, string msg)> DisableCategoryAsync(string name);

        (string status, string msg) EnableCategory(string name);

        Task<(string status, string msg)> EnableCategoryAsync(string name);
    }
}
