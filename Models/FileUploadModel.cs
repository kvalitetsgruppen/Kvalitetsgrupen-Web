// Models/FileUploadModel.cs
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AspnetCoreMvcStarter.Models
{
  public class FileUploadModel
  {
    public string Path { get; set; }
    public string Action { get; set; }
    public IList<IFormFile> UploadFiles { get; set; }
  }
}
