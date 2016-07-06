namespace FCCore.Abstractions.Bll
{
    using Model;
    using System.Collections.Generic;

    public interface IVideoBll
    {
        Video GetVideo(int id);
        IEnumerable<Video> GetMainVideos(int count, int offset);
        IEnumerable<Video> SearchByTitle(string text);
        int SaveVideo(Video entity);
    }
}
