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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPulse = new System.Windows.Forms.TextBox();
            this.tbNIBPS = new System.Windows.Forms.TextBox();
            this.tbNIBPD = new System.Windows.Forms.TextBox();
            this.tbSPO2 = new System.Windows.Forms.TextBox();
            this.tbRRC = new System.Windows.Forms.TextBox();
            this.dtRecordTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tbjsz = new System.Windows.Forms.TextBox();
            this.lbljsz = new System.Windows.Forms.Label();
            this.tbsdz = new System.Windows.Forms.TextBox();
            this.lblsdz = new System.Windows.Forms.Label();
            this.tbqdy = new System.Windows.Forms.TextBox();
            this.lblqdy = new System.Windows.Forms.Label();
            this.tbCVP = new System.Windows.Forms.TextBox();
            this.lblCVP = new System.Windows.Forms.Label();
            this.lbltw = new System.Windows.Forms.Label();
            this.tbtemp = new System.Windows.Forms.TextBox();
            this.tbetco2 = new System.Windows.Forms.TextBox();
            this.CoDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Co0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecordTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nibps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nibpd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pulse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etco2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cvp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tof = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.temp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qdy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sdz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jsz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nibpm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
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
            this.cvp,
            this.tof,
            this.temp,
            this.qdy,
            this.sdz,
            this.jsz,
            this.nibpm,
            this.hr});
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
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbPulse, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbNIBPS, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbNIBPD, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbSPO2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbRRC, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtRecordTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button4, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.button7, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.button2, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.button3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.button5, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbjsz, 7, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbljsz, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbsdz, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblsdz, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbqdy, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblqdy, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbCVP, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCVP, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbltw, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbtemp, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbetco2, 5, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 373);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(945, 153);
            this.tableLayoutPanel1.TabIndex = 11;
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
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Right;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.button4.ForeColor = System.Drawing.Color.Blue;
            this.button4.Location = new System.Drawing.Point(393, 113);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 37);
            this.button4.TabIndex = 36;
            this.button4.Text = "修改";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Right;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.button7.ForeColor = System.Drawing.Color.Blue;
            this.button7.Location = new System.Drawing.Point(487, 113);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(74, 37);
            this.button7.TabIndex = 57;
            this.button7.Text = "退出";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.button2.ForeColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(633, 113);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 37);
            this.button2.TabIndex = 66;
            this.button2.Text = "重置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Right;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(252, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 37);
            this.button3.TabIndex = 35;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Right;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.button5.ForeColor = System.Drawing.Color.Blue;
            this.button5.Location = new System.Drawing.Point(158, 113);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(74, 37);
            this.button5.TabIndex = 53;
            this.button5.Text = "添加";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tbjsz
            // 
            this.tbjsz.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbjsz.Location = new System.Drawing.Point(802, 73);
            this.tbjsz.Name = "tbjsz";
            this.tbjsz.Size = new System.Drawing.Size(108, 23);
            this.tbjsz.TabIndex = 81;
            this.tbjsz.Visible = false;
            // 
            // lbljsz
            // 
            this.lbljsz.AutoSize = true;
            this.lbljsz.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbljsz.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.lbljsz.Location = new System.Drawing.Point(735, 70);
            this.lbljsz.Name = "lbljsz";
            this.lbljsz.Size = new System.Drawing.Size(61, 40);
            this.lbljsz.TabIndex = 78;
            this.lbljsz.Text = "肌松值：";
            this.lbljsz.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbljsz.Visible = false;
            // 
            // tbsdz
            // 
            this.tbsdz.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbsdz.Location = new System.Drawing.Point(567, 73);
            this.tbsdz.Name = "tbsdz";
            this.tbsdz.Size = new System.Drawing.Size(108, 23);
            this.tbsdz.TabIndex = 80;
            this.tbsdz.Visible = false;
            // 
            // lblsdz
            // 
            this.lblsdz.AutoSize = true;
            this.lblsdz.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblsdz.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.lblsdz.Location = new System.Drawing.Point(500, 70);
            this.lblsdz.Name = "lblsdz";
            this.lblsdz.Size = new System.Drawing.Size(61, 40);
            this.lblsdz.TabIndex = 76;
            this.lblsdz.Text = "深度值：";
            this.lblsdz.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblsdz.Visible = false;
            // 
            // tbqdy
            // 
            this.tbqdy.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbqdy.Location = new System.Drawing.Point(332, 73);
            this.tbqdy.Name = "tbqdy";
            this.tbqdy.Size = new System.Drawing.Size(98, 23);
            this.tbqdy.TabIndex = 79;
            this.tbqdy.Visible = false;
            // 
            // lblqdy
            // 
            this.lblqdy.AutoSize = true;
            this.lblqdy.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblqdy.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.lblqdy.Location = new System.Drawing.Point(265, 70);
            this.lblqdy.Name = "lblqdy";
            this.lblqdy.Size = new System.Drawing.Size(61, 40);
            this.lblqdy.TabIndex = 77;
            this.lblqdy.Text = "气道压：";
            this.lblqdy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblqdy.Visible = false;
            // 
            // tbCVP
            // 
            this.tbCVP.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCVP.Location = new System.Drawing.Point(97, 73);
            this.tbCVP.Name = "tbCVP";
            this.tbCVP.Size = new System.Drawing.Size(108, 23);
            this.tbCVP.TabIndex = 62;
            this.tbCVP.Visible = false;
            this.tbCVP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCVP_KeyPress);
            // 
            // lblCVP
            // 
            this.lblCVP.AutoSize = true;
            this.lblCVP.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCVP.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCVP.Location = new System.Drawing.Point(43, 70);
            this.lblCVP.Name = "lblCVP";
            this.lblCVP.Size = new System.Drawing.Size(48, 40);
            this.lblCVP.TabIndex = 75;
            this.lblCVP.Text = "CVP：";
            this.lblCVP.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblCVP.Visible = false;
            // 
            // lbltw
            // 
            this.lbltw.AutoSize = true;
            this.lbltw.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbltw.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltw.Location = new System.Drawing.Point(748, 35);
            this.lbltw.Name = "lbltw";
            this.lbltw.Size = new System.Drawing.Size(48, 35);
            this.lbltw.TabIndex = 82;
            this.lbltw.Text = "体温：";
            this.lbltw.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbtemp
            // 
            this.tbtemp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbtemp.Location = new System.Drawing.Point(802, 38);
            this.tbtemp.Name = "tbtemp";
            this.tbtemp.Size = new System.Drawing.Size(108, 23);
            this.tbtemp.TabIndex = 63;
            // 
            // tbetco2
            // 
            this.tbetco2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbetco2.Location = new System.Drawing.Point(567, 38);
            this.tbetco2.Name = "tbetco2";
            this.tbetco2.Size = new System.Drawing.Size(108, 23);
            this.tbetco2.TabIndex = 83;
            this.tbetco2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbETCO2_KeyPress);
            // 
            // CoDelete
            // 
            this.CoDelete.HeaderText = "是否删除";
            this.CoDelete.Name = "CoDelete";
            this.CoDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoDelete.Width = 80;
            // 
            // Co0
            // 
            this.Co0.DataPropertyName = "mzjldid";
            this.Co0.HeaderText = "麻醉编号";
            this.Co0.Name = "Co0";
            this.Co0.ReadOnly = true;
            this.Co0.Visible = false;
            // 
            // RecordTime
            // 
            this.RecordTime.DataPropertyName = "RecordTime";
            this.RecordTime.HeaderText = "检测时间";
            this.RecordTime.Name = "RecordTime";
            this.RecordTime.ReadOnly = true;
            this.RecordTime.Width = 130;
            // 
            // nibps
            // 
            this.nibps.DataPropertyName = "nibps";
            this.nibps.HeaderText = "收缩压";
            this.nibps.Name = "nibps";
            this.nibps.ReadOnly = true;
            this.nibps.Width = 80;
            // 
            // nibpd
            // 
            this.nibpd.DataPropertyName = "nibpd";
            this.nibpd.HeaderText = "舒张压";
            this.nibpd.Name = "nibpd";
            this.nibpd.ReadOnly = true;
            this.nibpd.Width = 80;
            // 
            // pulse
            // 
            this.pulse.DataPropertyName = "pulse";
            this.pulse.HeaderText = "脉搏";
            this.pulse.Name = "pulse";
            this.pulse.ReadOnly = true;
            this.pulse.Width = 80;
            // 
            // rrc
            // 
            this.rrc.DataPropertyName = "rrc";
            this.rrc.HeaderText = "呼吸";
            this.rrc.Name = "rrc";
            this.rrc.ReadOnly = true;
            this.rrc.Width = 80;
            // 
            // spo2
            // 
            this.spo2.DataPropertyName = "spo2";
            this.spo2.HeaderText = "SpO2";
            this.spo2.Name = "spo2";
            this.spo2.ReadOnly = true;
            this.spo2.Width = 80;
            // 
            // etco2
            // 
            this.etco2.DataPropertyName = "etco2";
            this.etco2.HeaderText = "ETCO2";
            this.etco2.Name = "etco2";
            this.etco2.ReadOnly = true;
            this.etco2.Width = 80;
            // 
            // cvp
            // 
            this.cvp.DataPropertyName = "cvp";
            this.cvp.HeaderText = "CVP";
            this.cvp.Name = "cvp";
            this.cvp.ReadOnly = true;
            this.cvp.Visible = false;
            this.cvp.Width = 80;
            // 
            // tof
            // 
            this.tof.DataPropertyName = "tof";
            this.tof.HeaderText = "TOF";
            this.tof.Name = "tof";
            this.tof.ReadOnly = true;
            this.tof.Visible = false;
            this.tof.Width = 80;
            // 
            // temp
            // 
            this.temp.DataPropertyName = "temp";
            this.temp.HeaderText = "体温";
            this.temp.Name = "temp";
            this.temp.ReadOnly = true;
            this.temp.Width = 80;
            // 
            // qdy
            // 
            this.qdy.DataPropertyName = "qdy";
            this.qdy.HeaderText = "气道压";
            this.qdy.Name = "qdy";
            this.qdy.Visible = false;
            // 
            // sdz
            // 
            this.sdz.DataPropertyName = "sdz";
            this.sdz.HeaderText = "深度值";
            this.sdz.Name = "sdz";
            this.sdz.Visible = false;
            // 
            // jsz
            // 
            this.jsz.DataPropertyName = "jsz";
            this.jsz.HeaderText = "肌松值";
            this.jsz.Name = "jsz";
            this.jsz.Visible = false;
            // 
            // nibpm
            // 
            this.nibpm.DataPropertyName = "nibpm";
            this.nibpm.HeaderText = "无创血压";
            this.nibpm.Name = "nibpm";
            this.nibpm.Visible = false;
            // 
            // hr
            // 
            this.hr.DataPropertyName = "hr";
            this.hr.HeaderText = "hr";
            this.hr.Name = "hr";
            this.hr.Visible = false;
            // 
            // PointManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 526);
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
        private System.Windows.Forms.Label lblCVP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbtemp;
        private System.Windows.Forms.TextBox tbCVP;
        private System.Windows.Forms.TextBox tbPulse;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox tbNIBPS;
        private System.Windows.Forms.TextBox tbNIBPD;
        private System.Windows.Forms.TextBox tbSPO2;
        private System.Windows.Forms.TextBox tbRRC;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DateTimePicker dtRecordTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblsdz;
        private System.Windows.Forms.TextBox tbjsz;
        private System.Windows.Forms.TextBox tbsdz;
        private System.Windows.Forms.TextBox tbqdy;
        private System.Windows.Forms.Label lblqdy;
        private System.Windows.Forms.Label lbljsz;
        private System.Windows.Forms.Label lbltw;
        private System.Windows.Forms.TextBox tbetco2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CoDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Co0;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn nibps;
        private System.Windows.Forms.DataGridViewTextBoxColumn nibpd;
        private System.Windows.Forms.DataGridViewTextBoxColumn pulse;
        private System.Windows.Forms.DataGridViewTextBoxColumn rrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn spo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn etco2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cvp;
        private System.Windows.Forms.DataGridViewTextBoxColumn tof;
        private System.Windows.Forms.DataGridViewTextBoxColumn temp;
        private System.Windows.Forms.DataGridViewTextBoxColumn qdy;
        private System.Windows.Forms.DataGridViewTextBoxColumn sdz;
        private System.Windows.Forms.DataGridViewTextBoxColumn jsz;
        private System.Windows.Forms.DataGridViewTextBoxColumn nibpm;
        private System.Windows.Forms.DataGridViewTextBoxColumn hr;
    }
}