using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using adims_Utility;

namespace Adims_Tools
{
    public partial class MigrationForm : Form
    {
        adims_DAL.MigrationDal _MigrationDal = new adims_DAL.MigrationDal();
        DataTable dtHisInfo = new DataTable();
        EnumCreator.FlowType _FlowType;
        public MigrationForm()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int i = 0;
            foreach (DataRow dr in dtHisInfo.Rows)
            {
                i++;
                string patid = dr[0].ToString();
                string patZhuyuanId = dr[1].ToString();
                DateTime Odate = Convert.ToDateTime(dr[2].ToString());
                switch (_FlowType)
                {
                    case EnumCreator.FlowType.NotSet:
                        break;
                    case EnumCreator.FlowType.Paiban:
                        _MigrationDal.UpdatePaiban(patid, patZhuyuanId, Odate);
                        break;
                    case EnumCreator.FlowType.OperImplant:
                        break;
                    case EnumCreator.FlowType.Mzzqtys:
                        _MigrationDal.UpdateTable("Adims_MZZQTYS_YS", patid, patZhuyuanId, Odate);
                        break;
                    case EnumCreator.FlowType.BeforeVisit_YS:
                        _MigrationDal.UpdateTable("Adims_BeforeVisit_YS", patid, patZhuyuanId, Odate);
                        break;
                    case EnumCreator.FlowType.BeforeVisit_HS:
                        _MigrationDal.UpdateTable("Adims_BeforeVisit_HS", patid, patZhuyuanId, Odate);
                        break;
                    case EnumCreator.FlowType.Mzjld:
                        _MigrationDal.UpdateMzjldTable("Adims_Mzjld", patid, patZhuyuanId, Odate);
                        break;
                    case EnumCreator.FlowType.QixieQingdian:
                        break;
                    case EnumCreator.FlowType.PACU:
                        break;
                    case EnumCreator.FlowType.NurseRecord:
                        break;
                    case EnumCreator.FlowType.AfterAnalgesia:
                        _MigrationDal.UpdateTable("Adims_AfterAnalgesia", patid, patZhuyuanId, Odate);
                        break;
                    case EnumCreator.FlowType.Lsyz:
                        break;
                    case EnumCreator.FlowType.AfterVisit:
                        _MigrationDal.UpdateTable("Adims_AfterVisit_YS", patid, patZhuyuanId, Odate);
                        break;
                    case EnumCreator.FlowType.AnesthesiaSummary:
                        _MigrationDal.UpdateTable("Adims_AnesthesiaSummary", patid, patZhuyuanId, Odate);
                        break;
                    case EnumCreator.FlowType.TransfusionEvaluation:
                        break;
                    case EnumCreator.FlowType.ZTZLTYS:
                        _MigrationDal.UpdateTable("Adims_ZTZLZQTYS_YS", patid, patZhuyuanId, Odate);
                        break;
                    default:
                        break;
                }
                Thread.Sleep(10);
                worker.ReportProgress(i);
                if (worker.CancellationPending)  // 如果用户取消则跳出处理数据代码 
                {
                    e.Cancel = true;
                    break;
                }
            }

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

        private void btnPaiban_Click(object sender, EventArgs e)
        {
            _FlowType = EnumCreator.FlowType.Paiban;
            DoBackWork(_FlowType);
        }

        private void DoBackWork(EnumCreator.FlowType flowType)
        {
            dtHisInfo = _MigrationDal.GetPaiban();
            int rowCount = dtHisInfo.Rows.Count;

            this.backgroundWorker1.RunWorkerAsync(); // 运行 backgroundWorker 组件
            ProcessForm form = new ProcessForm(this.backgroundWorker1, rowCount);// 显示进度条窗体 
            form.ShowDialog(this);
            form.Close();
        }
        private void DoBackWorkFromPaiban(EnumCreator.FlowType flowType)
        {
            dtHisInfo = _MigrationDal.GetPaiban();
            int rowCount = dtHisInfo.Rows.Count;

            this.backgroundWorker1.RunWorkerAsync(); // 运行 backgroundWorker 组件
            ProcessForm form = new ProcessForm(this.backgroundWorker1, rowCount);// 显示进度条窗体 
            form.ShowDialog(this);
            form.Close();
        }

        private void btnBeforeVisit_YS_Click(object sender, EventArgs e)
        {
            _FlowType = EnumCreator.FlowType.BeforeVisit_YS;
            DoBackWorkFromPaiban(_FlowType);
        }

        private void btnBeforeVisit_HS_Click(object sender, EventArgs e)
        {
            _FlowType = EnumCreator.FlowType.BeforeVisit_HS;
            DoBackWorkFromPaiban(_FlowType);
        }

        private void btnMzzqtys_Click(object sender, EventArgs e)
        {
            _FlowType = EnumCreator.FlowType.Mzzqtys;
            DoBackWorkFromPaiban(_FlowType);
        }

        private void btnAfterVisit_YS_Click(object sender, EventArgs e)
        {
            _FlowType = EnumCreator.FlowType.AfterVisit;
            DoBackWorkFromPaiban(_FlowType);
        }

        private void btnZtzlzqtys_Click(object sender, EventArgs e)
        {
            _FlowType = EnumCreator.FlowType.ZTZLTYS;
            DoBackWorkFromPaiban(_FlowType);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _FlowType = EnumCreator.FlowType.AfterAnalgesia;
            DoBackWorkFromPaiban(_FlowType);
        }

        private void btnAnesthesiaSummary_Click(object sender, EventArgs e)
        {
            _FlowType = EnumCreator.FlowType.AnesthesiaSummary;
            DoBackWorkFromPaiban(_FlowType);
        }



        private void btnMzjld_Click(object sender, EventArgs e)
        {
            _FlowType = EnumCreator.FlowType.Mzjld;
            DoBackWorkFromPaiban(_FlowType);
        }

        private void MigrationForm_Load(object sender, EventArgs e)
        {
            //    string sql = @" EXEC  sp_rename 'Adims_BeforeShfs_YS' ,'Adims_AfterVisit_YS' ;
            //                    EXEC  sp_rename 'Adims_Mzzj_CJ' ,'Adims_AnesthesiaSummary' ;
            //                    EXEC  sp_rename 'Adims_SHZT_YS' ,'Adims_AfterAnalgesia' ;
            //                    EXEC  sp_rename 'Adims_SHZTGCZB_YS' ,'Adims_AfterAnalgesiaDetail' ;
            //                    EXEC  sp_rename 'Adims_OTypesetting' ,'Adims_OperSchedule' ;
            //                    EXEC  sp_rename 'Adims_SSSZR' ,'Adims_OperImplant' ;
            //                    EXEC  sp_rename 'Adims_AfterVisit_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_AfterVisit_YS.PayTime' ,'Odate' , 'COLUMN';
            //                    EXEC  sp_rename 'Adims_BeforeVisit_HS.ZYNumber' ,'PatId' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_BeforeVisit_YS.PayTime' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_BeforeVisit_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_MZZQTYS_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_AfterAnalgesia.ZYNumber' ,'PatId' , 'COLUMN'; 
            //                    EXEC  sp_rename 'Adims_ZTZLZQTYS_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_OperImplant.ZYNumber' ,'PatId' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_BeforeVisit_HS.sstiem' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_AnesthesiaSummary.Paytime' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_MZZQTYS_YS.Paytime' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_NurseRecord_HQ.Paytime' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_PACU.Paytime' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_SHZT_YS.Paytime' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_OperImplant.Paytime' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_AfterVisit_YS.Mzjld' ,'MzjldID' , 'COLUMN'; 
            //                    EXEC  sp_rename 'Adims_AfterAnalgesiaDetail.Paytime' ,'VisitTime' , 'COLUMN' 
            //                    EXEC  sp_rename 'Adims_ZTZLZQTYS_YS.Paytime' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_AfterAnalgesia.Paytime' ,'Odate' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_AfterAnalgesia.mzjld' ,'MzjldID' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_AfterAnalgesiaDetail.mzjld' ,'MzjldID' , 'COLUMN' ;
            //                    EXEC  sp_rename 'Adims_AfterVisit_YS.mzjid' ,'MzjldID', 'COLUMN' ;";
            //    try
            //    {
            //        _MigrationDal.UpdateTable(sql);
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
        }
    }
}
