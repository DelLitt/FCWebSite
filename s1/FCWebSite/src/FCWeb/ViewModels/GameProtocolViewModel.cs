namespace FCWeb.ViewModels.Protocol
{
    using System.Collections.Generic;

    public class GameProtocolViewModel
    {
        public ProtocolTeamViewModel home { get; set; } = new ProtocolTeamViewModel();
        public ProtocolTeamViewModel away { get; set; } = new ProtocolTeamViewModel();
    }

    public class ProtocolTeamViewModel
    {
        public IEnumerable<PersonViewModel> playersSquad { get; set; } = new List<PersonViewModel>();
        public IEnumerable<PersonViewModel> playersSubs { get; set; } = new List<PersonViewModel>();
        public IEnumerable<PersonViewModel> playersAll { get; set; } = new List<PersonViewModel>();
        public IEnumerable<ProtocolRecordViewModel> main { get; set; } = new List<ProtocolRecordViewModel>();
        public IEnumerable<ProtocolRecordViewModel> reserve { get; set; } = new List<ProtocolRecordViewModel>();
        public IEnumerable<ProtocolRecordViewModel> goals { get; set; } = new List<ProtocolRecordViewModel>();
        public IEnumerable<ProtocolRecordViewModel> subs { get; set; } = new List<ProtocolRecordViewModel>();
        public IEnumerable<ProtocolRecordViewModel> cards { get; set; } = new List<ProtocolRecordViewModel>();
        public IEnumerable<ProtocolRecordViewModel> others { get; set; } = new List<ProtocolRecordViewModel>();
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

        public EventViewModel eventModel { get; set; }
        public PersonViewModel mainPerson { get; set; }
        public PersonViewModel extraPerson { get; set; }
    }
}
