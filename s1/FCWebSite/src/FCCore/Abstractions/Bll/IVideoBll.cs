namespace FCCore.Abstractions.Bll
{
    using Model;
    using System.Collections.Generic;

    public interface IVideoBll
    {
        Video GetVideo(int id);
        Video GetVideo(string urlKey);
        IEnumerable<Video> GetMainVideos(int count, int offset);
        IEnumerable<Video> GetLatestVideos(int count, int offset);
        IEnumerable<Video> GetLatestVideos(int count, int offset, IEnumerable<string> groups);
        IEnumerable<Video> SearchByDefault(string text);
        int SaveVideo(Video entity);
    }
}
