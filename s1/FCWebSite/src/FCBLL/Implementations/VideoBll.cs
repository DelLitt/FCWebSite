namespace FCBLL.Implementations
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Configuration;
    using FCCore.Helpers;
    using FCCore.Model;

    public class VideoBll : IVideoBll
    {
        private IVideoDal dalVideo;
        private IVideoDal DALVideo
        {
            get
            {
                if (dalVideo == null)
                {
                    dalVideo = DALFactory.Create<IVideoDal>();
                }

                return dalVideo;
            }
        }

        public Video GetVideo(int id)
        {
            return DALVideo.GetVideo(id);
        }

        public Video GetVideo(string urlKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Video> GetMainVideos(int count, int offset)
        {
            short visibility = (short)(MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News);

            return DALVideo.GetLatestVideos(count, offset, visibility);
        }

        public IEnumerable<Video> GetLatestVideos(int count, int offset)
        {
            return DALVideo.GetLatestVideos(count, offset);
        }

        public IEnumerable<Video> GetLatestVideos(int count, int offset, IEnumerable<string> groups)
        {
            var visibility = (short)VisibilityHelper.VisibilityFromStrings(groups);

            return DALVideo.GetLatestVideos(count, offset, visibility);
        }

        public int SaveVideo(Video entity)
        {
            return DALVideo.SaveVideo(entity);
        }

        public IEnumerable<Video> SearchByDefault(string text)
        {
            if(string.IsNullOrWhiteSpace(text)) { return new Video[0]; }

            return DALVideo.SearchByDefault(text);
        }
    }
}
