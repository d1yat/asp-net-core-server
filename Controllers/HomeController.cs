using Microsoft.AspNetCore.Mvc;
using AspNetCoreDashboardBackend.Models;

namespace AspNetCoreDashboardBackend.Controllers;

public class HomeController : Controller {
    public IActionResult Index() {
        var response = JsonResultClass.CreateDataAsync();
        var companyList = response ?? new System.Data.DataTable();
        foreach (System.Data.DataRow company in companyList.Rows) {
            string? companyName = company["Name"]?.ToString();
            Console.WriteLine($"Company Name: {companyName}");
        }
        return View();
    }
}
