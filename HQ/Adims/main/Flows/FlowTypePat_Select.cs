using adims_DAL.Flows;
using adims_Utility;
using System;
using System.Data;
using System.Windows.Forms;

namespace main
{
    public partial class FlowTypePat_Select : Form
    {
        MzjldDal _MzjldDal = new MzjldDal();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        EnumCreator.FlowType _FlowType;
        public FlowTypePat_Select(EnumCreator.FlowType flowType)
        {
            _FlowType = flowType;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DataBind()
        {
            try
            {


                DataTable dt = new DataTable();
                string dtDate = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
                switch (_FlowType)
                {
                    case EnumCreator.FlowType.NotSet:
                        break;
                    case EnumCreator.FlowType.Paiban:
                        dt = _PaibanDal.GetPaibanByOdate(dtDate);
                        break;
                    case EnumCreator.FlowType.OperImplant:
                        dt = _PaibanDal.GetPaibanByOdate(dtDate);
                        break;
                    case EnumCreator.FlowType.Mzzqtys:
                        dt = _PaibanDal.GetPaibanByOdate(dtDate);
                        break;
                    case EnumCreator.FlowType.BeforeVisit_YS:
                        dt = _PaibanDal.GetPaibanByOdate(dtDate);
                        break;
                    case EnumCreator.FlowType.BeforeVisit_HS:
                        dt = _PaibanDal.GetPaibanByOdate(dtDate);
                        break;
                    case EnumCreator.FlowType.PACU:
                        dt = _MzjldDal.GetMzjldByOtime(dtDate);
                        break;
                    case EnumCreator.FlowType.QixieQingdian:
                        dt = _MzjldDal.GetMzjldByOtime(dtDate);
                        break;
                    case EnumCreator.FlowType.NurseRecord:
                        dt = _MzjldDal.GetMzjldByOtime(dtDate);
                        break;
                    case EnumCreator.FlowType.AfterAnalgesia:
                        dt = _MzjldDal.GetMzjldByOtime(dtDate);
                        break;
                    case EnumCreator.FlowType.AnesthesiaSummary:
                        dt = _MzjldDal.GetMzjldByOtime(dtDate);
                        break;
                    case EnumCreator.FlowType.Lsyz:
                        dt = _MzjldDal.GetMzjldByOtime(dtDate);
                        break;
                    case EnumCreator.FlowType.AfterVisit:
                        dt = _MzjldDal.GetMzjldByOtime(dtDate);
                        break;
                    case EnumCreator.FlowType.TransfusionEvaluation:
                        dt = _MzjldDal.GetMzjldByOtime(dtDate);
                        break;
                    default:
                        break;

                }

                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
        }

        private void BeforeVisit_Select_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells["病人编号"].Value.ToString();
                string mzjldId = "";
                if ((int)_FlowType>6)
                {
                    if (dataGridView1.CurrentRow.Cells["麻醉编号"].Value != null)
                    {
                        mzjldId = dataGridView1.CurrentRow.Cells["麻醉编号"].Value.ToString();
                    }         
                }
                string patZhuyuanID = dataGridView1.CurrentRow.Cells["住院号"].Value.ToString();
                string date = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
                switch (_FlowType)
                {
                    case EnumCreator.FlowType.NotSet:
                        break;
                    case EnumCreator.FlowType.Paiban:
                        break;
                    case EnumCreator.FlowType.OperImplant:
                        OperImplant SSSZR = new OperImplant(patID, date);
                        SSSZR.Show();
                        break;
                    case EnumCreator.FlowType.Mzzqtys:
                        MZZQTYS MZZQTYS = new MZZQTYS(patID, date);
                        MZZQTYS.Show();
                        break;
                    case EnumCreator.FlowType.BeforeVisit_YS:
                        BeforeVisit_HQ BeforeVisit_YS = new BeforeVisit_HQ(patID, date);
                        BeforeVisit_YS.Show();
                        break;
                    case EnumCreator.FlowType.BeforeVisit_HS:
                        BeforeVisit_HS_HQ BeforeVisit_HS = new BeforeVisit_HS_HQ(patID, date);
                        BeforeVisit_HS.Show();
                        break;
                    case EnumCreator.FlowType.Mzjld:
                        break;
                    case EnumCreator.FlowType.QixieQingdian:                        
                        break;
                    case EnumCreator.FlowType.PACU:
                        PACU_HQ pacu = new PACU_HQ(patID, mzjldId, date);
                        pacu.ShowDialog();
                        break;
                    case EnumCreator.FlowType.NurseRecord:
                        NurseRecord_HQ NurseRecord_HQ = new NurseRecord_HQ(mzjldId, patID, date);
                        NurseRecord_HQ.Show();
                        break;
                    case EnumCreator.FlowType.AfterAnalgesia:
                        AfterAnalgesia mzzt = new AfterAnalgesia(mzjldId, patID, date);
                        mzzt.Show();
                        break;
                    case EnumCreator.FlowType.AnesthesiaSummary:
                        AnesthesiaSummary mzzj = new AnesthesiaSummary(mzjldId, patID, date);
                        mzzj.Show();
                        break;
                    case EnumCreator.FlowType.Lsyz:
                        LsyzForm lsyz = new LsyzForm(mzjldId, patID);
                        lsyz.Show();
                        break;
                    case EnumCreator.FlowType.TransfusionEvaluation:
                        ShuXuePG sxpg = new ShuXuePG(mzjldId, patID);
                        sxpg.Show();
                        break;
                    default:
                        break;
                }
               
            }
            else MessageBox.Show("请选择病人！");
        }
       
    }
}
