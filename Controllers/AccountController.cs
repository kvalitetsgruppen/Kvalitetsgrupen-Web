using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AspnetCoreMvcStarter.Helpers;

public class AccountController : Controller
{
  // GET: /Account/Login
  public IActionResult Login()
  {
    return View();
  }

  // POST: /Account/Login
  [HttpPost]
  public IActionResult Login(string username, string password)
  {
    //Console.WriteLine($"Username: {username}, Password: {password}");

    if (DatabaseHelper.ValidateUser(username, password))
    {
      //Console.WriteLine("Right credentials");
      HttpContext.Session.SetString("User", username);
      return RedirectToAction("Welcome"); // go to welcome page
    }
    else
    {
      //Console.WriteLine("Wrong credentials");
      ViewData["ErrorMessage"] = "Invalid username or password!";
      return RedirectToAction("Index", "Page2"); // This stays on the login page and shows the error
    }

  }

  // GET: /Account/Welcome
  public IActionResult Welcome()
  {
    var user = HttpContext.Session.GetString("User");
    if (user == null)
    {
      return RedirectToAction("Login");
    }

    ViewData["Username"] = user;
    return View();
  }

  // GET: /Account/Logout
  public IActionResult Logout()
  {
    HttpContext.Session.Clear();
    return RedirectToAction("Index", "Page2");
  }
}
