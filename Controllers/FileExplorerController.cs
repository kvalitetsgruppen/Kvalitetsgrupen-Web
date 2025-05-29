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
      return args.Action switch
      {
        "read" => Json(_operation.ToCamelCase(_operation.GetFiles(args.Path, args.ShowHiddenItems))),
        "delete" => Json(_operation.ToCamelCase(_operation.Delete(args.Path, args.Names))),
        "details" => Json(_operation.ToCamelCase(_operation.Details(args.Path, args.Names ?? Array.Empty<string>(), args.Data))),
        "create" => Json(_operation.ToCamelCase(_operation.Create(args.Path, args.Name))),
        "search" => Json(_operation.ToCamelCase(_operation.Search(args.Path, args.SearchString, args.ShowHiddenItems, args.CaseSensitive))),
        "copy" => Json(_operation.ToCamelCase(_operation.Copy(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData))),
        "move" => Json(_operation.ToCamelCase(_operation.Move(args.Path, args.TargetPath, args.Names, args.RenameFiles, args.TargetData))),
        "rename" => Json(_operation.ToCamelCase(_operation.Rename(args.Path, args.Name, args.NewName))),
        _ => null
      };
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
                throw new UnauthorizedAccessException("Access denied for directory traversal");

              if (!Directory.Exists(newDirectoryPath))
                _operation.ToCamelCase(_operation.Create(path, folders[i]));

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

    // Opens file in browser or downloads with correct content type
    [HttpGet]
    public IActionResult OpenFile(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        return BadRequest("Path is required.");

      string safePath = path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
      string fullPath = Path.Combine(_basePath, safePath);

      if (!System.IO.File.Exists(fullPath))
        return NotFound("File not found.");

      var contentType = GetContentType(fullPath);
      var fileName = Path.GetFileName(fullPath);
      byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);

      return File(fileBytes, contentType, fileName);
    }

    // Reads and returns plain text content (for text files preview)
    [HttpGet]
    public IActionResult ReadFile(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        return BadRequest("Path is required.");

      string safePath = path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
      string fullPath = Path.Combine(_basePath, safePath);

      if (!System.IO.File.Exists(fullPath))
        return NotFound("File not found.");

      string content = System.IO.File.ReadAllText(fullPath);
      return Content(content, "text/plain");
    }

    // Loads document file as base64 string for Syncfusion DocumentEditor
    [HttpGet]
    public IActionResult LoadDocument(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        return BadRequest("Path is required.");

      string safePath = path.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
      string fullPath = Path.Combine(_basePath, safePath);

      if (!System.IO.File.Exists(fullPath))
        return NotFound("File not found.");

      byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
      string base64String = Convert.ToBase64String(fileBytes);

      return Json(new { base64 = base64String });
    }

    public IActionResult Index()
    {
      return View();
    }

    private string GetContentType(string path)
    {
      var types = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/msword" },
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".csv", "text/csv" },
                { ".rtf", "application/rtf" },
                // Add more types as needed
            };

      var ext = Path.GetExtension(path).ToLowerInvariant();
      return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
    }

    public class ErrorDetails
    {
      public string Message { get; set; }
      public string Code { get; set; }
    }
  }
}
