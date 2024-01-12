using DataAccess.Repository.IRepository;
using ITReviewer.Libraries.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ITReviewerWeb.Areas.General.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork= unitOfWork;
		}

		public IActionResult Index(string? searchName)
		{
			if(searchName == null) 
			{
                var objCompanyList = _unitOfWork.Company.GetAll().Result.ToList();
                return View(objCompanyList);
            }
			else
			{
				var objSearched = _unitOfWork.Company.GetRange(u => u.Name.Contains(searchName)).Result.ToList();
				return View(objSearched);
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}