namespace FCWeb.Core.Protocol
{
    using System.Collections.Generic;
    using FCCore.Common;
    using ViewModels;
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
            IEnumerable<FakeProtocolEventViewModel> goals = side == Side.Home
                ? gameNoteBuilder?.FakeProtocol?.home?.goals ?? new FakeProtocolEventViewModel[0]
                : gameNoteBuilder?.FakeProtocol?.away?.goals ?? new FakeProtocolEventViewModel[0];

            return GetEntityLinkProtocol(goals);
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetMainSquad(Side side)
        {
            IEnumerable<FakeProtocolEventViewModel> main = side == Side.Home
                ? gameNoteBuilder?.FakeProtocol?.home?.main ?? new FakeProtocolEventViewModel[0]
                : gameNoteBuilder?.FakeProtocol?.away?.main ?? new FakeProtocolEventViewModel[0];

            return GetEntityLinkProtocol(main);
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetOthers(Side side)
        {
            IEnumerable<FakeProtocolEventViewModel> others = side == Side.Home
                ? gameNoteBuilder?.FakeProtocol?.home?.others ?? new FakeProtocolEventViewModel[0]
                : gameNoteBuilder?.FakeProtocol?.away?.others ?? new FakeProtocolEventViewModel[0];

            return GetEntityLinkProtocol(others);
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetReds(Side side)
        {
            IEnumerable<FakeProtocolEventViewModel> reds = side == Side.Home
                ? gameNoteBuilder?.FakeProtocol?.home?.reds ?? new FakeProtocolEventViewModel[0]
                : gameNoteBuilder?.FakeProtocol?.away?.reds ?? new FakeProtocolEventViewModel[0];

            return GetEntityLinkProtocol(reds);
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetReserve(Side side)
        {
            IEnumerable<FakeProtocolSubViewModel> reserve = side == Side.Home
                ? gameNoteBuilder?.FakeProtocol?.home?.reserve ?? new FakeProtocolSubViewModel[0]
                : gameNoteBuilder?.FakeProtocol?.away?.reserve ?? new FakeProtocolSubViewModel[0];

            var protocolData = new List<EntityLinkProtocolViewModel>();

            foreach (FakeProtocolSubViewModel fakeRecord in reserve)
            {
                EntityLinkProtocolViewModel entityLink = null;

                if (!string.IsNullOrWhiteSpace(fakeRecord.name))
                {
                    entityLink = new EntityLinkProtocolViewModel()
                    {
                        main = new EntityLinkViewModel()
                        {
                            id = string.Empty,
                            text = fakeRecord.name,
                            title = fakeRecord.name
                        }
                    };
                }

                if (entityLink != null)
                {
                    protocolData.Add(entityLink);
                }
            }

            return protocolData;
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetYellows(Side side)
        {
            IEnumerable<FakeProtocolEventViewModel> yellows = side == Side.Home
                ? gameNoteBuilder?.FakeProtocol?.home?.yellows ?? new FakeProtocolEventViewModel[0]
                : gameNoteBuilder?.FakeProtocol?.away?.yellows ?? new FakeProtocolEventViewModel[0];

            return GetEntityLinkProtocol(yellows);
        }

        private IEnumerable<EntityLinkProtocolViewModel> GetEntityLinkProtocol(IEnumerable<FakeProtocolEventViewModel> fakeRecords)
        {
            var protocolData = new List<EntityLinkProtocolViewModel>();

            foreach (FakeProtocolEventViewModel fr in fakeRecords)
            {
                EntityLinkProtocolViewModel el = GetEntityLinkProtocol(fr);
                if (el != null)
                {
                    protocolData.Add(el);
                }
            }

            return protocolData;
        }

        private EntityLinkProtocolViewModel GetEntityLinkProtocol(FakeProtocolEventViewModel protocolRecord)
        {
            EntityLinkProtocolViewModel entityLink = null;

            if (!string.IsNullOrWhiteSpace(protocolRecord.name))
            {

                entityLink = new EntityLinkProtocolViewModel();

                entityLink.main = new EntityLinkViewModel()
                {
                    id = string.Empty,
                    text = protocolRecord.name,
                    title = protocolRecord.name
                };

                entityLink.minute = protocolRecord.minute;
                entityLink.extraTime = protocolRecord.extraTime;
                entityLink.info = protocolRecord.info;
            }

            return entityLink;
        }
    }
}
