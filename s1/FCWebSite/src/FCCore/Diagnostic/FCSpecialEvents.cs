namespace FCCore.Diagnostic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public static class FCSpecialEvents
    {
        public static EventId HookEventId = new EventId(10101);
    }
}
