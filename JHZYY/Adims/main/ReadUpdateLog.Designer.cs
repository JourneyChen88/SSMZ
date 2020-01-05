namespace main
{
    partial class ReadUpdateLog
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
            this.dtOprDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.lbSSMC = new System.Windows.Forms.Label();
            this.lbMZFF = new System.Windows.Forms.Label();
            this.mzjldid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xgdTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xgdlx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xgqValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xghValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xgry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.SuspendLayout();
            // 
            // dtOprDate
            // 
            this.dtOprDate.Location = new System.Drawing.Point(523, 27);
            this.dtOprDate.Name = "dtOprDate";
            this.dtOprDate.Size = new System.Drawing.Size(130, 23);
            this.dtOprDate.TabIndex = 0;
            this.dtOprDate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(450, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "手术日期：";
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mzjldid,
            this.patname,
            this.xgdTime,
            this.xgdlx,
            this.xgqValue,
            this.xghValue,
            this.xgry});
            this.dgvLog.GridColor = System.Drawing.SystemColors.Control;
            this.dgvLog.Location = new System.Drawing.Point(22, 85);
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowTemplate.Height = 23;
            this.dgvLog.Size = new System.Drawing.Size(693, 403);
            this.dgvLog.TabIndex = 2;
            this.dgvLog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLog_CellClick);
            // 
            // lbSSMC
            // 
            this.lbSSMC.AutoSize = true;
            this.lbSSMC.Location = new System.Drawing.Point(35, 18);
            this.lbSSMC.Name = "lbSSMC";
            this.lbSSMC.Size = new System.Drawing.Size(68, 17);
            this.lbSSMC.TabIndex = 4;
            this.lbSSMC.Text = "手术名称：";
            // 
            // lbMZFF
            // 
            this.lbMZFF.AutoSize = true;
            this.lbMZFF.Location = new System.Drawing.Point(35, 50);
            this.lbMZFF.Name = "lbMZFF";
            this.lbMZFF.Size = new System.Drawing.Size(68, 17);
            this.lbMZFF.TabIndex = 5;
            this.lbMZFF.Text = "麻醉方法：";
            // 
            // mzjldid
            // 
            this.mzjldid.DataPropertyName = "mzjldid";
            this.mzjldid.HeaderText = "麻醉编号";
            this.mzjldid.Name = "mzjldid";
            this.mzjldid.ReadOnly = true;
            // 
            // patname
            // 
            this.patname.DataPropertyName = "patname";
            this.patname.HeaderText = "病人姓名";
            this.patname.Name = "patname";
            this.patname.ReadOnly = true;
            this.patname.Visible = false;
            // 
            // xgdTime
            // 
            this.xgdTime.DataPropertyName = "xgdTime";
            this.xgdTime.HeaderText = "修改点时间";
            this.xgdTime.Name = "xgdTime";
            this.xgdTime.ReadOnly = true;
            this.xgdTime.Width = 120;
            // 
            // xgdlx
            // 
            this.xgdlx.DataPropertyName = "xgdlx";
            this.xgdlx.HeaderText = "修改点类型";
            this.xgdlx.Name = "xgdlx";
            this.xgdlx.ReadOnly = true;
            // 
            // xgqValue
            // 
            this.xgqValue.DataPropertyName = "xgqValue";
            this.xgqValue.HeaderText = "修改前值";
            this.xgqValue.Name = "xgqValue";
            this.xgqValue.ReadOnly = true;
            // 
            // xghValue
            // 
            this.xghValue.DataPropertyName = "xghValue";
            this.xghValue.HeaderText = "修改后值";
            this.xghValue.Name = "xghValue";
            this.xghValue.ReadOnly = true;
            // 
            // xgry
            // 
            this.xgry.DataPropertyName = "xgry";
            this.xgry.HeaderText = "修改人";
            this.xgry.Name = "xgry";
            this.xgry.ReadOnly = true;
            // 
            // ReadUpdateLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 511);
            this.Controls.Add(this.lbMZFF);
            this.Controls.Add(this.lbSSMC);
            this.Controls.Add(this.dgvLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtOprDate);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ReadUpdateLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查阅修改日志";
            this.Load += new System.EventHandler(this.ReadUpdateLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtOprDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.Label lbSSMC;
        private System.Windows.Forms.Label lbMZFF;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzjldid;
        private System.Windows.Forms.DataGridViewTextBoxColumn patname;
        private System.Windows.Forms.DataGridViewTextBoxColumn xgdTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn xgdlx;
        private System.Windows.Forms.DataGridViewTextBoxColumn xgqValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn xghValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn xgry;
    }
}