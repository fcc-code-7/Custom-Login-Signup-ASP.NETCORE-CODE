using Altivix.Web.Constants;
using Altivix.Web.Models;
using Altivix.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Altivix.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Client"))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("AddEmployee", "Admin");
                        }
                        else if (roles.Contains("Employee"))
                        {
                            return RedirectToAction("EmployeeDashboard", "Employee");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid user role.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Login Attempt");
                        return View(model);
                    }
                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
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
                    await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
    }
}
