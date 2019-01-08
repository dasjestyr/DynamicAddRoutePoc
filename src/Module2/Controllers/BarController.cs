using Microsoft.AspNetCore.Mvc;

namespace Module2.Controllers
{
    public class BarController : Controller
    {
        public IActionResult Get()
        {
            return Ok("I AM BAR");
        }
    }
}
