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
        public static FolderViewModel GetFolderView(string root, bool recursive = false)
        {
            string physicalPath = CheckRoot(root);
            string parent = string.Empty;

            DirectoryInfo parentDir = Directory.GetParent(physicalPath);

            if(parentDir.Exists)
            {
                string parentVirtual = WebHelper.ToVirtualPath(parentDir.FullName);
                if(CheckRootIsAllowed(parentVirtual))
                {
                    parent = parentVirtual;
                }
            }

            var folderView = new FolderViewModel()
            {
                path = root,
                name = Path.GetFileNameWithoutExtension(root) ?? string.Empty,
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

        // TODO: Fix checking. Check physical pathes (vice versa)
        private static string CheckRoot(string root)
        {
            Guard.CheckNull(root, "root");

            if (!CheckRootIsAllowed(root))
            {
                throw new SecurityException(string.Format("Path '{0}' is not allowed!", root));
            }

            string physicalPath = WebHelper.ToPhysicalPath(root);

            if(!Directory.Exists(physicalPath))
            {
                throw new DirectoryNotFoundException(string.Format("Directory {0} is not found!", root));
            }

            return physicalPath;
        }

        private static bool CheckRootIsAllowed(string root)
        {
            IEnumerable<string> availableRoots = MainCfg.UploadRoots;

            foreach (string availableRoot in availableRoots)
            {
                if (root.StartsWith(availableRoot, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
