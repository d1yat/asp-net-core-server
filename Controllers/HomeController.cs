using Microsoft.AspNetCore.Mvc;
using AspNetCoreDashboardBackend.Models;

namespace AspNetCoreDashboardBackend.Controllers;

public class HomeController : Controller {
    public IActionResult Index() {
        // This controller is made for debugging purposes only
        return View();
    }
}
