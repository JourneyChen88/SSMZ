namespace main
{
    partial class addYZZT
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
            this.dgvYizhu = new System.Windows.Forms.DataGridView();
            this.xuhao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mzid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.klDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.klTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yizhu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yisheng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hushi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zxDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zxtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zxyzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShua = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbYZbao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYizhu)).BeginInit();
            this.ctMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvYizhu
            // 
            this.dgvYizhu.AllowUserToAddRows = false;
            this.dgvYizhu.AllowUserToResizeColumns = false;
            this.dgvYizhu.AllowUserToResizeRows = false;
            this.dgvYizhu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvYizhu.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvYizhu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYizhu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xuhao,
            this.mzid,
            this.klDate,
            this.klTime,
            this.yizhu,
            this.yisheng,
            this.hushi,
            this.zxDate,
            this.zxtime,
            this.remark});
            this.dgvYizhu.ContextMenuStrip = this.ctMenu;
            this.dgvYizhu.Location = new System.Drawing.Point(2, 103);
            this.dgvYizhu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvYizhu.Name = "dgvYizhu";
            this.dgvYizhu.RowHeadersWidth = 25;
            this.dgvYizhu.RowTemplate.Height = 23;
            this.dgvYizhu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvYizhu.Size = new System.Drawing.Size(1005, 477);
            this.dgvYizhu.TabIndex = 41;
            this.dgvYizhu.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvYizhu_CellDoubleClick);
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
            this.klDate.HeaderText = "日期";
            this.klDate.Name = "klDate";
            this.klDate.Width = 60;
            // 
            // klTime
            // 
            this.klTime.DataPropertyName = "klTime";
            this.klTime.HeaderText = "时间";
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
            // hushi
            // 
            this.hushi.DataPropertyName = "hushi";
            this.hushi.HeaderText = "护士签字";
            this.hushi.Name = "hushi";
            this.hushi.Width = 90;
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
            this.remark.HeaderText = "备注";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShua);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.cmbYZbao);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(956, 73);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "医嘱包的维护";
            // 
            // btnShua
            // 
            this.btnShua.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShua.ForeColor = System.Drawing.Color.Blue;
            this.btnShua.Location = new System.Drawing.Point(239, 27);
            this.btnShua.Name = "btnShua";
            this.btnShua.Size = new System.Drawing.Size(75, 35);
            this.btnShua.TabIndex = 16;
            this.btnShua.Text = "刷新";
            this.btnShua.UseVisualStyleBackColor = true;
            this.btnShua.Click += new System.EventHandler(this.btnShua_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(338, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 35);
            this.button2.TabIndex = 15;
            this.button2.Text = "基础数据维护";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbYZbao
            // 
            this.cmbYZbao.FormattingEnabled = true;
            this.cmbYZbao.Location = new System.Drawing.Point(85, 32);
            this.cmbYZbao.Name = "cmbYZbao";
            this.cmbYZbao.Size = new System.Drawing.Size(121, 20);
            this.cmbYZbao.TabIndex = 1;
            this.cmbYZbao.SelectedIndexChanged += new System.EventHandler(this.cmbYZbao_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "医嘱包";
            // 
            // addYZZT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 582);
            this.Controls.Add(this.dgvYizhu);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "addYZZT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "医嘱包管理";
            this.Load += new System.EventHandler(this.addYZZT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvYizhu)).EndInit();
            this.ctMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvYizhu;
        private System.Windows.Forms.DataGridViewTextBoxColumn xuhao;
        private System.Windows.Forms.DataGridViewTextBoxColumn mzid;
        private System.Windows.Forms.DataGridViewTextBoxColumn klDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn klTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn yizhu;
        private System.Windows.Forms.DataGridViewTextBoxColumn yisheng;
        private System.Windows.Forms.DataGridViewTextBoxColumn hushi;
        private System.Windows.Forms.DataGridViewTextBoxColumn zxDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn zxtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbYZbao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip ctMenu;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zxyzToolStripMenuItem;
        private System.Windows.Forms.Button btnShua;
    }
}