namespace FCWeb.Core.ViewModelHepers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Common;
    using ViewModels;

    public class TourneyVMHelper
    {
        private TourneyViewModel tourney;

        public TourneyVMHelper(TourneyViewModel tourney)
        {
            Guard.CheckNull(tourney, nameof(tourney));

            this.tourney = tourney;
        }

        public void FillRoundsAvailableTeams()
        {
            var roundVMHelper = new RoundVMHelper(tourney.rounds);
            roundVMHelper.FillAvailableTeams();
        }
    }
}
