using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcStarter.Models; // update with your actual namespace

public class DocumentController : Controller
{
  [HttpGet]
    public IActionResult Edit()
    {
        return View(); // Optionally, pass model
    }

    [HttpPost]
    public IActionResult Edit(DocumentEditModel model)
    {
        if (ModelState.IsValid)
        {
            // Save changes
            return RedirectToAction("Edit");
        }

        return View(model);
    }
}
