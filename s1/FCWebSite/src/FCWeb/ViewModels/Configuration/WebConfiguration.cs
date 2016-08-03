﻿namespace FCWeb.ViewModels.Configuration
{
    using System.Collections.Generic;
    using FCCore.Common.Constants;
    using FCCore.Configuration;
    using FCCore.Model;
    using FCCore.Model.Refs;

    public class WebConfiguration
    {
        public SettingsVisibility SettingsVisibility
        {
            get
            {
                return MainCfg.SettingsVisibility;
            }
        }

        public int TimeShift
        {
            get
            {
                return MainCfg.TimeShift;
            }
        }

        public int MainTeamId
        {
            get
            {
                return MainCfg.MainTeamId;
            }
        }

        public MainCfg.ImagesCfg Images
        {
            get
            {
                return MainCfg.Images;
            }
        }

        public PersonRoleId PersonRoleIds
        {
            get
            {
                return new PersonRoleId();
            }
        }

        public PersonGroups PersonGroups
        {
            get
            {
                return new PersonGroups();
            }
        }

        public int MainTableTourneyId
        {
            get
            {
                return MainCfg.MainTableTourneyId;
            }
        }

        public IEnumerable<int> MainTeamTourneyIds
        {
            get
            {
                return MainCfg.MainTeamTourneyIds;
            }
        }

        public IEnumerable<int> ReserveTeamTourneyIds
        {
            get
            {
                return MainCfg.ReserveTeamTourneyIds;
            }
        }

        public string UrlKeyRegexPattern
        {
            get
            {
                return MainCfg.UrlKeyRegexPattern;
            }
        }
    }
}