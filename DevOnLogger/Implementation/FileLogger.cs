using DevOnLogger.Interface;

namespace DevOnLogger.Implementation
{
    /// <summary>
    /// FileLogger: Implements ICustomLogger which Sink messages to device files.
    /// </summary>
    public class FileLogger : ICustomLogger
    {
        string _absoluteFileName;
        private readonly object _sync = new object();

        /// <summary>
        /// FileLogger: This constructor check if directory and file are exist, it creates a file if it diesn't exixts
        /// </summary>
        /// <param name="LogDirectory"></param>
        /// <param name="FileName"></param>
        public FileLogger(string LogDirectory, string FileName)
        {
            if (!Directory.Exists(LogDirectory))
               throw new Exception("Directory doesn't exist:" + LogDirectory);

            _absoluteFileName = Path.Combine(LogDirectory, FileName);

            try
            {
                if (!File.Exists(_absoluteFileName))
                    File.Create(_absoluteFileName).Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error while creating File: " + _absoluteFileName, e);
            }
        }

        /// <summary>
        /// LogMessage: Append client messages to file.
        /// </summary>
        /// <param name="LogMessage"></param>
         public async Task LogMessageAsync(string LogMessage)
        {
            try
            {
                using (StreamWriter writetext = new StreamWriter(_absoluteFileName, true))
                {
                    await writetext.WriteLineAsync(LogMessage);
                }
            }
            catch (IOException e)
            {
                throw new Exception("Error while updating log to File sink.", e);
            }
        }

        public void LogMessage(string LogMessage)
        {
            try
            {
                lock (_sync)
                {
                    using (StreamWriter writetext = new StreamWriter(_absoluteFileName, true))
                    {
                        writetext.WriteLine(LogMessage);
                    }
                }
            }
            catch (IOException e)
            {
                throw new Exception("Error while updating log to File sink.", e);
            }

        }

    }
}
