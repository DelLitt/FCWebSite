namespace FCWeb.ViewModels.Protocol
{
    using System.Collections.Generic;

    public class FakeProtocoGameViewModel
    {
        public FakeProtocolTeamViewModel home { get; set; } = new FakeProtocolTeamViewModel();
        public FakeProtocolTeamViewModel away { get; set; } = new FakeProtocolTeamViewModel();
    }
}
