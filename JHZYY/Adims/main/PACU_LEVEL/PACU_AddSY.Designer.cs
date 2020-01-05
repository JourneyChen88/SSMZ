namespace main.PACU_LEVEL
{
    partial class PACU_AddSY
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
            this.cmbZRFS = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDW = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.kssj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zrff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbZRFS
            // 
            this.cmbZRFS.FormattingEnabled = true;
            this.cmbZRFS.Items.AddRange(new object[] {
            "肌注",
            "静注",
            "静脉滴注",
            ""});
            this.cmbZRFS.Location = new System.Drawing.Point(282, 15);
            this.cmbZRFS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbZRFS.Name = "cmbZRFS";
            this.cmbZRFS.Size = new System.Drawing.Size(62, 25);
            this.cmbZRFS.TabIndex = 8;
            this.cmbZRFS.Text = "肌注";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "用量";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(111, 103);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbName
            // 
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Items.AddRange(new object[] {
            "葡萄糖溶液",
            "盐水"});
            this.cmbName.Location = new System.Drawing.Point(59, 17);
            this.cmbName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(130, 25);
            this.cmbName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(14, 17);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(408, 53);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "液体使用记录";
            this.groupBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "注入方式";
            // 
            // cmbDW
            // 
            this.cmbDW.FormattingEnabled = true;
            this.cmbDW.Items.AddRange(new object[] {
            "ml",
            "cc"});
            this.cmbDW.Location = new System.Drawing.Point(144, 63);
            this.cmbDW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbDW.Name = "cmbDW";
            this.cmbDW.Size = new System.Drawing.Size(45, 25);
            this.cmbDW.TabIndex = 4;
            this.cmbDW.Text = "ml";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 10F);
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(218, 57);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 27);
            this.button4.TabIndex = 5;
            this.button4.Text = "删除";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbZRFS);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.cmbDW);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.cmbName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(17, 74);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(405, 138);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Blue;
            this.button5.Location = new System.Drawing.Point(218, 103);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(74, 27);
            this.button5.TabIndex = 6;
            this.button5.Text = "取消";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(59, 61);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(67, 23);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "液体名";
            // 
            // kssj
            // 
            this.kssj.DataPropertyName = "kssj";
            this.kssj.HeaderText = "使用时间";
            this.kssj.Name = "kssj";
            this.kssj.ReadOnly = true;
            this.kssj.Width = 120;
            // 
            // zrff
            // 
            this.zrff.DataPropertyName = "zrfs";
            this.zrff.HeaderText = "注入方式";
            this.zrff.Name = "zrff";
            this.zrff.ReadOnly = true;
            this.zrff.Width = 80;
            // 
            // dw
            // 
            this.dw.DataPropertyName = "dw";
            this.dw.HeaderText = "单位";
            this.dw.Name = "dw";
            this.dw.ReadOnly = true;
            this.dw.Width = 60;
            // 
            // jl
            // 
            this.jl.DataPropertyName = "jl";
            this.jl.HeaderText = "用量";
            this.jl.Name = "jl";
            this.jl.ReadOnly = true;
            this.jl.Width = 60;
            // 
            // name
            // 
            this.name.DataPropertyName = "shuyename";
            this.name.HeaderText = "输液名";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 120;
            // 
            // mzjld
            // 
            this.mzjld.DataPropertyName = "mzjldid";
            this.mzjld.HeaderText = "mzjldid";
            this.mzjld.Name = "mzjld";
            this.mzjld.ReadOnly = true;
            this.mzjld.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mzjld,
            this.name,
            this.jl,
            this.dw,
            this.zrff,
            this.kssj});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 20);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(402, 29);
            this.dataGridView1.TabIndex = 2;
            // 
            // PACU_AddSY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 247);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PACU_AddSY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PACU输液管理";
            this.Load += new System.EventHandler(this.PACU_AddSY_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbZRFS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDW;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjld;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn jl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw;
        private System.Windows.Forms.DataGridViewTextBoxColumn zrff;
        private System.Windows.Forms.DataGridViewTextBoxColumn kssj;
    }
}