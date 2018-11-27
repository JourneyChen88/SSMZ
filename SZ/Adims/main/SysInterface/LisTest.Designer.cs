namespace main
{
    partial class LisTest
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtName = new WindowsFormsControlLibrary5.UserControl1();
            this.txtSex = new WindowsFormsControlLibrary5.UserControl1();
            this.txtZhuYuanHao = new WindowsFormsControlLibrary5.UserControl1();
            this.txtBednumber = new WindowsFormsControlLibrary5.UserControl1();
            this.txtBingQu = new WindowsFormsControlLibrary5.UserControl1();
            this.txtAge = new WindowsFormsControlLibrary5.UserControl1();
            this.label31 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(0, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(947, 492);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.DataPropertyName = "indextypename";
            this.Column1.HeaderText = "检验编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 140;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "indexname";
            this.Column2.HeaderText = "检验项目";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 250;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "retvalue";
            this.Column3.HeaderText = "检验结果数值";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 151;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "indexunit";
            this.Column4.HeaderText = "检验单位";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 151;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "retstatus";
            this.Column5.HeaderText = "检验结果";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "retref";
            this.Column6.HeaderText = "检验参考值";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 151;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtSex);
            this.groupBox1.Controls.Add(this.txtZhuYuanHao);
            this.groupBox1.Controls.Add(this.txtBednumber);
            this.groupBox1.Controls.Add(this.txtBingQu);
            this.groupBox1.Controls.Add(this.txtAge);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label54);
            this.groupBox1.Controls.Add(this.label53);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Controls.Add(this.label47);
            this.groupBox1.Controls.Add(this.label62);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(947, 55);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(78, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(99, 23);
            this.txtName.TabIndex = 577;
            // 
            // txtSex
            // 
            this.txtSex.Location = new System.Drawing.Point(228, 20);
            this.txtSex.Name = "txtSex";
            this.txtSex.Size = new System.Drawing.Size(53, 23);
            this.txtSex.TabIndex = 578;
            // 
            // txtZhuYuanHao
            // 
            this.txtZhuYuanHao.Location = new System.Drawing.Point(849, 20);
            this.txtZhuYuanHao.Name = "txtZhuYuanHao";
            this.txtZhuYuanHao.Size = new System.Drawing.Size(74, 23);
            this.txtZhuYuanHao.TabIndex = 575;
            // 
            // txtBednumber
            // 
            this.txtBednumber.Location = new System.Drawing.Point(711, 20);
            this.txtBednumber.Name = "txtBednumber";
            this.txtBednumber.Size = new System.Drawing.Size(66, 23);
            this.txtBednumber.TabIndex = 573;
            // 
            // txtBingQu
            // 
            this.txtBingQu.Location = new System.Drawing.Point(493, 20);
            this.txtBingQu.Name = "txtBingQu";
            this.txtBingQu.Size = new System.Drawing.Size(146, 23);
            this.txtBingQu.TabIndex = 572;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(355, 20);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(31, 23);
            this.txtAge.TabIndex = 579;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label31.Location = new System.Drawing.Point(387, 21);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(29, 19);
            this.label31.TabIndex = 583;
            this.label31.Text = "岁";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label54.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label54.Location = new System.Drawing.Point(304, 21);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(49, 19);
            this.label54.TabIndex = 582;
            this.label54.Text = "年龄";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label53.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label53.Location = new System.Drawing.Point(182, 21);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(49, 19);
            this.label53.TabIndex = 581;
            this.label53.Text = "性别";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label52.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label52.Location = new System.Drawing.Point(32, 21);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(49, 19);
            this.label52.TabIndex = 580;
            this.label52.Text = "姓名";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label47.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label47.Location = new System.Drawing.Point(783, 21);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(69, 19);
            this.label47.TabIndex = 576;
            this.label47.Text = "住院号";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label62.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label62.Location = new System.Drawing.Point(656, 21);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(49, 19);
            this.label62.TabIndex = 574;
            this.label62.Text = "床号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(441, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 19);
            this.label4.TabIndex = 571;
            this.label4.Text = "病区";
            // 
            // LisTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 553);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(380, 0);
            this.MaximizeBox = false;
            this.Name = "LisTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LisTest";
            this.Load += new System.EventHandler(this.LisTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label52;
        private WindowsFormsControlLibrary5.UserControl1 txtAge;
        private WindowsFormsControlLibrary5.UserControl1 txtSex;
        private WindowsFormsControlLibrary5.UserControl1 txtName;
        private System.Windows.Forms.Label label47;
        private WindowsFormsControlLibrary5.UserControl1 txtZhuYuanHao;
        private System.Windows.Forms.Label label62;
        private WindowsFormsControlLibrary5.UserControl1 txtBednumber;
        private WindowsFormsControlLibrary5.UserControl1 txtBingQu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}