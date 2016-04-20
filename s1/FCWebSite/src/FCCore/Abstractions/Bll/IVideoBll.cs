namespace FCCore.Abstractions.Bll
{
    using FCCore.Model;
    using System.Collections.Generic;

    public interface IVideoBll
    {
        Video GetVideo(int id);
        IEnumerable<Video> GetLatestVideos(int count, int offset);
        IEnumerable<Video> SearchVideosByTitle(string titlePart);
        int SaveVideo(Video entity);
    }
}
