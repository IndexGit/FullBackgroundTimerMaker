using System;

namespace TimerClases.Objects.Helpers
{
    public static class ExceptionHelper
    {
        public static string MessageAll(this Exception e)
        {
            var i = 0;
            var str = "Исключение: [" + e.GetType() + "] " + e.Message + Environment.NewLine;

            try
            {
                str += (e.TargetSite == null || String.IsNullOrEmpty(e.TargetSite.ToString())
                           ? ""
                           : "Метод: " + e.TargetSite + Environment.NewLine) +
                       (String.IsNullOrEmpty(e.Source) ? "" : "Источник: " + e.Source + Environment.NewLine);
                //#if Debug
                str += (String.IsNullOrEmpty(e.StackTrace) ? "" : "Стек: " + e.StackTrace + Environment.NewLine);
                //#endif
                var innere = e.InnerException;
                while (innere != null)
                {
                    i++;
                    str += "Вложенное исключение " + i + ": " + innere.Message + Environment.NewLine +
                           (innere.TargetSite == null || String.IsNullOrEmpty(innere.TargetSite.ToString())
                               ? ""
                               : "Метод: " + innere.TargetSite + Environment.NewLine) +
                           (String.IsNullOrEmpty(innere.Source)
                               ? ""
                               : "Источник: " + innere.Source + Environment.NewLine);
                    //#if Debug
                    if (String.IsNullOrEmpty(str))
                        str += (String.IsNullOrEmpty(innere.StackTrace) ? "" : "Стек: " + innere.StackTrace + Environment.NewLine);
                    //#endif
                    str += "---------------------";
                    innere = innere.InnerException;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ObjectExtention.MessageAll Exception: " + ex.Message);
            }

            return str;
        }
    }
}
