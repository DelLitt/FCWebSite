namespace FCDAL.Implemetations
{
    using FCCore.Abstractions.Dal;
    using System.Collections.Generic;
    using FCCore.Model;
    using System.Linq;

    public class VideoDal : DalBase, IVideoDal
    {
        public IEnumerable<Video> GetLatestVideos(int count, int offset, short visibility)
        {
            if (count <= 0)
            {
                return new Video[0];
            }

            return Context.Video
                .OrderByDescending(p => p.DateDisplayed)
                .Skip(offset)
                .Take(count)
                .Where(p => p.Visibility == visibility);
        }

        public Video GetVideo(int id)
        {
            return Context.Video.FirstOrDefault(p => p.Id == id);
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

        public IEnumerable<Video> SearchVideosByTitle(string titlePart)
        {
            return Context.Video.Where(v => v.Title.Contains(titlePart));
        }
    }
}
