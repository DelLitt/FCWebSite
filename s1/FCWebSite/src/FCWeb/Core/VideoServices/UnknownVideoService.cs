using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FCCore.Common;
using FCCore.Configuration;
using FCCore.Model;

namespace FCWeb.Core.VideoServices
{
    public class UnknownVideoService : IVideoService
    {
        public string ImagePublicationItem
        {
            get
            {
                return MainCfg.Images.EmptyPreview;
            }
        }
    }
}
