namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Configuration;
    using FCCore.Helpers;
    using FCCore.Model;

    public class VideoBll : FCBllBase, IVideoBll
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
            string cacheKey = GetStringKey(nameof(GetVideo), urlKey);

            Video result = Cache.GetOrCreate(cacheKey, () => { return DALVideo.GetVideo(urlKey); });

            return result;
        }

        public IEnumerable<Video> GetMainVideos(int count, int offset)
        {
            string cacheKey = GetStringMethodKey(nameof(GetMainVideos), count, offset);

            IEnumerable<Video> result = Cache.GetOrCreate(cacheKey, () => { return GetMainVideosForce(count, offset); });

            return result;
        }

        public IEnumerable<Video> GetMainVideosForce(int count, int offset)
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
            string cacheKey = GetStringKey(nameof(GetLatestVideos), count, offset, groups);

            IEnumerable<Video> result = Cache.GetOrCreate(cacheKey, () => { return GetLatestVideosForce(count, offset, groups); });

            return result; ;
        }

        public IEnumerable<Video> GetLatestVideosForce(int count, int offset, IEnumerable<string> groups)
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
