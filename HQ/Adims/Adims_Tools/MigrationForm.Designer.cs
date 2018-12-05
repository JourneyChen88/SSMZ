namespace Adims_Tools
{
    partial class MigrationForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPaiban = new System.Windows.Forms.Button();
            this.btnBeforeVisit_HS = new System.Windows.Forms.Button();
            this.btnMzzqtys = new System.Windows.Forms.Button();
            this.btnBeforeVisit_YS = new System.Windows.Forms.Button();
            this.btnAfterVisit_YS = new System.Windows.Forms.Button();
            this.btnZtzlzqtys = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAnesthesiaSummary = new System.Windows.Forms.Button();
            this.btnMzjld = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPaiban
            // 
            this.btnPaiban.Location = new System.Drawing.Point(56, 70);
            this.btnPaiban.Name = "btnPaiban";
            this.btnPaiban.Size = new System.Drawing.Size(118, 50);
            this.btnPaiban.TabIndex = 0;
            this.btnPaiban.Text = "排班";
            this.btnPaiban.UseVisualStyleBackColor = true;
            this.btnPaiban.Visible = false;
            this.btnPaiban.Click += new System.EventHandler(this.btnPaiban_Click);
            // 
            // btnBeforeVisit_HS
            // 
            this.btnBeforeVisit_HS.Location = new System.Drawing.Point(368, 70);
            this.btnBeforeVisit_HS.Name = "btnBeforeVisit_HS";
            this.btnBeforeVisit_HS.Size = new System.Drawing.Size(118, 50);
            this.btnBeforeVisit_HS.TabIndex = 1;
            this.btnBeforeVisit_HS.Text = " 术前访视-护士";
            this.btnBeforeVisit_HS.UseVisualStyleBackColor = true;
            this.btnBeforeVisit_HS.Click += new System.EventHandler(this.btnBeforeVisit_HS_Click);
            // 
            // btnMzzqtys
            // 
            this.btnMzzqtys.Location = new System.Drawing.Point(56, 146);
            this.btnMzzqtys.Name = "btnMzzqtys";
            this.btnMzzqtys.Size = new System.Drawing.Size(118, 50);
            this.btnMzzqtys.TabIndex = 2;
            this.btnMzzqtys.Text = "麻醉知情书";
            this.btnMzzqtys.UseVisualStyleBackColor = true;
            this.btnMzzqtys.Click += new System.EventHandler(this.btnMzzqtys_Click);
            // 
            // btnBeforeVisit_YS
            // 
            this.btnBeforeVisit_YS.Location = new System.Drawing.Point(214, 70);
            this.btnBeforeVisit_YS.Name = "btnBeforeVisit_YS";
            this.btnBeforeVisit_YS.Size = new System.Drawing.Size(118, 50);
            this.btnBeforeVisit_YS.TabIndex = 3;
            this.btnBeforeVisit_YS.Text = "术前访视-医生";
            this.btnBeforeVisit_YS.UseVisualStyleBackColor = true;
            this.btnBeforeVisit_YS.Click += new System.EventHandler(this.btnBeforeVisit_YS_Click);
            // 
            // btnAfterVisit_YS
            // 
            this.btnAfterVisit_YS.Location = new System.Drawing.Point(214, 146);
            this.btnAfterVisit_YS.Name = "btnAfterVisit_YS";
            this.btnAfterVisit_YS.Size = new System.Drawing.Size(118, 50);
            this.btnAfterVisit_YS.TabIndex = 4;
            this.btnAfterVisit_YS.Text = "术后随访";
            this.btnAfterVisit_YS.UseVisualStyleBackColor = true;
            this.btnAfterVisit_YS.Click += new System.EventHandler(this.btnAfterVisit_YS_Click);
            // 
            // btnZtzlzqtys
            // 
            this.btnZtzlzqtys.Location = new System.Drawing.Point(368, 146);
            this.btnZtzlzqtys.Name = "btnZtzlzqtys";
            this.btnZtzlzqtys.Size = new System.Drawing.Size(118, 50);
            this.btnZtzlzqtys.TabIndex = 5;
            this.btnZtzlzqtys.Text = "镇痛知情书";
            this.btnZtzlzqtys.UseVisualStyleBackColor = true;
            this.btnZtzlzqtys.Click += new System.EventHandler(this.btnZtzlzqtys_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 50);
            this.button1.TabIndex = 6;
            this.button1.Text = "术后镇痛";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAnesthesiaSummary
            // 
            this.btnAnesthesiaSummary.Location = new System.Drawing.Point(214, 215);
            this.btnAnesthesiaSummary.Name = "btnAnesthesiaSummary";
            this.btnAnesthesiaSummary.Size = new System.Drawing.Size(118, 50);
            this.btnAnesthesiaSummary.TabIndex = 7;
            this.btnAnesthesiaSummary.Text = "麻醉总结";
            this.btnAnesthesiaSummary.UseVisualStyleBackColor = true;
            this.btnAnesthesiaSummary.Click += new System.EventHandler(this.btnAnesthesiaSummary_Click);
            // 
            // btnMzjld
            // 
            this.btnMzjld.Location = new System.Drawing.Point(368, 215);
            this.btnMzjld.Name = "btnMzjld";
            this.btnMzjld.Size = new System.Drawing.Size(118, 50);
            this.btnMzjld.TabIndex = 8;
            this.btnMzjld.Text = "麻醉记录单";
            this.btnMzjld.UseVisualStyleBackColor = true;
            this.btnMzjld.Click += new System.EventHandler(this.btnMzjld_Click);
            // 
            // MigrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 357);
            this.Controls.Add(this.btnMzjld);
            this.Controls.Add(this.btnAnesthesiaSummary);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnZtzlzqtys);
            this.Controls.Add(this.btnAfterVisit_YS);
            this.Controls.Add(this.btnBeforeVisit_YS);
            this.Controls.Add(this.btnMzzqtys);
            this.Controls.Add(this.btnBeforeVisit_HS);
            this.Controls.Add(this.btnPaiban);
            this.Name = "MigrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "同步HIS病人手术申请号";
            this.Load += new System.EventHandler(this.MigrationForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPaiban;
        private System.Windows.Forms.Button btnBeforeVisit_HS;
        private System.Windows.Forms.Button btnMzzqtys;
        private System.Windows.Forms.Button btnBeforeVisit_YS;
        private System.Windows.Forms.Button btnAfterVisit_YS;
        private System.Windows.Forms.Button btnZtzlzqtys;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAnesthesiaSummary;
        private System.Windows.Forms.Button btnMzjld;
    }
}

