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
    public class YouTubeVideoService : IVideoService
    {
        private string SizeSmallCode = "1";
        private string SizeBigCode = "0";

        private Video video;

        public YouTubeVideoService(Video video)
        {
            Guard.CheckNull(video, nameof(video));

            this.video = video;
        }

        public string ImagePublicationItem
        {
            get
            {
                if(string.IsNullOrWhiteSpace(video.ExternalId))
                {
                    return MainCfg.Images.EmptyPreview;
                }

                return string.Format(CultureInfo.InvariantCulture, "http://img.youtube.com/vi/{0}/{1}.jpg",
                                                                    video.ExternalId,
                                                                    SizeBigCode);
            }
        }
    }
}
