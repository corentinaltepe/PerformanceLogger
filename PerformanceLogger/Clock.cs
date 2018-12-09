using System;

namespace PerformanceLogger
{
    /// <summary>
    /// Provides DateTime.Now
    /// </summary>
    class Clock : IClock
    {
        public DateTime Now => DateTime.Now;
    }
}