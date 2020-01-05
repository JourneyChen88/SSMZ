namespace main
{
    partial class AddOperName
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
            this.dgvOperList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.OperName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operlevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CutType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuickInput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperList)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOperList
            // 
            this.dgvOperList.AllowUserToAddRows = false;
            this.dgvOperList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOperList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOperList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OperName,
            this.OperCode,
            this.Operlevel,
            this.CutType,
            this.QuickInput});
            this.dgvOperList.Location = new System.Drawing.Point(12, 51);
            this.dgvOperList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvOperList.MultiSelect = false;
            this.dgvOperList.Name = "dgvOperList";
            this.dgvOperList.ReadOnly = true;
            this.dgvOperList.RowHeadersWidth = 11;
            this.dgvOperList.RowTemplate.Height = 23;
            this.dgvOperList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOperList.Size = new System.Drawing.Size(393, 406);
            this.dgvOperList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "输入检索：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 16);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(313, 23);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(425, 95);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 37);
            this.button1.TabIndex = 3;
            this.button1.Text = "添加 >>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(425, 167);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 34);
            this.button2.TabIndex = 4;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvSelected);
            this.groupBox1.Location = new System.Drawing.Point(533, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(326, 441);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已添加手术列表";
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.id});
            this.dgvSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelected.Location = new System.Drawing.Point(3, 20);
            this.dgvSelected.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvSelected.MultiSelect = false;
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.ReadOnly = true;
            this.dgvSelected.RowHeadersWidth = 11;
            this.dgvSelected.RowTemplate.Height = 23;
            this.dgvSelected.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelected.Size = new System.Drawing.Size(320, 417);
            this.dgvSelected.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "OperName";
            this.Column1.HeaderText = "手术名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "OperCode";
            this.Column2.HeaderText = "手术编码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(425, 229);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 37);
            this.button3.TabIndex = 6;
            this.button3.Text = "删除 <<";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // OperName
            // 
            this.OperName.DataPropertyName = "OperName";
            this.OperName.HeaderText = "手术名";
            this.OperName.Name = "OperName";
            this.OperName.ReadOnly = true;
            this.OperName.Width = 200;
            // 
            // OperCode
            // 
            this.OperCode.DataPropertyName = "OperCode";
            this.OperCode.HeaderText = "编码";
            this.OperCode.Name = "OperCode";
            this.OperCode.ReadOnly = true;
            // 
            // Operlevel
            // 
            this.Operlevel.DataPropertyName = "Operlevel";
            this.Operlevel.HeaderText = "等级";
            this.Operlevel.Name = "Operlevel";
            this.Operlevel.ReadOnly = true;
            // 
            // CutType
            // 
            this.CutType.DataPropertyName = "CutType";
            this.CutType.HeaderText = "切口类型";
            this.CutType.Name = "CutType";
            this.CutType.ReadOnly = true;
            // 
            // QuickInput
            // 
            this.QuickInput.DataPropertyName = "QuickInput";
            this.QuickInput.HeaderText = "QuickInput";
            this.QuickInput.Name = "QuickInput";
            this.QuickInput.ReadOnly = true;
            this.QuickInput.Visible = false;
            // 
            // AddOperName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 470);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvOperList);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOperName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手术添加";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddOperName_FormClosed);
            this.Load += new System.EventHandler(this.osel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOperList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvSelected;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operlevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn CutType;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuickInput;
    }
}