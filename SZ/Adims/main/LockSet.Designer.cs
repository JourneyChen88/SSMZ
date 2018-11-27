namespace main
{
    partial class LockSet
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudSPJG = new System.Windows.Forms.NumericUpDown();
            this.btnSetLockTime = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSPJG)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudSPJG);
            this.groupBox1.Controls.Add(this.btnSetLockTime);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(71, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 179);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "锁屏时间间隔";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(116, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "分钟";
            // 
            // nudSPJG
            // 
            this.nudSPJG.Location = new System.Drawing.Point(20, 57);
            this.nudSPJG.Name = "nudSPJG";
            this.nudSPJG.Size = new System.Drawing.Size(90, 29);
            this.nudSPJG.TabIndex = 5;
            this.nudSPJG.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnSetLockTime
            // 
            this.btnSetLockTime.BackColor = System.Drawing.Color.Transparent;
            this.btnSetLockTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSetLockTime.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSetLockTime.ForeColor = System.Drawing.Color.Blue;
            this.btnSetLockTime.Location = new System.Drawing.Point(41, 107);
            this.btnSetLockTime.Name = "btnSetLockTime";
            this.btnSetLockTime.Size = new System.Drawing.Size(80, 42);
            this.btnSetLockTime.TabIndex = 4;
            this.btnSetLockTime.Text = "确定";
            this.btnSetLockTime.UseVisualStyleBackColor = false;
            this.btnSetLockTime.Click += new System.EventHandler(this.btnSetLockTime_Click);
            // 
            // LockSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 239);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LockSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "锁屏间隔";
            this.Load += new System.EventHandler(this.LockSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSPJG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudSPJG;
        private System.Windows.Forms.Button btnSetLockTime;
    }
}