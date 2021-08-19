using System;
using TimerClases.Objects.Enums;

namespace TimerClases.Objects.Interface
{
    public interface ITimer
    {
        TimeSpan Time { get; set; }

        ETimerState TimerState { get; set; }

        TimeSpan TimeOutTime { get; set; }
    }
}
