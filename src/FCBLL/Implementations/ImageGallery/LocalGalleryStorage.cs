namespace FCBLL.Implementations.ImageGallery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Abstractions.Bll.ImageGallery;
    using FCCore.Common;
    using FCCore.Extensions;
    using FCCore.Model.Storage;


    public class LocalGalleryStorage : IGalleryStorage
    {
        private FCCore.Model.ImageGallery imageGallery;

        public LocalGalleryStorage(FCCore.Model.ImageGallery imageGallery)
        {
            this.imageGallery = imageGallery;
        }

        public IEnumerable<string> GetImagesList()
        {
            if (imageGallery.Id == 0)
            {
                return new string[0];
            }

            string galleryFolderPath = imageGallery.GetGalleryUniquePath();

            if(string.IsNullOrWhiteSpace(galleryFolderPath))
            {
                return new string[0];
            }

            StorageFolder folderView = LocalStorageHelper.GetFolderView(galleryFolderPath, galleryFolderPath, false);

            return !Guard.IsEmptyIEnumerable(folderView.files)
                ? folderView.files.Select(f => f.path)
                : new string[0];
        }
    }
}
