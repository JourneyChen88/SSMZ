namespace main
{
    partial class addHSDJ
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DtTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keshi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DJNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSRY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MZS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QXHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XHHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SanSJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除一行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbKS = new System.Windows.Forms.ComboBox();
            this.btnPrintResult = new System.Windows.Forms.Button();
            this.txtMZYS = new WindowsFormsControlLibrary5.UserControl1();
            this.btndaoru = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.DtTime,
            this.keshi,
            this.SSJ,
            this.CH,
            this.DJNames,
            this.patid,
            this.Age,
            this.Sex,
            this.SSName,
            this.SSRY,
            this.MZS,
            this.QXHS,
            this.XHHS,
            this.SY,
            this.JZ,
            this.ZJ,
            this.SanSJ,
            this.QK,
            this.BZ});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1131, 563);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.Frozen = true;
            this.id.HeaderText = "编号";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // DtTime
            // 
            this.DtTime.DataPropertyName = "DtTime";
            this.DtTime.Frozen = true;
            this.DtTime.HeaderText = "日期";
            this.DtTime.Name = "DtTime";
            this.DtTime.Width = 54;
            // 
            // keshi
            // 
            this.keshi.DataPropertyName = "keshi";
            this.keshi.Frozen = true;
            this.keshi.HeaderText = "科室";
            this.keshi.Name = "keshi";
            this.keshi.Width = 54;
            // 
            // SSJ
            // 
            this.SSJ.DataPropertyName = "SSJ";
            this.SSJ.Frozen = true;
            this.SSJ.HeaderText = "手术间";
            this.SSJ.Name = "SSJ";
            this.SSJ.Width = 66;
            // 
            // CH
            // 
            this.CH.DataPropertyName = "CH";
            this.CH.Frozen = true;
            this.CH.HeaderText = "床号";
            this.CH.Name = "CH";
            this.CH.Width = 54;
            // 
            // DJNames
            // 
            this.DJNames.DataPropertyName = "DJNames";
            this.DJNames.Frozen = true;
            this.DJNames.HeaderText = "姓名";
            this.DJNames.Name = "DJNames";
            this.DJNames.Width = 54;
            // 
            // patid
            // 
            this.patid.DataPropertyName = "patid";
            this.patid.Frozen = true;
            this.patid.HeaderText = "住院号";
            this.patid.Name = "patid";
            this.patid.Width = 66;
            // 
            // Age
            // 
            this.Age.DataPropertyName = "Age";
            this.Age.Frozen = true;
            this.Age.HeaderText = "年龄";
            this.Age.Name = "Age";
            this.Age.Width = 54;
            // 
            // Sex
            // 
            this.Sex.DataPropertyName = "Sex";
            this.Sex.HeaderText = "性别";
            this.Sex.Name = "Sex";
            this.Sex.Width = 54;
            // 
            // SSName
            // 
            this.SSName.DataPropertyName = "SSName";
            this.SSName.HeaderText = "手术名称";
            this.SSName.Name = "SSName";
            this.SSName.Width = 78;
            // 
            // SSRY
            // 
            this.SSRY.DataPropertyName = "SSRY";
            this.SSRY.HeaderText = "手术人员";
            this.SSRY.Name = "SSRY";
            this.SSRY.Width = 78;
            // 
            // MZS
            // 
            this.MZS.DataPropertyName = "MZS";
            this.MZS.HeaderText = "麻醉师";
            this.MZS.Name = "MZS";
            this.MZS.Width = 66;
            // 
            // QXHS
            // 
            this.QXHS.DataPropertyName = "QXHS";
            this.QXHS.HeaderText = "器械护士";
            this.QXHS.Name = "QXHS";
            this.QXHS.Width = 78;
            // 
            // XHHS
            // 
            this.XHHS.DataPropertyName = "XHHS";
            this.XHHS.HeaderText = "巡回护士";
            this.XHHS.Name = "XHHS";
            this.XHHS.Width = 78;
            // 
            // SY
            // 
            this.SY.DataPropertyName = "SY";
            this.SY.HeaderText = "输液";
            this.SY.Name = "SY";
            this.SY.Width = 54;
            // 
            // JZ
            // 
            this.JZ.DataPropertyName = "JZ";
            this.JZ.HeaderText = "急诊";
            this.JZ.Name = "JZ";
            this.JZ.Width = 54;
            // 
            // ZJ
            // 
            this.ZJ.DataPropertyName = "ZJ";
            this.ZJ.HeaderText = "专家";
            this.ZJ.Name = "ZJ";
            this.ZJ.Width = 54;
            // 
            // SanSJ
            // 
            this.SanSJ.DataPropertyName = "SanSJ";
            this.SanSJ.HeaderText = "三四级";
            this.SanSJ.Name = "SanSJ";
            this.SanSJ.Width = 66;
            // 
            // QK
            // 
            this.QK.DataPropertyName = "QK";
            this.QK.HeaderText = "切口";
            this.QK.Name = "QK";
            this.QK.Width = 54;
            // 
            // BZ
            // 
            this.BZ.DataPropertyName = "BZ";
            this.BZ.HeaderText = "备注";
            this.BZ.Name = "BZ";
            this.BZ.Width = 54;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除一行ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // 删除一行ToolStripMenuItem
            // 
            this.删除一行ToolStripMenuItem.Name = "删除一行ToolStripMenuItem";
            this.删除一行ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除一行ToolStripMenuItem.Text = "删除一行";
            this.删除一行ToolStripMenuItem.Click += new System.EventHandler(this.删除一行ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "选择日期：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy年MM月dd日";
            this.dateTimePicker1.Location = new System.Drawing.Point(92, 22);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(109, 21);
            this.dateTimePicker1.TabIndex = 11;
            this.dateTimePicker1.Value = new System.DateTime(2014, 9, 5, 17, 12, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "科室：";
            // 
            // cmbKS
            // 
            this.cmbKS.FormattingEnabled = true;
            this.cmbKS.Location = new System.Drawing.Point(322, 25);
            this.cmbKS.Name = "cmbKS";
            this.cmbKS.Size = new System.Drawing.Size(121, 20);
            this.cmbKS.TabIndex = 14;
            this.cmbKS.SelectedIndexChanged += new System.EventHandler(this.cmbKS_SelectedIndexChanged);
            // 
            // btnPrintResult
            // 
            this.btnPrintResult.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrintResult.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnPrintResult.ForeColor = System.Drawing.Color.Blue;
            this.btnPrintResult.Image = global::main.Properties.Resources.Print;
            this.btnPrintResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintResult.Location = new System.Drawing.Point(1009, 13);
            this.btnPrintResult.Name = "btnPrintResult";
            this.btnPrintResult.Size = new System.Drawing.Size(114, 40);
            this.btnPrintResult.TabIndex = 645;
            this.btnPrintResult.Text = "打印登记表";
            this.btnPrintResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintResult.UseVisualStyleBackColor = true;
            this.btnPrintResult.Click += new System.EventHandler(this.btnPrintResult_Click);
            // 
            // txtMZYS
            // 
            this.txtMZYS.Location = new System.Drawing.Point(502, 22);
            this.txtMZYS.Name = "txtMZYS";
            this.txtMZYS.Size = new System.Drawing.Size(51, 23);
            this.txtMZYS.TabIndex = 646;
            this.txtMZYS.Visible = false;
            // 
            // btndaoru
            // 
            this.btndaoru.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btndaoru.ForeColor = System.Drawing.Color.Blue;
            this.btndaoru.Location = new System.Drawing.Point(900, 13);
            this.btndaoru.Name = "btndaoru";
            this.btndaoru.Size = new System.Drawing.Size(75, 40);
            this.btndaoru.TabIndex = 648;
            this.btndaoru.Text = "导入信息";
            this.btndaoru.UseVisualStyleBackColor = true;
            this.btndaoru.Click += new System.EventHandler(this.btndaoru_Click);
            // 
            // addHSDJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1155, 647);
            this.Controls.Add(this.txtMZYS);
            this.Controls.Add(this.btnPrintResult);
            this.Controls.Add(this.btndaoru);
            this.Controls.Add(this.cmbKS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "addHSDJ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手术登记";
            this.Load += new System.EventHandler(this.addHSDJ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbKS;
        private System.Windows.Forms.Button btnPrintResult;
        private WindowsFormsControlLibrary5.UserControl1 txtMZYS;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn DtTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn keshi;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn CH;
        private System.Windows.Forms.DataGridViewTextBoxColumn DJNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn patid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSRY;
        private System.Windows.Forms.DataGridViewTextBoxColumn MZS;
        private System.Windows.Forms.DataGridViewTextBoxColumn QXHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn XHHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SY;
        private System.Windows.Forms.DataGridViewTextBoxColumn JZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn SanSJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn QK;
        private System.Windows.Forms.DataGridViewTextBoxColumn BZ;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除一行ToolStripMenuItem;
        private System.Windows.Forms.Button btndaoru;
    }
}