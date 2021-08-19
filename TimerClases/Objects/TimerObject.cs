using System;
using TimerClases.Objects.Enums;
using TimerClases.Objects.Interface;

namespace TimerClases.Objects
{
    public class TimerObject: ITimer
    {

        public TimeSpan Time { get; set; }
        public ETimerState TimerState { get; set; }
        public TimeSpan TimeOutTime { get; set; }

        public PeriodObject PO;
        public TimerObject(PeriodObject po)
        {
            TimeOutTime = new TimeSpan(0,0,5,0);
            TimerState = ETimerState.Stopped;
            Time = po.PeriodDuration;

            PO = po;
        }


    }
}
