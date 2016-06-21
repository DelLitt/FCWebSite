namespace FCWeb.ViewModels
{
    using Protocol;

    public class GameNoteViewModel
    {
        public FakeProtocoGameViewModel fakeProtocol { get; set; } = new FakeProtocoGameViewModel();
        public GameNoteDataViewModel data { get; set; } = new GameNoteDataViewModel();
    }

    public class GameNoteDataViewModel
    {
        // for future extensions
    }
}
