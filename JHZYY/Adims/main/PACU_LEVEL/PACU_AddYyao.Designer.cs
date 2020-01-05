namespace main.PACU_LEVEL
{
    partial class PACU_AddYyao
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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjlid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yyname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yyfs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yytime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(332, 291);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 31);
            this.button3.TabIndex = 21;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(445, 291);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 31);
            this.button2.TabIndex = 20;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(214, 291);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 31);
            this.button1.TabIndex = 19;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "肌注",
            "静注",
            "口服"});
            this.comboBox2.Location = new System.Drawing.Point(524, 233);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(54, 20);
            this.comboBox2.TabIndex = 18;
            this.comboBox2.Text = "静注";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(469, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "用药方式";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ml",
            "mg",
            "ug"});
            this.comboBox1.Location = new System.Drawing.Point(429, 231);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(38, 20);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.Text = "mg";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(382, 231);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(46, 21);
            this.textBox2.TabIndex = 15;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "用量";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(214, 231);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 21);
            this.textBox1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "药品名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "开始时间";
            this.label4.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(216, 260);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(103, 21);
            this.dateTimePicker1.TabIndex = 24;
            this.dateTimePicker1.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.mzjlid,
            this.yyname,
            this.yl,
            this.dw,
            this.yyfs,
            this.yytime});
            this.dataGridView1.Location = new System.Drawing.Point(150, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(437, 206);
            this.dataGridView1.TabIndex = 26;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
            "昂丹司琼",
            "依托咪酯",
            "异丙酚",
            "罗库酰胺",
            "阿曲库胺",
            "酮络酸安氨丁三醇",
            "氟比硌芬酯",
            "艾克洛尔",
            "氟马酰",
            "咪痤安定",
            "多巴胺",
            "瑞芬太尼",
            "阿托品",
            "镁托咪定",
            "左旋布比卡因",
            "利多卡因",
            "甲强龙",
            "硝酸甘油"});
            this.listBox1.Location = new System.Drawing.Point(9, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(132, 316);
            this.listBox1.TabIndex = 25;
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click_1);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // mzjlid
            // 
            this.mzjlid.DataPropertyName = "mzjldid";
            this.mzjlid.HeaderText = "麻醉记录单号";
            this.mzjlid.Name = "mzjlid";
            this.mzjlid.ReadOnly = true;
            this.mzjlid.Visible = false;
            this.mzjlid.Width = 80;
            // 
            // yyname
            // 
            this.yyname.DataPropertyName = "yyname";
            this.yyname.HeaderText = "药品名字";
            this.yyname.Name = "yyname";
            this.yyname.ReadOnly = true;
            // 
            // yl
            // 
            this.yl.DataPropertyName = "yl";
            this.yl.HeaderText = "用量";
            this.yl.Name = "yl";
            this.yl.ReadOnly = true;
            this.yl.Width = 60;
            // 
            // dw
            // 
            this.dw.DataPropertyName = "dw";
            this.dw.HeaderText = "单位";
            this.dw.Name = "dw";
            this.dw.ReadOnly = true;
            this.dw.Width = 60;
            // 
            // yyfs
            // 
            this.yyfs.DataPropertyName = "yyfs";
            this.yyfs.HeaderText = "用药方式";
            this.yyfs.Name = "yyfs";
            this.yyfs.ReadOnly = true;
            this.yyfs.Width = 80;
            // 
            // yytime
            // 
            this.yytime.DataPropertyName = "yytime";
            this.yytime.HeaderText = "用药时间";
            this.yytime.Name = "yytime";
            this.yytime.ReadOnly = true;
            this.yytime.Width = 120;
            // 
            // PACU_AddYyao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 341);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PACU_AddYyao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PACU_用药记录";
            this.Load += new System.EventHandler(this.PACU_AddYyao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjlid;
        private System.Windows.Forms.DataGridViewTextBoxColumn yyname;
        private System.Windows.Forms.DataGridViewTextBoxColumn yl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw;
        private System.Windows.Forms.DataGridViewTextBoxColumn yyfs;
        private System.Windows.Forms.DataGridViewTextBoxColumn yytime;
    }
}