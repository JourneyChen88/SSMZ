namespace main
{
    partial class SSTSTJ
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("麻醉医生");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("手术间");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("科室");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ASA分级");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("麻醉方式");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("手术护士");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("切口等级");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("手术类别");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("3小时内");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("超过3小时");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("手术持续时间", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.mzid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patdpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patsex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qxhs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xhhs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shoushufs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pattmd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(445, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 48);
            this.button2.TabIndex = 2;
            this.button2.Text = "按麻醉医师统计";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(708, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 48);
            this.button1.TabIndex = 3;
            this.button1.Text = "按科室统计";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Blue;
            this.button3.Location = new System.Drawing.Point(585, 27);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 48);
            this.button3.TabIndex = 4;
            this.button3.Text = "按手术间统计";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dateTimePicker2.Location = new System.Drawing.Point(186, 42);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(119, 21);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "至";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(24, 39);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(128, 21);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(21, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "选择统计时间段:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1092, 90);
            this.panel1.TabIndex = 16;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.Color.Blue;
            this.button6.Location = new System.Drawing.Point(831, 28);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(101, 48);
            this.button6.TabIndex = 20;
            this.button6.Text = "护士统计";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Blue;
            this.button5.Location = new System.Drawing.Point(954, 28);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(101, 48);
            this.button5.TabIndex = 19;
            this.button5.Text = "刷新";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Blue;
            this.button4.Location = new System.Drawing.Point(319, 27);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 48);
            this.button4.TabIndex = 18;
            this.button4.Text = "查询";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.BackColor = System.Drawing.SystemColors.Control;
            this.treeView1.Font = new System.Drawing.Font("宋体", 11F);
            this.treeView1.Location = new System.Drawing.Point(0, 96);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Adims_User";
            treeNode1.Text = "麻醉医生";
            treeNode2.Name = "ssjstate";
            treeNode2.Text = "手术间";
            treeNode3.Name = "keshi";
            treeNode3.Text = "科室";
            treeNode4.Name = "Olevel";
            treeNode4.Text = "ASA分级";
            treeNode5.Name = "amethod";
            treeNode5.Text = "麻醉方式";
            treeNode6.Name = "Adims_User";
            treeNode6.Text = "手术护士";
            treeNode7.Name = "QKDJ";
            treeNode7.Text = "切口等级";
            treeNode8.Name = "SSLB";
            treeNode8.Text = "手术类别";
            treeNode9.Name = "节点3";
            treeNode9.Text = "3小时内";
            treeNode10.Name = "节点4";
            treeNode10.Text = "超过3小时";
            treeNode11.Name = "shijian";
            treeNode11.Text = "手术持续时间";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode11});
            this.treeView1.Size = new System.Drawing.Size(165, 396);
            this.treeView1.TabIndex = 13;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
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
            this.mzid,
            this.Column2,
            this.Column3,
            this.patdpm,
            this.patid,
            this.PatName,
            this.patsex,
            this.patage,
            this.mzff,
            this.mzys,
            this.qxhs,
            this.xhhs,
            this.shoushufs,
            this.os,
            this.asa,
            this.pattmd});
            this.dataGridView1.Location = new System.Drawing.Point(171, 96);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(918, 396);
            this.dataGridView1.TabIndex = 6;
            // 
            // mzid
            // 
            this.mzid.DataPropertyName = "mzid";
            this.mzid.HeaderText = "麻醉编号";
            this.mzid.Name = "mzid";
            this.mzid.ReadOnly = true;
            this.mzid.Visible = false;
            this.mzid.Width = 59;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "otime";
            this.Column2.HeaderText = "手术日期";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
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
            // patid
            // 
            this.patid.DataPropertyName = "patid";
            this.patid.HeaderText = "病人编号";
            this.patid.Name = "patid";
            this.patid.ReadOnly = true;
            this.patid.Width = 78;
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
            // mzff
            // 
            this.mzff.DataPropertyName = "mazuifs";
            this.mzff.HeaderText = "麻醉方法";
            this.mzff.Name = "mzff";
            this.mzff.ReadOnly = true;
            this.mzff.Width = 78;
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
            // xhhs
            // 
            this.xhhs.DataPropertyName = "xhhs";
            this.xhhs.HeaderText = "巡回护士";
            this.xhhs.Name = "xhhs";
            this.xhhs.ReadOnly = true;
            this.xhhs.Width = 78;
            // 
            // shoushufs
            // 
            this.shoushufs.DataPropertyName = "shoushufs";
            this.shoushufs.HeaderText = "手术名称";
            this.shoushufs.Name = "shoushufs";
            this.shoushufs.ReadOnly = true;
            this.shoushufs.Width = 78;
            // 
            // os
            // 
            this.os.DataPropertyName = "os";
            this.os.HeaderText = "手术医生";
            this.os.Name = "os";
            this.os.ReadOnly = true;
            this.os.Width = 78;
            // 
            // asa
            // 
            this.asa.DataPropertyName = "asa";
            this.asa.HeaderText = "手术等级";
            this.asa.Name = "asa";
            this.asa.ReadOnly = true;
            this.asa.Width = 78;
            // 
            // pattmd
            // 
            this.pattmd.DataPropertyName = "pattmd";
            this.pattmd.HeaderText = "疾病诊断";
            this.pattmd.Name = "pattmd";
            this.pattmd.ReadOnly = true;
            this.pattmd.Width = 78;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 495);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1092, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "当前手术总量：";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(240, 17);
            this.toolStripStatusLabel1.Text = "                                     当前手术总量：";
            // 
            // SSTSTJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 517);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "SSTSTJ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手术统计";
            this.Load += new System.EventHandler(this.SSTSTJ_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn patdpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn patid;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn patsex;
        private System.Windows.Forms.DataGridViewTextBoxColumn patage;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzff;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzys;
        private System.Windows.Forms.DataGridViewTextBoxColumn qxhs;
        private System.Windows.Forms.DataGridViewTextBoxColumn xhhs;
        private System.Windows.Forms.DataGridViewTextBoxColumn shoushufs;
        private System.Windows.Forms.DataGridViewTextBoxColumn os;
        private System.Windows.Forms.DataGridViewTextBoxColumn asa;
        private System.Windows.Forms.DataGridViewTextBoxColumn pattmd;
        private System.Windows.Forms.Button button6;
    }
}