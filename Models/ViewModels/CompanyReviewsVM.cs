using ITReviewer.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CompanyReviewsVM
    {
        public Company Company { get; set; }
        public PaginatedList<Review> Reviews { get; set; }
    }
}
