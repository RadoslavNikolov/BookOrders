using BookOrders.Areas.Admin.Pages.Category;
using BookOrders.Data.Models;
using BookOrders.Infrastructure;
using BookOrders.Models;
using BookOrders.Models.Category;
using BookOrders.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrders.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BookOrdersContext _context;
        private bool disposed = false;

        public CategoryService(BookOrdersContext context)
        {
            _context = context;
        }

        public CategoryInputModel GetById(int? id)
        {
            var category = _context.Categories.Find(id ?? 0);
            if (category == null)
            {
                return null;
            }

            return new CategoryInputModel
            {
                Id = category.Id,
                Name = category.Name,
                Code = category.Code,
                Descriprion = category.Descriprion,
                ParentId = category.ParentId
            };
        }

        public async Task<CategoryInputModel> GetByIdAsync(int? id)
        {
            var category = await _context.Categories.FindAsync(id ?? 0);
            if (category == null)
            {
                return null;
            }

            return new CategoryInputModel
            {
                Id = category.Id,
                Name = category.Name,
                Code = category.Code,
                Descriprion = category.Descriprion,
                ParentId = category.ParentId
            };
        }

        public IOrderedQueryable<CategoryViewModel> GetCategiories()
        {
            return _context.Categories.AsNoTracking()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    NameNormalized = c.NameNormalized,
                    Code = c.Code,
                    Identifier = c.Identifier,
                    Descriprion = c.Descriprion,
                    Disabled = c.Disabled,
                    ParentName = c.Parent != null ? c.Parent.Name : "",
                    CreatedAtUtc = c.CreatedAtUtc,
                    LastModifiedAtUtc = c.LastModifiedAtUtc,
                    LastModifiedBy = c.LastModifier != null ? c.LastModifier.UserName : ""
                })
                .OrderByDescending(c => c.ParentId)
                .ThenBy(c => c.Name);
        }

        public IList<SelectListItem> CategoriesListItems(int? excludedCategory = null)
        {
            var categories = _context.Categories.AsNoTracking()
                .Where(c => excludedCategory == null || c.Id != excludedCategory)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            categories.Insert(0, new SelectListItem { Value = "", Text = "--Избери родителска категория--" });

            return categories;
        }

        public async Task<IList<SelectListItem>> CategoriesListItemsAsync(int? excludedCategory = null)
        {
            var categories = await _context.Categories.AsNoTracking()
                .Where(c => excludedCategory == null || c.Id != excludedCategory)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            categories.Insert(0, new SelectListItem { Value = "", Text = "--Избери родителска категория--" });

            return categories;
        }

        public (bool success, string msg) AddCategory(CategoryInputModel model, string userId)
        {
            if (_context.Categories.Any(c => c.NameNormalized == model.NameNormmalized))
            {
                return (success: false, msg: $"Вече съществува категория '{(model.Name ?? "")}'.");
            }

            var newCategory = new Category
            {
                Name = model.Name.Trim(),
                NameNormalized = model.NameNormmalized,
                Code = model.Code,
                Descriprion = model.Descriprion,
                LastModifiedId = userId,
                Identifier = Guid.NewGuid(),
                ParentId = model.ParentId
            };

            _context.Categories.Add(newCategory);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return (success: false, msg: Constants.Error_Try_Again);
            }

            return (success: true, msg: $"Успешно създадена категория '{newCategory.Name}'.");
        }

        public async Task<(bool success, string msg)> AddCategoryAsync(CategoryInputModel model, string userId)
        {
            if (await _context.Categories.AnyAsync(c => c.NameNormalized == model.NameNormmalized))
            {
                return (success: false, msg: $"Вече съществува категория '{(model.Name ?? "")}'.");
            }

            var newCategory = new Category
            {
                Name = model.Name.Trim(),
                NameNormalized = model.NameNormmalized,
                Code = model.Code,
                Descriprion = model.Descriprion,
                LastModifiedId = userId,
                Identifier = Guid.NewGuid(),
                ParentId = model.ParentId
            };

            _context.Categories.Add(newCategory);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return (success: false, msg: Constants.Error_Try_Again);
            }

            return (success: true, msg: $"Успешно създадена категория '{newCategory.Name}'.");
        }

        public (bool success, string msg) EditCategory(CategoryInputModel model, string userId)
        {
            var normalizedName = (model.Name ?? "").Trim().ToUpper();
            if (_context.Categories.Any(c => c.NameNormalized == normalizedName && c.Id != model.Id))
            {
                return (success: false, msg: $"Вече съществува категория '{(model.Name ?? "")}'.");
            }

            var category = _context.Categories.Find(model.Id);
            category.Name = model.Name.Trim();
            category.NameNormalized = normalizedName;
            category.Code = model.Code;
            category.Descriprion = model.Descriprion;
            category.LastModifiedId = userId;
            category.ParentId = model.ParentId;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return (success: false, msg: Constants.Error_Try_Again);
            }

            return (success: true, msg: $"Успешно редактирана категория '{category.Name}'.");
        }

        public async Task<(bool success, string msg)> EditCategoryAsync(CategoryInputModel model, string userId)
        {
            var normalizedName = (model.Name ?? "").Trim().ToUpper();
            if (await _context.Categories.AnyAsync(c => c.NameNormalized == normalizedName && c.Id != model.Id))
            {
                return (success: false, msg: $"Вече съществува категория '{(model.Name ?? "")}'.");
            }

            var category = await _context.Categories.FindAsync(model.Id);
            category.Name = model.Name.Trim();
            category.NameNormalized = normalizedName;
            category.Code = model.Code;
            category.Descriprion = model.Descriprion;
            category.LastModifiedId = userId;
            category.ParentId = model.ParentId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return (success: false, msg: Constants.Error_Try_Again);
            }

            return (success: true, msg: $"Успешно редактирана категория '{category.Name}'.");
        }

        public (string status, string msg) DisableCategory(string name)
        {
            var category = _context.Categories.FirstOrDefault(c => c.NameNormalized == name);
            if (category == null)
            {
                return (status: "error", msg: $"Не може да бъде намерена категория '{(name ?? "")}'.");
            }

            if (category.Disabled)
            {
                return (status: "warning", msg: $"Категория '{(name ?? "")}' вече е деактивирана.");
            }

            category.Disabled = true;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return (status: "error", msg: Constants.Error_Try_Again);
            }

            return (status: "success", msg: $"Успешно деактивирана категория '{(name ?? "")}'.");
        }

        public async Task<(string status, string msg)> DisableCategoryAsync(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.NameNormalized == name);
            if (category == null)
            {
                return (status: "error", msg: $"Не може да бъде намерена категория '{(name ?? "")}'.");
            }

            if (category.Disabled)
            {
                return (status: "warning", msg: $"Категория '{(name ?? "")}' вече е деактивирана.");
            }

            category.Disabled = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return (status: "error", msg: Constants.Error_Try_Again);
            }

            return (status: "success", msg: $"Успешно деактивирана категория '{(name ?? "")}'.");
        }

        public (string status, string msg) EnableCategory(string name)
        {
            var category = _context.Categories.FirstOrDefault(c => c.NameNormalized == name);
            if (category == null)
            {
                return (status: "error", msg: $"Не може да бъде намерена категория '{(name ?? "")}'.");
            }

            if (!category.Disabled)
            {
                return (status: "warning", msg: $"Категория '{(name ?? "")}' вече е активна.");
            }

            category.Disabled = false;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return (status: "error", msg: Constants.Error_Try_Again);
            }

            return (status: "success", msg: $"Успешно активирана категория '{(name ?? "")}'.");
        }

        public async Task<(string status, string msg)> EnableCategoryAsync(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.NameNormalized == name);
            if (category == null)
            {
                return (status: "error", msg: $"Не може да бъде намерена категория '{(name ?? "")}'.");
            }

            if (!category.Disabled)
            {
                return (status: "warning", msg: $"Категория '{(name ?? "")}' вече е активна.");
            }

            category.Disabled = false;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return (status: "error", msg: Constants.Error_Try_Again);
            }

            return (status: "success", msg: $"Успешно активирана категория '{(name ?? "")}'.");
        }
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any other managed objects here.
                _context.Dispose();
            }

            // Free any unmanaged objects here.

            disposed = true;
        }

        ~CategoryService()
        {
            Dispose(false);
        }
    }
}
