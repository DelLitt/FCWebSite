namespace FCWeb.ViewModels.Protocol
{
    public class EntityLinkProtocolViewModel
    {
        public EntityLinkViewModel main { get; set; }
        public int? minute { get; set; }
        public int? extraMinute { get; set; }
        public EntityLinkProtocolViewModel extra { get; set; }
        public string info { get; set; }
        public string data { get; set; }
    }
}
