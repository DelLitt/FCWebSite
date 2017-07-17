namespace FCCore.Diagnostic.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public static class LoggerExtensions
    {
        public static void LogInformationHook(this ILogger logger, string message, params string[] args)
        {
            logger.LogInformation(FCSpecialEvents.HookEventId, message, args);
        }

        public static void LogWarningHook(this ILogger logger, string message, params string[] args)
        {
            logger.LogWarning(FCSpecialEvents.HookEventId, message, args);
        }

        public static void LogErrorHook(this ILogger logger, string message, params string[] args)
        {
            logger.LogError(FCSpecialEvents.HookEventId, message, args);
        }
    }
}
