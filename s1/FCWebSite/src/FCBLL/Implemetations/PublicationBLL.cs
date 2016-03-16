using FCCore.Abstractions.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCCore.Model;
using FCCore.Abstractions.DAL;
using FCCore.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCBLL.Implemetations
{
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

        public IEnumerable<Publication> GetMainPublications(int count, int offset)
        {
            short visibility = (short)(MainCfg.SettingsVisibility.Main | MainCfg.SettingsVisibility.News);
            return DALPublication.GetLatestPublications(count, offset, visibility);
        }
    }
}
