using System;

namespace PerformanceLogger
{
    /// <summary>
    /// Provides DateTime.Now
    /// </summary>
    public class Clock : IClock
    {
        public DateTime Now => DateTime.Now;
    }
}