using ITReviewer.Libraries.Models.ViewModels;
using ITReviewer.Libraries.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace ITReviewerWeb.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            User userFromDB = await _unitOfWork.Account.Get(u => u.Name == User.Identity.Name);
            var reviewsFromDB = (await _unitOfWork.Review.GetRange(u => u.UserId == userFromDB.Id)).OrderByDescending(u => u.RegDate).ToList();
            int pageSize = 5;
            var pagReviews = PaginatedList<Review>.Create(reviewsFromDB, pageNumber ?? 1, pageSize);
			Profile_IndexVM obj = new Profile_IndexVM
            {
                Name = User.Identity.Name,
                Role = (User.FindFirst(ClaimTypes.Role)).Value,
                Bio = userFromDB.Bio,
                RegDate = userFromDB.RegDate.ToString().Substring(0, 10),
                ImagePath = userFromDB.ImagePath,
                Reviews = pagReviews /*reviewsFromDB*/
            };
            return View(obj);
        }
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userFromDB = await _unitOfWork.Account.Get(u => u.Id.ToString() == User.FindFirst("Id").Value);
                Profile_EditVM obj = new Profile_EditVM
                {
                    Bio = userFromDB.Bio,
                    ImagePath = userFromDB.ImagePath
                };
                return View(obj);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Profile_EditVM obj)
        {
            if(ModelState.IsValid) 
            {
                User userFromDB = await _unitOfWork.Account.Get(u => u.Name == User.Identity.Name);
                if(obj.ImageFile != null)
                {
					if (userFromDB.ImagePath != null)
					{
						_unitOfWork.File.DeleteImage(userFromDB.ImagePath);
					}
					var fileResult = _unitOfWork.File.SaveImage(obj.ImageFile);
					if (fileResult.Item1 == 0)
					{
						ModelState.AddModelError("extension", fileResult.Item2);
                        return View();
					}
					userFromDB.ImagePath = fileResult.Item2;
				}
                else
                {
                    _unitOfWork.File.DeleteImage(userFromDB.ImagePath);
                    userFromDB.ImagePath = null;
                }
                userFromDB.Bio = obj.Bio;
                _unitOfWork.Account.Update(userFromDB);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }
    }
}
