namespace FCWeb.ViewModels.Protocol
{
    public class ProtocolGameViewModel
    {
        public ProtocolTeamViewModel home { get; set; } = new ProtocolTeamViewModel();
        public ProtocolTeamViewModel away { get; set; } = new ProtocolTeamViewModel();

        public FakeProtocoGameViewModel fake { get; set; } = new FakeProtocoGameViewModel();
    }
}
