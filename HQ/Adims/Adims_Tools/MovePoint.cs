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
                i++;
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

        private void button1_Click(object sender, EventArgs e)
        {
            listPoint = Orm.GetAdims_mzjld_Point();
            listRecord = Orm.GetAdims_MonitorRecord();
            backgroundWorker2.RunWorkerAsync(); // 运行 backgroundWorker 组件
            ProcessForm form = new ProcessForm(backgroundWorker2, listRecord.Count);// 显示进度条窗体 
            form.ShowDialog(this);
            form.Close();
            tbCount.Text = realCount.ToString();
        }
        int realCount = 0;
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int i = 0;
            realCount = 0;
            foreach (var item in listRecord)
            {
                if (listPoint.FirstOrDefault(a => a.Mzjldid == item.Mzjldid && a.RecordTime == item.RecordTime) == null)
                {
                    var mzjld = Orm.GetAdims_mzjld(item.Mzjldid.Value);
                    TimeSpan ts = item.RecordTime.Value - mzjld.Otime.Value.ToString("yyyy-MM-dd HH:mm:00").ToDateTime();
                    if (ts.TotalMinutes / mzjld.Jcsjjg.Value == 0)
                    {
                        var res = MovePointDal.InsertAdims_mzjld_Point(item);
                        realCount++;
                    }


                }
                i++;
                Thread.Sleep(5);
                worker.ReportProgress(i);
                if (worker.CancellationPending)  // 如果用户取消则跳出处理数据代码 
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            var res = MovePointDal.UpdateAdims_OperSchedule();
            if (res > 0)
            {
                MessageBox.Show($"共  {res} 条 修改状态成功！");
            }
        }
    }
}
