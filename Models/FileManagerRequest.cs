// Models/FileManagerRequest.cs
namespace AspnetCoreMvcStarter.Models
{
    public class FileManagerRequest
    {
        public string Path { get; set; }
        public string[] Names { get; set; }
        public string Name { get; set; }
        public string NewName { get; set; }
        public string TargetPath { get; set; }
        public string SearchString { get; set; }
        public bool ShowHiddenItems { get; set; }
        public bool CaseSensitive { get; set; }
        public string Action { get; set; }
        public bool? RenameFiles { get; set; }
    }
}
