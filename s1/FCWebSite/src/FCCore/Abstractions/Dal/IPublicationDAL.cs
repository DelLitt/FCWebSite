namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IPublicationDal : IDalBase
    {
        Publication GetPublication(int id);
        Publication GetPublication(string urlKey);
        IEnumerable<Publication> GetLatestPublications(int count, int offset, short visibility);
        IEnumerable<Publication> GetLatestPublications(int count, int offset);
        IEnumerable<Publication> SearchByDefault(string text);
        int SavPublication(Publication entity);
    }
}
