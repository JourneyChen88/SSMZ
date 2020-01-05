namespace main
{
    partial class LsyzForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LsyzForm));
            this.label53 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnQUE = new System.Windows.Forms.Button();
            this.cmbYZbao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbZhuyuanID = new System.Windows.Forms.TextBox();
            this.cmbSex = new System.Windows.Forms.ComboBox();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.tbPatName = new System.Windows.Forms.TextBox();
            this.tbBedNo = new System.Windows.Forms.TextBox();
            this.tbKeshi = new System.Windows.Forms.TextBox();
            this.dgvYizhu = new System.Windows.Forms.DataGridView();
            this.CoDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.xuhao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.klDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.klTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yizhu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yisheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zxDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zxtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zxyzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnPrint = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYizhu)).BeginInit();
            this.ctMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(252, 27);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(44, 17);
            this.label53.TabIndex = 51;
            this.label53.Text = "床号：";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(28, 27);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(44, 17);
            this.label52.TabIndex = 49;
            this.label52.Text = "科别：";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(388, 69);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(44, 17);
            this.label51.TabIndex = 47;
            this.label51.Text = "年龄：";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(252, 72);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(44, 17);
            this.label50.TabIndex = 45;
            this.label50.Text = "性别：";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(390, 30);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(44, 17);
            this.label49.TabIndex = 43;
            this.label49.Text = "姓名：";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(7, 70);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(56, 17);
            this.label48.TabIndex = 41;
            this.label48.Text = "住院号：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tbZhuyuanID);
            this.groupBox1.Controls.Add(this.cmbSex);
            this.groupBox1.Controls.Add(this.tbAge);
            this.groupBox1.Controls.Add(this.tbPatName);
            this.groupBox1.Controls.Add(this.tbBedNo);
            this.groupBox1.Controls.Add(this.tbKeshi);
            this.groupBox1.Controls.Add(this.label48);
            this.groupBox1.Controls.Add(this.label49);
            this.groupBox1.Controls.Add(this.label50);
            this.groupBox1.Controls.Add(this.label51);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Controls.Add(this.label53);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1027, 106);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnQUE);
            this.groupBox2.Controls.Add(this.cmbYZbao);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(510, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 85);
            this.groupBox2.TabIndex = 636;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "使用医嘱包";
            this.groupBox2.Visible = false;
            // 
            // btnQUE
            // 
            this.btnQUE.Location = new System.Drawing.Point(238, 22);
            this.btnQUE.Name = "btnQUE";
            this.btnQUE.Size = new System.Drawing.Size(75, 38);
            this.btnQUE.TabIndex = 2;
            this.btnQUE.Text = "确定";
            this.btnQUE.UseVisualStyleBackColor = true;
            this.btnQUE.Visible = false;
            this.btnQUE.Click += new System.EventHandler(this.btnQUE_Click);
            // 
            // cmbYZbao
            // 
            this.cmbYZbao.FormattingEnabled = true;
            this.cmbYZbao.Location = new System.Drawing.Point(91, 30);
            this.cmbYZbao.Name = "cmbYZbao";
            this.cmbYZbao.Size = new System.Drawing.Size(121, 25);
            this.cmbYZbao.TabIndex = 1;
            this.cmbYZbao.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择医嘱包";
            this.label1.Visible = false;
            // 
            // tbZhuyuanID
            // 
            this.tbZhuyuanID.Location = new System.Drawing.Point(69, 66);
            this.tbZhuyuanID.Name = "tbZhuyuanID";
            this.tbZhuyuanID.ReadOnly = true;
            this.tbZhuyuanID.Size = new System.Drawing.Size(155, 23);
            this.tbZhuyuanID.TabIndex = 58;
            // 
            // cmbSex
            // 
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.Items.AddRange(new object[] {
            "男",
            "女"});
            this.cmbSex.Location = new System.Drawing.Point(302, 67);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.Size = new System.Drawing.Size(50, 25);
            this.cmbSex.TabIndex = 57;
            // 
            // tbAge
            // 
            this.tbAge.Location = new System.Drawing.Point(432, 66);
            this.tbAge.Name = "tbAge";
            this.tbAge.ReadOnly = true;
            this.tbAge.Size = new System.Drawing.Size(50, 23);
            this.tbAge.TabIndex = 56;
            // 
            // tbPatName
            // 
            this.tbPatName.Location = new System.Drawing.Point(432, 24);
            this.tbPatName.Name = "tbPatName";
            this.tbPatName.ReadOnly = true;
            this.tbPatName.Size = new System.Drawing.Size(50, 23);
            this.tbPatName.TabIndex = 55;
            // 
            // tbBedNo
            // 
            this.tbBedNo.Location = new System.Drawing.Point(302, 24);
            this.tbBedNo.Name = "tbBedNo";
            this.tbBedNo.ReadOnly = true;
            this.tbBedNo.Size = new System.Drawing.Size(50, 23);
            this.tbBedNo.TabIndex = 53;
            // 
            // tbKeshi
            // 
            this.tbKeshi.Location = new System.Drawing.Point(69, 27);
            this.tbKeshi.Name = "tbKeshi";
            this.tbKeshi.ReadOnly = true;
            this.tbKeshi.Size = new System.Drawing.Size(155, 23);
            this.tbKeshi.TabIndex = 52;
            // 
            // dgvYizhu
            // 
            this.dgvYizhu.AllowUserToAddRows = false;
            this.dgvYizhu.AllowUserToResizeColumns = false;
            this.dgvYizhu.AllowUserToResizeRows = false;
            this.dgvYizhu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvYizhu.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvYizhu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYizhu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CoDelete,
            this.xuhao,
            this.mzid,
            this.klDate,
            this.klTime,
            this.yizhu,
            this.yisheng,
            this.zxDate,
            this.zxtime,
            this.remark});
            this.dgvYizhu.ContextMenuStrip = this.ctMenu;
            this.dgvYizhu.Location = new System.Drawing.Point(0, 114);
            this.dgvYizhu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvYizhu.Name = "dgvYizhu";
            this.dgvYizhu.RowHeadersVisible = false;
            this.dgvYizhu.RowHeadersWidth = 25;
            this.dgvYizhu.RowTemplate.Height = 23;
            this.dgvYizhu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvYizhu.Size = new System.Drawing.Size(1027, 495);
            this.dgvYizhu.TabIndex = 40;
            this.dgvYizhu.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvYizhu_CellDoubleClick);
            // 
            // CoDelete
            // 
            this.CoDelete.HeaderText = "是否删除";
            this.CoDelete.Name = "CoDelete";
            this.CoDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoDelete.Width = 80;
            // 
            // xuhao
            // 
            this.xuhao.DataPropertyName = "id";
            this.xuhao.HeaderText = "序号";
            this.xuhao.Name = "xuhao";
            this.xuhao.Visible = false;
            // 
            // mzid
            // 
            this.mzid.DataPropertyName = "mzjldid";
            this.mzid.HeaderText = "麻醉编号";
            this.mzid.Name = "mzid";
            this.mzid.Visible = false;
            // 
            // klDate
            // 
            this.klDate.DataPropertyName = "klDate";
            this.klDate.HeaderText = "开立日期";
            this.klDate.Name = "klDate";
            this.klDate.Width = 60;
            // 
            // klTime
            // 
            this.klTime.DataPropertyName = "klTime";
            this.klTime.HeaderText = "开立时间";
            this.klTime.Name = "klTime";
            this.klTime.Width = 60;
            // 
            // yizhu
            // 
            this.yizhu.DataPropertyName = "yizhu";
            this.yizhu.HeaderText = "医嘱";
            this.yizhu.Name = "yizhu";
            this.yizhu.Width = 480;
            // 
            // yisheng
            // 
            this.yisheng.DataPropertyName = "yisheng";
            this.yisheng.HeaderText = "医生签字";
            this.yisheng.Name = "yisheng";
            this.yisheng.Width = 90;
            // 
            // zxDate
            // 
            this.zxDate.DataPropertyName = "zxDate";
            this.zxDate.HeaderText = "执行日期";
            this.zxDate.Name = "zxDate";
            this.zxDate.Width = 60;
            // 
            // zxtime
            // 
            this.zxtime.DataPropertyName = "zxTime";
            this.zxtime.HeaderText = "执行时间";
            this.zxtime.Name = "zxtime";
            this.zxtime.Width = 60;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "执行者签名";
            this.remark.Name = "remark";
            // 
            // ctMenu
            // 
            this.ctMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.zxyzToolStripMenuItem});
            this.ctMenu.Name = "ctMenu";
            this.ctMenu.Size = new System.Drawing.Size(185, 70);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.addToolStripMenuItem.Text = "添加一行";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.deleteToolStripMenuItem.Text = "删除此行";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // zxyzToolStripMenuItem
            // 
            this.zxyzToolStripMenuItem.Name = "zxyzToolStripMenuItem";
            this.zxyzToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.zxyzToolStripMenuItem.Text = "录入执行日期和时间";
            this.zxyzToolStripMenuItem.Click += new System.EventHandler(this.zxyzToolStripMenuItem_Click);
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
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.ForeColor = System.Drawing.Color.Blue;
            this.btnPrint.Image = global::main.Properties.Resources.Print;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(603, 638);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 40);
            this.btnPrint.TabIndex = 636;
            this.btnPrint.Text = "打印";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click_1);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(476, 638);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 40);
            this.button3.TabIndex = 637;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = global::main.Properties.Resources.Save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(349, 639);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 40);
            this.btnSave.TabIndex = 638;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // LsyzForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 712);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvYizhu);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LsyzForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "临时医嘱单";
            this.Load += new System.EventHandler(this.LsyzForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYizhu)).EndInit();
            this.ctMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvYizhu;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.TextBox tbKeshi;
        private System.Windows.Forms.TextBox tbAge;
        private System.Windows.Forms.TextBox tbPatName;
        private System.Windows.Forms.TextBox tbBedNo;
        private System.Windows.Forms.ComboBox cmbSex;
        private System.Windows.Forms.TextBox tbZhuyuanID;
        private System.Windows.Forms.ContextMenuStrip ctMenu;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zxyzToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbYZbao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQUE;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CoDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn xuhao;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzid;
        private System.Windows.Forms.DataGridViewTextBoxColumn klDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn klTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn yizhu;
        private System.Windows.Forms.DataGridViewTextBoxColumn yisheng;
        private System.Windows.Forms.DataGridViewTextBoxColumn zxDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn zxtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;


    }
}