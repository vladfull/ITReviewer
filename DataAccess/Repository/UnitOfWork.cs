using DataAccess.Repository.IRepository;
using ITReviewer.Libraries.DataAccess.Data;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDBContext _db;
		private IWebHostEnvironment environment;
		public IAccountRepository Account { get; }
		public ICompanyRepository Company { get; }
		public IReviewRepository Review { get; }
		public IFileService File { get; }
		public UnitOfWork(ApplicationDBContext db, IWebHostEnvironment env)
		{
			_db= db;
			environment= env;
			Account = new AccountRepository(_db);
			Company = new CompanyRepository(_db);
			Review = new ReviewRepository(_db);
			File = new FileService(env);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
