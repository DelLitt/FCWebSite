namespace FCWeb.Core
{
    using FCCore.Common;
    using FCCore.Configuration;
    using FCWeb.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;

    public static class StorageHelper
    {
        public static FolderViewModel GetFolderView(string path, string root, bool recursive = false)
        {
            string physicalPath = CheckPath(path, root);
            string parent = string.Empty;

            DirectoryInfo parentDir = Directory.GetParent(physicalPath);

            if(parentDir.Exists)
            {
                string parentVirtual = WebHelper.ToVirtualPath(parentDir.FullName);
                if(CheckPathIsAllowed(parentVirtual, root))
                {
                    parent = parentVirtual;
                }
            }

            var folderView = new FolderViewModel()
            {
                path = path,
                name = Path.GetFileNameWithoutExtension(path) ?? string.Empty,
                parent = parent
            };

            IEnumerable<string> folders = Directory.EnumerateDirectories(physicalPath);
            if(folders != null)
            {
                folderView.folders = folders.Select(f => new FolderViewModel()
                {
                    name = Path.GetFileNameWithoutExtension(f) ?? string.Empty,
                    path = WebHelper.ToVirtualPath(f)
                });
            }

            IEnumerable<string> files = Directory.EnumerateFiles(physicalPath);
            if (files != null)
            {
                folderView.files = files.Select(f => new FileViewModel()
                {
                    name = Path.GetFileName(f) ?? string.Empty,
                    path = WebHelper.ToVirtualPath(f)
                });
            }

            //if (recursive) { };

            return folderView;
        }

        private static string CheckPath(string path, string root)
        {
            Guard.CheckNull(path, "path");
            Guard.CheckNull(root, "root");

            if (!CheckPathIsAllowed(path, root))
            {
                throw new SecurityException(string.Format("Path '{0}' is not allowed!", path));
            }

            string physicalPath = WebHelper.ToPhysicalPath(path);

            if(!Directory.Exists(physicalPath))
            {
                throw new DirectoryNotFoundException(string.Format("Directory {0} is not found!", path));
            }

            return physicalPath;
        }

        private static bool CheckPathIsAllowed(string path, string root)
        {
            IEnumerable<string> availableRoots = MainCfg.UploadRoots;

            if(!availableRoots.Any())
            {
                throw new KeyNotFoundException("UploadRoots doesn't set!");
            }

            if(!availableRoots.Contains(root.ToLower()))
            {
                return false;
            }

            return path.StartsWith(root, StringComparison.OrdinalIgnoreCase);
        }
    }
}
