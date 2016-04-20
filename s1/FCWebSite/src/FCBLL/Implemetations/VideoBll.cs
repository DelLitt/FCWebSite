namespace FCBLL.Implemetations
{
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using System.Collections.Generic;
    using FCCore.Model;
    using FCCore.Configuration;

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

        public IEnumerable<Video> GetLatestVideos(int count, int offset)
        {
            short visibility = (short)(MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News);
            return DALVideo.GetLatestVideos(count, offset, visibility);
        }

        public Video GetVideo(int id)
        {
            return DALVideo.GetVideo(id);
        }

        public int SaveVideo(Video entity)
        {
            return DALVideo.SaveVideo(entity);
        }

        public IEnumerable<Video> SearchVideosByTitle(string titlePart)
        {
            if(string.IsNullOrWhiteSpace(titlePart)) { return new Video[0]; }
            return DALVideo.SearchVideosByTitle(titlePart);
        }
    }
}
