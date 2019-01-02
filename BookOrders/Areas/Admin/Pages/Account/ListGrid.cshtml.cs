using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BookOrders.Models;
using BookOrders.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookOrders.Areas.Admin.Pages.Account.Grids
{
    [Authorize(Roles = "Admin")]
    public class ListGridModel : PageModel
    {
        private readonly BookOrdersContext _context;
        public ListGridModel(BookOrdersContext context)
        {
            _context = context;
        }

        public IOrderedQueryable<UserModel> Users { get; set; }
        public void OnGet()
        {
            Users = _context.Users.AsNoTracking()
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    EmailConfirmed = u.EmailConfirmed,
                    Disabled = u.Disabled,
                })
                .OrderBy(u => u.FirstName);
        }
    }
}