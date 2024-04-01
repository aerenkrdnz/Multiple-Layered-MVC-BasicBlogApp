using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    public class HomeController : Controller
    {		
		public IActionResult Index()
        {            
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Projects(int? categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }
    }
}
