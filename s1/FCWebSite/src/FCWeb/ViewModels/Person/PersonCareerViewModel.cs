namespace FCWeb.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class PersonCareerViewModel
    {
        public int id { get; set; }
        public DateTime? dateFinish { get; set; }
        public DateTime dateStart { get; set; }
        public int personId { get; set; }
        public int teamId { get; set; }
        public string teamName { get; set; }
        public IEnumerable<ushort> tourneyIds { get; set; } = new ushort[0];
    }
}
