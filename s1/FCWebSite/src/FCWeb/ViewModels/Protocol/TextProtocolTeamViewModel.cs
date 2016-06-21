namespace FCWeb.ViewModels.Protocol
{
    using System.Collections.Generic;

    public class TextProtocolTeamViewModel
    {
        public IEnumerable<EntityLinkProtocolViewModel> main { get; set; } = new EntityLinkProtocolViewModel[0];
        public IEnumerable<EntityLinkProtocolViewModel> reserve { get; set; } = new EntityLinkProtocolViewModel[0];
        public IEnumerable<EntityLinkProtocolViewModel> goals { get; set; } = new EntityLinkProtocolViewModel[0];
        public IEnumerable<EntityLinkProtocolViewModel> yellows { get; set; } = new EntityLinkProtocolViewModel[0];
        public IEnumerable<EntityLinkProtocolViewModel> reds { get; set; } = new EntityLinkProtocolViewModel[0];
        public IEnumerable<EntityLinkProtocolViewModel> others { get; set; } = new EntityLinkProtocolViewModel[0];
    }
}
