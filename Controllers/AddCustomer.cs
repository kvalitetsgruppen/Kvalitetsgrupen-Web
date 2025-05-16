using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcStarter.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspnetCoreMvcStarter.Controllers
{
  public class AddCustomerController : Controller
  {
    [HttpGet]
    public IActionResult Index()
    {
      var model = new DocumentEditModel
      {
        ClassificationList = GetClassificationList(),
        SatisfactionLevelList = GetSatisfactionLevelList(),
        EvaluationDate = DateTime.Now // default if needed
      };

      return View(model);
    }

    [HttpPost]
    public IActionResult AddCustomer(DocumentEditModel model)
    {
      if (!ModelState.IsValid)
      {
        model.ClassificationList = GetClassificationList();
        model.SatisfactionLevelList = GetSatisfactionLevelList();
        return View("Index", model);
      }

      // ✅ EvaluationDate is now bound and can be used:
      var evalDate = model.EvaluationDate;

      TempData["Message"] = $"Customer added with evaluation date: {evalDate?.ToString("g") ?? "None"}";
      return RedirectToAction("Index");
    }

    private IEnumerable<SelectListItem> GetClassificationList()
    {
      return new List<SelectListItem>
            {
                new SelectListItem { Text = "A Customer", Value = "A Customer" },
                new SelectListItem { Text = "B Customer", Value = "B Customer" },
                new SelectListItem { Text = "C Customer", Value = "C Customer" },
                new SelectListItem { Text = "D Customer", Value = "D Customer" },
                new SelectListItem { Text = "Not Active", Value = "Not Active" },
                new SelectListItem { Text = "Prospect", Value = "Prospect" }
            };
    }

    private IEnumerable<SelectListItem> GetSatisfactionLevelList()
    {
      return new List<SelectListItem>
            {
                new SelectListItem { Text = "Satisfied", Value = "Satisfied" },
                new SelectListItem { Text = "Not Satisfied", Value = "Not Satisfied" },
                new SelectListItem { Text = "No Record", Value = "No Record" }
            };
    }
  }
}
