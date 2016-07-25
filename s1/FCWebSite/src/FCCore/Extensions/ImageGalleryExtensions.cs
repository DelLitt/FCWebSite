namespace FCCore.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Configuration;
    using Model;

    public static class ImageGalleryExtensions
    {
        public static string GetGalleryUniqueDir(this ImageGallery item, Guid? temGuid = null, bool tempStrongly = false)
        {
            if (item.Id == 0 && !temGuid.HasValue) { return string.Empty; }

            string uniqueId = !tempStrongly ?
                item.Id > 0 ? item.Id.ToString() : temGuid.Value.ToString()
                : temGuid.HasValue ? temGuid.Value.ToString() : item.Id.ToString();

            string dir = item.DateCreated.ToString("yyyyMMdd") + "-_-" + uniqueId;

            return dir;
        }

        public static string GetGalleryUniquePath(this ImageGallery item, Guid? temGuid = null)
        {
            if (item.Id == 0 && !temGuid.HasValue) { return string.Empty; }

            string uniqueDir = item.GetGalleryUniqueDir(temGuid);

            string path = MainCfg.Images.Gallery + "/" + Path.Combine(item.DateCreated.ToString("yyyy") + "/", uniqueDir);

            return path;
        }
    }
}
