namespace FCBLL.Core.Protocol
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Common;
    using FCCore.Common.Constants;
    using FCCore.Model;

    public class PersonGameStatsCalculator
    {
        private Game game;

        /// <summary>
        /// Constructor. Game should be filled with protocol records
        /// </summary>
        /// <param name="game">Game. Should be filled with protocol records</param>
        public PersonGameStatsCalculator(Game game)
        {
            this.game = game;
        }

        /// <summary>
        /// Calculate statistic. Person should be filled with person career
        /// </summary>
        /// <param name="person">Person. Should be filled with person career</param>
        /// <returns></returns>
        public PersonStatistics Calculate(Person person)
        {
            PersonCareer personCareerItem = GetPersonCareerItemOfTheGame(person);

            if (personCareerItem == null || game.ProtocolRecord == null)
            {
                return null;
            }

            IEnumerable<ProtocolRecordPersonInfo> protocolPersonInfo =
                game.ProtocolRecord.Select(pr => new ProtocolRecordPersonInfo(person, pr));

            var personStats = new PersonStatistics()
            {
                teamId = personCareerItem.teamId,
                personId = personCareerItem.personId
            };

            CalculatePlayerOnThePitchStats(person, personCareerItem, protocolPersonInfo, ref personStats);

            if (personStats.Games > 0)
            {
                CalculateGoalkeeperAdditionalStats(person, personCareerItem, protocolPersonInfo, ref personStats);
            }

            return personStats.Games > 0 ? personStats : null;
        }

        private void CalculatePlayerOnThePitchStats(Person person, PersonCareer personCareerItem, IEnumerable<ProtocolRecordPersonInfo> protocolPersonInfo, ref PersonStatistics personStatistics)
        {
            IEnumerable<ProtocolRecordPersonInfo> playerOnPitchProtocol =
                protocolPersonInfo.Where(pr => pr.ProtocolRecord.teamId == personCareerItem.teamId
                                          && (pr.ProtocolRecord.personId == personCareerItem.personId 
                                            || pr.ProtocolRecord.CustomIntValue == personCareerItem.personId));

            if (Guard.IsEmptyIEnumerable(playerOnPitchProtocol))
            {
                return;
            }

            foreach (ProtocolRecordPersonInfo prInfo in playerOnPitchProtocol)
            {
                if (prInfo.IsGoal && person.roleId.HasValue)
                {
                    //if (PersonRoleGroupId.rgTeamPitchPlayer.Contains(person.roleId.Value))
                    //{
                        personStatistics.Goals++;
                    //}
                    //else if (person.roleId.Value == PersonRoleId.rrGoalkeeper)
                    //{
                        //personStatistics.CustomIntValue = (short)(personStatistics.CustomIntValue.HasValue ? personStatistics.CustomIntValue.Value + 1 : 1);
                    //}
                }
                else if (prInfo.IsAssist)
                {
                    personStatistics.Assists++;
                }
                else if (prInfo.IsStartMain)
                {
                    personStatistics.Games++;
                }
                else if (prInfo.IsSubstitutionIn)
                {
                    personStatistics.Games++;
                    personStatistics.Substitutes++;
                }
                else if (prInfo.IsYellowCard)
                {
                    personStatistics.Yellows++;
                }
                else if (prInfo.IsRedCard)
                {
                    personStatistics.Reds++;
                }
            }
        }

        private void CalculateGoalkeeperAdditionalStats(Person person, PersonCareer personCareerItem, IEnumerable<ProtocolRecordPersonInfo> protocolPersonInfo, ref PersonStatistics personStatistics)
        {
            if (person.roleId != PersonRoleId.rrGoalkeeper)
            {
                return;
            }

            ProtocolRecordPersonInfo recordIn = protocolPersonInfo.FirstOrDefault(pr => pr.IsStartMain || pr.IsSubstitutionIn);

            if(recordIn == null)
            {
                return;
            }

            // The value will be always present because pr.IsStartMain || pr.IsSubstitutionIn check it
            int minuteIn = recordIn.ProtocolRecord.Minute.Value;

            ProtocolRecordPersonInfo recordOut = protocolPersonInfo.FirstOrDefault(pr => pr.IsSubstitutionOut);
            
            int minuteOut = recordOut != null ? recordOut.ProtocolRecord.Minute.Value : GameDefinitions.MaxGameDurationInMinutes;

            IEnumerable<ProtocolRecordInfo> goalsAgainstProtocol = 
                protocolPersonInfo.Where(pr => pr.ProtocolRecord.teamId != personCareerItem.teamId)
                                  .Select(pr => new ProtocolRecordInfo(pr.ProtocolRecord))
                                  .Where(pr => pr.IsGoal);

            personStatistics.CustomIntValue = (short)goalsAgainstProtocol.Count(gp => gp.ProtocolRecord.Minute >= minuteIn && gp.ProtocolRecord.Minute <= minuteOut);
        }

        private PersonCareer GetPersonCareerItemOfTheGame(Person person)
        {
            if(Guard.IsEmptyIEnumerable(person.PersonCareer))
            {
                return null;
            }

            return person.PersonCareer.FirstOrDefault(pc => pc.DateStart <= game.GameDate && pc.DateFinish >= game.GameDate);
        }
    }
}
