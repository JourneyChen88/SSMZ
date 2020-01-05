namespace main
{
    partial class mazuishtjjitj
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.rowid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Otime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Patdpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patbedno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Patname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShoushuFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MazuiFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zhentb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fangshiren = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tesqk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.babengren = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bbtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rowid,
            this.Otime,
            this.Patdpm,
            this.patbedno,
            this.Patname,
            this.mzys,
            this.ShoushuFS,
            this.MazuiFS,
            this.asa,
            this.zhentb,
            this.fangshiren,
            this.tesqk,
            this.babengren,
            this.bbtime});
            this.dataGridView1.Location = new System.Drawing.Point(12, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(844, 352);
            this.dataGridView1.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(60, 27);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(119, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(227, 27);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(119, 21);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(376, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 3;
            this.button1.Text = "查询镇痛泵";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button2.Location = new System.Drawing.Point(678, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 34);
            this.button2.TabIndex = 4;
            this.button2.Text = "导出excel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(488, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 34);
            this.button3.TabIndex = 5;
            this.button3.Text = "查询复苏病人";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // rowid
            // 
            this.rowid.DataPropertyName = "rowid";
            this.rowid.HeaderText = "序号";
            this.rowid.Name = "rowid";
            // 
            // Otime
            // 
            this.Otime.DataPropertyName = "Otime";
            this.Otime.HeaderText = "手术日期";
            this.Otime.Name = "Otime";
            // 
            // Patdpm
            // 
            this.Patdpm.DataPropertyName = "Patdpm";
            this.Patdpm.HeaderText = "科室";
            this.Patdpm.Name = "Patdpm";
            // 
            // patbedno
            // 
            this.patbedno.DataPropertyName = "patbedno";
            this.patbedno.HeaderText = "床号";
            this.patbedno.Name = "patbedno";
            // 
            // Patname
            // 
            this.Patname.DataPropertyName = "Patname";
            this.Patname.HeaderText = "姓名";
            this.Patname.Name = "Patname";
            // 
            // mzys
            // 
            this.mzys.DataPropertyName = "mzys";
            this.mzys.HeaderText = "麻醉医生";
            this.mzys.Name = "mzys";
            // 
            // ShoushuFS
            // 
            this.ShoushuFS.DataPropertyName = "ShoushuFS";
            this.ShoushuFS.HeaderText = "手术方式";
            this.ShoushuFS.Name = "ShoushuFS";
            // 
            // MazuiFS
            // 
            this.MazuiFS.DataPropertyName = "MazuiFS";
            this.MazuiFS.HeaderText = "麻醉方式";
            this.MazuiFS.Name = "MazuiFS";
            // 
            // asa
            // 
            this.asa.DataPropertyName = "ASA";
            this.asa.HeaderText = "ASA分级";
            this.asa.Name = "asa";
            // 
            // zhentb
            // 
            this.zhentb.DataPropertyName = "zhentb";
            this.zhentb.HeaderText = "镇痛泵有无";
            this.zhentb.Name = "zhentb";
            // 
            // fangshiren
            // 
            this.fangshiren.DataPropertyName = "fangshiren";
            this.fangshiren.HeaderText = "访视人员";
            this.fangshiren.Name = "fangshiren";
            // 
            // tesqk
            // 
            this.tesqk.DataPropertyName = "tesuqingk";
            this.tesqk.HeaderText = "特殊情况";
            this.tesqk.Name = "tesqk";
            // 
            // babengren
            // 
            this.babengren.DataPropertyName = "babengren";
            this.babengren.HeaderText = "拔泵人";
            this.babengren.Name = "babengren";
            // 
            // bbtime
            // 
            this.bbtime.DataPropertyName = "babengshijian";
            this.bbtime.HeaderText = "拔泵时间";
            this.bbtime.Name = "bbtime";
            // 
            // mazuishtjjitj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 482);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "mazuishtjjitj";
            this.Text = "麻醉统计随访患者情况";
            this.Load += new System.EventHandler(this.mazuishtjjitj_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Otime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Patdpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn patbedno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Patname;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzys;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShoushuFS;
        private System.Windows.Forms.DataGridViewTextBoxColumn MazuiFS;
        private System.Windows.Forms.DataGridViewTextBoxColumn asa;
        private System.Windows.Forms.DataGridViewTextBoxColumn zhentb;
        private System.Windows.Forms.DataGridViewTextBoxColumn fangshiren;
        private System.Windows.Forms.DataGridViewTextBoxColumn tesqk;
        private System.Windows.Forms.DataGridViewTextBoxColumn babengren;
        private System.Windows.Forms.DataGridViewTextBoxColumn bbtime;
    }
}