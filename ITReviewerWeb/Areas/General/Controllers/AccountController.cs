using ITReviewer.Libraries.DataAccess.Data;
using ITReviewerWeb.Managers;
using ITReviewer.Libraries.Models;
using ITReviewer.Libraries.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using DataAccess.Repository.IRepository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace ITReviewerWeb.Areas.General.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public AccountController(IUnitOfWork unitOfWork)
		{
			_unitOfWork= unitOfWork;
		}

		public IActionResult Register()
		{
			ClaimsPrincipal claimUser = HttpContext.User;
			if (claimUser.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
			return View();
		}
		[HttpPost]
		public IActionResult Register(RegisterVM obj)
		{
			var userFromDB = _unitOfWork.Account.Get(u => u.Name == obj.Name);
			if (userFromDB != null)
			{
				ModelState.AddModelError("Name", "Даний логін уже використовується");
			}
			if (ModelState.IsValid)
			{
				User user = new User
				{
					Name = obj.Name,
					Email = obj.Email,
					Password = PasswordManager.HashPassword(obj.Password),
					Bio = "",
					Role = Role.User
				};
				_unitOfWork.Account.Add(user);
				_unitOfWork.Save();
				return View("RegisterPassed");
			}
			return View();
		}

		public IActionResult Login()
		{
			ClaimsPrincipal claimUser = HttpContext.User;
			if(claimUser.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM obj)
		{
			if(ModelState.IsValid)
			{
				var userFromDB = _unitOfWork.Account.Get(u => u.Name == obj.Name);
				if (userFromDB == null)
				{
					ModelState.AddModelError("Name", "Даного логіна не знайдено");
					return View();
				}
				if(PasswordManager.HashPassword(obj.Password) == userFromDB.Password) 
				{
					ClaimsIdentity claimsIdentity = _unitOfWork.Account.CreateIdentity(userFromDB);//new ClaimsIdentity(claims,
					AuthenticationProperties properties = new AuthenticationProperties()
					{
						IsPersistent= obj.KeepLogged,
						AllowRefresh= true
					};

					await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity), properties);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("Password", "Невірний пароль");
				}
			}
			return View();
		}

		//public IActionResult LogOut()
		//{
		//	return View();
		//}
		[HttpGet(Name = "Logout")]
		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
			//return View();
		}
    }
}
