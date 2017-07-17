namespace FCCore.Diagnostic.Logging.File
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public static class TextFileLoggerExtensions
    {
        public static ILoggerFactory AddTextFile(this ILoggerFactory factory, string filePath)
        {
            factory.AddProvider(new TextFileLoggerProvider(filePath));
            return factory;
        }
    }
}
