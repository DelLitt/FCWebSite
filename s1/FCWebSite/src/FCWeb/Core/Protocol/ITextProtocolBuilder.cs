namespace FCWeb.Core.Protocol
{
    using System.Collections.Generic;
    using FCCore.Common;
    using ViewModels.Protocol;

    public interface ITextProtocolBuilder
    {
        bool IsAvailable { get; }
        bool IsAvailableHome { get; }
        bool IsAvailableAway { get; }
        IEnumerable<EntityLinkProtocolViewModel> GetMainSquad(Side side);
        IEnumerable<EntityLinkProtocolViewModel> GetReserve(Side side);
        IEnumerable<EntityLinkProtocolViewModel> GetGoals(Side side);
        IEnumerable<EntityLinkProtocolViewModel> GetYellows(Side side);
        IEnumerable<EntityLinkProtocolViewModel> GetReds(Side side);
        IEnumerable<EntityLinkProtocolViewModel> GetOthers(Side side);
    }
}
