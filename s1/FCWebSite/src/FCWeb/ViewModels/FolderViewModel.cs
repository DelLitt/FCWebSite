namespace FCWeb.ViewModels
{
    using System.Collections.Generic;

    public class FolderViewModel
    {
        public string name { get; set; }
        public string path { get; set; }
        public string parent { get; set; }

        public IEnumerable<FolderViewModel> folders { get; set; } = new List<FolderViewModel>();
        public IEnumerable<FileViewModel> files { get; set; } = new List<FileViewModel>();
    }
}
