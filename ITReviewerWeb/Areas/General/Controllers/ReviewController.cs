using DataAccess.Repository.IRepository;
using ITReviewer.Libraries.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;
using System.Security.Claims;

namespace ITReviewerWeb.Areas.General.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }
        public async Task<IActionResult> Index(int? id, int? pageNumber)
        {
            int pageSize = 5;
            if (id == null | id == 0)
            {
                return NotFound();
            }
            var compFromDB = await _unitOfWork.Company.Get(u => u.Id == id);

            if (compFromDB == null) 
            {
                return NotFound();
            }
            var reviewsFromDB = await _unitOfWork.Review.GetFullReview(id);
            var pagReviews = PaginatedList<Review>.Create(reviewsFromDB, pageNumber ?? 1, pageSize);
            CompanyReviewsVM obj = new CompanyReviewsVM
            {
                Company = compFromDB,
                Reviews = pagReviews
            };
            TempData["Rule"] = true;        //TODO
            return View(obj);
        }
        public async Task<IActionResult> GetMoreReviews(int? id, int? pageNumber) 
        {
            int pageSize = 5;
			var reviewsFromDB = await _unitOfWork.Review.GetFullReview(id);
			var pagReviews = PaginatedList<Review>.Create(reviewsFromDB, pageNumber ?? 1, pageSize);
            return PartialView("MoreReviewsPartial", pagReviews);
		}
        [HttpPost]
        public async Task<IActionResult> Create(Review obj) 
        {
            if(ModelState.IsValid) 
            {
				obj.UserId = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst("Id")?.Value);
                await _unitOfWork.Review.Add(obj);
                _unitOfWork.Save();
                var companyFromDB = await _unitOfWork.Company.Get(u => u.Id == obj.CompanyId);
                companyFromDB.Rating = _unitOfWork.Company.CalculateRate(companyFromDB.Id);
                _unitOfWork.Company.Update(companyFromDB);
                _unitOfWork.Save();
			}
			return RedirectToAction("Index", new {id =  obj.CompanyId });
        }
        //[HttpGet]
        //public IActionResult Delete(int? id) 
        //{
        //    if(id == null | id == 0)
        //    {
        //        return NotFound();
        //    }
        //    return View(id);
        //}
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            Review? obj = await _unitOfWork.Review.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Review.Remove(obj);
            _unitOfWork.Save();
            var companyFromDB = await _unitOfWork.Company.Get(u => u.Id == obj.CompanyId);        
            companyFromDB.Rating = _unitOfWork.Company.CalculateRate(companyFromDB.Id);
            _unitOfWork.Company.Update(companyFromDB);
            _unitOfWork.Save();
            return RedirectToAction("Index", new {id = obj.CompanyId});
        }
    }
}
