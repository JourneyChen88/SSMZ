namespace main
{
    partial class QXBCodes_Select
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
            this.dtDATE = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rdOld = new System.Windows.Forms.RadioButton();
            this.rdNew = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtDATE
            // 
            this.dtDATE.Location = new System.Drawing.Point(383, 19);
            this.dtDATE.Name = "dtDATE";
            this.dtDATE.Size = new System.Drawing.Size(124, 21);
            this.dtDATE.TabIndex = 17;
            this.dtDATE.ValueChanged += new System.EventHandler(this.dtDATE_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "选择手术日期：";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.Color.Blue;
            this.btnClose.Location = new System.Drawing.Point(441, 260);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 35);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.Blue;
            this.btnOK.Location = new System.Drawing.Point(334, 260);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 35);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rdOld
            // 
            this.rdOld.AutoSize = true;
            this.rdOld.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdOld.Location = new System.Drawing.Point(162, 16);
            this.rdOld.Name = "rdOld";
            this.rdOld.Size = new System.Drawing.Size(97, 24);
            this.rdOld.TabIndex = 13;
            this.rdOld.TabStop = true;
            this.rdOld.Text = "已扫描病人";
            this.rdOld.UseVisualStyleBackColor = true;
            this.rdOld.CheckedChanged += new System.EventHandler(this.rdOld_CheckedChanged);
            // 
            // rdNew
            // 
            this.rdNew.AutoSize = true;
            this.rdNew.Checked = true;
            this.rdNew.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdNew.Location = new System.Drawing.Point(58, 16);
            this.rdNew.Name = "rdNew";
            this.rdNew.Size = new System.Drawing.Size(97, 24);
            this.rdNew.TabIndex = 12;
            this.rdNew.TabStop = true;
            this.rdNew.Text = "未扫描病人";
            this.rdNew.UseVisualStyleBackColor = true;
            this.rdNew.CheckedChanged += new System.EventHandler(this.rdNew_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataGridView1.Location = new System.Drawing.Point(12, 55);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(516, 195);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // QXBCodes_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 322);
            this.Controls.Add(this.dtDATE);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rdOld);
            this.Controls.Add(this.rdNew);
            this.Controls.Add(this.dataGridView1);
            this.Name = "QXBCodes_Select";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "器械包扫描";
            this.Load += new System.EventHandler(this.QXBCodes_Select_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtDATE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rdOld;
        private System.Windows.Forms.RadioButton rdNew;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}