namespace FCCore.Model.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class StorageFile
    {
        public string name { get; set; }
        public string path { get; set; }
        public bool isChecked { get; set; } = false;
    }
}
