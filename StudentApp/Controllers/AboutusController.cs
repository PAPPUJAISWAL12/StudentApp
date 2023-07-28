using Microsoft.AspNetCore.Mvc;

namespace StudentApp.Controllers
{
	public class AboutusController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
