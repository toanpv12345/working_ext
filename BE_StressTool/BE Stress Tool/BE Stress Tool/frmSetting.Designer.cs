namespace BE_Stress_Tool
{
    partial class frmSetting
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
            this.tabAll = new System.Windows.Forms.TabControl();
            this.tabSocket = new System.Windows.Forms.TabPage();
            this.gridSocket = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabCONNECT = new System.Windows.Forms.TabPage();
            this.gridCONNECT = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabSHAKE = new System.Windows.Forms.TabPage();
            this.gridSHAKE = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabCHECKIN = new System.Windows.Forms.TabPage();
            this.tabCOMMIT = new System.Windows.Forms.TabPage();
            this.tabROLLBACK = new System.Windows.Forms.TabPage();
            this.tabTERMINATE = new System.Windows.Forms.TabPage();
            this.tabCHARGE = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabAll.SuspendLayout();
            this.tabSocket.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSocket)).BeginInit();
            this.tabCONNECT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCONNECT)).BeginInit();
            this.tabSHAKE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSHAKE)).BeginInit();
            this.tabCHECKIN.SuspendLayout();
            this.tabCOMMIT.SuspendLayout();
            this.tabROLLBACK.SuspendLayout();
            this.tabTERMINATE.SuspendLayout();
            this.tabCHARGE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.SuspendLayout();
            // 
            // tabAll
            // 
            this.tabAll.Controls.Add(this.tabSocket);
            this.tabAll.Controls.Add(this.tabCONNECT);
            this.tabAll.Controls.Add(this.tabSHAKE);
            this.tabAll.Controls.Add(this.tabCHECKIN);
            this.tabAll.Controls.Add(this.tabCOMMIT);
            this.tabAll.Controls.Add(this.tabROLLBACK);
            this.tabAll.Controls.Add(this.tabTERMINATE);
            this.tabAll.Controls.Add(this.tabCHARGE);
            this.tabAll.Location = new System.Drawing.Point(12, 12);
            this.tabAll.Name = "tabAll";
            this.tabAll.SelectedIndex = 0;
            this.tabAll.Size = new System.Drawing.Size(717, 414);
            this.tabAll.TabIndex = 0;
            // 
            // tabSocket
            // 
            this.tabSocket.Controls.Add(this.gridSocket);
            this.tabSocket.Location = new System.Drawing.Point(4, 22);
            this.tabSocket.Name = "tabSocket";
            this.tabSocket.Padding = new System.Windows.Forms.Padding(3);
            this.tabSocket.Size = new System.Drawing.Size(709, 388);
            this.tabSocket.TabIndex = 0;
            this.tabSocket.Text = "Socket";
            this.tabSocket.UseVisualStyleBackColor = true;
            // 
            // gridSocket
            // 
            this.gridSocket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSocket.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colVal});
            this.gridSocket.Location = new System.Drawing.Point(3, 16);
            this.gridSocket.Name = "gridSocket";
            this.gridSocket.Size = new System.Drawing.Size(700, 366);
            this.gridSocket.TabIndex = 0;
            // 
            // colName
            // 
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 250;
            // 
            // colVal
            // 
            this.colVal.HeaderText = "Value";
            this.colVal.Name = "colVal";
            this.colVal.Width = 400;
            // 
            // tabCONNECT
            // 
            this.tabCONNECT.Controls.Add(this.gridCONNECT);
            this.tabCONNECT.Location = new System.Drawing.Point(4, 22);
            this.tabCONNECT.Name = "tabCONNECT";
            this.tabCONNECT.Padding = new System.Windows.Forms.Padding(3);
            this.tabCONNECT.Size = new System.Drawing.Size(709, 388);
            this.tabCONNECT.TabIndex = 1;
            this.tabCONNECT.Text = "CONNECT";
            this.tabCONNECT.UseVisualStyleBackColor = true;
            // 
            // gridCONNECT
            // 
            this.gridCONNECT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCONNECT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.gridCONNECT.Location = new System.Drawing.Point(4, 11);
            this.gridCONNECT.Name = "gridCONNECT";
            this.gridCONNECT.Size = new System.Drawing.Size(700, 366);
            this.gridCONNECT.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // tabSHAKE
            // 
            this.tabSHAKE.Controls.Add(this.gridSHAKE);
            this.tabSHAKE.Location = new System.Drawing.Point(4, 22);
            this.tabSHAKE.Name = "tabSHAKE";
            this.tabSHAKE.Padding = new System.Windows.Forms.Padding(3);
            this.tabSHAKE.Size = new System.Drawing.Size(709, 388);
            this.tabSHAKE.TabIndex = 2;
            this.tabSHAKE.Text = "SHAKE";
            this.tabSHAKE.UseVisualStyleBackColor = true;
            // 
            // gridSHAKE
            // 
            this.gridSHAKE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSHAKE.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.gridSHAKE.Location = new System.Drawing.Point(4, 11);
            this.gridSHAKE.Name = "gridSHAKE";
            this.gridSHAKE.Size = new System.Drawing.Size(700, 366);
            this.gridSHAKE.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 250;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Value";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 400;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(651, 444);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(570, 445);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(484, 446);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabCHECKIN
            // 
            this.tabCHECKIN.Controls.Add(this.dataGridView1);
            this.tabCHECKIN.Location = new System.Drawing.Point(4, 22);
            this.tabCHECKIN.Name = "tabCHECKIN";
            this.tabCHECKIN.Padding = new System.Windows.Forms.Padding(3);
            this.tabCHECKIN.Size = new System.Drawing.Size(709, 388);
            this.tabCHECKIN.TabIndex = 3;
            this.tabCHECKIN.Text = "CHECKIN";
            this.tabCHECKIN.UseVisualStyleBackColor = true;
            // 
            // tabCOMMIT
            // 
            this.tabCOMMIT.Controls.Add(this.dataGridView2);
            this.tabCOMMIT.Location = new System.Drawing.Point(4, 22);
            this.tabCOMMIT.Name = "tabCOMMIT";
            this.tabCOMMIT.Padding = new System.Windows.Forms.Padding(3);
            this.tabCOMMIT.Size = new System.Drawing.Size(709, 388);
            this.tabCOMMIT.TabIndex = 4;
            this.tabCOMMIT.Text = "COMMIT";
            this.tabCOMMIT.UseVisualStyleBackColor = true;
            // 
            // tabROLLBACK
            // 
            this.tabROLLBACK.Controls.Add(this.dataGridView3);
            this.tabROLLBACK.Location = new System.Drawing.Point(4, 22);
            this.tabROLLBACK.Name = "tabROLLBACK";
            this.tabROLLBACK.Padding = new System.Windows.Forms.Padding(3);
            this.tabROLLBACK.Size = new System.Drawing.Size(709, 388);
            this.tabROLLBACK.TabIndex = 5;
            this.tabROLLBACK.Text = "ROLLBACK";
            this.tabROLLBACK.UseVisualStyleBackColor = true;
            // 
            // tabTERMINATE
            // 
            this.tabTERMINATE.Controls.Add(this.dataGridView4);
            this.tabTERMINATE.Location = new System.Drawing.Point(4, 22);
            this.tabTERMINATE.Name = "tabTERMINATE";
            this.tabTERMINATE.Padding = new System.Windows.Forms.Padding(3);
            this.tabTERMINATE.Size = new System.Drawing.Size(709, 388);
            this.tabTERMINATE.TabIndex = 6;
            this.tabTERMINATE.Text = "TERMINATE";
            this.tabTERMINATE.UseVisualStyleBackColor = true;
            // 
            // tabCHARGE
            // 
            this.tabCHARGE.Controls.Add(this.dataGridView5);
            this.tabCHARGE.Location = new System.Drawing.Point(4, 22);
            this.tabCHARGE.Name = "tabCHARGE";
            this.tabCHARGE.Padding = new System.Windows.Forms.Padding(3);
            this.tabCHARGE.Size = new System.Drawing.Size(709, 388);
            this.tabCHARGE.TabIndex = 7;
            this.tabCHARGE.Text = "CHARGE";
            this.tabCHARGE.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dataGridView1.Location = new System.Drawing.Point(4, 11);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(700, 366);
            this.dataGridView1.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Name";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 250;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Value";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 400;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dataGridView2.Location = new System.Drawing.Point(4, 11);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(700, 366);
            this.dataGridView2.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Name";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 250;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Value";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 400;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.dataGridView3.Location = new System.Drawing.Point(4, 11);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(700, 366);
            this.dataGridView3.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Name";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 250;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Value";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 400;
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.dataGridView4.Location = new System.Drawing.Point(4, 11);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(700, 366);
            this.dataGridView4.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Name";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 250;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Value";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 400;
            // 
            // dataGridView5
            // 
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14});
            this.dataGridView5.Location = new System.Drawing.Point(4, 11);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.Size = new System.Drawing.Size(700, 366);
            this.dataGridView5.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "Name";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 250;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "Value";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 400;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 479);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tabAll);
            this.Name = "frmSetting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.tabAll.ResumeLayout(false);
            this.tabSocket.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSocket)).EndInit();
            this.tabCONNECT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCONNECT)).EndInit();
            this.tabSHAKE.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSHAKE)).EndInit();
            this.tabCHECKIN.ResumeLayout(false);
            this.tabCOMMIT.ResumeLayout(false);
            this.tabROLLBACK.ResumeLayout(false);
            this.tabTERMINATE.ResumeLayout(false);
            this.tabCHARGE.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabAll;
        private System.Windows.Forms.TabPage tabSocket;
        private System.Windows.Forms.TabPage tabCONNECT;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView gridSocket;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVal;
        private System.Windows.Forms.DataGridView gridCONNECT;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabPage tabSHAKE;
        private System.Windows.Forms.DataGridView gridSHAKE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.TabPage tabCHECKIN;
        private System.Windows.Forms.TabPage tabCOMMIT;
        private System.Windows.Forms.TabPage tabROLLBACK;
        private System.Windows.Forms.TabPage tabTERMINATE;
        private System.Windows.Forms.TabPage tabCHARGE;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
    }
}