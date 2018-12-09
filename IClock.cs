using System;

namespace perflogger
{
    /// <summary>
    /// Abstraction layer to DateTime.Now
    /// </summary>
    public interface IClock
    {
        DateTime Now { get; }
    }
}