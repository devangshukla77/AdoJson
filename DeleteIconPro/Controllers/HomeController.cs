using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DeleteIconPro.Models;
using DeleteIconPro.Data;

namespace DeleteIconPro.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataService _dataService;

    public HomeController(ILogger<HomeController> logger, DataService dataservice)
    {
        _logger = logger;
        _dataService = dataservice;
    }

    public IActionResult Index()
    {
        var persons = _dataService.GetAllPersons();
        return View(persons);
    }
    [HttpPost]
    public JsonResult DeletePerson(int id)
    {
        try
        {
            _dataService.DeletePerson(id);
            return Json(new { success = true });
        }
        catch
        {
            return Json(new { success = false });
        }
    }

    public JsonResult UpdatePerson(int id, string name)
    {
        try
        {
            _dataService.UpdatePerson(id,name);
            return Json(new { success = true });
        }
        catch
        {
            return Json(new { success = false });
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
