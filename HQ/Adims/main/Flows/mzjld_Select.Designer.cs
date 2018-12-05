namespace main
{
    partial class Mzjld_Select
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbOroomName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbMzs = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteMZJLDmenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rdNew = new System.Windows.Forms.RadioButton();
            this.rdOld = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtDATE = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前手术间：";
            // 
            // lbOroomName
            // 
            this.lbOroomName.AutoSize = true;
            this.lbOroomName.Location = new System.Drawing.Point(113, 270);
            this.lbOroomName.Name = "lbOroomName";
            this.lbOroomName.Size = new System.Drawing.Size(23, 12);
            this.lbOroomName.TabIndex = 1;
            this.lbOroomName.Text = "111";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "当前麻醉师：";
            // 
            // lbMzs
            // 
            this.lbMzs.AutoSize = true;
            this.lbMzs.Location = new System.Drawing.Point(260, 270);
            this.lbMzs.Name = "lbMzs";
            this.lbMzs.Size = new System.Drawing.Size(29, 12);
            this.lbMzs.TabIndex = 3;
            this.lbMzs.Text = "张三";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataGridView1.Location = new System.Drawing.Point(12, 56);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(516, 195);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteMZJLDmenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 26);
            // 
            // DeleteMZJLDmenu
            // 
            this.DeleteMZJLDmenu.Name = "DeleteMZJLDmenu";
            this.DeleteMZJLDmenu.Size = new System.Drawing.Size(172, 22);
            this.DeleteMZJLDmenu.Text = "删除旧麻醉记录单";
            this.DeleteMZJLDmenu.Click += new System.EventHandler(this.DeleteMZJLDmenu_Click);
            // 
            // rdNew
            // 
            this.rdNew.AutoSize = true;
            this.rdNew.Checked = true;
            this.rdNew.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdNew.Location = new System.Drawing.Point(58, 17);
            this.rdNew.Name = "rdNew";
            this.rdNew.Size = new System.Drawing.Size(97, 24);
            this.rdNew.TabIndex = 5;
            this.rdNew.TabStop = true;
            this.rdNew.Text = "新建麻醉单";
            this.rdNew.UseVisualStyleBackColor = true;
            this.rdNew.CheckedChanged += new System.EventHandler(this.rdNew_CheckedChanged);
            // 
            // rdOld
            // 
            this.rdOld.AutoSize = true;
            this.rdOld.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdOld.Location = new System.Drawing.Point(162, 17);
            this.rdOld.Name = "rdOld";
            this.rdOld.Size = new System.Drawing.Size(111, 24);
            this.rdOld.TabIndex = 6;
            this.rdOld.TabStop = true;
            this.rdOld.Text = "进入旧麻醉单";
            this.rdOld.UseVisualStyleBackColor = true;
            this.rdOld.CheckedChanged += new System.EventHandler(this.rdOld_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.Blue;
            this.btnOK.Location = new System.Drawing.Point(334, 261);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 35);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.Color.Blue;
            this.btnClose.Location = new System.Drawing.Point(441, 261);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 35);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "选择手术日期：";
            // 
            // dtDATE
            // 
            this.dtDATE.Location = new System.Drawing.Point(383, 20);
            this.dtDATE.Name = "dtDATE";
            this.dtDATE.Size = new System.Drawing.Size(124, 21);
            this.dtDATE.TabIndex = 10;
            this.dtDATE.ValueChanged += new System.EventHandler(this.dtDATE_ValueChanged);
            // 
            // mzjld_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 322);
            this.Controls.Add(this.dtDATE);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rdOld);
            this.Controls.Add(this.rdNew);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbMzs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbOroomName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mzjld_Select";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择病人";
            this.Load += new System.EventHandler(this.Smzjld_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbOroomName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbMzs;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton rdNew;
        private System.Windows.Forms.RadioButton rdOld;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtDATE;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DeleteMZJLDmenu;
    }
}