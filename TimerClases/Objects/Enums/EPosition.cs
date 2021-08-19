using System.ComponentModel.DataAnnotations;

namespace TimerClases.Objects.Enums
{
    public enum EPosition
    {
        [Display(Name = "По центру")]
        Center,
        [Display(Name = "Сверху")]
        Top,
        [Display(Name = "Внизу")]
        Bottom,
        [Display(Name = "Слева")]
        Left,
        [Display(Name = "Справа")]
        Right,
        [Display(Name = "Левый верхний угол")]
        TopLeft,
        [Display(Name = "Правый верхний угол")]
        TopRight,
        [Display(Name = "Левый нижний угол")]
        BottomLeft,
        [Display(Name = "Правый нижний угол")]
        BottomRight
    }
}
