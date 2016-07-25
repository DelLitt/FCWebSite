namespace FCCore.Model.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class StorageFolder
    {
        public string name { get; set; }
        public string path { get; set; }
        public string parent { get; set; }

        public IEnumerable<StorageFolder> folders { get; set; } = new List<StorageFolder>();
        public IEnumerable<StorageFile> files { get; set; } = new List<StorageFile>();
    }
}
