using System.ComponentModel.DataAnnotations;

namespace TimerClases.Objects.Enums
{
    public enum EPeriodEventStartType
    {
        [Display(Name = "Через [X] от начала")]
        AfterStart,
        [Display(Name = "За [X] до конца")]
        BeforeEnd
    }
}
