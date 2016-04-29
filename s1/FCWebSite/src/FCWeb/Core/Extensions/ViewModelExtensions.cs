namespace FCWeb.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using FCCore.Common;
    using FCCore.Model;
    using FCWeb.ViewModels;
    using System;
    using Newtonsoft.Json;
    public static class ViewModelExtensions
    {
        public static IEnumerable<PublicationShortViewModel> ToShortViewModel(this IEnumerable<Publication> publications)
        {
            if(Guard.IsEmptyIEnumerable(publications)) { return new PublicationShortViewModel[0]; }

            return publications.Select(p => new PublicationShortViewModel()
            {
                id = p.Id,
                title = p.Title,
                img = p.Image
            });
        }

        public static PublicationViewModel ToViewModel(this Publication publication)
        {
            if (publication == null) { return null; }

            return new PublicationViewModel()
            {
                id = publication.Id,
                author = publication.Author,
                contentHTML = publication.ContentHTML,
                dateChanged = publication.DateChanged,
                dateCreated = publication.DateCreated,
                dateDisplayed = publication.DateDisplayed,
                enable = publication.Enable,
                header = publication.Header,
                image = publication.Image,
                imageGalleryId = publication.imageGalleryId,
                priority = publication.Priority,
                showImageInContent = publication.ShowImageInContet,
                title = publication.Title,
                urlKey = publication.URLKey,
                videoId = publication.videoId,
                visibility = publication.Visibility,
                lead = publication.Lead
            };
        }

        public static IEnumerable<PublicationViewModel> ToViewModel(this IEnumerable<Publication> publications)
        {
            if (Guard.IsEmptyIEnumerable(publications)) { return new PublicationViewModel[0]; }

            return publications.Select(p => p.ToViewModel());
        }

        public static RankingTableViewModel ToViewModel(this IEnumerable<TableRecord> tableRecords, string tourneyName)
        {
            IEnumerable<TableRecordViewModel> rows = tableRecords == null
                ? new TableRecordViewModel[0]
                : tableRecords.Select(p => new TableRecordViewModel()
                    {
                        draws = p.Draws,
                        games = p.Games,
                        goalsAgainst = p.GoalsAgainst,
                        goalsFor = p.GoalsFor,
                        loses = p.Loses,
                        points = p.Points,
                        position = p.Position,
                        team = p.Team.Name,
                        wins = p.Wins
                    });


            return new RankingTableViewModel()
            {
                name = tourneyName,
                rows = rows
            };
        }

        public static IEnumerable<RoundViewModel> ToRoundViewModel(this IEnumerable<Game> games)
        {
            if (Guard.IsEmptyIEnumerable(games)) { return new RoundViewModel[0]; }

            IEnumerable<IGrouping<short, Game>> grouppedGamesByRound = games.GroupBy(g => g.roundId);
            var roundViews = new List<RoundViewModel>();

            foreach (IGrouping<short, Game> roundGames in grouppedGamesByRound)
            {
                var firstGame = roundGames.First();
                var roundView = new RoundViewModel()
                {
                    //tourney = firstGame?.round?.tourney?.Name ?? string.Empty,
                    tourney = "Турнир",
                    logo = string.Empty,
                    roundId = firstGame.roundId,
                    name = firstGame?.round?.Name ?? string.Empty,
                    dateGames = new List<DayGamesShortViewModel>()
                };

                IEnumerable<IGrouping<string, Game>> grouppedGamesByDay =
                    roundGames.GroupBy(g => g.GameDate.ToString("MMM d"));

                foreach (var dayGames in grouppedGamesByDay)
                {
                    var dayGameInfo = dayGames.First();
                    roundView.dateGames.Add(new DayGamesShortViewModel()
                    {
                        date = new DateInfoFormat1()
                        {
                            date = dayGameInfo.GameDate,
                            dateString = dayGameInfo.GameDate.ToString("d MMMM"),
                            dayString = dayGameInfo.GameDate.ToString("dddd")
                        },
                        games = dayGames.ToGameShortViewModel()
                    });
                }

                roundViews.Add(roundView);
            }

            return roundViews;
        }

        public static IEnumerable<GameShortViewModel> ToGameShortViewModel(this IEnumerable<Game> games)
        {
            if(Guard.IsEmptyIEnumerable(games)) { return new GameShortViewModel[0]; }

            return games.Select(g => new GameShortViewModel()
            {
                home = g?.home?.Name ?? string.Empty,
                away = g?.away?.Name ?? string.Empty,
                homeScore = g.homeScore,
                awayScore = g.awayScore,
                extra = g.HomePenalties.HasValue ? "пен." : (g.HomeAddScore.HasValue ? "доп." : string.Empty),
                start = g.GameDate,
                time = g.GameDate.ToString("HH:mm")
            });
        }

        public static PersonViewModel ToViewModel(this Person person)
        {
            if (person == null) { return null; }

            Guid? tempGuid = null;

            if (person.Id == 0)
            {
                tempGuid = Guid.NewGuid();
            }

            var infoView = !string.IsNullOrWhiteSpace(person.Info)
                ? JsonConvert.DeserializeObject<PersonInfoView>(person.Info)
                : new PersonInfoView();

            return new PersonViewModel()
            {
                id = person.Id,
                active = person.Active,
                birthDate = person.BirthDate,
                cityId = person.cityId,
                height = person.Height,
                image = person.Image,
                info = infoView,
                nameFirst = person.NameFirst,
                nameLast = person.NameLast,
                nameMiddle = person.NameMiddle,
                nameNick = person.NameNick,
                number = person.Number,
                personStatusId = person.personStatusId,
                roleId = person.roleId,
                teamId = person.teamId,
                weight = person.Weight,
                tempGuid = tempGuid
            };
        }

        public static VisibilityViewModel ToViewModel(this SettingsVisibility visibility)
        {
            if (visibility == null) { return null; }

            return new VisibilityViewModel()
            {
                authorized = visibility.Authorized,
                main = visibility.Main,
                news = visibility.News,
                reserve = visibility.Reserve,
                youth = visibility.Youth
            };
        }

        public static VideoViewModel ToViewModel(this Video video)
        {
            if (video == null) { return null; }

            return new VideoViewModel()
            {
                id = video.Id,
                author = video.Author,
                codeHTML = video.CodeHTML,
                description = video.Description,
                externalId = video.ExternalId,
                dateChanged = video.DateChanged,
                dateCreated = video.DateCreated,
                dateDisplayed = video.DateDisplayed,
                enable = video.Enable,
                header = video.Header,
                priority = video.Priority,
                title = video.Title,
                urlKey = video.URLKey,
                visibility = video.Visibility
            };
        }

        public static Video ToBaseModel(this VideoViewModel videoModel)
        {
            if (videoModel == null) { return null; }

            return new Video()
            {
                Id = videoModel.id,
                Author = videoModel.author,
                CodeHTML = videoModel.codeHTML,
                Description = videoModel.description,
                DateChanged = videoModel.dateChanged,
                DateCreated = videoModel.dateCreated,
                DateDisplayed = videoModel.dateDisplayed,
                Enable = videoModel.enable,
                ExternalId = videoModel.externalId,
                Header = videoModel.header,
                Priority = videoModel.priority,
                Title = videoModel.title,
                URLKey = videoModel.urlKey,
                Visibility = videoModel.visibility
            };
        }

        public static IEnumerable<VideoViewModel> ToViewModel(this IEnumerable<Video> videos)
        {
            if (Guard.IsEmptyIEnumerable(videos)) { return new VideoViewModel[0]; }

            return videos.Select(v => v.ToViewModel()).ToList();
        }

        public static Publication ToBaseModel(this PublicationViewModel publicationModel)
        {
            if (publicationModel == null) { return null; }

            return new Publication()
            {
                Id = publicationModel.id,
                articleId = null,
                Author = publicationModel.author,
                ContentHTML = publicationModel.contentHTML,               
                DateChanged = publicationModel.dateChanged,
                DateCreated = publicationModel.dateCreated,
                DateDisplayed = publicationModel.dateDisplayed,
                Enable = publicationModel.enable,
                Header = publicationModel.header,
                Image = publicationModel.image,
                imageGalleryId = null,
                Lead = publicationModel.lead,
                Priority = publicationModel.priority,
                ShowImageInContet = publicationModel.showImageInContent,                
                Title = publicationModel.title,
                URLKey = publicationModel.urlKey,
                videoId = publicationModel.videoId,
                Visibility = publicationModel.visibility
            };
        }

        public static Person ToBaseModel(this PersonViewModel personView)
        {
            if (personView == null) { return null; }

            string info = personView.info != null
                ? JsonConvert.SerializeObject(personView.info)
                : string.Empty;

            return new Person()
            {
                Id = personView.id,
                Active = personView.active,
                BirthDate = personView.birthDate,
                cityId = personView.cityId,
                Height = personView.height,
                Image = personView.image,
                Info = info,
                NameFirst = personView.nameFirst,
                NameLast = personView.nameLast,
                NameMiddle = personView.nameMiddle,
                NameNick = personView.nameNick,
                Number = personView.number,
                personStatusId = personView.personStatusId,
                roleId = personView.roleId,
                teamId = personView.teamId,
                Weight = personView.weight
            };
        }
    }
}
