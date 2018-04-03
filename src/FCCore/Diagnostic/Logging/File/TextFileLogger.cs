namespace FCCore.Diagnostic.Logging.File
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class TextFileLogger : ILogger
    {
        private string filePath;
        private object _lock = new object();

        public TextFileLogger(string path)
        {
            filePath = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null && eventId.Id == FCSpecialEvents.HookEventId.Id)
            {
                string preffix = $"{DateTime.UtcNow.ToString("yyyy.MM.dd HH:mm:ss")} - [{logLevel.ToString()}]: ";

                lock (_lock)
                {
                    System.IO.File.AppendAllText(filePath, preffix + formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
