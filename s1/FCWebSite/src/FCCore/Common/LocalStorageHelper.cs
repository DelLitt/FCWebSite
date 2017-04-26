namespace FCCore.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;
    using Configuration;
    using Model.Storage;  

    public static class LocalStorageHelper
    {
        public static bool MoveFromTempToStorage(string storagePath, string tempPath, string tempFolderName)
        {
            if(string.IsNullOrWhiteSpace(storagePath))
            {
                throw new ArgumentException("Storage path is not defined!");
            }

            if (!CheckPathIsAllowed(storagePath, storagePath))
            {
                throw new SecurityException(string.Format("Storage path '{0}' is not allowed!", storagePath));
            }

            if(string.IsNullOrWhiteSpace(tempFolderName))
            {
                throw new ArgumentException("Temporary folder name is not defined!");
            }
            string physicalTempPath = WebHelper.ToPhysicalPath(tempPath);
            string physicalStoragePath = WebHelper.ToPhysicalPath(storagePath);

            if (!Directory.Exists(physicalStoragePath))
            {
                Directory.CreateDirectory(physicalStoragePath);
            }

            DirectoryCopy(physicalTempPath, physicalStoragePath, true);

            // delete temp folder
            var currentTempDir = new DirectoryInfo(physicalTempPath);
            while(currentTempDir != null)
            {
                if (currentTempDir.Name.Equals(tempFolderName, StringComparison.OrdinalIgnoreCase))
                {
                    currentTempDir.Delete(true);
                    return true;
                }

                currentTempDir = currentTempDir.Parent;
            }

            return false;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var sourceDir = new DirectoryInfo(sourceDirName);

            if (!sourceDir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = sourceDir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = sourceDir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static StorageFolder GetFolderView(string path, string root, bool createNew, bool recursive = false)
        {
            string physicalPath = CheckPath(path, root, createNew);
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

            var folderView = new StorageFolder()
            {
                path = path,
                name = Path.GetFileNameWithoutExtension(path) ?? string.Empty,
                parent = parent
            };

            IEnumerable<string> folders = Directory.EnumerateDirectories(physicalPath);
            if(folders != null)
            {
                folderView.folders = folders.Select(f => new StorageFolder()
                {
                    name = Path.GetFileNameWithoutExtension(f) ?? string.Empty,
                    path = WebHelper.ToVirtualPath(f)
                });
            }

            IEnumerable<string> files = Directory.EnumerateFiles(physicalPath);
            if (files != null)
            {
                folderView.files = files.Select(f => new StorageFile()
                {
                    name = Path.GetFileName(f) ?? string.Empty,
                    path = WebHelper.ToVirtualPath(f)
                });
            }

            //if (recursive) { };

            return folderView;
        }

        public static bool RemoveFiles(IEnumerable<StorageFile> files)
        {
            bool isAnyRemoved = false;

            if (Guard.IsEmptyIEnumerable(files)) { return isAnyRemoved; }

            foreach(StorageFile file in files)
            {
                int lastSlashPos = file.path.LastIndexOf('/');

                if(lastSlashPos < 0)
                {
                    // TODO: Log error
                    continue;
                }

                string path = file.path.Substring(0, lastSlashPos);
                string root = path;

                string physicalDirPath = CheckPath(path, root, false);
                string physicalFilePath = Path.Combine(physicalDirPath, file.name);

                var realFile = new FileInfo(physicalFilePath);

                if (!realFile.Exists)
                {
                    // TODO: Log error
                    continue;
                }

                try
                {
                    realFile.Delete();
                    isAnyRemoved = true;
                }
                catch
                {
                    // TODO: Log error
                    continue;
                }
            }

            return isAnyRemoved;
        }

        private static string CheckPath(string path, string root, bool createNew)
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
                if (createNew)
                {
                    Directory.CreateDirectory(physicalPath);
                }
                else
                {
                    throw new DirectoryNotFoundException(string.Format("Directory {0} is not found!", path));
                }
            }

            return physicalPath;
        }

        public static bool CheckPathIsAllowed(string path, string root)
        {
            IEnumerable<string> availableRoots = MainCfg.UploadRoots;

            if(!availableRoots.Any())
            {
                throw new KeyNotFoundException("UploadRoots doesn't set!");
            }

            bool allowed = false;
            foreach(var ar in availableRoots)
            {
                if(root.StartsWith(ar))
                {
                    allowed = true;
                    break;
                }
            }

            if(!allowed) { return false; }

            return path.StartsWith(root, StringComparison.OrdinalIgnoreCase);
        }
    }
}
