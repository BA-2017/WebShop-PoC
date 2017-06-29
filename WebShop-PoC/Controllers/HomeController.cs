using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebShop_PoC.Controllers
{

    public class InjectServerName : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ((Controller) context.Controller).ViewData["ServerName"] = Environment.MachineName;
            ((Controller) context.Controller).ViewData["Version"] = "v0.35";
            ((Controller) context.Controller).ViewData["Time"] = DateTime.Now.ToString();
            base.OnActionExecuting(context);
        }
    }

    [InjectServerName]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger){
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "MachineName: " + Environment.MachineName;
            _logger.LogInformation("TEST log");
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Ready(){
            return Ok();
        }

        public IActionResult Live(){
            return Ok();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
