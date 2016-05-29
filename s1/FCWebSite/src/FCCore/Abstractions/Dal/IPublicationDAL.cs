namespace FCCore.Abstractions.Dal
{
    using System.Collections.Generic;
    using Model;

    public interface IPublicationDal : IDalBase
    {
        Publication GetPublication(int id);
        Publication GetPublication(string urlKey);
        IEnumerable<Publication> GetLatestPublications(int count, int offset, short visibility);
        int SavPublication(Publication entity);
    }
}
