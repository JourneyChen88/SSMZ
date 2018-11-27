namespace main
{
    partial class SelectYSorHS
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
            this.listChoice = new System.Windows.Forms.ListBox();
            this.tbJiansuo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.tbMannul = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listChoice);
            this.groupBox1.Location = new System.Drawing.Point(55, 69);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(214, 411);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待选列表";
            // 
            // listChoice
            // 
            this.listChoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listChoice.FormattingEnabled = true;
            this.listChoice.ItemHeight = 20;
            this.listChoice.Items.AddRange(new object[] {
            "欧阳涛",
            "范铁",
            "祈萌",
            "张祯",
            "步召德",
            "宗祥龙",
            "李子禹",
            "武爱文",
            "王洪义",
            "李明",
            "梁静",
            "黄信孚",
            "孙谊",
            "包全",
            "范照青",
            "王崑",
            "郝纯毅",
            "姚云峰",
            "赵军",
            "吴楠",
            "梁震",
            "陈晋峰",
            "王宏伟",
            "杨越",
            "吴蝻",
            "吴勇教授",
            "冷家华",
            "吴晓江",
            "钱宏刚",
            "刘松岭",
            "金克敏"});
            this.listChoice.Location = new System.Drawing.Point(4, 24);
            this.listChoice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listChoice.Name = "listChoice";
            this.listChoice.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listChoice.Size = new System.Drawing.Size(206, 382);
            this.listChoice.TabIndex = 3;
            // 
            // tbJiansuo
            // 
            this.tbJiansuo.Location = new System.Drawing.Point(109, 35);
            this.tbJiansuo.Name = "tbJiansuo";
            this.tbJiansuo.Size = new System.Drawing.Size(143, 26);
            this.tbJiansuo.TabIndex = 12;
            this.tbJiansuo.Visible = false;
            this.tbJiansuo.TextChanged += new System.EventHandler(this.tbJiansuo_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "拼音检索：";
            this.label4.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvSelected);
            this.groupBox2.Location = new System.Drawing.Point(383, 69);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(242, 411);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已选列表";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(298, 285);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 33);
            this.button2.TabIndex = 15;
            this.button2.Text = "<<<";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 202);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 33);
            this.button1.TabIndex = 14;
            this.button1.Text = ">>>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnConfig
            // 
            this.btnConfig.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnConfig.Location = new System.Drawing.Point(557, 35);
            this.btnConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(66, 25);
            this.btnConfig.TabIndex = 18;
            this.btnConfig.Text = "确认添加";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // tbMannul
            // 
            this.tbMannul.Location = new System.Drawing.Point(452, 35);
            this.tbMannul.Name = "tbMannul";
            this.tbMannul.Size = new System.Drawing.Size(99, 26);
            this.tbMannul.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(390, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "手动输入";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(298, 362);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(67, 33);
            this.button3.TabIndex = 19;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSelected.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelected.Location = new System.Drawing.Point(4, 24);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.RowHeadersWidth = 11;
            this.dgvSelected.RowTemplate.Height = 23;
            this.dgvSelected.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelected.Size = new System.Drawing.Size(234, 382);
            this.dgvSelected.TabIndex = 14;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "DoctorName";
            this.Column1.HeaderText = "医生";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "DoctorNo";
            this.Column2.HeaderText = "编号";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // SelectYSorHS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 504);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.tbMannul);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tbJiansuo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "SelectYSorHS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "麻醉医生和护士选择";
            this.Load += new System.EventHandler(this.SelectYSorHS_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbJiansuo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listChoice;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.TextBox tbMannul;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dgvSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}