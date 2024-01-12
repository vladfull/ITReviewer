using ITReviewer.Libraries.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		public IAccountRepository Account { get; }
		public ICompanyRepository Company { get; }
        public IReviewRepository Review { get; }
        public IFileService File { get; }
		public void Save();
	}
}
