namespace main
{
    partial class menzhenfashidan
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.menzhenhao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvJizhenPaiban = new System.Windows.Forms.DataGridView();
            this.patid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menzhenhaocishu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tizhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mingzu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oroom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.second = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patdpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patsex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.on1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.on2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pattmd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJizhenPaiban)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(108, 42);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(152, 21);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(321, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "按门诊号查询";
            // 
            // menzhenhao
            // 
            this.menzhenhao.Location = new System.Drawing.Point(404, 42);
            this.menzhenhao.Name = "menzhenhao";
            this.menzhenhao.Size = new System.Drawing.Size(147, 21);
            this.menzhenhao.TabIndex = 2;
            this.menzhenhao.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "门诊评估日期:";
            // 
            // dgvJizhenPaiban
            // 
            this.dgvJizhenPaiban.AllowUserToAddRows = false;
            this.dgvJizhenPaiban.AllowUserToDeleteRows = false;
            this.dgvJizhenPaiban.AllowUserToResizeRows = false;
            this.dgvJizhenPaiban.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJizhenPaiban.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvJizhenPaiban.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvJizhenPaiban.ColumnHeadersHeight = 25;
            this.dgvJizhenPaiban.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.patid,
            this.menzhenhaocishu,
            this.tizhong,
            this.mingzu,
            this.oroom,
            this.second,
            this.StartTime,
            this.patdpm,
            this.patname,
            this.patsex,
            this.patage,
            this.bedNo,
            this.on1,
            this.on2,
            this.sn1,
            this.sn2,
            this.os,
            this.ap1,
            this.pattmd,
            this.oname,
            this.amethod,
            this.remarks,
            this.GR});
            this.dgvJizhenPaiban.Location = new System.Drawing.Point(33, 93);
            this.dgvJizhenPaiban.MultiSelect = false;
            this.dgvJizhenPaiban.Name = "dgvJizhenPaiban";
            this.dgvJizhenPaiban.RowHeadersVisible = false;
            this.dgvJizhenPaiban.RowTemplate.Height = 23;
            this.dgvJizhenPaiban.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJizhenPaiban.Size = new System.Drawing.Size(668, 266);
            this.dgvJizhenPaiban.TabIndex = 4;
            // 
            // patid
            // 
            this.patid.DataPropertyName = "menzhenhao";
            this.patid.Frozen = true;
            this.patid.HeaderText = "门诊号";
            this.patid.Name = "patid";
            this.patid.ReadOnly = true;
            this.patid.Width = 66;
            // 
            // menzhenhaocishu
            // 
            this.menzhenhaocishu.DataPropertyName = "menzhenhaocishu";
            this.menzhenhaocishu.HeaderText = "门诊次数";
            this.menzhenhaocishu.Name = "menzhenhaocishu";
            this.menzhenhaocishu.Width = 78;
            // 
            // tizhong
            // 
            this.tizhong.DataPropertyName = "tizhong";
            this.tizhong.HeaderText = "体重";
            this.tizhong.Name = "tizhong";
            this.tizhong.Width = 54;
            // 
            // mingzu
            // 
            this.mingzu.DataPropertyName = "mingzu";
            this.mingzu.HeaderText = "民族";
            this.mingzu.Name = "mingzu";
            this.mingzu.Width = 54;
            // 
            // oroom
            // 
            this.oroom.HeaderText = "手术间";
            this.oroom.Name = "oroom";
            this.oroom.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.oroom.Visible = false;
            this.oroom.Width = 66;
            // 
            // second
            // 
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.second.DefaultCellStyle = dataGridViewCellStyle1;
            this.second.HeaderText = "台次";
            this.second.Name = "second";
            this.second.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.second.Visible = false;
            this.second.Width = 54;
            // 
            // StartTime
            // 
            this.StartTime.DataPropertyName = "dengjishijian";
            this.StartTime.HeaderText = "登记时间";
            this.StartTime.Name = "StartTime";
            this.StartTime.Visible = false;
            this.StartTime.Width = 78;
            // 
            // patdpm
            // 
            this.patdpm.DataPropertyName = "keshi";
            this.patdpm.HeaderText = "科室";
            this.patdpm.Name = "patdpm";
            this.patdpm.ReadOnly = true;
            this.patdpm.Width = 54;
            // 
            // patname
            // 
            this.patname.DataPropertyName = "xingming";
            this.patname.HeaderText = "病人姓名";
            this.patname.Name = "patname";
            this.patname.ReadOnly = true;
            this.patname.Width = 78;
            // 
            // patsex
            // 
            this.patsex.DataPropertyName = "sex";
            this.patsex.HeaderText = "性别";
            this.patsex.Name = "patsex";
            this.patsex.ReadOnly = true;
            this.patsex.Width = 54;
            // 
            // patage
            // 
            this.patage.DataPropertyName = "age";
            this.patage.HeaderText = "年龄";
            this.patage.Name = "patage";
            this.patage.ReadOnly = true;
            this.patage.Width = 54;
            // 
            // bedNo
            // 
            this.bedNo.HeaderText = "床号";
            this.bedNo.Name = "bedNo";
            this.bedNo.ReadOnly = true;
            this.bedNo.Visible = false;
            this.bedNo.Width = 54;
            // 
            // on1
            // 
            this.on1.HeaderText = "洗手护士1";
            this.on1.Name = "on1";
            this.on1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.on1.Visible = false;
            this.on1.Width = 84;
            // 
            // on2
            // 
            this.on2.HeaderText = "洗手护士2";
            this.on2.Name = "on2";
            this.on2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.on2.Visible = false;
            this.on2.Width = 84;
            // 
            // sn1
            // 
            this.sn1.HeaderText = "巡回护士1";
            this.sn1.Name = "sn1";
            this.sn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sn1.Visible = false;
            this.sn1.Width = 84;
            // 
            // sn2
            // 
            this.sn2.HeaderText = "巡回护士2";
            this.sn2.Name = "sn2";
            this.sn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sn2.Visible = false;
            this.sn2.Width = 84;
            // 
            // os
            // 
            this.os.HeaderText = "手术医生";
            this.os.Name = "os";
            this.os.ReadOnly = true;
            this.os.Visible = false;
            this.os.Width = 78;
            // 
            // ap1
            // 
            this.ap1.HeaderText = "麻醉医生";
            this.ap1.Name = "ap1";
            this.ap1.Visible = false;
            this.ap1.Width = 78;
            // 
            // pattmd
            // 
            this.pattmd.DataPropertyName = "zhenduan";
            this.pattmd.HeaderText = "术前诊断";
            this.pattmd.Name = "pattmd";
            this.pattmd.ReadOnly = true;
            this.pattmd.Width = 78;
            // 
            // oname
            // 
            this.oname.HeaderText = "手术名字";
            this.oname.Name = "oname";
            this.oname.ReadOnly = true;
            this.oname.Visible = false;
            this.oname.Width = 78;
            // 
            // amethod
            // 
            this.amethod.DataPropertyName = "mazuifangshi";
            this.amethod.HeaderText = "麻醉方法";
            this.amethod.Name = "amethod";
            this.amethod.Width = 78;
            // 
            // remarks
            // 
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.Visible = false;
            this.remarks.Width = 54;
            // 
            // GR
            // 
            this.GR.HeaderText = "感染";
            this.GR.Name = "GR";
            this.GR.Visible = false;
            this.GR.Width = 54;
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Location = new System.Drawing.Point(167, 365);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 41);
            this.button1.TabIndex = 5;
            this.button1.Text = "门诊访视";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button2.Location = new System.Drawing.Point(404, 365);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 41);
            this.button2.TabIndex = 6;
            this.button2.Text = "麻醉记录单";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menzhenfashidan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 432);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvJizhenPaiban);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menzhenhao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "menzhenfashidan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病人基本信息";
            ((System.ComponentModel.ISupportInitialize)(this.dgvJizhenPaiban)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox menzhenhao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvJizhenPaiban;
        private System.Windows.Forms.DataGridViewTextBoxColumn patid;
        private System.Windows.Forms.DataGridViewTextBoxColumn menzhenhaocishu;
        private System.Windows.Forms.DataGridViewTextBoxColumn tizhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn mingzu;
        private System.Windows.Forms.DataGridViewTextBoxColumn oroom;
        private System.Windows.Forms.DataGridViewTextBoxColumn second;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn patdpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn patname;
        private System.Windows.Forms.DataGridViewTextBoxColumn patsex;
        private System.Windows.Forms.DataGridViewTextBoxColumn patage;
        private System.Windows.Forms.DataGridViewTextBoxColumn bedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn on1;
        private System.Windows.Forms.DataGridViewTextBoxColumn on2;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn os;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pattmd;
        private System.Windows.Forms.DataGridViewTextBoxColumn oname;
        private System.Windows.Forms.DataGridViewTextBoxColumn amethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn GR;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}