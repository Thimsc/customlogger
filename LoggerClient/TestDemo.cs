using DevOnLogger;
using DevOnLogger.Interface;
using DevOnLogger.Models;

namespace Test
{
    public class TestDemo
    {
        IDevOnCustomLogger _logger;
        public TestDemo(IDevOnCustomLogger l)
        {
            _logger = l.AttachCurrentType<TestDemo>();
        }

        public async Task Show()
        {
            //Test for log with error
            await _logger.LogAsync("Logger test method", MessageLevel.ERROR);
        }
    }
}

