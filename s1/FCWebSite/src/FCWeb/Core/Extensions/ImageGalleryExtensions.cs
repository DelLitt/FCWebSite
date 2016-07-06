namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Model;
    using ViewModels;

    public static class ImageGalleryExtensions
    {
        public static string GetGalleryUniquePath(this ImageGallery item)
        {
            if(item.Id == 0) { return string.Empty; }

            string path = MainCfg.Images.Gallery + "/" + Path.Combine(item.DateCreated.ToString("yyyy") + "/", item.DateCreated.ToString("yyyymmdd") + "-_-" + item.Id);

            return path;
        }

        public static string GetGalleryPublicationPreview(this ImageGallery item)
        {
            if (item.Id == 0) { return MainCfg.Images.EmptyPreview; }

            string uniquePath = item.GetGalleryUniquePath();

            DirectoryInfo dir = new DirectoryInfo(uniquePath);

            if (dir.Exists)
            {
                FileInfo[] presentFiles = dir.GetFiles() ?? new FileInfo[0];

                if(presentFiles.Any())
                {
                    return uniquePath + "/" + presentFiles.First().Name;
                }
            }

            return MainCfg.Images.EmptyPreview;
        }

        public static IEnumerable<ImageGalleryShortViewModel> ToShortViewModel(this IEnumerable<ImageGallery> imageGalleries)
        {
            if (Guard.IsEmptyIEnumerable(imageGalleries)) { return new ImageGalleryShortViewModel[0]; }

            return imageGalleries.Select(p => new ImageGalleryShortViewModel()
            {
                id = p.Id,
                urlKey = p.URLKey,
                title = p.Title,
                header = p.Header,
                img = p.GetGalleryPublicationPreview()
            });
        }
    }
}
