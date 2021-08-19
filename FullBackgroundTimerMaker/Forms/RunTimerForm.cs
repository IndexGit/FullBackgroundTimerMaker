using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TimerClases.Objects;
using TimerClases.Objects.Enums;
using TimerClases.Objects.Helpers;

namespace FullBackgroundTimerMaker.Forms
{
    public partial class RunTimerForm : Form
    {
        private bool MouseTouched;
        private bool MouseUnderControls;
        private Point MouseLastPoint;

        private readonly PeriodProject PP;
        private TimerObject TM;

        private bool IsFullScreenMode;


        public RunTimerForm(PeriodProject pp)
        {
            InitializeComponent();
            PP = pp;
            SetDefault();
            //ResetTimer();
            TM = new TimerObject(PP.Current);
        }

        /// <summary>
        /// Установки из проекта по умолчанию
        /// </summary>
        private void SetDefault()
        {
            tsTimeOut.TimeSpan = PP.DefaultTimeOutTime;
        }

        /// <summary>
        /// Установка таймера следующего периода
        /// </summary>
        /// <param name="firstTime">Первый период?</param>
        /// <returns></returns>
        private bool SetNextTimer(bool firstTime = false)
        {
            // есть следующий таймер?
            var ismoved = firstTime || PP.MoveNext();

            var oldTM = TM;
            if (ismoved)
            {
                TM = new TimerObject(PP.Current);

                TM.PO.ResetEventsProceed();

                if (oldTM != null)
                {
                    TM.TimerState = oldTM.TimerState;
                }

                UpdateTimerUI(TM);
                return true;
            }
            else
            {
                RunPause(true);
                return false;
            }
            
        }

        private void UpdateTimerUI(TimerObject tm)
        {
            ControlsHideShow();

            lPeriodName.Text = tm.PO.PeriodName;
            lTimer.Text = tm.Time.ToString(@"hh\:mm\:ss");
            lTimer.ForeColor = tm.PO.TimerColor;
            if (!string.IsNullOrEmpty(tm.PO.BackGroundImage))
                BackgroundImage = tm.PO.GetBackGroundImage(PP.ProjectLocalImageFolder);

            lPeriodName.ForeColor = tm.PO.NameColor;

            // fonts
            lTimer.Font = new Font(tm.PO.TimerFont, PP.DefaultTimerFontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            lTimeOutTimer.Font = new Font(tm.PO.TimerFont, PP.DefaultTimerFontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            lPeriodName.Font = new Font(tm.PO.NameFont, PP.DefaultNameFontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            FullScreenTimer();
            SetLabelPosition(lPeriodName, tm.PO.NamePosition);

            SetLabelPosition(lTimer, tm.PO.TimerPosition);
            SetTimeOutTimerPosition();
            if(!IsFullScreenMode)
                FullScreenTimer(true);

            // buttons
            bPrevTimer.Enabled = PP.HasPrevPeriod;
            bNextTimer.Enabled = PP.HasNextPeriod;

            ControlsHideShow(true);
        }
        private int sp = 5;
        private void SetTimeOutTimerPosition()
        {
            lTimeOutTimer.Left = lTimer.Left;
            lTimeOutTimer.Top = lTimer.Top;
            lTimeOutTimer.Refresh();
        }

        
        private void SetLabelPosition(Label label, EPosition pos)
        {
            var cx = ClientRectangle.Width / 2 - label.Bounds.Width/2;
            var cy = ClientRectangle.Height / 2 - label.Bounds.Height/2;

            var bottomGap = lcControls.Bounds.Height + 30;

            switch (pos)
            {
                    case EPosition.Bottom:
                    label.Left = cx;
                    label.Top = ClientRectangle.Height - label.Bounds.Height - bottomGap - sp * 2;
                    break;

                    case EPosition.BottomLeft:
                    label.Left = sp;
                    label.Top = ClientRectangle.Height - label.Bounds.Height - bottomGap - sp * 2;
                    break;

                    case EPosition.BottomRight:
                    label.Left = ClientRectangle.Width - label.Bounds.Width - sp;
                    label.Top = ClientRectangle.Height - label.Bounds.Height - bottomGap - sp * 2;
                    break;

                    case EPosition.Center:
                    label.Left = cx;
                    label.Top = cy;
                    break;

                    case EPosition.Left:
                    label.Left = sp;
                    label.Top = cy;
                    break;

                    case EPosition.Right:
                    label.Left = ClientRectangle.Width - label.Bounds.Width - sp;
                    label.Top = cy;
                    break;

                    case EPosition.Top:
                    label.Left = cx;
                    label.Top = sp;
                    break;

                    case EPosition.TopLeft:
                    label.Left = sp;
                    label.Top = sp;
                    break;

                    case EPosition.TopRight:
                    label.Left = ClientRectangle.Width - label.Bounds.Width - sp;
                    label.Top = sp;
                    break;
            }

            label.Refresh();
        }

        private void timerShowControlBar_Tick(object sender, EventArgs e)
        {
            if (MouseTouched)
                MouseTouched = false;
            else
                if(!MouseUnderControls)
                    lcControls.Visible = false;
        }

        private void RunTimerForm_MouseMove(object sender, MouseEventArgs e)
        {
            var cp = Cursor.Position;
            MouseUnderControls = lcControls.ClientRectangle.Contains(lcControls.PointToClient(cp));
                

            MouseTouched = MouseLastPoint != cp;
            MouseLastPoint = cp;

            if (!lcControls.Visible && (MouseUnderControls || MouseTouched))
                lcControls.Visible = true;
        }


        /// <summary>
        /// Полноэктанный режим
        /// </summary>
        /// <param name="off">Выкл.</param>
        private void FullScreenTimer(bool off = false)
        {
            Application.DoEvents();
            if (!off)
            {
                TopMost = true;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                
            }
            else
            {
                TopMost = false;
                FormBorderStyle = FormBorderStyle.Sizable;
            }

            Refresh();
            Application.DoEvents();
        }

        private void bRunPause_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayerHelper.Stop();
            if (TM.TimerState == ETimerState.Stopped || TM.TimerState == ETimerState.Paused)
            {
                RunPause();
            }
            else if(TM.TimerState == ETimerState.Running)
            {
                RunPause(true);
            }
        }

        /// <summary>
        /// Запуск\Остановка таймера
        /// </summary>
        /// <param name="pause">Остановка</param>
        private void RunPause(bool pause = false)
        {
            WindowsMediaPlayerHelper.Stop();
            if (!pause)
            {
                //lTimer.Visible = true;
                TM.TimerState = ETimerState.Running;
                bRunPause.Text = @"Пауза ||";
                bRunPause.ImageIndex = 1;
                FullScreenTimer();
                mainTimer.Start();
            }
            else 
            {
                mainTimer.Stop();
                TM.TimerState = ETimerState.Paused;
                bRunPause.Text = @"ЗАПУСК >>>";
                bRunPause.ImageIndex = 0;
                FullScreenTimer(true);

                StopTimeOut();
            }
            IsFullScreenMode = !pause;
            mainTimer.Enabled = !pause;
            bTimeOut.Enabled = !pause;
            tsTimeOut.Enabled = !pause;
        }

        private void RunTimerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (TM != null)
                {
                    if (TM.TimerState == ETimerState.TimeOut)
                    {
                        StopTimeOut();
                        RunPause();
                    }

                    if (TM.TimerState == ETimerState.Running)
                    {
                        RunPause(true);
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    Close();
                }
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            TM.Time -= new TimeSpan(0,0,0,0, mainTimer.Interval);

            if (TM.Time <= TimeSpan.Zero)
            {
                TM.Time = TimeSpan.Zero;

                if (!SetNextTimer())                        
                {
                    RunPause(true);
                }
            }

            lTimer.Text = TM.Time.ToString(@"hh\:mm\:ss");

            if(!timeoutTimer.Enabled)
                ProcessPeriodEvents();

        }

        /// <summary>
        /// Обработка действий в периоде
        /// </summary>
        private void ProcessPeriodEvents(bool isForTimeOut = false)
        {
            if(!isForTimeOut)
                if (!TM.PO.DefaultChangePeriodSoundProceed &&
                    TM.PO.PeriodDuration - TM.Time < new TimeSpan(0, 0, 1))
                {
                    TM.PO.DefaultChangePeriodSoundProceed = true;
                    var hasAnyStartPeriodSound = TM.PO.PeriodEvents.Any(e => e.ObjectStartTime <= new TimeSpan(0, 0, 1));
                    if (!hasAnyStartPeriodSound)
                    {
                        PP.PlayDefaultSoundChangePeriod();                    
                    }
                }

            var ms = isForTimeOut
                ? PP.TimeOutEvents.Where(e => !e.Proceed)
                : TM.PO.PeriodEvents.Where(e => !e.Proceed);

            var po = isForTimeOut
                ? new PeriodObject()
                {
                    PeriodDuration = tsTimeOut.TimeSpan,

                }
                : TM.PO;

            // Для чуть раннего воспроизведения
            var delta = new TimeSpan(0,0,0,0,900);
            foreach (var e in ms)
            {
                var succeed = false;

                var currTime = (isForTimeOut ? TM.TimeOutTime : TM.Time) - delta;

                // проверка по времени
                // TM.Time истекает в обратном порядке
                switch (e.PeriodEventStartType)
                {
                        case EPeriodEventStartType.AfterStart:
                        succeed = currTime <= po.PeriodDuration - e.ObjectStartTime;
                        break;

                        case EPeriodEventStartType.BeforeEnd:
                        succeed = currTime <= e.ObjectStartTime;                        
                        break;
                }

                if (succeed)
                {
                    e.Proceed = true;

                    // действие по типу
                    switch (e.PeriodEventType)
                    {
                        case EPeriodEventType.BackgroundImage:
                            if (!string.IsNullOrEmpty(e.ObjectPath))
                            {
                                BackgroundImage = isForTimeOut ?
                                    e.GetImage(PP.ProjectLocalTimeOutImageFolder):
                                e.GetImage(PP.ProjectLocalEventImageFolder);
                            }
                            break;
                        case EPeriodEventType.Sound:
                            if (!string.IsNullOrEmpty(e.ObjectPath))
                            {
                                e.PlaySound(isForTimeOut
                                    ? PP.ProjectLocalTimeOutSoundFolder
                                    : PP.ProjectLocalEventSoundFolder);
                            }
                            break;
                    }
                }
            }
        }

        private void timeuotTimer_Tick(object sender, EventArgs e)
        {
            TM.TimeOutTime -= new TimeSpan(0, 0, 0, 0, timeoutTimer.Interval);
            if (TM.TimeOutTime <= TimeSpan.Zero)
            {
                StopTimeOut();
                RunPause();
            }
            else
            {
                lTimeOutTimer.Visible = true;
                lTimeOutTimer.Text = TM.TimeOutTime.ToString(@"hh\:mm\:ss");
            }

            ProcessPeriodEvents(true);
        }

        private void bTimeOut_Click(object sender, EventArgs e)
        {
            if (TM.TimerState == ETimerState.Running)
            {
                StartTimeOut();
            }
            else if(TM.TimerState == ETimerState.TimeOut)
            {
                StopTimeOut();
                RunPause();
            }
        }
        
        /// <summary>
        /// Запуск таймаута
        /// </summary>
        private void StartTimeOut()
        {
            PP.ResetTimeOutEvents();
            bRunPause.Enabled = false;
            TM.TimerState = ETimerState.TimeOut;
            mainTimer.Stop();
            timeoutTimer.Stop();
            TM.TimeOutTime = tsTimeOut.TimeSpan;
            lTimeOutTimer.Visible = true;
            lTimeOutTimer.Text = TM.TimeOutTime.ToString(@"hh\:mm\:ss");
            timeoutTimer.Start();
        }

        /// <summary>
        /// Остановить таймаут
        /// </summary>
        private void StopTimeOut()
        {
            bRunPause.Enabled = true;
            timeoutTimer.Stop();
            if(TM != null)
                TM.TimeOutTime = TimeSpan.Zero;
            lTimeOutTimer.Visible = false;
        }

        private void bRestartTimer_Click(object sender, EventArgs e)
        {
            StopTimeOut();
            RunPause(true);

            SetNextTimer(true);
        }

        private void ControlsHideShow(bool show = false)
        {
            lPeriodName.Visible = show;
            lTimer.Visible = show;
            Application.DoEvents();
            Refresh();
            Application.DoEvents();
        }


        private void bNextTimer_Click(object sender, EventArgs e)
        {
            StopTimeOut();
            RunPause(true);
            
            SetNextTimer();
            RunPause();
        }

        /// <summary>
        /// Полный сброс таймера
        /// </summary>
        public void ResetTimer()
        {
            RunPause(true);
            PP.Reset();
            SetNextTimer(true);
            RunPause(true);
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(@"Полностью перематать таймер?", @"Перезапуск таймера", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ResetTimer();
            }
        }

        private void bPrevTimer_Click(object sender, EventArgs e)
        {
            StopTimeOut();
            RunPause(true);
            PP.MoveBack();
            SetNextTimer(true);
        }

        private void RunTimerForm_Shown(object sender, EventArgs e)
        {
            //SetNextTimer(true);
            ResetTimer();
        }

        private void RunTimerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RunPause(true);
        }
    }
}
