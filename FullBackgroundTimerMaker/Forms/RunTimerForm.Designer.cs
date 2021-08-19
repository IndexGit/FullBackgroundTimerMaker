namespace FullBackgroundTimerMaker.Forms
{
    partial class RunTimerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunTimerForm));
            this.lTimer = new System.Windows.Forms.Label();
            this.lcControls = new DevExpress.XtraLayout.LayoutControl();
            this.tsTimeOut = new DevExpress.XtraEditors.TimeSpanEdit();
            this.bTimeOut = new DevExpress.XtraEditors.SimpleButton();
            this.bReset = new DevExpress.XtraEditors.SimpleButton();
            this.bNextTimer = new DevExpress.XtraEditors.SimpleButton();
            this.bRunPause = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollectionRunPause = new DevExpress.Utils.ImageCollection(this.components);
            this.bRestartTimer = new DevExpress.XtraEditors.SimpleButton();
            this.bPrevTimer = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lTimeOutTimer = new System.Windows.Forms.Label();
            this.timerShowControlBar = new System.Windows.Forms.Timer(this.components);
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.lPeriodName = new System.Windows.Forms.Label();
            this.timeoutTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lcControls)).BeginInit();
            this.lcControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsTimeOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionRunPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // lTimer
            // 
            this.lTimer.AutoSize = true;
            this.lTimer.BackColor = System.Drawing.Color.Transparent;
            this.lTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lTimer.Location = new System.Drawing.Point(386, 187);
            this.lTimer.Name = "lTimer";
            this.lTimer.Size = new System.Drawing.Size(347, 91);
            this.lTimer.TabIndex = 0;
            this.lTimer.Text = "00:00:05";
            // 
            // lcControls
            // 
            this.lcControls.BackColor = System.Drawing.Color.Transparent;
            this.lcControls.Controls.Add(this.tsTimeOut);
            this.lcControls.Controls.Add(this.bTimeOut);
            this.lcControls.Controls.Add(this.bReset);
            this.lcControls.Controls.Add(this.bNextTimer);
            this.lcControls.Controls.Add(this.bRunPause);
            this.lcControls.Controls.Add(this.bRestartTimer);
            this.lcControls.Controls.Add(this.bPrevTimer);
            this.lcControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lcControls.Location = new System.Drawing.Point(0, 379);
            this.lcControls.Margin = new System.Windows.Forms.Padding(0);
            this.lcControls.Name = "lcControls";
            this.lcControls.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(653, 227, 450, 400);
            this.lcControls.Root = this.layoutControlGroup1;
            this.lcControls.Size = new System.Drawing.Size(1105, 88);
            this.lcControls.TabIndex = 1;
            this.lcControls.Text = "layoutControl1";
            // 
            // tsTimeOut
            // 
            this.tsTimeOut.EditValue = System.TimeSpan.Parse("00:05:00");
            this.tsTimeOut.Location = new System.Drawing.Point(632, 44);
            this.tsTimeOut.MinimumSize = new System.Drawing.Size(0, 38);
            this.tsTimeOut.Name = "tsTimeOut";
            this.tsTimeOut.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.tsTimeOut.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.tsTimeOut.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.tsTimeOut.Properties.Appearance.Options.UseBackColor = true;
            this.tsTimeOut.Properties.Appearance.Options.UseFont = true;
            this.tsTimeOut.Properties.Appearance.Options.UseForeColor = true;
            this.tsTimeOut.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.tsTimeOut.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tsTimeOut.Properties.Mask.EditMask = "dd.HH:mm:ss";
            this.tsTimeOut.Size = new System.Drawing.Size(188, 42);
            this.tsTimeOut.StyleController = this.lcControls;
            this.tsTimeOut.TabIndex = 2;
            // 
            // bTimeOut
            // 
            this.bTimeOut.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.bTimeOut.Appearance.Options.UseFont = true;
            this.bTimeOut.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bTimeOut.ImageOptions.Image")));
            this.bTimeOut.Location = new System.Drawing.Point(483, 44);
            this.bTimeOut.Name = "bTimeOut";
            this.bTimeOut.Size = new System.Drawing.Size(145, 38);
            this.bTimeOut.StyleController = this.lcControls;
            this.bTimeOut.TabIndex = 3;
            this.bTimeOut.Text = "ТАЙМ-АУТ!";
            this.bTimeOut.Click += new System.EventHandler(this.bTimeOut_Click);
            // 
            // bReset
            // 
            this.bReset.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.bReset.Appearance.Options.UseFont = true;
            this.bReset.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bReset.ImageOptions.Image")));
            this.bReset.Location = new System.Drawing.Point(824, 2);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(189, 38);
            this.bReset.StyleController = this.lcControls;
            this.bReset.TabIndex = 7;
            this.bReset.Text = "Полный сброс";
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bNextTimer
            // 
            this.bNextTimer.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.bNextTimer.Appearance.Options.UseFont = true;
            this.bNextTimer.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bNextTimer.ImageOptions.Image")));
            this.bNextTimer.Location = new System.Drawing.Point(632, 2);
            this.bNextTimer.Name = "bNextTimer";
            this.bNextTimer.Size = new System.Drawing.Size(188, 38);
            this.bNextTimer.StyleController = this.lcControls;
            this.bNextTimer.TabIndex = 4;
            this.bNextTimer.Text = "Следующий таймер";
            this.bNextTimer.Click += new System.EventHandler(this.bNextTimer_Click);
            // 
            // bRunPause
            // 
            this.bRunPause.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.bRunPause.Appearance.Options.UseFont = true;
            this.bRunPause.ImageOptions.ImageIndex = 0;
            this.bRunPause.ImageOptions.ImageList = this.imageCollectionRunPause;
            this.bRunPause.Location = new System.Drawing.Point(483, 2);
            this.bRunPause.Name = "bRunPause";
            this.bRunPause.Size = new System.Drawing.Size(145, 38);
            this.bRunPause.StyleController = this.lcControls;
            this.bRunPause.TabIndex = 1;
            this.bRunPause.Text = "ЗАПУСК >>>";
            this.bRunPause.Click += new System.EventHandler(this.bRunPause_Click);
            // 
            // imageCollectionRunPause
            // 
            this.imageCollectionRunPause.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollectionRunPause.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionRunPause.ImageStream")));
            this.imageCollectionRunPause.InsertGalleryImage("play_32x32.png", "images/arrows/play_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/play_32x32.png"), 0);
            this.imageCollectionRunPause.Images.SetKeyName(0, "play_32x32.png");
            this.imageCollectionRunPause.InsertGalleryImage("pause_32x32.png", "images/arrows/pause_32x32.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/arrows/pause_32x32.png"), 1);
            this.imageCollectionRunPause.Images.SetKeyName(1, "pause_32x32.png");
            // 
            // bRestartTimer
            // 
            this.bRestartTimer.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.bRestartTimer.Appearance.Options.UseFont = true;
            this.bRestartTimer.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bRestartTimer.ImageOptions.Image")));
            this.bRestartTimer.Location = new System.Drawing.Point(288, 2);
            this.bRestartTimer.Name = "bRestartTimer";
            this.bRestartTimer.Size = new System.Drawing.Size(191, 38);
            this.bRestartTimer.StyleController = this.lcControls;
            this.bRestartTimer.TabIndex = 5;
            this.bRestartTimer.Text = "В начало таймера";
            this.bRestartTimer.Click += new System.EventHandler(this.bRestartTimer_Click);
            // 
            // bPrevTimer
            // 
            this.bPrevTimer.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.bPrevTimer.Appearance.Options.UseFont = true;
            this.bPrevTimer.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bPrevTimer.ImageOptions.Image")));
            this.bPrevTimer.Location = new System.Drawing.Point(80, 2);
            this.bPrevTimer.Name = "bPrevTimer";
            this.bPrevTimer.Size = new System.Drawing.Size(204, 38);
            this.bPrevTimer.StyleController = this.lcControls;
            this.bPrevTimer.TabIndex = 6;
            this.bPrevTimer.Text = "Предыдущий таймер";
            this.bPrevTimer.Click += new System.EventHandler(this.bPrevTimer_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1105, 88);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.bPrevTimer;
            this.layoutControlItem1.Location = new System.Drawing.Point(78, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(208, 88);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.bRestartTimer;
            this.layoutControlItem2.Location = new System.Drawing.Point(286, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(195, 88);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(78, 88);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(1015, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(90, 88);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.bRunPause;
            this.layoutControlItem3.Location = new System.Drawing.Point(481, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(149, 42);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.bNextTimer;
            this.layoutControlItem4.Location = new System.Drawing.Point(630, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(192, 42);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.bReset;
            this.layoutControlItem5.Location = new System.Drawing.Point(822, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(193, 88);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.bTimeOut;
            this.layoutControlItem6.Location = new System.Drawing.Point(481, 42);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(149, 46);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.tsTimeOut;
            this.layoutControlItem7.Location = new System.Drawing.Point(630, 42);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(192, 46);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // lTimeOutTimer
            // 
            this.lTimeOutTimer.AutoSize = true;
            this.lTimeOutTimer.BackColor = System.Drawing.Color.Transparent;
            this.lTimeOutTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lTimeOutTimer.ForeColor = System.Drawing.Color.Red;
            this.lTimeOutTimer.Location = new System.Drawing.Point(386, 278);
            this.lTimeOutTimer.Name = "lTimeOutTimer";
            this.lTimeOutTimer.Size = new System.Drawing.Size(347, 91);
            this.lTimeOutTimer.TabIndex = 2;
            this.lTimeOutTimer.Text = "00:00:05";
            this.lTimeOutTimer.Visible = false;
            // 
            // timerShowControlBar
            // 
            this.timerShowControlBar.Enabled = true;
            this.timerShowControlBar.Interval = 1000;
            this.timerShowControlBar.Tick += new System.EventHandler(this.timerShowControlBar_Tick);
            // 
            // mainTimer
            // 
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // lPeriodName
            // 
            this.lPeriodName.AutoSize = true;
            this.lPeriodName.BackColor = System.Drawing.Color.Transparent;
            this.lPeriodName.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lPeriodName.ForeColor = System.Drawing.Color.White;
            this.lPeriodName.Location = new System.Drawing.Point(12, 9);
            this.lPeriodName.Name = "lPeriodName";
            this.lPeriodName.Size = new System.Drawing.Size(325, 76);
            this.lPeriodName.TabIndex = 3;
            this.lPeriodName.Text = "Таймер 1";
            // 
            // timeoutTimer
            // 
            this.timeoutTimer.Tick += new System.EventHandler(this.timeuotTimer_Tick);
            // 
            // RunTimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1105, 467);
            this.Controls.Add(this.lPeriodName);
            this.Controls.Add(this.lTimeOutTimer);
            this.Controls.Add(this.lcControls);
            this.Controls.Add(this.lTimer);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "RunTimerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "cproc.ru - Таймер!";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RunTimerForm_FormClosing);
            this.Shown += new System.EventHandler(this.RunTimerForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RunTimerForm_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RunTimerForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.lcControls)).EndInit();
            this.lcControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tsTimeOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionRunPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lTimer;
        private DevExpress.XtraLayout.LayoutControl lcControls;
        private DevExpress.XtraEditors.TimeSpanEdit tsTimeOut;
        private DevExpress.XtraEditors.SimpleButton bTimeOut;
        private DevExpress.XtraEditors.SimpleButton bReset;
        private DevExpress.XtraEditors.SimpleButton bNextTimer;
        private DevExpress.XtraEditors.SimpleButton bRunPause;
        private DevExpress.XtraEditors.SimpleButton bRestartTimer;
        private DevExpress.XtraEditors.SimpleButton bPrevTimer;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private System.Windows.Forms.Label lTimeOutTimer;
        private System.Windows.Forms.Timer timerShowControlBar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.Utils.ImageCollection imageCollectionRunPause;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.Label lPeriodName;
        private System.Windows.Forms.Timer timeoutTimer;
    }
}