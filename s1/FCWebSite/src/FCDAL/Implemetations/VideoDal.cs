namespace FCDAL.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Abstractions.Dal;
    using FCCore.Model;

    public class VideoDal : DalBase, IVideoDal
    {
        public Video GetVideo(int id)
        {
            return Context.Video.FirstOrDefault(v => v.Id == id);
        }

        public Video GetVideo(string urlKey)
        {
            if (string.IsNullOrWhiteSpace(urlKey)) { return null; }

            return Context.Video.FirstOrDefault(v => v.URLKey.Equals(urlKey, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Video> GetLatestVideos(int count, int offset)
        {
            if (count <= 0)
            {
                return new Video[0];
            }

            return Context.Video
                .OrderByDescending(v => v.DateDisplayed)
                .Skip(offset)
                .Take(count);
        }

        public IEnumerable<Video> GetLatestVideos(int count, int offset, short visibility)
        {
            if (count <= 0)
            {
                return new Video[0];
            }

            return Context.Video
                .Where(v => (v.Visibility & visibility) != 0)
                .OrderByDescending(v => v.DateDisplayed)
                .Skip(offset)
                .Take(count);
        }

        public IEnumerable<Video> SearchByDefault(string text)
        {
            return Context.Video.Where(v => v.Title.Contains(text));
        }

        public int SaveVideo(Video entity)
        {
            if(entity.Id > 0)
            {
                Context.Video.Update(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }
            else
            {
                Context.Video.Add(entity, Microsoft.Data.Entity.GraphBehavior.SingleObject);
            }

            Context.SaveChanges();

            return entity.Id;
        }
    }
}
