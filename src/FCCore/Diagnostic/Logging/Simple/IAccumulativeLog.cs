namespace FCCore.Diagnostic.Logging.Simple
{
    public interface IAccumulativeLog
    {
        bool Enable { get; set; }
        void Trace(string format, params object[] args);
        string Log { get; }
    }
}
