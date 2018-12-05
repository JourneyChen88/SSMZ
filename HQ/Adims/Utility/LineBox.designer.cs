namespace adims_Utility
{
    partial class LineBox
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lableName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbValue
            // 
            this.tbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbValue.BackColor = System.Drawing.SystemColors.Control;
            this.tbValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbValue.Location = new System.Drawing.Point(56, 0);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(137, 18);
            this.tbValue.TabIndex = 0;
            this.tbValue.Click += new System.EventHandler(this.tbValue_Click);
            // 
            // lableName
            // 
            this.lableName.AutoSize = true;
            this.lableName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lableName.Location = new System.Drawing.Point(0, 0);
            this.lableName.Name = "lableName";
            this.lableName.Size = new System.Drawing.Size(47, 15);
            this.lableName.TabIndex = 1;
            this.lableName.Text = "lable";
            this.lableName.TextChanged += new System.EventHandler(this.lableName_TextChanged);
            // 
            // LineBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lableName);
            this.Controls.Add(this.tbValue);
            this.Name = "LineBox";
            this.Size = new System.Drawing.Size(193, 28);
            this.Load += new System.EventHandler(this.LineBox_Load);
            this.Click += new System.EventHandler(this.LineBox_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LineBox_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lableName;
    }
}
