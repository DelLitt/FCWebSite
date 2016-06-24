namespace FCWeb.Core.Protocol
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using FCCore.Abstractions.Bll;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Common;
    using FCCore.Configuration;
    using FCCore.Model;
    using Microsoft.Extensions.DependencyInjection;
    using ViewModels;
    using ViewModels.Protocol;

    public class TextProtocolBuilder : ITextProtocolBuilder
    {
        private IGameProtocolManager protocolManager;        

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

        private bool isPersonsLoaded = false;
        private IEnumerable<Person> persons = new Person[0];
        protected IEnumerable<Person> Persons
        {
            get
            {
                if(!persons.Any() && !isPersonsLoaded)
                {
                    IPersonBll personBll = MainCfg.ServiceProvider.GetService<IPersonBll>();
                    persons = personBll.GetPersons(protocolManager.PersonIds);
                    isPersonsLoaded = true;
                }

                return persons;
            }
        }

        private bool isEventsLoaded = false;
        private IEnumerable<Event> events = new Event[0];
        protected IEnumerable<Event> Events
        {
            get
            {
                if (!events.Any() && !isEventsLoaded)
                {
                    IEventBll eventBll = MainCfg.ServiceProvider.GetService<IEventBll>();
                    events = eventBll.GetAll();
                    isEventsLoaded = true;
                }

                return events;
            }
        }

        public TextProtocolBuilder(IGameProtocolManager protocolManager)
        {
            Guard.CheckNull(protocolManager, nameof(protocolManager));

            this.protocolManager = protocolManager;
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetGoals(Side side)
        {
            IEnumerable<ProtocolRecord> goals = protocolManager.GetGoals(GetTeamId(side));

            var protocolData = new List<EntityLinkProtocolViewModel>();

            foreach (ProtocolRecord pr in goals)
            {
                EntityLinkProtocolViewModel el = GetEntityLinkProtocol(pr);
                if (el != null)
                {
                    protocolData.Add(el);
                }
            }

            return protocolData;
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetMainSquad(Side side)
        {
            IEnumerable<ProtocolRecord> main = protocolManager.GetMainPlayers(GetTeamId(side));
            IEnumerable<ProtocolRecord> subs = protocolManager.GetSubstitutions(GetTeamId(side));

            var protocolData = new List<EntityLinkProtocolViewModel>();

            foreach(ProtocolRecord pr in main)
            {
                if(pr.personId > 0)
                {
                    var el = new EntityLinkProtocolViewModel();

                    el.main = new EntityLinkViewModel()
                    {
                        id = pr.personId.ToString(),
                        text = GetPersonName(pr.personId.Value),
                        title = GetPersonName(pr.personId.Value)
                    };

                    el.minute = pr.Minute;
                    el.extraTime = pr.ExtraTime;
                    el.info = GetEventName(pr.eventId);

                    ProtocolRecord sub = subs.FirstOrDefault(s => s.CustomIntValue == pr.personId);

                    if (sub != null && sub.CustomIntValue.HasValue)
                    {
                        el.extra = new EntityLinkViewModel()
                        {
                            id = sub.personId.ToString(),
                            text = GetPersonName(sub.personId.Value),
                            title = GetPersonName(sub.personId.Value)
                        };
                    }

                    protocolData.Add(el);
                }
            }

            return protocolData;
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetOthers(Side side)
        {
            IEnumerable<ProtocolRecord> others = protocolManager.GetOthers(GetTeamId(side));

            var protocolData = new List<EntityLinkProtocolViewModel>();

            foreach (ProtocolRecord pr in others)
            {
                EntityLinkProtocolViewModel el = GetEntityLinkProtocol(pr);
                if (el != null)
                {
                    protocolData.Add(el);
                }
            }

            return protocolData;
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetReds(Side side)
        {
            IEnumerable<ProtocolRecord> reds = protocolManager.GetReds(GetTeamId(side));

            var protocolData = new List<EntityLinkProtocolViewModel>();

            foreach (ProtocolRecord pr in reds)
            {
                EntityLinkProtocolViewModel el = GetEntityLinkProtocol(pr);
                if (el != null)
                {
                    protocolData.Add(el);
                }
            }

            return protocolData;
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetReserve(Side side)
        {
            IEnumerable<ProtocolRecord> subs = protocolManager.GetSubstitutions(GetTeamId(side));
            IEnumerable<ProtocolRecord> reserve = protocolManager.GetReservePlayers(GetTeamId(side));

            var protocolData = new List<EntityLinkProtocolViewModel>();

            foreach (ProtocolRecord pr in reserve)
            {
                // don't duplicate players who will shown in main stuff as sub
                if (!subs.Any(r => r.personId == pr.personId))
                {
                    EntityLinkProtocolViewModel el = GetEntityLinkProtocol(pr);
                    if (el != null)
                    {
                        protocolData.Add(el);
                    }
                }
            }

            return protocolData;
        }

        public IEnumerable<EntityLinkProtocolViewModel> GetYellows(Side side)
        {
            IEnumerable<ProtocolRecord> yellows = protocolManager.GetYellows(GetTeamId(side));

            var protocolData = new List<EntityLinkProtocolViewModel>();

            foreach (ProtocolRecord pr in yellows)
            {
                EntityLinkProtocolViewModel el = GetEntityLinkProtocol(pr);
                if (el != null)
                {
                    protocolData.Add(el);
                }
            }

            return protocolData;
        }

        private int GetTeamId(Side side)
        {
            return side == Side.Home ? protocolManager.Game.homeId : protocolManager.Game.awayId;
        }

        private EntityLinkProtocolViewModel GetEntityLinkProtocol(ProtocolRecord protocolRecord)
        {
            EntityLinkProtocolViewModel entityLink = null;

            if (protocolRecord.personId > 0)
            {
                entityLink = new EntityLinkProtocolViewModel();

                entityLink.main = new EntityLinkViewModel()
                {
                    id = protocolRecord.personId.ToString(),
                    text = GetPersonName(protocolRecord.personId.Value),
                    title = GetPersonName(protocolRecord.personId.Value)
                };

                entityLink.minute = protocolRecord.Minute;
                entityLink.extraTime = protocolRecord.ExtraTime;
                entityLink.info = GetEventName(protocolRecord.eventId);

                if (protocolRecord.CustomIntValue.HasValue)
                {
                    entityLink.extra = new EntityLinkViewModel()
                    {
                        id = protocolRecord.CustomIntValue.ToString(),
                        text = GetPersonName(protocolRecord.CustomIntValue.Value),
                        title = GetPersonName(protocolRecord.CustomIntValue.Value)
                    };
                }
            }

            return entityLink;
        }

        private string GetPersonName(int personId)
        {
            Person person = Persons.FirstOrDefault(p => p.Id == personId);
            return person.NameDefaultWithNumber();
        }

        private string GetEventName(int eventId)
        {
            Event _event = Events.FirstOrDefault(e => e.Id == eventId);
            return _event.NameFull;
        }
    }
}
