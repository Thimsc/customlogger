using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOnLogger.Models
{
    /// <summary>
    /// MessageLevel: Error message seviority level, this must be passed every time when client logs
    /// </summary>
    public enum MessageLevel
    {
        FATAL,
        ERROR,
        WARN,
        INFO,
        DEBUG
    }
}
