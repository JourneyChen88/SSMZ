namespace main
{
    partial class ZoomTimeSet
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.btnADD = new System.Windows.Forms.Button();
            this.dgvZoomTime = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzjldid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tbInterval = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgvZoomTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(479, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 75;
            this.label2.Text = "结束时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(479, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 74;
            this.label1.Text = "开始时间";
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(538, 95);
            this.dtEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(183, 23);
            this.dtEnd.TabIndex = 72;
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(538, 43);
            this.dtStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(183, 23);
            this.dtStart.TabIndex = 71;
            // 
            // btnADD
            // 
            this.btnADD.Location = new System.Drawing.Point(646, 199);
            this.btnADD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(75, 31);
            this.btnADD.TabIndex = 70;
            this.btnADD.Text = "添加";
            this.btnADD.UseVisualStyleBackColor = true;
            this.btnADD.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvZoomTime
            // 
            this.dgvZoomTime.AllowUserToAddRows = false;
            this.dgvZoomTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvZoomTime.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvZoomTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZoomTime.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.mzjldid,
            this.StartTime,
            this.EndTime,
            this.Interval});
            this.dgvZoomTime.Location = new System.Drawing.Point(8, 12);
            this.dgvZoomTime.Name = "dgvZoomTime";
            this.dgvZoomTime.RowTemplate.Height = 23;
            this.dgvZoomTime.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvZoomTime.Size = new System.Drawing.Size(465, 375);
            this.dgvZoomTime.TabIndex = 76;
            this.dgvZoomTime.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvZoomTime_CellClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "ID";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // mzjldid
            // 
            this.mzjldid.DataPropertyName = "mzjldID";
            this.mzjldid.HeaderText = "麻醉编号";
            this.mzjldid.Name = "mzjldid";
            this.mzjldid.Width = 80;
            // 
            // StartTime
            // 
            this.StartTime.DataPropertyName = "StartTime";
            this.StartTime.HeaderText = "开始时间";
            this.StartTime.Name = "StartTime";
            this.StartTime.ReadOnly = true;
            this.StartTime.Width = 120;
            // 
            // EndTime
            // 
            this.EndTime.DataPropertyName = "EndTime";
            this.EndTime.HeaderText = "结束时间";
            this.EndTime.Name = "EndTime";
            this.EndTime.ReadOnly = true;
            this.EndTime.Width = 120;
            // 
            // Interval
            // 
            this.Interval.DataPropertyName = "Interval";
            this.Interval.HeaderText = "间隔时间";
            this.Interval.Name = "Interval";
            this.Interval.Width = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(479, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 77;
            this.label3.Text = "间隔分钟";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(538, 199);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 31);
            this.btnDelete.TabIndex = 79;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tbInterval
            // 
            this.tbInterval.Location = new System.Drawing.Point(538, 149);
            this.tbInterval.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tbInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(183, 23);
            this.tbInterval.TabIndex = 80;
            this.tbInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ZoomTimeSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 399);
            this.Controls.Add(this.tbInterval);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvZoomTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.btnADD);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ZoomTimeSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "间隔时间设置";
            this.Load += new System.EventHandler(this.ZoomTimeSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvZoomTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Button btnADD;
        private System.Windows.Forms.DataGridView dgvZoomTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjldid;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interval;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.NumericUpDown tbInterval;
    }
}