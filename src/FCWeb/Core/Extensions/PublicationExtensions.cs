﻿namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class PublicationExtensions
    {
        public static IEnumerable<PublicationShortViewModel> ToShortViewModel(this IEnumerable<Publication> publications)
        {
            if (Guard.IsEmptyIEnumerable(publications)) { return new PublicationShortViewModel[0]; }

            // TODO: Cahce checking for existing
            return publications.Select(p => new PublicationShortViewModel()
            {
                id = p.Id,
                urlKey = p.URLKey,
                title = p.Title,
                header = p.Header,
                img = p.Image,
                //img = System.IO.File.Exists(WebHelper.ToPhysicalPath(p.Image)) 
                //    ? p.Image
                //    : FCCore.Configuration.MainCfg.Images.EmptyPreview,
                date = p.DateDisplayed,
                hasPhoto = p.imageGalleryId.HasValue,
                hasVideo = p.videoId.HasValue
            });
        }

        public static Publication ToBaseModel(this PublicationViewModel publicationModel)
        {
            if (publicationModel == null) { return null; }

            return new Publication()
            {
                Id = publicationModel.id,
                Author = publicationModel.author,
                ContentHTML = publicationModel.contentHTML,
                DateChanged = publicationModel.dateChanged,
                DateCreated = publicationModel.dateCreated,
                DateDisplayed = publicationModel.dateDisplayed,
                Enable = publicationModel.enable,
                Header = publicationModel.header,
                Image = publicationModel.image,
                imageGalleryId = publicationModel.imageGalleryId.HasValue && publicationModel.imageGalleryId > 0 ? publicationModel.imageGalleryId : null,
                Lead = publicationModel.lead,
                Priority = publicationModel.priority,
                ShowImageInContet = publicationModel.showImageInContent,
                Title = publicationModel.title,
                URLKey = publicationModel.urlKey,
                videoId = publicationModel.videoId.HasValue && publicationModel.videoId > 0 ? publicationModel.videoId : null,
                Visibility = publicationModel.visibility
            };
        }

        public static PublicationViewModel ToViewModel(this Publication publication)
        {
            if (publication == null) { return null; }

            return new PublicationViewModel()
            {
                id = publication.Id,
                author = publication.Author,
                contentHTML = publication.ContentHTML,
                dateChanged = publication.DateChanged,
                dateCreated = publication.DateCreated,
                dateDisplayed = publication.DateDisplayed,
                enable = publication.Enable,
                header = publication.Header,
                image = publication.Image,
                imageGalleryId = publication.imageGalleryId,
                priority = publication.Priority,
                showImageInContent = publication.ShowImageInContet,
                title = publication.Title,
                urlKey = publication.URLKey,
                videoId = publication.videoId,
                visibility = publication.Visibility,
                lead = publication.Lead
            };
        }

        public static IEnumerable<PublicationViewModel> ToViewModel(this IEnumerable<Publication> publications)
        {
            if (Guard.IsEmptyIEnumerable(publications)) { return new PublicationViewModel[0]; }

            return publications.Select(p => p.ToViewModel());
        }
    }
}
