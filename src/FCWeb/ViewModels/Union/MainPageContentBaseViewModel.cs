namespace FCWeb.ViewModels.Union
{
    using System.Collections.Generic;

    public class MainPageContentBaseViewModel
    {
        public IEnumerable<PublicationShortViewModel> publications { get; set; }
        public IEnumerable<ImageGalleryShortViewModel> imageGalleries { get; set; }
        public IEnumerable<VideoShortViewModel> videos { get; set; }
        public IEnumerable<RankingTableViewModel> rankingTable { get; set; }
    }
}
