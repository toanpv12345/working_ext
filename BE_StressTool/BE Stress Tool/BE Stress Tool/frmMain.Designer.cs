namespace BE_Stress_Tool
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tbarMain = new System.Windows.Forms.ToolStrip();
            this.tbarAdd = new System.Windows.Forms.ToolStripButton();
            this.tbarRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbarConnectionLbl = new System.Windows.Forms.ToolStripLabel();
            this.tbarConnectionTxt = new System.Windows.Forms.ToolStripTextBox();
            this.tbarLoopLbl = new System.Windows.Forms.ToolStripLabel();
            this.tbarLoopTxt = new System.Windows.Forms.ToolStripTextBox();
            this.tbarStart = new System.Windows.Forms.ToolStripButton();
            this.tbarStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tbarSetting = new System.Windows.Forms.ToolStripButton();
            this.tbarTest = new System.Windows.Forms.ToolStripButton();
            this.listFile = new System.Windows.Forms.ListBox();
            this.gridFileStep = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.tbarMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFileStep)).BeginInit();
            this.SuspendLayout();
            // 
            // tbarMain
            // 
            this.tbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbarAdd,
            this.tbarRemove,
            this.toolStripSeparator2,
            this.tbarConnectionLbl,
            this.tbarConnectionTxt,
            this.tbarLoopLbl,
            this.tbarLoopTxt,
            this.tbarStart,
            this.tbarStop,
            this.toolStripSeparator4,
            this.tbarSetting,
            this.tbarTest});
            this.tbarMain.Location = new System.Drawing.Point(0, 0);
            this.tbarMain.Name = "tbarMain";
            this.tbarMain.Size = new System.Drawing.Size(773, 25);
            this.tbarMain.TabIndex = 0;
            this.tbarMain.Text = "Toolbar for Main";
            // 
            // tbarAdd
            // 
            this.tbarAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbarAdd.Image = ((System.Drawing.Image)(resources.GetObject("tbarAdd.Image")));
            this.tbarAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbarAdd.Name = "tbarAdd";
            this.tbarAdd.Size = new System.Drawing.Size(23, 22);
            this.tbarAdd.Text = "Add File";
            this.tbarAdd.Click += new System.EventHandler(this.tbarAdd_Click);
            // 
            // tbarRemove
            // 
            this.tbarRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbarRemove.Image = ((System.Drawing.Image)(resources.GetObject("tbarRemove.Image")));
            this.tbarRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbarRemove.Name = "tbarRemove";
            this.tbarRemove.Size = new System.Drawing.Size(23, 22);
            this.tbarRemove.Text = "tbarRemove";
            this.tbarRemove.Click += new System.EventHandler(this.tbarRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tbarConnectionLbl
            // 
            this.tbarConnectionLbl.Name = "tbarConnectionLbl";
            this.tbarConnectionLbl.Size = new System.Drawing.Size(69, 22);
            this.tbarConnectionLbl.Text = "Connection";
            // 
            // tbarConnectionTxt
            // 
            this.tbarConnectionTxt.Name = "tbarConnectionTxt";
            this.tbarConnectionTxt.Size = new System.Drawing.Size(100, 25);
            this.tbarConnectionTxt.Text = "10";
            // 
            // tbarLoopLbl
            // 
            this.tbarLoopLbl.Name = "tbarLoopLbl";
            this.tbarLoopLbl.Size = new System.Drawing.Size(34, 22);
            this.tbarLoopLbl.Text = "Loop";
            // 
            // tbarLoopTxt
            // 
            this.tbarLoopTxt.Name = "tbarLoopTxt";
            this.tbarLoopTxt.Size = new System.Drawing.Size(100, 25);
            this.tbarLoopTxt.Text = "10";
            // 
            // tbarStart
            // 
            this.tbarStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbarStart.Image = ((System.Drawing.Image)(resources.GetObject("tbarStart.Image")));
            this.tbarStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbarStart.Name = "tbarStart";
            this.tbarStart.Size = new System.Drawing.Size(23, 22);
            this.tbarStart.Text = "Start";
            this.tbarStart.Click += new System.EventHandler(this.tbarStart_Click);
            // 
            // tbarStop
            // 
            this.tbarStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbarStop.Image = ((System.Drawing.Image)(resources.GetObject("tbarStop.Image")));
            this.tbarStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbarStop.Name = "tbarStop";
            this.tbarStop.Size = new System.Drawing.Size(23, 22);
            this.tbarStop.Text = "Stop";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tbarSetting
            // 
            this.tbarSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbarSetting.Image = global::BE_Stress_Tool.Properties.Resources.setting;
            this.tbarSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbarSetting.Name = "tbarSetting";
            this.tbarSetting.Size = new System.Drawing.Size(23, 22);
            this.tbarSetting.Text = "Setting";
            this.tbarSetting.Click += new System.EventHandler(this.tbarSetting_Click);
            // 
            // tbarTest
            // 
            this.tbarTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbarTest.Image = ((System.Drawing.Image)(resources.GetObject("tbarTest.Image")));
            this.tbarTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbarTest.Name = "tbarTest";
            this.tbarTest.Size = new System.Drawing.Size(33, 22);
            this.tbarTest.Text = "Test";
            this.tbarTest.Click += new System.EventHandler(this.tbarTest_Click);
            // 
            // listFile
            // 
            this.listFile.FormattingEnabled = true;
            this.listFile.Location = new System.Drawing.Point(0, 28);
            this.listFile.Name = "listFile";
            this.listFile.ScrollAlwaysVisible = true;
            this.listFile.Size = new System.Drawing.Size(180, 498);
            this.listFile.TabIndex = 1;
            this.listFile.SelectedIndexChanged += new System.EventHandler(this.listFile_SelectedIndexChanged);
            this.listFile.DoubleClick += new System.EventHandler(this.listFile_DoubleClick);
            // 
            // gridFileStep
            // 
            this.gridFileStep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFileStep.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colCommand,
            this.colData,
            this.colResult});
            this.gridFileStep.Location = new System.Drawing.Point(186, 28);
            this.gridFileStep.Name = "gridFileStep";
            this.gridFileStep.Size = new System.Drawing.Size(583, 498);
            this.gridFileStep.TabIndex = 3;
            // 
            // colSTT
            // 
            this.colSTT.HeaderText = "No";
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 40;
            // 
            // colCommand
            // 
            this.colCommand.HeaderText = "Command";
            this.colCommand.Name = "colCommand";
            this.colCommand.Width = 200;
            // 
            // colData
            // 
            this.colData.HeaderText = "Data";
            this.colData.Name = "colData";
            this.colData.Width = 200;
            // 
            // colResult
            // 
            this.colResult.HeaderText = "Result";
            this.colResult.Name = "colResult";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(0, 532);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(769, 212);
            this.txtStatus.TabIndex = 4;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 749);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.gridFileStep);
            this.Controls.Add(this.listFile);
            this.Controls.Add(this.tbarMain);
            this.Name = "frmMain";
            this.Text = "BE Stress Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.tbarMain.ResumeLayout(false);
            this.tbarMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridFileStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tbarMain;
        private System.Windows.Forms.ListBox listFile;
        private System.Windows.Forms.ToolStripButton tbarStart;
        private System.Windows.Forms.ToolStripButton tbarStop;
        private System.Windows.Forms.ToolStripButton tbarAdd;
        private System.Windows.Forms.ToolStripButton tbarRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tbarConnectionLbl;
        private System.Windows.Forms.ToolStripTextBox tbarConnectionTxt;
        private System.Windows.Forms.ToolStripLabel tbarLoopLbl;
        private System.Windows.Forms.ToolStripTextBox tbarLoopTxt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tbarSetting;
        private System.Windows.Forms.DataGridView gridFileStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn colData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResult;
        private System.Windows.Forms.ToolStripButton tbarTest;
        private System.Windows.Forms.TextBox txtStatus;
    }
}

