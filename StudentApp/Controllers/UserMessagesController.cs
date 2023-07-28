using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;

namespace StudentApp.Controllers
{
	[Authorize(Roles ="Admin")]
	public class UserMessagesController : Controller
	{
		private readonly StudentBlogContext _context;

		public UserMessagesController(StudentBlogContext context)
		{
			_context = context;
		}	

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Create()
		{
			return PartialView("_Create");
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("MsgId,Uname,Uaddress,Uphone,Umsg")] UserMessage msg)
		{
			if (msg != null)
			{
				_context.Add(msg);
				await _context.SaveChangesAsync();
				/*return RedirectToAction("Index", "Students");*/
				return Content("success");
			}
			else
			{
				return Content("failled");
			}
			
			
		}
	}
	
}
