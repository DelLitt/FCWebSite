namespace FCWeb.ViewModels.Protocol
{
    using System.Collections.Generic;

    public class ProtocolTeamViewModel
    {
        public int teamId { get; set; }
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
}
