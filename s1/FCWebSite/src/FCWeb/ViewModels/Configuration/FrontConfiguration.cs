namespace FCWeb.ViewModels.Configuration
{
    using FCCore.Configuration;

    public class FrontConfiguration : WebConfiguration
    {
        public int TeamPublicationsCount
        {
            get
            {
                return MainCfg.TeamPublicationsCount;
            }
        }

        public int MainPublicationsCount
        {
            get
            {
                return MainCfg.MainPublicationsCount;
            }
        }

        public int MainPublicationsRowCount
        {
            get
            {
                return MainCfg.MainPublicationsCount;
            }
        }

        public int MainPublicationsHotCount
        {
            get
            {
                return MainCfg.MainPublicationsHotCount;
            }
        }

        public int MainPublicationsMoreCount
        {
            get
            {
                return MainCfg.MainPublicationsMoreCount;
            }
        }

        public int MainVideosCount
        {
            get
            {
                return MainCfg.MainVideosCount;
            }
        }

        public int MainVideosRowCount
        {
            get
            {
                return MainCfg.MainVideosRowCount;
            }
        }

        public int MainVideosMoreCount
        {
            get
            {
                return MainCfg.MainVideosMoreCount;
            }
        }

        public int MainGalleriesCount
        {
            get
            {
                return MainCfg.MainGalleriesCount;
            }
        }

        public int MainGalleriesRowCount
        {
            get
            {
                return MainCfg.MainGalleriesRowCount;
            }
        }

        public int MainGalleriesMoreCount
        {
            get
            {
                return MainCfg.MainGalleriesMoreCount;
            }
        }
    }
}
