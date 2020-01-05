namespace main.权限管理
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
            this.cmbSJType = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSJname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvShijian = new System.Windows.Forms.DataGridView();
            this.tbSJSuoxie = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suoxie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShijian)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSJType
            // 
            this.cmbSJType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSJType.FormattingEnabled = true;
            this.cmbSJType.Items.AddRange(new object[] {
            "常规类",
            "术中类",
            "其他类"});
            this.cmbSJType.Location = new System.Drawing.Point(497, 59);
            this.cmbSJType.Name = "cmbSJType";
            this.cmbSJType.Size = new System.Drawing.Size(155, 20);
            this.cmbSJType.TabIndex = 28;
            this.cmbSJType.SelectedIndexChanged += new System.EventHandler(this.cmbYpType_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(589, 184);
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
            this.button3.Location = new System.Drawing.Point(506, 184);
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
            this.button1.Location = new System.Drawing.Point(424, 184);
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
            this.label1.Location = new System.Drawing.Point(433, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "事件类型";
            // 
            // txtSJname
            // 
            this.txtSJname.Location = new System.Drawing.Point(497, 99);
            this.txtSJname.Name = "txtSJname";
            this.txtSJname.Size = new System.Drawing.Size(155, 21);
            this.txtSJname.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(433, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "事件名称";
            // 
            // dgvShijian
            // 
            this.dgvShijian.AllowUserToAddRows = false;
            this.dgvShijian.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvShijian.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShijian.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Name,
            this.suoxie,
            this.Type});
            this.dgvShijian.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvShijian.Location = new System.Drawing.Point(0, 0);
            this.dgvShijian.Name = "dgvShijian";
            this.dgvShijian.ReadOnly = true;
            this.dgvShijian.RowHeadersVisible = false;
            this.dgvShijian.RowTemplate.Height = 23;
            this.dgvShijian.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShijian.Size = new System.Drawing.Size(386, 381);
            this.dgvShijian.TabIndex = 24;
            this.dgvShijian.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvYaowu_CellClick);
            // 
            // tbSJSuoxie
            // 
            this.tbSJSuoxie.Location = new System.Drawing.Point(497, 139);
            this.tbSJSuoxie.Name = "tbSJSuoxie";
            this.tbSJSuoxie.Size = new System.Drawing.Size(155, 21);
            this.tbSJSuoxie.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "事件名简写";
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "编号";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 60;
            // 
            // Name
            // 
            this.Name.DataPropertyName = "name";
            this.Name.HeaderText = "事件名称";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            this.Name.Width = 120;
            // 
            // suoxie
            // 
            this.suoxie.DataPropertyName = "suoxie";
            this.suoxie.HeaderText = "事件名缩写";
            this.suoxie.Name = "suoxie";
            this.suoxie.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "type";
            this.Type.HeaderText = "事件类型";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 80;
            // 
            // szsjGuanli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 381);
            this.Controls.Add(this.tbSJSuoxie);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbSJType);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSJname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvShijian);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "术中事件管理";
            this.Load += new System.EventHandler(this.szsjGuanli_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShijian)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSJType;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSJname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvShijian;
        private System.Windows.Forms.TextBox tbSJSuoxie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn suoxie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
    }
}