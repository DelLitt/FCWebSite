namespace FCCore.Abstractions.Bll
{
    using Model;
    using System.Collections.Generic;

    public interface IPublicationBll
    {
        Publication GetPublication(int id);
        Publication GetPublication(string urlKey);
        IEnumerable<Publication> GetMainPublications(int count, int offset);
        IEnumerable<Publication> GetLatestPublications(int count, int offset);
        IEnumerable<Publication> GetLatestPublications(int count, int offset, IEnumerable<string> groups);
        IEnumerable<Publication> SearchByDefault(string text);
        int SavePublication(Publication entity);
    }
}
