namespace main.科室事物管理
{
    partial class FlowStatusQuery
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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("医师术前访视查询");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("医师术后访视查询");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("医师麻醉总结查询");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("护士术前访视");
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.row_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yizuo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patdpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatZhuYuanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patsex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Szzd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MazuiFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ssys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qxhs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Xhhs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSQFS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMZZJ = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSHFS = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCX = new System.Windows.Forms.Label();
            this.btnPrintResult = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsmiHS = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.row_id,
            this.yizuo,
            this.otime,
            this.Column3,
            this.patdpm,
            this.PatZhuYuanID,
            this.PatName,
            this.patsex,
            this.patage,
            this.Szzd,
            this.oname,
            this.Amethod,
            this.MazuiFS,
            this.asa,
            this.Ssys,
            this.mzys,
            this.qxhs,
            this.Xhhs});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(171, 96);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(915, 476);
            this.dataGridView1.TabIndex = 18;
            // 
            // row_id
            // 
            this.row_id.DataPropertyName = "row_id";
            this.row_id.HeaderText = "编号";
            this.row_id.Name = "row_id";
            this.row_id.ReadOnly = true;
            this.row_id.Visible = false;
            this.row_id.Width = 35;
            // 
            // yizuo
            // 
            this.yizuo.DataPropertyName = "yizuo";
            this.yizuo.HeaderText = "是否已做";
            this.yizuo.Name = "yizuo";
            this.yizuo.ReadOnly = true;
            this.yizuo.Width = 78;
            // 
            // otime
            // 
            this.otime.DataPropertyName = "otime";
            this.otime.HeaderText = "手术日期";
            this.otime.Name = "otime";
            this.otime.ReadOnly = true;
            this.otime.Width = 78;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Oroom";
            this.Column3.HeaderText = "手术间";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 66;
            // 
            // patdpm
            // 
            this.patdpm.DataPropertyName = "patdpm";
            this.patdpm.HeaderText = "科室";
            this.patdpm.Name = "patdpm";
            this.patdpm.ReadOnly = true;
            this.patdpm.Width = 54;
            // 
            // PatZhuYuanID
            // 
            this.PatZhuYuanID.DataPropertyName = "PatZhuYuanID";
            this.PatZhuYuanID.HeaderText = "住院号";
            this.PatZhuYuanID.Name = "PatZhuYuanID";
            this.PatZhuYuanID.ReadOnly = true;
            this.PatZhuYuanID.Width = 66;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "病人姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.Width = 78;
            // 
            // patsex
            // 
            this.patsex.DataPropertyName = "patsex";
            this.patsex.HeaderText = "性别";
            this.patsex.Name = "patsex";
            this.patsex.ReadOnly = true;
            this.patsex.Width = 54;
            // 
            // patage
            // 
            this.patage.DataPropertyName = "patage";
            this.patage.HeaderText = "年龄";
            this.patage.Name = "patage";
            this.patage.ReadOnly = true;
            this.patage.Width = 54;
            // 
            // Szzd
            // 
            this.Szzd.DataPropertyName = "Szzd";
            this.Szzd.HeaderText = "术中诊断";
            this.Szzd.Name = "Szzd";
            this.Szzd.ReadOnly = true;
            this.Szzd.Visible = false;
            this.Szzd.Width = 78;
            // 
            // oname
            // 
            this.oname.DataPropertyName = "oname";
            this.oname.HeaderText = "手术名称";
            this.oname.Name = "oname";
            this.oname.ReadOnly = true;
            this.oname.Width = 78;
            // 
            // Amethod
            // 
            this.Amethod.DataPropertyName = "Amethod";
            this.Amethod.HeaderText = "拟行麻醉";
            this.Amethod.Name = "Amethod";
            this.Amethod.ReadOnly = true;
            this.Amethod.Width = 78;
            // 
            // MazuiFS
            // 
            this.MazuiFS.DataPropertyName = "MazuiFS";
            this.MazuiFS.HeaderText = "实施麻醉";
            this.MazuiFS.Name = "MazuiFS";
            this.MazuiFS.ReadOnly = true;
            this.MazuiFS.Width = 78;
            // 
            // asa
            // 
            this.asa.DataPropertyName = "asa";
            this.asa.HeaderText = "麻醉等级";
            this.asa.Name = "asa";
            this.asa.ReadOnly = true;
            this.asa.Width = 78;
            // 
            // Ssys
            // 
            this.Ssys.DataPropertyName = "Ssys";
            this.Ssys.HeaderText = "手术医生";
            this.Ssys.Name = "Ssys";
            this.Ssys.ReadOnly = true;
            this.Ssys.Width = 78;
            // 
            // mzys
            // 
            this.mzys.DataPropertyName = "mzys";
            this.mzys.HeaderText = "麻醉医生";
            this.mzys.Name = "mzys";
            this.mzys.ReadOnly = true;
            this.mzys.Width = 78;
            // 
            // qxhs
            // 
            this.qxhs.DataPropertyName = "qxhs";
            this.qxhs.HeaderText = "器械护士";
            this.qxhs.Name = "qxhs";
            this.qxhs.ReadOnly = true;
            this.qxhs.Width = 78;
            // 
            // Xhhs
            // 
            this.Xhhs.DataPropertyName = "Xhhs";
            this.Xhhs.HeaderText = "巡回护士";
            this.Xhhs.Name = "Xhhs";
            this.Xhhs.ReadOnly = true;
            this.Xhhs.Width = 78;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSQFS,
            this.tsmiMZZJ,
            this.tsmiSHFS,
            this.tsmiHS});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
            // 
            // tsmiSQFS
            // 
            this.tsmiSQFS.Name = "tsmiSQFS";
            this.tsmiSQFS.Size = new System.Drawing.Size(152, 22);
            this.tsmiSQFS.Text = "医师术前访视";
            this.tsmiSQFS.Click += new System.EventHandler(this.tsmiSQFS_Click);
            // 
            // tsmiMZZJ
            // 
            this.tsmiMZZJ.Name = "tsmiMZZJ";
            this.tsmiMZZJ.Size = new System.Drawing.Size(152, 22);
            this.tsmiMZZJ.Text = "医师麻醉总结";
            this.tsmiMZZJ.Click += new System.EventHandler(this.tsmiMZZJ_Click);
            // 
            // tsmiSHFS
            // 
            this.tsmiSHFS.Name = "tsmiSHFS";
            this.tsmiSHFS.Size = new System.Drawing.Size(152, 22);
            this.tsmiSHFS.Text = "医师术后访视";
            this.tsmiSHFS.Click += new System.EventHandler(this.tsmiSHFS_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.BackColor = System.Drawing.SystemColors.Control;
            this.treeView1.Font = new System.Drawing.Font("宋体", 11F);
            this.treeView1.Location = new System.Drawing.Point(0, 96);
            this.treeView1.Name = "treeView1";
            treeNode5.Name = "shuqian";
            treeNode5.Text = "医师术前访视查询";
            treeNode6.Name = "shuhou";
            treeNode6.Text = "医师术后访视查询";
            treeNode7.Name = "mzzj";
            treeNode7.Text = "医师麻醉总结查询";
            treeNode8.Name = "hs";
            treeNode8.Text = "护士术前访视";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.treeView1.Size = new System.Drawing.Size(165, 476);
            this.treeView1.TabIndex = 19;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(21, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "选择查询时间段:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblCX);
            this.panel1.Controls.Add(this.btnPrintResult);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1089, 90);
            this.panel1.TabIndex = 21;
            // 
            // lblCX
            // 
            this.lblCX.AutoSize = true;
            this.lblCX.Font = new System.Drawing.Font("宋体", 11F);
            this.lblCX.ForeColor = System.Drawing.Color.Red;
            this.lblCX.Location = new System.Drawing.Point(21, 66);
            this.lblCX.Name = "lblCX";
            this.lblCX.Size = new System.Drawing.Size(97, 15);
            this.lblCX.TabIndex = 646;
            this.lblCX.Text = "术前访视查询";
            // 
            // btnPrintResult
            // 
            this.btnPrintResult.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnPrintResult.ForeColor = System.Drawing.Color.Blue;
            this.btnPrintResult.Image = global::main.Properties.Resources.Print;
            this.btnPrintResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintResult.Location = new System.Drawing.Point(460, 22);
            this.btnPrintResult.Name = "btnPrintResult";
            this.btnPrintResult.Size = new System.Drawing.Size(92, 45);
            this.btnPrintResult.TabIndex = 645;
            this.btnPrintResult.Text = "打印统计";
            this.btnPrintResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintResult.UseVisualStyleBackColor = true;
            this.btnPrintResult.Click += new System.EventHandler(this.btnPrintResult_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Blue;
            this.button4.Location = new System.Drawing.Point(319, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 48);
            this.button4.TabIndex = 18;
            this.button4.Text = "查询";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dateTimePicker2.Location = new System.Drawing.Point(186, 34);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(119, 21);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(24, 31);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(128, 21);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "至";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(311, 17);
            this.toolStripStatusLabel1.Text = "                                     当前手术总量：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 575);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1098, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "当前手术总量：";
            // 
            // tsmiHS
            // 
            this.tsmiHS.Name = "tsmiHS";
            this.tsmiHS.Size = new System.Drawing.Size(152, 22);
            this.tsmiHS.Text = "护士术前访视";
            this.tsmiHS.Click += new System.EventHandler(this.tsmiHS_Click);
            // 
            // Select_YZBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 597);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Select_YZBD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询表单";
            this.Load += new System.EventHandler(this.Select_YZBD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrintResult;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label lblCX;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSQFS;
        private System.Windows.Forms.ToolStripMenuItem tsmiMZZJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn row_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn yizuo;
        private System.Windows.Forms.DataGridViewTextBoxColumn otime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn patdpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatZhuYuanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn patsex;
        private System.Windows.Forms.DataGridViewTextBoxColumn patage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Szzd;
        private System.Windows.Forms.DataGridViewTextBoxColumn oname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn MazuiFS;
        private System.Windows.Forms.DataGridViewTextBoxColumn asa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ssys;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzys;
        private System.Windows.Forms.DataGridViewTextBoxColumn qxhs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Xhhs;
        private System.Windows.Forms.ToolStripMenuItem tsmiSHFS;
        private System.Windows.Forms.ToolStripMenuItem tsmiHS;
    }
}