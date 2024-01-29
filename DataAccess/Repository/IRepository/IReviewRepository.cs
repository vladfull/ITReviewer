using Bulky.DataAccess.Repository.IRepository;
using ITReviewer.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IReviewRepository : IRepository<Review>
    {
        public void Update(Review review);
        public Task<List<Review>> GetFullReview(int? id);
    }
}
