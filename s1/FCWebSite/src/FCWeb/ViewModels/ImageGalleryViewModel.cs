namespace FCWeb.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImageGalleryViewModel
    {
        public int id { get; set; }
        public string author { get; set; }
        [Required]
        public DateTime dateChanged { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateDisplayed { get; set; }
        public string description { get; set; }
        public bool enable { get; set; }
        [Required]
        public string header { get; set; }
        [Range(0, 10)]
        public byte priority { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string urlKey { get; set; }
        public Guid userChanged { get; set; }
        public Guid userCreated { get; set; }
        public Guid? tempGuid { get; set; }
        public int visibility { get; set; }
        public string path { get; set; }
        public bool createNew { get; set; }
    }
}
