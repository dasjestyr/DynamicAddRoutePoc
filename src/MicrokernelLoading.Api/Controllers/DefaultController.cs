using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicrokernelLoading.Api.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult NotFound()
        {
            return NotFound();
        }
    }
}
