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
        private IEnumerable<PersonViewModel> homePersons { get; set; }
        private IEnumerable<PersonViewModel> awayPersons { get; set; }

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

            protocolViewModel.home.allPlayers = GetPersons(Side.Home);
            protocolViewModel.home.allPlayersUnique = protocolViewModel.home.allPlayers;
            protocolViewModel.home.main = GetMain(Side.Home);
            protocolViewModel.home.reserve = GetReserve(Side.Home);
            protocolViewModel.home.goals = GetGoals(Side.Home);
            protocolViewModel.away.allPlayers = GetPersons(Side.Away);
            protocolViewModel.away.allPlayersUnique = protocolViewModel.away.allPlayers;
            protocolViewModel.away.main = GetMain(Side.Away);
            protocolViewModel.away.reserve = GetReserve(Side.Away);
            protocolViewModel.away.goals = GetGoals(Side.Away);

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

        private void FillPersons(IEnumerable<ProtocolRecordViewModel> records, IEnumerable<PersonViewModel> persons)
        {
            foreach (ProtocolRecordViewModel record in records)
            {
                record.mainPerson = persons.FirstOrDefault(p => p.id == record.personId);
                record.extraPerson = persons.FirstOrDefault(p => p.id == record.customIntValue);
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
