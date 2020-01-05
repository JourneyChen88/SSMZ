namespace main.药品耗材管理
{
    partial class YaopinManage
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
            this.cmbYpType = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtYPname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvYaowu = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ypName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ypType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameSX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNameSuox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbBagName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmbDW = new System.Windows.Forms.ComboBox();
            this.cmbZRFS = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbYL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbYaowuName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ypname2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zrfs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cxyy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYaowu)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbYpType
            // 
            this.cmbYpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYpType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbYpType.FormattingEnabled = true;
            this.cmbYpType.Location = new System.Drawing.Point(577, 169);
            this.cmbYpType.Name = "cmbYpType";
            this.cmbYpType.Size = new System.Drawing.Size(155, 28);
            this.cmbYpType.TabIndex = 20;
            this.cmbYpType.SelectedIndexChanged += new System.EventHandler(this.cmbYpType_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(674, 212);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(66, 35);
            this.button4.TabIndex = 19;
            this.button4.Text = "删除";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Blue;
            this.button3.Location = new System.Drawing.Point(591, 212);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 35);
            this.button3.TabIndex = 18;
            this.button3.Text = "修改";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(509, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 35);
            this.button1.TabIndex = 17;
            this.button1.Text = "增加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(513, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "药物类型";
            // 
            // txtYPname
            // 
            this.txtYPname.Location = new System.Drawing.Point(577, 80);
            this.txtYPname.Name = "txtYPname";
            this.txtYPname.Size = new System.Drawing.Size(155, 26);
            this.txtYPname.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(513, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "药物名称";
            // 
            // dgvYaowu
            // 
            this.dgvYaowu.AllowUserToAddRows = false;
            this.dgvYaowu.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvYaowu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYaowu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.ypName,
            this.ypType,
            this.NameSX});
            this.dgvYaowu.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvYaowu.Location = new System.Drawing.Point(3, 3);
            this.dgvYaowu.Name = "dgvYaowu";
            this.dgvYaowu.ReadOnly = true;
            this.dgvYaowu.RowHeadersVisible = false;
            this.dgvYaowu.RowTemplate.Height = 23;
            this.dgvYaowu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvYaowu.Size = new System.Drawing.Size(468, 359);
            this.dgvYaowu.TabIndex = 16;
            this.dgvYaowu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvYaowu_CellClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "id";
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // ypName
            // 
            this.ypName.DataPropertyName = "ypname";
            this.ypName.HeaderText = "药物名称";
            this.ypName.Name = "ypName";
            this.ypName.ReadOnly = true;
            this.ypName.Width = 120;
            // 
            // ypType
            // 
            this.ypType.DataPropertyName = "yptype";
            this.ypType.HeaderText = "药物类型";
            this.ypType.Name = "ypType";
            this.ypType.ReadOnly = true;
            this.ypType.Width = 120;
            // 
            // NameSX
            // 
            this.NameSX.DataPropertyName = "NameSX";
            this.NameSX.HeaderText = "药物名缩写";
            this.NameSX.Name = "NameSX";
            this.NameSX.ReadOnly = true;
            this.NameSX.Width = 120;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(842, 398);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.txtNameSuox);
            this.tabPage1.Controls.Add(this.cmbYpType);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.dgvYaowu);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.txtYPname);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(834, 365);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "药品管理";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(513, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "药物缩写";
            // 
            // txtNameSuox
            // 
            this.txtNameSuox.Location = new System.Drawing.Point(577, 120);
            this.txtNameSuox.Name = "txtNameSuox";
            this.txtNameSuox.Size = new System.Drawing.Size(155, 26);
            this.txtNameSuox.TabIndex = 22;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.cmbBagName);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.cmbDW);
            this.tabPage2.Controls.Add(this.cmbZRFS);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.tbYL);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.cmbYaowuName);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(834, 365);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "用药包管理";
            // 
            // cmbBagName
            // 
            this.cmbBagName.FormattingEnabled = true;
            this.cmbBagName.Location = new System.Drawing.Point(615, 136);
            this.cmbBagName.Name = "cmbBagName";
            this.cmbBagName.Size = new System.Drawing.Size(194, 28);
            this.cmbBagName.TabIndex = 39;
            this.cmbBagName.SelectedIndexChanged += new System.EventHandler(this.cmbBagName_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(698, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 20);
            this.label8.TabIndex = 38;
            this.label8.Text = "单位";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(715, 233);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 24);
            this.checkBox1.TabIndex = 37;
            this.checkBox1.Text = "持续用药";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cmbDW
            // 
            this.cmbDW.FormattingEnabled = true;
            this.cmbDW.Items.AddRange(new object[] {
            "mg",
            "ug",
            "ml"});
            this.cmbDW.Location = new System.Drawing.Point(737, 180);
            this.cmbDW.Name = "cmbDW";
            this.cmbDW.Size = new System.Drawing.Size(72, 28);
            this.cmbDW.TabIndex = 36;
            this.cmbDW.Text = "mg";
            // 
            // cmbZRFS
            // 
            this.cmbZRFS.FormattingEnabled = true;
            this.cmbZRFS.Items.AddRange(new object[] {
            "肌注",
            "静注",
            "静脉滴注",
            "静脉泵注",
            "口服"});
            this.cmbZRFS.Location = new System.Drawing.Point(615, 229);
            this.cmbZRFS.Name = "cmbZRFS";
            this.cmbZRFS.Size = new System.Drawing.Size(86, 28);
            this.cmbZRFS.TabIndex = 35;
            this.cmbZRFS.Text = "肌注";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(551, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 34;
            this.label7.Text = "注入方式";
            // 
            // tbYL
            // 
            this.tbYL.Location = new System.Drawing.Point(613, 182);
            this.tbYL.Name = "tbYL";
            this.tbYL.Size = new System.Drawing.Size(77, 26);
            this.tbYL.TabIndex = 33;
            this.tbYL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(573, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 20);
            this.label6.TabIndex = 32;
            this.label6.Text = "用量";
            // 
            // cmbYaowuName
            // 
            this.cmbYaowuName.FormattingEnabled = true;
            this.cmbYaowuName.Location = new System.Drawing.Point(615, 85);
            this.cmbYaowuName.Name = "cmbYaowuName";
            this.cmbYaowuName.Size = new System.Drawing.Size(194, 28);
            this.cmbYaowuName.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(551, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 29;
            this.label5.Text = "药包名称";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.bagName,
            this.ypname2,
            this.yl,
            this.dw,
            this.zrfs,
            this.cxyy});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(516, 359);
            this.dataGridView1.TabIndex = 28;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // bagName
            // 
            this.bagName.DataPropertyName = "bagName";
            this.bagName.HeaderText = "药包名称";
            this.bagName.Name = "bagName";
            this.bagName.ReadOnly = true;
            // 
            // ypname2
            // 
            this.ypname2.DataPropertyName = "ypname";
            this.ypname2.HeaderText = "药物名称";
            this.ypname2.Name = "ypname2";
            this.ypname2.ReadOnly = true;
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
            // zrfs
            // 
            this.zrfs.DataPropertyName = "zrff";
            this.zrfs.HeaderText = "注入方式";
            this.zrfs.Name = "zrfs";
            this.zrfs.ReadOnly = true;
            this.zrfs.Width = 90;
            // 
            // cxyy
            // 
            this.cxyy.DataPropertyName = "cxyy";
            this.cxyy.HeaderText = "持续用药";
            this.cxyy.Name = "cxyy";
            this.cxyy.ReadOnly = true;
            this.cxyy.Width = 90;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(704, 294);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 35);
            this.button2.TabIndex = 26;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Blue;
            this.button5.Location = new System.Drawing.Point(537, 294);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(69, 35);
            this.button5.TabIndex = 25;
            this.button5.Text = "修改";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(551, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 21;
            this.label3.Text = "药物名称";
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.Color.Blue;
            this.button6.Location = new System.Drawing.Point(615, 294);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(66, 35);
            this.button6.TabIndex = 24;
            this.button6.Text = "增加";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // YaopinManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 410);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "YaopinManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "药品和用药包管理";
            this.Load += new System.EventHandler(this.YPguanli_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvYaowu)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbYpType;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtYPname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvYaowu;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbZRFS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbYL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDW;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn bagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ypname2;
        private System.Windows.Forms.DataGridViewTextBoxColumn yl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw;
        private System.Windows.Forms.DataGridViewTextBoxColumn zrfs;
        private System.Windows.Forms.DataGridViewTextBoxColumn cxyy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNameSuox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ypName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ypType;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameSX;
        private System.Windows.Forms.ComboBox cmbYaowuName;

    }
}