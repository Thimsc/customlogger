using DevOnLogger.Models;

namespace DevOnLogger.Interface
{
    /// <summary>
    /// IDevOnCustomLogger:Observer interface which appends/register multiple logger objects to Sink for one or more mode of logging
    /// 
    /// </summary>
    public interface IDevOnCustomLogger
    {
        public void RegisterObserver(ICustomLogger observer);
        public Task LogAsync(string message, MessageLevel level);
        public void Log(string message, MessageLevel level);
        public IDevOnCustomLogger AttachCurrentType<T>();
    }

}
