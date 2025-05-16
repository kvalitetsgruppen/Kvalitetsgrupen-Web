using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcStarter.Models;

namespace AspnetCoreMvcStarter.Controllers;

public class Page3 : Controller
{
  public IActionResult Index() => View();

  public IActionResult Treeview() => View();
  public ActionResult ToggleButton()
{
    return View();
}

}
