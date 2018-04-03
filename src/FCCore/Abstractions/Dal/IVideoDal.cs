namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IVideoDal : IDalBase
    {
        Video GetVideo(int id);
        Video GetVideo(string urlKey);
        IEnumerable<Video> GetLatestVideos(int count, int offset);
        IEnumerable<Video> GetLatestVideos(int count, int offset, short visibility);
        IEnumerable<Video> SearchByDefault(string text);
        int SaveVideo(Video entity);
    }
}
