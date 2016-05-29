namespace FCWeb.Core.Extensions
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

            return publications.Select(p => new PublicationShortViewModel()
            {
                id = p.Id,
                title = p.Title,
                header = p.Header,
                img = p.Image
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
                imageGalleryId = null,
                Lead = publicationModel.lead,
                Priority = publicationModel.priority,
                ShowImageInContet = publicationModel.showImageInContent,
                Title = publicationModel.title,
                URLKey = publicationModel.urlKey,
                videoId = publicationModel.videoId,
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
