namespace main.OrgBusinessManage
{
    partial class LeaveRegistration
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSumDay = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbEmployeesName = new System.Windows.Forms.ComboBox();
            this.txtLeaveReason = new System.Windows.Forms.TextBox();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbEmployeesNO = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvLeaveRegistration = new System.Windows.Forms.DataGridView();
            this.LeaveRegistrationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeesNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeesName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeaveReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaveRegistration)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtEndTime);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dtEndDate);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtSumDay);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbEmployeesName);
            this.panel1.Controls.Add(this.txtLeaveReason);
            this.panel1.Controls.Add(this.txtStartTime);
            this.panel1.Controls.Add(this.dtStartDate);
            this.panel1.Controls.Add(this.cmbEmployeesNO);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(653, 198);
            this.panel1.TabIndex = 0;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Location = new System.Drawing.Point(441, 35);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(29, 21);
            this.txtEndTime.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(474, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "时";
            // 
            // dtEndDate
            // 
            this.dtEndDate.Location = new System.Drawing.Point(309, 34);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(121, 21);
            this.dtEndDate.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(152, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "天";
            // 
            // txtSumDay
            // 
            this.txtSumDay.Location = new System.Drawing.Point(96, 61);
            this.txtSumDay.Name = "txtSumDay";
            this.txtSumDay.Size = new System.Drawing.Size(50, 21);
            this.txtSumDay.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "共  计：";
            // 
            // cmbEmployeesName
            // 
            this.cmbEmployeesName.FormattingEnabled = true;
            this.cmbEmployeesName.Location = new System.Drawing.Point(314, 5);
            this.cmbEmployeesName.Name = "cmbEmployeesName";
            this.cmbEmployeesName.Size = new System.Drawing.Size(121, 20);
            this.cmbEmployeesName.TabIndex = 10;
            // 
            // txtLeaveReason
            // 
            this.txtLeaveReason.Location = new System.Drawing.Point(96, 93);
            this.txtLeaveReason.Multiline = true;
            this.txtLeaveReason.Name = "txtLeaveReason";
            this.txtLeaveReason.Size = new System.Drawing.Size(482, 91);
            this.txtLeaveReason.TabIndex = 9;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Location = new System.Drawing.Point(238, 34);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(23, 21);
            this.txtStartTime.TabIndex = 8;
            // 
            // dtStartDate
            // 
            this.dtStartDate.Location = new System.Drawing.Point(96, 33);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(121, 21);
            this.dtStartDate.TabIndex = 7;
            // 
            // cmbEmployeesNO
            // 
            this.cmbEmployeesNO.FormattingEnabled = true;
            this.cmbEmployeesNO.Location = new System.Drawing.Point(96, 5);
            this.cmbEmployeesNO.Name = "cmbEmployeesNO";
            this.cmbEmployeesNO.Size = new System.Drawing.Size(121, 20);
            this.cmbEmployeesNO.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "请假原因";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "时  到";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "开始日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "员工姓名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "员工编号";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(61, 207);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "添加";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(158, 207);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(258, 207);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(356, 207);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvLeaveRegistration);
            this.panel2.Location = new System.Drawing.Point(0, 236);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(653, 249);
            this.panel2.TabIndex = 5;
            // 
            // dgvLeaveRegistration
            // 
            this.dgvLeaveRegistration.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            this.dgvLeaveRegistration.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeaveRegistration.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLeaveRegistration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeaveRegistration.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LeaveRegistrationID,
            this.EmployeesNO,
            this.EmployeesName,
            this.StartDate,
            this.StartTime,
            this.EndDate,
            this.EndTime,
            this.SumDay,
            this.LeaveReason});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLeaveRegistration.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLeaveRegistration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLeaveRegistration.Location = new System.Drawing.Point(0, 0);
            this.dgvLeaveRegistration.Name = "dgvLeaveRegistration";
            this.dgvLeaveRegistration.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeaveRegistration.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLeaveRegistration.RowTemplate.Height = 23;
            this.dgvLeaveRegistration.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeaveRegistration.Size = new System.Drawing.Size(653, 249);
            this.dgvLeaveRegistration.TabIndex = 0;
            this.dgvLeaveRegistration.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLeaveRegistration_CellDoubleClick);
            // 
            // LeaveRegistrationID
            // 
            this.LeaveRegistrationID.DataPropertyName = "LeaveRegistrationID";
            this.LeaveRegistrationID.HeaderText = "LeaveRegistrationID";
            this.LeaveRegistrationID.Name = "LeaveRegistrationID";
            this.LeaveRegistrationID.ReadOnly = true;
            this.LeaveRegistrationID.Visible = false;
            // 
            // EmployeesNO
            // 
            this.EmployeesNO.DataPropertyName = "EmployeesNO";
            this.EmployeesNO.HeaderText = "员工编号";
            this.EmployeesNO.Name = "EmployeesNO";
            this.EmployeesNO.ReadOnly = true;
            // 
            // EmployeesName
            // 
            this.EmployeesName.DataPropertyName = "EmployeesName";
            this.EmployeesName.HeaderText = "员工姓名";
            this.EmployeesName.Name = "EmployeesName";
            this.EmployeesName.ReadOnly = true;
            // 
            // StartDate
            // 
            this.StartDate.DataPropertyName = "StartDate";
            this.StartDate.HeaderText = "开始日期";
            this.StartDate.Name = "StartDate";
            this.StartDate.ReadOnly = true;
            // 
            // StartTime
            // 
            this.StartTime.DataPropertyName = "StartTime";
            this.StartTime.HeaderText = "开始时间";
            this.StartTime.Name = "StartTime";
            this.StartTime.ReadOnly = true;
            // 
            // EndDate
            // 
            this.EndDate.DataPropertyName = "EndDate";
            this.EndDate.HeaderText = "结束日期";
            this.EndDate.Name = "EndDate";
            this.EndDate.ReadOnly = true;
            // 
            // EndTime
            // 
            this.EndTime.DataPropertyName = "EndTime";
            this.EndTime.HeaderText = "结束时间";
            this.EndTime.Name = "EndTime";
            this.EndTime.ReadOnly = true;
            // 
            // SumDay
            // 
            this.SumDay.DataPropertyName = "SumDay";
            this.SumDay.HeaderText = "天数";
            this.SumDay.Name = "SumDay";
            this.SumDay.ReadOnly = true;
            // 
            // LeaveReason
            // 
            this.LeaveReason.DataPropertyName = "LeaveReason";
            this.LeaveReason.HeaderText = "原因";
            this.LeaveReason.Name = "LeaveReason";
            this.LeaveReason.ReadOnly = true;
            // 
            // LeaveRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 485);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LeaveRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请假登记";
            this.Load += new System.EventHandler(this.LeaveRegistration_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaveRegistration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbEmployeesName;
        private System.Windows.Forms.TextBox txtLeaveReason;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.DateTimePicker dtStartDate;
        private System.Windows.Forms.ComboBox cmbEmployeesNO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvLeaveRegistration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSumDay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtEndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeaveRegistrationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeesNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeaveReason;
    }
}