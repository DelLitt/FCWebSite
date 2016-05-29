namespace FCBLL.Implementations
{
    using FCCore.Abstractions.Bll;
    using System.Collections.Generic;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using FCCore.Configuration;
    using System;

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

        public int SavePublication(Publication entity)
        {
            return DALPublication.SavPublication(entity);
        }
    }
}
