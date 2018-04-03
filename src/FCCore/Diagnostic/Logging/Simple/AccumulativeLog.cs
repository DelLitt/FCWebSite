namespace FCCore.Diagnostic.Logging.Simple
{
    using System;
    using System.Globalization;
    using System.Text;

    public class AccumulativeLog : IAccumulativeLog
    {
        private StringBuilder sb = new StringBuilder();

        public bool Enable { get; set; } = false;

        public string Log
        {
            get
            {
                return sb.ToString();
            }
        }

        public void Trace(string format, params object[] args)
        {
            if(!Enable) { return; }

            DateTime now = DateTime.UtcNow;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, now.ToString("HH:mm:ss dd/MM/yyyy") + " | " + 
                format, args));
        }
    }
}
