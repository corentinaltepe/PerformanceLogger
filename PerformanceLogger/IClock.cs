using System;

namespace PerformanceLogger
{
    /// <summary>
    /// Abstraction layer to DateTime.Now
    /// </summary>
    interface IClock
    {
        DateTime Now { get; }
    }
}