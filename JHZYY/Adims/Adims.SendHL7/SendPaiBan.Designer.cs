﻿namespace Adims.SendHL7
{
    partial class SendPaiBan
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
            this.label5 = new System.Windows.Forms.Label();
            this.btnQUERY = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.tbCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtOdate = new System.Windows.Forms.DateTimePicker();
            this.dgvOTypesetting = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Oroom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.second = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patdpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Patbedno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patsex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pattmd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.applyid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatZhuYuanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asae = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOTypesetting)).BeginInit();
            this.SuspendLayout();
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
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // tbCount
            // 
            this.tbCount.Location = new System.Drawing.Point(134, 34);
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(100, 27);
            this.tbCount.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "手术名条数：";
            // 
            // dtOdate
            // 
            this.dtOdate.Location = new System.Drawing.Point(394, 34);
            this.dtOdate.Name = "dtOdate";
            this.dtOdate.Size = new System.Drawing.Size(153, 27);
            this.dtOdate.TabIndex = 19;
            this.dtOdate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dgvOTypesetting
            // 
            this.dgvOTypesetting.AllowUserToAddRows = false;
            this.dgvOTypesetting.AllowUserToDeleteRows = false;
            this.dgvOTypesetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOTypesetting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOTypesetting.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOTypesetting.ColumnHeadersHeight = 25;
            this.dgvOTypesetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Column2,
            this.Oroom,
            this.second,
            this.patdpm,
            this.Patbedno,
            this.patname,
            this.patAge,
            this.patsex,
            this.oname,
            this.pattmd,
            this.OS,
            this.Amethod,
            this.applyid,
            this.Column1,
            this.PatZhuYuanID,
            this.patid,
            this.asae});
            this.dgvOTypesetting.Location = new System.Drawing.Point(13, 74);
            this.dgvOTypesetting.MultiSelect = false;
            this.dgvOTypesetting.Name = "dgvOTypesetting";
            this.dgvOTypesetting.RowHeadersVisible = false;
            this.dgvOTypesetting.RowTemplate.Height = 23;
            this.dgvOTypesetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOTypesetting.Size = new System.Drawing.Size(943, 410);
            this.dgvOTypesetting.TabIndex = 20;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 49;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PatNation";
            this.Column2.HeaderText = "民族";
            this.Column2.Name = "Column2";
            this.Column2.Width = 64;
            // 
            // Oroom
            // 
            this.Oroom.DataPropertyName = "oroom";
            this.Oroom.HeaderText = "手术间";
            this.Oroom.MinimumWidth = 80;
            this.Oroom.Name = "Oroom";
            this.Oroom.Width = 80;
            // 
            // second
            // 
            this.second.DataPropertyName = "Second";
            this.second.HeaderText = "台次";
            this.second.Name = "second";
            this.second.Width = 64;
            // 
            // patdpm
            // 
            this.patdpm.DataPropertyName = "patdpm";
            this.patdpm.HeaderText = "科室";
            this.patdpm.Name = "patdpm";
            this.patdpm.ReadOnly = true;
            this.patdpm.Width = 64;
            // 
            // Patbedno
            // 
            this.Patbedno.DataPropertyName = "Patbedno";
            this.Patbedno.HeaderText = "床号";
            this.Patbedno.Name = "Patbedno";
            this.Patbedno.ReadOnly = true;
            this.Patbedno.Width = 64;
            // 
            // patname
            // 
            this.patname.DataPropertyName = "patname";
            this.patname.HeaderText = "病人姓名";
            this.patname.Name = "patname";
            this.patname.ReadOnly = true;
            this.patname.Width = 94;
            // 
            // patAge
            // 
            this.patAge.DataPropertyName = "patAge";
            this.patAge.HeaderText = "年龄";
            this.patAge.Name = "patAge";
            this.patAge.ReadOnly = true;
            this.patAge.Width = 64;
            // 
            // patsex
            // 
            this.patsex.DataPropertyName = "patsex";
            this.patsex.HeaderText = "性别";
            this.patsex.Name = "patsex";
            this.patsex.ReadOnly = true;
            this.patsex.Width = 64;
            // 
            // oname
            // 
            this.oname.DataPropertyName = "oname";
            this.oname.HeaderText = "手术名称";
            this.oname.Name = "oname";
            this.oname.ReadOnly = true;
            this.oname.Width = 94;
            // 
            // pattmd
            // 
            this.pattmd.DataPropertyName = "pattmd";
            this.pattmd.HeaderText = "术前诊断";
            this.pattmd.Name = "pattmd";
            this.pattmd.ReadOnly = true;
            this.pattmd.Width = 94;
            // 
            // OS
            // 
            this.OS.DataPropertyName = "os";
            this.OS.HeaderText = "手术医生";
            this.OS.Name = "OS";
            this.OS.ReadOnly = true;
            this.OS.Width = 94;
            // 
            // Amethod
            // 
            this.Amethod.DataPropertyName = "Amethod";
            this.Amethod.HeaderText = "麻醉方法";
            this.Amethod.Name = "Amethod";
            this.Amethod.ReadOnly = true;
            this.Amethod.Width = 94;
            // 
            // applyid
            // 
            this.applyid.DataPropertyName = "ApplyId";
            this.applyid.HeaderText = "流水号";
            this.applyid.Name = "applyid";
            this.applyid.ReadOnly = true;
            this.applyid.Visible = false;
            this.applyid.Width = 79;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "CardId";
            this.Column1.HeaderText = "卡号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 64;
            // 
            // PatZhuYuanID
            // 
            this.PatZhuYuanID.DataPropertyName = "PatZhuYuanID";
            this.PatZhuYuanID.HeaderText = "住院号";
            this.PatZhuYuanID.Name = "PatZhuYuanID";
            this.PatZhuYuanID.ReadOnly = true;
            this.PatZhuYuanID.Width = 79;
            // 
            // patid
            // 
            this.patid.DataPropertyName = "PatId";
            this.patid.HeaderText = "病人编号";
            this.patid.Name = "patid";
            this.patid.ReadOnly = true;
            this.patid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.patid.Width = 94;
            // 
            // asae
            // 
            this.asae.DataPropertyName = "asae";
            this.asae.HeaderText = "急诊";
            this.asae.Name = "asae";
            this.asae.Visible = false;
            this.asae.Width = 64;
            // 
            // PaiBanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 496);
            this.Controls.Add(this.dgvOTypesetting);
            this.Controls.Add(this.dtOdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCount);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.btnQUERY);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PaiBanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发送排班信息";
            this.Load += new System.EventHandler(this.DataView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOTypesetting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnQUERY;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.TextBox tbCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtOdate;
        private System.Windows.Forms.DataGridView dgvOTypesetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Oroom;
        private System.Windows.Forms.DataGridViewTextBoxColumn second;
        private System.Windows.Forms.DataGridViewTextBoxColumn patdpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Patbedno;
        private System.Windows.Forms.DataGridViewTextBoxColumn patname;
        private System.Windows.Forms.DataGridViewTextBoxColumn patAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn patsex;
        private System.Windows.Forms.DataGridViewTextBoxColumn oname;
        private System.Windows.Forms.DataGridViewTextBoxColumn pattmd;
        private System.Windows.Forms.DataGridViewTextBoxColumn OS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn applyid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatZhuYuanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn patid;
        private System.Windows.Forms.DataGridViewTextBoxColumn asae;
    }
}

