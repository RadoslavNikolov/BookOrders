using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookOrders.Areas.Identity.Data;
using BookOrders.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using reCAPTCHA.AspNetCore;

namespace BookOrders.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<BookOrdersUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IRecaptchaService _recaptcha;

        public AccountController(UserManager<BookOrdersUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRecaptchaService recaptcha)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _recaptcha = recaptcha;
        }

        public ActionResult Index()
        {
            return Redirect("/Admin/Account/List");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> DisableUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username ?? "");
            if (user == null)
            {
                return Json(new { status = "error", msg = $"Не може да бъде намерен потребител '{(username??"")}'." });
            }

            if (user.Disabled)
            {
                return Json(new { status = "warning", msg = $"Потребител '{(username ?? "")}' вече е деактивиран." });

            }

            user.Disabled = true;
            await _userManager.UpdateAsync(user);

            return Json(new { status = "success", msg = $"Успешно деактивиран потребител '{(username ?? "")}'." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> EnableUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username ?? "");
            if (user == null)
            {
                return Json(new { status = "error", msg = $"Не може да бъде намерен потребител <b>'{(username ?? "")}'</b>." });
            }

            if (!user.Disabled)
            {
                return Json(new { status = "warning", msg = $"Потребител <b>'{(username ?? "")}'</b> вече е активен." });

            }

            user.Disabled = false;
            await _userManager.UpdateAsync(user);

            return Json(new { status = "success", msg = $"Успешно активиран потребител <b>'{(username ?? "")}'</b>." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> DeleterOLE(string roleName)
        {          
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return Json(new { status = "error", msg = $"Роля <b>'{(roleName ?? "")}'</b> не може да бъде намерена." });
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            if (usersInRole.Any())
            {
                return Json(new { status = "warning", msg = $"Потребители <b>'{string.Join(",", usersInRole.Select(u => u.UserName).ToArray())}'</b> са асоциирани към роля <b>'{role.Name}'</b>. " +
                    $"Необходимо е първо да асоциирате потребителите към други роли." });
            }
            await _roleManager.DeleteAsync(role);

            return Json(new { status = "success", msg = $"Успешно изтрита роля <b>'{(roleName ?? "")}'</b>." });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                }

                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                }
            }
            base.Dispose(disposing);    
        }
    }
}