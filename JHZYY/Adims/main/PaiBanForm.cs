using adims_BLL;
using Adims_Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
//using CrystalDecisions.Shared;

namespace main
{
    public partial class PaiBanForm : Form
    {
        #region <<Members>>

        adims_DAL.hisdbhelp_oracle hisoracl = new adims_DAL.hisdbhelp_oracle();
        adims_DAL.HisDB_Help HisHelp = new adims_DAL.HisDB_Help();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        string ssj = "全部手术间";
        int isBool = 2;//0表示未排班，1表示已排班,2表示全部

        DataTable dtRoom = new DataTable();

        DataTable dtSurgeryStaff = new DataTable();
        DataTable ds = new DataTable();
        private DateTimePicker dtKStime = new DateTimePicker();
        private ComboBox cmbRoom = new ComboBox();
        private ComboBox cmbXHHS = new ComboBox();
        private ComboBox cmbMZYS = new ComboBox();
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
        public PaiBanForm()
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
                //btnisJZ.Visible = false;//隐藏急诊打印按钮
                List<adims_MODEL.oroomstate> rs = new List<adims_MODEL.oroomstate>();
                dgvOTypesetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                BindPaibanInfo();//绑定列表
                dtRoom = dal.GetOROOM();
                BindOroomList(dtRoom);
                BindOroom(dtRoom);
                DataTable dtMZYS = dal.GetAllMZYS();
                BindMZYS(dtMZYS);
                DataTable dtXHHS = dal.GetAll_hushi();
                BindXHHS(dtXHHS);
                cmbRoom.Visible = false;
                cmbXHHS.Visible = false;
                cmbMZYS.Visible = false;
                dtKStime.Visible = false;
                cmbRoom.SelectedIndexChanged += new EventHandler(cmbRoom_SelectedIndexChanged);
                cmbMZYS.SelectedIndexChanged += new EventHandler(cmbMZYS_SelectedIndexChanged);
                cmbXHHS.SelectedIndexChanged += new EventHandler(cmbXHHS_SelectedIndexChanged);
                dtKStime.ValueChanged += new EventHandler(dtKStime_ValueChanged);
                dgvOTypesetting.Controls.Add(cmbRoom);
                dgvOTypesetting.Controls.Add(cmbMZYS);
                dgvOTypesetting.Controls.Add(dtKStime);
                dgvOTypesetting.Controls.Add(cmbXHHS);
                dgvOTypesetting.CellValueChanged += new DataGridViewCellEventHandler(dgvOTypesetting_CellValueChanged);

                var cell = this.dgvOTypesetting.GetCellDisplayRectangle(0, -1, true);
                checkbox = new CheckBox { Left = cell.Left + 23, Top = cell.Top + 5, Width = 15, Height = 15 };
                this.dgvOTypesetting.Controls.Add(checkbox);

                this.checkbox.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);

            }
            catch (Exception ex)
            {
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

        }

        private void BindPaibanInfo()
        {
            ds = dal.GetOTypesetting(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = ds.DefaultView;
            lblTS.Text = dgvOTypesetting.Rows.Count.ToString();
            isBool = 2;
            checkbox.Checked = false;

        }

        private void HisDataBind()
        {
            try
            {
                DataTable dt1 = hisoracl.GetHISpaiban(dtDataTime.Value.ToString("yyyy-MM-dd"));
                List<string> HISinfo = new List<string>();
                int result = 0;

                //string colname = "";
                //foreach (DataColumn item in dt1.Columns)
                //{
                //    colname += item.ColumnName + "~~  ";
                //}
                //LogHelp.WriteLog(colname);


                foreach (DataRow dr in dt1.Rows)
                {
                    //string colValue = "";
                    //int a = dr.ItemArray.Length;
                    //for (int i = 0; i < a; i++)
                    //{
                    //    colValue += dr[i].ToStringForce() + "~~  ";
                    //}
                    //LogHelp.WriteLog(colValue);

                    HISinfo.Clear();
                    HISinfo.Add(dr["PATID"].ToString());//1
                    HISinfo.Add(dr["ZYID"].ToString());//2               
                    HISinfo.Add(dr["CARDID"].ToString());//3
                    HISinfo.Add(dr["PATNAME"].ToString());//4
                    HISinfo.Add(dr["PATSEX"].ToString());//5
                    HISinfo.Add(dr["PATAGE"].ToString());//6
                    HISinfo.Add(dr["PATNATION"].ToString());//7
                    HISinfo.Add(dr["BedNo"].ToString());//8
                    HISinfo.Add(dr["PATTMD"].ToString());//9

                    HISinfo.Add(dr["ONAME"].ToString());//10
                    HISinfo.Add(dr["AMETHOD"].ToString());//11
                    HISinfo.Add(dr["OS"].ToString());//12
                    HISinfo.Add(dr["OS1"].ToString());//13
                    HISinfo.Add(dr["OS2"].ToString());//14
                    HISinfo.Add(dr["OS3"].ToString());//15
                    HISinfo.Add(dr["OS4"].ToString());//16
                    HISinfo.Add(dr["StartTime"].ToDateTime().ToString("HH:mm"));
                    HISinfo.Add(dr["Odate"].ToString());//17
                    HISinfo.Add(dr["TIWEI"].ToString());//18
                    HISinfo.Add(dr["SSLB"].ToString());//19
                    HISinfo.Add(dr["PATDPM"].ToString());//20
                    DataTable dt = dal.GetALLPAIBANs(dr["ZHUYUANNO"].ToString(), Convert.ToDateTime(dr["Odate"]).ToString("yyyy-MM-dd"));
                    if (dt.Rows.Count == 0)
                        result = dal.InsertPaiban(HISinfo);
                    //else
                    //    result = dal.UpdatePaiban(HISinfo);

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
            dt1 = dal.GetOROOM();
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
        private void b_Click(object sender, EventArgs e)
        {
            //ssj = ((Button)sender).Text;
            //DataTable ds = new DataTable();
            //ds = dal.GetOTypesetting(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
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
                DataTable dt = bll.selectSinglemzjld(PatID, dtDataTime.Value.ToString("yyyy-MM-dd"));
                if (dt.Rows.Count > 0)
                    mzjldID = Convert.ToInt32(dt.Rows[0][0]);
                mzjldEdit F1 = new mzjldEdit(PatID, Oroom, DateTime.Parse(Odate), mzjldID);
                F1.ShowDialog();
            }
        }

        private void dgvOTypesetting_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvOTypesetting.Rows.Count > 0)
            //{
            //    lbPatName.Text = dgvOTypesetting.CurrentRow.Cells["patname"].Value.ToString();
            //    lbKeshi.Text = dgvOTypesetting.CurrentRow.Cells["patdpm"].Value.ToString();
            //    lbOS.Text = dgvOTypesetting.CurrentRow.Cells["os"].Value.ToString();
            //    lbSSMC.Text = dgvOTypesetting.CurrentRow.Cells["oname"].Value.ToString();
            //    int ColIndex = dgvOTypesetting.CurrentCell.ColumnIndex;
            //    if (ColIndex == 1)
            //    {
            //        cmbXHHS.Visible = false;
            //        cmbRoom.Visible = false;
            //        cmbMZYS.Visible = false;
            //        Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
            //        cmbRoom.Left = rect.Left;
            //        cmbRoom.Top = rect.Top;
            //        cmbRoom.Width = rect.Width;
            //        cmbRoom.Height = rect.Height;
            //        cmbRoom.Visible = true;
            //        cmbRoom.Text = "";
            //        cmbRoom.DroppedDown = true;
            //    }

            //    else if (ColIndex == 14 || ColIndex == 15 || ColIndex == 16 || ColIndex == 17)
            //    {
            //        cmbXHHS.Visible = false;
            //        cmbRoom.Visible = false;
            //        cmbMZYS.Visible = false;
            //        Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
            //        cmbXHHS.Left = rect.Left;
            //        cmbXHHS.Top = rect.Top;
            //        cmbXHHS.Width = rect.Width;
            //        cmbXHHS.Height = rect.Height;
            //        cmbXHHS.Visible = true;
            //        cmbXHHS.Text = "";
            //        cmbXHHS.DroppedDown = true;
            //    }
            //    else if (ColIndex == 18 || ColIndex == 19 || ColIndex == 20)
            //    {
            //        cmbXHHS.Visible = false;
            //        cmbRoom.Visible = false;
            //        cmbMZYS.Visible = false;
            //        Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
            //        cmbMZYS.Left = rect.Left;
            //        cmbMZYS.Top = rect.Top;
            //        cmbMZYS.Width = rect.Width;
            //        cmbMZYS.Height = rect.Height;
            //        cmbMZYS.Visible = true;
            //        cmbMZYS.Text = "";
            //        cmbMZYS.DroppedDown = true;
            //    }
            //    else
            //    {
            //        cmbXHHS.Visible = false;
            //        cmbRoom.Visible = false;
            //        cmbMZYS.Visible = false;
            //    }
            //}
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
            int time_count = ds.Rows.Count;
            time_object[] times = new time_object[time_count];
            for (i = 0; i < time_count; i++)
            {
                times[i].start = (DateTime)(ds.Rows[i][19]);
                times[i].end = (DateTime)(ds.Rows[i][20]);
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
                ds = dal.GetOTypesetting(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
                dgvOTypesetting.DataSource = ds.DefaultView;
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
            ds = dal.GetOTypesetting(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = ds.DefaultView;
            lblTS.Text = dgvOTypesetting.Rows.Count.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string IPaddress = "172.1.1.1";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true) HisDataBind();
            else MessageBox.Show("HIS数据库未连接，请检查网络");
            groupBox2.Visible = true;
            //checkbox.Checked = false;
            //btnisJZ.Visible = false;
            //btnPrint.Enabled = true;


        }

        private void btnYES_Click(object sender, EventArgs e)
        {
            ds = dal.GetIsPaiban(1, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = ds.DefaultView;
            lblTS.Text = dgvOTypesetting.Rows.Count.ToString();
            isBool = 1;
            groupBox2.Visible = true;
            checkbox.Checked = false;
            //btnisJZ.Visible = false;
            //btnPrint.Enabled = true;
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            ds = dal.GetIsPaibans(0, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = ds.DefaultView;
            lblTS.Text = dgvOTypesetting.Rows.Count.ToString();
            isBool = 0;
            groupBox2.Visible = true;
            checkbox.Checked = false;
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
            //if (dgvOTypesetting.CurrentCell.ColumnIndex == 1 || dgvOTypesetting.CurrentCell.ColumnIndex == 0)
            //{
            //    if (adims_BLL.ValidationRegex.ValidteData(e.KeyChar.ToString()))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }
        private void dgvOTypesetting_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvOTypesetting.CurrentCell.ColumnIndex == 2)
                e.Control.KeyPress += new KeyPressEventHandler(dgvOTypesetting_KeyPress);
            else
                e.Control.KeyPress -= new KeyPressEventHandler(dgvOTypesetting_KeyPress);
        }
        private void dgvOTypesetting_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 2)
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
                //try
                //{
                //    if (!String.IsNullOrEmpty(e.FormattedValue.ToString()))
                //    {
                //        DateTime val = DateTime.Parse(e.FormattedValue.ToString());
                //    }
                //}
                //catch (Exception)
                //{
                //    dgvOTypesetting.Rows[e.RowIndex].ErrorText = "必须时间格式（HH:mm）";
                //    MessageBox.Show("请输入正确的时间格式（HH:mm）");
                //    e.Cancel = true;
                //    return;
                //}
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
            ds = dal.GetIsJZPaiban(dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            dgvOTypesetting.DataSource = ds.DefaultView;
            lblTS.Text = dgvOTypesetting.Rows.Count.ToString();
            if (dgvOTypesetting.Rows.Count > 0)
                //btnisJZ.Visible=true;
                groupBox2.Visible = false;
            checkbox.Checked = false;
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

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.printDocument1.DefaultPageSettings.Landscape = true;//横向打印
            this.printDocument1.DefaultPageSettings.Color = true;//彩色打印   
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            dgvRowCount = dgvOTypesetting.Rows.Count;
            rowindex = 0;
            Fzrows = 0;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen black = new Pen(Brushes.Black);
            black.Width = 1;
            Font HeiTi = new System.Drawing.Font("黑体", 14);
            Font SongTi = new System.Drawing.Font("宋体", 10);
            Font HeiTi9 = new System.Drawing.Font("黑体", 9);
            Font Song = new System.Drawing.Font("宋体", 8);
            //Font Kti = new System.Drawing.Font("楷体", 7);
            int lineSpace = 20;
            int left = 50;
            int x = left, y = 0, y1 = 0;
            int x1 = 0;

            int right = x + 1020;
            int yNext = y + lineSpace;
            for (int i = Fzrows; i < dgvRowCount; i++)
            {
                if ((bool)dgvOTypesetting.Rows[i].Cells["Column1"].EditedFormattedValue == true)
                {
                    DataGridViewRow dgvRow = dgvOTypesetting.Rows[i];
                    y = y + 30;
                    e.Graphics.DrawString("               金华市中医院           日期： " + dtDataTime.Value.ToString("yyyy/MM/dd"),
                        new Font("宋体", 12, FontStyle.Bold), Brushes.Black, new Point(x + 120, y));

                    x = left; y = y + lineSpace; y1 = y + 3;
                    e.Graphics.DrawLine(black, x, y, right, y);
                    x = x + 0; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("手术间", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 50; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("台次", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 40; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("类别", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 70; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("病区", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 130; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("床号", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 40; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("姓名", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 50; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("住院号", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 80; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("性别", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 40; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("年龄", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 40; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("     手术名称", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 250; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("主刀", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 70; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("一助", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 70; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("二助", HeiTi9, Brushes.Black, new Point(x1, y1));
                    e.Graphics.DrawLine(black, right, y, right, y + 2 * lineSpace);

                    x = left; y = y + lineSpace; y1 = y + 3;
                    e.Graphics.DrawLine(black, x, y, right, y);
                    x = x + 0; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["Oroom"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 50; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["second"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    string sslb = "";
                    if (dgvRow.Cells["sslb"].Value.ToInt32() == 1)
                    {
                        sslb = "择期手术";
                    }
                    else
                    {
                        sslb = "急诊手术";
                    }
                    x = x + 40; x1 = x + 3;
                    e.Graphics.DrawString(sslb, Song, Brushes.Black, new Point(x1, y1));
                    x = x + 70; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["PatDpm"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 130; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["PatBedno"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 40; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["PatName"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 50; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["PatZhuYuanID"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 80; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["Patsex"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 40; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["PatAge"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 40; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["Oname"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 250; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["OS"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 70; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["OS1"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 70; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["OS2"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));


                    x = left; y = y + lineSpace; y1 = y + 3;
                    e.Graphics.DrawLine(black, x, y, right, y);
                    x = x + 0; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("       术前诊断", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 160; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("         医生对话信息", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 300; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("麻醉方式", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 80; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("麻醉医师", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 170; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("洗手", HeiTi9, Brushes.Black, new Point(x1, y1));
                    x = x + 150; x1 = x + 3;
                    e.Graphics.DrawLine(black, x, y, x, y + 2 * lineSpace);
                    e.Graphics.DrawString("巡回", HeiTi9, Brushes.Black, new Point(x1, y1));
                    e.Graphics.DrawLine(black, right, y, right, y + 2 * lineSpace);

                    x = left; y = y + lineSpace; y1 = y + 3;
                    e.Graphics.DrawLine(black, x, y, right, y);
                    x = x + 0; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["PatTmd"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 160; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["TalkInfo"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 300; x1 = x + 3;
                    e.Graphics.DrawString(dgvRow.Cells["Amethod"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    x = x + 80; x1 = x + 3;
                    string mzys = dgvRow.Cells["Ap1"].Value.ToStringForce() +
                        " " + dgvRow.Cells["Ap2"].Value.ToStringForce() +
                        " " + dgvRow.Cells["Ap3"].Value.ToStringForce(); ;
                    e.Graphics.DrawString(mzys, Song, Brushes.Black, new Point(x1, y1));
                    x = x + 170; x1 = x + 3;
                    string xshs = dgvRow.Cells["SN1"].Value.ToStringForce()
                        + " " + dgvRow.Cells["SN2"].Value.ToStringForce();
                    e.Graphics.DrawString(xshs, Song, Brushes.Black, new Point(x1, y1));
                    x = x + 150; x1 = x + 3;
                    string xhhs = dgvRow.Cells["ON1"].Value.ToStringForce()
                    + " " + dgvRow.Cells["ON2"].Value.ToStringForce();
                    e.Graphics.DrawString(xhhs, Song, Brushes.Black, new Point(x1, y1));

                    x = left; y = y + lineSpace; y1 = y + 3;
                    e.Graphics.DrawLine(black, x, y, right, y);

                    y = y + 50;
                    rowindex++;
                    if (rowindex % 4 == 0 && rowindex > 3)
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

                int isflg = 0;
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
                    if (isflg > 0)
                        sql1 += ",starttime ";
                    else
                        sql1 += "starttime ";
                    isflg++;
                }
                if (cbKeshi.Checked)
                {
                    if (isflg > 0)
                        sql1 += ",patdpm ";
                    else
                        sql1 += "patdpm ";
                }
                string sql = sql1 + sql2;
                if (isBool == 0)
                {
                    DataTable dt = dal.GetPaiXu(0, dtDataTime.Value.Date.ToString("yyyy-MM-dd"), sql);
                    dgvOTypesetting.DataSource = dt.DefaultView;
                }
                else if (isBool == 1)
                {
                    DataTable dt = dal.GetPaiXu(1, dtDataTime.Value.Date.ToString("yyyy-MM-dd"), sql);
                    dgvOTypesetting.DataSource = dt.DefaultView;
                }
                else
                {
                    DataTable dt = dal.GetPaiXu(dtDataTime.Value.Date.ToString("yyyy-MM-dd"), sql);
                    dgvOTypesetting.DataSource = dt.DefaultView;
                }
            }
            else
            {
                BindPaibanInfo();
                //DataTable dt = dal.GetPaiXu(dtDataTime.Value.Date.ToString("yyyy-MM-dd"),"");
                //dgvOTypesetting.DataSource = dt.DefaultView;
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
        /// <summary>
        /// 打印手术室接病人流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnisJZ_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                printPreviewDialog2.Document = printDocument2;
                if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
                    printDocument2.Print();
                //printDocument2.PaperOrientation = PaperOrientation.Landscape;
                printDocument2.DefaultPageSettings.Landscape = false;
            }
        }
        int dgvRowCount2 = 0;
        int rowindex2 = 0;
        int Fzrows2 = 0;
        private void printDocument2_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.printDocument2.DefaultPageSettings.Color = true;//彩色打印   
            printPreviewDialog2.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog2.WindowState = FormWindowState.Maximized;
            printDocument2.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            dgvRowCount2 = dgvOTypesetting.Rows.Count;
            rowindex2 = 0;
            Fzrows2 = 0;
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Pen black = new Pen(Brushes.Black);
            black.Width = 1;
            //Font HeiTi = new System.Drawing.Font("黑体", 9);
            Font SongTi = new System.Drawing.Font("宋体", 11);
            //Font Kti = new System.Drawing.Font("楷体", 7);
            int x = 50, y = 0;
            for (int i = Fzrows2; i < dgvRowCount2; i++)
            {
                if ((bool)dgvOTypesetting.Rows[i].Cells["Column1"].EditedFormattedValue == true)
                {
                    y = y + 50;
                    e.Graphics.DrawString("手术室接病人流程", new Font("宋体", 12, FontStyle.Regular), Brushes.Black, new Point(x + 30, y));
                    y = y + 30;
                    e.Graphics.DrawString("填写通知单→安全转运（1人1车、护栏、固定带、盖被、管道）→接收核对、防护、保暖、将病人", SongTi, Brushes.Black, new Point(x + 30, y));
                    y = y + 20;
                    e.Graphics.DrawString("病历、用物装袋带回→安全等待→巡回护士核对→接病人入手术间。", SongTi, Brushes.Black, new Point(x + 30, y));
                    y = y + 30;
                    e.Graphics.DrawString("手术室优质服务", new Font("宋体", 13, FontStyle.Regular), Brushes.Black, new Point(x + 30, y));
                    y = y + 30;
                    e.Graphics.DrawString("一个真诚的微笑    一个亲切的问候", SongTi, Brushes.Black, new Point(x + 30, y));
                    y = y + 20;
                    e.Graphics.DrawString("一次仔细的核对    一幅整洁的推车", SongTi, Brushes.Black, new Point(x + 30, y));
                    y = y + 20;
                    e.Graphics.DrawString("一个良好的环境    一次安全的护送", SongTi, Brushes.Black, new Point(x + 30, y));
                    y = y + 80;
                    rowindex2++;
                    if (rowindex2 % 4 == 0 && rowindex2 > 3)
                    {
                        e.HasMorePages = true;
                        y = 0;
                        Fzrows2++;
                        break;
                    }
                }
                Fzrows2++;

            }
        }


        /// <summary>
        /// 打印急诊通知单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJiZheng_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                printPreviewDialog3.Document = printDocument3;
                if (printPreviewDialog3.ShowDialog() == DialogResult.OK)
                    printDocument3.Print();
                printDocument3.DefaultPageSettings.Landscape = false;
            }

        }
        int dgvRowCount3 = 0;
        int rowindex3 = 0;
        int Fzrows3 = 0;
        private void printDocument3_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.printDocument3.DefaultPageSettings.Landscape = true;
            this.printDocument3.DefaultPageSettings.Color = true;//彩色打印   
            printPreviewDialog3.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog3.WindowState = FormWindowState.Maximized;
            printDocument3.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            dgvRowCount3 = dgvOTypesetting.Rows.Count;
            rowindex3 = 0;
            Fzrows3 = 0;
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen black = new Pen(Brushes.Black);
            black.Width = 1;
            Font HeiTi = new System.Drawing.Font("黑体", 14);
            Font SongTi = new System.Drawing.Font("宋体", 10);
            Font HeiTi7 = new System.Drawing.Font("黑体", 8);
            Font Song = new System.Drawing.Font("宋体", 7);
            //Font Kti = new System.Drawing.Font("楷体", 7);
            int x = 20, y = 0, y1 = 0;
            int x1 = 0, xstart = 0;
            int xEnd = x + 1100;
            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("               金华市中医院手术管理安排（ 日期： " + dtDataTime.Value.ToString("yyyy/MM/dd") + "）",
                        new Font("宋体", 12, FontStyle.Bold), Brushes.Black, new Point(x + 120, y));

            y = y + 30; y1 = y + 3; x1 = x + 5;
            e.Graphics.DrawLine(black, x, y, xEnd, y);
            #region 画头部信息
            xstart = x + 0;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("手术间", HeiTi7, Brushes.Black, new Point(x1, y1));
            xstart = xstart + 40;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("台次", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 30;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("手术时间", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 50;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("病区", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 100;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("床号", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 40;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("姓名", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 40;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("住院号/就诊卡", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 80;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("性别", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 30;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("年龄", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 30;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("手术名称", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 170;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("主刀", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 40;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("一助", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 40;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("二助", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 40;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("麻醉方式", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 60;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("麻醉师", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 40;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("洗手", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 40;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("巡回", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 40;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("术前诊断", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 100;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("是否隔离", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 60;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            e.Graphics.DrawString("备注", HeiTi7, Brushes.Black, new Point(xstart + 3, y1));
            xstart = xstart + 30;
            e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
            #endregion
            y = y + 20; y1 = y + 3; x1 = x + 5;
            e.Graphics.DrawLine(black, x, y, xEnd, y);

            int selectIndex = 0;
            for (int i = Fzrows3; i < dgvRowCount3; i++)
            {
                if ((bool)dgvOTypesetting.Rows[i].Cells["Column1"].EditedFormattedValue == true)
                {
                    selectIndex++;
                    DataGridViewRow dgvRow = dgvOTypesetting.Rows[i];
                    xstart = x + 0; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["Oroom"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 40; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["second"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 30; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["StartTime"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 50; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["PatDpm"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 100; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["PatBedno"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 40; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["PatName"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 40; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["PatZhuYuanID"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 80; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["Patsex"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 30; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["PatAge"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 30; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["Oname"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 170; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["OS"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 40; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["OS1"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 40; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["OS2"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 40; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["Amethod"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 60; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["Ap1"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 40; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["SN1"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 40; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["ON1"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 40; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["Pattmd"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 100; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["IsGeli"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));
                    xstart = xstart + 60; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));
                    e.Graphics.DrawString(dgvRow.Cells["Remarks"].Value.ToStringForce(), Song, Brushes.Black, new Point(x1, y1));

                    xstart = xstart + 30; x1 = xstart + 3;
                    e.Graphics.DrawLine(black, new Point(xstart, y), new Point(xstart, y + 20));


                    y = y + 20; y1 = y + 3; x1 = x + 5;
                    e.Graphics.DrawLine(black, x, y, xEnd, y);

                }
                Fzrows3++;


            }

        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigPaiban();
        }

        private void ConfigPaiban()
        {
            //try
            //{
            //    if (dgvOTypesetting.SelectedCells.Count == 1)
            //    {
            //        //List<PaibanDto> list = (List<PaibanDto>)dgvOTypesetting.DataSource;
            //        //PaibanDto model = list[dgvOTypesetting.CurrentRow.Index];
            //        //string patid = model.PatID;
            //        //string Oroom = model.Oroom;
            //        //string Osecond = model.Second;
            //        string patid = dgvOTypesetting.CurrentRow.Cells["patid"].Value.ToString();
            //        string Oroom = dgvOTypesetting.CurrentRow.Cells["Oroom"].Value.ToString();
            //        string Osecond = dgvOTypesetting.CurrentRow.Cells["second"].Value.ToString();
            //        string id = dgvOTypesetting.CurrentRow.Cells["id"].Value.ToString();
            //        if (Oroom.IsNullOrEmpty() && Osecond.IsNullOrEmpty())
            //        {
            //            MessageBox.Show("手术间和台次不能都为空");
            //            return;
            //        }


            //        string message = Hl7Bll.AppendHL7stringConfig(id.ToInt32(), Program.yh, Program.zhanghao);

            //        LogHelp.SaveLogHL7(message);
            //        string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];
            //        //if (message.Length > 0 && UserFunction.PingHost(HL7IPaddress))
            //        if (true)
            //        {
            //            string HL7port = ConfigurationManager.AppSettings["HL7port"];
            //            SenderRoutingLib.SocketSender send = new SenderRoutingLib.SocketSender();
            //            object objResult;
            //            int iResult = 0;
            //            int count = 1;
            //            if (count < 10)
            //            {
            //                new System.Threading.Thread(o =>
            //                {
            //                    for (int i = 0; i < count; i++)
            //                    {
            //                        objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
            //                        string ack = objResult == null ? string.Empty : objResult.ToString();
            //                        if (ack.Contains("AA"))
            //                        {
            //                            iResult++;
            //                            LogHelp.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //                            // SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //                        }
            //                        else
            //                        {
            //                            iResult++;
            //                            LogHelp.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //                            // SetText(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //                        }
            //                    }
            //                }).Start();
            //            }
            //            else
            //            {
            //                for (int j = 0; j < 10; j++)
            //                {
            //                    new System.Threading.Thread(o =>
            //                    {
            //                        for (int i = 0; i < count / 10; i++)
            //                        {
            //                            objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
            //                            string ack = objResult == null ? string.Empty : objResult.ToString();
            //                            if (ack.Contains("AA"))
            //                            {
            //                                iResult++;
            //                                // SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //                            }
            //                            else
            //                            {
            //                                iResult++;
            //                                //SetText(string.Format("\r\n发送失败，错误信息：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //                            }
            //                        }
            //                    }).Start();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("IP地址或端口错误");
            //        }
            //    }
            //    else
            //        MessageBox.Show("请选择一位病人");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
        private void buttonConfig_Click(object sender, EventArgs e)
        {
            ConfigPaiban();
        }



        private void dgvOTypesetting_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                string id = dgvOTypesetting.CurrentRow.Cells["id"].Value.ToString();
                PAIBAN_Add add = new PAIBAN_Add(id);
                add.ShowDialog();
                BindPaibanInfo();
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            dtDataTime.Value = dtDataTime.Value.AddDays(-1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            dtDataTime.Value = dtDataTime.Value.AddDays(1);
        }
    }
}