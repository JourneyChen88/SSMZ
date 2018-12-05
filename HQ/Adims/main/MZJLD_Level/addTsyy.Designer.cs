namespace main
{
    partial class addTsyy
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
            this.listYaopin = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbYl = new System.Windows.Forms.TextBox();
            this.cmbDW1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbYYFS1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPinyin = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjlid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hxb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quanxue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xuejiang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yyfs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listYaopin
            // 
            this.listYaopin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listYaopin.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listYaopin.FormattingEnabled = true;
            this.listYaopin.ItemHeight = 17;
            this.listYaopin.Items.AddRange(new object[] {
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
            this.listYaopin.Location = new System.Drawing.Point(3, 71);
            this.listYaopin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listYaopin.Name = "listYaopin";
            this.listYaopin.Size = new System.Drawing.Size(180, 293);
            this.listYaopin.TabIndex = 0;
            this.listYaopin.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "药品名称";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(68, 18);
            this.tbName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(117, 23);
            this.tbName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "用量";
            // 
            // tbYl
            // 
            this.tbYl.Location = new System.Drawing.Point(225, 19);
            this.tbYl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbYl.Name = "tbYl";
            this.tbYl.Size = new System.Drawing.Size(52, 23);
            this.tbYl.TabIndex = 4;
            this.tbYl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // cmbDW1
            // 
            this.cmbDW1.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbDW1.FormattingEnabled = true;
            this.cmbDW1.Items.AddRange(new object[] {
            "mg",
            "ml",
            "ug",
            "ku"});
            this.cmbDW1.Location = new System.Drawing.Point(282, 19);
            this.cmbDW1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbDW1.Name = "cmbDW1";
            this.cmbDW1.Size = new System.Drawing.Size(44, 21);
            this.cmbDW1.TabIndex = 5;
            this.cmbDW1.Text = "mg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(337, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "用药方式";
            // 
            // cmbYYFS1
            // 
            this.cmbYYFS1.FormattingEnabled = true;
            this.cmbYYFS1.Items.AddRange(new object[] {
            "口服",
            "静脉滴注",
            "皮下注射",
            "肌肉注射",
            "静脉注射",
            "皮肉注射"});
            this.cmbYYFS1.Location = new System.Drawing.Point(396, 18);
            this.cmbYYFS1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbYYFS1.Name = "cmbYYFS1";
            this.cmbYYFS1.Size = new System.Drawing.Size(63, 25);
            this.cmbYYFS1.TabIndex = 7;
            this.cmbYYFS1.Text = "口服";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(68, 53);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 8;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(294, 53);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 30);
            this.button2.TabIndex = 9;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(181, 53);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 30);
            this.button3.TabIndex = 10;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            this.hxb,
            this.quanxue,
            this.xuejiang,
            this.yyfs,
            this.time,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 19);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(474, 239);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbPinyin);
            this.groupBox1.Controls.Add(this.listYaopin);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 367);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待选药品列表";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "拼音检索";
            // 
            // tbPinyin
            // 
            this.tbPinyin.Location = new System.Drawing.Point(88, 27);
            this.tbPinyin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbPinyin.Name = "tbPinyin";
            this.tbPinyin.Size = new System.Drawing.Size(69, 23);
            this.tbPinyin.TabIndex = 9;
            this.tbPinyin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPinyin_KeyDown);
            this.tbPinyin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPinyin_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(210, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 261);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已使用药品列表";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tbName);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.tbYl);
            this.groupBox3.Controls.Add(this.cmbDW1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cmbYYFS1);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(210, 288);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(477, 100);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
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
            this.mzjlid.HeaderText = "麻醉单号";
            this.mzjlid.Name = "mzjlid";
            this.mzjlid.ReadOnly = true;
            this.mzjlid.Visible = false;
            this.mzjlid.Width = 80;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "药品名字";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 130;
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
            // hxb
            // 
            this.hxb.DataPropertyName = "hxb";
            this.hxb.HeaderText = "红细胞";
            this.hxb.Name = "hxb";
            this.hxb.ReadOnly = true;
            this.hxb.Visible = false;
            // 
            // quanxue
            // 
            this.quanxue.DataPropertyName = "quanxue";
            this.quanxue.HeaderText = "全血";
            this.quanxue.Name = "quanxue";
            this.quanxue.ReadOnly = true;
            this.quanxue.Visible = false;
            // 
            // xuejiang
            // 
            this.xuejiang.DataPropertyName = "xuejiang";
            this.xuejiang.HeaderText = "血浆";
            this.xuejiang.Name = "xuejiang";
            this.xuejiang.ReadOnly = true;
            this.xuejiang.Visible = false;
            // 
            // yyfs
            // 
            this.yyfs.DataPropertyName = "yyfs";
            this.yyfs.HeaderText = "用药方式";
            this.yyfs.Name = "yyfs";
            this.yyfs.ReadOnly = true;
            this.yyfs.Width = 80;
            // 
            // time
            // 
            this.time.DataPropertyName = "kstime";
            this.time.HeaderText = "用药时间";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            this.time.Width = 130;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "yptype";
            this.Column1.HeaderText = "药品类型";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "cxyy";
            this.Column2.HeaderText = "持续用药";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "jstime";
            this.Column3.HeaderText = "结束时间";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // addTsyy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 404);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addTsyy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "其他用药";
            this.Load += new System.EventHandler(this.addtsyy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listYaopin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbYl;
        private System.Windows.Forms.ComboBox cmbDW1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbYYFS1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPinyin;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjlid;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn yl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw;
        private System.Windows.Forms.DataGridViewTextBoxColumn hxb;
        private System.Windows.Forms.DataGridViewTextBoxColumn quanxue;
        private System.Windows.Forms.DataGridViewTextBoxColumn xuejiang;
        private System.Windows.Forms.DataGridViewTextBoxColumn yyfs;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}