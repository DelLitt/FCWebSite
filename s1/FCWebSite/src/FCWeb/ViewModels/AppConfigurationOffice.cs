using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCWeb.ViewModels
{
    public class AppConfigurationOfficeViewModel
    {
        public ImagesConfigurationViewModel images = new ImagesConfigurationViewModel();
    }

    public class ImagesConfigurationViewModel
    {
        public string persons { get; set; }
    }
}
