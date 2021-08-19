using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using FullBackgroundTimerMaker.Properties;
using TimerClases.Objects;
using TimerClases.Objects.Enums;
using TimerClases.Objects.Helpers;
using System.Drawing;
using DevExpress.XtraEditors;

namespace FullBackgroundTimerMaker.Forms
{
    public partial class ProjectEditForm : Form
    {
        public PeriodProject PP;

        private PeriodEvent FocusedEvent => (gvTimeOutEvents.GetRow(gvTimeOutEvents.FocusedRowHandle) as PeriodEvent);


        public ProjectEditForm()
        {
            var pp = new PeriodProject();

            Init(pp);
        }

        public ProjectEditForm(PeriodProject pp)
        {
            Init(pp);
        }

        private void Init(PeriodProject pp)
        {
            InitializeComponent();
            PP = pp;

            tsDefaultTimeOutTime.EditValue = PP.DefaultTimeOutTime;

            gcTimeOutEvents.DataSource = PP.TimeOutEvents;
        }

        private void teProjectName_EditValueChanged(object sender, EventArgs e)
        {
            PP.Name = teProjectName.Text;
            PP.SetModified();
            UpdateUIStatus();
        }

        private void UpdateUIStatus()
        {
            bOk.Enabled = teProjectName.Text.Length > 0;
            bPlayDefaultPeriodEndSound.Enabled = !string.IsNullOrEmpty(PP.DefaultSoundChangePeriod);
            bClearDefaultPeriodEndSound.Enabled = !string.IsNullOrEmpty(PP.DefaultSoundChangePeriod);
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bCansel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ProjectEditForm_Load(object sender, EventArgs e)
        {
        }

        private void tePeriodEndSound_Click(object sender, EventArgs e)
        {
            var name = Guid.NewGuid().ToString("N");

            var cid = new OpenFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                CheckFileExists = true,
                Filter = @"Audio Files|*.mp3;*.wav;*.;*.wma;",
                Multiselect = false
            };
            if (cid.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var selImg = new FileInfo(cid.FileName);
                    var toImg = new FileInfo(Path.Combine(PP.ProjectLocalSoundFolder, name + selImg.Extension));

                    if (!toImg.Directory.Exists)
                        toImg.Directory.Create();

                    File.Copy(selImg.FullName, toImg.FullName, true);

                    PP.DefaultSoundChangePeriod = name + selImg.Extension;
                    tePeriodEndSound.Text = PP.DefaultSoundChangePeriod;
                    bPlayDefaultPeriodEndSound.Enabled = !string.IsNullOrEmpty(PP.DefaultSoundChangePeriod);
                    bClearDefaultPeriodEndSound.Enabled = !string.IsNullOrEmpty(PP.DefaultSoundChangePeriod);
                    PP.SetModified();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Не удалось выбрать аудиозапись: {ex.Message}");
                }
            }

        }

        private void bPlayDefaultPeriodEndSound_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(PP.DefaultSoundChangePeriod))
                PP.PlayDefaultSoundChangePeriod();
        }

        private void ProjectEditForm_Shown(object sender, EventArgs e)
        {
            teProjectName.Text = PP.Name;
            tePeriodEndSound.Text = PP.DefaultSoundChangePeriod;
            bPlayDefaultPeriodEndSound.Enabled = !string.IsNullOrEmpty(PP.DefaultSoundChangePeriod);
            bClearDefaultPeriodEndSound.Enabled = !string.IsNullOrEmpty(PP.DefaultSoundChangePeriod);
            teNameFontSize.Value = PP.DefaultNameFontSize;
            teTimerFontSize.Value = PP.DefaultTimerFontSize;

            UpdateUIStatus();
        }

        private void teNameFontSize_ValueChanged(object sender, EventArgs e)
        {
            PP.DefaultNameFontSize = (int) teNameFontSize.Value;
            PP.SetModified();
        }

        private void teTimerFontSize_ValueChanged(object sender, EventArgs e)
        {
            PP.DefaultTimerFontSize = (int) teTimerFontSize.Value;
            PP.SetModified();
        }

        private void bClearDefaultPeriodEndSound_Click(object sender, EventArgs e)
        {
            WindowsMediaPlayerHelper.Stop();
            PP.DefaultSoundChangePeriod = null;
            tePeriodEndSound.Text = "";
            PP.SetModified();
        }

        private void gvTimeOutEvents_PopupMenuShowing(object sender,
            DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == GridHitTest.EmptyRow ||
                e.HitInfo.HitTest == GridHitTest.RowCell)
                pmTimeOut.ShowPopup(MousePosition);
        }

        private void bbiAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvTimeOutEvents.AddNewRow();
            gvTimeOutEvents.ShowEditForm();
            PP.SetModified();
        }

        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvTimeOutEvents.FocusedRowHandle >= 0)
            {
                gvTimeOutEvents.ShowEditForm();
                PP.SetModified();
            }
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvTimeOutEvents.FocusedRowHandle >= 0)
                gvTimeOutEvents.DeleteRow(gvTimeOutEvents.FocusedRowHandle);

            PP.SetModified();
        }

        private string LoadOpenedPeriodEventImage()
        {

            if (FocusedEvent == null) return "";

            var name = Guid.NewGuid().ToString("N");

            var cid = new OpenFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                CheckFileExists = true,
                Filter = @"Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;",
                Multiselect = false
            };
            if (cid.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var selImg = new FileInfo(cid.FileName);
                    var toImg = new FileInfo(Path.Combine(PP.ProjectLocalTimeOutImageFolder, name + selImg.Extension));

                    if (!toImg.Directory.Exists)
                        toImg.Directory.Create();

                    File.Copy(selImg.FullName, toImg.FullName, true);

                    FocusedEvent.ObjectPath = name + selImg.Extension;

                    gvTimeOutEvents.RefreshData();

                    PP.SetModified();

                    return FocusedEvent.ObjectPath;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Не удалось выбрать изображение: {ex.Message}");
                }
            }
            return "";
        }

        private string LoadOpenedPeriodEventSound()
        {
            if (FocusedEvent == null) return "";

            var name = Guid.NewGuid().ToString("N");

            var cid = new OpenFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                CheckFileExists = true,
                Filter = @"Audio Files|*.mp3;*.wav;*.;*.wma;",
                Multiselect = false
            };
            if (cid.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var selImg = new FileInfo(cid.FileName);
                    var toImg = new FileInfo(Path.Combine(PP.ProjectLocalTimeOutSoundFolder, name + selImg.Extension));

                    if (!toImg.Directory.Exists)
                        toImg.Directory.Create();

                    File.Copy(selImg.FullName, toImg.FullName, true);

                    
                    FocusedEvent.ObjectPath = name + selImg.Extension;

                    gvTimeOutEvents.RefreshData();

                    PP.SetModified();

                    return FocusedEvent.ObjectPath;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Не удалось выбрать аудиозапись: {ex.Message}");
                }
            }
            return "";
        }

        private void bbiPlaySound_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (FocusedEvent == null || FocusedEvent?.PeriodEventType != EPeriodEventType.Sound)
                return;

            WindowsMediaPlayerHelper.PlayAsync(GetSoundEventPath());
        }

        public string GetSoundEventPath()
        {
            return Path.Combine(PP.ProjectLocalTimeOutSoundFolder, FocusedEvent.ObjectPath);
        }

        private void OnPeriodEventListFocusedRowChanged()
        {
            if (FocusedEvent != null)
            {

                switch (FocusedEvent.PeriodEventType)
                {
                    case EPeriodEventType.Sound:
                        picEventObject.Image = (Image)Resources.ResourceManager.GetObject("AudioFile");
                        break;

                    case EPeriodEventType.BackgroundImage:
                        picEventObject.Image = FocusedEvent.GetImage(PP.ProjectLocalEventImageFolder);
                        break;
                }
            }
            else
            {
                picEventObject.Image = null;
            }
            UpdateUIStatus();
        }

        private void gvTimeOutEvents_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            OnPeriodEventListFocusedRowChanged();
        }

        private void tsDefaultTimeOutTime_EditValueChanged(object sender, EventArgs e)
        {
            PP.DefaultTimeOutTime = tsDefaultTimeOutTime.TimeSpan;
        }

        private void pmTimeOut_BeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bbiDelete.Enabled = FocusedEvent != null;
            bbiEdit.Enabled = FocusedEvent != null;

            bbiPlaySound.Enabled = FocusedEvent?.PeriodEventType == EPeriodEventType.Sound && 
                !string.IsNullOrEmpty(FocusedEvent.ObjectPath);
        }

        private void ProjectEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            WindowsMediaPlayerHelper.Stop();
        }

        private void riChoseFile2_Click(object sender, EventArgs e)
        {
            var val = "";
            if (FocusedEvent.PeriodEventType == EPeriodEventType.BackgroundImage)
                val = LoadOpenedPeriodEventImage();
            else if (FocusedEvent.PeriodEventType == EPeriodEventType.Sound)
                val = LoadOpenedPeriodEventSound();

            OnPeriodEventListFocusedRowChanged();
            if (val != "")
                (sender as ButtonEdit).EditValue = val;
        }

        private void ProjectEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
