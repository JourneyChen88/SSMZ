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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.UseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StorageID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContentMerge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrgID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyID1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnInsert = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnQUERY = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSync = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UseID,
            this.StorageID,
            this.Barcode,
            this.ContentMerge,
            this.OPID,
            this.UseDate,
            this.OrgID,
            this.Content,
            this.KeyID1});
            this.dataGridView.Location = new System.Drawing.Point(32, 77);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 27;
            this.dataGridView.Size = new System.Drawing.Size(1149, 436);
            this.dataGridView.TabIndex = 0;
            // 
            // UseID
            // 
            this.UseID.DataPropertyName = "UseID";
            this.UseID.HeaderText = "使用ID";
            this.UseID.Name = "UseID";
            // 
            // StorageID
            // 
            this.StorageID.DataPropertyName = "StorageID";
            this.StorageID.HeaderText = "库存ID";
            this.StorageID.Name = "StorageID";
            // 
            // Barcode
            // 
            this.Barcode.DataPropertyName = "Barcode";
            this.Barcode.HeaderText = "包条码";
            this.Barcode.Name = "Barcode";
            this.Barcode.Width = 200;
            // 
            // ContentMerge
            // 
            this.ContentMerge.DataPropertyName = "ContentMerge";
            this.ContentMerge.HeaderText = "病人关联";
            this.ContentMerge.Name = "ContentMerge";
            this.ContentMerge.Width = 200;
            // 
            // OPID
            // 
            this.OPID.DataPropertyName = "OPID";
            this.OPID.HeaderText = "使用人";
            this.OPID.Name = "OPID";
            // 
            // UseDate
            // 
            this.UseDate.DataPropertyName = "UseDate";
            this.UseDate.HeaderText = "使用时间";
            this.UseDate.Name = "UseDate";
            // 
            // OrgID
            // 
            this.OrgID.DataPropertyName = "OrgID";
            this.OrgID.HeaderText = "使用科室";
            this.OrgID.Name = "OrgID";
            // 
            // Content
            // 
            this.Content.DataPropertyName = "Content";
            this.Content.HeaderText = "使用详细";
            this.Content.Name = "Content";
            this.Content.Width = 200;
            // 
            // KeyID1
            // 
            this.KeyID1.DataPropertyName = "KeyID1";
            this.KeyID1.HeaderText = "关联ID1";
            this.KeyID1.Name = "KeyID1";
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(1054, 28);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(117, 37);
            this.btnInsert.TabIndex = 9;
            this.btnInsert.Text = "插入";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "开始时间：";
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(141, 31);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(200, 27);
            this.dtStart.TabIndex = 11;
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(493, 31);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(200, 27);
            this.dtEnd.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(379, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "开始时间：";
            // 
            // btnQUERY
            // 
            this.btnQUERY.Location = new System.Drawing.Point(742, 30);
            this.btnQUERY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQUERY.Name = "btnQUERY";
            this.btnQUERY.Size = new System.Drawing.Size(117, 37);
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
            this.richTextBox1.Size = new System.Drawing.Size(1149, 258);
            this.richTextBox1.TabIndex = 15;
            this.richTextBox1.Text = "";
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(901, 30);
            this.btnSync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(117, 37);
            this.btnSync.TabIndex = 16;
            this.btnSync.Text = "同步";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // DataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 804);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnQUERY);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.dataGridView);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DataView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "使用日志补录工具";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnQUERY;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UseID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StorageID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContentMerge;
        private System.Windows.Forms.DataGridViewTextBoxColumn OPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UseDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrgID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Content;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyID1;
        private System.Windows.Forms.Button btnSync;
    }
}

