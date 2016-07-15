namespace FCBLL.Implementations
{
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Dal;
    using FCCore.Configuration;
    using FCCore.Helpers;
    using FCCore.Model;

    public sealed class PublicationBll : IPublicationBll
    {
        private IPublicationDal dalPublication;
        private IPublicationDal DALPublication
        {
            get
            {
                if (dalPublication == null)
                {
                    dalPublication = DALFactory.Create<IPublicationDal>();
                }

                return dalPublication;
            }
        }

        public Publication GetPublication(int id)
        {
            return DALPublication.GetPublication(id);
        }

        public Publication GetPublication(string urlKey)
        {
            return DALPublication.GetPublication(urlKey);
        }

        public IEnumerable<Publication> GetMainPublications(int count, int offset)
        {
            short visibility = (short)(MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News);

            return DALPublication.GetLatestPublications(count, offset, visibility);
        }

        public IEnumerable<Publication> GetLatestPublications(int count, int offset)
        {
            return DALPublication.GetLatestPublications(count, offset);
        }

        public IEnumerable<Publication> GetLatestPublications(int count, int offset, IEnumerable<string> groups)
        {
            var visibility = (short)VisibilityHelper.VisibilityFromStrings(groups);

            return DALPublication.GetLatestPublications(count, offset, visibility);
        }

        public IEnumerable<Publication> SearchByDefault(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new Publication[0]; }

            return DALPublication.SearchByDefault(text);
        }

        public int SavePublication(Publication entity)
        {
            return DALPublication.SavPublication(entity);
        }
    }
}
