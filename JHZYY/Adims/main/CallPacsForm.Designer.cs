namespace main
{
    partial class CallPacsForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbPatId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCall = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmbPictureType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbPatId
            // 
            this.tbPatId.Location = new System.Drawing.Point(212, 128);
            this.tbPatId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPatId.Name = "tbPatId";
            this.tbPatId.Size = new System.Drawing.Size(239, 31);
            this.tbPatId.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 131);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "病人编号：";
            // 
            // buttonCall
            // 
            this.buttonCall.Location = new System.Drawing.Point(348, 262);
            this.buttonCall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new System.Drawing.Size(103, 47);
            this.buttonCall.TabIndex = 2;
            this.buttonCall.Text = "查看";
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new System.EventHandler(this.buttonCall_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "患者编号类型：";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "门诊号",
            "住院号",
            "处方号",
            "社保号"});
            this.cmbType.Location = new System.Drawing.Point(212, 56);
            this.cmbType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(239, 32);
            this.cmbType.TabIndex = 5;
            // 
            // cmbPictureType
            // 
            this.cmbPictureType.FormattingEnabled = true;
            this.cmbPictureType.Items.AddRange(new object[] {
            "图像",
            "报告"});
            this.cmbPictureType.Location = new System.Drawing.Point(212, 198);
            this.cmbPictureType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbPictureType.Name = "cmbPictureType";
            this.cmbPictureType.Size = new System.Drawing.Size(239, 32);
            this.cmbPictureType.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 201);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "图像类型：";
            // 
            // CallPacsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 347);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbPictureType);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPatId);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CallPacsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "调用Pacs";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CallForm_FormClosed);
            this.Load += new System.EventHandler(this.CallForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPatId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.ComboBox cmbPictureType;
        private System.Windows.Forms.Label label3;
    }
}

