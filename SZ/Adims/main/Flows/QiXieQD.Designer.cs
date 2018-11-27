namespace main
{
    partial class QiXieQD
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
            this.dgvQXGL = new System.Windows.Forms.DataGridView();
            this.id1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.model1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qxmc1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qxcount1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除器械ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSTJSHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbQXGL = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnC = new System.Windows.Forms.Button();
            this.txtQXnum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQXName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvModel = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qxmc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qxcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQXGL)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModel)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvQXGL
            // 
            this.dgvQXGL.AllowUserToAddRows = false;
            this.dgvQXGL.AllowUserToResizeColumns = false;
            this.dgvQXGL.AllowUserToResizeRows = false;
            this.dgvQXGL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQXGL.BackgroundColor = System.Drawing.Color.White;
            this.dgvQXGL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQXGL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id1,
            this.model1,
            this.qxmc1,
            this.qxcount1});
            this.dgvQXGL.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvQXGL.Location = new System.Drawing.Point(3, 3);
            this.dgvQXGL.Name = "dgvQXGL";
            this.dgvQXGL.ReadOnly = true;
            this.dgvQXGL.RowHeadersVisible = false;
            this.dgvQXGL.RowTemplate.Height = 23;
            this.dgvQXGL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQXGL.Size = new System.Drawing.Size(485, 393);
            this.dgvQXGL.TabIndex = 0;
            this.dgvQXGL.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // id1
            // 
            this.id1.DataPropertyName = "id";
            this.id1.HeaderText = "id";
            this.id1.Name = "id1";
            this.id1.ReadOnly = true;
            this.id1.Visible = false;
            // 
            // model1
            // 
            this.model1.DataPropertyName = "model";
            this.model1.HeaderText = "器械包名";
            this.model1.Name = "model1";
            this.model1.ReadOnly = true;
            // 
            // qxmc1
            // 
            this.qxmc1.DataPropertyName = "qxmc";
            this.qxmc1.FillWeight = 118.7817F;
            this.qxmc1.HeaderText = "器械名称";
            this.qxmc1.Name = "qxmc1";
            this.qxmc1.ReadOnly = true;
            // 
            // qxcount1
            // 
            this.qxcount1.DataPropertyName = "qxcount";
            this.qxcount1.FillWeight = 81.21828F;
            this.qxcount1.HeaderText = "器械数量";
            this.qxcount1.Name = "qxcount1";
            this.qxcount1.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除器械ToolStripMenuItem,
            this.TSTJSHToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // 删除器械ToolStripMenuItem
            // 
            this.删除器械ToolStripMenuItem.Name = "删除器械ToolStripMenuItem";
            this.删除器械ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除器械ToolStripMenuItem.Text = "删除器械";
            this.删除器械ToolStripMenuItem.Click += new System.EventHandler(this.删除器械ToolStripMenuItem_Click);
            // 
            // TSTJSHToolStripMenuItem
            // 
            this.TSTJSHToolStripMenuItem.Name = "TSTJSHToolStripMenuItem";
            this.TSTJSHToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.TSTJSHToolStripMenuItem.Text = "添加三行";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(16, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "选择器械包：";
            // 
            // cmbQXGL
            // 
            this.cmbQXGL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQXGL.FormattingEnabled = true;
            this.cmbQXGL.Location = new System.Drawing.Point(116, 32);
            this.cmbQXGL.Name = "cmbQXGL";
            this.cmbQXGL.Size = new System.Drawing.Size(187, 20);
            this.cmbQXGL.TabIndex = 7;
            this.cmbQXGL.SelectedIndexChanged += new System.EventHandler(this.cmbQXGL_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(69, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "增加器械";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnC);
            this.groupBox2.Controls.Add(this.txtQXnum);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtQXName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbQXGL);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(494, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 393);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "器械操作";
            // 
            // btnC
            // 
            this.btnC.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnC.ForeColor = System.Drawing.Color.Red;
            this.btnC.Location = new System.Drawing.Point(189, 288);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(114, 35);
            this.btnC.TabIndex = 17;
            this.btnC.Text = "删除器械";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // txtQXnum
            // 
            this.txtQXnum.Location = new System.Drawing.Point(119, 212);
            this.txtQXnum.Name = "txtQXnum";
            this.txtQXnum.Size = new System.Drawing.Size(187, 21);
            this.txtQXnum.TabIndex = 16;
            this.txtQXnum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQXnum_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(34, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "器械数量:";
            // 
            // txtQXName
            // 
            this.txtQXName.Location = new System.Drawing.Point(116, 129);
            this.txtQXName.Name = "txtQXName";
            this.txtQXName.Size = new System.Drawing.Size(190, 21);
            this.txtQXName.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(31, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "器械名称:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(822, 425);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.dgvModel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(814, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "器械包管理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.cmbModel);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(458, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(353, 393);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "器械包操作";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(140, 199);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(172, 21);
            this.textBox2.TabIndex = 13;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(7, 290);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 52);
            this.button3.TabIndex = 9;
            this.button3.Text = "增加器械包";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(37, -67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "器械数量:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(34, -95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "器械名称:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(256, 290);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 52);
            this.button2.TabIndex = 8;
            this.button2.Text = "删除器械包";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbModel
            // 
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Location = new System.Drawing.Point(141, 112);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(171, 20);
            this.cmbModel.TabIndex = 7;
            this.cmbModel.SelectedIndexChanged += new System.EventHandler(this.cmbModel_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(19, -126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "选择器械包：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(34, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "选择器械包：";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(114, 290);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 52);
            this.button4.TabIndex = 6;
            this.button4.Text = "修改器械包名称";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(33, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "器械包名称：";
            // 
            // dgvModel
            // 
            this.dgvModel.AllowUserToAddRows = false;
            this.dgvModel.AllowUserToResizeColumns = false;
            this.dgvModel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvModel.BackgroundColor = System.Drawing.Color.White;
            this.dgvModel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.model,
            this.qxmc,
            this.qxcount});
            this.dgvModel.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvModel.Location = new System.Drawing.Point(3, 3);
            this.dgvModel.Name = "dgvModel";
            this.dgvModel.ReadOnly = true;
            this.dgvModel.RowHeadersVisible = false;
            this.dgvModel.RowTemplate.Height = 23;
            this.dgvModel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvModel.Size = new System.Drawing.Size(448, 393);
            this.dgvModel.TabIndex = 17;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // model
            // 
            this.model.DataPropertyName = "model";
            this.model.HeaderText = "器械包名";
            this.model.Name = "model";
            this.model.ReadOnly = true;
            // 
            // qxmc
            // 
            this.qxmc.DataPropertyName = "qxmc";
            this.qxmc.FillWeight = 118.7817F;
            this.qxmc.HeaderText = "器械名称";
            this.qxmc.Name = "qxmc";
            this.qxmc.ReadOnly = true;
            // 
            // qxcount
            // 
            this.qxcount.DataPropertyName = "qxcount";
            this.qxcount.FillWeight = 81.21828F;
            this.qxcount.HeaderText = "器械数量";
            this.qxcount.Name = "qxcount";
            this.qxcount.ReadOnly = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvQXGL);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(814, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "器械管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // QiXieQD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 425);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QiXieQD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "器械清点";
            this.Load += new System.EventHandler(this.QiXieQD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQXGL)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModel)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvQXGL;
        private System.Windows.Forms.ComboBox cmbQXGL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtQXnum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQXName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除器械ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSTJSHToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn id1;
        private System.Windows.Forms.DataGridViewTextBoxColumn model1;
        private System.Windows.Forms.DataGridViewTextBoxColumn qxmc1;
        private System.Windows.Forms.DataGridViewTextBoxColumn qxcount1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn model;
        private System.Windows.Forms.DataGridViewTextBoxColumn qxmc;
        private System.Windows.Forms.DataGridViewTextBoxColumn qxcount;
        private System.Windows.Forms.Button btnC;
    }
}