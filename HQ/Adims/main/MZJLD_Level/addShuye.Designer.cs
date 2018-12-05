namespace main
{
    partial class addShuye
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button5 = new System.Windows.Forms.Button();
            this.cmbDW = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.tbYL = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbCXYY = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbZRFS = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hxb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quanxue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xuejiang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yyfs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kssj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flags = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(14, 17);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(649, 224);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输液使用记录";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.mzjld,
            this.name,
            this.yl,
            this.dw,
            this.hxb,
            this.quanxue,
            this.xuejiang,
            this.yyfs,
            this.kssj,
            this.Column1,
            this.Column2,
            this.Column3,
            this.flags});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 20);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(643, 200);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Blue;
            this.button5.Location = new System.Drawing.Point(403, 77);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(65, 30);
            this.button5.TabIndex = 6;
            this.button5.Text = "关闭";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cmbDW
            // 
            this.cmbDW.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbDW.FormattingEnabled = true;
            this.cmbDW.Items.AddRange(new object[] {
            "ml",
            "cc",
            "mg",
            "ug",
            "g",
            "u",
            "%",
            "ml/h",
            "mg/h"});
            this.cmbDW.Location = new System.Drawing.Point(334, 17);
            this.cmbDW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbDW.Name = "cmbDW";
            this.cmbDW.Size = new System.Drawing.Size(55, 21);
            this.cmbDW.TabIndex = 4;
            this.cmbDW.Text = "ml";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(323, 77);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(65, 30);
            this.button4.TabIndex = 5;
            this.button4.Text = "删除";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tbYL
            // 
            this.tbYL.Location = new System.Drawing.Point(259, 16);
            this.tbYL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbYL.Name = "tbYL";
            this.tbYL.Size = new System.Drawing.Size(67, 23);
            this.tbYL.TabIndex = 3;
            this.tbYL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbCXYY);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbZRFS);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.cmbDW);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.tbYL);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.cmbName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 245);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(646, 133);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // cbCXYY
            // 
            this.cbCXYY.AutoSize = true;
            this.cbCXYY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCXYY.Location = new System.Drawing.Point(565, 20);
            this.cbCXYY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbCXYY.Name = "cbCXYY";
            this.cbCXYY.Size = new System.Drawing.Size(75, 21);
            this.cbCXYY.TabIndex = 11;
            this.cbCXYY.Text = "持续用药";
            this.cbCXYY.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "注入方式";
            // 
            // cmbZRFS
            // 
            this.cmbZRFS.FormattingEnabled = true;
            this.cmbZRFS.Items.AddRange(new object[] {
            "肌注",
            "静注",
            "静脉滴注",
            "静脉泵注"});
            this.cmbZRFS.Location = new System.Drawing.Point(468, 16);
            this.cmbZRFS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbZRFS.Name = "cmbZRFS";
            this.cmbZRFS.Size = new System.Drawing.Size(72, 25);
            this.cmbZRFS.TabIndex = 8;
            this.cmbZRFS.Text = "肌注";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Blue;
            this.button3.Location = new System.Drawing.Point(243, 77);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 30);
            this.button3.TabIndex = 4;
            this.button3.Text = "结束";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "用量";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(163, 77);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbName
            // 
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Items.AddRange(new object[] {
            "乳酸林格氏液",
            "贺斯",
            "5%葡萄糖液",
            "5%葡萄糖盐液",
            "10%葡萄糖液",
            "20%甘露醇",
            "碳酸氢钠",
            "血安定"});
            this.cmbName.Location = new System.Drawing.Point(69, 16);
            this.cmbName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(144, 25);
            this.cmbName.TabIndex = 1;
            this.cmbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbName_KeyDown);
            this.cmbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "液体名";
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "Id";
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
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "输液名";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 120;
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
            this.yyfs.HeaderText = "注入方式";
            this.yyfs.Name = "yyfs";
            this.yyfs.ReadOnly = true;
            this.yyfs.Width = 80;
            // 
            // kssj
            // 
            this.kssj.DataPropertyName = "kstime";
            this.kssj.HeaderText = "使用时间";
            this.kssj.Name = "kssj";
            this.kssj.ReadOnly = true;
            this.kssj.Width = 120;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "jstime";
            this.Column1.HeaderText = "结束时间";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "yptype";
            this.Column2.HeaderText = "类型";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "cxyy";
            this.Column3.HeaderText = "持续用药";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // flags
            // 
            this.flags.DataPropertyName = "flags";
            this.flags.HeaderText = "标志";
            this.flags.Name = "flags";
            this.flags.ReadOnly = true;
            // 
            // addShuye
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 382);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addShuye";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输液管理--麻醉记录单";
            this.Load += new System.EventHandler(this.addshuye_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cmbDW;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox tbYL;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbZRFS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox cbCXYY;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjld;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn yl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw;
        private System.Windows.Forms.DataGridViewTextBoxColumn hxb;
        private System.Windows.Forms.DataGridViewTextBoxColumn quanxue;
        private System.Windows.Forms.DataGridViewTextBoxColumn xuejiang;
        private System.Windows.Forms.DataGridViewTextBoxColumn yyfs;
        private System.Windows.Forms.DataGridViewTextBoxColumn kssj;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn flags;
    }
}