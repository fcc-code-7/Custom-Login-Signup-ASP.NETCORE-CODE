using Altivix.Web.Constants;
using Altivix.Web.Models;
using Altivix.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Altivix.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> _userManager)
        {
         
            this._userManager = _userManager;
        }
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    UserName = model.Email,  // Ensure UserName is set
                    Name = model.Name,
                    Email = model.Email,
                    Company = model.Company,
                    IsDisabled = model.IsDisabled,
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Employee.ToString());
                    ModelState.Clear();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        // Log the error for debugging
                        Console.WriteLine($"Error: {error.Description}");
                    }
                }
            }

            // Return the model back to the view in case of errors
            return View(model);
        }
    }
}
