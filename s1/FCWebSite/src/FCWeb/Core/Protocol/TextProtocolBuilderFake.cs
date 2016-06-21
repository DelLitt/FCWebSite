namespace FCWeb.Core.Protocol
{
    using System;
    using System.Collections.Generic;
    using FCCore.Common;
    using ViewModels.Protocol;

    public class TextProtocolBuilderFake : ITextProtocolBuilder
    {
        private GameNoteBuilder gameNoteBuilder;

        public TextProtocolBuilderFake(GameNoteBuilder gameNoteBuilder)
        {
            Guard.CheckNull(gameNoteBuilder, nameof(gameNoteBuilder));
            this.gameNoteBuilder = gameNoteBuilder;
        }

        public bool IsAvailable
        {
            get
            {
                return gameNoteBuilder.IsAvailable;
            }
        }

        public bool IsAvailableAway
        {
            get
            {
                return gameNoteBuilder.IsAvailableAway;
            }
        }

        public bool IsAvailableHome
        {
            get
            {
                return gameNoteBuilder.IsAvailableHome;
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
    }
}
