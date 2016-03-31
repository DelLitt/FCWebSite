namespace FCCore.Abstractions.Bll
{
    using FCCore.Model;
    using System.Collections.Generic;

    public interface IPublicationBll
    {
        Publication GetPublication(int id);
        IEnumerable<Publication> GetMainPublications(int count, int offset);
    }
}
