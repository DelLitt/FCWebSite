namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using ViewModels;

    public static class VideoExtensions
    {
        public static VideoViewModel ToViewModel(this Video video)
        {
            if (video == null) { return null; }

            return new VideoViewModel()
            {
                id = video.Id,
                author = video.Author,
                codeHTML = video.CodeHTML,
                description = video.Description,
                externalId = video.ExternalId,
                dateChanged = video.DateChanged,
                dateCreated = video.DateCreated,
                dateDisplayed = video.DateDisplayed,
                enable = video.Enable,
                header = video.Header,
                priority = video.Priority,
                title = video.Title,
                urlKey = video.URLKey,
                visibility = video.Visibility
            };
        }

        public static Video ToBaseModel(this VideoViewModel videoModel)
        {
            if (videoModel == null) { return null; }

            return new Video()
            {
                Id = videoModel.id,
                Author = videoModel.author,
                CodeHTML = videoModel.codeHTML,
                Description = videoModel.description,
                DateChanged = videoModel.dateChanged,
                DateCreated = videoModel.dateCreated,
                DateDisplayed = videoModel.dateDisplayed,
                Enable = videoModel.enable,
                ExternalId = videoModel.externalId,
                Header = videoModel.header,
                Priority = videoModel.priority,
                Title = videoModel.title,
                URLKey = videoModel.urlKey,
                Visibility = videoModel.visibility
            };
        }

        public static IEnumerable<VideoViewModel> ToViewModel(this IEnumerable<Video> videos)
        {
            if (Guard.IsEmptyIEnumerable(videos)) { return new VideoViewModel[0]; }

            return videos.Select(v => v.ToViewModel()).ToList();
        }
    }
}
