namespace main
{
    partial class PaiBanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaiBanForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnAp3 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnIntelligent = new System.Windows.Forms.Button();
            this.btnPrintView = new System.Windows.Forms.Button();
            this.dgvOTypesetting = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oroom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.second = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OS2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatZhuYuanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isGeli = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pattmd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patsex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patdpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Patbedno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TalkInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patNation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiwei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expertName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.on1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.on2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sqys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sslb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmsPaibanbiao = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EnterMZD_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAp2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbKeshi = new System.Windows.Forms.CheckBox();
            this.btnConfig = new System.Windows.Forms.Button();
            this.cbTime = new System.Windows.Forms.CheckBox();
            this.cbSecond = new System.Windows.Forms.CheckBox();
            this.cbOroom = new System.Windows.Forms.CheckBox();
            this.lbPatName = new System.Windows.Forms.Label();
            this.lbSSMC = new System.Windows.Forms.Label();
            this.lbRoomName = new System.Windows.Forms.Label();
            this.lbSSYS = new System.Windows.Forms.Label();
            this.lbOname1 = new System.Windows.Forms.Label();
            this.lbPatName1 = new System.Windows.Forms.Label();
            this.lbOS = new System.Windows.Forms.Label();
            this.lbKeshi = new System.Windows.Forms.Label();
            this.buttonConfig = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtDataTime = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOperInfo = new System.Windows.Forms.Button();
            this.lblTS = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnisJZ = new System.Windows.Forms.Button();
            this.btnPrintResult = new System.Windows.Forms.Button();
            this.btnNO = new System.Windows.Forms.Button();
            this.listboxRoom = new System.Windows.Forms.ListBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnYES = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.printPreviewDialog2 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog3 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument3 = new System.Drawing.Printing.PrintDocument();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOTypesetting)).BeginInit();
            this.ctmsPaibanbiao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            // btnAp3
            // 
            this.btnAp3.Location = new System.Drawing.Point(18, 640);
            this.btnAp3.Name = "btnAp3";
            this.btnAp3.Size = new System.Drawing.Size(192, 23);
            this.btnAp3.TabIndex = 6;
            this.btnAp3.Text = "副麻医师2";
            this.btnAp3.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(44, 751);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 28);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnIntelligent
            // 
            this.btnIntelligent.Location = new System.Drawing.Point(114, 751);
            this.btnIntelligent.Name = "btnIntelligent";
            this.btnIntelligent.Size = new System.Drawing.Size(77, 28);
            this.btnIntelligent.TabIndex = 5;
            this.btnIntelligent.Text = "智能排班";
            this.btnIntelligent.UseVisualStyleBackColor = true;
            this.btnIntelligent.Visible = false;
            this.btnIntelligent.Click += new System.EventHandler(this.btnIntelligent_Click);
            // 
            // btnPrintView
            // 
            this.btnPrintView.Location = new System.Drawing.Point(119, 698);
            this.btnPrintView.Name = "btnPrintView";
            this.btnPrintView.Size = new System.Drawing.Size(90, 28);
            this.btnPrintView.TabIndex = 8;
            this.btnPrintView.Text = "打印预览";
            this.btnPrintView.UseVisualStyleBackColor = true;
            // 
            // dgvOTypesetting
            // 
            this.dgvOTypesetting.AllowUserToAddRows = false;
            this.dgvOTypesetting.AllowUserToDeleteRows = false;
            this.dgvOTypesetting.AllowUserToResizeRows = false;
            this.dgvOTypesetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOTypesetting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOTypesetting.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOTypesetting.ColumnHeadersHeight = 25;
            this.dgvOTypesetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.oroom,
            this.second,
            this.StartTime,
            this.os,
            this.os1,
            this.OS2,
            this.PatZhuYuanID,
            this.patname,
            this.Amethod,
            this.oname,
            this.isGeli,
            this.pattmd,
            this.patsex,
            this.patage,
            this.patdpm,
            this.Patbedno,
            this.TalkInfo,
            this.patNation,
            this.tiwei,
            this.expertName,
            this.remarks,
            this.on1,
            this.on2,
            this.sn1,
            this.sn2,
            this.ap1,
            this.ap2,
            this.ap3,
            this.id,
            this.sqys,
            this.sslb,
            this.patid});
            this.dgvOTypesetting.ContextMenuStrip = this.ctmsPaibanbiao;
            this.dgvOTypesetting.Location = new System.Drawing.Point(3, 104);
            this.dgvOTypesetting.MultiSelect = false;
            this.dgvOTypesetting.Name = "dgvOTypesetting";
            this.dgvOTypesetting.ReadOnly = true;
            this.dgvOTypesetting.RowHeadersVisible = false;
            this.dgvOTypesetting.RowTemplate.Height = 23;
            this.dgvOTypesetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOTypesetting.Size = new System.Drawing.Size(941, 623);
            this.dgvOTypesetting.TabIndex = 0;
            this.dgvOTypesetting.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOTypesetting_CellClick);
            this.dgvOTypesetting.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOTypesetting_CellDoubleClick);
            this.dgvOTypesetting.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvOTypesetting_CellValidating);
            this.dgvOTypesetting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvOTypesetting_EditingControlShowing);
            this.dgvOTypesetting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvOTypesetting_KeyPress);
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "      ";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 58;
            // 
            // oroom
            // 
            this.oroom.DataPropertyName = "oroom";
            this.oroom.Frozen = true;
            this.oroom.HeaderText = "手术间";
            this.oroom.Name = "oroom";
            this.oroom.ReadOnly = true;
            this.oroom.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.oroom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.oroom.Width = 57;
            // 
            // second
            // 
            this.second.DataPropertyName = "second";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.second.DefaultCellStyle = dataGridViewCellStyle1;
            this.second.Frozen = true;
            this.second.HeaderText = "台次";
            this.second.Name = "second";
            this.second.ReadOnly = true;
            this.second.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.second.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.second.Width = 43;
            // 
            // StartTime
            // 
            this.StartTime.DataPropertyName = "StartTime";
            this.StartTime.Frozen = true;
            this.StartTime.HeaderText = "预计开始";
            this.StartTime.Name = "StartTime";
            this.StartTime.ReadOnly = true;
            this.StartTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StartTime.Width = 71;
            // 
            // os
            // 
            this.os.DataPropertyName = "os";
            this.os.Frozen = true;
            this.os.HeaderText = "手术医生";
            this.os.Name = "os";
            this.os.ReadOnly = true;
            this.os.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.os.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.os.Width = 71;
            // 
            // os1
            // 
            this.os1.DataPropertyName = "OS1";
            this.os1.Frozen = true;
            this.os1.HeaderText = "一助";
            this.os1.Name = "os1";
            this.os1.ReadOnly = true;
            this.os1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.os1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.os1.Width = 43;
            // 
            // OS2
            // 
            this.OS2.DataPropertyName = "OS2";
            this.OS2.HeaderText = "二助";
            this.OS2.Name = "OS2";
            this.OS2.ReadOnly = true;
            this.OS2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OS2.Width = 62;
            // 
            // PatZhuYuanID
            // 
            this.PatZhuYuanID.DataPropertyName = "PatZhuYuanID";
            this.PatZhuYuanID.HeaderText = "住院号";
            this.PatZhuYuanID.Name = "PatZhuYuanID";
            this.PatZhuYuanID.ReadOnly = true;
            this.PatZhuYuanID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatZhuYuanID.Visible = false;
            this.PatZhuYuanID.Width = 47;
            // 
            // patname
            // 
            this.patname.DataPropertyName = "patname";
            this.patname.HeaderText = "姓名";
            this.patname.Name = "patname";
            this.patname.ReadOnly = true;
            this.patname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patname.Width = 43;
            // 
            // Amethod
            // 
            this.Amethod.DataPropertyName = "Amethod";
            this.Amethod.HeaderText = "麻醉方法";
            this.Amethod.Name = "Amethod";
            this.Amethod.ReadOnly = true;
            this.Amethod.Width = 90;
            // 
            // oname
            // 
            this.oname.DataPropertyName = "oname";
            this.oname.HeaderText = "手术名称";
            this.oname.Name = "oname";
            this.oname.ReadOnly = true;
            this.oname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.oname.Width = 71;
            // 
            // isGeli
            // 
            this.isGeli.DataPropertyName = "isGeli";
            this.isGeli.HeaderText = "隔离";
            this.isGeli.Name = "isGeli";
            this.isGeli.ReadOnly = true;
            this.isGeli.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.isGeli.Width = 43;
            // 
            // pattmd
            // 
            this.pattmd.DataPropertyName = "pattmd";
            this.pattmd.HeaderText = "术前诊断";
            this.pattmd.Name = "pattmd";
            this.pattmd.ReadOnly = true;
            this.pattmd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pattmd.Width = 71;
            // 
            // patsex
            // 
            this.patsex.DataPropertyName = "patsex";
            this.patsex.HeaderText = "性别";
            this.patsex.Name = "patsex";
            this.patsex.ReadOnly = true;
            this.patsex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patsex.Width = 43;
            // 
            // patage
            // 
            this.patage.DataPropertyName = "patage";
            this.patage.HeaderText = "年龄";
            this.patage.Name = "patage";
            this.patage.ReadOnly = true;
            this.patage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patage.Width = 43;
            // 
            // patdpm
            // 
            this.patdpm.DataPropertyName = "patdpm";
            this.patdpm.HeaderText = "科室";
            this.patdpm.Name = "patdpm";
            this.patdpm.ReadOnly = true;
            this.patdpm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patdpm.Width = 43;
            // 
            // Patbedno
            // 
            this.Patbedno.DataPropertyName = "Patbedno";
            this.Patbedno.HeaderText = "床号";
            this.Patbedno.Name = "Patbedno";
            this.Patbedno.ReadOnly = true;
            this.Patbedno.Visible = false;
            this.Patbedno.Width = 54;
            // 
            // TalkInfo
            // 
            this.TalkInfo.DataPropertyName = "TalkInfo";
            this.TalkInfo.HeaderText = "对话信息";
            this.TalkInfo.Name = "TalkInfo";
            this.TalkInfo.ReadOnly = true;
            this.TalkInfo.Width = 90;
            // 
            // patNation
            // 
            this.patNation.DataPropertyName = "patNation";
            this.patNation.HeaderText = "民族";
            this.patNation.Name = "patNation";
            this.patNation.ReadOnly = true;
            this.patNation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patNation.Visible = false;
            this.patNation.Width = 35;
            // 
            // tiwei
            // 
            this.tiwei.DataPropertyName = "tiwei";
            this.tiwei.HeaderText = "手术体位";
            this.tiwei.Name = "tiwei";
            this.tiwei.ReadOnly = true;
            this.tiwei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tiwei.Visible = false;
            this.tiwei.Width = 59;
            // 
            // expertName
            // 
            this.expertName.DataPropertyName = "expertName";
            this.expertName.HeaderText = "外院专家";
            this.expertName.Name = "expertName";
            this.expertName.ReadOnly = true;
            this.expertName.Visible = false;
            this.expertName.Width = 78;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.ReadOnly = true;
            this.remarks.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.remarks.Visible = false;
            this.remarks.Width = 35;
            // 
            // on1
            // 
            this.on1.DataPropertyName = "on1";
            this.on1.HeaderText = "洗手护士";
            this.on1.Name = "on1";
            this.on1.ReadOnly = true;
            this.on1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.on1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.on1.Width = 71;
            // 
            // on2
            // 
            this.on2.DataPropertyName = "on2";
            this.on2.HeaderText = "洗手护士2";
            this.on2.MinimumWidth = 50;
            this.on2.Name = "on2";
            this.on2.ReadOnly = true;
            this.on2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.on2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.on2.Width = 79;
            // 
            // sn1
            // 
            this.sn1.DataPropertyName = "sn1";
            this.sn1.HeaderText = "巡回护士";
            this.sn1.Name = "sn1";
            this.sn1.ReadOnly = true;
            this.sn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sn1.Width = 71;
            // 
            // sn2
            // 
            this.sn2.DataPropertyName = "sn2";
            this.sn2.HeaderText = "巡回护士2";
            this.sn2.MinimumWidth = 50;
            this.sn2.Name = "sn2";
            this.sn2.ReadOnly = true;
            this.sn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sn2.Width = 79;
            // 
            // ap1
            // 
            this.ap1.DataPropertyName = "ap1";
            this.ap1.HeaderText = "主麻医师";
            this.ap1.Name = "ap1";
            this.ap1.ReadOnly = true;
            this.ap1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ap1.Width = 71;
            // 
            // ap2
            // 
            this.ap2.DataPropertyName = "ap2";
            this.ap2.HeaderText = "副麻医师1";
            this.ap2.Name = "ap2";
            this.ap2.ReadOnly = true;
            this.ap2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ap2.Width = 79;
            // 
            // ap3
            // 
            this.ap3.DataPropertyName = "ap3";
            this.ap3.HeaderText = "副麻医师2";
            this.ap3.Name = "ap3";
            this.ap3.ReadOnly = true;
            this.ap3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ap3.Width = 79;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 42;
            // 
            // sqys
            // 
            this.sqys.DataPropertyName = "sqys";
            this.sqys.HeaderText = "申请医生签名";
            this.sqys.Name = "sqys";
            this.sqys.ReadOnly = true;
            this.sqys.Visible = false;
            this.sqys.Width = 102;
            // 
            // sslb
            // 
            this.sslb.DataPropertyName = "sslb";
            this.sslb.HeaderText = "手术类别";
            this.sslb.Name = "sslb";
            this.sslb.ReadOnly = true;
            this.sslb.Visible = false;
            this.sslb.Width = 78;
            // 
            // patid
            // 
            this.patid.DataPropertyName = "patid";
            this.patid.HeaderText = "patid";
            this.patid.Name = "patid";
            this.patid.ReadOnly = true;
            this.patid.Visible = false;
            this.patid.Width = 60;
            // 
            // ctmsPaibanbiao
            // 
            this.ctmsPaibanbiao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnterMZD_ToolStripMenuItem});
            this.ctmsPaibanbiao.Name = "contextMenuStrip1";
            this.ctmsPaibanbiao.Size = new System.Drawing.Size(161, 26);
            // 
            // EnterMZD_ToolStripMenuItem
            // 
            this.EnterMZD_ToolStripMenuItem.Name = "EnterMZD_ToolStripMenuItem";
            this.EnterMZD_ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.EnterMZD_ToolStripMenuItem.Text = "进入麻醉记录单";
            this.EnterMZD_ToolStripMenuItem.Visible = false;
            this.EnterMZD_ToolStripMenuItem.Click += new System.EventHandler(this.EnterMZD_ToolStripMenuItem_Click);
            // 
            // btnAp2
            // 
            this.btnAp2.Location = new System.Drawing.Point(506, 617);
            this.btnAp2.Name = "btnAp2";
            this.btnAp2.Size = new System.Drawing.Size(192, 23);
            this.btnAp2.TabIndex = 5;
            this.btnAp2.Text = "副麻医师1";
            this.btnAp2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lbPatName);
            this.groupBox1.Controls.Add(this.lbSSMC);
            this.groupBox1.Controls.Add(this.lbRoomName);
            this.groupBox1.Controls.Add(this.lbSSYS);
            this.groupBox1.Controls.Add(this.lbOname1);
            this.groupBox1.Controls.Add(this.lbPatName1);
            this.groupBox1.Controls.Add(this.lbOS);
            this.groupBox1.Controls.Add(this.lbKeshi);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(956, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前选中病人";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbKeshi);
            this.groupBox2.Controls.Add(this.btnConfig);
            this.groupBox2.Controls.Add(this.cbTime);
            this.groupBox2.Controls.Add(this.cbSecond);
            this.groupBox2.Controls.Add(this.cbOroom);
            this.groupBox2.Location = new System.Drawing.Point(526, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(227, 89);
            this.groupBox2.TabIndex = 654;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择排序方式";
            // 
            // cbKeshi
            // 
            this.cbKeshi.AutoSize = true;
            this.cbKeshi.Location = new System.Drawing.Point(95, 55);
            this.cbKeshi.Name = "cbKeshi";
            this.cbKeshi.Size = new System.Drawing.Size(56, 24);
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
            // cbTime
            // 
            this.cbTime.AutoSize = true;
            this.cbTime.Location = new System.Drawing.Point(15, 54);
            this.cbTime.Name = "cbTime";
            this.cbTime.Size = new System.Drawing.Size(56, 24);
            this.cbTime.TabIndex = 651;
            this.cbTime.Text = "时间";
            this.cbTime.UseVisualStyleBackColor = true;
            // 
            // cbSecond
            // 
            this.cbSecond.AutoSize = true;
            this.cbSecond.Location = new System.Drawing.Point(95, 26);
            this.cbSecond.Name = "cbSecond";
            this.cbSecond.Size = new System.Drawing.Size(56, 24);
            this.cbSecond.TabIndex = 652;
            this.cbSecond.Text = "台次";
            this.cbSecond.UseVisualStyleBackColor = true;
            // 
            // cbOroom
            // 
            this.cbOroom.AutoSize = true;
            this.cbOroom.Location = new System.Drawing.Point(15, 26);
            this.cbOroom.Name = "cbOroom";
            this.cbOroom.Size = new System.Drawing.Size(70, 24);
            this.cbOroom.TabIndex = 650;
            this.cbOroom.Text = "手术间";
            this.cbOroom.UseVisualStyleBackColor = true;
            // 
            // lbPatName
            // 
            this.lbPatName.AutoSize = true;
            this.lbPatName.Location = new System.Drawing.Point(108, 27);
            this.lbPatName.Name = "lbPatName";
            this.lbPatName.Size = new System.Drawing.Size(0, 20);
            this.lbPatName.TabIndex = 9;
            // 
            // lbSSMC
            // 
            this.lbSSMC.AutoSize = true;
            this.lbSSMC.Location = new System.Drawing.Point(309, 58);
            this.lbSSMC.Name = "lbSSMC";
            this.lbSSMC.Size = new System.Drawing.Size(0, 20);
            this.lbSSMC.TabIndex = 7;
            // 
            // lbRoomName
            // 
            this.lbRoomName.AutoSize = true;
            this.lbRoomName.ForeColor = System.Drawing.Color.Maroon;
            this.lbRoomName.Location = new System.Drawing.Point(248, 27);
            this.lbRoomName.Name = "lbRoomName";
            this.lbRoomName.Size = new System.Drawing.Size(51, 20);
            this.lbRoomName.TabIndex = 6;
            this.lbRoomName.Text = "科室：";
            // 
            // lbSSYS
            // 
            this.lbSSYS.AutoSize = true;
            this.lbSSYS.ForeColor = System.Drawing.Color.Maroon;
            this.lbSSYS.Location = new System.Drawing.Point(23, 57);
            this.lbSSYS.Name = "lbSSYS";
            this.lbSSYS.Size = new System.Drawing.Size(79, 20);
            this.lbSSYS.TabIndex = 5;
            this.lbSSYS.Text = "手术医师：";
            // 
            // lbOname1
            // 
            this.lbOname1.AutoSize = true;
            this.lbOname1.ForeColor = System.Drawing.Color.Maroon;
            this.lbOname1.Location = new System.Drawing.Point(221, 57);
            this.lbOname1.Name = "lbOname1";
            this.lbOname1.Size = new System.Drawing.Size(79, 20);
            this.lbOname1.TabIndex = 4;
            this.lbOname1.Text = "手术名称：";
            // 
            // lbPatName1
            // 
            this.lbPatName1.AutoSize = true;
            this.lbPatName1.ForeColor = System.Drawing.Color.Maroon;
            this.lbPatName1.Location = new System.Drawing.Point(51, 27);
            this.lbPatName1.Name = "lbPatName1";
            this.lbPatName1.Size = new System.Drawing.Size(51, 20);
            this.lbPatName1.TabIndex = 3;
            this.lbPatName1.Text = "姓名：";
            // 
            // lbOS
            // 
            this.lbOS.AutoSize = true;
            this.lbOS.Location = new System.Drawing.Point(108, 57);
            this.lbOS.Name = "lbOS";
            this.lbOS.Size = new System.Drawing.Size(0, 20);
            this.lbOS.TabIndex = 2;
            // 
            // lbKeshi
            // 
            this.lbKeshi.AutoSize = true;
            this.lbKeshi.Location = new System.Drawing.Point(309, 28);
            this.lbKeshi.Name = "lbKeshi";
            this.lbKeshi.Size = new System.Drawing.Size(0, 20);
            this.lbKeshi.TabIndex = 1;
            // 
            // buttonConfig
            // 
            this.buttonConfig.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.buttonConfig.Image = global::main.Properties.Resources.Lock;
            this.buttonConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConfig.Location = new System.Drawing.Point(50, 319);
            this.buttonConfig.Name = "buttonConfig";
            this.buttonConfig.Size = new System.Drawing.Size(132, 40);
            this.buttonConfig.TabIndex = 646;
            this.buttonConfig.Text = "单条确认排班";
            this.buttonConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonConfig.UseVisualStyleBackColor = true;
            this.buttonConfig.Visible = false;
            this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // btnE
            // 
            this.btnE.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnE.Image = global::main.Properties.Resources.Add;
            this.btnE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnE.Location = new System.Drawing.Point(38, 181);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(132, 40);
            this.btnE.TabIndex = 640;
            this.btnE.Text = "查看急诊手术";
            this.btnE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Visible = false;
            this.btnE.Click += new System.EventHandler(this.btnE_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "手术申请日期：";
            // 
            // dtDataTime
            // 
            this.dtDataTime.CustomFormat = "";
            this.dtDataTime.Location = new System.Drawing.Point(48, 34);
            this.dtDataTime.Name = "dtDataTime";
            this.dtDataTime.Size = new System.Drawing.Size(134, 26);
            this.dtDataTime.TabIndex = 0;
            this.dtDataTime.Value = new System.DateTime(2014, 5, 9, 11, 5, 21, 0);
            this.dtDataTime.ValueChanged += new System.EventHandler(this.dtDataTime_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPre);
            this.panel1.Controls.Add(this.buttonConfig);
            this.panel1.Controls.Add(this.btnOperInfo);
            this.panel1.Controls.Add(this.lblTS);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnisJZ);
            this.panel1.Controls.Add(this.btnPrintResult);
            this.panel1.Controls.Add(this.btnNO);
            this.panel1.Controls.Add(this.listboxRoom);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnYES);
            this.panel1.Controls.Add(this.btnIntelligent);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.dtDataTime);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 742);
            this.panel1.TabIndex = 3;
            // 
            // btnOperInfo
            // 
            this.btnOperInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOperInfo.ForeColor = System.Drawing.Color.Blue;
            this.btnOperInfo.Image = global::main.Properties.Resources.Print;
            this.btnOperInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOperInfo.Location = new System.Drawing.Point(48, 518);
            this.btnOperInfo.Name = "btnOperInfo";
            this.btnOperInfo.Size = new System.Drawing.Size(141, 40);
            this.btnOperInfo.TabIndex = 645;
            this.btnOperInfo.Text = "打印手术通知";
            this.btnOperInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOperInfo.UseVisualStyleBackColor = true;
            this.btnOperInfo.Click += new System.EventHandler(this.btnJiZheng_Click);
            // 
            // lblTS
            // 
            this.lblTS.AutoSize = true;
            this.lblTS.Location = new System.Drawing.Point(137, 583);
            this.lblTS.Name = "lblTS";
            this.lblTS.Size = new System.Drawing.Size(50, 20);
            this.lblTS.TabIndex = 644;
            this.lblTS.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 583);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 643;
            this.label2.Text = "手术的台数：";
            // 
            // btnisJZ
            // 
            this.btnisJZ.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnisJZ.ForeColor = System.Drawing.Color.Blue;
            this.btnisJZ.Image = global::main.Properties.Resources.Print;
            this.btnisJZ.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnisJZ.Location = new System.Drawing.Point(50, 673);
            this.btnisJZ.Name = "btnisJZ";
            this.btnisJZ.Size = new System.Drawing.Size(141, 40);
            this.btnisJZ.TabIndex = 642;
            this.btnisJZ.Text = "打印接病人流程";
            this.btnisJZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnisJZ.UseVisualStyleBackColor = true;
            this.btnisJZ.Visible = false;
            this.btnisJZ.Click += new System.EventHandler(this.btnisJZ_Click);
            // 
            // btnPrintResult
            // 
            this.btnPrintResult.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrintResult.ForeColor = System.Drawing.Color.Blue;
            this.btnPrintResult.Image = global::main.Properties.Resources.Query;
            this.btnPrintResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintResult.Location = new System.Drawing.Point(50, 699);
            this.btnPrintResult.Name = "btnPrintResult";
            this.btnPrintResult.Size = new System.Drawing.Size(141, 40);
            this.btnPrintResult.TabIndex = 641;
            this.btnPrintResult.Text = "查看排班结果";
            this.btnPrintResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintResult.UseVisualStyleBackColor = true;
            this.btnPrintResult.Visible = false;
            this.btnPrintResult.Click += new System.EventHandler(this.btnPrintResult_Click);
            // 
            // btnNO
            // 
            this.btnNO.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNO.Image = global::main.Properties.Resources.Remove3;
            this.btnNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNO.Location = new System.Drawing.Point(48, 380);
            this.btnNO.Name = "btnNO";
            this.btnNO.Size = new System.Drawing.Size(141, 40);
            this.btnNO.TabIndex = 639;
            this.btnNO.Text = "未排班手术";
            this.btnNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNO.UseVisualStyleBackColor = true;
            this.btnNO.Click += new System.EventHandler(this.btnNO_Click);
            // 
            // listboxRoom
            // 
            this.listboxRoom.FormattingEnabled = true;
            this.listboxRoom.ItemHeight = 20;
            this.listboxRoom.Items.AddRange(new object[] {
            "全部手术间"});
            this.listboxRoom.Location = new System.Drawing.Point(50, 130);
            this.listboxRoom.Name = "listboxRoom";
            this.listboxRoom.Size = new System.Drawing.Size(134, 244);
            this.listboxRoom.TabIndex = 638;
            this.listboxRoom.SelectedIndexChanged += new System.EventHandler(this.listboxRoom_SelectedIndexChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.ForeColor = System.Drawing.Color.Blue;
            this.btnPrint.Image = global::main.Properties.Resources.Print;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(48, 472);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(141, 40);
            this.btnPrint.TabIndex = 637;
            this.btnPrint.Text = "打印通知信息";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnYES
            // 
            this.btnYES.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnYES.Image = global::main.Properties.Resources.List;
            this.btnYES.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnYES.Location = new System.Drawing.Point(50, 426);
            this.btnYES.Name = "btnYES";
            this.btnYES.Size = new System.Drawing.Size(141, 40);
            this.btnYES.TabIndex = 125;
            this.btnYES.Text = "已排班手术";
            this.btnYES.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnYES.UseVisualStyleBackColor = true;
            this.btnYES.Click += new System.EventHandler(this.btnYES_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.button4.Image = global::main.Properties.Resources.Refresh;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(50, 97);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(132, 30);
            this.button4.TabIndex = 124;
            this.button4.Text = "刷新HIS申请表";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.dgvOTypesetting);
            this.panel2.Controls.Add(this.btnE);
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(216, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(956, 730);
            this.panel2.TabIndex = 4;
            // 
            // printPreviewDialog2
            // 
            this.printPreviewDialog2.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog2.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog2.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog2.Document = this.printDocument2;
            this.printPreviewDialog2.Enabled = true;
            this.printPreviewDialog2.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog2.Icon")));
            this.printPreviewDialog2.Name = "printPreviewDialog2";
            this.printPreviewDialog2.Visible = false;
            // 
            // printDocument2
            // 
            this.printDocument2.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument2_BeginPrint);
            this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
            // 
            // printPreviewDialog3
            // 
            this.printPreviewDialog3.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog3.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog3.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog3.Document = this.printDocument3;
            this.printPreviewDialog3.Enabled = true;
            this.printPreviewDialog3.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog3.Icon")));
            this.printPreviewDialog3.Name = "printPreviewDialog3";
            this.printPreviewDialog3.Visible = false;
            // 
            // printDocument3
            // 
            this.printDocument3.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument3_BeginPrint);
            this.printDocument3.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument3_PrintPage);
            // 
            // btnPre
            // 
            this.btnPre.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPre.ForeColor = System.Drawing.Color.Blue;
            this.btnPre.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPre.Location = new System.Drawing.Point(48, 63);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(56, 30);
            this.btnPre.TabIndex = 647;
            this.btnPre.Text = "前一天";
            this.btnPre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNext.ForeColor = System.Drawing.Color.Blue;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(125, 63);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(57, 30);
            this.btnNext.TabIndex = 648;
            this.btnNext.Text = "后一天";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // PaiBanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 742);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "PaiBanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "排班";
            this.Load += new System.EventHandler(this.paiban_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOTypesetting)).EndInit();
            this.ctmsPaibanbiao.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button btnAp3;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnIntelligent;
        private System.Windows.Forms.Button btnPrintView;
        private System.Windows.Forms.DataGridView dgvOTypesetting;
        private System.Windows.Forms.Button btnAp2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbSSMC;
        private System.Windows.Forms.Label lbRoomName;
        private System.Windows.Forms.Label lbSSYS;
        private System.Windows.Forms.Label lbOname1;
        private System.Windows.Forms.Label lbPatName1;
        private System.Windows.Forms.Label lbOS;
        private System.Windows.Forms.Label lbKeshi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtDataTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnYES;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listboxRoom;
        private System.Windows.Forms.Label lbPatName;
        private System.Windows.Forms.Button btnNO;
        private System.Windows.Forms.Button btnE;
        private System.Windows.Forms.ContextMenuStrip ctmsPaibanbiao;
        private System.Windows.Forms.ToolStripMenuItem EnterMZD_ToolStripMenuItem;
        private System.Windows.Forms.Button btnPrintResult;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbKeshi;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.CheckBox cbTime;
        private System.Windows.Forms.CheckBox cbSecond;
        private System.Windows.Forms.CheckBox cbOroom;
        private System.Windows.Forms.Button btnisJZ;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog2;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Windows.Forms.Label lblTS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOperInfo;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog3;
        private System.Drawing.Printing.PrintDocument printDocument3;
        private System.Windows.Forms.Button buttonConfig;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn oroom;
        private System.Windows.Forms.DataGridViewTextBoxColumn second;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn os;
        private System.Windows.Forms.DataGridViewTextBoxColumn os1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OS2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatZhuYuanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn patname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn oname;
        private System.Windows.Forms.DataGridViewTextBoxColumn isGeli;
        private System.Windows.Forms.DataGridViewTextBoxColumn pattmd;
        private System.Windows.Forms.DataGridViewTextBoxColumn patsex;
        private System.Windows.Forms.DataGridViewTextBoxColumn patage;
        private System.Windows.Forms.DataGridViewTextBoxColumn patdpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Patbedno;
        private System.Windows.Forms.DataGridViewTextBoxColumn TalkInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn patNation;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiwei;
        private System.Windows.Forms.DataGridViewTextBoxColumn expertName;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn on1;
        private System.Windows.Forms.DataGridViewTextBoxColumn on2;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap3;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqys;
        private System.Windows.Forms.DataGridViewTextBoxColumn sslb;
        private System.Windows.Forms.DataGridViewTextBoxColumn patid;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
    }
}