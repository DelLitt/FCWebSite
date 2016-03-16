using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Publication
    {
        public int Id { get; set; }
        public int? articleId { get; set; }
        public string ContentHTML { get; set; }
        public DateTime DateChanged { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDisplayed { get; set; }
        public bool Enable { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
        public int? imageGalleryId { get; set; }
        public byte Priority { get; set; }
        public bool ShowImageInContet { get; set; }
        public string Title { get; set; }
        public string URLKey { get; set; }
        public Guid userChanged { get; set; }
        public Guid userCreated { get; set; }
        public int? videoId { get; set; }
        public int Visibility { get; set; }

        public virtual Article article { get; set; }
        public virtual ImageGallery imageGallery { get; set; }
        public virtual Video video { get; set; }
    }
}
