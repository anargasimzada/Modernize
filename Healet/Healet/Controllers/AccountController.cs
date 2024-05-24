using Healet.Models;
using Healet.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healet.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM vM)
		{
			if (!ModelState.IsValid) return View(vM);
			AppUser app1 = new AppUser
			{
				Name = vM.Name,
				Email = vM.Email,
				UserName = vM.UserName,
				Surname = vM.Surname,
			};
			IdentityResult result = await _userManager.CreateAsync(app1, vM.Password);
			if (!result.Succeeded)
			{
				foreach (var eror in result.Errors)
				{
					ModelState.AddModelError("", eror.Description);
				}
				return View(vM);
			}
			return RedirectToAction("login", "account");
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM vm)
		{
			if (!ModelState.IsValid) return View(vm);
			AppUser result = await _userManager.FindByNameAsync(vm.UserNameOrEmail);
			if (result == null)
			{
				result = await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
				if (result == null)
				{
					ModelState.AddModelError("", "user ve ya email yanlisdir");
					return View(vm);
				}
			}

			var ab = await _signInManager.PasswordSignInAsync(result, vm.Password, true, true);
			if (ab.Succeeded)
			{
				return RedirectToAction("Index", "Home");

			}
			else
			{
				return BadRequest();
			}
		}


		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

	}
}
