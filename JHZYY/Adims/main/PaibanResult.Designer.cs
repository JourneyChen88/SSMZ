namespace main
{
    partial class PaibanResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaibanResult));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOTypesetting = new System.Windows.Forms.DataGridView();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPrintResult = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.dtDataTime = new System.Windows.Forms.DateTimePicker();
            this.btnAfter = new System.Windows.Forms.Button();
            this.btnBefore = new System.Windows.Forms.Button();
            this.cbOroom = new System.Windows.Forms.CheckBox();
            this.cbTime = new System.Windows.Forms.CheckBox();
            this.cbSecond = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbKeshi = new System.Windows.Forms.CheckBox();
            this.btnConfig = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.CoDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oroom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.second = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patdpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patNation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pattmd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isjizhen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiwei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hushi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sqys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zrys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOTypesetting)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOTypesetting
            // 
            this.dgvOTypesetting.AllowUserToAddRows = false;
            this.dgvOTypesetting.AllowUserToDeleteRows = false;
            this.dgvOTypesetting.AllowUserToResizeColumns = false;
            this.dgvOTypesetting.AllowUserToResizeRows = false;
            this.dgvOTypesetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOTypesetting.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOTypesetting.ColumnHeadersHeight = 25;
            this.dgvOTypesetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CoDelete,
            this.oroom,
            this.second,
            this.StartTime,
            this.patid,
            this.patdpm,
            this.patname,
            this.bedNo,
            this.patNation,
            this.pattmd,
            this.isjizhen,
            this.oname,
            this.amethod,
            this.os,
            this.tiwei,
            this.gr,
            this.os1,
            this.remarks,
            this.ap,
            this.hushi,
            this.sqys,
            this.zrys});
            this.dgvOTypesetting.Location = new System.Drawing.Point(5, 104);
            this.dgvOTypesetting.MultiSelect = false;
            this.dgvOTypesetting.Name = "dgvOTypesetting";
            this.dgvOTypesetting.RowHeadersVisible = false;
            this.dgvOTypesetting.RowTemplate.Height = 23;
            this.dgvOTypesetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOTypesetting.Size = new System.Drawing.Size(958, 537);
            this.dgvOTypesetting.TabIndex = 4;
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnExcel.ForeColor = System.Drawing.Color.Blue;
            this.btnExcel.Image = global::main.Properties.Resources.Excel1;
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(687, 35);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(117, 40);
            this.btnExcel.TabIndex = 645;
            this.btnExcel.Text = "导出Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrintResult
            // 
            this.btnPrintResult.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnPrintResult.ForeColor = System.Drawing.Color.Blue;
            this.btnPrintResult.Image = global::main.Properties.Resources.Print;
            this.btnPrintResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintResult.Location = new System.Drawing.Point(555, 35);
            this.btnPrintResult.Name = "btnPrintResult";
            this.btnPrintResult.Size = new System.Drawing.Size(114, 40);
            this.btnPrintResult.TabIndex = 644;
            this.btnPrintResult.Text = "打印排班表";
            this.btnPrintResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintResult.UseVisualStyleBackColor = true;
            this.btnPrintResult.Click += new System.EventHandler(this.btnPrintResult_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // dtDataTime
            // 
            this.dtDataTime.Location = new System.Drawing.Point(62, 41);
            this.dtDataTime.Name = "dtDataTime";
            this.dtDataTime.Size = new System.Drawing.Size(130, 23);
            this.dtDataTime.TabIndex = 646;
            this.dtDataTime.ValueChanged += new System.EventHandler(this.dtDataTime_ValueChanged_1);
            // 
            // btnAfter
            // 
            this.btnAfter.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnAfter.ForeColor = System.Drawing.Color.Blue;
            this.btnAfter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAfter.Location = new System.Drawing.Point(196, 40);
            this.btnAfter.Name = "btnAfter";
            this.btnAfter.Size = new System.Drawing.Size(53, 26);
            this.btnAfter.TabIndex = 647;
            this.btnAfter.Text = "后一天";
            this.btnAfter.UseVisualStyleBackColor = true;
            this.btnAfter.Click += new System.EventHandler(this.btnAfter_Click);
            // 
            // btnBefore
            // 
            this.btnBefore.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnBefore.ForeColor = System.Drawing.Color.Blue;
            this.btnBefore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBefore.Location = new System.Drawing.Point(4, 40);
            this.btnBefore.Name = "btnBefore";
            this.btnBefore.Size = new System.Drawing.Size(53, 26);
            this.btnBefore.TabIndex = 648;
            this.btnBefore.Text = "前一天";
            this.btnBefore.UseVisualStyleBackColor = true;
            this.btnBefore.Click += new System.EventHandler(this.btnBefore_Click);
            // 
            // cbOroom
            // 
            this.cbOroom.AutoSize = true;
            this.cbOroom.Location = new System.Drawing.Point(15, 26);
            this.cbOroom.Name = "cbOroom";
            this.cbOroom.Size = new System.Drawing.Size(60, 16);
            this.cbOroom.TabIndex = 650;
            this.cbOroom.Text = "手术间";
            this.cbOroom.UseVisualStyleBackColor = true;
            // 
            // cbTime
            // 
            this.cbTime.AutoSize = true;
            this.cbTime.Location = new System.Drawing.Point(15, 54);
            this.cbTime.Name = "cbTime";
            this.cbTime.Size = new System.Drawing.Size(48, 16);
            this.cbTime.TabIndex = 651;
            this.cbTime.Text = "时间";
            this.cbTime.UseVisualStyleBackColor = true;
            // 
            // cbSecond
            // 
            this.cbSecond.AutoSize = true;
            this.cbSecond.Location = new System.Drawing.Point(95, 26);
            this.cbSecond.Name = "cbSecond";
            this.cbSecond.Size = new System.Drawing.Size(48, 16);
            this.cbSecond.TabIndex = 652;
            this.cbSecond.Text = "台次";
            this.cbSecond.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbKeshi);
            this.groupBox1.Controls.Add(this.btnConfig);
            this.groupBox1.Controls.Add(this.cbTime);
            this.groupBox1.Controls.Add(this.cbSecond);
            this.groupBox1.Controls.Add(this.cbOroom);
            this.groupBox1.Location = new System.Drawing.Point(307, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 89);
            this.groupBox1.TabIndex = 653;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择排序方式";
            // 
            // cbKeshi
            // 
            this.cbKeshi.AutoSize = true;
            this.cbKeshi.Location = new System.Drawing.Point(95, 55);
            this.cbKeshi.Name = "cbKeshi";
            this.cbKeshi.Size = new System.Drawing.Size(48, 16);
            this.cbKeshi.TabIndex = 654;
            this.cbKeshi.Text = "科室";
            this.cbKeshi.UseVisualStyleBackColor = true;
            // 
            // btnConfig
            // 
            this.btnConfig.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnConfig.ForeColor = System.Drawing.Color.Blue;
            this.btnConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfig.Location = new System.Drawing.Point(160, 34);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(53, 39);
            this.btnConfig.TabIndex = 653;
            this.btnConfig.Text = "确定";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAfter);
            this.groupBox2.Controls.Add(this.btnBefore);
            this.groupBox2.Controls.Add(this.dtDataTime);
            this.groupBox2.Location = new System.Drawing.Point(29, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 89);
            this.groupBox2.TabIndex = 654;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择手术日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(841, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 655;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(892, 43);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 23);
            this.numericUpDown1.TabIndex = 656;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(818, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 657;
            this.label2.Text = "打印份数：";
            // 
            // CoDelete
            // 
            this.CoDelete.Frozen = true;
            this.CoDelete.HeaderText = "选择";
            this.CoDelete.Name = "CoDelete";
            this.CoDelete.Visible = false;
            this.CoDelete.Width = 50;
            // 
            // oroom
            // 
            this.oroom.DataPropertyName = "oroom";
            this.oroom.Frozen = true;
            this.oroom.HeaderText = "手术间";
            this.oroom.MinimumWidth = 50;
            this.oroom.Name = "oroom";
            this.oroom.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.oroom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.oroom.Width = 50;
            // 
            // second
            // 
            this.second.DataPropertyName = "second";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.second.DefaultCellStyle = dataGridViewCellStyle1;
            this.second.Frozen = true;
            this.second.HeaderText = "台次";
            this.second.MinimumWidth = 40;
            this.second.Name = "second";
            this.second.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.second.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.second.Width = 40;
            // 
            // StartTime
            // 
            this.StartTime.DataPropertyName = "StartTime";
            this.StartTime.Frozen = true;
            this.StartTime.HeaderText = "时间";
            this.StartTime.MinimumWidth = 60;
            this.StartTime.Name = "StartTime";
            this.StartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StartTime.Width = 60;
            // 
            // patid
            // 
            this.patid.DataPropertyName = "patid";
            this.patid.Frozen = true;
            this.patid.HeaderText = "住院号";
            this.patid.MinimumWidth = 80;
            this.patid.Name = "patid";
            this.patid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patid.Width = 80;
            // 
            // patdpm
            // 
            this.patdpm.DataPropertyName = "patdpm";
            this.patdpm.Frozen = true;
            this.patdpm.HeaderText = "科室";
            this.patdpm.MinimumWidth = 80;
            this.patdpm.Name = "patdpm";
            this.patdpm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patdpm.Width = 80;
            // 
            // patname
            // 
            this.patname.DataPropertyName = "patname";
            this.patname.Frozen = true;
            this.patname.HeaderText = "患者信息";
            this.patname.MinimumWidth = 80;
            this.patname.Name = "patname";
            this.patname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patname.Width = 80;
            // 
            // bedNo
            // 
            this.bedNo.DataPropertyName = "patbedno";
            this.bedNo.HeaderText = "床号";
            this.bedNo.MinimumWidth = 45;
            this.bedNo.Name = "bedNo";
            this.bedNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.bedNo.Width = 45;
            // 
            // patNation
            // 
            this.patNation.DataPropertyName = "patNation";
            this.patNation.HeaderText = "民族";
            this.patNation.MinimumWidth = 50;
            this.patNation.Name = "patNation";
            this.patNation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patNation.Width = 50;
            // 
            // pattmd
            // 
            this.pattmd.DataPropertyName = "pattmd";
            this.pattmd.HeaderText = "术前诊断";
            this.pattmd.MinimumWidth = 110;
            this.pattmd.Name = "pattmd";
            this.pattmd.Width = 110;
            // 
            // isjizhen
            // 
            this.isjizhen.DataPropertyName = "isjizhen";
            this.isjizhen.HeaderText = "择期/急症";
            this.isjizhen.Name = "isjizhen";
            // 
            // oname
            // 
            this.oname.DataPropertyName = "oname";
            this.oname.HeaderText = "手术名称";
            this.oname.MinimumWidth = 110;
            this.oname.Name = "oname";
            this.oname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.oname.Width = 110;
            // 
            // amethod
            // 
            this.amethod.DataPropertyName = "amethod";
            this.amethod.HeaderText = "麻醉方法";
            this.amethod.MinimumWidth = 80;
            this.amethod.Name = "amethod";
            this.amethod.Width = 80;
            // 
            // os
            // 
            this.os.DataPropertyName = "os";
            this.os.HeaderText = "手术者";
            this.os.MinimumWidth = 70;
            this.os.Name = "os";
            this.os.Width = 70;
            // 
            // tiwei
            // 
            this.tiwei.DataPropertyName = "tiwei";
            this.tiwei.HeaderText = "体位";
            this.tiwei.MinimumWidth = 50;
            this.tiwei.Name = "tiwei";
            this.tiwei.Width = 50;
            // 
            // gr
            // 
            this.gr.DataPropertyName = "gr";
            this.gr.HeaderText = "感染";
            this.gr.MinimumWidth = 60;
            this.gr.Name = "gr";
            this.gr.Width = 60;
            // 
            // os1
            // 
            this.os1.DataPropertyName = "bx";
            this.os1.HeaderText = "备血";
            this.os1.MinimumWidth = 60;
            this.os1.Name = "os1";
            this.os1.Width = 60;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.MinimumWidth = 80;
            this.remarks.Name = "remarks";
            this.remarks.Width = 80;
            // 
            // ap
            // 
            this.ap.DataPropertyName = "ap";
            this.ap.HeaderText = "麻醉者";
            this.ap.MinimumWidth = 70;
            this.ap.Name = "ap";
            this.ap.Width = 70;
            // 
            // hushi
            // 
            this.hushi.DataPropertyName = "hushi";
            this.hushi.HeaderText = "洗手/巡回护士";
            this.hushi.MinimumWidth = 80;
            this.hushi.Name = "hushi";
            this.hushi.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.hushi.Width = 80;
            // 
            // sqys
            // 
            this.sqys.DataPropertyName = "sqys";
            this.sqys.HeaderText = "申请医生签名";
            this.sqys.Name = "sqys";
            // 
            // zrys
            // 
            this.zrys.DataPropertyName = "zrys";
            this.zrys.HeaderText = "科室主任签名";
            this.zrys.Name = "zrys";
            // 
            // PaibanResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 641);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrintResult);
            this.Controls.Add(this.dgvOTypesetting);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PaibanResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "排版结果查询";
            this.Load += new System.EventHandler(this.PaibanResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOTypesetting)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOTypesetting;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrintResult;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.DateTimePicker dtDataTime;
        private System.Windows.Forms.Button btnAfter;
        private System.Windows.Forms.Button btnBefore;
        private System.Windows.Forms.CheckBox cbOroom;
        private System.Windows.Forms.CheckBox cbTime;
        private System.Windows.Forms.CheckBox cbSecond;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbKeshi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CoDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn oroom;
        private System.Windows.Forms.DataGridViewTextBoxColumn second;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn patid;
        private System.Windows.Forms.DataGridViewTextBoxColumn patdpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn patname;
        private System.Windows.Forms.DataGridViewTextBoxColumn bedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn patNation;
        private System.Windows.Forms.DataGridViewTextBoxColumn pattmd;
        private System.Windows.Forms.DataGridViewTextBoxColumn isjizhen;
        private System.Windows.Forms.DataGridViewTextBoxColumn oname;
        private System.Windows.Forms.DataGridViewTextBoxColumn amethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn os;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiwei;
        private System.Windows.Forms.DataGridViewTextBoxColumn gr;
        private System.Windows.Forms.DataGridViewTextBoxColumn os1;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap;
        private System.Windows.Forms.DataGridViewTextBoxColumn hushi;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqys;
        private System.Windows.Forms.DataGridViewTextBoxColumn zrys;

    }
}