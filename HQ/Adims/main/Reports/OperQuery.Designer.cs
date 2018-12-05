namespace main
{
    partial class SSTSTJ
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
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("麻醉医生");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("手术间");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("科室");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("麻醉等级");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("麻醉方式");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("麻醉效果");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("护士");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("手术级别");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("风险评估");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("手术医生");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("身份");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("手术类别");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SSTSTJ));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEXCEL = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnPrintResult = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.row_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Oroom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patdpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatZhuYuanID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patsex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jizhen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rssj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kssj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jssj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Szzd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MazuiFS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ssys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qxhs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Xhhs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ssjb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fxpg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shengfen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMZJLD = new System.Windows.Forms.ToolStripMenuItem();
            this.进入护士术前访视记录单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.进入医师术前访视记录单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.进入医师术后访视ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.进入麻醉总结ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(451, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "按麻醉医师统计";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(595, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "按科室统计";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Blue;
            this.button3.Location = new System.Drawing.Point(451, 50);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 30);
            this.button3.TabIndex = 4;
            this.button3.Text = "按手术间统计";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dateTimePicker2.Location = new System.Drawing.Point(186, 42);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(119, 21);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "至";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(24, 39);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(128, 21);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(21, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "选择统计时间段:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnEXCEL);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.btnPrintResult);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1109, 90);
            this.panel1.TabIndex = 16;
            // 
            // btnEXCEL
            // 
            this.btnEXCEL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEXCEL.ForeColor = System.Drawing.Color.Blue;
            this.btnEXCEL.Image = global::main.Properties.Resources.Excel1;
            this.btnEXCEL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEXCEL.Location = new System.Drawing.Point(973, 27);
            this.btnEXCEL.Name = "btnEXCEL";
            this.btnEXCEL.Size = new System.Drawing.Size(104, 40);
            this.btnEXCEL.TabIndex = 18;
            this.btnEXCEL.Text = "导出Excel";
            this.btnEXCEL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEXCEL.UseVisualStyleBackColor = true;
            this.btnEXCEL.Click += new System.EventHandler(this.btnEXCEL_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.Color.Blue;
            this.button6.Location = new System.Drawing.Point(718, 14);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(101, 30);
            this.button6.TabIndex = 647;
            this.button6.Text = "按手术医生查询";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.Blue;
            this.button5.Location = new System.Drawing.Point(595, 50);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(102, 30);
            this.button5.TabIndex = 646;
            this.button5.Text = "按护士统计";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnPrintResult
            // 
            this.btnPrintResult.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnPrintResult.ForeColor = System.Drawing.Color.Blue;
            this.btnPrintResult.Image = global::main.Properties.Resources.Print;
            this.btnPrintResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintResult.Location = new System.Drawing.Point(864, 25);
            this.btnPrintResult.Name = "btnPrintResult";
            this.btnPrintResult.Size = new System.Drawing.Size(92, 45);
            this.btnPrintResult.TabIndex = 645;
            this.btnPrintResult.Text = "打印统计";
            this.btnPrintResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintResult.UseVisualStyleBackColor = true;
            this.btnPrintResult.Click += new System.EventHandler(this.btnPrintResult_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.Blue;
            this.button4.Location = new System.Drawing.Point(319, 27);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 48);
            this.button4.TabIndex = 18;
            this.button4.Text = "查询";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView.BackColor = System.Drawing.SystemColors.Control;
            this.treeView.Font = new System.Drawing.Font("宋体", 11F);
            this.treeView.Location = new System.Drawing.Point(0, 96);
            this.treeView.Name = "treeView";
            treeNode13.Name = "Adims_User";
            treeNode13.Text = "麻醉医生";
            treeNode14.Name = "ssjstate";
            treeNode14.Text = "手术间";
            treeNode15.Name = "keshi";
            treeNode15.Text = "科室";
            treeNode16.Name = "Olevel";
            treeNode16.Text = "麻醉等级";
            treeNode17.Name = "amethod";
            treeNode17.Text = "麻醉方式";
            treeNode18.Name = "mzxg";
            treeNode18.Text = "麻醉效果";
            treeNode19.Name = "Adims_User";
            treeNode19.Text = "护士";
            treeNode20.Name = "SSJB";
            treeNode20.Text = "手术级别";
            treeNode21.Name = "FXPG";
            treeNode21.Text = "风险评估";
            treeNode22.Name = "SSYS";
            treeNode22.Text = "手术医生";
            treeNode23.Name = "SF";
            treeNode23.Text = "身份";
            treeNode24.Name = "SSLB";
            treeNode24.Text = "手术类别";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24});
            this.treeView.Size = new System.Drawing.Size(165, 396);
            this.treeView.TabIndex = 13;
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.row_id,
            this.mzid,
            this.Column2,
            this.Oroom,
            this.patdpm,
            this.PatZhuYuanID,
            this.PatName,
            this.patsex,
            this.patage,
            this.jizhen,
            this.rssj,
            this.kssj,
            this.jssj,
            this.Szzd,
            this.oname,
            this.Amethod,
            this.MazuiFS,
            this.asa,
            this.Ssys,
            this.mzys,
            this.qxhs,
            this.Xhhs,
            this.Ssjb,
            this.Fxpg,
            this.shengfen,
            this.Remarks});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(171, 96);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 40;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(919, 396);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView1_RowStateChanged);
            // 
            // row_id
            // 
            this.row_id.DataPropertyName = "row_id";
            this.row_id.HeaderText = "序号";
            this.row_id.Name = "row_id";
            this.row_id.ReadOnly = true;
            this.row_id.Width = 54;
            // 
            // mzid
            // 
            this.mzid.DataPropertyName = "mzid";
            this.mzid.HeaderText = "麻醉编号";
            this.mzid.Name = "mzid";
            this.mzid.ReadOnly = true;
            this.mzid.Visible = false;
            this.mzid.Width = 78;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "otime";
            this.Column2.HeaderText = "手术日期";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // Oroom
            // 
            this.Oroom.DataPropertyName = "Oroom";
            this.Oroom.HeaderText = "手术间";
            this.Oroom.Name = "Oroom";
            this.Oroom.ReadOnly = true;
            this.Oroom.Width = 66;
            // 
            // patdpm
            // 
            this.patdpm.DataPropertyName = "patdpm";
            this.patdpm.HeaderText = "科室";
            this.patdpm.Name = "patdpm";
            this.patdpm.ReadOnly = true;
            this.patdpm.Width = 54;
            // 
            // PatZhuYuanID
            // 
            this.PatZhuYuanID.DataPropertyName = "PatZhuYuanID";
            this.PatZhuYuanID.HeaderText = "住院号";
            this.PatZhuYuanID.Name = "PatZhuYuanID";
            this.PatZhuYuanID.ReadOnly = true;
            this.PatZhuYuanID.Width = 66;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "病人姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.Width = 78;
            // 
            // patsex
            // 
            this.patsex.DataPropertyName = "patsex";
            this.patsex.HeaderText = "性别";
            this.patsex.Name = "patsex";
            this.patsex.ReadOnly = true;
            this.patsex.Width = 54;
            // 
            // patage
            // 
            this.patage.DataPropertyName = "patage";
            this.patage.HeaderText = "年龄";
            this.patage.Name = "patage";
            this.patage.ReadOnly = true;
            this.patage.Width = 54;
            // 
            // jizhen
            // 
            this.jizhen.DataPropertyName = "jizhen";
            this.jizhen.HeaderText = "择期/急诊";
            this.jizhen.Name = "jizhen";
            this.jizhen.ReadOnly = true;
            this.jizhen.Width = 84;
            // 
            // rssj
            // 
            this.rssj.DataPropertyName = "rssj";
            this.rssj.HeaderText = "入室时间";
            this.rssj.Name = "rssj";
            this.rssj.ReadOnly = true;
            this.rssj.Width = 78;
            // 
            // kssj
            // 
            this.kssj.DataPropertyName = "kssj";
            this.kssj.HeaderText = "手术开始";
            this.kssj.Name = "kssj";
            this.kssj.ReadOnly = true;
            this.kssj.Width = 78;
            // 
            // jssj
            // 
            this.jssj.DataPropertyName = "jssj";
            this.jssj.HeaderText = "出室时间";
            this.jssj.Name = "jssj";
            this.jssj.ReadOnly = true;
            this.jssj.Width = 78;
            // 
            // Szzd
            // 
            this.Szzd.DataPropertyName = "Szzd";
            this.Szzd.HeaderText = "术中诊断";
            this.Szzd.Name = "Szzd";
            this.Szzd.ReadOnly = true;
            this.Szzd.Width = 78;
            // 
            // oname
            // 
            this.oname.DataPropertyName = "oname";
            this.oname.HeaderText = "手术名称";
            this.oname.Name = "oname";
            this.oname.ReadOnly = true;
            this.oname.Width = 78;
            // 
            // Amethod
            // 
            this.Amethod.DataPropertyName = "Amethod";
            this.Amethod.HeaderText = "拟行麻醉";
            this.Amethod.Name = "Amethod";
            this.Amethod.ReadOnly = true;
            this.Amethod.Width = 78;
            // 
            // MazuiFS
            // 
            this.MazuiFS.DataPropertyName = "MazuiFS";
            this.MazuiFS.HeaderText = "实施麻醉";
            this.MazuiFS.Name = "MazuiFS";
            this.MazuiFS.ReadOnly = true;
            this.MazuiFS.Width = 78;
            // 
            // asa
            // 
            this.asa.DataPropertyName = "asa";
            this.asa.HeaderText = "麻醉等级";
            this.asa.Name = "asa";
            this.asa.ReadOnly = true;
            this.asa.Width = 78;
            // 
            // Ssys
            // 
            this.Ssys.DataPropertyName = "Ssys";
            this.Ssys.HeaderText = "手术医生";
            this.Ssys.Name = "Ssys";
            this.Ssys.ReadOnly = true;
            this.Ssys.Width = 78;
            // 
            // mzys
            // 
            this.mzys.DataPropertyName = "mzys";
            this.mzys.HeaderText = "麻醉医生";
            this.mzys.Name = "mzys";
            this.mzys.ReadOnly = true;
            this.mzys.Width = 78;
            // 
            // qxhs
            // 
            this.qxhs.DataPropertyName = "qxhs";
            this.qxhs.HeaderText = "器械护士";
            this.qxhs.Name = "qxhs";
            this.qxhs.ReadOnly = true;
            this.qxhs.Width = 78;
            // 
            // Xhhs
            // 
            this.Xhhs.DataPropertyName = "Xhhs";
            this.Xhhs.HeaderText = "巡回护士";
            this.Xhhs.Name = "Xhhs";
            this.Xhhs.ReadOnly = true;
            this.Xhhs.Width = 78;
            // 
            // Ssjb
            // 
            this.Ssjb.DataPropertyName = "Ssjb";
            this.Ssjb.HeaderText = "手术类别";
            this.Ssjb.Name = "Ssjb";
            this.Ssjb.ReadOnly = true;
            this.Ssjb.Width = 78;
            // 
            // Fxpg
            // 
            this.Fxpg.DataPropertyName = "Fxpg";
            this.Fxpg.HeaderText = "风险评估";
            this.Fxpg.Name = "Fxpg";
            this.Fxpg.ReadOnly = true;
            this.Fxpg.Width = 78;
            // 
            // shengfen
            // 
            this.shengfen.DataPropertyName = "shengfen";
            this.shengfen.HeaderText = "身份";
            this.shengfen.Name = "shengfen";
            this.shengfen.ReadOnly = true;
            this.shengfen.Width = 54;
            // 
            // Remarks
            // 
            this.Remarks.DataPropertyName = "Remarks";
            this.Remarks.HeaderText = "备注";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            this.Remarks.Width = 54;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMZJLD,
            this.进入护士术前访视记录单ToolStripMenuItem,
            this.进入医师术前访视记录单ToolStripMenuItem,
            this.进入医师术后访视ToolStripMenuItem,
            this.进入麻醉总结ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 114);
            // 
            // tsmiMZJLD
            // 
            this.tsmiMZJLD.Name = "tsmiMZJLD";
            this.tsmiMZJLD.Size = new System.Drawing.Size(172, 22);
            this.tsmiMZJLD.Text = "进入麻醉记录单";
            this.tsmiMZJLD.Click += new System.EventHandler(this.tsmiMZJLD_Click);
            // 
            // 进入护士术前访视记录单ToolStripMenuItem
            // 
            this.进入护士术前访视记录单ToolStripMenuItem.Name = "进入护士术前访视记录单ToolStripMenuItem";
            this.进入护士术前访视记录单ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.进入护士术前访视记录单ToolStripMenuItem.Text = "进入护士术前访视";
            this.进入护士术前访视记录单ToolStripMenuItem.Click += new System.EventHandler(this.进入护士术前访视记录单ToolStripMenuItem_Click);
            // 
            // 进入医师术前访视记录单ToolStripMenuItem
            // 
            this.进入医师术前访视记录单ToolStripMenuItem.Name = "进入医师术前访视记录单ToolStripMenuItem";
            this.进入医师术前访视记录单ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.进入医师术前访视记录单ToolStripMenuItem.Text = "进入医师术前访视";
            this.进入医师术前访视记录单ToolStripMenuItem.Click += new System.EventHandler(this.进入医师术前访视记录单ToolStripMenuItem_Click);
            // 
            // 进入医师术后访视ToolStripMenuItem
            // 
            this.进入医师术后访视ToolStripMenuItem.Name = "进入医师术后访视ToolStripMenuItem";
            this.进入医师术后访视ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.进入医师术后访视ToolStripMenuItem.Text = "进入医师术后访视";
            this.进入医师术后访视ToolStripMenuItem.Click += new System.EventHandler(this.进入医师术后访视ToolStripMenuItem_Click);
            // 
            // 进入麻醉总结ToolStripMenuItem
            // 
            this.进入麻醉总结ToolStripMenuItem.Name = "进入麻醉总结ToolStripMenuItem";
            this.进入麻醉总结ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.进入麻醉总结ToolStripMenuItem.Text = "进入麻醉总结";
            this.进入麻醉总结ToolStripMenuItem.Click += new System.EventHandler(this.进入麻醉总结ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 495);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1109, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "当前手术总量：";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(240, 17);
            this.toolStripStatusLabel1.Text = "                                     当前手术总量：";
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
            // SSTSTJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 517);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "SSTSTJ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手术统计";
            this.Load += new System.EventHandler(this.SSTSTJ_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button btnPrintResult;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnEXCEL;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMZJLD;
        private System.Windows.Forms.DataGridViewTextBoxColumn row_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Oroom;
        private System.Windows.Forms.DataGridViewTextBoxColumn patdpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatZhuYuanID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn patsex;
        private System.Windows.Forms.DataGridViewTextBoxColumn patage;
        private System.Windows.Forms.DataGridViewTextBoxColumn jizhen;
        private System.Windows.Forms.DataGridViewTextBoxColumn rssj;
        private System.Windows.Forms.DataGridViewTextBoxColumn kssj;
        private System.Windows.Forms.DataGridViewTextBoxColumn jssj;
        private System.Windows.Forms.DataGridViewTextBoxColumn Szzd;
        private System.Windows.Forms.DataGridViewTextBoxColumn oname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn MazuiFS;
        private System.Windows.Forms.DataGridViewTextBoxColumn asa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ssys;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzys;
        private System.Windows.Forms.DataGridViewTextBoxColumn qxhs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Xhhs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ssjb;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fxpg;
        private System.Windows.Forms.DataGridViewTextBoxColumn shengfen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.ToolStripMenuItem 进入护士术前访视记录单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 进入医师术前访视记录单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 进入医师术后访视ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 进入麻醉总结ToolStripMenuItem;
    }
}