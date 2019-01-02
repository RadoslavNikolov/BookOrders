using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookOrders.Areas.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class RoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        
        public IOrderedQueryable<IdentityRole> Roles { get; set; }

        public void OnGet()
        {
            Roles = _roleManager.Roles.AsNoTracking()
                .OrderBy(r => r.Name);
        }
    }
}