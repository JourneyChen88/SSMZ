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
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnAp3 = new System.Windows.Forms.Button();
            this.btnPrintView = new System.Windows.Forms.Button();
            this.dgvOTypesetting = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.on1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.on2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.applyid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatZhuYuanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asae = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmsPaibanbiao = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EnterMZD_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSQRPBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSJMWCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSQXSSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hbbrsqdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hcsydjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAp2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbOroom = new System.Windows.Forms.CheckBox();
            this.lbASAE = new System.Windows.Forms.Label();
            this.cbKeshi = new System.Windows.Forms.CheckBox();
            this.lbPatName = new System.Windows.Forms.Label();
            this.lbSSMC = new System.Windows.Forms.Label();
            this.cbOld = new System.Windows.Forms.CheckBox();
            this.cbNew = new System.Windows.Forms.CheckBox();
            this.lbRoomName = new System.Windows.Forms.Label();
            this.lbSSYS = new System.Windows.Forms.Label();
            this.lbOname1 = new System.Windows.Forms.Label();
            this.lbPatName1 = new System.Windows.Forms.Label();
            this.lbOS = new System.Windows.Forms.Label();
            this.lbKeshi = new System.Windows.Forms.Label();
            this.cbEye = new System.Windows.Forms.CheckBox();
            this.lbOutPut = new System.Windows.Forms.Label();
            this.btnAllConfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtDataTime = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPrintSort = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.btnNO = new System.Windows.Forms.Button();
            this.listboxRoom = new System.Windows.Forms.ListBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnYES = new System.Windows.Forms.Button();
            this.btnHisRefresh = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOTypesetting)).BeginInit();
            this.ctmsPaibanbiao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
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
            this.dgvOTypesetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOTypesetting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOTypesetting.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOTypesetting.ColumnHeadersHeight = 25;
            this.dgvOTypesetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
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
            this.on1,
            this.on2,
            this.sn1,
            this.sn2,
            this.Remarks,
            this.ap1,
            this.ap2,
            this.ap3,
            this.applyid,
            this.PatZhuYuanID,
            this.patid,
            this.asae});
            this.dgvOTypesetting.ContextMenuStrip = this.ctmsPaibanbiao;
            this.dgvOTypesetting.Location = new System.Drawing.Point(0, 95);
            this.dgvOTypesetting.MultiSelect = false;
            this.dgvOTypesetting.Name = "dgvOTypesetting";
            this.dgvOTypesetting.RowHeadersVisible = false;
            this.dgvOTypesetting.RowTemplate.Height = 23;
            this.dgvOTypesetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOTypesetting.Size = new System.Drawing.Size(943, 579);
            this.dgvOTypesetting.TabIndex = 0;
            this.dgvOTypesetting.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOTypesetting_CellClick);
            this.dgvOTypesetting.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOTypesetting_CellDoubleClick);
            this.dgvOTypesetting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvOTypesetting_EditingControlShowing);
            this.dgvOTypesetting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvOTypesetting_KeyPress);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 42;
            // 
            // Oroom
            // 
            this.Oroom.DataPropertyName = "oroom";
            this.Oroom.Frozen = true;
            this.Oroom.HeaderText = "手术间";
            this.Oroom.MinimumWidth = 80;
            this.Oroom.Name = "Oroom";
            this.Oroom.Width = 80;
            // 
            // second
            // 
            this.second.DataPropertyName = "second";
            this.second.Frozen = true;
            this.second.HeaderText = "台次";
            this.second.Name = "second";
            this.second.Width = 62;
            // 
            // patdpm
            // 
            this.patdpm.DataPropertyName = "patdpm";
            this.patdpm.Frozen = true;
            this.patdpm.HeaderText = "科室";
            this.patdpm.Name = "patdpm";
            this.patdpm.ReadOnly = true;
            this.patdpm.Width = 62;
            // 
            // Patbedno
            // 
            this.Patbedno.DataPropertyName = "Patbedno";
            this.Patbedno.Frozen = true;
            this.Patbedno.HeaderText = "床号";
            this.Patbedno.Name = "Patbedno";
            this.Patbedno.ReadOnly = true;
            this.Patbedno.Width = 62;
            // 
            // patname
            // 
            this.patname.DataPropertyName = "patname";
            this.patname.Frozen = true;
            this.patname.HeaderText = "病人姓名";
            this.patname.Name = "patname";
            this.patname.ReadOnly = true;
            this.patname.Width = 90;
            // 
            // patAge
            // 
            this.patAge.DataPropertyName = "patAge";
            this.patAge.HeaderText = "年龄";
            this.patAge.Name = "patAge";
            this.patAge.ReadOnly = true;
            this.patAge.Width = 62;
            // 
            // patsex
            // 
            this.patsex.DataPropertyName = "patsex";
            this.patsex.HeaderText = "性别";
            this.patsex.Name = "patsex";
            this.patsex.ReadOnly = true;
            this.patsex.Width = 62;
            // 
            // oname
            // 
            this.oname.DataPropertyName = "oname";
            this.oname.HeaderText = "手术名称";
            this.oname.Name = "oname";
            this.oname.ReadOnly = true;
            this.oname.Width = 90;
            // 
            // pattmd
            // 
            this.pattmd.DataPropertyName = "pattmd";
            this.pattmd.HeaderText = "术前诊断";
            this.pattmd.Name = "pattmd";
            this.pattmd.ReadOnly = true;
            this.pattmd.Width = 90;
            // 
            // OS
            // 
            this.OS.DataPropertyName = "os";
            this.OS.HeaderText = "手术医生";
            this.OS.Name = "OS";
            this.OS.ReadOnly = true;
            this.OS.Width = 90;
            // 
            // Amethod
            // 
            this.Amethod.DataPropertyName = "Amethod";
            this.Amethod.HeaderText = "麻醉方法";
            this.Amethod.Name = "Amethod";
            this.Amethod.ReadOnly = true;
            this.Amethod.Width = 90;
            // 
            // on1
            // 
            this.on1.DataPropertyName = "on1";
            this.on1.HeaderText = "洗手护士";
            this.on1.Name = "on1";
            this.on1.Width = 90;
            // 
            // on2
            // 
            this.on2.DataPropertyName = "on2";
            this.on2.HeaderText = "洗手护士2";
            this.on2.Name = "on2";
            this.on2.Width = 98;
            // 
            // sn1
            // 
            this.sn1.DataPropertyName = "sn1";
            this.sn1.HeaderText = "巡回护士";
            this.sn1.Name = "sn1";
            this.sn1.Width = 90;
            // 
            // sn2
            // 
            this.sn2.DataPropertyName = "sn2";
            this.sn2.HeaderText = "巡回护士2";
            this.sn2.Name = "sn2";
            this.sn2.Width = 98;
            // 
            // Remarks
            // 
            this.Remarks.DataPropertyName = "Remarks";
            this.Remarks.HeaderText = "备注";
            this.Remarks.MinimumWidth = 80;
            this.Remarks.Name = "Remarks";
            this.Remarks.Width = 80;
            // 
            // ap1
            // 
            this.ap1.DataPropertyName = "ap1";
            this.ap1.HeaderText = "麻醉医师";
            this.ap1.Name = "ap1";
            this.ap1.Width = 90;
            // 
            // ap2
            // 
            this.ap2.DataPropertyName = "ap2";
            this.ap2.HeaderText = "副醉医师1";
            this.ap2.Name = "ap2";
            this.ap2.Width = 98;
            // 
            // ap3
            // 
            this.ap3.DataPropertyName = "ap3";
            this.ap3.HeaderText = "副醉医师2";
            this.ap3.Name = "ap3";
            this.ap3.Width = 98;
            // 
            // applyid
            // 
            this.applyid.DataPropertyName = "applyid";
            this.applyid.HeaderText = "流水号";
            this.applyid.Name = "applyid";
            this.applyid.ReadOnly = true;
            this.applyid.Visible = false;
            this.applyid.Width = 66;
            // 
            // PatZhuYuanID
            // 
            this.PatZhuYuanID.DataPropertyName = "PatZhuYuanID";
            this.PatZhuYuanID.HeaderText = "住院号";
            this.PatZhuYuanID.Name = "PatZhuYuanID";
            this.PatZhuYuanID.ReadOnly = true;
            this.PatZhuYuanID.Visible = false;
            this.PatZhuYuanID.Width = 66;
            // 
            // patid
            // 
            this.patid.DataPropertyName = "patid";
            this.patid.HeaderText = "病人编号";
            this.patid.Name = "patid";
            this.patid.ReadOnly = true;
            this.patid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.patid.Visible = false;
            this.patid.Width = 78;
            // 
            // asae
            // 
            this.asae.DataPropertyName = "asae";
            this.asae.HeaderText = "急诊";
            this.asae.Name = "asae";
            this.asae.Visible = false;
            this.asae.Width = 54;
            // 
            // ctmsPaibanbiao
            // 
            this.ctmsPaibanbiao.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctmsPaibanbiao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnterMZD_ToolStripMenuItem,
            this.TSQRPBToolStripMenuItem,
            this.TSJMWCToolStripMenuItem,
            this.TSQXSSToolStripMenuItem,
            this.hbbrsqdToolStripMenuItem,
            this.UpdateDateToolStripMenuItem,
            this.hcsydjToolStripMenuItem});
            this.ctmsPaibanbiao.Name = "contextMenuStrip1";
            this.ctmsPaibanbiao.Size = new System.Drawing.Size(161, 158);
            // 
            // EnterMZD_ToolStripMenuItem
            // 
            this.EnterMZD_ToolStripMenuItem.Name = "EnterMZD_ToolStripMenuItem";
            this.EnterMZD_ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.EnterMZD_ToolStripMenuItem.Text = "进入麻醉记录单";
            this.EnterMZD_ToolStripMenuItem.Visible = false;
            this.EnterMZD_ToolStripMenuItem.Click += new System.EventHandler(this.EnterMZD_ToolStripMenuItem_Click);
            // 
            // TSQRPBToolStripMenuItem
            // 
            this.TSQRPBToolStripMenuItem.Name = "TSQRPBToolStripMenuItem";
            this.TSQRPBToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.TSQRPBToolStripMenuItem.Text = "确认排班";
            this.TSQRPBToolStripMenuItem.Click += new System.EventHandler(this.TSQRPBToolStripMenuItem_Click);
            // 
            // TSJMWCToolStripMenuItem
            // 
            this.TSJMWCToolStripMenuItem.Name = "TSJMWCToolStripMenuItem";
            this.TSJMWCToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.TSJMWCToolStripMenuItem.Text = "局麻完成";
            this.TSJMWCToolStripMenuItem.Click += new System.EventHandler(this.TSJMWCToolStripMenuItem_Click);
            // 
            // TSQXSSToolStripMenuItem
            // 
            this.TSQXSSToolStripMenuItem.Name = "TSQXSSToolStripMenuItem";
            this.TSQXSSToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.TSQXSSToolStripMenuItem.Text = "取消手术";
            this.TSQXSSToolStripMenuItem.Click += new System.EventHandler(this.TSQXSSToolStripMenuItem_Click);
            // 
            // hbbrsqdToolStripMenuItem
            // 
            this.hbbrsqdToolStripMenuItem.Name = "hbbrsqdToolStripMenuItem";
            this.hbbrsqdToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.hbbrsqdToolStripMenuItem.Text = "合并病人申请单";
            this.hbbrsqdToolStripMenuItem.Click += new System.EventHandler(this.hbbrsqdToolStripMenuItem_Click);
            // 
            // UpdateDateToolStripMenuItem
            // 
            this.UpdateDateToolStripMenuItem.Name = "UpdateDateToolStripMenuItem";
            this.UpdateDateToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.UpdateDateToolStripMenuItem.Text = "修改手术日期";
            this.UpdateDateToolStripMenuItem.Click += new System.EventHandler(this.UpdateDateToolStripMenuItem_Click);
            // 
            // hcsydjToolStripMenuItem
            // 
            this.hcsydjToolStripMenuItem.Name = "hcsydjToolStripMenuItem";
            this.hcsydjToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.hcsydjToolStripMenuItem.Text = "耗材使用登记";
            this.hcsydjToolStripMenuItem.Click += new System.EventHandler(this.hcsydjToolStripMenuItem_Click);
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
            this.groupBox1.Controls.Add(this.cbOroom);
            this.groupBox1.Controls.Add(this.lbASAE);
            this.groupBox1.Controls.Add(this.cbKeshi);
            this.groupBox1.Controls.Add(this.lbPatName);
            this.groupBox1.Controls.Add(this.lbSSMC);
            this.groupBox1.Controls.Add(this.cbOld);
            this.groupBox1.Controls.Add(this.cbNew);
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
            this.groupBox1.Size = new System.Drawing.Size(943, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前选中病人";
            // 
            // cbOroom
            // 
            this.cbOroom.AutoSize = true;
            this.cbOroom.Location = new System.Drawing.Point(771, 47);
            this.cbOroom.Name = "cbOroom";
            this.cbOroom.Size = new System.Drawing.Size(70, 24);
            this.cbOroom.TabIndex = 650;
            this.cbOroom.Text = "手术间";
            this.cbOroom.UseVisualStyleBackColor = true;
            this.cbOroom.Visible = false;
            // 
            // lbASAE
            // 
            this.lbASAE.AutoSize = true;
            this.lbASAE.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbASAE.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lbASAE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbASAE.Location = new System.Drawing.Point(613, 39);
            this.lbASAE.Name = "lbASAE";
            this.lbASAE.Size = new System.Drawing.Size(0, 25);
            this.lbASAE.TabIndex = 655;
            // 
            // cbKeshi
            // 
            this.cbKeshi.AutoSize = true;
            this.cbKeshi.Location = new System.Drawing.Point(772, 19);
            this.cbKeshi.Name = "cbKeshi";
            this.cbKeshi.Size = new System.Drawing.Size(56, 24);
            this.cbKeshi.TabIndex = 654;
            this.cbKeshi.Text = "科室";
            this.cbKeshi.UseVisualStyleBackColor = true;
            this.cbKeshi.Visible = false;
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
            // cbOld
            // 
            this.cbOld.AutoSize = true;
            this.cbOld.Location = new System.Drawing.Point(847, 20);
            this.cbOld.Name = "cbOld";
            this.cbOld.Size = new System.Drawing.Size(56, 24);
            this.cbOld.TabIndex = 654;
            this.cbOld.Text = "老院";
            this.cbOld.UseVisualStyleBackColor = true;
            this.cbOld.Visible = false;
            // 
            // cbNew
            // 
            this.cbNew.AutoSize = true;
            this.cbNew.Location = new System.Drawing.Point(847, 50);
            this.cbNew.Name = "cbNew";
            this.cbNew.Size = new System.Drawing.Size(56, 24);
            this.cbNew.TabIndex = 653;
            this.cbNew.Text = "新院";
            this.cbNew.UseVisualStyleBackColor = true;
            this.cbNew.Visible = false;
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
            // cbEye
            // 
            this.cbEye.AutoSize = true;
            this.cbEye.Location = new System.Drawing.Point(103, 284);
            this.cbEye.Name = "cbEye";
            this.cbEye.Size = new System.Drawing.Size(84, 24);
            this.cbEye.TabIndex = 656;
            this.cbEye.Text = "眼科手术";
            this.cbEye.UseVisualStyleBackColor = true;
            this.cbEye.CheckedChanged += new System.EventHandler(this.cbEye_CheckedChanged);
            // 
            // lbOutPut
            // 
            this.lbOutPut.AutoSize = true;
            this.lbOutPut.ForeColor = System.Drawing.Color.Maroon;
            this.lbOutPut.Location = new System.Drawing.Point(44, 645);
            this.lbOutPut.Name = "lbOutPut";
            this.lbOutPut.Size = new System.Drawing.Size(21, 20);
            this.lbOutPut.TabIndex = 656;
            this.lbOutPut.Text = "ss";
            this.lbOutPut.Visible = false;
            // 
            // btnAllConfig
            // 
            this.btnAllConfig.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnAllConfig.ForeColor = System.Drawing.Color.Blue;
            this.btnAllConfig.Image = global::main.Properties.Resources.Category;
            this.btnAllConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllConfig.Location = new System.Drawing.Point(48, 460);
            this.btnAllConfig.Name = "btnAllConfig";
            this.btnAllConfig.Size = new System.Drawing.Size(132, 40);
            this.btnAllConfig.TabIndex = 653;
            this.btnAllConfig.Text = "全部排班确认";
            this.btnAllConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAllConfig.UseVisualStyleBackColor = true;
            this.btnAllConfig.Click += new System.EventHandler(this.btnAllConfig_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "手术申请日期：";
            // 
            // dtDataTime
            // 
            this.dtDataTime.CustomFormat = "";
            this.dtDataTime.Location = new System.Drawing.Point(48, 43);
            this.dtDataTime.Name = "dtDataTime";
            this.dtDataTime.Size = new System.Drawing.Size(134, 26);
            this.dtDataTime.TabIndex = 0;
            this.dtDataTime.Value = new System.DateTime(2014, 5, 9, 11, 5, 21, 0);
            this.dtDataTime.ValueChanged += new System.EventHandler(this.dtDataTime_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbEye);
            this.panel1.Controls.Add(this.lbOutPut);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.btnAllConfig);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnPrintSort);
            this.panel1.Controls.Add(this.btnE);
            this.panel1.Controls.Add(this.btnNO);
            this.panel1.Controls.Add(this.listboxRoom);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnYES);
            this.panel1.Controls.Add(this.btnHisRefresh);
            this.panel1.Controls.Add(this.dtDataTime);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 594);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 564);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 658;
            this.label2.Text = "打印份数：";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(125, 562);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(53, 26);
            this.numericUpDown1.TabIndex = 657;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(3, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 23);
            this.button2.TabIndex = 656;
            this.button2.Text = "昨天";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(184, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 23);
            this.button1.TabIndex = 655;
            this.button1.Text = "明天";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnPrintSort
            // 
            this.btnPrintSort.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrintSort.ForeColor = System.Drawing.Color.Blue;
            this.btnPrintSort.Image = global::main.Properties.Resources.Query;
            this.btnPrintSort.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintSort.Location = new System.Drawing.Point(48, 516);
            this.btnPrintSort.Name = "btnPrintSort";
            this.btnPrintSort.Size = new System.Drawing.Size(134, 40);
            this.btnPrintSort.TabIndex = 641;
            this.btnPrintSort.Text = "按手术间排序";
            this.btnPrintSort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintSort.UseVisualStyleBackColor = true;
            this.btnPrintSort.Click += new System.EventHandler(this.btnPrintSort_Click);
            // 
            // btnE
            // 
            this.btnE.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnE.Image = global::main.Properties.Resources.Add;
            this.btnE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnE.Location = new System.Drawing.Point(48, 313);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(132, 40);
            this.btnE.TabIndex = 640;
            this.btnE.Text = "查看急诊手术";
            this.btnE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Click += new System.EventHandler(this.btnE_Click);
            // 
            // btnNO
            // 
            this.btnNO.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNO.Image = global::main.Properties.Resources.Remove3;
            this.btnNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNO.Location = new System.Drawing.Point(48, 362);
            this.btnNO.Name = "btnNO";
            this.btnNO.Size = new System.Drawing.Size(132, 40);
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
            this.listboxRoom.Location = new System.Drawing.Point(48, 110);
            this.listboxRoom.Name = "listboxRoom";
            this.listboxRoom.Size = new System.Drawing.Size(134, 164);
            this.listboxRoom.TabIndex = 638;
            this.listboxRoom.SelectedIndexChanged += new System.EventHandler(this.listboxRoom_SelectedIndexChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.ForeColor = System.Drawing.Color.Blue;
            this.btnPrint.Image = global::main.Properties.Resources.Print;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(48, 599);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(134, 40);
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
            this.btnYES.Location = new System.Drawing.Point(48, 412);
            this.btnYES.Name = "btnYES";
            this.btnYES.Size = new System.Drawing.Size(132, 40);
            this.btnYES.TabIndex = 125;
            this.btnYES.Text = "已排班手术";
            this.btnYES.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnYES.UseVisualStyleBackColor = true;
            this.btnYES.Click += new System.EventHandler(this.btnYES_Click);
            // 
            // btnHisRefresh
            // 
            this.btnHisRefresh.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnHisRefresh.Image = global::main.Properties.Resources.Refresh;
            this.btnHisRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHisRefresh.Location = new System.Drawing.Point(48, 78);
            this.btnHisRefresh.Name = "btnHisRefresh";
            this.btnHisRefresh.Size = new System.Drawing.Size(132, 29);
            this.btnHisRefresh.TabIndex = 124;
            this.btnHisRefresh.Text = "刷新HIS申请";
            this.btnHisRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHisRefresh.UseVisualStyleBackColor = true;
            this.btnHisRefresh.Click += new System.EventHandler(this.btnHisRefresh_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.dgvOTypesetting);
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(241, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(943, 674);
            this.panel2.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PaiBanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 594);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button btnAp3;
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
        private System.Windows.Forms.Button btnHisRefresh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listboxRoom;
        private System.Windows.Forms.Label lbPatName;
        private System.Windows.Forms.Button btnNO;
        private System.Windows.Forms.Button btnE;
        private System.Windows.Forms.ContextMenuStrip ctmsPaibanbiao;
        private System.Windows.Forms.ToolStripMenuItem EnterMZD_ToolStripMenuItem;
        private System.Windows.Forms.Button btnPrintSort;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.CheckBox cbKeshi;
        private System.Windows.Forms.CheckBox cbOroom;
        private System.Windows.Forms.CheckBox cbOld;
        private System.Windows.Forms.CheckBox cbNew;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem TSJMWCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSQXSSToolStripMenuItem;
        private System.Windows.Forms.Button btnAllConfig;
        private System.Windows.Forms.ToolStripMenuItem TSQRPBToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn on1;
        private System.Windows.Forms.DataGridViewTextBoxColumn on2;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap3;
        private System.Windows.Forms.DataGridViewTextBoxColumn applyid;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatZhuYuanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn patid;
        private System.Windows.Forms.DataGridViewTextBoxColumn asae;
        private System.Windows.Forms.Label lbASAE;
        private System.Windows.Forms.ToolStripMenuItem hbbrsqdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdateDateToolStripMenuItem;
        private System.Windows.Forms.Label lbOutPut;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbEye;
        private System.Windows.Forms.ToolStripMenuItem hcsydjToolStripMenuItem;

    }
}