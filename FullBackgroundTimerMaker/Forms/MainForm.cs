using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using TimerClases.Objects;
using TimerClases.Objects.Enums;
using FullBackgroundTimerMaker.Properties;
using TimerClases.Objects.Helpers;

namespace FullBackgroundTimerMaker.Forms
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private PeriodProject PP;

        private string _saveProjectPath;

        /// <summary>
        /// Выбранный период
        /// </summary>
        private PeriodObject FocusedPeriodObject => (gvPeriodObjectList.GetRow(gvPeriodObjectList.FocusedRowHandle) as PeriodObject);

        /// <summary>
        /// Вьюха событий
        /// </summary>
        private GridView PeriodEvenView => (gvPeriodObjectList?.GetDetailView(gvPeriodObjectList.FocusedRowHandle,
                                            gvPeriodObjectList.DefaultRelationIndex) as GridView);

        /// <summary>
        /// Выбранное событие периода
        /// </summary>
        private PeriodEvent FocusedEvent => PeriodEvenView?.GetRow(PeriodEvenView.FocusedRowHandle) as PeriodEvent;

        /// <summary>
        /// Пометить проект как изменённый
        /// </summary>
        /// <param name="SetModified">Пометить</param>
        /// <param name="Modified">Пометить или снять</param>
        private void ModifyPP(bool SetModified = true, bool Modified = true)
        {
            if(SetModified)
                PP.SetModified(Modified);
            UpdateUIStatus();
        }
        public MainForm()
        {
            InitializeComponent();

            UpdateUIStatus();

            _saveProjectPath = Path.Combine((new FileInfo(Assembly.GetExecutingAssembly().FullName)).Directory.FullName,
                "Saves");

            var di = new DirectoryInfo(_saveProjectPath);
            if(!di.Exists) di.Create();
        }

        void UpdateStatusBar()
        {
            bsiRecordsCount.Caption = @"Периодов : " +

                                      ((BindingList<PeriodObject>) gcObjectPeriodList.DataSource)?.Count;

        }


        public BindingList<PeriodObject> GetDataSourcePeriods()
        {
            return PP?.PeriodObjects;
        }


        private void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvPeriodObjectList.FocusedRowHandle >= 0)
            {
                gvPeriodObjectList.ShowEditForm();
                ModifyPP();
            }           
        }


        private string LoadOpenedPeriodImage()
        {
            if (FocusedPeriodObject == null) return "";

            if (string.IsNullOrEmpty(FocusedPeriodObject?.PeriodName))
                throw new Exception("Необходимо вначале заполнить имя периода!");

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
                    var toImg = new FileInfo(Path.Combine(PP.ProjectLocalImageFolder, FocusedPeriodObject.PeriodName + selImg.Extension));

                    if (!toImg.Directory.Exists)
                        toImg.Directory.Create();

                    File.Copy(selImg.FullName, toImg.FullName, true);

                    FocusedPeriodObject.BackGroundImage = FocusedPeriodObject.PeriodName + selImg.Extension;

                    gvPeriodObjectList.RefreshData();
                    ModifyPP();
                    OnPeriodObjectFocusedRowChanged();

                    return FocusedPeriodObject.BackGroundImage;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Не удалось выбрать изображение: {ex.Message}");
                }
            }
            return "";
        }

        private string LoadOpenedPeriodEventImage()
        {

            if(FocusedEvent == null) return "";

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
                    var toImg = new FileInfo(Path.Combine(PP.ProjectLocalEventImageFolder, name + selImg.Extension));

                    if (!toImg.Directory.Exists)
                        toImg.Directory.Create();

                    File.Copy(selImg.FullName, toImg.FullName, true);

                    FocusedEvent.ObjectPath = name + selImg.Extension;

                    PeriodEvenView.RefreshData();

                    ModifyPP();

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
                    var toImg = new FileInfo(Path.Combine(PP.ProjectLocalEventSoundFolder, name + selImg.Extension));

                    if (!toImg.Directory.Exists)
                        toImg.Directory.Create();

                    File.Copy(selImg.FullName, toImg.FullName, true);

                    FocusedEvent.ObjectPath = name + selImg.Extension;

                    PeriodEvenView.RefreshData();
                    ModifyPP();

                    return FocusedEvent.ObjectPath;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Не удалось выбрать аудиозапись: {ex.Message}");
                }
            }
            return "";
        }

        /// <summary>
        /// Спросить надо ли сохранять проект
        /// </summary>
        /// <returns></returns>
        private bool AskSaveProject()
        {
            if (PP != null && PP.Modified)
            {
                var dr = MessageBox.Show(this, @"Сохранить текущий проект?", @"Текущие данные могут быть утеряны",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    SaveProject();
                }

                if (dr == DialogResult.Cancel)
                    return false;

            }

            return true;
        }

        private void bbiNewProject_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!AskSaveProject()) return;

            var pef = new ProjectEditForm();
            if (pef.ShowDialog(this) == DialogResult.OK)

            {
                PP = pef.PP;
                UpdateUIStatus();
                PP.SetPeriodObjectsCounterValues();
                gcObjectPeriodList.DataSource = GetDataSourcePeriods();
            }


        }

        private void bbiOpenProject_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!AskSaveProject()) return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();

                var ofd = new OpenFileDialog()
                {
                    AddExtension = true,
                    CheckPathExists = true,
                    CheckFileExists = true,
                    Filter = @"Cproc timer(*.cproc_timer)|*.cproc_timer",
                    DefaultExt = "cproc_timer",
                    Multiselect = false,
                    InitialDirectory = _saveProjectPath
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PP = PeriodProject.LoadFromFile(ofd.FileName);
                        _saveProjectPath = (new FileInfo(ofd.FileName).Directory.FullName);
                    }
                    catch (Exception ex)
                    {
                        ErrorForm.Show("Ошибка открытия проекта",ex);
                    }
                }
                gcObjectPeriodList.DataSource = GetDataSourcePeriods();

                UpdateUIStatus();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void bbiSaveProject_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();

                if (PP != null)
                {
                    SaveProject();
                }

                UpdateUIStatus();
            }
            catch (Exception ex)
            {
                ErrorForm.Show("Ошибка при сохранении проекта",ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private bool SaveProject()
        {
            var sfd = new SaveFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                Filter = @"Cproc timer(*.cproc_timer)|*.cproc_timer",
                DefaultExt = "cproc_timer",
                FileName = PP.Name,
                InitialDirectory = _saveProjectPath
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    WindowsMediaPlayerHelper.Close();
                    PP.SaveToFile(sfd.FileName);
                    _saveProjectPath = (new FileInfo(sfd.FileName).Directory.FullName);
                }
                catch (Exception e)
                {
                    ErrorForm.Show("Ошибка сохранения проекта", e);
                }

                var saved = sfd.FileNames.Any();
                if (saved) saved = File.Exists(sfd.FileNames[0]);

                ModifyPP(saved, false);

                return saved;
            }

            return false;
        }



        private void UpdateUIStatus()
        {
            if (PP == null)
            {
                bbiSaveProject.Enabled = false;
                rpgPeriod.Enabled = false;
                rpgEvent.Enabled = false;
                bbiEditProjectParams.Enabled = false;
                bbiRun.Enabled = false;

                bbiAddEvent.Enabled = false;
                bbiPeriodOrderUp.Enabled = false;
                bbiPeriodOrderDown.Enabled = false;

                bbiEventPlaySound.Enabled = false;
            }
            else
            {
                bbiSaveProject.Enabled = PP.Modified;
                rpgPeriod.Enabled = true;

                var PeriodSelected = gvPeriodObjectList.FocusedRowHandle >= 0;
                var EventSelected = PeriodEvenView?.FocusedRowHandle >= 0;

                rpgEvent.Enabled = PeriodSelected;
                bbiEditEvent.Enabled = EventSelected;
                bbiDeleteEvent.Enabled = EventSelected;


                bbiEditPeriod.Enabled = PeriodSelected;
                bbiDeletePeriod.Enabled = PeriodSelected;

                bbiEditProjectParams.Enabled = true;
                bbiRun.Enabled = true;

                bbiAddEvent.Enabled = PeriodSelected;
                bbiPeriodOrderUp.Enabled = PeriodSelected;
                bbiPeriodOrderDown.Enabled = PeriodSelected;

                bbiEventPlaySound.Enabled = EventSelected && (FocusedEvent?.PeriodEventType == EPeriodEventType.Sound) && (gcObjectPeriodList.FocusedView.Name == "gvPeriodEventList");
            }

            UpdateStatusBar();
        }

        private void bbiNewPeriod_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvPeriodObjectList.AddNewRow();
            gvPeriodObjectList.ShowEditForm();

            ModifyPP();
        }

        private void bbiEditProjectParams_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (PP == null) return;

            var pef = new ProjectEditForm(PP);
            if (pef.ShowDialog(this) == DialogResult.OK)
            {
                PP = pef.PP;
                UpdateUIStatus();
            }
        }

        private void gvPeriodObjectList_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            OnPeriodObjectFocusedRowChanged();
            ModifyPP();
        }

        private void gvPeriodObjectList_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            ModifyPP();
        }

        private void bbiAddEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            FocusedPeriodObject.PeriodEvents.AddNew();
            gvPeriodObjectList.SetMasterRowExpanded(gvPeriodObjectList.FocusedRowHandle, true);
            PeriodEvenView.ShowEditForm();
            ModifyPP();
        }

        private void bbiEditEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (PeriodEvenView.FocusedRowHandle >= 0)
            {
                PeriodEvenView.ShowEditForm();
                ModifyPP();
            }


        }

        private void gvPeriodEventList_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            (sender as GridView).ShowEditForm();
        }

        private void ExpandAllRows(GridView gv)
        {
            gv.BeginUpdate();
            try
            {

                for (int i = 0; i < gv.DataRowCount; i++)
                    gv.SetMasterRowExpanded(i, true);
            }
            finally
            {
                gv.EndUpdate();
            }
        }

        private void gvPeriodObjectList_InitNewRow(object sender, InitNewRowEventArgs e)
        {

        }



        private void bbiDeletePeriod_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvPeriodObjectList.FocusedRowHandle >= 0)
                gvPeriodObjectList.DeleteRow(gvPeriodObjectList.FocusedRowHandle);

            ModifyPP();
        }

        private void bbiDeleteEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (PeriodEvenView.FocusedRowHandle >= 0)
                PeriodEvenView.DeleteRow(PeriodEvenView.FocusedRowHandle);

            ModifyPP();
        }

        private void bbiExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!AskSaveProject()) return;

            Application.Exit();
        }

        private void bbiAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            var af = new AboutForm();
            af.ShowDialog(this);
        }

        private void bbiRun_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(!AskSaveProject()) return;

            WindowsMediaPlayerHelper.Stop();
            var rtf = new RunTimerForm(PP);
            rtf.ShowDialog(this);
        }

        private void bbiPeriodOrderUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            var prevRow = (gvPeriodObjectList.GetRow(gvPeriodObjectList.FocusedRowHandle - 1) as PeriodObject);

            if (FocusedPeriodObject == null || prevRow == null) return;

            var fro = FocusedPeriodObject.Order;
            FocusedPeriodObject.Order = prevRow.Order;
            prevRow.Order = fro;

            PP.PeriodObjects = new BindingList<PeriodObject>(PP.PeriodObjects.OrderBy(p => p.Order).ToList());

            gvPeriodObjectList.RefreshData();
            ModifyPP();
        }

        private void bbiPeriodOrderDown_ItemClick(object sender, ItemClickEventArgs e)
        {
            var nextRow = (gvPeriodObjectList.GetRow(gvPeriodObjectList.FocusedRowHandle + 1) as PeriodObject);

            if (FocusedPeriodObject == null || nextRow == null) return;

            var fro = FocusedPeriodObject.Order;
            FocusedPeriodObject.Order = nextRow.Order;
            nextRow.Order = fro;

            PP.PeriodObjects = new BindingList<PeriodObject>(PP.PeriodObjects.OrderBy(p => p.Order).ToList());

            gvPeriodObjectList.RefreshData();
            ModifyPP();
        }

        private void gvPeriodObjectList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            OnPeriodObjectFocusedRowChanged();
        }

        private void OnPeriodObjectFocusedRowChanged()
        {
            if (FocusedPeriodObject?.BackGroundImage != null)
                picPeriodObject.Image = FocusedPeriodObject.GetBackGroundImage(PP.ProjectLocalImageFolder);
            else
            {
                picPeriodObject.Image = null;
            }
            UpdateUIStatus();
        }

        private void gvPeriodEventList_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(gcObjectPeriodList.FocusedView.Name == "gvPeriodEventList")
                OnPeriodEventListFocusedRowChanged();
        }

        private void OnPeriodEventListFocusedRowChanged()
        {
            if (FocusedEvent != null)
            {

                switch (FocusedEvent.PeriodEventType)
                {
                    case EPeriodEventType.Sound:
                        picPeriodObject.Image = (Image)Resources.ResourceManager.GetObject("AudioFile");
                        break;

                    case EPeriodEventType.BackgroundImage:
                        picPeriodObject.Image = FocusedEvent.GetImage(PP.ProjectLocalEventImageFolder);
                        break;
                }
            }
            else
            {
                picPeriodObject.Image = null;
            }
            UpdateUIStatus();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(PP == null) return;

            if (PP.Modified)
                if (!AskSaveProject())
                {
                    e.Cancel = true;
                    return;                    
                }

            try
            {
                var tmpDir = new DirectoryInfo(PP.ProjectLocalFolder).Parent;
                if (tmpDir != null && tmpDir.Exists)
                    tmpDir.Delete(true);
            }
            catch
            {
                //
            }
        }

        private void gvPeriodEventList_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            ModifyPP();
        }

        private void gvPeriodEventList_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            OnPeriodEventListFocusedRowChanged();
            ModifyPP();
        }

        private void bbiEventPlaySound_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(FocusedEvent == null || FocusedEvent?.PeriodEventType != EPeriodEventType.Sound)
                return;

            FocusedEvent.PlaySound(PP.ProjectLocalEventSoundFolder);
        }

        private void gcObjectPeriodList_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            if(e.View == null) return;
            
            if (e.View.Name == "gvPeriodObjectList")
            {
                OnPeriodObjectFocusedRowChanged();
            }
            else if (e.View.Name == "gvPeriodEventList")
            {
                OnPeriodEventListFocusedRowChanged();
            }

        }

        private void riChooseFile2_Click(object sender, EventArgs e)
        {
            var gvName = gcObjectPeriodList.FocusedView.Name;

            var val = "";

            if (gvName == "gvPeriodObjectList")
                val = LoadOpenedPeriodImage();
            else if (gvName == "gvPeriodEventList")
            {
                if (FocusedEvent.PeriodEventType == EPeriodEventType.BackgroundImage)
                    val = LoadOpenedPeriodEventImage();
                else if (FocusedEvent.PeriodEventType == EPeriodEventType.Sound)
                    val = LoadOpenedPeriodEventSound();

                OnPeriodEventListFocusedRowChanged();
            }
            if (val != "")
                (sender as ButtonEdit).EditValue = val;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (AskSaveProject())
                    Close();
            }
        }
    }
}