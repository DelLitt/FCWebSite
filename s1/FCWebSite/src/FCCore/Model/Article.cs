using System;
using System.Collections.Generic;

namespace FCCore.Model
{
    public partial class Article
    {
        public Article()
        {
            Publication = new HashSet<Publication>();
        }

        public int Id { get; set; }
        public string Author { get; set; }
        public string ContentHTML { get; set; }
        public DateTime DateChanged { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDisplayed { get; set; }
        public bool Enable { get; set; }
        public string Header { get; set; }
        public string Lead { get; set; }
        public string Title { get; set; }
        public string URLKey { get; set; }
        public Guid userChanged { get; set; }
        public Guid userCreated { get; set; }
        public bool Visible { get; set; }

        public virtual ICollection<Publication> Publication { get; set; }
    }
}
