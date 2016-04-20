using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCWeb.ViewModels
{
    public class AppConfigurationViewModel
    {
        public ImagesConfigurationViewModel images = new ImagesConfigurationViewModel();
        public VisibilityViewModel settingsVisibility = new VisibilityViewModel();
    }

    public class AppConfigurationOfficeViewModel
    {
        public ImagesConfigurationOfficeViewModel images = new ImagesConfigurationOfficeViewModel();
        public VisibilityViewModel settingsVisibility = new VisibilityViewModel();
    }

    public class ImagesConfigurationViewModel
    {
        public string persons { get; set; }
    }

    public class ImagesConfigurationOfficeViewModel
    {
        public string persons { get; set; }
        public string store { get; set; }
    }

    public class VisibilityViewModel
    {
        public short authorized { get; set; }
        public short main { get; set; }
        public short news { get; set; }
        public short reserve { get; set; }
        public short youth { get; set; }
    }
}
