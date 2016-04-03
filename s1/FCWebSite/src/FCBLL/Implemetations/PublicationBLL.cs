namespace FCBLL.Implemetations
{
    using FCCore.Abstractions.Bll;
    using System.Collections.Generic;
    using FCCore.Model;
    using FCCore.Abstractions.Dal;
    using FCCore.Configuration;

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

        public IEnumerable<Publication> GetMainPublications(int count, int offset)
        {
            short visibility = (short)(MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News);
            return DALPublication.GetLatestPublications(count, offset, visibility);
        }
    }
}
