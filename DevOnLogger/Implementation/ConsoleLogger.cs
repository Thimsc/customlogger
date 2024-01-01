using DevOnLogger.Interface;

namespace DevOnLogger.Implementation
{
    /// <summary>
    /// ConsoleLogger: Implements ICustomLogger which Sink messages to Console terminal.
    /// </summary>
    public class ConsoleLogger : ICustomLogger
    {
        public async Task LogMessageAsync (string LogMessage)
        {
            Console.WriteLine(LogMessage);
            await Task.FromResult<object>(null);
        }

        public void LogMessage(string LogMessage)
        {
            Console.WriteLine(LogMessage);
        }
    }
   

}
