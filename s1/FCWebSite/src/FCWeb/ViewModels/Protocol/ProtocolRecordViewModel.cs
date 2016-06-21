namespace FCWeb.ViewModels.Protocol
{
    public class ProtocolRecordViewModel
    {
        public int id { get; set; }
        public int? customIntValue { get; set; }
        public short eventId { get; set; }
        public int gameId { get; set; }
        public byte? minute { get; set; }
        public int? personId { get; set; }
        public int teamId { get; set; }
        public bool extraTime { get; set; }

        public EventViewModel eventModel { get; set; }
        public PersonViewModel mainPerson { get; set; }
        public PersonViewModel extraPerson { get; set; }
    }
}
