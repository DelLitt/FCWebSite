namespace FCWeb.ViewModels.Game
{
    using Protocol;

    public class GameNoteViewModel
    {
        public FakeProtocoGameViewModel fakeProtocol { get; set; } = new FakeProtocoGameViewModel();
        public GameNoteDataViewModel data { get; set; } = new GameNoteDataViewModel();
    }
}
