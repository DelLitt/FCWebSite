﻿namespace FCWeb.ViewModels.Configuration
{
    using System.Collections.Generic;
    using Core;
    using Game;
    public class OfficeConfiguration : WebConfiguration
    {
        public Dictionary<int, string> EventGroupFriendlyNames
        {
            get
            {
                return EventHelper.FriendlyNames;
            }
        }

        public TourneyViewModel EmptyTourney
        {
            get
            {
                return new TourneyViewModel();
            }
        }

        public RoundViewModel EmptyRound
        {
            get
            {
                return new RoundViewModel();
            }
        }

        public GameViewModel EmptyGame
        {
            get
            {
                return new GameViewModel();
            }
        }


        public PersonViewModel EmptyPerson
        {
            get
            {
                return new PersonViewModel();
            }
        }

        public EntityLinkViewModel EmptyEntityLink
        {
            get
            {
                return new EntityLinkViewModel();
            }
        }
    }
}
