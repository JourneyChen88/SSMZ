using adims_DAL;
using adims_MODEL;
using adims_Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Adims_Tools
{
    public partial class MovePoint : Form
    {
        MovePointDal MovePointDal = new MovePointDal();
        public MovePoint()
        {
            InitializeComponent();
        }
        
        private void MovePoint_Load(object sender, EventArgs e)
        {
            DataTable dt1 = MovePointDal.GetAdims_mzjld_PointCount();
            tbPoint.Text = dt1.Rows[0][0].ToString();

            DataTable dt2 = MovePointDal.GetAdims_MonitorRecordCount();
            tbRecord.Text = dt2.Rows[0][0].ToString();


        }

        List<Mzjld_Point> listPoint = new List<Mzjld_Point>();
        List<MonitorRecord> listRecord = new List<MonitorRecord>();
   
        private void btnPaiban_Click(object sender, EventArgs e)
        {             
            listPoint = Orm.GetAdims_mzjld_Point();
            listRecord = Orm.GetAdims_MonitorRecord();
            backgroundWorker1.RunWorkerAsync(); // 运行 backgroundWorker 组件
            ProcessForm form = new ProcessForm(backgroundWorker1, listPoint.Count);// 显示进度条窗体 
            form.ShowDialog(this);
            form.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int i = 0;
            foreach (var item in listPoint)
            {
                if (listRecord.FirstOrDefault(a => a.Mzjldid == item.Mzjldid && a.RecordTime == item.RecordTime) == null)
                {
                    var res = MovePointDal.InsertAdims_MonitorRecord(item);
                  
                }
                i ++;
                Thread.Sleep(5);
                worker.ReportProgress(i);
                if (worker.CancellationPending)  // 如果用户取消则跳出处理数据代码 
                {
                    e.Cancel = true;
                    break;
                }
            }
          //  tbCount.Text = i.ToString();

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
            }
            else
            {
            }
        }
    }
}
