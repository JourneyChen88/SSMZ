namespace main
{
    partial class MZYYZT
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
            this.dgvMZYYZT = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XMMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YSFY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSFY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KDKS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZXKS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZYFYLB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbDW = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtitem_name = new System.Windows.Forms.TextBox();
            this.cmbGG = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numSL = new System.Windows.Forms.NumericUpDown();
            this.tbPinyin = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.listXMMC = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbYpType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.cmbZT = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMZYYZT)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSL)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMZYYZT
            // 
            this.dgvMZYYZT.AllowUserToAddRows = false;
            this.dgvMZYYZT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMZYYZT.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMZYYZT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMZYYZT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.LB,
            this.XMMC,
            this.GG,
            this.DW,
            this.SL,
            this.DJ,
            this.F,
            this.YSFY,
            this.SSFY,
            this.KDKS,
            this.ZXKS,
            this.ZYFYLB});
            this.dgvMZYYZT.Location = new System.Drawing.Point(247, 47);
            this.dgvMZYYZT.Name = "dgvMZYYZT";
            this.dgvMZYYZT.RowHeadersVisible = false;
            this.dgvMZYYZT.RowTemplate.Height = 23;
            this.dgvMZYYZT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMZYYZT.Size = new System.Drawing.Size(930, 587);
            this.dgvMZYYZT.TabIndex = 17;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "编号";
            this.id.Name = "id";
            this.id.Visible = false;
            this.id.Width = 80;
            // 
            // LB
            // 
            this.LB.DataPropertyName = "LB";
            this.LB.HeaderText = "类别";
            this.LB.Name = "LB";
            this.LB.Width = 120;
            // 
            // XMMC
            // 
            this.XMMC.DataPropertyName = "XMMC";
            this.XMMC.HeaderText = "项目名称";
            this.XMMC.Name = "XMMC";
            this.XMMC.Width = 120;
            // 
            // GG
            // 
            this.GG.DataPropertyName = "GG";
            this.GG.HeaderText = "规格";
            this.GG.Name = "GG";
            // 
            // DW
            // 
            this.DW.DataPropertyName = "DW";
            this.DW.HeaderText = "单位";
            this.DW.Name = "DW";
            // 
            // SL
            // 
            this.SL.DataPropertyName = "SL";
            this.SL.HeaderText = "数量";
            this.SL.Name = "SL";
            // 
            // DJ
            // 
            this.DJ.DataPropertyName = "DJ";
            this.DJ.HeaderText = "单价";
            this.DJ.Name = "DJ";
            // 
            // F
            // 
            this.F.DataPropertyName = "F";
            this.F.HeaderText = "付";
            this.F.Name = "F";
            // 
            // YSFY
            // 
            this.YSFY.DataPropertyName = "YSFY";
            this.YSFY.HeaderText = "应收费用";
            this.YSFY.Name = "YSFY";
            // 
            // SSFY
            // 
            this.SSFY.DataPropertyName = "SSFY";
            this.SSFY.HeaderText = "实收费用";
            this.SSFY.Name = "SSFY";
            // 
            // KDKS
            // 
            this.KDKS.DataPropertyName = "KDKS";
            this.KDKS.HeaderText = "开单科室";
            this.KDKS.Name = "KDKS";
            // 
            // ZXKS
            // 
            this.ZXKS.DataPropertyName = "ZXKS";
            this.ZXKS.HeaderText = "执行科室";
            this.ZXKS.Name = "ZXKS";
            // 
            // ZYFYLB
            // 
            this.ZYFYLB.DataPropertyName = "ZYFYLB";
            this.ZYFYLB.HeaderText = "住院费用类别";
            this.ZYFYLB.Name = "ZYFYLB";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbDW);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtitem_name);
            this.groupBox1.Controls.Add(this.cmbGG);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numSL);
            this.groupBox1.Controls.Add(this.tbPinyin);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.listXMMC);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbYpType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.cmbZT);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(233, 646);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // cmbDW
            // 
            this.cmbDW.FormattingEnabled = true;
            this.cmbDW.Location = new System.Drawing.Point(79, 532);
            this.cmbDW.Name = "cmbDW";
            this.cmbDW.Size = new System.Drawing.Size(68, 20);
            this.cmbDW.TabIndex = 678;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 428);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "项目名称：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 535);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 677;
            this.label7.Text = "单位：";
            // 
            // txtitem_name
            // 
            this.txtitem_name.Location = new System.Drawing.Point(74, 425);
            this.txtitem_name.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtitem_name.Name = "txtitem_name";
            this.txtitem_name.Size = new System.Drawing.Size(114, 21);
            this.txtitem_name.TabIndex = 9;
            // 
            // cmbGG
            // 
            this.cmbGG.FormattingEnabled = true;
            this.cmbGG.Location = new System.Drawing.Point(79, 498);
            this.cmbGG.Name = "cmbGG";
            this.cmbGG.Size = new System.Drawing.Size(68, 20);
            this.cmbGG.TabIndex = 676;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 501);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 675;
            this.label6.Text = "规格：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "拼音检索：";
            // 
            // numSL
            // 
            this.numSL.Location = new System.Drawing.Point(82, 462);
            this.numSL.Name = "numSL";
            this.numSL.Size = new System.Drawing.Size(65, 21);
            this.numSL.TabIndex = 674;
            this.numSL.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tbPinyin
            // 
            this.tbPinyin.Location = new System.Drawing.Point(79, 147);
            this.tbPinyin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbPinyin.Name = "tbPinyin";
            this.tbPinyin.Size = new System.Drawing.Size(114, 21);
            this.tbPinyin.TabIndex = 7;
            this.tbPinyin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPinyin_KeyDown);
            this.tbPinyin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPinyin_KeyPress);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(121, 564);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(66, 35);
            this.button4.TabIndex = 673;
            this.button4.Text = "删除";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listXMMC
            // 
            this.listXMMC.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listXMMC.FormattingEnabled = true;
            this.listXMMC.ItemHeight = 12;
            this.listXMMC.Location = new System.Drawing.Point(12, 176);
            this.listXMMC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listXMMC.Name = "listXMMC";
            this.listXMMC.Size = new System.Drawing.Size(185, 232);
            this.listXMMC.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.button2.Image = global::main.Properties.Resources.Refresh;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(6, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 40);
            this.button2.TabIndex = 668;
            this.button2.Text = "刷新";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(30, 564);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 35);
            this.button1.TabIndex = 671;
            this.button1.Text = "增加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 664;
            this.label1.Text = "药品类别：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 464);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 670;
            this.label3.Text = "数量：";
            // 
            // cmbYpType
            // 
            this.cmbYpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYpType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbYpType.FormattingEnabled = true;
            this.cmbYpType.Location = new System.Drawing.Point(79, 119);
            this.cmbYpType.Name = "cmbYpType";
            this.cmbYpType.Size = new System.Drawing.Size(112, 20);
            this.cmbYpType.TabIndex = 665;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 666;
            this.label4.Text = "组套名称：";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.button5.Location = new System.Drawing.Point(112, 21);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 40);
            this.button5.TabIndex = 669;
            this.button5.Text = "组套管理";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cmbZT
            // 
            this.cmbZT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbZT.FormattingEnabled = true;
            this.cmbZT.Location = new System.Drawing.Point(79, 83);
            this.cmbZT.Name = "cmbZT";
            this.cmbZT.Size = new System.Drawing.Size(112, 20);
            this.cmbZT.TabIndex = 667;
            // 
            // MZYYZT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 646);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvMZYYZT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MZYYZT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "麻醉用药组套管理";
            this.Load += new System.EventHandler(this.MZYYZT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMZYYZT)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMZYYZT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPinyin;
        private System.Windows.Forms.ListBox listXMMC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtitem_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn LB;
        private System.Windows.Forms.DataGridViewTextBoxColumn XMMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn GG;
        private System.Windows.Forms.DataGridViewTextBoxColumn DW;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL;
        private System.Windows.Forms.DataGridViewTextBoxColumn DJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn F;
        private System.Windows.Forms.DataGridViewTextBoxColumn YSFY;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSFY;
        private System.Windows.Forms.DataGridViewTextBoxColumn KDKS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZXKS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZYFYLB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbYpType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cmbZT;
        private System.Windows.Forms.ComboBox cmbDW;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbGG;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numSL;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
    }
}