using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevOnLogger
{
    /// <summary>
    /// This class is to build configuration for multiple types of Sink for logging.
    /// This class can be mapped from configudarion sent by Client
    /// </summary>
    public class LoggingConfiguration 
    {
        public FileProvider FileProvider { get; set; }
        public DBProvider DBProvider { get; set; }
        public ConsoleProvider ConsoleProvider { get; set; }
    }


    /// <summary>
    /// FileProvider: Type to prepare configuration for Sink client log messages to file
    /// </summary>
    public class FileProvider
    {
        //Name of the file with extension
        public string FileName { get; set; }

        //Directory path to store log.
        //Specify "." for current directory
        public string LogDirectory { get; set; }

    }

    /// <summary>
    /// DBProvider: Type to prepare configuration for Sink client log messages to database
    /// </summary>
    public class DBProvider
    {
        //Database connection string
        public string ConnectionString { get; set; }
    }

    /// <summary>
    /// DBProvider: Type to prepare configuration for Sink client log messages to console
    /// </summary>
    public class ConsoleProvider
    {
        //Enable: Set to true to enable log to console
        public bool Enable { get; set;}
    }

}
