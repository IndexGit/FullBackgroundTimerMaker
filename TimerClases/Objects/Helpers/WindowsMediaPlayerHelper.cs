using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace TimerClases.Objects.Helpers
{
    public static class WindowsMediaPlayerHelper
    {
        private static MediaPlayer _wplayer = new MediaPlayer();

        public static void PlayAsync(string fileFullPath)
        {
            try
            {

                _wplayer.Stop();

                _wplayer.Open(new Uri(fileFullPath));
               
                _wplayer.Play();

            }
            catch (Exception e)
            {
                //
            }

        }

        public static void Stop()
        {
            _wplayer.Stop();
        }

        public static void Close()
        {
            _wplayer.Close();
        }

    }
}
