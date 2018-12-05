namespace main
{
    partial class LisResult
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbSqzd = new System.Windows.Forms.Label();
            this.lbBedNO = new System.Windows.Forms.Label();
            this.lbKeshi = new System.Windows.Forms.Label();
            this.lbAge = new System.Windows.Forms.Label();
            this.lbSex = new System.Windows.Forms.Label();
            this.lbPatname = new System.Windows.Forms.Label();
            this.lbPatID = new System.Windows.Forms.Label();
            this.dtOdate = new System.Windows.Forms.DateTimePicker();
            this.label68 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(843, 554);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtOdate);
            this.panel1.Controls.Add(this.label68);
            this.panel1.Controls.Add(this.lbSqzd);
            this.panel1.Controls.Add(this.lbBedNO);
            this.panel1.Controls.Add(this.lbKeshi);
            this.panel1.Controls.Add(this.lbAge);
            this.panel1.Controls.Add(this.lbSex);
            this.panel1.Controls.Add(this.lbPatname);
            this.panel1.Controls.Add(this.lbPatID);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(21, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 653);
            this.panel1.TabIndex = 2;
            // 
            // lbSqzd
            // 
            this.lbSqzd.AutoSize = true;
            this.lbSqzd.Location = new System.Drawing.Point(180, 71);
            this.lbSqzd.Name = "lbSqzd";
            this.lbSqzd.Size = new System.Drawing.Size(65, 12);
            this.lbSqzd.TabIndex = 8;
            this.lbSqzd.Text = "术前诊断：";
            // 
            // lbBedNO
            // 
            this.lbBedNO.AutoSize = true;
            this.lbBedNO.Location = new System.Drawing.Point(41, 71);
            this.lbBedNO.Name = "lbBedNO";
            this.lbBedNO.Size = new System.Drawing.Size(41, 12);
            this.lbBedNO.TabIndex = 7;
            this.lbBedNO.Text = "床号：";
            // 
            // lbKeshi
            // 
            this.lbKeshi.AutoSize = true;
            this.lbKeshi.Location = new System.Drawing.Point(581, 45);
            this.lbKeshi.Name = "lbKeshi";
            this.lbKeshi.Size = new System.Drawing.Size(41, 12);
            this.lbKeshi.TabIndex = 6;
            this.lbKeshi.Text = "科室：";
            // 
            // lbAge
            // 
            this.lbAge.AutoSize = true;
            this.lbAge.Location = new System.Drawing.Point(458, 45);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(41, 12);
            this.lbAge.TabIndex = 5;
            this.lbAge.Text = "年龄：";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.Location = new System.Drawing.Point(360, 45);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(41, 12);
            this.lbSex.TabIndex = 4;
            this.lbSex.Text = "性别：";
            // 
            // lbPatname
            // 
            this.lbPatname.AutoSize = true;
            this.lbPatname.Location = new System.Drawing.Point(180, 45);
            this.lbPatname.Name = "lbPatname";
            this.lbPatname.Size = new System.Drawing.Size(65, 12);
            this.lbPatname.TabIndex = 3;
            this.lbPatname.Text = "病人姓名：";
            // 
            // lbPatID
            // 
            this.lbPatID.AutoSize = true;
            this.lbPatID.Location = new System.Drawing.Point(41, 45);
            this.lbPatID.Name = "lbPatID";
            this.lbPatID.Size = new System.Drawing.Size(53, 12);
            this.lbPatID.TabIndex = 2;
            this.lbPatID.Text = "住院号：";
            // 
            // dtOdate
            // 
            this.dtOdate.CalendarMonthBackground = System.Drawing.SystemColors.InactiveCaption;
            this.dtOdate.Location = new System.Drawing.Point(116, 10);
            this.dtOdate.Name = "dtOdate";
            this.dtOdate.Size = new System.Drawing.Size(111, 21);
            this.dtOdate.TabIndex = 585;
            this.dtOdate.ValueChanged += new System.EventHandler(this.dtOdate_ValueChanged);
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label68.ForeColor = System.Drawing.Color.Maroon;
            this.label68.Location = new System.Drawing.Point(41, 16);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(57, 12);
            this.label68.TabIndex = 584;
            this.label68.Text = "接收日期";
            // 
            // LisResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 678);
            this.Controls.Add(this.panel1);
            this.Name = "LisResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检验病历";
            this.Load += new System.EventHandler(this.jybl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbPatID;
        private System.Windows.Forms.Label lbPatname;
        private System.Windows.Forms.Label lbSex;
        private System.Windows.Forms.Label lbAge;
        private System.Windows.Forms.Label lbKeshi;
        private System.Windows.Forms.Label lbSqzd;
        private System.Windows.Forms.Label lbBedNO;
        private System.Windows.Forms.DateTimePicker dtOdate;
        private System.Windows.Forms.Label label68;
    }
}