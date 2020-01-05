namespace main.科室事物管理
{
    partial class DateSetup
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("手术体位");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("麻醉方法");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("器械名称");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("科  室");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("麻醉平面");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("术中事件");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("ASA分级");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("合并症");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("监护项目");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("手术间");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("药品类型");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("切口类型");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("用量单位");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("麻醉用药剂费组套");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("医嘱管理");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("医嘱包");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("用药方式");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("效果评价");
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbText = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "tiwei";
            treeNode1.Text = "手术体位";
            treeNode2.Name = "Mazuifangfa";
            treeNode2.Text = "麻醉方法";
            treeNode3.Name = "QiXie";
            treeNode3.Text = "器械名称";
            treeNode4.Name = "keshi";
            treeNode4.Text = "科  室";
            treeNode5.Name = "MazuiPingmian";
            treeNode5.Text = "麻醉平面";
            treeNode6.Name = "ShuzhongShijian";
            treeNode6.Text = "术中事件";
            treeNode7.Name = "ASA";
            treeNode7.Text = "ASA分级";
            treeNode8.Name = "HeBingZheng";
            treeNode8.Text = "合并症";
            treeNode9.Name = "JianhuXiangmu";
            treeNode9.Text = "监护项目";
            treeNode10.Name = "ssjstate";
            treeNode10.Text = "手术间";
            treeNode11.Name = "YaopinType";
            treeNode11.Text = "药品类型";
            treeNode12.Name = "QiekouType";
            treeNode12.Text = "切口类型";
            treeNode13.Name = "YLDW";
            treeNode13.Text = "用量单位";
            treeNode14.Name = "Adims_MZYYZTType";
            treeNode14.Text = "麻醉用药剂费组套";
            treeNode15.Name = "Adims_YZtype";
            treeNode15.Text = "医嘱管理";
            treeNode16.Name = "Adims_YZBao";
            treeNode16.Text = "医嘱包";
            treeNode17.Name = "YYFS";
            treeNode17.Text = "用药方式";
            treeNode18.Name = "pingjia";
            treeNode18.Text = "效果评价";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18});
            this.treeView1.Size = new System.Drawing.Size(161, 474);
            this.treeView1.TabIndex = 1;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.lbText);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(158, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 473);
            this.panel1.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.ForeColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(388, 19);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 35);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.ForeColor = System.Drawing.Color.Blue;
            this.btnAdd.Location = new System.Drawing.Point(294, 19);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 35);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(117, 24);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(153, 23);
            this.tbName.TabIndex = 11;
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Location = new System.Drawing.Point(20, 27);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(32, 17);
            this.lbText.TabIndex = 10;
            this.lbText.Text = "名称";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvTable);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 408);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "列表";
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTable.Location = new System.Drawing.Point(3, 19);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.RowTemplate.Height = 23;
            this.dgvTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTable.Size = new System.Drawing.Size(472, 386);
            this.dgvTable.TabIndex = 1;
            // 
            // DateSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 474);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DateSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基础数据维护";
            this.Load += new System.EventHandler(this.data_maintenance_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.TextBox tbName;
        public System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
    }
}