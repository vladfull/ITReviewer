using DataAccess.Repository.IRepository;
using ITReviewer.Libraries.DataAccess.Data;
using ITReviewer.Libraries.DataAccess.Repository;
using ITReviewer.Libraries.Models;
using ITReviewer.Libraries.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public class AccountRepository : Repository<User>, IAccountRepository
	{
		private ApplicationDBContext _db;
		public AccountRepository(ApplicationDBContext db) : base(db)
		{
			_db = db;
		}
		public void Update(User user)
		{
			_db.Update(user);
		}
		public ClaimsIdentity CreateIdentity(User user)
		{
			List<Claim> claims = new List<Claim>()
					{
						new Claim("Id", user.Id.ToString(), ClaimValueTypes.Integer),
						new Claim(ClaimTypes.Name, user.Name),
						new Claim(ClaimTypes.Email, user.Email),
						new Claim(ClaimTypes.Role, user.Role.ToString())
					};
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
				CookieAuthenticationDefaults.AuthenticationScheme);
			return claimsIdentity;
		}
	}
}
