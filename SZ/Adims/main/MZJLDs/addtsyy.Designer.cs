namespace main
{
    partial class addtsyy
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
            this.tbName1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbYl1 = new System.Windows.Forms.TextBox();
            this.cmbDW1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbYYFS1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjlid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yyfs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbYYFS2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDW2 = new System.Windows.Forms.ComboBox();
            this.tbYl2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbName2 = new System.Windows.Forms.TextBox();
            this.lab2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.Font = new System.Drawing.Font("宋体", 10F);
            this.listBox1.FormattingEnabled = true;
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
            "缩宫素",
            "左旋布比卡因",
            "利多卡因",
            "甲强龙",
            "硝酸甘油"});
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(149, 260);
            this.listBox1.TabIndex = 0;
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "药品名称";
            // 
            // tbName1
            // 
            this.tbName1.Location = new System.Drawing.Point(146, 266);
            this.tbName1.Name = "tbName1";
            this.tbName1.Size = new System.Drawing.Size(118, 21);
            this.tbName1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "用量";
            // 
            // tbYl1
            // 
            this.tbYl1.Location = new System.Drawing.Point(305, 268);
            this.tbYl1.Name = "tbYl1";
            this.tbYl1.Size = new System.Drawing.Size(57, 21);
            this.tbYl1.TabIndex = 4;
            this.tbYl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // cmbDW1
            // 
            this.cmbDW1.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbDW1.FormattingEnabled = true;
            this.cmbDW1.Items.AddRange(new object[] {
            "mg",
            "u",
            "g",
            "L",
            "ml",
            "ug",
            "ku"});
            this.cmbDW1.Location = new System.Drawing.Point(364, 268);
            this.cmbDW1.Name = "cmbDW1";
            this.cmbDW1.Size = new System.Drawing.Size(38, 21);
            this.cmbDW1.TabIndex = 5;
            this.cmbDW1.Text = "mg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(420, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "用药方式";
            // 
            // cmbYYFS1
            // 
            this.cmbYYFS1.FormattingEnabled = true;
            this.cmbYYFS1.Items.AddRange(new object[] {
            "静脉滴注",
            "静脉推注",
            "静脉泵注",
            "静注",
            "肌注",
            "口服"});
            this.cmbYYFS1.Location = new System.Drawing.Point(479, 267);
            this.cmbYYFS1.Name = "cmbYYFS1";
            this.cmbYYFS1.Size = new System.Drawing.Size(74, 20);
            this.cmbYYFS1.TabIndex = 7;
            this.cmbYYFS1.Text = "静脉推注";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(147, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 31);
            this.button1.TabIndex = 8;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(375, 345);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 31);
            this.button2.TabIndex = 9;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(260, 345);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 31);
            this.button3.TabIndex = 10;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 260);
            this.panel1.TabIndex = 11;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.mzjlid,
            this.name,
            this.yl,
            this.dw,
            this.yyfs,
            this.time});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(149, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(463, 260);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // mzjlid
            // 
            this.mzjlid.DataPropertyName = "mzjldid";
            this.mzjlid.HeaderText = "麻醉单号";
            this.mzjlid.Name = "mzjlid";
            this.mzjlid.Visible = false;
            this.mzjlid.Width = 80;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "药品名字";
            this.name.Name = "name";
            this.name.Width = 120;
            // 
            // yl
            // 
            this.yl.DataPropertyName = "yl";
            this.yl.HeaderText = "用量";
            this.yl.Name = "yl";
            this.yl.Width = 60;
            // 
            // dw
            // 
            this.dw.DataPropertyName = "dw";
            this.dw.HeaderText = "单位";
            this.dw.Name = "dw";
            this.dw.Width = 60;
            // 
            // yyfs
            // 
            this.yyfs.DataPropertyName = "yyfs";
            this.yyfs.HeaderText = "用药方式";
            this.yyfs.Name = "yyfs";
            this.yyfs.Width = 80;
            // 
            // time
            // 
            this.time.DataPropertyName = "time";
            this.time.HeaderText = "用药时间";
            this.time.Name = "time";
            this.time.Width = 120;
            // 
            // cmbYYFS2
            // 
            this.cmbYYFS2.FormattingEnabled = true;
            this.cmbYYFS2.Items.AddRange(new object[] {
            "静脉滴注",
            "静脉泵注",
            "静注",
            "肌注",
            "口服"});
            this.cmbYYFS2.Location = new System.Drawing.Point(479, 309);
            this.cmbYYFS2.Name = "cmbYYFS2";
            this.cmbYYFS2.Size = new System.Drawing.Size(74, 20);
            this.cmbYYFS2.TabIndex = 18;
            this.cmbYYFS2.Text = "静脉滴注";
            this.cmbYYFS2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(420, 313);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "用药方式";
            this.label4.Visible = false;
            // 
            // cmbDW2
            // 
            this.cmbDW2.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbDW2.FormattingEnabled = true;
            this.cmbDW2.Items.AddRange(new object[] {
            "mg",
            "g",
            "ml",
            "L",
            "ug"});
            this.cmbDW2.Location = new System.Drawing.Point(364, 310);
            this.cmbDW2.Name = "cmbDW2";
            this.cmbDW2.Size = new System.Drawing.Size(38, 21);
            this.cmbDW2.TabIndex = 16;
            this.cmbDW2.Text = "mg";
            // 
            // tbYl2
            // 
            this.tbYl2.Location = new System.Drawing.Point(305, 310);
            this.tbYl2.Name = "tbYl2";
            this.tbYl2.Size = new System.Drawing.Size(57, 21);
            this.tbYl2.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 313);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "用量";
            // 
            // tbName2
            // 
            this.tbName2.Location = new System.Drawing.Point(146, 308);
            this.tbName2.Name = "tbName2";
            this.tbName2.Size = new System.Drawing.Size(118, 21);
            this.tbName2.TabIndex = 13;
            // 
            // lab2
            // 
            this.lab2.AutoSize = true;
            this.lab2.Location = new System.Drawing.Point(87, 312);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(53, 12);
            this.lab2.TabIndex = 12;
            this.lab2.Text = "液体名称";
            // 
            // addtsyy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 388);
            this.Controls.Add(this.cmbYYFS2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbDW2);
            this.Controls.Add(this.tbYl2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbName2);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbYYFS1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbDW1);
            this.Controls.Add(this.tbYl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addtsyy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特殊用药";
            this.Load += new System.EventHandler(this.addtsyy_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbYl1;
        private System.Windows.Forms.ComboBox cmbDW1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbYYFS1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjlid;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn yl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw;
        private System.Windows.Forms.DataGridViewTextBoxColumn yyfs;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.ComboBox cmbYYFS2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDW2;
        private System.Windows.Forms.TextBox tbYl2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbName2;
        private System.Windows.Forms.Label lab2;
    }
}