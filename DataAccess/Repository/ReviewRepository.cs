using DataAccess.Repository.IRepository;
using ITReviewer.Libraries.DataAccess.Data;
using ITReviewer.Libraries.DataAccess.Repository;
using ITReviewer.Libraries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private ApplicationDBContext _db;
        public ReviewRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

		public async Task<List<Review>> GetFullReview(int? id)
		{
            var obj = _db.Reviews.Where(r => r.CompanyId == id).Include(r => r.User).OrderByDescending(u => u.RegDate).ToList();
            return obj;
		}

		public void Update(Review review)
        {
            _db.Update(review);
        }
        
    }
}
