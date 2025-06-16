using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcStarter.Models;


public class People : Controller
{
  public IActionResult Index() => View();

  public IActionResult ViewAccount
  () => View();

  public IActionResult ViewCalendar
() => View();

}
