using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;

namespace StudentApp.Controllers
{
	public class StudentsController : Controller
	{
		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("dashbord", "Home");
			}
			else
			{
				return View();
			}
			
		}

		public IActionResult StudentList()
		{
			List<Student> stdList = new List<Student>
			{
				new Student{id=1,Name="Ram",Address="Itahari"},
				new Student{id=2,Name="sita",Address="Itahari"},
				new Student{id=3,Name="rita",Address="Itahari"},
				new Student{id=4,Name="gita",Address="Itahari"}
			};
			return PartialView("_StudentList",stdList);
		}
		public IActionResult StudentListView()
		{
			return View();
		}

		public IActionResult Feature()
		{
			return View();
		}
	}
}
