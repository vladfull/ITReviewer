using DataAccess.Repository.IRepository;
using ITReviewer.Libraries.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ITReviewerWeb.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork= unitOfWork;
        }
        public IActionResult Index()
        {
			var objCompanyList = _unitOfWork.Company.GetAll().Result.ToList();
			return View(objCompanyList);
        }

        public IActionResult Create() 
        {
            return View();
        }
		[HttpPost]
		public IActionResult Create(Company obj)
		{
			//if (obj.Name == obj.DisplayOrder.ToString())
			//{
			//	ModelState.AddModelError("name", "The Name cannot exactly match the Display Order");
			//}
			if (ModelState.IsValid)
			{
				_unitOfWork.Company.Add(obj);
				_unitOfWork.Save();
				TempData["success"] = "Company created successfully";
				return RedirectToAction("Index");
			}
			return View();
		}
		
		public IActionResult Edit(int? id) 
		{
			if (id == null | id == 0)
			{
				return NotFound();
			}
			Company? companyFromDB = _unitOfWork.Company.Get(u => u.Id == id).Result;
			if (companyFromDB == null)
			{
				return NotFound();
			}

			return View(companyFromDB);
		}
		[HttpPost]
		public IActionResult Edit(Company obj) 
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Company.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Company updated successfully";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null | id == 0)
			{
				return NotFound();
			}
			Company? companyFromDB = _unitOfWork.Company.Get(u => u.Id == id).Result;
			if (companyFromDB == null)
			{
				return NotFound();
			}

			return View(companyFromDB);
		}
		[HttpPost]
		[ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Company? obj = _unitOfWork.Company.Get(u => u.Id == id).Result;
			if (obj == null)
			{
				return NotFound();
			}
			_unitOfWork.Company.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Company deleted successfully";
			return RedirectToAction("Index");

		}
	}
}
