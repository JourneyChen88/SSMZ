namespace Adims.SendHL7
{
    partial class SendOperConfig
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
            this.MzjldId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Odate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AP1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ostate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.btnQUERY = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.tbCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtOdate = new System.Windows.Forms.DateTimePicker();
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
            this.MzjldId,
            this.patid,
            this.PatName,
            this.Odate,
            this.AP1,
            this.UserNo,
            this.Ostate});
            this.dgvOper.Location = new System.Drawing.Point(23, 76);
            this.dgvOper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvOper.Name = "dgvOper";
            this.dgvOper.RowHeadersWidth = 10;
            this.dgvOper.RowTemplate.Height = 27;
            this.dgvOper.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOper.Size = new System.Drawing.Size(902, 424);
            this.dgvOper.TabIndex = 0;
            // 
            // MzjldId
            // 
            this.MzjldId.DataPropertyName = "MzjldId";
            this.MzjldId.HeaderText = "麻醉ID";
            this.MzjldId.Name = "MzjldId";
            // 
            // patid
            // 
            this.patid.DataPropertyName = "patid";
            this.patid.HeaderText = "病人ID";
            this.patid.Name = "patid";
            this.patid.Width = 150;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "姓名";
            this.PatName.Name = "PatName";
            this.PatName.Width = 150;
            // 
            // Odate
            // 
            this.Odate.DataPropertyName = "Odate";
            this.Odate.HeaderText = "手术日期";
            this.Odate.Name = "Odate";
            this.Odate.Width = 200;
            // 
            // AP1
            // 
            this.AP1.DataPropertyName = "AP1";
            this.AP1.HeaderText = "麻醉医生";
            this.AP1.Name = "AP1";
            // 
            // UserNo
            // 
            this.UserNo.DataPropertyName = "UserNo";
            this.UserNo.HeaderText = "医生编号";
            this.UserNo.Name = "UserNo";
            // 
            // Ostate
            // 
            this.Ostate.DataPropertyName = "Ostate";
            this.Ostate.HeaderText = "状态";
            this.Ostate.Name = "Ostate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "手术名条数：";
            // 
            // btnQUERY
            // 
            this.btnQUERY.Location = new System.Drawing.Point(692, 29);
            this.btnQUERY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQUERY.Name = "btnQUERY";
            this.btnQUERY.Size = new System.Drawing.Size(104, 37);
            this.btnQUERY.TabIndex = 14;
            this.btnQUERY.Text = "单条发送";
            this.btnQUERY.UseVisualStyleBackColor = true;
            this.btnQUERY.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(827, 28);
            this.btnSync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(107, 37);
            this.btnSync.TabIndex = 16;
            this.btnSync.Text = "全部发送";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // tbCount
            // 
            this.tbCount.Location = new System.Drawing.Point(256, 34);
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(100, 27);
            this.tbCount.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(377, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "手术名条数：";
            // 
            // dtOdate
            // 
            this.dtOdate.Location = new System.Drawing.Point(497, 34);
            this.dtOdate.Name = "dtOdate";
            this.dtOdate.Size = new System.Drawing.Size(153, 27);
            this.dtOdate.TabIndex = 19;
            this.dtOdate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // OperConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 514);
            this.Controls.Add(this.dtOdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCount);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.btnQUERY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvOper);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "OperConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发送手术信息";
            this.Load += new System.EventHandler(this.DataView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOper)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOper;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnQUERY;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.TextBox tbCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtOdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MzjldId;
        private System.Windows.Forms.DataGridViewTextBoxColumn patid;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Odate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AP1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ostate;
    }
}

