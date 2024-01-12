using DataAccess.Repository.IRepository;
using ITReviewer.Libraries.DataAccess.Data;
using ITReviewer.Libraries.DataAccess.Repository;
using ITReviewer.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public class CompanyRepository : Repository<Company>, ICompanyRepository
	{
		private ApplicationDBContext _db;
		public CompanyRepository(ApplicationDBContext db) : base(db)
		{
			_db = db;
		}
		public void Update(Company user)
		{
			_db.Update(user);
		}
		public double CalculateRate(int id)
		{
            List<Review> reviews = _db.Reviews.Where(u => u.CompanyId == id).ToList();
			double sum = 0;
			foreach (var review in reviews)
			{
				sum += review.Rate;
			}
			double rating = Math.Round(sum / reviews.Count,1);
			return rating;
		}
	}
}
