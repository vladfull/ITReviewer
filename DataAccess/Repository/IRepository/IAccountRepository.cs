using Bulky.DataAccess.Repository.IRepository;
using ITReviewer.Libraries.Models;
using ITReviewer.Libraries.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface IAccountRepository : IRepository<User>
	{
		public void Update(User user);
		public ClaimsIdentity CreateIdentity(User user);
	}
}
