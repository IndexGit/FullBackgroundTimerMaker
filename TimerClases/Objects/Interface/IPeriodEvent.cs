using System;
using System.Xml.Serialization;
using TimerClases.Objects.Enums;

namespace TimerClases.Objects.Interface
{
    public interface IPeriodEvent
    {
        string ObjectPath { get; set; }

        TimeSpan ObjectStartTime { get; set; }

        EPeriodEventType PeriodEventType { get; set; }

        EPeriodEventStartType PeriodEventStartType { get; set; }

        [XmlIgnore]
        bool Proceed { get; set; }
    }
}
