using System;
using System.Windows.Forms;
using FullBackgroundTimerMaker.Forms;

namespace FullBackgroundTimerMaker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                ErrorForm.Show("Ошибка в приложении",e);
            }

        }
    }
}
