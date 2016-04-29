namespace FCWeb.ViewModels
{
    using System;

    public class PublicationViewModel
    {
        public int id { get; set; }
        public string author { get; set; }
        public string contentHTML { get; set; }
        public DateTime dateChanged { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateDisplayed { get; set; }
        public bool enable { get; set; }
        public string header { get; set; }
        public string image { get; set; }
        public int? imageGalleryId { get; set; }
        public byte priority { get; set; }
        public bool showImageInContent { get; set; }
        public string title { get; set; }
        public string urlKey { get; set; }
        public int? videoId { get; set; }
        public int visibility { get; set; }
        public string lead { get; set; }

        //public virtual Article article { get; set; }
        //public virtual ImageGallery imageGallery { get; set; }
        //public virtual Video video { get; set; }
    }
}
