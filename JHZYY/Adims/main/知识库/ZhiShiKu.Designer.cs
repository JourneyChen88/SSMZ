namespace main.知识库
{
    partial class ZhiShiKu
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
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // helpProvider1
            // 
            this.helpProvider1.HelpNamespace = "D:\\Products\\Adims\\main\\知识库\\zhishiku.chm";
            // 
            // ZhiShiKu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 457);
            this.Name = "ZhiShiKu";
            this.Text = "ZhiShiKu";
            this.Load += new System.EventHandler(this.ZhiShiKu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}