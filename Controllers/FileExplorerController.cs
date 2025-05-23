using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Syncfusion.EJ2.FileManager.Base;
using Syncfusion.EJ2.FileManager.PhysicalFileProvider;

namespace AspnetCoreMvcStarter.Controllers
{
  public class FileExplorerController : Controller
  {
    private readonly PhysicalFileProvider _operation;
    private readonly string _basePath;

    public FileExplorerController(IWebHostEnvironment hostingEnvironment)
    {
      _operation = new PhysicalFileProvider();
      _basePath = Path.Combine(hostingEnvironment.WebRootPath, "Content", "Files");
      _operation.RootFolder(_basePath);
    }

    [HttpPost]
    public IActionResult FileOperations([FromBody] FileManagerDirectoryContent args)
    {
      switch (args.Action)
      {
        case "read":
          return Json(_operation.ToCamelCase(_operation.GetFiles(args.Path, args.ShowHiddenItems)));
        case "delete":
          return Json(_operation.ToCamelCase(_operation.Delete(args.Path, args.Names)));
        case "details":
          args.Names ??= Array.Empty<string>();
          return Json(_operation.ToCamelCase(_operation.Details(args.Path, args.Names, args.Data)));
        case "create":
          return Json(_operation.ToCamelCase(_operation.Create(args.Path, args.Name)));
        case "search":
          return Json(_operation.ToCamelCase(_operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive)));
        case "copy":
          return Json(_operation.ToCamelCase(_operation.Copy(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData)));
        case "move":
          return Json(_operation.ToCamelCase(_operation.Move(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData)));
        case "rename":
          return Json(_operation.ToCamelCase(_operation.Rename(args.Path, args.Name, args.NewName)));
      }
      return null;
    }

    [HttpPost]
    public IActionResult Upload(string path, List<IFormFile> uploadFiles, string action)
    {
      try
      {
        foreach (var file in uploadFiles)
        {
          var folders = file.FileName.Split('/');
          if (folders.Length > 1)
          {
            for (int i = 0; i < folders.Length - 1; i++)
            {
              string newDirectoryPath = Path.Combine(_basePath + path, folders[i]);

              if (Path.GetFullPath(newDirectoryPath) != (Path.GetDirectoryName(newDirectoryPath) + Path.DirectorySeparatorChar + folders[i]))
              {
                throw new UnauthorizedAccessException("Access denied for directory traversal");
              }

              if (!Directory.Exists(newDirectoryPath))
              {
                _operation.ToCamelCase(_operation.Create(path, folders[i]));
              }

              path += folders[i] + "/";
            }
          }
        }

        var uploadResponse = _operation.Upload(path, uploadFiles, action, 0);
        return Content(""); // Syncfusion expects empty content on success
      }
      catch (Exception)
      {
        var error = new ErrorDetails
        {
          Message = "Access denied for directory traversal",
          Code = "417"
        };

        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";
        Response.StatusCode = StatusCodes.Status417ExpectationFailed;

        return Content(JsonConvert.SerializeObject(error));
      }
    }

    [HttpPost]
    public IActionResult Download([FromBody] string downloadInput)
    {
      var args = JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
      return _operation.Download(args.Path, args.Names);
    }

    [HttpPost]
    public IActionResult GetImage([FromBody] FileManagerDirectoryContent args)
    {
      string id = args.Id?.ToString() ?? string.Empty;
      return _operation.GetImage(args.Path, id, false, null, null);
    }

    public IActionResult Index()
    {
      return View();
    }

    public class ErrorDetails
    {
      public string Message { get; set; }
      public string Code { get; set; }
    }
  }
}
