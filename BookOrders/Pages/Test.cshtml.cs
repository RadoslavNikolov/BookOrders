using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using reCAPTCHA.AspNetCore;

namespace BookOrders.Pages
{
    public class TestModel : PageModel
    {
        private IRecaptchaService _recaptcha;

        public TestModel(IRecaptchaService recaptcha)
        {
            _recaptcha = recaptcha;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var recaptcha = await _recaptcha.Validate(Request);
            if (!recaptcha.success)
            {
                ModelState.AddModelError("Recaptcha", "There was an error validating recatpcha. Please try again!");
                ViewData["Result"] = "Error";
                return Page();
            }

            ViewData["Result"] = "Success";
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}