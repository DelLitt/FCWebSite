namespace FCWeb.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Common;
    using FCCore.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ViewModels;
    using ViewModels.Protocol;

    public class ProtocolViewModelBuilder
    {
        private IGameProtocolManager protocolManager;
        private IPersonBll personBll { get; set; } = MainCfg.ServiceProvider.GetService<IPersonBll>();
        private IEventBll eventBll { get; set; } = MainCfg.ServiceProvider.GetService<IEventBll>();
        private IEnumerable<PersonViewModel> homePersons { get; set; }
        private IEnumerable<PersonViewModel> awayPersons { get; set; }
        private IEnumerable<EventViewModel> events { get; set; }

        public ProtocolViewModelBuilder(IGameProtocolManager protocolManager)
        {
            Guard.CheckNull(protocolManager, nameof(protocolManager));
            Guard.CheckNull(protocolManager.Game, "protocolManager.Game");

            this.protocolManager = protocolManager;

            homePersons = personBll.GetTeamPersons(GetTeamId(Side.Home), protocolManager.Game.GameDate).ToViewModel();
            awayPersons = personBll.GetTeamPersons(GetTeamId(Side.Away), protocolManager.Game.GameDate).ToViewModel();
        }

        public GameProtocolViewModel viewModel;
        public GameProtocolViewModel ViewModel
        {
            get
            {
                if(viewModel == null)
                {
                    viewModel = BuildViewModel();
                }

                return viewModel;
            }
        }

        private GameProtocolViewModel BuildViewModel()
        {
            var protocolViewModel = new GameProtocolViewModel();

            protocolViewModel.home.playersAll = GetPersons(Side.Home);
            protocolViewModel.home.playersSquad = protocolViewModel.home.playersAll;
            protocolViewModel.home.playersSubs = protocolViewModel.home.playersAll;
            protocolViewModel.home.main = GetMain(Side.Home);
            protocolViewModel.home.reserve = GetReserve(Side.Home);
            protocolViewModel.home.goals = GetGoals(Side.Home);
            protocolViewModel.home.subs = GetSubstitutions(Side.Home);
            protocolViewModel.home.cards = GetCards(Side.Home);
            protocolViewModel.home.others = GetOthers(Side.Home);

            protocolViewModel.away.playersAll = GetPersons(Side.Away);
            protocolViewModel.away.playersSquad = protocolViewModel.away.playersAll;
            protocolViewModel.away.playersSubs = protocolViewModel.away.playersAll;
            protocolViewModel.away.main = GetMain(Side.Away);
            protocolViewModel.away.reserve = GetReserve(Side.Away);
            protocolViewModel.away.goals = GetGoals(Side.Away);
            protocolViewModel.away.subs = GetSubstitutions(Side.Away);
            protocolViewModel.away.cards = GetCards(Side.Away);
            protocolViewModel.away.others = GetOthers(Side.Away);

            return protocolViewModel;
        }

        private IEnumerable<ProtocolRecordViewModel> GetMain(Side side)
        {
            int teamId = GetTeamId(side);
            IEnumerable<PersonViewModel> persons = GetPersons(side);
            IEnumerable<ProtocolRecordViewModel> records = protocolManager.GetMainPlayers(teamId).ToViewModel();

            FillPersons(records, persons);

            return records;
        }

        private IEnumerable<ProtocolRecordViewModel> GetReserve(Side side)
        {
            int teamId = GetTeamId(side);
            IEnumerable<PersonViewModel> persons = GetPersons(side);
            IEnumerable<ProtocolRecordViewModel> records = protocolManager.GetReservePlayers(teamId).ToViewModel();

            FillPersons(records, persons);

            return records;
        }

        private IEnumerable<ProtocolRecordViewModel> GetGoals(Side side)
        {
            int teamId = GetTeamId(side);
            IEnumerable<PersonViewModel> persons = GetPersons(side);
            IEnumerable<ProtocolRecordViewModel> records = protocolManager.GetGoals(teamId).ToViewModel();

            FillPersons(records, persons);

            return records;
        }

        private IEnumerable<ProtocolRecordViewModel> GetSubstitutions(Side side)
        {
            int teamId = GetTeamId(side);
            IEnumerable<PersonViewModel> persons = GetPersons(side);
            IEnumerable<ProtocolRecordViewModel> records = protocolManager.GetSubstitutions(teamId).ToViewModel();

            FillPersons(records, persons);

            return records;
        }

        private IEnumerable<ProtocolRecordViewModel> GetCards(Side side)
        {
            int teamId = GetTeamId(side);
            IEnumerable<ProtocolRecordViewModel> records = protocolManager.GetCards(teamId).ToViewModel();

            FillEvents(records);

            return records;
        }

        private IEnumerable<ProtocolRecordViewModel> GetOthers(Side side)
        {
            int teamId = GetTeamId(side);
            IEnumerable<ProtocolRecordViewModel> records = protocolManager.GetOthers(teamId).ToViewModel();

            FillEvents(records);

            return records;
        }

        private void FillPersons(IEnumerable<ProtocolRecordViewModel> records, IEnumerable<PersonViewModel> persons)
        {
            foreach (ProtocolRecordViewModel record in records)
            {
                record.mainPerson = persons.FirstOrDefault(p => p.id == record.personId);
                record.extraPerson = persons.FirstOrDefault(p => p.id == record.customIntValue);
            }
        }

        private void FillEvents(IEnumerable<ProtocolRecordViewModel> records)
        {
            IEnumerable<int> eventIds = records.Select(r => (int)r.eventId).Distinct();
            IEnumerable<EventViewModel> events = eventBll.GetEvents(eventIds).ToViewModel();

            foreach (ProtocolRecordViewModel record in records)
            {
                record.eventModel = events.FirstOrDefault(e => e.id == record.eventId);
            }
        }

        private int GetTeamId(Side side)
        {
            return side == Side.Home ? protocolManager.Game.homeId : protocolManager.Game.awayId;
        }

        private IEnumerable<PersonViewModel> GetPersons(Side side)
        {
            return side == Side.Home ? homePersons : awayPersons;
        }
    }
}
