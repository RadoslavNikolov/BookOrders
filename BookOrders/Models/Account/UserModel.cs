using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrders.Models.Account
{
    public class UserModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool Disabled { get; set; }

        public string Role { get; set; }

    }
}
