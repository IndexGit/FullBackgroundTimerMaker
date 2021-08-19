using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerClases.Objects.Helpers
{
    public static class WindowEffects
    {
        public static void FadeIn(Form o, int interval = 80)
        {
            //Object is not fully invisible. Fade it in
            while (o.Opacity < 1.0)
            {
                //await Task.Delay(interval);
                Thread.Sleep(interval);
                
                o.Opacity += 0.1;
                o.Refresh();
                Application.DoEvents();
            }
            o.Opacity = 1; //make fully visible       
            o.Refresh();
        }

        public static void FadeOut(Form o, int interval = 80)
        {

            //Object is fully visible. Fade it out
            while (o.Opacity > 0.0)
            {
                //await Task.Delay(interval);
                Thread.Sleep(interval);
                o.Opacity -= 0.1;
                o.Refresh();
                Application.DoEvents();
            }
            o.Opacity = 0; //make fully invisible       
            o.Refresh();
        }
    }
}
