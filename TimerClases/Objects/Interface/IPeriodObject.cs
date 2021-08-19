using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using TimerClases.Objects.Enums;

namespace TimerClases.Objects.Interface
{
    public interface IPeriodObject
    {
        int Order { get; set; }
        int PeriodID { get; }
        [Required]
        string PeriodName { get; set; }
        [Required]
        TimeSpan PeriodDuration { get; set; }
        Color TimerColor { get; set; }
        Color NameColor { get; set; }
        EPosition NamePosition { get; set; }
        string BackGroundImage { get; set; }

        EPosition TimerPosition { get; set; }

        string TimerFont { get; set; }
        string NameFont { get; set; }

        BindingList<PeriodEvent> PeriodEvents { get; }

    }
}
