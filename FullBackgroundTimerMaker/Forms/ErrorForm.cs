using System;
using System.Windows.Forms;
using TimerClases.Objects.Helpers;

namespace FullBackgroundTimerMaker.Forms
{
    public partial class ErrorForm : Form
    {
        public static ErrorForm Form = new ErrorForm();
        private ErrorForm()
        {
            InitializeComponent();
        }

        public static void Show(string Error, Exception ex)
        {
            Form.mError.Text = Error + Environment.NewLine + ex.MessageAll();
            Form.ShowDialog();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
