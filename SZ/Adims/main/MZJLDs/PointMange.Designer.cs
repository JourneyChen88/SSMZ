namespace main
{
    partial class PointManage
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbETCO2 = new System.Windows.Forms.TextBox();
            this.tbTemp = new System.Windows.Forms.TextBox();
            this.tbPulse = new System.Windows.Forms.TextBox();
            this.tbNIBPS = new System.Windows.Forms.TextBox();
            this.tbNIBPD = new System.Windows.Forms.TextBox();
            this.tbSPO2 = new System.Windows.Forms.TextBox();
            this.tbRRC = new System.Windows.Forms.TextBox();
            this.dtRecordTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbTOF = new System.Windows.Forms.TextBox();
            this.tbBIS = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.CoDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Co0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecordTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nibps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nibpd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pulse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etco2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.temp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tof = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CoDelete,
            this.Co0,
            this.RecordTime,
            this.nibps,
            this.nibpd,
            this.pulse,
            this.rrc,
            this.spo2,
            this.etco2,
            this.temp,
            this.tof,
            this.bis});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(945, 367);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.label8, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbETCO2, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbTemp, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPulse, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbNIBPS, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbNIBPD, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbSPO2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbRRC, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtRecordTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbTOF, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbBIS, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnUpdate, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnReset, 6, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 373);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(945, 183);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(748, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 35);
            this.label8.TabIndex = 75;
            this.label8.Text = "体温：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(497, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 35);
            this.label7.TabIndex = 74;
            this.label7.Text = "ETCO2：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(269, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 35);
            this.label6.TabIndex = 73;
            this.label6.Text = "SPO2：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(43, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 35);
            this.label5.TabIndex = 72;
            this.label5.Text = "呼吸：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(748, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 35);
            this.label4.TabIndex = 71;
            this.label4.Text = "脉搏：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(500, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 35);
            this.label3.TabIndex = 70;
            this.label3.Text = "舒张压：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(265, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 35);
            this.label2.TabIndex = 69;
            this.label2.Text = "收缩压：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbETCO2
            // 
            this.tbETCO2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbETCO2.Location = new System.Drawing.Point(567, 38);
            this.tbETCO2.Name = "tbETCO2";
            this.tbETCO2.Size = new System.Drawing.Size(108, 23);
            this.tbETCO2.TabIndex = 63;
            this.tbETCO2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbETCO2_KeyPress);
            // 
            // tbTemp
            // 
            this.tbTemp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTemp.Location = new System.Drawing.Point(802, 38);
            this.tbTemp.Name = "tbTemp";
            this.tbTemp.Size = new System.Drawing.Size(108, 23);
            this.tbTemp.TabIndex = 62;
            this.tbTemp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCVP_KeyPress);
            // 
            // tbPulse
            // 
            this.tbPulse.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPulse.Location = new System.Drawing.Point(802, 3);
            this.tbPulse.Name = "tbPulse";
            this.tbPulse.Size = new System.Drawing.Size(108, 23);
            this.tbPulse.TabIndex = 60;
            this.tbPulse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPulse_KeyPress);
            // 
            // tbNIBPS
            // 
            this.tbNIBPS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbNIBPS.Location = new System.Drawing.Point(332, 3);
            this.tbNIBPS.Name = "tbNIBPS";
            this.tbNIBPS.Size = new System.Drawing.Size(98, 23);
            this.tbNIBPS.TabIndex = 6;
            this.tbNIBPS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNIBPS_KeyPress);
            // 
            // tbNIBPD
            // 
            this.tbNIBPD.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbNIBPD.Location = new System.Drawing.Point(567, 3);
            this.tbNIBPD.Name = "tbNIBPD";
            this.tbNIBPD.Size = new System.Drawing.Size(108, 23);
            this.tbNIBPD.TabIndex = 7;
            this.tbNIBPD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNIBPD_KeyPress);
            // 
            // tbSPO2
            // 
            this.tbSPO2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSPO2.Location = new System.Drawing.Point(332, 38);
            this.tbSPO2.Name = "tbSPO2";
            this.tbSPO2.Size = new System.Drawing.Size(98, 23);
            this.tbSPO2.TabIndex = 11;
            this.tbSPO2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSPO2_KeyPress);
            // 
            // tbRRC
            // 
            this.tbRRC.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRRC.Location = new System.Drawing.Point(97, 38);
            this.tbRRC.Name = "tbRRC";
            this.tbRRC.Size = new System.Drawing.Size(116, 23);
            this.tbRRC.TabIndex = 13;
            this.tbRRC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRRC_KeyPress);
            // 
            // dtRecordTime
            // 
            this.dtRecordTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtRecordTime.Location = new System.Drawing.Point(97, 3);
            this.dtRecordTime.Name = "dtRecordTime";
            this.dtRecordTime.Size = new System.Drawing.Size(135, 21);
            this.dtRecordTime.TabIndex = 64;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(17, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 35);
            this.label1.TabIndex = 68;
            this.label1.Text = "监测时间：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(44, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 40);
            this.label9.TabIndex = 77;
            this.label9.Text = "TOF：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(284, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 40);
            this.label10.TabIndex = 82;
            this.label10.Text = "BIS：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbTOF
            // 
            this.tbTOF.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTOF.Location = new System.Drawing.Point(97, 73);
            this.tbTOF.Name = "tbTOF";
            this.tbTOF.Size = new System.Drawing.Size(116, 23);
            this.tbTOF.TabIndex = 83;
            // 
            // tbBIS
            // 
            this.tbBIS.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbBIS.Location = new System.Drawing.Point(332, 73);
            this.tbBIS.Name = "tbBIS";
            this.tbBIS.Size = new System.Drawing.Size(98, 23);
            this.tbBIS.TabIndex = 84;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("幼圆", 11F);
            this.btnAdd.ForeColor = System.Drawing.Color.Blue;
            this.btnAdd.Location = new System.Drawing.Point(238, 113);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 34);
            this.btnAdd.TabIndex = 76;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("幼圆", 11F);
            this.btnDelete.ForeColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(332, 113);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 34);
            this.btnDelete.TabIndex = 78;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("幼圆", 11F);
            this.btnUpdate.ForeColor = System.Drawing.Color.Blue;
            this.btnUpdate.Location = new System.Drawing.Point(473, 113);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(74, 34);
            this.btnUpdate.TabIndex = 79;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("幼圆", 11F);
            this.btnClose.ForeColor = System.Drawing.Color.Blue;
            this.btnClose.Location = new System.Drawing.Point(567, 113);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 34);
            this.btnClose.TabIndex = 80;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("幼圆", 11F);
            this.btnReset.ForeColor = System.Drawing.Color.Blue;
            this.btnReset.Location = new System.Drawing.Point(708, 113);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(74, 34);
            this.btnReset.TabIndex = 81;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // CoDelete
            // 
            this.CoDelete.FillWeight = 194.0482F;
            this.CoDelete.HeaderText = "是否删除";
            this.CoDelete.Name = "CoDelete";
            this.CoDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoDelete.Width = 59;
            // 
            // Co0
            // 
            this.Co0.DataPropertyName = "mzjldid";
            this.Co0.HeaderText = "麻醉编号";
            this.Co0.Name = "Co0";
            this.Co0.ReadOnly = true;
            this.Co0.Visible = false;
            this.Co0.Width = 78;
            // 
            // RecordTime
            // 
            this.RecordTime.DataPropertyName = "RecordTime";
            this.RecordTime.FillWeight = 639.5937F;
            this.RecordTime.HeaderText = "检测时间";
            this.RecordTime.Name = "RecordTime";
            this.RecordTime.ReadOnly = true;
            this.RecordTime.Width = 78;
            // 
            // nibps
            // 
            this.nibps.DataPropertyName = "nibps";
            this.nibps.FillWeight = 9.479706F;
            this.nibps.HeaderText = "收缩压";
            this.nibps.Name = "nibps";
            this.nibps.ReadOnly = true;
            this.nibps.Width = 66;
            // 
            // nibpd
            // 
            this.nibpd.DataPropertyName = "nibpd";
            this.nibpd.FillWeight = 9.479706F;
            this.nibpd.HeaderText = "舒张压";
            this.nibpd.Name = "nibpd";
            this.nibpd.ReadOnly = true;
            this.nibpd.Width = 66;
            // 
            // pulse
            // 
            this.pulse.DataPropertyName = "pulse";
            this.pulse.FillWeight = 9.479706F;
            this.pulse.HeaderText = "脉搏";
            this.pulse.Name = "pulse";
            this.pulse.ReadOnly = true;
            this.pulse.Width = 54;
            // 
            // rrc
            // 
            this.rrc.DataPropertyName = "rrc";
            this.rrc.FillWeight = 9.479706F;
            this.rrc.HeaderText = "呼吸";
            this.rrc.Name = "rrc";
            this.rrc.ReadOnly = true;
            this.rrc.Width = 54;
            // 
            // spo2
            // 
            this.spo2.DataPropertyName = "spo2";
            this.spo2.FillWeight = 9.479706F;
            this.spo2.HeaderText = "SpO2";
            this.spo2.Name = "spo2";
            this.spo2.ReadOnly = true;
            this.spo2.Width = 54;
            // 
            // etco2
            // 
            this.etco2.DataPropertyName = "etco2";
            this.etco2.FillWeight = 9.479706F;
            this.etco2.HeaderText = "ETCO2";
            this.etco2.Name = "etco2";
            this.etco2.ReadOnly = true;
            this.etco2.Width = 60;
            // 
            // temp
            // 
            this.temp.DataPropertyName = "temp";
            this.temp.FillWeight = 9.479706F;
            this.temp.HeaderText = "体温";
            this.temp.Name = "temp";
            this.temp.ReadOnly = true;
            this.temp.Width = 54;
            // 
            // tof
            // 
            this.tof.DataPropertyName = "tof";
            this.tof.HeaderText = "TOF";
            this.tof.Name = "tof";
            this.tof.ReadOnly = true;
            this.tof.Width = 48;
            // 
            // bis
            // 
            this.bis.DataPropertyName = "bis";
            this.bis.HeaderText = "BIS";
            this.bis.Name = "bis";
            this.bis.ReadOnly = true;
            this.bis.Width = 48;
            // 
            // PointManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 556);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PointManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测数据管理";
            this.Load += new System.EventHandler(this.SljlAddPoint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbETCO2;
        private System.Windows.Forms.TextBox tbTemp;
        private System.Windows.Forms.TextBox tbPulse;
        private System.Windows.Forms.TextBox tbNIBPS;
        private System.Windows.Forms.TextBox tbNIBPD;
        private System.Windows.Forms.TextBox tbSPO2;
        private System.Windows.Forms.TextBox tbRRC;
        private System.Windows.Forms.DateTimePicker dtRecordTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbTOF;
        private System.Windows.Forms.TextBox tbBIS;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CoDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Co0;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn nibps;
        private System.Windows.Forms.DataGridViewTextBoxColumn nibpd;
        private System.Windows.Forms.DataGridViewTextBoxColumn pulse;
        private System.Windows.Forms.DataGridViewTextBoxColumn rrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn spo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn etco2;
        private System.Windows.Forms.DataGridViewTextBoxColumn temp;
        private System.Windows.Forms.DataGridViewTextBoxColumn tof;
        private System.Windows.Forms.DataGridViewTextBoxColumn bis;
    }
}