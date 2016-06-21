namespace FCWeb.Core.Protocol
{
    using System;
    using System.Collections.Generic;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Common;
    using ViewModels.Protocol;

    public class TextProtocolBuilder : ITextProtocolBuilder
    {
        private IGameProtocolManager protocolManager;

        public TextProtocolBuilder(IGameProtocolManager protocolManager)
        {
            Guard.CheckNull(protocolManager, nameof(protocolManager));

            this.protocolManager = protocolManager;
        }

        public bool IsAvailable
        {
            get
            {
                return protocolManager.IsAvailable;
            }
        }

        public bool IsAvailableAway
        {
            get
            {
                return protocolManager.IsAvailableAway;
            }
        }

        public bool IsAvailableHome
        {
            get
            {
                return protocolManager.IsAvailableHome;
            }
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetGoals(Side side)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetMainSquad(Side side)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetOthers(Side side)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetReds(Side side)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetReserve(Side side)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetYellows(Side side)
        {
            throw new NotImplementedException();
        }

        private int GetTeamId(Side side)
        {
            return side == Side.Home ? protocolManager.Game.homeId : protocolManager.Game.awayId;
        }
    }
}
