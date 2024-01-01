using DevOnLogger;
using DevOnLogger.Interface;
using DevOnLogger.Models;

namespace Test
{
    public class LoggerTest
    {
        IDevOnCustomLogger _logger;
        public LoggerTest(IDevOnCustomLogger l)
        {
            _logger = l.AttachCurrentType<LoggerTest>();
        }

        public async Task Print()
        {     
            //Test for log with info
            await _logger.LogAsync("Logger test info message", MessageLevel.INFO);
            await _logger.LogAsync("Logger test error message", MessageLevel.ERROR);
            await _logger.LogAsync("Logger test fatal message", MessageLevel.FATAL);
            await _logger.LogAsync("Logger test warning message", MessageLevel.WARN);
            await _logger.LogAsync("Logger test debug message", MessageLevel.DEBUG);

        }
    }
}

