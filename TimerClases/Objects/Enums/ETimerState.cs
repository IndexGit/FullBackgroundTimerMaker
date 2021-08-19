using System.ComponentModel.DataAnnotations;

namespace TimerClases.Objects.Enums
{
    public enum ETimerState
    {
        [Display(Name = "Остановлен")]
        Stopped,
        [Display(Name = "Идёт..")]
        Running,
        [Display(Name = "Пауза ||")]
        Paused,
        [Display(Name = "Таймаут")]
        TimeOut
    }
}
