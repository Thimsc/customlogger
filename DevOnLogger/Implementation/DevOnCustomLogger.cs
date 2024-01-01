using DevOnLogger.Interface;
using DevOnLogger.Models;
using Microsoft.Extensions.Configuration;
using static System.Collections.Specialized.BitVector32;

namespace DevOnLogger.Implementation
{
    public class DevOnCustomLogger : IDevOnCustomLogger
    {
        private List<ICustomLogger> mObservers;

        private Type classType;

        public DevOnCustomLogger()
        {
            mObservers = new List<ICustomLogger>();
        }

        /// <summary>
        //AttachCurrentType: Is basically pass class type to logger which 
        // will be used to log the source where error occurred
        /// </summary>
        /// <typeparam name="T">T is the class type</typeparam>
        /// <returns>Returns current instance</returns>
        public IDevOnCustomLogger AttachCurrentType<T>()
        {
            classType = typeof(T);
            return this;
        }


        /// <summary>
        /// Configure: Register logger services based on configuration
        //Example config section on json file:
        /*"Logging": {
            "FileProvider": {
                "FileName": "log.txt",
                "LogDirectory": "."
            },
                "DBProvider": {
                    "ConnectionString": ""
                },
            "ConsoleProvider": {
                "Enable": true/false
             }
        },*/
        /// </summary>
        /// <param name="config">Json configuration section to configure different logging provider</param>
        /// <exception cref="Exception"></exception>
        public void Configure(IConfigurationSection config)
        {
            try
            {
                //Get config section from Client and Map to internal object
                var logConfig = config.Get<LoggingConfiguration>();

                //Register one or more log Sink based on configuration 
                if (logConfig == null) { throw new Exception("No providers are found"); }
                else
                {
                    //Register file based logging
                    if (logConfig.FileProvider != null
                        && !string.IsNullOrEmpty(logConfig.FileProvider.LogDirectory)
                        && !string.IsNullOrEmpty(logConfig.FileProvider.FileName))
                        mObservers.Add(new FileLogger(logConfig.FileProvider.LogDirectory, logConfig.FileProvider.FileName));

                    //Register database based logging
                    if (logConfig.DBProvider != null && !string.IsNullOrEmpty(logConfig.DBProvider.ConnectionString))
                        mObservers.Add(new DBLogger(logConfig.DBProvider.ConnectionString));

                    //Register Console based logging
                    if (logConfig.ConsoleProvider != null && logConfig.ConsoleProvider.Enable)
                        mObservers.Add(new ConsoleLogger());
                }
            }
            catch (Exception e)
            {
                throw new Exception("Errow while configure logger", e);
            }
        }

        //Register logger Sink manually
        public void RegisterObserver(ICustomLogger observer)
        {
            if (!mObservers.Contains(observer))
            {
                mObservers.Add(observer);
            }
        }

        /// <summary>
        /// Log messages from client
        /// </summary>
        /// <param name="message">description of the message</param>
        /// <param name="level">Seviority of the message</param>
        public async Task LogAsync(string Message, MessageLevel Level)
        {
            foreach (ICustomLogger observer in mObservers)
            {
                try
                {
                    await observer.LogMessageAsync(GetFormatedMessage(Message, Level));
                }
                catch (Exception e)
                {
                    throw new Exception("Error while logging.", e);
                }
                
            }
        }

        public void Log(string Message, MessageLevel Level)
        {
            foreach (ICustomLogger observer in mObservers)
            {
                try
                {
                    observer.LogMessage(GetFormatedMessage(Message, Level));
                }
                catch (Exception e)
                {
                    throw new Exception("Error while logging.", e);
                }
            }
        }

        /// <summary>
        /// GetFormatedMessage: Build formatted message to store into Sink
        /// Format: [MessageLevel][namespace] - Date : Error Message
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        private string GetFormatedMessage(string Message, MessageLevel Level)
        {
           return  string.Format("[{0}][{1}] - {2} : {3}",
            Enum.GetName(Level), classType == null ? "" : classType, DateTime.Now.ToString(), Message);
        }

    }
}
