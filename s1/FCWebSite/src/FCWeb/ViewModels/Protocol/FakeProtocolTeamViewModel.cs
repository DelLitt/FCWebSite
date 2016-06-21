namespace FCWeb.ViewModels.Protocol
{
    using System.Collections.Generic;

    public class FakeProtocolTeamViewModel
    {
        public IEnumerable<FakeProtocolStartViewModel> main { get; set; } = new List<FakeProtocolStartViewModel>();
        public IEnumerable<FakeProtocolSubViewModel> reserve { get; set; } = new List<FakeProtocolSubViewModel>();
        public IEnumerable<FakeProtocolEventViewModel> goals { get; set; } = new List<FakeProtocolEventViewModel>();
        public IEnumerable<FakeProtocolEventViewModel> yellows { get; set; } = new List<FakeProtocolEventViewModel>();
        public IEnumerable<FakeProtocolEventViewModel> reds { get; set; } = new List<FakeProtocolEventViewModel>();
        public IEnumerable<FakeProtocolEventViewModel> others { get; set; } = new List<FakeProtocolEventViewModel>();
    }
}
