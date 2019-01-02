using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrders.Areas.Identity.Data
{
    public class BookOrderDbInitializer
    {
        public static void SeedUsers(UserManager<BookOrdersUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                //Не е намерен
                var user = new BookOrdersUser
                {
                    UserName = "admin",
                    FirstName = "admin",
                    LastName = "adminov",
                    Email = "admin@bookorders.com"
                };

                var result = userManager.CreateAsync(user, "123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("poweruser").Result == null)
            {
                //Не е намерен
                var user = new BookOrdersUser
                {
                    UserName = "poweruser",
                    FirstName = "poweruser",
                    LastName = "poweruser",
                    Email = "poweruser@bookorders.com"
                };

                var result = userManager.CreateAsync(user, "123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "PowerUser").Wait();
                }
            }

            if (userManager.FindByNameAsync("user").Result == null)
            {
                //Не е намерен
                var user = new BookOrdersUser
                {
                    UserName = "user",
                    FirstName = "user",
                    LastName = "user",
                    Email = "user@bookorders.com"
                };

                var result = userManager.CreateAsync(user, "123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByNameAsync("guest").Result == null)
            {
                //Не е намерен
                var user = new BookOrdersUser
                {
                    UserName = "guest",
                    FirstName = "guest",
                    LastName = "guest",
                    Email = "guest@bookorders.com"
                };

                var result = userManager.CreateAsync(user, "123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Guest").Wait();
                }
            }
        }
    }
}
