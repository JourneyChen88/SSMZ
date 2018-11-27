namespace InsertSysLogTool
{
    partial class DataView
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvOper = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CutType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuickInput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.btnQUERY = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.tbCount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOper)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOper
            // 
            this.dgvOper.AllowUserToAddRows = false;
            this.dgvOper.AllowUserToDeleteRows = false;
            this.dgvOper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOper.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvOper.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOper.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.OperName,
            this.OperCode,
            this.OperLevel,
            this.CutType,
            this.QuickInput});
            this.dgvOper.Location = new System.Drawing.Point(32, 77);
            this.dgvOper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvOper.Name = "dgvOper";
            this.dgvOper.RowHeadersWidth = 10;
            this.dgvOper.RowTemplate.Height = 27;
            this.dgvOper.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOper.Size = new System.Drawing.Size(902, 436);
            this.dgvOper.TabIndex = 0;
            // 
            // id
            // 
            this.id.DataPropertyName = "ID";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Width = 50;
            // 
            // OperName
            // 
            this.OperName.DataPropertyName = "OperName";
            this.OperName.HeaderText = "手术名称";
            this.OperName.Name = "OperName";
            this.OperName.Width = 200;
            // 
            // OperCode
            // 
            this.OperCode.DataPropertyName = "OperCode";
            this.OperCode.HeaderText = "手术编码";
            this.OperCode.Name = "OperCode";
            this.OperCode.Width = 200;
            // 
            // OperLevel
            // 
            this.OperLevel.DataPropertyName = "OperLevel";
            this.OperLevel.HeaderText = "手术等级";
            this.OperLevel.Name = "OperLevel";
            this.OperLevel.Width = 200;
            // 
            // CutType
            // 
            this.CutType.DataPropertyName = "CutType";
            this.CutType.HeaderText = "切口等级";
            this.CutType.Name = "CutType";
            // 
            // QuickInput
            // 
            this.QuickInput.DataPropertyName = "QuickInput";
            this.QuickInput.HeaderText = "检索码";
            this.QuickInput.Name = "QuickInput";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "手术名条数：";
            // 
            // btnQUERY
            // 
            this.btnQUERY.Location = new System.Drawing.Point(645, 27);
            this.btnQUERY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQUERY.Name = "btnQUERY";
            this.btnQUERY.Size = new System.Drawing.Size(76, 37);
            this.btnQUERY.TabIndex = 14;
            this.btnQUERY.Text = "查询";
            this.btnQUERY.UseVisualStyleBackColor = true;
            this.btnQUERY.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(32, 534);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(902, 258);
            this.richTextBox1.TabIndex = 15;
            this.richTextBox1.Text = "";
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(768, 27);
            this.btnSync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(81, 37);
            this.btnSync.TabIndex = 16;
            this.btnSync.Text = "同步";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // tbCount
            // 
            this.tbCount.Location = new System.Drawing.Point(134, 34);
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(100, 27);
            this.tbCount.TabIndex = 17;
            // 
            // DataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 804);
            this.Controls.Add(this.tbCount);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnQUERY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvOper);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DataView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "使用日志补录工具";
            this.Load += new System.EventHandler(this.DataView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOper)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOper;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnQUERY;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn CutType;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuickInput;
        private System.Windows.Forms.TextBox tbCount;
    }
}

