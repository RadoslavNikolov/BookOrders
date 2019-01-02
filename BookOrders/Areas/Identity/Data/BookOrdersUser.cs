using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookOrders.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BookOrders.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BookOrdersUser class
    public class BookOrdersUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<Category> ModifiedCategories { get; set; }
    }
}
