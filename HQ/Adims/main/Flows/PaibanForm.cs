///************************************
///Updated By        : Senvi
///************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using adims_BLL;
using adims_DAL.Dics;
using adims_Utility;
using adims_DAL.Flows;
//using CrystalDecisions.Shared;

namespace main
{
    public partial class PaibanForm : Form
    {
        #region <<Members>>
        adims_DAL.HisDB_Help HisHelp = new adims_DAL.HisDB_Help();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        DataDicDal _DataDicDal = new DataDicDal();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        adims_BLL.AdimsController BLL = new adims_BLL.AdimsController();
        string ssj = "全部手术间";
        int isBool =2;//0表示未排班，1表示已排班,2表示全部
       
        DataTable dtRoom = new DataTable();

        DataTable dtSurgeryStaff = new DataTable();
        DataTable dtPaiban = new DataTable();
        private DateTimePicker dtKStime = new DateTimePicker();
        private ComboBox cmbRoom = new ComboBox();
        private ComboBox cmbXHHS = new ComboBox();
        private ComboBox cmbMZYS = new ComboBox();
        private ComboBox cmbMZFF = new ComboBox();
        private ComboBox cmbSF = new ComboBox();
        private CheckBox checkbox = new CheckBox();

        public String printName = String.Empty;
        public Font prtTextFont = new Font("Verdana", 10);
        public Font prtTitleFont = new Font("宋体", 10);
        private String[] titles = new String[0];
        public String[] Titles
        {
            set
            {
                titles = value as String[];
                if (null == titles)
                {
                    titles = new String[0];
                }
            }
            get
            {
                return titles;
            }
        }

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public PaibanForm()
        {
            InitializeComponent();
            dtDataTime.Value = DateTime.Now;
        }

        #endregion

        #region <<Events>>
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paiban_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.dtDataTime.Value = DateTime.Now.AddDays(1);
            try
            {
                btnisJZ.Visible = false;//隐藏急诊打印按钮
                List<adims_MODEL.oroomstate> rs = new List<adims_MODEL.oroomstate>();
                dgvOTypesetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                BindPaibanInfo();//绑定列表
                dtRoom = _DataDicDal.GetOroom();
                BindOroomList(dtRoom);
                BindOroom(dtRoom);
                DataTable dtMZFF = _DataDicDal.GetMazuiFangfaAll();
                BindMZFF(dtMZFF);
                DataTable dtMZYS = _DataDicDal.GetAllMZYS();
                BindMZYS(dtMZYS);
                DataTable dtXHHS = _DataDicDal.GetAll_hushi();
                BindXHHS(dtXHHS);
                DataTable dtSF = BLL.GetSF();
                BindSF(dtSF);
                cmbRoom.Visible = false;
                cmbXHHS.Visible = false;
                cmbMZYS.Visible = false;
                dtKStime.Visible = false;
                cmbMZFF.Visible = false;
                cmbSF.Visible = false;
                cmbRoom.SelectedIndexChanged += new EventHandler(cmbRoom_SelectedIndexChanged);               
                cmbMZYS.SelectedIndexChanged += new EventHandler(cmbMZYS_SelectedIndexChanged);
                cmbXHHS.SelectedIndexChanged += new EventHandler(cmbXHHS_SelectedIndexChanged);
                cmbMZFF.SelectedIndexChanged += new EventHandler(cmbMZFF_SelectedIndexChanged);
                cmbSF.SelectedIndexChanged += new EventHandler(cmbSF_SelectedIndexChanged);
                dtKStime.ValueChanged += new EventHandler(dtKStime_ValueChanged);
                dgvOTypesetting.Controls.Add(cmbRoom);
                dgvOTypesetting.Controls.Add(cmbMZYS);
                dgvOTypesetting.Controls.Add(dtKStime);
                dgvOTypesetting.Controls.Add(cmbXHHS);
                dgvOTypesetting.Controls.Add(cmbMZFF);
                dgvOTypesetting.Controls.Add(cmbSF);
                dgvOTypesetting.CellValueChanged += 
                    new DataGridViewCellEventHandler(dgvOTypesetting_CellValueChanged);

                var cell = this.dgvOTypesetting.GetCellDisplayRectangle(0, -1, true);
                checkbox = new CheckBox { Left = cell.Left +5, Top = cell.Top + 5, Width = 15, Height = 15 };
                this.dgvOTypesetting.Controls.Add(checkbox);

                this.checkbox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            }
            catch (Exception ex)
            {
               Logger.WriteErrorLog(ex);
                throw ex;
            }
        }
        /// <summary>
        /// check事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox.Checked == true)
            {
                for (int i = 0; i < dgvOTypesetting.Rows.Count; i++)
                {
                    dgvOTypesetting.Rows[i].Cells["Column1"].Value = true;
                }
            }
            else if (checkbox.Checked == false)
            {
                for (int i = 0; i < dgvOTypesetting.Rows.Count; i++)
                {
                    dgvOTypesetting.Rows[i].Cells["Column1"].Value = false;
                }
            }
        }
        private void dgvOTypesetting_CellValueChanged(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                if (dgvOTypesetting.CurrentCell.ColumnIndex == 0)
                {
                }
                else
                {
                    string id = dgvOTypesetting.CurrentRow.Cells["id"].Value.ToString();
                    if (id != "")
                    {
                        string RoomName = dgvOTypesetting.Columns[dgvOTypesetting.CurrentCell.ColumnIndex].Name;
                        string Value_Info = dgvOTypesetting.CurrentCell.Value.ToString();
                        string Odates = Convert.ToDateTime(dtDataTime.Text).ToString("yyyy-MM-dd");
                        if (dgvOTypesetting.CurrentRow.Cells["oroom"].Value.ToString() != "")
                        {
                            _PaibanDal.UpdatePaiban(id, RoomName, Value_Info, "1", Odates);
                        }
                        else
                        {
                            _PaibanDal.UpdatePaiban(id, RoomName, Value_Info, "0", Odates);
                        }
                    }
                }
            }
        }

        private void BindPaibanInfo()
        {
            dtPaiban = this._PaibanDal.GetByRoomAndOdate(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = dtPaiban.DefaultView;
            isBool = 2;
           
        }

        private void HisDataBind()
        {
            try
            {
                DataTable dt1 = HisHelp.GetHisInfo(dtDataTime.Value.ToString("yyyy-MM-dd"));
                List<string> HISinfo = new List<string>();
                int result = 0;
                foreach (DataRow dr in dt1.Rows)
                {
                    HISinfo.Clear();
                    HISinfo.Add(dr["patid"].ToString());//0
                    HISinfo.Add(dr["ZhuYuanNo"].ToString());//1
                    HISinfo.Add(dr["CardID"].ToString());
                    HISinfo.Add(dr["patName"].ToString());
                    HISinfo.Add(dr["patAge"].ToString());
                    HISinfo.Add(dr["patSex"].ToString());
                    HISinfo.Add(dr["patNation"].ToString());
                    HISinfo.Add(dr["BedNo"].ToString());
                    HISinfo.Add(dr["Patdpm"].ToString());
                    HISinfo.Add(dr["Pattmd"].ToString());
                    HISinfo.Add(dr["PatHeight"].ToString());
                    HISinfo.Add(dr["PatWeight"].ToString());
                    HISinfo.Add(dr["PatBloodType"].ToString());
                    HISinfo.Add(dr["Oname"].ToString());
                    HISinfo.Add(Convert.ToDateTime(dr["Odate"]).ToString("yyyy-MM-dd HH:mm"));
                    HISinfo.Add(dr["OS1"].ToString());
                    HISinfo.Add(dr["OS2"].ToString());
                    HISinfo.Add(dr["OS3"].ToString());
                    HISinfo.Add(dr["OS4"].ToString());
                    HISinfo.Add(dr["OS5"].ToString());
                    HISinfo.Add(dr["Amethod"].ToString());
                    HISinfo.Add(dr["BX"].ToString());
                    HISinfo.Add(dr["Tiwei"].ToString());
                    HISinfo.Add(dr["GR"].ToString());
                    HISinfo.Add(dr["Remarks"].ToString());
                   
                    HISinfo.Add(Convert.ToDateTime(dr["Odate"]).ToString("HH:mm"));
                    HISinfo.Add("0");//未排班
                    if (dr["SSLB"].ToString() == "" || dr["SSLB"].ToString() == "0")
                        HISinfo.Add("0");
                    else HISinfo.Add(dr["SSLB"].ToString());//是否急诊                    
                    HISinfo.Add(dr["Ocode"].ToString());//手术编码
                    HISinfo.Add(dr["SSDJ"].ToString()); //手术类型
                    HISinfo.Add(dr["SQSJ"].ToString());//申请时间
                    HISinfo.Add(dr["ms"].ToString());
                    DataTable dt = _PaibanDal.GetPaibanByPatId(dr["patid"].ToString());
                    if (dt.Rows.Count == 0)
                        result = _PaibanDal.InsertPaiban(HISinfo);
                    //else
                    //    result = _PaibanDal.UpdatePaiban(HISinfo);

                }
                BindPaibanInfo();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString() + " 出错");
            }

        }
        private void HisDataBind_LocalTest()
        {
            try
            {
                DataTable dt1 = dal.GetHisInfo_LocalTest(dtDataTime.Value.ToString("yyyy-MM-dd"));
                List<string> HISinfo = new List<string>();
                int result = 0;
                foreach (DataRow dr in dt1.Rows)
                {
                    HISinfo.Clear();
                    HISinfo.Add(dr["patid"].ToString());//0
                    HISinfo.Add(dr["ZhuYuanNo"].ToString());//1
                    HISinfo.Add(dr["CardID"].ToString());
                    HISinfo.Add(dr["patName"].ToString());
                    HISinfo.Add(dr["patAge"].ToString());
                    HISinfo.Add(dr["patSex"].ToString());
                    HISinfo.Add(dr["patNation"].ToString());
                    HISinfo.Add(dr["BedNo"].ToString());
                    HISinfo.Add(dr["Patdpm"].ToString());
                    HISinfo.Add(dr["Pattmd"].ToString());
                    HISinfo.Add(dr["PatHeight"].ToString());
                    HISinfo.Add(dr["PatWeight"].ToString());
                    HISinfo.Add(dr["PatBloodType"].ToString());
                    HISinfo.Add(dr["Oname"].ToString());
                    HISinfo.Add(Convert.ToDateTime(dr["Odate"]).ToString("yyyy-MM-dd HH:mm"));
                    HISinfo.Add(dr["OS1"].ToString());
                    HISinfo.Add(dr["OS2"].ToString());
                    HISinfo.Add(dr["OS3"].ToString());
                    HISinfo.Add(dr["OS4"].ToString());
                    HISinfo.Add(dr["OS5"].ToString());
                    HISinfo.Add(dr["Amethod"].ToString());
                    HISinfo.Add(dr["BX"].ToString());
                    HISinfo.Add(dr["Tiwei"].ToString());
                    HISinfo.Add(dr["GR"].ToString());
                    HISinfo.Add(dr["Remarks"].ToString());

                    HISinfo.Add(Convert.ToDateTime(dr["Odate"]).ToString("HH:mm"));
                    HISinfo.Add("0");//未排班
                    if (dr["SSLB"].ToString() == "" || dr["SSLB"].ToString() == "0")
                        HISinfo.Add("0");
                    else HISinfo.Add(dr["SSLB"].ToString());//是否急诊                    
                    HISinfo.Add(dr["Ocode"].ToString());//手术编码
                    HISinfo.Add(dr["SSDJ"].ToString()); //手术类型
                    HISinfo.Add(dr["SQSJ"].ToString());//申请时间
                    HISinfo.Add(dr["ms"].ToString());
                    DataTable dt = _PaibanDal.GetPaibanByPatId(dr["patid"].ToString());
                    if (dt.Rows.Count == 0)
                        result = _PaibanDal.InsertPaiban(HISinfo);
                    //else
                    //    result = _PaibanDal.UpdatePaiban(HISinfo);

                }
                BindPaibanInfo();

            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString() + " 出错");
            }

        }
        private void BindOroomList(DataTable dt1)
        {
            listboxRoom.Items.Clear();
            listboxRoom.Items.Add("全部手术间");
            dt1 = _DataDicDal.GetOroom();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.listboxRoom.Items.Add(dt1.Rows[i][0]);
            }
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvOTypesetting.CurrentCell.Value = cmbRoom.SelectedItem.ToString();
            cmbRoom.Visible = false;
        }

        private void cmbSF_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvOTypesetting.CurrentCell.Value = cmbSF.SelectedItem.ToString();
            cmbSF.Visible = false;
        }
        private void cmbMZFF_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvOTypesetting.CurrentCell.Value = cmbMZFF.SelectedItem.ToString();
            cmbMZFF.Visible = false;
        }
        private void cmbXHHS_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvOTypesetting.CurrentCell.Value = cmbXHHS.SelectedItem.ToString();
            cmbXHHS.Visible = false;
        }

        private void cmbMZYS_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvOTypesetting.CurrentCell.Value = cmbMZYS.SelectedItem.ToString();
            cmbMZYS.Visible = false;
        }

        private void dtKStime_ValueChanged(object sender, EventArgs e)
        {
            dgvOTypesetting.CurrentCell.Value = dtKStime.Value.ToString("yyyy/MM/dd HH:mm");
            dtKStime.Visible = false;
        }

        private void BindOroom(DataTable dt1)
        {
            cmbRoom.Items.Clear();           
            for (int i = 0; i < dt1.Rows.Count; i++)
            {                
                this.cmbRoom.Items.Add(dt1.Rows[i][0]);
            }
           this.cmbRoom.Items.Add("");
        }
        private void BindMZYS(DataTable dt1)
        {
            cmbMZYS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbMZYS.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbMZYS.Items.Add("");
        }
        private void BindXHHS(DataTable dt1)
        {
            cmbXHHS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbXHHS.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbXHHS.Items.Add("");
        }     
        private void BindSF(DataTable dt1)
        {
            cmbSF.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbSF.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbSF.Items.Add("");
        }
        /// <summary>
        /// 绑定麻醉方式
        /// </summary>
        /// <param name="dt1"></param>
        private void BindMZFF(DataTable dt1)
        {
            cmbMZFF.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbMZFF.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbMZFF.Items.Add("");
        }
        private void b_Click(object sender, EventArgs e)
        {
            //ssj = ((Button)sender).Text;
            //DataTable ds = new DataTable();
            //ds = _PaibanDal.GetOTypesetting(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            //dgvOTypesetting.DataSource = ds.DefaultView;
        }

        /// <summary>
        /// 时间改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtDataTime_ValueChanged(object sender, EventArgs e)
        {
            BindPaibanInfo();
        }


        /// <summary>
        /// 点击任意部分触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void EnterMZD_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                string Oroom = dgvOTypesetting.CurrentRow.Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(Oroom))
                {
                    MessageBox.Show("手术间号不能为空。");
                    return;
                }
                string PatID = dgvOTypesetting.CurrentRow.Cells["id"].Value.ToString();
                if (string.IsNullOrEmpty(PatID))
                {
                    MessageBox.Show("住院号不能为空。");
                    return;
                }
                int mzjldID = 0;
                string Odate = dtDataTime.Value.ToString("yyyy-MM-dd");
                DataTable dt = new MzjldDal().GetMzjldByPatId(PatID);
                if (dt.Rows.Count > 0)
                    mzjldID = Convert.ToInt32(dt.Rows[0][0]);
                MzjldEdit F1 = new MzjldEdit(PatID, Oroom, DateTime.Parse(Odate), mzjldID);
                F1.ShowDialog();                
            }
        }

        private void dgvOTypesetting_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                lbPatName.Text = dgvOTypesetting.CurrentRow.Cells["patname"].Value.ToString();
                lbKeshi.Text = dgvOTypesetting.CurrentRow.Cells["patdpm"].Value.ToString();
                lbPatZhuYuanID.Text = dgvOTypesetting.CurrentRow.Cells["PatZhuYuanID"].Value.ToString();
                lbSSMC.Text = dgvOTypesetting.CurrentRow.Cells["oname"].Value.ToString();
                lbAmethod.Text = dgvOTypesetting.CurrentRow.Cells["Amethod"].Value.ToString();
                lbSSmzfs.Text = dgvOTypesetting.CurrentRow.Cells["SSmzfs"].Value.ToString();             

                int ColIndex = dgvOTypesetting.CurrentCell.ColumnIndex;
                if (ColIndex == 1)
                {
                    cmbSF.Visible = false;
                    cmbXHHS.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZYS.Visible = false;
                    cmbMZFF.Visible = false;
                    Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
                    cmbRoom.Left = rect.Left;
                    cmbRoom.Top = rect.Top;
                    cmbRoom.Width = rect.Width;
                    cmbRoom.Height = rect.Height;
                    cmbRoom.Visible = true;
                    cmbRoom.Text = "";
                    cmbRoom.DroppedDown = true;
                }
                if (ColIndex ==14)
                {
                    cmbSF.Visible = false;
                    cmbXHHS.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZYS.Visible = false;
                    Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
                    cmbMZFF.Left = rect.Left;
                    cmbMZFF.Top = rect.Top;
                    cmbMZFF.Width = rect.Width;
                    cmbMZFF.Height = rect.Height;
                    cmbMZFF.Visible = true;
                    cmbMZFF.Text = "";
                    cmbMZFF.DroppedDown = true;
                }
                else if (ColIndex == 18 || ColIndex == 19 || ColIndex == 20 || ColIndex == 21)
                {
                    cmbSF.Visible = false;
                    cmbMZFF.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZYS.Visible = false;
                    Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
                    cmbXHHS.Left = rect.Left;
                    cmbXHHS.Top = rect.Top;
                    cmbXHHS.Width = rect.Width;
                    cmbXHHS.Height = rect.Height;
                    cmbXHHS.Visible = true;
                    cmbXHHS.Text = "";
                    cmbXHHS.DroppedDown = true;
                }
                else if (ColIndex == 22 || ColIndex == 23 || ColIndex == 24)
                {
                    cmbSF.Visible = false;
                    cmbXHHS.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZFF.Visible = false;
                    Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
                    cmbMZYS.Left = rect.Left;
                    cmbMZYS.Top = rect.Top;
                    cmbMZYS.Width = rect.Width;
                    cmbMZYS.Height = rect.Height;
                    cmbMZYS.Visible = true;
                    cmbMZYS.Text = "";
                    cmbMZYS.DroppedDown = true;
                }
                else if (ColIndex == 29)
                {
                    cmbMZYS.Visible = false;
                    cmbXHHS.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZFF.Visible = false;
                    Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
                    cmbSF.Left = rect.Left;
                    cmbSF.Top = rect.Top;
                    cmbSF.Width = rect.Width;
                    cmbSF.Height = rect.Height;
                    cmbSF.Visible = true;
                    cmbSF.Text = "";
                    cmbSF.DroppedDown = true;
                }
                else
                {
                    cmbSF.Visible = false;
                    cmbXHHS.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZYS.Visible = false;
                    cmbMZFF.Visible = false;
                }
            }
        }
        

        #region  智能排班
        /// <summary>
        /// 智能排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public struct time_object
        {
            public DateTime start;
            public DateTime end;
            public int time_ID;
            public int person_ID;

        }
        public struct person_object
        {
            public int count;   //已做手术例数
            public double waitFree;
            public int ID;
        }

        public void order(time_object[] times, int time_length)
        {
            int i, j, k;
            time_object temp;
            for (i = 1; i < time_length; i++)
            {
                for (j = i - 1; j >= 0; j--)
                {
                    if (times[j].start < times[i].start)
                    {
                        temp = times[i];
                        for (k = i - 1; k > j; k--)
                            times[k + 1] = times[k];
                        times[j + 1] = temp;
                        break;
                    }
                }
                if (j == -1)
                {
                    temp = times[i];
                    for (k = i - 1; k >= 0; k--)
                    { times[k + 1] = times[k]; }
                    times[0] = temp;

                }
            }
        }
        public void order(int[] data, int length)
        {
            int i, j, k;
            int temp;
            for (i = 1; i < length; i++)
            {
                for (j = i - 1; j >= 0; j--)
                {
                    if (data[j] < data[i])
                    {
                        temp = data[i];
                        for (k = i - 1; k > j; k--)
                            data[k + 1] = data[k];
                        data[j + 1] = temp;
                        break;
                    }
                }
                if (j == -1)
                {
                    temp = data[i];
                    for (k = i - 1; k >= 0; k--)
                    { data[k + 1] = data[k]; }
                    data[0] = temp;

                }
            }

        }
        public void order(person_object[] persons, int person_count)
        {
            int i, j, k;
            person_object temp;
            for (i = 1; i < person_count; i++)
            {
                for (j = i - 1; j >= 0; j--)
                {
                    if (persons[j].count < persons[i].count)
                    {
                        temp = persons[i];
                        for (k = i - 1; k > j; k--)
                            persons[k + 1] = persons[k];
                        persons[j + 1] = temp;
                        break;
                    }
                }
                if (j == -1)
                {
                    temp = persons[i];
                    for (k = i - 1; k >= 0; k--)
                    { persons[k + 1] = persons[k]; }
                    persons[0] = temp;

                }
            }
        }

        public bool arrange(time_object[] times, int time_length, int person_count, double mini_gap)
        {
            person_object[] persons = new person_object[person_count];
            order(times, time_length);
            int i = 0, j = 0;
            for (i = 0; i < person_count; i++)
            {
                persons[i].count = 0;
                persons[i].waitFree = 0;
                persons[i].ID = i;
            }
            DateTime comtemprary = times[0].start;  //当前时间
            times[0].person_ID = persons[0].ID;
            persons[0].count++;
            persons[0].waitFree = (times[0].end - times[0].start).TotalHours + mini_gap;
            for (i = 1; i < time_length; i++)
            {
                order(persons, person_count);
                for (j = 0; j < person_count; j++)
                {
                    persons[j].waitFree -= (times[i].start - comtemprary).TotalHours;
                    if (persons[j].waitFree < 0) persons[j].waitFree = 0;
                }
                for (j = 0; j < person_count; j++)
                {
                    if (persons[j].waitFree == 0)
                    {
                        times[i].person_ID = persons[j].ID;
                        persons[j].count++;
                        persons[j].waitFree = (times[i].end - times[i].start).TotalHours + mini_gap;
                        comtemprary = times[i].start;
                        break;
                    }
                }
                if (j == person_count) return false;
            }
            return true;
        }

        private void btnIntelligent_Click(object sender, EventArgs e)
        {
            int i = 0;
            int time_count = this.dtPaiban.Rows.Count;
            time_object[] times = new time_object[time_count];
            for (i = 0; i < time_count; i++)
            {
                times[i].start = (DateTime)(dtPaiban.Rows[i][19]);
                times[i].end = (DateTime)(dtPaiban.Rows[i][20]);
                times[i].time_ID = i;
                times[i].person_ID = -1;
            }
            DataTable zhuma = new DataTable();
            zhuma = bll.select_staff(0);
            int zhuma_count = zhuma.Rows.Count;
            bool result = arrange(times, time_count, zhuma_count, 0);
            if (result)
            {
                // for (i = 0; i < time_count; i++)
                //  dgvOTypesetting.Rows[i].Cells[12].Value = zhuma.Rows[times[i].person_ID][2];
                object[] doctors = new object[time_count];
                for (i = 0; i < time_count; i++)
                    doctors[i] = zhuma.Rows[times[i].person_ID][2];
                bll.UpdateOTypesetting(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"), doctors);
                ssj = "全部手术间";
                dtPaiban = _PaibanDal.GetByRoomAndOdate(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
                dgvOTypesetting.DataSource = dtPaiban.DefaultView;
                MessageBox.Show("智能排班成功");
            }
            else
                MessageBox.Show("医师人数不够，智能排班失败");
        }
        #endregion
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {

        }
        private void btnPrintResult_Click(object sender, EventArgs e)
        {
            PaibanResult f1 = new PaibanResult();
            f1.ShowDialog();
        }
        private void listboxRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            bll.clearpaiban(dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            ssj = listboxRoom.SelectedItem.ToString();
            dtPaiban = _PaibanDal.GetByRoomAndOdate(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = dtPaiban.DefaultView;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //HisDataBind_LocalTest();
            string IPaddress = "192.168.1.8";
            bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            if (flag == true) HisDataBind();
            else MessageBox.Show("HIS数据库未连接，请检查网络");
            groupBox2.Visible = true;
            //btnisJZ.Visible = false;
            //btnPrint.Enabled = true;

        }

        private void btnYES_Click(object sender, EventArgs e)
        {
            dtPaiban = this._PaibanDal.GetIsPaiban(1, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = dtPaiban.DefaultView;
            isBool = 1;
            groupBox2.Visible = true;
            //btnisJZ.Visible = false;
            //btnPrint.Enabled = true;
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            dtPaiban = _PaibanDal.GetIsPaiban(0, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = dtPaiban.DefaultView;
            isBool = 0;
            groupBox2.Visible = true;
            //btnisJZ.Visible = false;
            //btnPrint.Enabled = true;
        }


        #endregion

        private void dgvOTypesetting_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }          
        }

        private void dgvOTypesetting_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                //if (dgvOTypesetting.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
                //{
                //    //dgvOTypesetting.Rows[e.RowIndex].ErrorText = "";
                //    //int newInteger = 0;
                //    //if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0)
                //    //{
                //    //    e.Cancel = true;
                //    //    dgvOTypesetting.Rows[e.RowIndex].ErrorText = "台数格式错误，请重新输入。";
                //    //    MessageBox.Show("台数格式错误，请重新输入。");
                //    //    return;
                //    //}
                //    if (true)
                //    {      
                //}            
                //    }   
            }
            if (e.ColumnIndex == 3)
            {
                try
                {
                    if (!String.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        DateTime val = DateTime.Parse(e.FormattedValue.ToString());
                    }
                }
                catch (Exception)
                {
                    dgvOTypesetting.Rows[e.RowIndex].ErrorText = "必须时间格式（HH:mm）";
                    MessageBox.Show("请输入正确的时间格式（HH:mm）");
                    e.Cancel = true;
                    return;
                }
            }

        }
        /// <summary>
        /// 查看急症手术
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnE_Click(object sender, EventArgs e)
        {
            //PAIBAN_SG F1 = new PAIBAN_SG();
            //F1.ShowDialog();
            //BindPaibanInfo();
            dtPaiban = this._PaibanDal.GetPaibanByJizhen(dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = dtPaiban.DefaultView;
            if (dgvOTypesetting.Rows.Count>0)
                //btnisJZ.Visible=true;
            groupBox2.Visible = false;           
            //btnPrint.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ExportExcel(dtDataTime.Text, dgvOTypesetting);
        }
        private void ExportExcel(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            Microsoft.Office.Interop.Excel.Range range = null;
            Microsoft.Office.Interop.Excel.Range range1 = worksheet.get_Range("B2", "S4");
            range1.Select();
            range1.Merge();
            range1.Font.Size = 16;
            range1.Borders.LineStyle = 1;
            range1.Value2 = "昌 吉 州 人 民 医 院 手 术 通 知 单";
            range1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range range2 = worksheet.get_Range("B4", "S4");
            range2.Select();
            range2.Font.Size = 12;
            range2.Value2 = "        手术日期：" + dtDataTime.Text;
            Microsoft.Office.Interop.Excel.Range excelRange = worksheet.get_Range("A6", "V6");
            excelRange.Select();
            xlApp.ActiveWindow.FreezePanes = true;

            //写入标题
            //int ColCount = 0;
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[5, i + 2] = myDGV.Columns[i].HeaderText;
                range = xlApp.Cells[5, i + 2];
                range.Font.Bold = true;
                range.RowHeight = 25;
                range.Interior.ColorIndex = 34;
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                range.Borders.LineStyle = 1;
                if (i == 3 || i == 10 || i == 11)
                {
                    range.ColumnWidth = 11;
                }
                else range.EntireColumn.AutoFit();

            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 6, i + 2] = myDGV.Rows[r].Cells[i].Value;
                    range = worksheet.Cells[r + 6, i + 2];
                    range.WrapText = true;
                    int[] a = { 1, 1, 2, 4, 5, 6, 7, 8, 9 };
                    foreach (int dr in a)
                    {
                        if (i == dr)
                        {
                            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        }
                        else
                            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                    }
                    range.EntireRow.AutoFit();//行高自适应
                    range.Borders.LineStyle = 1;

                }
                System.Windows.Forms.Application.DoEvents();
            }
            //worksheet.Columns.EntireColumn.Width = 40;//列宽自适应
            //worksheet.Rows.AutoFilter();
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                    ProgressBar pbar = new ProgressBar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//强行销毁
            MessageBox.Show("文件： " + fileName + ".xls 保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region 打印通知信息
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                printPreviewDialog1.Document = printDocument1;
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    printDocument1.Print();
                //printDocument2.PaperOrientation = PaperOrientation.Landscape;
                printDocument1.DefaultPageSettings.Landscape = false;
            }

        }
        int dgvRowCount = 0;
        int rowindex = 0;
        int Fzrows = 0;
        int YY = 0;
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.pdDocument.DefaultPageSettings.Landscape = true;//横向打印
            this.printDocument1.DefaultPageSettings.Color = true;//彩色打印   
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("A4", 820, 1160);
              printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 740, 1020);  
            dgvRowCount = dgvOTypesetting.Rows.Count;
            rowindex = 0;
            Fzrows = 0;
            YY = 0;
        }
        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen black = new Pen(Brushes.Black);
            black.Width = 1;
            Font HeiTi = new System.Drawing.Font("黑体", 14);
            Font SongTi = new System.Drawing.Font("新宋体", 9);           
            Font Song = new System.Drawing.Font("宋体", 10);           
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            //Font Kti = new System.Drawing.Font("楷体", 7);
            int x = 50, y = 0, y1 = 0;
            for (int i = Fzrows; i < dgvRowCount; i++)
            {
                if ((bool)dgvOTypesetting.Rows[i].Cells["Column1"].EditedFormattedValue == true)
                {
                    DataRow dr = this._PaibanDal.GetPaibanByID(dgvOTypesetting.Rows[Fzrows].Cells["id"].Value.ToString()).Rows[0];
                    y = y + 30;
                    e.Graphics.DrawString("手术通知单", new Font("宋体", 14, FontStyle.Bold), Brushes.Black, new Point(x + 250, y));
                    y = y + 30;
                    e.Graphics.DrawString("住院号：" + dr["PatZhuYuanID"].ToString(), Song, Brushes.Black, new Point(x + 10, y));
                    if (dr["SQSJ"].ToString()!="")
                    {
                        if (dr["SQSJ"].ToString().Contains("1900"))
                        {
                            e.Graphics.DrawString("申请时间：" + Convert.ToDateTime(dr["Odate"]).ToString("yyyy-MM-dd HH:mm"), Song, Brushes.Black, new Point(x + 160, y));
                        }
                        else
                        {
                            e.Graphics.DrawString("申请时间：" + Convert.ToDateTime(dr["SQSJ"]).ToString("yyyy-MM-dd HH:mm"), Song, Brushes.Black, new Point(x + 160, y));
                        }
                    }
                    else
                    {
                        e.Graphics.DrawString("申请时间：" + Convert.ToDateTime(dr["Odate"]).ToString("yyyy-MM-dd HH:mm"), Song, Brushes.Black, new Point(x + 160, y));
                    }                  
                    e.Graphics.DrawString("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), Song, Brushes.Black, new Point(x + 400, y));
                    y = y + 30; YY = y;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);
                    y = y + 5;
                    e.Graphics.DrawString("病人姓名：" + dr["Patname"].ToString(), Song, Brushes.Black, new Point(x + 10, y ));
                    e.Graphics.DrawString("性别：" + dr["Patsex"].ToString(), Song, Brushes.Black, new Point(x + 160, y ));
                    e.Graphics.DrawString("年龄：" + dr["Patage"].ToString()+"岁", Song, Brushes.Black, new Point(x + 240, y ));
                    e.Graphics.DrawString("病床：" + dr["Patbedno"].ToString(), Song, Brushes.Black, new Point(x + 340, y));
                    e.Graphics.DrawRectangle(Pens.Black, x + 440, y, 14, 14);
                    e.Graphics.DrawString("择期", Song, Brushes.Black, x + 460, y);
                    if (dr["isjizhen"].ToString() != "1")
                    {
                        e.Graphics.DrawLine(pblue2, x + 440, y, x + 447, y + 14);
                        e.Graphics.DrawLine(pblue2, x + 447, y + 14, x + 454, y - 2);
                    }
                    e.Graphics.DrawRectangle(Pens.Black, x + 520, y, 14, 14);
                    e.Graphics.DrawString("急诊", Song, Brushes.Black, x + 540, y);
                    if (dr["isjizhen"].ToString() == "1")
                    {
                        e.Graphics.DrawLine(pblue2, x + 520, y, x + 527, y + 14);
                        e.Graphics.DrawLine(pblue2, x + 527, y + 14, x + 534, y - 2);
                    }
                    y = y + 25;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);
                    y = y + 5;
                    e.Graphics.DrawString("科别：" + dr["Patdpm"].ToString(), Song, Brushes.Black, new Point(x + 10, y));
                    //e.Graphics.DrawString("病室：" , Song, Brushes.Black, new Point(x + 160, y));
                    e.Graphics.DrawString("身份：" + dr["shengfen"].ToString(), Song, Brushes.Black, new Point(x + 250, y));
                    e.Graphics.DrawString("手术时间：" + Convert.ToDateTime(dr["Odate"]).ToString("yyyy-MM-dd") + " " + dr["StartTime"].ToString(), Song, Brushes.Black, new Point(x + 400, y));
                    y = y + 25;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);
                    y = y + 5;
                    e.Graphics.DrawString("诊断名称：", SongTi, Brushes.Black, x + 10, y);
                    var arrayPattmd = ArrayHelper.SplitLength(dr["Pattmd"].ToString().Trim(), 45);
                    foreach (string item in arrayPattmd)
                    {
                        e.Graphics.DrawString(item, SongTi, Brushes.Black, x + 75, y);
                        y += 15;
                    }
                    y = y + 10;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);

                    y = y + 5;
                
                    y = y + 5;
                    e.Graphics.DrawString("手术名称：" + dr["oname"].ToString(), Song, Brushes.Black, new Point(x + 10, y));
                    y = y + 25;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);
                    y = y + 5;
                    e.Graphics.DrawString("描述：", SongTi, Brushes.Black, x + 10, y);
                    var arrayMs = ArrayHelper.SplitLength(dr["ms"].ToString().Trim(), 45);
                    foreach (string item in arrayMs)
                    {
                        e.Graphics.DrawString(item, SongTi, Brushes.Black, x + 45, y);
                        y += 15;
                    }
                    if (arrayMs.Count==0)
                    {
                        y = y + 15;
                    }
                    y = y + 10;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);
                  
                    y = y + 5;
                    e.Graphics.DrawString("拟行麻醉方式：" + dr["Amethod"].ToString(), Song, Brushes.Black, new Point(x + 10, y));
                    e.Graphics.DrawString("手术室：  第" + dr["Oroom"].ToString() + "手术房间", Song, Brushes.Black, new Point(x + 350, y));
                    y = y + 25;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);
                    y = y + 5;
                    e.Graphics.DrawString("手术医师：" + dr["OS"].ToString(), Song, Brushes.Black, new Point(x + 10, y));
                    e.Graphics.DrawString("第一助理：" + dr["OS1"].ToString(), Song, Brushes.Black, new Point(x + 190, y));
                    e.Graphics.DrawString("第二助理：" + dr["OS2"].ToString(), Song, Brushes.Black, new Point(x + 400, y));
                    y = y + 25;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);
                    y = y + 5;
                    e.Graphics.DrawString("麻醉医师：" + dr["ap1"].ToString() + " " + dr["ap2"].ToString() + " " + dr["ap3"].ToString(), Song, Brushes.Black, new Point(x + 10, y));
                    e.Graphics.DrawString("器械护士：" + dr["on1"].ToString() + " " + dr["on2"].ToString(), Song, Brushes.Black, new Point(x + 300, y));
                    e.Graphics.DrawString("巡回护士：" + dr["sn1"].ToString() + " " + dr["sn2"].ToString(), Song, Brushes.Black, new Point(x + 470, y));
                    y = y + 25;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);
                    y = y + 5;
                    e.Graphics.DrawString("备\n注", Song, Brushes.Black, new Point(x + 10, y));
                    int BZYYSS = y;
                    string strSS1 = "";
                    int StrLengthSS = dr["Remarks"].ToString().Length;
                    int rowSS = StrLengthSS / 30;                
                    for (int J = 0; J <= rowSS; J++)//50个字符就换行
                    {
                        if (J < rowSS)
                            strSS1 = dr["Remarks"].ToString().Trim().ToString().Substring(J * 30, 30); //从第i*50个开始，截取50个字符串
                        else
                            strSS1 = dr["Remarks"].ToString().Trim().ToString().Substring(J * 30);

                       
                        e.Graphics.DrawString(strSS1, Song, Brushes.Black, x + 50, BZYYSS);
                        BZYYSS = BZYYSS + 30;
                    }
                    y = y + 55;
                    e.Graphics.DrawLine(black, x, y, x + 650, y);
                    e.Graphics.DrawLine(black, x, YY, x, y);
                    e.Graphics.DrawLine(black, x + 650, YY, x + 650, y);

                    rowindex++;
                    if (rowindex % 2 == 0 && rowindex > 1)
                    {
                        e.HasMorePages = true;
                        y = 0; y1 = 0;
                        Fzrows++;
                        break;
                    }
                }
                Fzrows++;


            }
             
        }

        #endregion
        public void PaiXu()
        {
            if (cbOroom.Checked || cbSecond.Checked || cbTime.Checked || cbKeshi.Checked)
            {
              
                int isflg= 0;
                string sql1 = "order by  ";
                string sql2 = " asc";
                if (cbOroom.Checked)
                {
                    sql1 += "Convert(int,oroom) ";
                    isflg++;
                }
                if (cbSecond.Checked)
                {
                    if (isflg > 0)              
                        sql1 += ",Convert(int,second) ";                    
                    else             
                        sql1 += "Convert(int,second) ";
                    
                    isflg++;
                }
                if (cbTime.Checked)
                {
                    if (isflg>0)
                        sql1 += ",starttime ";
                    else
                        sql1 += "starttime ";
                    isflg++;
                }
                if (cbKeshi.Checked) 
                {
                    if (isflg>0)
                        sql1 += ",patdpm ";
                    else
                        sql1 += "patdpm ";
                }                   
                string sql = sql1 + sql2;
                if (isBool==0)
                {
                    DataTable dt = this._PaibanDal.GetPaiBanSort(0, dtDataTime.Value.Date.ToString("yyyy-MM-dd"), sql);
                    dgvOTypesetting.DataSource = dt.DefaultView;
                }
                else if (isBool == 1)
                {
                    DataTable dt = _PaibanDal.GetPaiBanSort(1, dtDataTime.Value.Date.ToString("yyyy-MM-dd"), sql);
                    dgvOTypesetting.DataSource = dt.DefaultView;
                }
                else
                {
                    DataTable dt = _PaibanDal.GetPaiBanSort(dtDataTime.Value.Date.ToString("yyyy-MM-dd"), sql);
                    dgvOTypesetting.DataSource = dt.DefaultView;
                }                     
            }
            else
            {
                BindPaibanInfo();
                
            }
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfig_Click(object sender, EventArgs e)
        {
            PaiXu();
        }

        private void dgvOTypesetting_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvOTypesetting.CurrentCell.ColumnIndex == 1)
                e.Control.KeyPress += new KeyPressEventHandler(dgvOTypesetting_KeyPress);
            else
                e.Control.KeyPress -= new KeyPressEventHandler(dgvOTypesetting_KeyPress);

        }

        private void btnisJZ_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 修改为急诊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiJZ_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count>0)
            {
                 string id = dgvOTypesetting.CurrentRow.Cells["id"].Value.ToString();
                 int res = this._PaibanDal.UpdatePaibanJizhen(id, 1);
                 if (res>0)
                 {
                     MessageBox.Show("修改成功");
                 }
                 else
                 {
                     MessageBox.Show("修改失败");
                 }
            }
        }
        /// <summary>
        /// 修改为择期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiZQ_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                string id = dgvOTypesetting.CurrentRow.Cells["id"].Value.ToString();
                int res = _PaibanDal.UpdatePaibanJizhen(id, 0);
                if (res > 0)
                {
                    MessageBox.Show("修改成功");
                }
                else
                {
                    MessageBox.Show("修改失败");
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                string id = dgvOTypesetting.CurrentRow.Cells["id"].Value.ToString();
                int res = this._PaibanDal.DeletePaiban(id);
                if (res > 0)
                {
                    MessageBox.Show("删除成功");
                    BindPaibanInfo();
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
        }
      

    }
}