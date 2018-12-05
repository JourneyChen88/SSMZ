namespace main
{
    partial class PaibanSG
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaibanSG));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbOroom = new System.Windows.Forms.ComboBox();
            this.cmbOlevel = new System.Windows.Forms.ComboBox();
            this.dtOSDate = new System.Windows.Forms.DateTimePicker();
            this.ctmsPaibanbiao = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EnterMZD_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label20 = new System.Windows.Forms.Label();
            this.pdDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.cmbMZFF = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvJizhenPaiban = new System.Windows.Forms.DataGridView();
            this.PatZhuYuanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lineSecond = new adims_Utility.LineBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lineRemark = new adims_Utility.LineBox();
            this.lineQXHS = new adims_Utility.LineBox();
            this.lineXHHS = new adims_Utility.LineBox();
            this.lineMZYS = new adims_Utility.LineBox();
            this.lineSSYS = new adims_Utility.LineBox();
            this.lineNSSSS = new adims_Utility.LineBox();
            this.lineTSQK = new adims_Utility.LineBox();
            this.cmbSex = new System.Windows.Forms.ComboBox();
            this.lineSQZD = new adims_Utility.LineBox();
            this.lineNation = new adims_Utility.LineBox();
            this.lineAge = new adims_Utility.LineBox();
            this.lineName = new adims_Utility.LineBox();
            this.lineBedno = new adims_Utility.LineBox();
            this.linePatid = new adims_Utility.LineBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbTiwei = new System.Windows.Forms.ComboBox();
            this.cmbKeshi = new System.Windows.Forms.ComboBox();
            this.btnCheckHis = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbGR = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbZeqi = new System.Windows.Forms.CheckBox();
            this.cbJizhen = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ctmsPaibanbiao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJizhenPaiban)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbOroom
            // 
            this.cmbOroom.FormattingEnabled = true;
            this.cmbOroom.Items.AddRange(new object[] {
            "手术间1"});
            this.cmbOroom.Location = new System.Drawing.Point(89, 166);
            this.cmbOroom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOroom.Name = "cmbOroom";
            this.cmbOroom.Size = new System.Drawing.Size(96, 23);
            this.cmbOroom.TabIndex = 48;
            // 
            // cmbOlevel
            // 
            this.cmbOlevel.FormattingEnabled = true;
            this.cmbOlevel.Items.AddRange(new object[] {
            "Ⅰ",
            "Ⅱ",
            "Ⅲ",
            "Ⅳ"});
            this.cmbOlevel.Location = new System.Drawing.Point(579, 120);
            this.cmbOlevel.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOlevel.Name = "cmbOlevel";
            this.cmbOlevel.Size = new System.Drawing.Size(64, 23);
            this.cmbOlevel.TabIndex = 77;
            // 
            // dtOSDate
            // 
            this.dtOSDate.CalendarFont = new System.Drawing.Font("宋体", 11F);
            this.dtOSDate.Font = new System.Drawing.Font("宋体", 10F);
            this.dtOSDate.Location = new System.Drawing.Point(457, 165);
            this.dtOSDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtOSDate.Name = "dtOSDate";
            this.dtOSDate.Size = new System.Drawing.Size(217, 27);
            this.dtOSDate.TabIndex = 87;
            // 
            // ctmsPaibanbiao
            // 
            this.ctmsPaibanbiao.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctmsPaibanbiao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnterMZD_ToolStripMenuItem});
            this.ctmsPaibanbiao.Name = "contextMenuStrip1";
            this.ctmsPaibanbiao.Size = new System.Drawing.Size(190, 30);
            // 
            // EnterMZD_ToolStripMenuItem
            // 
            this.EnterMZD_ToolStripMenuItem.Name = "EnterMZD_ToolStripMenuItem";
            this.EnterMZD_ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.EnterMZD_ToolStripMenuItem.Text = "进入麻醉记录单";
            this.EnterMZD_ToolStripMenuItem.Click += new System.EventHandler(this.EnterMZD_ToolStripMenuItem_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(23, 166);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(54, 20);
            this.label20.TabIndex = 107;
            this.label20.Text = "手术间";
            // 
            // pdDocument
            // 
            this.pdDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.pdDocument_BeginPrint);
            this.pdDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pdDocument_PrintPage);
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
            // cmbMZFF
            // 
            this.cmbMZFF.FormattingEnabled = true;
            this.cmbMZFF.Location = new System.Drawing.Point(99, 278);
            this.cmbMZFF.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMZFF.Name = "cmbMZFF";
            this.cmbMZFF.Size = new System.Drawing.Size(317, 23);
            this.cmbMZFF.TabIndex = 119;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvJizhenPaiban);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 470);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1448, 358);
            this.groupBox1.TabIndex = 120;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "急诊手术列表";
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
            this.PatZhuYuanID,
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
            this.dgvJizhenPaiban.ContextMenuStrip = this.ctmsPaibanbiao;
            this.dgvJizhenPaiban.Location = new System.Drawing.Point(4, 66);
            this.dgvJizhenPaiban.Margin = new System.Windows.Forms.Padding(4);
            this.dgvJizhenPaiban.MultiSelect = false;
            this.dgvJizhenPaiban.Name = "dgvJizhenPaiban";
            this.dgvJizhenPaiban.RowHeadersVisible = false;
            this.dgvJizhenPaiban.RowTemplate.Height = 23;
            this.dgvJizhenPaiban.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJizhenPaiban.Size = new System.Drawing.Size(1440, 288);
            this.dgvJizhenPaiban.TabIndex = 1;
            this.dgvJizhenPaiban.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJizhenPaiban_CellDoubleClick);
            // 
            // PatZhuYuanID
            // 
            this.PatZhuYuanID.DataPropertyName = "PatZhuYuanID";
            this.PatZhuYuanID.Frozen = true;
            this.PatZhuYuanID.HeaderText = "住院号";
            this.PatZhuYuanID.Name = "PatZhuYuanID";
            this.PatZhuYuanID.ReadOnly = true;
            this.PatZhuYuanID.Width = 83;
            // 
            // oroom
            // 
            this.oroom.DataPropertyName = "oroom";
            this.oroom.HeaderText = "手术间";
            this.oroom.Name = "oroom";
            this.oroom.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.oroom.Width = 83;
            // 
            // second
            // 
            this.second.DataPropertyName = "second";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.second.DefaultCellStyle = dataGridViewCellStyle1;
            this.second.HeaderText = "台次";
            this.second.Name = "second";
            this.second.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.second.Width = 68;
            // 
            // StartTime
            // 
            this.StartTime.DataPropertyName = "StartTime";
            this.StartTime.HeaderText = "预计时间";
            this.StartTime.Name = "StartTime";
            this.StartTime.Width = 98;
            // 
            // patdpm
            // 
            this.patdpm.DataPropertyName = "patdpm";
            this.patdpm.HeaderText = "科室";
            this.patdpm.Name = "patdpm";
            this.patdpm.ReadOnly = true;
            this.patdpm.Width = 68;
            // 
            // patname
            // 
            this.patname.DataPropertyName = "patname";
            this.patname.HeaderText = "病人姓名";
            this.patname.Name = "patname";
            this.patname.ReadOnly = true;
            this.patname.Width = 98;
            // 
            // patsex
            // 
            this.patsex.DataPropertyName = "patsex";
            this.patsex.HeaderText = "性别";
            this.patsex.Name = "patsex";
            this.patsex.ReadOnly = true;
            this.patsex.Width = 68;
            // 
            // patage
            // 
            this.patage.DataPropertyName = "patage";
            this.patage.HeaderText = "年龄";
            this.patage.Name = "patage";
            this.patage.ReadOnly = true;
            this.patage.Width = 68;
            // 
            // bedNo
            // 
            this.bedNo.DataPropertyName = "patbedno";
            this.bedNo.HeaderText = "床号";
            this.bedNo.Name = "bedNo";
            this.bedNo.ReadOnly = true;
            this.bedNo.Width = 68;
            // 
            // on1
            // 
            this.on1.DataPropertyName = "on1";
            this.on1.HeaderText = "洗手护士1";
            this.on1.Name = "on1";
            this.on1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.on1.Width = 107;
            // 
            // on2
            // 
            this.on2.DataPropertyName = "on2";
            this.on2.HeaderText = "洗手护士2";
            this.on2.Name = "on2";
            this.on2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.on2.Width = 107;
            // 
            // sn1
            // 
            this.sn1.DataPropertyName = "sn1";
            this.sn1.HeaderText = "巡回护士1";
            this.sn1.Name = "sn1";
            this.sn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sn1.Width = 107;
            // 
            // sn2
            // 
            this.sn2.DataPropertyName = "sn2";
            this.sn2.HeaderText = "巡回护士2";
            this.sn2.Name = "sn2";
            this.sn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sn2.Width = 107;
            // 
            // os
            // 
            this.os.DataPropertyName = "os";
            this.os.HeaderText = "手术医生";
            this.os.Name = "os";
            this.os.ReadOnly = true;
            this.os.Width = 98;
            // 
            // ap1
            // 
            this.ap1.DataPropertyName = "ap1";
            this.ap1.HeaderText = "麻醉医生";
            this.ap1.Name = "ap1";
            this.ap1.Width = 98;
            // 
            // pattmd
            // 
            this.pattmd.DataPropertyName = "pattmd";
            this.pattmd.HeaderText = "术前诊断";
            this.pattmd.Name = "pattmd";
            this.pattmd.ReadOnly = true;
            this.pattmd.Width = 98;
            // 
            // oname
            // 
            this.oname.DataPropertyName = "oname";
            this.oname.HeaderText = "手术名字";
            this.oname.Name = "oname";
            this.oname.ReadOnly = true;
            this.oname.Width = 98;
            // 
            // amethod
            // 
            this.amethod.DataPropertyName = "amethod";
            this.amethod.HeaderText = "麻醉方法";
            this.amethod.Name = "amethod";
            this.amethod.Width = 98;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.Width = 68;
            // 
            // GR
            // 
            this.GR.DataPropertyName = "GR";
            this.GR.HeaderText = "感染";
            this.GR.Name = "GR";
            this.GR.Width = 68;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 11F);
            this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(435, 21);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(180, 27);
            this.dateTimePicker1.TabIndex = 137;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10F);
            this.label21.Location = new System.Drawing.Point(251, 28);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(161, 17);
            this.label21.TabIndex = 86;
            this.label21.Text = "选择查看手术日期：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lineSecond);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lineRemark);
            this.groupBox2.Controls.Add(this.lineQXHS);
            this.groupBox2.Controls.Add(this.lineXHHS);
            this.groupBox2.Controls.Add(this.lineMZYS);
            this.groupBox2.Controls.Add(this.lineSSYS);
            this.groupBox2.Controls.Add(this.lineNSSSS);
            this.groupBox2.Controls.Add(this.lineTSQK);
            this.groupBox2.Controls.Add(this.cmbSex);
            this.groupBox2.Controls.Add(this.lineSQZD);
            this.groupBox2.Controls.Add(this.lineNation);
            this.groupBox2.Controls.Add(this.lineAge);
            this.groupBox2.Controls.Add(this.lineName);
            this.groupBox2.Controls.Add(this.lineBedno);
            this.groupBox2.Controls.Add(this.linePatid);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.cmbTiwei);
            this.groupBox2.Controls.Add(this.cmbKeshi);
            this.groupBox2.Controls.Add(this.btnCheckHis);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cmbGR);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cbZeqi);
            this.groupBox2.Controls.Add(this.cbJizhen);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmbOlevel);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.cmbMZFF);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.dtOSDate);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.cmbOroom);
            this.groupBox2.Location = new System.Drawing.Point(16, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1423, 426);
            this.groupBox2.TabIndex = 121;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "手术安排";
            // 
            // lineSecond
            // 
            this.lineSecond.LableText = "台次：";
            this.lineSecond.Location = new System.Drawing.Point(222, 163);
            this.lineSecond.Name = "lineSecond";
            this.lineSecond.Size = new System.Drawing.Size(146, 28);
            this.lineSecond.TabIndex = 163;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(948, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 162;
            this.label2.Text = "科室";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(921, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 161;
            this.label1.Text = "性别";
            // 
            // lineRemark
            // 
            this.lineRemark.LableText = "备注：";
            this.lineRemark.Location = new System.Drawing.Point(471, 278);
            this.lineRemark.Name = "lineRemark";
            this.lineRemark.Size = new System.Drawing.Size(744, 28);
            this.lineRemark.TabIndex = 160;
            // 
            // lineQXHS
            // 
            this.lineQXHS.LableText = "器械护士：";
            this.lineQXHS.Location = new System.Drawing.Point(943, 213);
            this.lineQXHS.Name = "lineQXHS";
            this.lineQXHS.Size = new System.Drawing.Size(244, 28);
            this.lineQXHS.TabIndex = 159;
            // 
            // lineXHHS
            // 
            this.lineXHHS.LableText = "巡回护士：";
            this.lineXHHS.Location = new System.Drawing.Point(640, 213);
            this.lineXHHS.Name = "lineXHHS";
            this.lineXHHS.Size = new System.Drawing.Size(272, 28);
            this.lineXHHS.TabIndex = 158;
            // 
            // lineMZYS
            // 
            this.lineMZYS.LableText = "麻醉医生：";
            this.lineMZYS.Location = new System.Drawing.Point(331, 213);
            this.lineMZYS.Name = "lineMZYS";
            this.lineMZYS.Size = new System.Drawing.Size(272, 28);
            this.lineMZYS.TabIndex = 157;
            // 
            // lineSSYS
            // 
            this.lineSSYS.LableText = "手术医生：";
            this.lineSSYS.Location = new System.Drawing.Point(27, 213);
            this.lineSSYS.Name = "lineSSYS";
            this.lineSSYS.Size = new System.Drawing.Size(242, 28);
            this.lineSSYS.TabIndex = 156;
            // 
            // lineNSSSS
            // 
            this.lineNSSSS.LableText = "拟实施手术：";
            this.lineNSSSS.Location = new System.Drawing.Point(27, 120);
            this.lineNSSSS.Name = "lineNSSSS";
            this.lineNSSSS.Size = new System.Drawing.Size(439, 28);
            this.lineNSSSS.TabIndex = 155;
            // 
            // lineTSQK
            // 
            this.lineTSQK.LableText = "特殊情况：";
            this.lineTSQK.Location = new System.Drawing.Point(518, 72);
            this.lineTSQK.Name = "lineTSQK";
            this.lineTSQK.Size = new System.Drawing.Size(669, 28);
            this.lineTSQK.TabIndex = 154;
            // 
            // cmbSex
            // 
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.Items.AddRange(new object[] {
            "男女"});
            this.cmbSex.Location = new System.Drawing.Point(974, 15);
            this.cmbSex.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.Size = new System.Drawing.Size(64, 23);
            this.cmbSex.TabIndex = 153;
            // 
            // lineSQZD
            // 
            this.lineSQZD.LableText = "术前诊断：";
            this.lineSQZD.Location = new System.Drawing.Point(27, 72);
            this.lineSQZD.Name = "lineSQZD";
            this.lineSQZD.Size = new System.Drawing.Size(439, 28);
            this.lineSQZD.TabIndex = 152;
            // 
            // lineNation
            // 
            this.lineNation.LableText = "名族：";
            this.lineNation.Location = new System.Drawing.Point(782, 18);
            this.lineNation.Name = "lineNation";
            this.lineNation.Size = new System.Drawing.Size(146, 28);
            this.lineNation.TabIndex = 151;
            // 
            // lineAge
            // 
            this.lineAge.LableText = "年龄：";
            this.lineAge.Location = new System.Drawing.Point(640, 18);
            this.lineAge.Name = "lineAge";
            this.lineAge.Size = new System.Drawing.Size(152, 28);
            this.lineAge.TabIndex = 150;
            // 
            // lineName
            // 
            this.lineName.LableText = "姓名：";
            this.lineName.Location = new System.Drawing.Point(457, 18);
            this.lineName.Name = "lineName";
            this.lineName.Size = new System.Drawing.Size(177, 28);
            this.lineName.TabIndex = 149;
            // 
            // lineBedno
            // 
            this.lineBedno.LableText = "床号：";
            this.lineBedno.Location = new System.Drawing.Point(299, 21);
            this.lineBedno.Name = "lineBedno";
            this.lineBedno.Size = new System.Drawing.Size(167, 28);
            this.lineBedno.TabIndex = 148;
            // 
            // linePatid
            // 
            this.linePatid.LableText = "住院号：";
            this.linePatid.Location = new System.Drawing.Point(24, 26);
            this.linePatid.Name = "linePatid";
            this.linePatid.Size = new System.Drawing.Size(197, 28);
            this.linePatid.TabIndex = 147;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(695, 119);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 20);
            this.label10.TabIndex = 146;
            this.label10.Text = "手术体位";
            // 
            // cmbTiwei
            // 
            this.cmbTiwei.FormattingEnabled = true;
            this.cmbTiwei.Items.AddRange(new object[] {
            "Ⅰ",
            "Ⅱ",
            "Ⅲ",
            "Ⅳ"});
            this.cmbTiwei.Location = new System.Drawing.Point(782, 116);
            this.cmbTiwei.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTiwei.Name = "cmbTiwei";
            this.cmbTiwei.Size = new System.Drawing.Size(125, 23);
            this.cmbTiwei.TabIndex = 145;
            // 
            // cmbKeshi
            // 
            this.cmbKeshi.FormattingEnabled = true;
            this.cmbKeshi.Items.AddRange(new object[] {
            "Ⅰ",
            "Ⅱ",
            "Ⅲ",
            "Ⅳ"});
            this.cmbKeshi.Location = new System.Drawing.Point(1000, 113);
            this.cmbKeshi.Margin = new System.Windows.Forms.Padding(4);
            this.cmbKeshi.Name = "cmbKeshi";
            this.cmbKeshi.Size = new System.Drawing.Size(167, 23);
            this.cmbKeshi.TabIndex = 144;
            // 
            // btnCheckHis
            // 
            this.btnCheckHis.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheckHis.ForeColor = System.Drawing.Color.Maroon;
            this.btnCheckHis.Location = new System.Drawing.Point(228, 21);
            this.btnCheckHis.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckHis.Name = "btnCheckHis";
            this.btnCheckHis.Size = new System.Drawing.Size(55, 31);
            this.btnCheckHis.TabIndex = 141;
            this.btnCheckHis.Text = "检索";
            this.btnCheckHis.UseVisualStyleBackColor = true;
            this.btnCheckHis.Click += new System.EventHandler(this.btnCheckHis_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(375, 171);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 20);
            this.label9.TabIndex = 138;
            this.label9.Text = "手术时间";
            // 
            // cmbGR
            // 
            this.cmbGR.FormattingEnabled = true;
            this.cmbGR.Items.AddRange(new object[] {
            "正常",
            "隔离",
            "放射"});
            this.cmbGR.Location = new System.Drawing.Point(751, 172);
            this.cmbGR.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGR.Name = "cmbGR";
            this.cmbGR.Size = new System.Drawing.Size(109, 23);
            this.cmbGR.TabIndex = 136;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(704, 172);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 20);
            this.label8.TabIndex = 135;
            this.label8.Text = "隔离";
            // 
            // cbZeqi
            // 
            this.cbZeqi.AutoSize = true;
            this.cbZeqi.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbZeqi.Location = new System.Drawing.Point(1043, 172);
            this.cbZeqi.Margin = new System.Windows.Forms.Padding(4);
            this.cbZeqi.Name = "cbZeqi";
            this.cbZeqi.Size = new System.Drawing.Size(68, 28);
            this.cbZeqi.TabIndex = 134;
            this.cbZeqi.Text = "择期";
            this.cbZeqi.UseVisualStyleBackColor = true;
            // 
            // cbJizhen
            // 
            this.cbJizhen.AutoSize = true;
            this.cbJizhen.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbJizhen.Location = new System.Drawing.Point(943, 171);
            this.cbJizhen.Margin = new System.Windows.Forms.Padding(4);
            this.cbJizhen.Name = "cbJizhen";
            this.cbJizhen.Size = new System.Drawing.Size(68, 28);
            this.cbJizhen.TabIndex = 133;
            this.cbJizhen.Text = "急诊";
            this.cbJizhen.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(514, 120);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 20);
            this.label7.TabIndex = 132;
            this.label7.Text = "等级";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Image = global::main.Properties.Resources.Print;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(641, 339);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(195, 49);
            this.button3.TabIndex = 109;
            this.button3.Text = "打印手术通知单";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Image = global::main.Properties.Resources.Exit;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(499, 339);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 51);
            this.button2.TabIndex = 89;
            this.button2.Text = "退出";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Image = global::main.Properties.Resources.Save;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(357, 339);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 51);
            this.button1.TabIndex = 88;
            this.button1.Text = "保存";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PaibanSG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1487, 828);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PaibanSG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "急诊手术登记";
            this.Load += new System.EventHandler(this.PAIBAN_SGShuRu_Load);
            this.ctmsPaibanbiao.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJizhenPaiban)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

    
        
        private System.Windows.Forms.ComboBox cmbOroom;
        private System.Windows.Forms.ComboBox cmbOlevel;
        private System.Windows.Forms.DateTimePicker dtOSDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button button3;
        private System.Drawing.Printing.PrintDocument pdDocument;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ComboBox cmbMZFF;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip ctmsPaibanbiao;
        private System.Windows.Forms.ToolStripMenuItem EnterMZD_ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvJizhenPaiban;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbZeqi;
        private System.Windows.Forms.CheckBox cbJizhen;
        private System.Windows.Forms.ComboBox cmbGR;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private WindowsFormsControlLibrary5.UserControl1 txtBZ;
        private System.Windows.Forms.Button btnCheckHis;
        private WindowsFormsControlLibrary5.UserControl1 txtPatNation;
        private System.Windows.Forms.ComboBox cmbKeshi;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbTiwei;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatZhuYuanID;
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
        private System.Windows.Forms.ComboBox cmbSex;
        private adims_Utility.LineBox lineSQZD;
        private adims_Utility.LineBox lineNation;
        private adims_Utility.LineBox lineAge;
        private adims_Utility.LineBox lineName;
        private adims_Utility.LineBox lineBedno;
        private adims_Utility.LineBox linePatid;
        private adims_Utility.LineBox lineTSQK;
        private adims_Utility.LineBox lineRemark;
        private adims_Utility.LineBox lineQXHS;
        private adims_Utility.LineBox lineXHHS;
        private adims_Utility.LineBox lineMZYS;
        private adims_Utility.LineBox lineSSYS;
        private adims_Utility.LineBox lineNSSSS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private adims_Utility.LineBox lineSecond;
    }
}