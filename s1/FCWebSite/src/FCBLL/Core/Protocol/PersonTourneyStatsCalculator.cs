namespace FCBLL.Core.Protocol
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Common;
    using FCCore.Model;

    public class PersonTourneyStatsCalculator
    {
        private Tourney tourney;
        private IEnumerable<Person> persons;

        private IList<PersonStatistics> personStatistics;
        public IEnumerable<PersonStatistics> PersonStatistics
        {
            get
            {
                if(personStatistics == null)
                {
                    personStatistics = new List<PersonStatistics>();
                    Calculate(ref personStatistics);
                }

                return personStatistics;
            }
        }

        /// <summary>
        /// Constructor. Should be filled rounds, games, protocol records and persons career
        /// </summary>
        /// <param name="tourney">Tourney. Should be filled with rounds, games and protocol records</param>
        /// <param name="persons">Persons. Should be filled with person career</param>
        public PersonTourneyStatsCalculator(Tourney tourney, IEnumerable<Person> persons)
        {
            this.tourney = tourney;

            if (tourney.Round == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Unable to calculate statistic of the tournament (ID:{0}, Name:{1}). {2} are not set!",
                        tourney.Id,
                        tourney.Name,
                        nameof(tourney.Round)));
            }

            this.persons = persons;
        }

        private void Calculate(ref IList<PersonStatistics> personStats)
        {
            foreach (Round round in tourney.Round.Where(r => r.Game != null))
            {
                foreach (Game game in round.Game)
                {
                    var gameStatsCalculator = new PersonGameStatsCalculator(game);

                    foreach (Person person in persons)
                    {                        
                        PersonStatistics personGameStats = gameStatsCalculator.Calculate(person);

                        if (personGameStats != null)
                        {
                            AddPersonStats(personStats, personGameStats);
                        }
                    }
                }
            }
        }

        private void AddPersonStats(IList<PersonStatistics> personStatistics, PersonStatistics personStatsItem)
        {
            PersonStatistics existingPersonStatsItem = personStatistics.SingleOrDefault(ps => ps.personId == personStatsItem.personId);
            personStatsItem.tourneyId = tourney.Id;

            if(existingPersonStatsItem == null)
            {
                personStatistics.Add(personStatsItem);
                return;
            }

            ComputeStatsItems(existingPersonStatsItem, personStatsItem);
        }

        private void ComputeStatsItems(PersonStatistics incrementalItem, PersonStatistics addItem)
        {
            incrementalItem.Assists += addItem.Assists;
            incrementalItem.Games += addItem.Games;
            incrementalItem.Goals += addItem.Goals;
            incrementalItem.Reds += addItem.Reds;
            incrementalItem.Substitutes += addItem.Substitutes;
            incrementalItem.Yellows += addItem.Yellows;
            incrementalItem.CustomIntValue += addItem.CustomIntValue;
            incrementalItem.teamId = addItem.teamId;
            incrementalItem.tourneyId = tourney.Id;
        }
    }
}
