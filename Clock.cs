using System;

namespace perflogger
{
    /// <summary>
    /// Provides DateTime.Now
    /// </summary>
    public class Clock : IClock
    {
        public DateTime Now => DateTime.Now;
    }
}