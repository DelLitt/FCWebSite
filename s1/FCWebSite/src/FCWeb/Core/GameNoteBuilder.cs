namespace FCWeb.Core
{
    using System.Linq;
    using FCCore.Model;
    using Newtonsoft.Json;
    using ViewModels;
    using ViewModels.Protocol;

    public class GameNoteBuilder
    {
        private GameNoteViewModel gameNote;

        public Game Game { get; private set; }

        public bool IsAvailable
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Game.Note);
            }
        }

        public bool IsAvailableAway
        {
            get
            {
                return gameNote.fakeProtocol.away.main.Any()
                    || gameNote.fakeProtocol.away.reserve.Any()
                    || gameNote.fakeProtocol.away.goals.Any()
                    || gameNote.fakeProtocol.away.yellows.Any()
                    || gameNote.fakeProtocol.away.reds.Any()
                    || gameNote.fakeProtocol.away.others.Any();
            }
        }

        public bool IsAvailableHome
        {
            get
            {
                return gameNote.fakeProtocol.home.main.Any()
                    || gameNote.fakeProtocol.home.reserve.Any()
                    || gameNote.fakeProtocol.home.goals.Any()
                    || gameNote.fakeProtocol.home.yellows.Any()
                    || gameNote.fakeProtocol.home.reds.Any()
                    || gameNote.fakeProtocol.home.others.Any();
            }
        }

        public GameNoteBuilder(Game game)
        {
            Game = game;

            gameNote = !string.IsNullOrWhiteSpace(Game.Note) 
                ? JsonConvert.DeserializeObject<GameNoteViewModel>(Game.Note)
                : new GameNoteViewModel();
        }

        public FakeProtocoGameViewModel FakeProtocol
        {
            get
            {
                return gameNote.fakeProtocol;
            }

            set
            {
                gameNote.fakeProtocol = value;
                SerializeNoteData();
            }
        }

        public GameNoteDataViewModel Data
        {
            get
            {
                return gameNote.data;
            }

            set
            {
                gameNote.data = value;
                SerializeNoteData();
            }
        }

        private void SerializeNoteData()
        {
            Game.Note = JsonConvert.SerializeObject(gameNote);
        }
    }
}
