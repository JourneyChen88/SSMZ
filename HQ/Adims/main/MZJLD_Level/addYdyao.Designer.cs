namespace main
{
    partial class addYdyao
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPinyin = new System.Windows.Forms.TextBox();
            this.listYaopin = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbYL = new System.Windows.Forms.TextBox();
            this.cmbDW = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbYYFS = new System.Windows.Forms.ComboBox();
            this.cbCXYY = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBagName = new System.Windows.Forms.ComboBox();
            this.btnBagUse = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yptype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hxb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quanxue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xuejiang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cxyy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kstime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jstime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yyfs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flags = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbPinyin);
            this.groupBox1.Controls.Add(this.listYaopin);
            this.groupBox1.Location = new System.Drawing.Point(14, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(160, 347);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待选药品列表";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "拼音检索";
            // 
            // tbPinyin
            // 
            this.tbPinyin.Location = new System.Drawing.Point(74, 30);
            this.tbPinyin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbPinyin.Name = "tbPinyin";
            this.tbPinyin.Size = new System.Drawing.Size(69, 23);
            this.tbPinyin.TabIndex = 7;
            this.tbPinyin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPinyin_KeyDown);
            this.tbPinyin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPinyin_KeyPress);
            // 
            // listYaopin
            // 
            this.listYaopin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listYaopin.FormattingEnabled = true;
            this.listYaopin.ItemHeight = 17;
            this.listYaopin.Location = new System.Drawing.Point(3, 67);
            this.listYaopin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listYaopin.Name = "listYaopin";
            this.listYaopin.Size = new System.Drawing.Size(154, 276);
            this.listYaopin.TabIndex = 0;
            this.listYaopin.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.groupBox2.Location = new System.Drawing.Point(180, 16);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(621, 347);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "用药记录";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.mzjld,
            this.yptype,
            this.name,
            this.yl,
            this.dw,
            this.hxb,
            this.quanxue,
            this.xuejiang,
            this.cxyy,
            this.kstime,
            this.jstime,
            this.yyfs,
            this.flags});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 19);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(615, 324);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "药品名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(284, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "用量";
            // 
            // tbYL
            // 
            this.tbYL.Location = new System.Drawing.Point(322, 25);
            this.tbYL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbYL.Name = "tbYL";
            this.tbYL.Size = new System.Drawing.Size(69, 23);
            this.tbYL.TabIndex = 5;
            this.tbYL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // cmbDW
            // 
            this.cmbDW.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbDW.FormattingEnabled = true;
            this.cmbDW.Items.AddRange(new object[] {
            "mg",
            "ml",
            "ug",
            "g",
            "u",
            "%",
            "ml/h",
            "mg/h"});
            this.cmbDW.Location = new System.Drawing.Point(399, 25);
            this.cmbDW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbDW.Name = "cmbDW";
            this.cmbDW.Size = new System.Drawing.Size(52, 21);
            this.cmbDW.TabIndex = 6;
            this.cmbDW.Text = "mg";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "用药方式";
            // 
            // cmbYYFS
            // 
            this.cmbYYFS.FormattingEnabled = true;
            this.cmbYYFS.Items.AddRange(new object[] {
            "肌注",
            "静注",
            "吸入",
            "静脉滴注",
            "静脉泵注",
            "口服"});
            this.cmbYYFS.Location = new System.Drawing.Point(145, 63);
            this.cmbYYFS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbYYFS.Name = "cmbYYFS";
            this.cmbYYFS.Size = new System.Drawing.Size(115, 25);
            this.cmbYYFS.TabIndex = 8;
            this.cmbYYFS.Text = "肌注";
            // 
            // cbCXYY
            // 
            this.cbCXYY.AutoSize = true;
            this.cbCXYY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCXYY.Location = new System.Drawing.Point(289, 64);
            this.cbCXYY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbCXYY.Name = "cbCXYY";
            this.cbCXYY.Size = new System.Drawing.Size(75, 21);
            this.cbCXYY.TabIndex = 9;
            this.cbCXYY.Text = "持续用药";
            this.cbCXYY.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(82, 102);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 30);
            this.button1.TabIndex = 10;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(169, 102);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 30);
            this.button2.TabIndex = 11;
            this.button2.Text = "结束";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(360, 102);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 30);
            this.button3.TabIndex = 12;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(449, 102);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(65, 30);
            this.button4.TabIndex = 13;
            this.button4.Text = "关闭";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cmbBagName);
            this.groupBox3.Controls.Add(this.btnBagUse);
            this.groupBox3.Location = new System.Drawing.Point(20, 371);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(157, 149);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "药包使用";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "选择药包:";
            // 
            // cmbBagName
            // 
            this.cmbBagName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBagName.FormattingEnabled = true;
            this.cmbBagName.Items.AddRange(new object[] {
            "气体药",
            "局麻药",
            "诱导药",
            "术前用药",
            "特殊用药",
            "镇痛药",
            "输液",
            "输血"});
            this.cmbBagName.Location = new System.Drawing.Point(24, 50);
            this.cmbBagName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbBagName.Name = "cmbBagName";
            this.cmbBagName.Size = new System.Drawing.Size(113, 25);
            this.cmbBagName.TabIndex = 21;
            // 
            // btnBagUse
            // 
            this.btnBagUse.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBagUse.ForeColor = System.Drawing.Color.Blue;
            this.btnBagUse.Location = new System.Drawing.Point(53, 86);
            this.btnBagUse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBagUse.Name = "btnBagUse";
            this.btnBagUse.Size = new System.Drawing.Size(84, 36);
            this.btnBagUse.TabIndex = 11;
            this.btnBagUse.Text = "使用药包";
            this.btnBagUse.UseVisualStyleBackColor = true;
            this.btnBagUse.Click += new System.EventHandler(this.btnBagUse_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbName);
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.tbYL);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.cmbDW);
            this.groupBox4.Controls.Add(this.cbCXYY);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.cmbYYFS);
            this.groupBox4.Location = new System.Drawing.Point(183, 371);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(575, 149);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "单个用药";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(145, 27);
            this.tbName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(123, 23);
            this.tbName.TabIndex = 15;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Blue;
            this.button5.Location = new System.Drawing.Point(260, 102);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 30);
            this.button5.TabIndex = 14;
            this.button5.Text = "修改用量";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // mzjld
            // 
            this.mzjld.DataPropertyName = "mzjldid";
            this.mzjld.HeaderText = "mzjldid";
            this.mzjld.Name = "mzjld";
            this.mzjld.ReadOnly = true;
            this.mzjld.Visible = false;
            // 
            // yptype
            // 
            this.yptype.DataPropertyName = "yptype";
            this.yptype.HeaderText = "yptype";
            this.yptype.Name = "yptype";
            this.yptype.ReadOnly = true;
            this.yptype.Visible = false;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "药品名";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 80;
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
            // 
            // cxyy
            // 
            this.cxyy.DataPropertyName = "cxyy";
            this.cxyy.HeaderText = "是否持续";
            this.cxyy.Name = "cxyy";
            this.cxyy.ReadOnly = true;
            this.cxyy.Width = 80;
            // 
            // kstime
            // 
            this.kstime.DataPropertyName = "kstime";
            this.kstime.HeaderText = "开始时间";
            this.kstime.Name = "kstime";
            this.kstime.ReadOnly = true;
            this.kstime.Width = 120;
            // 
            // jstime
            // 
            this.jstime.DataPropertyName = "jstime";
            this.jstime.HeaderText = "结束时间";
            this.jstime.Name = "jstime";
            this.jstime.ReadOnly = true;
            this.jstime.Width = 120;
            // 
            // yyfs
            // 
            this.yyfs.DataPropertyName = "yyfs";
            this.yyfs.HeaderText = "用药方式";
            this.yyfs.Name = "yyfs";
            this.yyfs.ReadOnly = true;
            this.yyfs.Width = 80;
            // 
            // flags
            // 
            this.flags.DataPropertyName = "flags";
            this.flags.HeaderText = "标志";
            this.flags.Name = "flags";
            this.flags.ReadOnly = true;
            this.flags.Visible = false;
            this.flags.Width = 60;
            // 
            // addYdyao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 529);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addYdyao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加全麻药";
            this.Load += new System.EventHandler(this.addyt_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listYaopin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbYL;
        private System.Windows.Forms.ComboBox cmbDW;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbYYFS;
        private System.Windows.Forms.CheckBox cbCXYY;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnBagUse;
        private System.Windows.Forms.ComboBox cmbBagName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPinyin;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjld;
        private System.Windows.Forms.DataGridViewTextBoxColumn yptype;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn yl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw;
        private System.Windows.Forms.DataGridViewTextBoxColumn hxb;
        private System.Windows.Forms.DataGridViewTextBoxColumn quanxue;
        private System.Windows.Forms.DataGridViewTextBoxColumn xuejiang;
        private System.Windows.Forms.DataGridViewTextBoxColumn cxyy;
        private System.Windows.Forms.DataGridViewTextBoxColumn kstime;
        private System.Windows.Forms.DataGridViewTextBoxColumn jstime;
        private System.Windows.Forms.DataGridViewTextBoxColumn yyfs;
        private System.Windows.Forms.DataGridViewTextBoxColumn flags;
    }
}