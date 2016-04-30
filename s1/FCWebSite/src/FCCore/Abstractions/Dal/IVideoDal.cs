namespace FCCore.Abstractions.Dal
{
    using FCCore.Model;
    using System.Collections.Generic;

    public interface IVideoDal : IDalBase
    {
        Video GetVideo(int id);
        IEnumerable<Video> GetLatestVideos(int count, int offset, short visibility);
        IEnumerable<Video> SearchByTitle(string text);
        int SaveVideo(Video entity);
    }
}
