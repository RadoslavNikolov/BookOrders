using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookOrders.Areas.Admin.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}