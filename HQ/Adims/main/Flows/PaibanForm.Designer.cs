namespace main
{
    partial class PaibanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaibanForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnAp3 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnIntelligent = new System.Windows.Forms.Button();
            this.btnPrintView = new System.Windows.Forms.Button();
            this.dgvOTypesetting = new System.Windows.Forms.DataGridView();
            this.ctmsPaibanbiao = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EnterMZD_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiJZ = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZQ = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAp2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbSSmzfs = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbAmethod = new System.Windows.Forms.Label();
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
            this.lbPatZhuYuanID = new System.Windows.Forms.Label();
            this.lbKeshi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtDataTime = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnisJZ = new System.Windows.Forms.Button();
            this.btnPrintResult = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.btnNO = new System.Windows.Forms.Button();
            this.listboxRoom = new System.Windows.Forms.ListBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnYES = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oroom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.second = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatZhuYuanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patdpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patsex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patNation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pattmd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSmzfs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.os2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiwei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.on1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.on2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ap3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shengfen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patid = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.btnClear.Location = new System.Drawing.Point(48, 677);
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
            this.btnIntelligent.Location = new System.Drawing.Point(118, 677);
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
            this.PatZhuYuanID,
            this.patdpm,
            this.patname,
            this.patsex,
            this.patage,
            this.bedNo,
            this.patNation,
            this.pattmd,
            this.oname,
            this.Amethod,
            this.SSmzfs,
            this.os,
            this.os1,
            this.os2,
            this.tiwei,
            this.gr,
            this.bx,
            this.remarks,
            this.on1,
            this.on2,
            this.sn1,
            this.sn2,
            this.ap1,
            this.ap2,
            this.ap3,
            this.shengfen,
            this.id,
            this.patid});
            this.dgvOTypesetting.ContextMenuStrip = this.ctmsPaibanbiao;
            this.dgvOTypesetting.Location = new System.Drawing.Point(0, 121);
            this.dgvOTypesetting.MultiSelect = false;
            this.dgvOTypesetting.Name = "dgvOTypesetting";
            this.dgvOTypesetting.RowHeadersVisible = false;
            this.dgvOTypesetting.RowTemplate.Height = 23;
            this.dgvOTypesetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOTypesetting.Size = new System.Drawing.Size(915, 588);
            this.dgvOTypesetting.TabIndex = 0;
            this.dgvOTypesetting.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOTypesetting_CellClick);
            this.dgvOTypesetting.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvOTypesetting_CellValidating);
            this.dgvOTypesetting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvOTypesetting_EditingControlShowing);
            this.dgvOTypesetting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvOTypesetting_KeyPress);
            // 
            // ctmsPaibanbiao
            // 
            this.ctmsPaibanbiao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnterMZD_ToolStripMenuItem,
            this.tsmiJZ,
            this.tsmiZQ,
            this.tsmiDelete});
            this.ctmsPaibanbiao.Name = "contextMenuStrip1";
            this.ctmsPaibanbiao.Size = new System.Drawing.Size(161, 92);
            // 
            // EnterMZD_ToolStripMenuItem
            // 
            this.EnterMZD_ToolStripMenuItem.Name = "EnterMZD_ToolStripMenuItem";
            this.EnterMZD_ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.EnterMZD_ToolStripMenuItem.Text = "进入麻醉记录单";
            this.EnterMZD_ToolStripMenuItem.Visible = false;
            this.EnterMZD_ToolStripMenuItem.Click += new System.EventHandler(this.EnterMZD_ToolStripMenuItem_Click);
            // 
            // tsmiJZ
            // 
            this.tsmiJZ.Name = "tsmiJZ";
            this.tsmiJZ.Size = new System.Drawing.Size(160, 22);
            this.tsmiJZ.Text = "修改为急诊";
            this.tsmiJZ.Click += new System.EventHandler(this.tsmiJZ_Click);
            // 
            // tsmiZQ
            // 
            this.tsmiZQ.Name = "tsmiZQ";
            this.tsmiZQ.Size = new System.Drawing.Size(160, 22);
            this.tsmiZQ.Text = "修改为择期";
            this.tsmiZQ.Click += new System.EventHandler(this.tsmiZQ_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(160, 22);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
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
            this.groupBox1.Controls.Add(this.lbSSmzfs);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbAmethod);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lbPatName);
            this.groupBox1.Controls.Add(this.lbSSMC);
            this.groupBox1.Controls.Add(this.lbRoomName);
            this.groupBox1.Controls.Add(this.lbSSYS);
            this.groupBox1.Controls.Add(this.lbOname1);
            this.groupBox1.Controls.Add(this.lbPatName1);
            this.groupBox1.Controls.Add(this.lbPatZhuYuanID);
            this.groupBox1.Controls.Add(this.lbKeshi);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(915, 115);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前选中病人";
            // 
            // lbSSmzfs
            // 
            this.lbSSmzfs.AutoSize = true;
            this.lbSSmzfs.Location = new System.Drawing.Point(384, 90);
            this.lbSSmzfs.Name = "lbSSmzfs";
            this.lbSSmzfs.Size = new System.Drawing.Size(0, 20);
            this.lbSSmzfs.TabIndex = 658;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(6, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 657;
            this.label3.Text = "拟行麻醉方法：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(262, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 656;
            this.label4.Text = "实施麻醉方法：";
            // 
            // lbAmethod
            // 
            this.lbAmethod.AutoSize = true;
            this.lbAmethod.Location = new System.Drawing.Point(108, 89);
            this.lbAmethod.Name = "lbAmethod";
            this.lbAmethod.Size = new System.Drawing.Size(0, 20);
            this.lbAmethod.TabIndex = 655;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbKeshi);
            this.groupBox2.Controls.Add(this.btnConfig);
            this.groupBox2.Controls.Add(this.cbTime);
            this.groupBox2.Controls.Add(this.cbSecond);
            this.groupBox2.Controls.Add(this.cbOroom);
            this.groupBox2.Location = new System.Drawing.Point(577, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 109);
            this.groupBox2.TabIndex = 654;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择排序方式";
            // 
            // cbKeshi
            // 
            this.cbKeshi.AutoSize = true;
            this.cbKeshi.Location = new System.Drawing.Point(95, 68);
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
            this.btnConfig.Location = new System.Drawing.Point(170, 31);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(57, 57);
            this.btnConfig.TabIndex = 653;
            this.btnConfig.Text = "确定";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // cbTime
            // 
            this.cbTime.AutoSize = true;
            this.cbTime.Location = new System.Drawing.Point(15, 67);
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
            this.lbSSMC.Location = new System.Drawing.Point(384, 58);
            this.lbSSMC.Name = "lbSSMC";
            this.lbSSMC.Size = new System.Drawing.Size(0, 20);
            this.lbSSMC.TabIndex = 7;
            // 
            // lbRoomName
            // 
            this.lbRoomName.AutoSize = true;
            this.lbRoomName.ForeColor = System.Drawing.Color.Maroon;
            this.lbRoomName.Location = new System.Drawing.Point(318, 26);
            this.lbRoomName.Name = "lbRoomName";
            this.lbRoomName.Size = new System.Drawing.Size(51, 20);
            this.lbRoomName.TabIndex = 6;
            this.lbRoomName.Text = "科室：";
            // 
            // lbSSYS
            // 
            this.lbSSYS.AutoSize = true;
            this.lbSSYS.ForeColor = System.Drawing.Color.Maroon;
            this.lbSSYS.Location = new System.Drawing.Point(43, 57);
            this.lbSSYS.Name = "lbSSYS";
            this.lbSSYS.Size = new System.Drawing.Size(65, 20);
            this.lbSSYS.TabIndex = 5;
            this.lbSSYS.Text = "住院号：";
            // 
            // lbOname1
            // 
            this.lbOname1.AutoSize = true;
            this.lbOname1.ForeColor = System.Drawing.Color.Maroon;
            this.lbOname1.Location = new System.Drawing.Point(289, 58);
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
            // lbPatZhuYuanID
            // 
            this.lbPatZhuYuanID.AutoSize = true;
            this.lbPatZhuYuanID.Location = new System.Drawing.Point(108, 57);
            this.lbPatZhuYuanID.Name = "lbPatZhuYuanID";
            this.lbPatZhuYuanID.Size = new System.Drawing.Size(0, 20);
            this.lbPatZhuYuanID.TabIndex = 2;
            // 
            // lbKeshi
            // 
            this.lbKeshi.AutoSize = true;
            this.lbKeshi.Location = new System.Drawing.Point(384, 28);
            this.lbKeshi.Name = "lbKeshi";
            this.lbKeshi.Size = new System.Drawing.Size(0, 20);
            this.lbKeshi.TabIndex = 1;
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
            this.dtDataTime.Location = new System.Drawing.Point(48, 43);
            this.dtDataTime.Name = "dtDataTime";
            this.dtDataTime.Size = new System.Drawing.Size(134, 26);
            this.dtDataTime.TabIndex = 0;
            this.dtDataTime.Value = new System.DateTime(2014, 5, 9, 11, 5, 21, 0);
            this.dtDataTime.ValueChanged += new System.EventHandler(this.dtDataTime_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnisJZ);
            this.panel1.Controls.Add(this.btnPrintResult);
            this.panel1.Controls.Add(this.btnE);
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
            this.panel1.Size = new System.Drawing.Size(229, 721);
            this.panel1.TabIndex = 3;
            // 
            // btnisJZ
            // 
            this.btnisJZ.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnisJZ.ForeColor = System.Drawing.Color.Blue;
            this.btnisJZ.Image = global::main.Properties.Resources.Print;
            this.btnisJZ.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnisJZ.Location = new System.Drawing.Point(48, 623);
            this.btnisJZ.Name = "btnisJZ";
            this.btnisJZ.Size = new System.Drawing.Size(134, 40);
            this.btnisJZ.TabIndex = 642;
            this.btnisJZ.Text = "打印急诊信息";
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
            this.btnPrintResult.Location = new System.Drawing.Point(50, 531);
            this.btnPrintResult.Name = "btnPrintResult";
            this.btnPrintResult.Size = new System.Drawing.Size(134, 40);
            this.btnPrintResult.TabIndex = 641;
            this.btnPrintResult.Text = "查看排班结果";
            this.btnPrintResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintResult.UseVisualStyleBackColor = true;
            this.btnPrintResult.Click += new System.EventHandler(this.btnPrintResult_Click);
            // 
            // btnE
            // 
            this.btnE.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnE.Image = global::main.Properties.Resources.Add;
            this.btnE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnE.Location = new System.Drawing.Point(48, 132);
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
            this.btnNO.Location = new System.Drawing.Point(50, 435);
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
            this.listboxRoom.Location = new System.Drawing.Point(48, 185);
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
            this.btnPrint.Location = new System.Drawing.Point(48, 577);
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
            this.btnYES.Location = new System.Drawing.Point(50, 481);
            this.btnYES.Name = "btnYES";
            this.btnYES.Size = new System.Drawing.Size(132, 40);
            this.btnYES.TabIndex = 125;
            this.btnYES.Text = "已排班手术";
            this.btnYES.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnYES.UseVisualStyleBackColor = true;
            this.btnYES.Click += new System.EventHandler(this.btnYES_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.button4.Image = global::main.Properties.Resources.Refresh;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(50, 83);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(132, 40);
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
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(241, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(915, 721);
            this.panel2.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 19;
            // 
            // oroom
            // 
            this.oroom.DataPropertyName = "oroom";
            this.oroom.Frozen = true;
            this.oroom.HeaderText = "手术间";
            this.oroom.Name = "oroom";
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
            this.StartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StartTime.Width = 71;
            // 
            // PatZhuYuanID
            // 
            this.PatZhuYuanID.DataPropertyName = "PatZhuYuanID";
            this.PatZhuYuanID.Frozen = true;
            this.PatZhuYuanID.HeaderText = "住院号";
            this.PatZhuYuanID.Name = "PatZhuYuanID";
            this.PatZhuYuanID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatZhuYuanID.Visible = false;
            this.PatZhuYuanID.Width = 57;
            // 
            // patdpm
            // 
            this.patdpm.DataPropertyName = "patdpm";
            this.patdpm.Frozen = true;
            this.patdpm.HeaderText = "科室";
            this.patdpm.Name = "patdpm";
            this.patdpm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patdpm.Width = 43;
            // 
            // patname
            // 
            this.patname.DataPropertyName = "patname";
            this.patname.Frozen = true;
            this.patname.HeaderText = "姓名";
            this.patname.Name = "patname";
            this.patname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patname.Width = 43;
            // 
            // patsex
            // 
            this.patsex.DataPropertyName = "patsex";
            this.patsex.HeaderText = "性别";
            this.patsex.Name = "patsex";
            this.patsex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patsex.Width = 43;
            // 
            // patage
            // 
            this.patage.DataPropertyName = "patage";
            this.patage.HeaderText = "年龄";
            this.patage.Name = "patage";
            this.patage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patage.Width = 43;
            // 
            // bedNo
            // 
            this.bedNo.DataPropertyName = "patbedno";
            this.bedNo.HeaderText = "床号";
            this.bedNo.Name = "bedNo";
            this.bedNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.bedNo.Width = 43;
            // 
            // patNation
            // 
            this.patNation.DataPropertyName = "patNation";
            this.patNation.HeaderText = "民族";
            this.patNation.Name = "patNation";
            this.patNation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patNation.Width = 43;
            // 
            // pattmd
            // 
            this.pattmd.DataPropertyName = "pattmd";
            this.pattmd.HeaderText = "术前诊断";
            this.pattmd.Name = "pattmd";
            this.pattmd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pattmd.Width = 71;
            // 
            // oname
            // 
            this.oname.DataPropertyName = "oname";
            this.oname.HeaderText = "手术名称";
            this.oname.Name = "oname";
            this.oname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.oname.Width = 71;
            // 
            // Amethod
            // 
            this.Amethod.DataPropertyName = "Amethod";
            this.Amethod.HeaderText = "拟行麻醉方法";
            this.Amethod.Name = "Amethod";
            this.Amethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Amethod.Width = 99;
            // 
            // SSmzfs
            // 
            this.SSmzfs.DataPropertyName = "SSmzfs";
            this.SSmzfs.HeaderText = "实施麻醉方法";
            this.SSmzfs.Name = "SSmzfs";
            this.SSmzfs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SSmzfs.Width = 99;
            // 
            // os
            // 
            this.os.DataPropertyName = "os";
            this.os.HeaderText = "手术医生";
            this.os.Name = "os";
            this.os.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.os.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.os.Width = 71;
            // 
            // os1
            // 
            this.os1.DataPropertyName = "os1";
            this.os1.HeaderText = "第一助理";
            this.os1.Name = "os1";
            this.os1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.os1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.os1.Width = 71;
            // 
            // os2
            // 
            this.os2.DataPropertyName = "os2";
            this.os2.HeaderText = "第二助理";
            this.os2.Name = "os2";
            this.os2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.os2.Width = 71;
            // 
            // tiwei
            // 
            this.tiwei.DataPropertyName = "tiwei";
            this.tiwei.HeaderText = "手术体位";
            this.tiwei.Name = "tiwei";
            this.tiwei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tiwei.Width = 71;
            // 
            // gr
            // 
            this.gr.DataPropertyName = "gr";
            this.gr.HeaderText = "感染";
            this.gr.Name = "gr";
            this.gr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.gr.Width = 43;
            // 
            // bx
            // 
            this.bx.DataPropertyName = "bx";
            this.bx.HeaderText = "备血";
            this.bx.Name = "bx";
            this.bx.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.bx.Width = 43;
            // 
            // remarks
            // 
            this.remarks.DataPropertyName = "remarks";
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.remarks.Width = 43;
            // 
            // on1
            // 
            this.on1.DataPropertyName = "on1";
            this.on1.HeaderText = "洗手护士";
            this.on1.Name = "on1";
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
            this.on2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.on2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.on2.Width = 79;
            // 
            // sn1
            // 
            this.sn1.DataPropertyName = "sn1";
            this.sn1.HeaderText = "巡回护士";
            this.sn1.Name = "sn1";
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
            this.sn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sn2.Width = 79;
            // 
            // ap1
            // 
            this.ap1.DataPropertyName = "ap1";
            this.ap1.HeaderText = "主麻医师";
            this.ap1.Name = "ap1";
            this.ap1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ap1.Width = 71;
            // 
            // ap2
            // 
            this.ap2.DataPropertyName = "ap2";
            this.ap2.HeaderText = "副麻医师1";
            this.ap2.Name = "ap2";
            this.ap2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ap2.Width = 79;
            // 
            // ap3
            // 
            this.ap3.DataPropertyName = "ap3";
            this.ap3.HeaderText = "副麻医师2";
            this.ap3.Name = "ap3";
            this.ap3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ap3.Width = 79;
            // 
            // shengfen
            // 
            this.shengfen.DataPropertyName = "shengfen";
            this.shengfen.HeaderText = "身份";
            this.shengfen.Name = "shengfen";
            this.shengfen.Visible = false;
            this.shengfen.Width = 62;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Visible = false;
            this.id.Width = 49;
            // 
            // patid
            // 
            this.patid.DataPropertyName = "patid";
            this.patid.HeaderText = "patid";
            this.patid.Name = "patid";
            this.patid.Visible = false;
            this.patid.Width = 69;
            // 
            // PaibanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 721);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "PaibanForm";
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
        private System.Windows.Forms.Label lbPatZhuYuanID;
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
        private System.Windows.Forms.Label lbSSmzfs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbAmethod;
        private System.Windows.Forms.ToolStripMenuItem tsmiJZ;
        private System.Windows.Forms.ToolStripMenuItem tsmiZQ;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn oroom;
        private System.Windows.Forms.DataGridViewTextBoxColumn second;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatZhuYuanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn patdpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn patname;
        private System.Windows.Forms.DataGridViewTextBoxColumn patsex;
        private System.Windows.Forms.DataGridViewTextBoxColumn patage;
        private System.Windows.Forms.DataGridViewTextBoxColumn bedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn patNation;
        private System.Windows.Forms.DataGridViewTextBoxColumn pattmd;
        private System.Windows.Forms.DataGridViewTextBoxColumn oname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSmzfs;
        private System.Windows.Forms.DataGridViewTextBoxColumn os;
        private System.Windows.Forms.DataGridViewTextBoxColumn os1;
        private System.Windows.Forms.DataGridViewTextBoxColumn os2;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiwei;
        private System.Windows.Forms.DataGridViewTextBoxColumn gr;
        private System.Windows.Forms.DataGridViewTextBoxColumn bx;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn on1;
        private System.Windows.Forms.DataGridViewTextBoxColumn on2;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ap3;
        private System.Windows.Forms.DataGridViewTextBoxColumn shengfen;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn patid;

    }
}