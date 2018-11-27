namespace main.PrivigeManage
{
    partial class szsjGuanli
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
            this.sjName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sjType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYaowu)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbYpType
            // 
            this.cmbYpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYpType.FormattingEnabled = true;
            this.cmbYpType.Items.AddRange(new object[] {
            "常规类",
            "术中类",
            "其他类"});
            this.cmbYpType.Location = new System.Drawing.Point(440, 169);
            this.cmbYpType.Name = "cmbYpType";
            this.cmbYpType.Size = new System.Drawing.Size(155, 20);
            this.cmbYpType.TabIndex = 28;
            this.cmbYpType.SelectedIndexChanged += new System.EventHandler(this.cmbYpType_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(537, 212);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(66, 35);
            this.button4.TabIndex = 27;
            this.button4.Text = "删除";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Blue;
            this.button3.Location = new System.Drawing.Point(454, 212);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 35);
            this.button3.TabIndex = 26;
            this.button3.Text = "修改";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(372, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 35);
            this.button1.TabIndex = 25;
            this.button1.Text = "增加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(376, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "药物类型";
            // 
            // txtYPname
            // 
            this.txtYPname.Location = new System.Drawing.Point(440, 126);
            this.txtYPname.Name = "txtYPname";
            this.txtYPname.Size = new System.Drawing.Size(155, 21);
            this.txtYPname.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(376, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "事件名称";
            // 
            // dgvYaowu
            // 
            this.dgvYaowu.AllowUserToAddRows = false;
            this.dgvYaowu.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvYaowu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYaowu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.sjName,
            this.sjType});
            this.dgvYaowu.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvYaowu.Location = new System.Drawing.Point(0, 0);
            this.dgvYaowu.Name = "dgvYaowu";
            this.dgvYaowu.ReadOnly = true;
            this.dgvYaowu.RowHeadersVisible = false;
            this.dgvYaowu.RowTemplate.Height = 23;
            this.dgvYaowu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvYaowu.Size = new System.Drawing.Size(346, 381);
            this.dgvYaowu.TabIndex = 24;
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
            // sjName
            // 
            this.sjName.DataPropertyName = "name";
            this.sjName.HeaderText = "事件名称";
            this.sjName.Name = "sjName";
            this.sjName.ReadOnly = true;
            this.sjName.Width = 120;
            // 
            // sjType
            // 
            this.sjType.DataPropertyName = "type";
            this.sjType.HeaderText = "事件类型";
            this.sjType.Name = "sjType";
            this.sjType.ReadOnly = true;
            this.sjType.Width = 120;
            // 
            // szsjGuanli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 381);
            this.Controls.Add(this.cmbYpType);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtYPname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvYaowu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "szsjGuanli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "术中事件管理";
            this.Load += new System.EventHandler(this.szsjGuanli_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvYaowu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sjName;
        private System.Windows.Forms.DataGridViewTextBoxColumn sjType;
    }
}