using Bulky.DataAccess.Repository.IRepository;
using ITReviewer.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface ICompanyRepository : IRepository<Company>
	{
		public void Update(Company company);
		public double CalculateRate(int id);
	}
}
