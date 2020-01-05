namespace main
{
    partial class HuShiTJ
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
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("专家手术");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("手术切口");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("三四级手术");
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
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
            this.SY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SanSJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 87);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "选择统计时间段:";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Blue;
            this.button4.Location = new System.Drawing.Point(310, 22);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 48);
            this.button4.TabIndex = 23;
            this.button4.Text = "查询";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dateTimePicker2.Location = new System.Drawing.Point(177, 37);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(119, 21);
            this.dateTimePicker2.TabIndex = 21;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(15, 34);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(128, 21);
            this.dateTimePicker1.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "至";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.BackColor = System.Drawing.SystemColors.Control;
            this.treeView1.Font = new System.Drawing.Font("宋体", 11F);
            this.treeView1.Location = new System.Drawing.Point(0, 93);
            this.treeView1.Name = "treeView1";
            treeNode4.Name = "ZJ";
            treeNode4.Text = "专家手术";
            treeNode5.Name = "QK";
            treeNode5.Text = "手术切口";
            treeNode6.Name = "SanSJ";
            treeNode6.Text = "三四级手术";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeView1.Size = new System.Drawing.Size(165, 449);
            this.treeView1.TabIndex = 14;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
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
            this.SY,
            this.JZ,
            this.ZJ,
            this.SanSJ,
            this.QK});
            this.dataGridView1.Location = new System.Drawing.Point(171, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(815, 449);
            this.dataGridView1.TabIndex = 15;
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 545);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(998, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "当前手术总量：";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(240, 17);
            this.toolStripStatusLabel1.Text = "                                     当前手术总量：";
            // 
            // HuShiTJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 567);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.Name = "HuShiTJ";
            this.Text = "护士统计";
            this.Load += new System.EventHandler(this.HuShiTJ_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dataGridView1;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn SY;
        private System.Windows.Forms.DataGridViewTextBoxColumn JZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn SanSJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn QK;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}