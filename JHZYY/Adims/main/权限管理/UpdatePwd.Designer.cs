namespace main.权限管理
{
    partial class UpdatePwd
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
            this.old_txt = new System.Windows.Forms.TextBox();
            this.new1_txt = new System.Windows.Forms.TextBox();
            this.new2_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbUserName = new System.Windows.Forms.Label();
            this.lbUserNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // old_txt
            // 
            this.old_txt.Location = new System.Drawing.Point(116, 85);
            this.old_txt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.old_txt.Name = "old_txt";
            this.old_txt.PasswordChar = '*';
            this.old_txt.Size = new System.Drawing.Size(157, 23);
            this.old_txt.TabIndex = 0;
            // 
            // new1_txt
            // 
            this.new1_txt.Location = new System.Drawing.Point(116, 122);
            this.new1_txt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.new1_txt.MaxLength = 12;
            this.new1_txt.Name = "new1_txt";
            this.new1_txt.PasswordChar = '*';
            this.new1_txt.Size = new System.Drawing.Size(157, 23);
            this.new1_txt.TabIndex = 1;
            // 
            // new2_txt
            // 
            this.new2_txt.Location = new System.Drawing.Point(116, 160);
            this.new2_txt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.new2_txt.MaxLength = 12;
            this.new2_txt.Name = "new2_txt";
            this.new2_txt.PasswordChar = '*';
            this.new2_txt.Size = new System.Drawing.Size(157, 23);
            this.new2_txt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "  原 密 码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "  新 密 码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "新密码确认";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(206, 205);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 205);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 33);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Location = new System.Drawing.Point(129, 51);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(43, 17);
            this.lbUserName.TabIndex = 11;
            this.lbUserName.Text = "label4";
            // 
            // lbUserNo
            // 
            this.lbUserNo.AutoSize = true;
            this.lbUserNo.Location = new System.Drawing.Point(129, 21);
            this.lbUserNo.Name = "lbUserNo";
            this.lbUserNo.Size = new System.Drawing.Size(43, 17);
            this.lbUserNo.TabIndex = 10;
            this.lbUserNo.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "用户姓名";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "用户编号";
            // 
            // UpdatePwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 267);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.lbUserNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.new2_txt);
            this.Controls.Add(this.new1_txt);
            this.Controls.Add(this.old_txt);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatePwd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改登录密码";
            this.Load += new System.EventHandler(this.UpdatePwd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox old_txt;
        private System.Windows.Forms.TextBox new1_txt;
        private System.Windows.Forms.TextBox new2_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label lbUserNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}