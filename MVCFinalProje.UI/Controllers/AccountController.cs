using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.DTOs.CustomerDTOs;
using MVCFinalProje.Business.Services.CustomerServices;
using MVCFinalProje.UI.Models.AccountVMs;
using MVCFİnalProje.Domain.Utilities.Concretes;

namespace MVCFinalProje.UI.Controllers
{
    public class AccountController : BaseController
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICustomerService _customerService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
        }


        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ErrorNotfy("Kullanıcı adı veya şifre hatalı");
                return View(model);
            }
            var checkPassword = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!checkPassword.Succeeded)
            {
                ErrorNotfy("Kullanıcı adı veya şifre hatalı");
                return View(model);
            }

            var userRole = await _userManager.GetRolesAsync(user);

            if (userRole == null)
            {
                ErrorNotfy("Kullanıcı adı veya şifre hatalı");
                return View(model);
            }

            return RedirectToAction("Index", "Home", new {Area = userRole[0].ToString() });
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _customerService.AddAsync(model.Adapt<CustomerCreateDTO>());
            if (!result.IsSucces)
            {
                ErrorNotfy(result.Message);
                return View(model);
            }
            SuccessNotfy(result.Message);
            return RedirectToAction("Login", "Account");
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError("", error.Description);
            //}


            //if (ModelState.IsValid)
            //{
            //    if (model.Password != model.ConfirmPassword)
            //    {
            //        ModelState.AddModelError("", "Passwords do not match.");
            //        return View(model);
            //    }

            //    var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            //    var result = await _userManager.CreateAsync(user, model.Password);

            //    if (result.Succeeded)
            //    {
            //        SuccessNotfy("Kayıt Gerçekleştirildi");
            //        return RedirectToAction("Login", "Account");
            //    }

            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError("", error.Description);
            //    }
            //}

            //return View(model);
        }



    }

}
