using System.ComponentModel.DataAnnotations;

namespace TimerClases.Objects.Enums
{
    /// <summary>
    /// Типы событий в периоде
    /// </summary>
    public enum EPeriodEventType
    {
        [Display(Name = "Играть аудио-файл")]
        Sound,
        [Display(Name = "Сменить фоновое изображение")]
        BackgroundImage,
    }
}
