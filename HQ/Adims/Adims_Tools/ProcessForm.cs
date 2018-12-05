using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Adims_Tools
{
    public partial class ProcessForm : Form
    {
        BackgroundWorker backgroundWorker;
        public ProcessForm(BackgroundWorker backgroundWorker1, int Maximum)
        {
            InitializeComponent();
            backgroundWorker = backgroundWorker1;
            progressBar1.Maximum = Maximum;
            this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();//执行完之后，直接关闭页面
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.backgroundWorker.CancelAsync();
            this.btnCancel.Enabled = false;
            this.Close();
        }
    }
}
