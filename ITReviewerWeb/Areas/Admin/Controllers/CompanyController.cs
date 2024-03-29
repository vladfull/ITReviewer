﻿using DataAccess.Repository.IRepository;
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
        public async Task<IActionResult> Index()
        {
			var objCompanyList = (await _unitOfWork.Company.GetAll()).ToList();
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
		
		public async Task<IActionResult> Edit(int? id) 
		{
			if (id == null | id == 0)
			{
				return NotFound();
			}
			Company? companyFromDB = await _unitOfWork.Company.Get(u => u.Id == id);
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

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null | id == 0)
			{
				return NotFound();
			}
			Company? companyFromDB = await _unitOfWork.Company.Get(u => u.Id == id);
			if (companyFromDB == null)
			{
				return NotFound();
			}

			return View(companyFromDB);
		}
		[HttpPost]
		[ActionName("Delete")]
		public async Task<IActionResult> DeletePOST(int? id)
		{
			Company? obj = await _unitOfWork.Company.Get(u => u.Id == id);
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
