namespace FCCore.Diagnostic.Logging.File
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class TextFileLoggerProvider : ILoggerProvider
    {
        private string path;

        public TextFileLoggerProvider(string _path)
        {
            path = _path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TextFileLogger(path);
        }

        public void Dispose()
        {
        }
    }
}
