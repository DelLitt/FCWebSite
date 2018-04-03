namespace FCWeb.Core.VideoServices
{
    using FCCore.Common;
    using FCCore.Model;

    public class VideoServicesFactory
    {
        public static IVideoService Create(Video video)
        {
            Guard.CheckNull(video, nameof(video));

            if(CheckForYouTube(video))
            {
                return new YouTubeVideoService(video);
            }

            return new UnknownVideoService();
        }

        private static bool CheckForYouTube(Video video)
        {
            return video.CodeHTML.Contains("youtube");
        }
    }
}
