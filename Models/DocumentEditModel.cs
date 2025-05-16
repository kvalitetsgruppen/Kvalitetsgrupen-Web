using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcStarter.Models
{
  public class DocumentEditModel
  {
    [Required]
    public string CustomerName { get; set; }

    [Required]
    public string CustomerID { get; set; }

    public string ContactPerson { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Website { get; set; }

    public string Classification { get; set; }

    public string SatisfactionLevel { get; set; }


    [Display(Name = "Evaluation Date")]
    [DataType(DataType.DateTime)]
    public DateTime? EvaluationDate { get; set; }

    // ✅ Add these dropdown list properties
    public IEnumerable<SelectListItem> ClassificationList { get; set; }
    public IEnumerable<SelectListItem> SatisfactionLevelList { get; set; }

  }
}
