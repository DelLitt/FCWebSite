﻿namespace FCWeb.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FCCore.Abstractions.Bll.ImageGallery;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Extensions;
    using FCCore.Model;
    using Microsoft.Extensions.DependencyInjection;
    using ViewModels;

    public static class ImageGalleryExtensions
    {
        public static string GetGalleryPublicationPreview(this ImageGallery item)
        {
            if (item.Id == 0) { return MainCfg.Images.EmptyPreview; }

            string uniquePath = item.GetGalleryUniquePath();

            DirectoryInfo dir = new DirectoryInfo(WebHelper.ToPhysicalPath(uniquePath));

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
                img = p.GetGalleryPublicationPreview(),
                date = p.DateDisplayed
            });
        }

        public static ImageGalleryViewModel ToViewModel(this ImageGallery imageGallery)
        {
            if (imageGallery == null) { return null; }

            Guid? tempGuid = imageGallery.Id == 0 ? Guid.NewGuid() : (Guid?)null;

            var galleryStorageFactory = MainCfg.ServiceProvider.GetService<IGalleryStorageFactory>();
            IGalleryStorage galleryStorage = galleryStorageFactory.Create(imageGallery);

            IEnumerable<string> images = galleryStorage.GetImagesList();
            var imageItems = new List<ImageGalleryItemViewModel>();

            if(!Guard.IsEmptyIEnumerable(images))
            {
                imageItems.AddRange(images.Select(i => new ImageGalleryItemViewModel() { url = i }));
            }

            return new ImageGalleryViewModel()
            {
                id = imageGallery.Id,
                author = imageGallery.Author,
                description = imageGallery.Description,
                dateChanged = imageGallery.DateChanged,
                dateCreated = imageGallery.DateCreated,
                dateDisplayed = imageGallery.DateDisplayed,
                enable = imageGallery.Enable,
                header = imageGallery.Header,
                priority = imageGallery.Priority,
                title = imageGallery.Title,
                urlKey = imageGallery.URLKey,
                visibility = imageGallery.Visibility,
                tempGuid = tempGuid,
                path = imageGallery.GetGalleryUniquePath(tempGuid),
                createNew = tempGuid.HasValue,
                images = imageItems
            };
        }

        public static IEnumerable<ImageGalleryViewModel> ToViewModel(this IEnumerable<ImageGallery> imageGalleries)
        {
            if (Guard.IsEmptyIEnumerable(imageGalleries)) { return new ImageGalleryViewModel[0]; }

            return imageGalleries.Select(v => v.ToViewModel()).ToList();
        }

        public static ImageGallery ToBaseModel(this ImageGalleryViewModel imageGalleryModel)
        {
            if (imageGalleryModel == null) { return null; }

            return new ImageGallery()
            {
                Id = imageGalleryModel.id,
                Author = imageGalleryModel.author,
                Description = imageGalleryModel.description,
                DateChanged = imageGalleryModel.dateChanged,
                DateCreated = imageGalleryModel.dateCreated,
                DateDisplayed = imageGalleryModel.dateDisplayed,
                Enable = imageGalleryModel.enable,
                Header = imageGalleryModel.header,
                Priority = imageGalleryModel.priority,
                Title = imageGalleryModel.title,
                URLKey = imageGalleryModel.urlKey,
                Visibility = imageGalleryModel.visibility
            };
        }
    }
}
