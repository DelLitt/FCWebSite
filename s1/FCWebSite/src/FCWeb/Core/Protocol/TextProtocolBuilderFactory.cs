namespace FCWeb.Core.Protocol
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Abstractions.Bll.Protocol;
    using FCCore.Configuration;

    public static class TextProtocolBuilderFactory
    {
        public static ITextProtocolBuilder Create(IGameProtocolManager protocolManager)
        {
            return new TextProtocolBuilder(protocolManager);
        }

        public static ITextProtocolBuilder Create(GameNoteBuilder gameNoteBuilder)
        {
            return new TextProtocolBuilderFake(gameNoteBuilder);
        }
    }
}
