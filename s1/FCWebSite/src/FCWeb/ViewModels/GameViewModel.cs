namespace FCWeb.ViewModels
{
    using System;

    public class GameViewModel
    {
        public int id { get; set; }
        public int? audience { get; set; }
        public byte? awayAddScore { get; set; }
        public int awayId { get; set; }
        public byte? awayPenalties { get; set; }
        public byte? awayScore { get; set; }
        public DateTime gameDate { get; set; }
        public byte? homeAddScore { get; set; }
        public int homeId { get; set; }
        public byte? homePenalties { get; set; }
        public byte? homeScore { get; set; }
        public int? imageGalleryId { get; set; }
        public string note { get; set; }
        public bool played { get; set; }
        public string referees { get; set; }
        public short roundId { get; set; }
        public bool showTime { get; set; }
        public int? stadiumId { get; set; }
        public int? videoId { get; set; }

        public RoundViewModel round { get; set; }
        public TeamViewModel home { get; set; }
        public TeamViewModel away { get; set; }
        public GameEntityLinkData dataEL { get; set; }
    }
}
