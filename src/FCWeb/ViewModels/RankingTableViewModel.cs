namespace FCWeb.ViewModels
{
    using System.Collections.Generic;

    public class RankingTableViewModel
    {
        public string name { get; set; }
        public IEnumerable<TableRecordViewModel> rows { get; set; }
        public string data { get; set; }
    }
}
