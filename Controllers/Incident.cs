//using System.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
/*using AspnetCoreMvcStarter.Models;

namespace AspnetCoreMvcStarter.Controllers;

public class Incident : Controller
{
  public IActionResult Index() => View();
}*/
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcStarter.Models;

namespace AspnetCoreMvcStarter.Controllers;

public class PageIncident : Controller
{
  public IActionResult Add() => View();
  public IActionResult Index() => View();
  public IActionResult Edit() => View();
  public IActionResult Details(int id)
  {
    // TODO: Replace with real data fetching logic
    var incident = new Incident
    {
      Id = id,
      Type = "Sample Type",
      Subject = "Sample Subject",
      Location = "Sample Location",
      Title = "Sample Title",
      Customer = "Sample Customer",
      Supplier = "Sample Supplier",
      Description = "On June 1st, 2024, a major network outage occurred in Building 1, disrupting connectivity for all employees on the first and second floors. The incident began at 8:15 AM and was reported by multiple users experiencing loss of access to internal systems and the internet. IT staff responded immediately, identifying a failed core switch as the root cause. Temporary measures were implemented to restore partial service while replacement hardware was sourced.",
      DateClosed = DateTime.Parse("2024-06-01"),
      DateReported = DateTime.Parse("2024-06-01"),
      DateFollowUp = DateTime.Parse("2024-06-02"),
      OperatedBy = "Sample Operated By",
      CustomerID = 1,
      SupplierID = 1,
      DocID = 1,
      Secret = false,
      SecretLabel = "Secretess",
      PreventingMeasures = "Sample Preventing Measures",
      DecidedByUID = 2,
      FollowUp = "After the initial response, the IT team conducted a thorough investigation to determine the root cause of the outage. It was discovered that a power surge had damaged the core network switch, leading to widespread connectivity issues. The faulty hardware was promptly replaced, and additional surge protection was installed. A review meeting was held to discuss preventive measures and improve incident response protocols for future events.",
      FollowUID = 3,
      Notes = "Sample Notes",
      Status = "Pending"
    };

    if (incident == null)
    {
      return NotFound();
    }

    return View(incident);
  }
  public IActionResult Print() => View();
}
