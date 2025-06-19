using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
