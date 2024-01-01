namespace DevOnLogger.Interface
{
    //Observer pattern
    /// <summary>
    /// ICustomLogger: Custom logger interface, which can be implemented for different modes of logging mechanism
    /// for ex. File, Database, Console
    /// </summary>
    public interface ICustomLogger
    {
        Task LogMessageAsync(string LogMessage);
        void LogMessage(string LogMessage);
    }
}
