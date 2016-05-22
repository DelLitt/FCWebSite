namespace FCWeb.ViewModels.Protocol
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GameProtocolViewModel
    {
        public ProtocolTeamViewModel home { get; set; } = new ProtocolTeamViewModel();
        public ProtocolTeamViewModel away { get; set; } = new ProtocolTeamViewModel();
    }

    public class ProtocolTeamViewModel
    {
        public IEnumerable<PersonViewModel> allPlayersUnique { get; set; } = new List<PersonViewModel>();
        public IEnumerable<PersonViewModel> allPlayers { get; set; } = new List<PersonViewModel>();
        public IEnumerable<ProtocolRecordViewModel> main { get; set; } = new List<ProtocolRecordViewModel>();
        public IEnumerable<ProtocolRecordViewModel> reserve { get; set; } = new List<ProtocolRecordViewModel>();
        public IEnumerable<ProtocolRecordViewModel> goals { get; set; } = new List<ProtocolRecordViewModel>();
    }

    public class ProtocolRecordViewModel
    {
        public int id { get; set; }
        public int? customIntValue { get; set; }
        public short eventId { get; set; }
        public int gameId { get; set; }
        public byte? minute { get; set; }
        public int? personId { get; set; }
        public int teamId { get; set; }
        public bool extraTime { get; set; }
        public PersonViewModel mainPerson { get; set; }
        public PersonViewModel extraPerson { get; set; }
    }
}
