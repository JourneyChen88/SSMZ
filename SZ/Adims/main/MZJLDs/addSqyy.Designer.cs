namespace main
{
    partial class addSqyy
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbYpName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbYyfs = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbYl = new System.Windows.Forms.TextBox();
            this.cmbDw = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dtUseTime = new System.Windows.Forms.DateTimePicker();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ypname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yyff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 19;
            this.listBox1.Items.AddRange(new object[] {
            "异氟醚",
            "地氟醚",
            "异氟烷",
            "佳乐施",
            "万汶",
            "复方乳酸钠山梨醇",
            "复方氢化钠",
            "乳酸林格(500)",
            "盐水(0.9%)",
            "氯胺酮"});
            this.listBox1.Location = new System.Drawing.Point(4, 32);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(217, 384);
            this.listBox1.TabIndex = 0;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 255);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "药品名称";
            // 
            // tbYpName
            // 
            this.tbYpName.BackColor = System.Drawing.SystemColors.Window;
            this.tbYpName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbYpName.Location = new System.Drawing.Point(322, 252);
            this.tbYpName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbYpName.Name = "tbYpName";
            this.tbYpName.Size = new System.Drawing.Size(360, 25);
            this.tbYpName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 294);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "用药方式";
            // 
            // cmbYyfs
            // 
            this.cmbYyfs.FormattingEnabled = true;
            this.cmbYyfs.Items.AddRange(new object[] {
            "静脉滴注",
            "肌注",
            "静注",
            "静脉推注",
            "静脉泵注",
            "口服"});
            this.cmbYyfs.Location = new System.Drawing.Point(322, 294);
            this.cmbYyfs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbYyfs.Name = "cmbYyfs";
            this.cmbYyfs.Size = new System.Drawing.Size(145, 27);
            this.cmbYyfs.TabIndex = 4;
            this.cmbYyfs.Text = "静注";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(486, 297);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "用量";
            // 
            // tbYl
            // 
            this.tbYl.Location = new System.Drawing.Point(535, 295);
            this.tbYl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbYl.Name = "tbYl";
            this.tbYl.Size = new System.Drawing.Size(73, 25);
            this.tbYl.TabIndex = 6;
            this.tbYl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // cmbDw
            // 
            this.cmbDw.FormattingEnabled = true;
            this.cmbDw.Items.AddRange(new object[] {
            "ml",
            "g",
            "mg",
            "L",
            "ug"});
            this.cmbDw.Location = new System.Drawing.Point(616, 295);
            this.cmbDw.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbDw.Name = "cmbDw";
            this.cmbDw.Size = new System.Drawing.Size(64, 27);
            this.cmbDw.TabIndex = 7;
            this.cmbDw.Text = "mg";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(390, 369);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 34);
            this.button1.TabIndex = 8;
            this.button1.Text = "使用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(494, 369);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 34);
            this.button2.TabIndex = 9;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "待选项目";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "已添加";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.mzjld,
            this.ypname,
            this.yl,
            this.dw,
            this.yyff,
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(240, 32);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(442, 204);
            this.dataGridView1.TabIndex = 13;
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Blue;
            this.button3.Location = new System.Drawing.Point(605, 369);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 34);
            this.button3.TabIndex = 14;
            this.button3.Text = "退出";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.Color.Blue;
            this.button4.Location = new System.Drawing.Point(240, 369);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(99, 34);
            this.button4.TabIndex = 15;
            this.button4.Text = "添加常规药";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(245, 333);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "用药时间";
            // 
            // dtUseTime
            // 
            this.dtUseTime.CustomFormat = "\"yyyy-MM-dd HH:mm\"";
            this.dtUseTime.Location = new System.Drawing.Point(322, 331);
            this.dtUseTime.Name = "dtUseTime";
            this.dtUseTime.Size = new System.Drawing.Size(358, 25);
            this.dtUseTime.TabIndex = 17;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // mzjld
            // 
            this.mzjld.DataPropertyName = "mzjldid";
            this.mzjld.HeaderText = "mzjld";
            this.mzjld.Name = "mzjld";
            this.mzjld.ReadOnly = true;
            this.mzjld.Visible = false;
            // 
            // ypname
            // 
            this.ypname.DataPropertyName = "ypname";
            this.ypname.HeaderText = "药物名";
            this.ypname.Name = "ypname";
            this.ypname.ReadOnly = true;
            this.ypname.Width = 87;
            // 
            // yl
            // 
            this.yl.DataPropertyName = "yl";
            this.yl.HeaderText = "剂量";
            this.yl.Name = "yl";
            this.yl.ReadOnly = true;
            this.yl.Width = 87;
            // 
            // dw
            // 
            this.dw.DataPropertyName = "dw";
            this.dw.HeaderText = "单位";
            this.dw.Name = "dw";
            this.dw.ReadOnly = true;
            this.dw.Width = 87;
            // 
            // yyff
            // 
            this.yyff.DataPropertyName = "yyfs";
            this.yyff.HeaderText = "使用方式";
            this.yyff.Name = "yyff";
            this.yyff.ReadOnly = true;
            this.yyff.Width = 87;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "UseTime";
            this.Column1.HeaderText = "使用时间";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // addSqyy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 429);
            this.Controls.Add(this.dtUseTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbDw);
            this.Controls.Add(this.tbYl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbYyfs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbYpName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addSqyy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "术前用药";
            this.Load += new System.EventHandler(this.sqyy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbYpName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbYyfs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbYl;
        private System.Windows.Forms.ComboBox cmbDw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtUseTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjld;
        private System.Windows.Forms.DataGridViewTextBoxColumn ypname;
        private System.Windows.Forms.DataGridViewTextBoxColumn yl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw;
        private System.Windows.Forms.DataGridViewTextBoxColumn yyff;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}