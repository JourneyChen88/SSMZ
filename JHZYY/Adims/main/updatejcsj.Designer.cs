namespace main
{
    partial class updatejcsj
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbxjhlx = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtjkValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(217, 173);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 40);
            this.button2.TabIndex = 91;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 173);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 40);
            this.button1.TabIndex = 90;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 89;
            this.label2.Text = "结束时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 88;
            this.label1.Text = "开始时间";
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(133, 115);
            this.dtEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(179, 21);
            this.dtEnd.TabIndex = 87;
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(133, 77);
            this.dtStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(179, 21);
            this.dtStart.TabIndex = 86;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 92;
            this.label3.Text = "修改的列名";
            // 
            // cmbxjhlx
            // 
            this.cmbxjhlx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxjhlx.FormattingEnabled = true;
            this.cmbxjhlx.Items.AddRange(new object[] {
            "舒张压",
            "收缩压",
            "呼吸",
            "脉搏",
            "SPO2",
            "体温",
            "ETCO2",
            "CVP",
            "BIS"});
            this.cmbxjhlx.Location = new System.Drawing.Point(131, 18);
            this.cmbxjhlx.Name = "cmbxjhlx";
            this.cmbxjhlx.Size = new System.Drawing.Size(121, 20);
            this.cmbxjhlx.TabIndex = 93;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 94;
            this.label4.Text = "修改的值";
            // 
            // txtjkValue
            // 
            this.txtjkValue.Location = new System.Drawing.Point(133, 48);
            this.txtjkValue.Name = "txtjkValue";
            this.txtjkValue.Size = new System.Drawing.Size(119, 21);
            this.txtjkValue.TabIndex = 95;
            // 
            // updatejcsj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 262);
            this.Controls.Add(this.txtjkValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbxjhlx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtStart);
            this.Name = "updatejcsj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改某列数据";
            this.Load += new System.EventHandler(this.updatejcsj_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbxjhlx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtjkValue;
    }
}