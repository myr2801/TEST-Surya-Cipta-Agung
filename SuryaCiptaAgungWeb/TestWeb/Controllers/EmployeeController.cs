using Microsoft.AspNetCore.Mvc;

namespace TestWeb.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
