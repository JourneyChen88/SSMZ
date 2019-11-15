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
using adims_DAL;
using WindowsFormsControlLibrary5;
using System.Net.NetworkInformation;
using System.Threading;
using main.HisWebReference;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using NHapi.Model.V24.Message;
using MediII.Common;
using Microsoft.International.Converters.PinYinConverter;
using System.Configuration;

namespace main
{
    public partial class PaiBanForm : Form
    {
        #region <<Members>>
        adims_MODEL.PaibanModel pbModel = new adims_MODEL.PaibanModel();
        
        string whereOstate1 = " and ostate>='1' ";
        string whereOstate0 = " and ostate='0' ";
        string whereEyeOper = " and patdpm like '%眼科%' ";
        string whereNotEyeOper = " and patdpm not like '%眼科%' ";

        //adims_DAL.HisDB_Help HisHelp = new adims_DAL.HisDB_Help();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        admin_T_SQL at = new admin_T_SQL();
        string ssj = "全部手术间";
        DataTable dtRoom = new DataTable();
        DB2help db2 = new DB2help();
        DataTable dtSurgeryStaff = new DataTable();
        DataTable ds = new DataTable();
        private DateTimePicker dtKStime = new DateTimePicker();
        private ComboBox cmbRoom = new ComboBox();
        private ComboBox cmbXHHS = new ComboBox();
        private ComboBox cmbMZYS = new ComboBox();

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
          
            this.numericUpDown1.Value = 1;
            this.WindowState = FormWindowState.Maximized;
            this.dtDataTime.Value = DateTime.Now;
            try
            {
                List<adims_MODEL.oroomstate> rs = new List<adims_MODEL.oroomstate>();
                dgvOTypesetting.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                BindPaibanInfo(whereNotEyeOper);//绑定列表
                dtRoom = dal.GetOROOM();
                BindOroom(dtRoom);
                BindOroomList(dtRoom);//绑定左侧手术间列表
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvOTypesetting_CellValueChanged(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                string patID = dgvOTypesetting.CurrentRow.Cells["patid"].Value.ToString();
                if (patID != "")
                {
                    string RoomName = dgvOTypesetting.Columns[dgvOTypesetting.CurrentCell.ColumnIndex].Name;
                    string Value_Info = dgvOTypesetting.CurrentCell.Value.ToString();
                    dal.UpdatePaiban(patID, RoomName, Value_Info);

                }
            }
        }

        private void BindPaibanInfo(string sqlwhere)//绑定我们系统数据表排班情况
        {

            ds = dal.GetPAIBAN(dtDataTime.Value.Date.ToString("yyyy-MM-dd"), sqlwhere);
            dgvOTypesetting.DataSource = ds.DefaultView;
        }

        private void HisDataBind()//刷新HIS排班申请，导入我们数据库
        {
            try
            {
                DataTable dt1 = db2.GetPAIBAN(dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
                List<string> HISinfo = new List<string>();
                int result = 0;
                foreach (DataRow dr in dt1.Rows)
                {
                    HISinfo.Clear();
                    HISinfo.Add(dr["patID"].ToString());//病人编号
                    HISinfo.Add(dr["cardno"].ToString());//就诊卡号
                    HISinfo.Add(dr["ZhuYuanNo"].ToString());//住院号
                    HISinfo.Add(dr["patName"].ToString());//病人姓名
                    HISinfo.Add(dr["patAge"].ToString());//年龄
                    HISinfo.Add(dr["patSex"].ToString());//性别
                    HISinfo.Add(dr["Patdpm"].ToString());//科室
                    HISinfo.Add(dr["SQZD"].ToString());//术前诊断
                    HISinfo.Add(dr["Oname"].ToString());//手术名称
                    HISinfo.Add(Convert.ToDateTime(dr["otime"]).ToString("yyyy-MM-dd"));//手术时间
                    HISinfo.Add(dr["OS"].ToString());//手术医生
                    HISinfo.Add(dr["mzfa"].ToString());//麻醉法
                    HISinfo.Add(dr["BedNo"].ToString());//床号
                    HISinfo.Add(dr["applyid"].ToString());//申请单号__patid
                    if (dr["ASAE"].ToString() == "")
                        HISinfo.Add("2");
                    else
                        HISinfo.Add(dr["ASAE"].ToString().Substring(1, 1));//急诊
                 
                    HISinfo.Add("0");//未排班的
                    DataTable dt = dal.GetPaiban(dr["applyid"].ToString());
                    if (dt.Rows.Count == 0)
                        result = dal.InsertPaiban(HISinfo);

                }
                if (cbEye.Checked)
                {
                    BindPaibanInfo(whereEyeOper);//绑定列表
                }
                else
                {
                    BindPaibanInfo(whereNotEyeOper);//绑定列表
                }

            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString() + " 出错");
            }

        }

        private void BindOroomList(DataTable dt1)//绑定手术间列表
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
            if (cmbRoom.SelectedIndex != -1)
            {
                dgvOTypesetting.CurrentCell.Value = cmbRoom.SelectedItem.ToString();
                cmbRoom.Visible = false;
                //cmbRoom.Text = "";
            }

        }

        private void cmbXHHS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbXHHS.SelectedIndex != -1)
            {
                dgvOTypesetting.CurrentCell.Value = cmbXHHS.SelectedItem.ToString();
                cmbXHHS.Visible = false;
                //cmbXHHS.Text = ""; 
            }
        }

        private void cmbMZYS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMZYS.SelectedIndex != -1)
            {
                dgvOTypesetting.CurrentCell.Value = cmbMZYS.SelectedItem.ToString();
                cmbMZYS.Visible = false;
                //cmbMZYS.Text = "";
            }
        }

        private void dtKStime_ValueChanged(object sender, EventArgs e)
        {
            dgvOTypesetting.CurrentCell.Value = dtKStime.Value.ToString("yyyy/MM/dd HH:mm");
            dtKStime.Visible = false;
        }

        private void BindOroom(DataTable dt1)//绑定手术间下拉框
        {
            cmbRoom.Items.Clear();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbRoom.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbRoom.Items.Add("");
        }

        private void BindMZYS(DataTable dt1)//绑定麻醉医生下拉框
        {
            cmbMZYS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbMZYS.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbMZYS.Items.Add("");
        }
        private void BindXHHS(DataTable dt1)//绑定护士下拉框
        {
            cmbXHHS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbXHHS.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbXHHS.Items.Add("");
        }


        /// <summary>
        /// 时间改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtDataTime_ValueChanged(object sender, EventArgs e)
        {
            string sqlWhere = "";
            if (cbEye.Checked)
            {
                sqlWhere = whereEyeOper;
            }
            else
            {
                sqlWhere = whereNotEyeOper;
            }
            BindPaibanInfo(sqlWhere);//绑定列表
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
                string Oroom = dgvOTypesetting.CurrentRow.Cells["oroom"].Value.ToString();
                if (string.IsNullOrEmpty(Oroom))
                {
                    MessageBox.Show("手术间号不能为空。");
                    return;
                }
                string PatID = dgvOTypesetting.CurrentRow.Cells["patid"].Value.ToString();
                if (string.IsNullOrEmpty(PatID))
                {
                    MessageBox.Show("住院号不能为空。");
                    return;
                }
                int mzjldID = 0;
                string Odate = dtDataTime.Value.ToString("yyyy-MM-dd");
                DataTable dt = bll.selectSinglemzjld1(PatID, Odate);
                if (dt.Rows.Count > 0)
                    mzjldID = Convert.ToInt32(dt.Rows[0][0]);
                mzjldEdit F1 = new mzjldEdit(PatID, Oroom, DateTime.Parse(Odate), mzjldID);
                F1.ShowDialog();
            }
        }

        private void dgvOTypesetting_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                cmbXHHS.Visible = false;
                cmbRoom.Visible = false;
                cmbMZYS.Visible = false;
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
            //                                 int i = 0;
            //int time_count = ds.Rows.Count;
            //time_object[] times = new time_object[time_count];
            //for (i = 0; i < time_count; i++)
            //{
            //    times[i].start = (DateTime)(ds.Rows[i][19]);
            //    times[i].end = (DateTime)(ds.Rows[i][20]);
            //    times[i].time_ID = i;
            //    times[i].person_ID = -1;
            //}
            //DataTable zhuma = new DataTable();
            //zhuma = bll.select_staff(0);
            //int zhuma_count = zhuma.Rows.Count;
            //bool result = arrange(times, time_count, zhuma_count, 0);
            //if (result)
            //{
            //    // for (i = 0; i < time_count; i++)
            //    //  dgvOTypesetting.Rows[i].Cells[12].Value = zhuma.Rows[times[i].person_ID][2];
            //    object[] doctors = new object[time_count];
            //    for (i = 0; i < time_count; i++)
            //        doctors[i] = zhuma.Rows[times[i].person_ID][2];
            //    bll.UpdateOTypesetting(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"), doctors);
            //    ssj = "全部手术间";
            //    ds = dal.GetOTypesetting(ssj, dtDataTime.Value.Date.ToString("yyyy-MM-dd"));
            //    dgvOTypesetting.DataSource = ds.DefaultView;
            //    MessageBox.Show("智能排班成功");
            //}
            //else
            //    MessageBox.Show("医师人数不够，智能排班失败");
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

        }

        private void listboxRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where = "";
            ssj = listboxRoom.SelectedItem.ToString();
            if (ssj == "全部手术间")
                where = "";
            else
                where = "and oroom='" + ssj + "'";
            ds = dal.GetPAIBAN(dtDataTime.Value.Date.ToString("yyyy-MM-dd"), where);
            dgvOTypesetting.DataSource = ds.DefaultView;
        }
        /// <summary>
        /// ping 连通服务器
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="TimeOut"></param>
        /// <returns></returns>
        public bool PingHost(string Address, int TimeOut = 1000)
        {
            try
            {
                using (System.Net.NetworkInformation.Ping PingSender = new System.Net.NetworkInformation.Ping())
                {
                    PingOptions Options = new PingOptions();
                    Options.DontFragment = true;
                    string Data = "test";
                    byte[] DataBuffer = Encoding.ASCII.GetBytes(Data);
                    PingReply Reply = PingSender.Send(Address, TimeOut, DataBuffer, Options);
                    if (Reply.Status == IPStatus.Success)
                        return true;
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("网络连接异常");
                return false;
            }

        }
        int checkNO;
        private void btnYES_Click(object sender, EventArgs e)
        {
            if (cbEye.Checked)
            {
                BindPaibanInfo(whereEyeOper + whereOstate1);
            }
            else
            {
                BindPaibanInfo(whereNotEyeOper + whereOstate1);
            }
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            if (cbEye.Checked)
            {
                BindPaibanInfo(whereEyeOper + whereOstate0);
            }
            else
            {
                BindPaibanInfo(whereNotEyeOper + whereOstate0);
            }
        }


        #endregion

        private void dgvOTypesetting_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvOTypesetting.CurrentCell.ColumnIndex == 1 || dgvOTypesetting.CurrentCell.ColumnIndex == 0)
            {
                if (adims_BLL.ValidationRegex.ValidteData(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }
        private void btnE_Click(object sender, EventArgs e)
        {
            string where = " and asae = '1'";
            ds = dal.GetPAIBAN(dtDataTime.Value.Date.ToString("yyyy-MM-dd"), where);
            dgvOTypesetting.DataSource = ds.DefaultView;
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
            range1.Value2 = "江 苏 盛 泽 人 民 医 院 手 术 通 知 单";
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
                //printPreviewDialog1.Document = printDocument1;
                //if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                //    printDocument1.Print();
                //printDocument1.DefaultPageSettings.Landscape = true;
                if (numericUpDown1.Text != "0")
                {
                    short DaYinNum = short.Parse(numericUpDown1.Text.Trim());//打印的份数
                    PrintDataGridViewLandscape.Print(dgvOTypesetting, "江苏盛泽医院手术通知单", "手术日期：" + dtDataTime.Text, DaYinNum);
                }
                else MessageBox.Show("请填入份数");
            }

        }
        int dgvRowCount = 0;
        int rowindex = 0;
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //rowindex = 0;
            //C#打印设置之设置横向打印   
            this.printDocument1.DefaultPageSettings.Landscape = true;
            //C#打印设置之设置彩色打印   
            this.printDocument1.DefaultPageSettings.Color = true;
            //C#打印设置之设置打印纸张类型和大小  
            this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("16K", 740, 1020);
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }
        int dyi = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 100, y = 50;
            Pen black = new Pen(Brushes.Black);
            black.Width = 1;
            Font HeiTi = new System.Drawing.Font("黑体", 9);
            Font Kti = new System.Drawing.Font("宋体", 7);
            e.Graphics.DrawString("手 术 通 知 单", new Font("宋体", 16, FontStyle.Bold), Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawString("手术日期：" + dtDataTime.Value.ToLongDateString(), HeiTi, Brushes.Black, new Point(x + 348, y + 30));
            e.Graphics.DrawString("共 " + dgvOTypesetting.Rows.Count.ToString() + " 台", HeiTi, Brushes.Black, new Point(x + 390, y + 45));
            y = y + 60;
            e.Graphics.DrawLine(black, new Point(x, y), new Point(970, y));
            e.Graphics.DrawString("手术间", HeiTi, Brushes.Black, new Point(x + 8, y + 5));
            e.Graphics.DrawString("台次", HeiTi, Brushes.Black, new Point(x + 65, y + 5));
            e.Graphics.DrawString("姓名", HeiTi, Brushes.Black, new Point(x + 105, y + 5));
            e.Graphics.DrawString("性别", HeiTi, Brushes.Black, new Point(x + 155, y + 5));
            e.Graphics.DrawString("年龄", HeiTi, Brushes.Black, new Point(x + 196, y + 5));
            e.Graphics.DrawString("床号", HeiTi, Brushes.Black, new Point(x + 237, y + 5));
            e.Graphics.DrawString("手术名称", HeiTi, Brushes.Black, new Point(x + 300, y + 5));
            e.Graphics.DrawString("手术医师", HeiTi, Brushes.Black, new Point(x + 430, y + 5));
            e.Graphics.DrawString("麻醉方法", HeiTi, Brushes.Black, new Point(x + 525, y + 5));
            e.Graphics.DrawString("麻醉医师", HeiTi, Brushes.Black, new Point(x + 630, y + 5));
            e.Graphics.DrawString("器械/巡回", HeiTi, Brushes.Black, new Point(x + 730, y + 5));
            e.Graphics.DrawString("备注", HeiTi, Brushes.Black, new Point(x + 800, y + 5));

            y = y + 25;
            for (; dyi < dgvOTypesetting.Rows.Count; dyi++)
            {
                e.Graphics.DrawLine(black, new Point(x, y), new Point(970, y));
                e.Graphics.DrawString(dgvOTypesetting[1, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 5, y + 5));//手术间
                e.Graphics.DrawString(dgvOTypesetting[2, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 75, y + 5));//台次
                e.Graphics.DrawString(dgvOTypesetting[4, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 105, y + 5));//姓名
                e.Graphics.DrawString(dgvOTypesetting[7, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 160, y + 5));//性别
                e.Graphics.DrawString(dgvOTypesetting[6, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 200, y + 3));//年龄
                e.Graphics.DrawString(dgvOTypesetting[5, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 235, y + 5));//床号
                e.Graphics.DrawString(dgvOTypesetting[9, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 283, y + 5));//手术名称
                e.Graphics.DrawString(dgvOTypesetting[10, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 435, y + 5));//手术医师
                e.Graphics.DrawString(dgvOTypesetting[12, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 500, y + 5));//麻醉方法
                e.Graphics.DrawString(dgvOTypesetting[13, dyi].Value.ToString() + "/" + dgvOTypesetting[14, dyi].Value.ToString() + "/" + dgvOTypesetting[15, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 620, y + 3));//麻醉医师
                e.Graphics.DrawString(dgvOTypesetting[16, dyi].Value.ToString() + "/" + dgvOTypesetting[17, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 700, y + 5));//器械
                e.Graphics.DrawString(dgvOTypesetting[18, dyi].Value.ToString() + "/" + dgvOTypesetting[19, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 800, y + 5));//巡回护士
                y = y + 25;
            }
            e.Graphics.DrawLine(black, new Point(x, y), new Point(970, y));
            int hangshu = dgvOTypesetting.Rows.Count + 1;
            //
            e.Graphics.DrawLine(black, new Point(x, y - hangshu * 25), new Point(x, y));
            //手术间
            e.Graphics.DrawLine(black, new Point(x + 50, y - hangshu * 25), new Point(x + 50, y));
            //台次
            e.Graphics.DrawLine(black, new Point(x + 100, y - hangshu * 25), new Point(x + 100, y));
            //姓名
            e.Graphics.DrawLine(black, new Point(x + 150, y - hangshu * 25), new Point(x + 150, y));
            //性别
            e.Graphics.DrawLine(black, new Point(x + 190, y - hangshu * 25), new Point(x + 190, y));
            //年龄
            e.Graphics.DrawLine(black, new Point(x + 230, y - hangshu * 25), new Point(x + 230, y));
            //床号
            e.Graphics.DrawLine(black, new Point(x + 280, y - hangshu * 25), new Point(x + 280, y));
            //手术名称
            e.Graphics.DrawLine(black, new Point(x + 430, y - hangshu * 25), new Point(x + 430, y));
            //手术医生
            e.Graphics.DrawLine(black, new Point(x + 490, y - hangshu * 25), new Point(x + 490, y));
            //麻醉方法
            e.Graphics.DrawLine(black, new Point(x + 610, y - hangshu * 25), new Point(x + 610, y));
            //麻醉医生
            e.Graphics.DrawLine(black, new Point(x + 700, y - hangshu * 25), new Point(x + 700, y));
            //器械
            e.Graphics.DrawLine(black, new Point(x + 790, y - hangshu * 25), new Point(x + 790, y));
            //巡回
            e.Graphics.DrawLine(black, new Point(x + 870, y - hangshu * 25), new Point(x + 870, y));
            dyi = 0;




















            //Pen black = new Pen(Brushes.Black);
            //black.Width = 1;
            //Font HeiTi = new System.Drawing.Font("黑体", 9);
            //Font SongTi = new System.Drawing.Font("宋体", 9);
            //Font Kti = new System.Drawing.Font("楷体", 7);
            //int x = 50, y = 0, y1 = 0;
            //for (int i = rowindex; i < dgvRowCount; i++)
            //{
            //    y = y + 30; y1 = y + 15;
            //    e.Graphics.DrawString("    昌吉州人民医院接手术病人通知单", new Font("宋体", 14, FontStyle.Bold), Brushes.Black, new Point(x + 120, y));
            //    y = y + 35; y1 = y + 15;
            //    e.Graphics.DrawString("日期：" + dtDataTime.Value.ToShortDateString(), SongTi, Brushes.Black, new Point(x + 30, y));
            //    e.Graphics.DrawLine(black, x + 60, y1, x + 170, y1);
            //    e.Graphics.DrawString("科室：" + dgvOTypesetting.Rows[rowindex].Cells["patdpm"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 180, y));
            //    e.Graphics.DrawLine(black, x + 210, y1, x + 320, y1);
            //    e.Graphics.DrawString("手术间：" + dgvOTypesetting.Rows[rowindex].Cells["oroom"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 330, y));
            //    e.Graphics.DrawLine(black, x + 370, y1, x + 470, y1);
            //    e.Graphics.DrawString("台次：" + dgvOTypesetting.Rows[rowindex].Cells["second"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 480, y));
            //    e.Graphics.DrawLine(black, x + 510, y1, x + 620, y1);
            //    y = y + 30; y1 = y + 15;
            //    e.Graphics.DrawString("姓名：" + dgvOTypesetting.Rows[rowindex].Cells["patname"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 30, y));
            //    e.Graphics.DrawLine(black, x + 60, y1, x + 170, y1);
            //    e.Graphics.DrawString("床号：" + dgvOTypesetting.Rows[rowindex].Cells["bedno"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 180, y));
            //    e.Graphics.DrawLine(black, x + 210, y1, x + 320, y1);
            //    e.Graphics.DrawString("性别：" + dgvOTypesetting.Rows[rowindex].Cells["patsex"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 330, y));
            //    e.Graphics.DrawLine(black, x + 360, y1, x + 470, y1);
            //    e.Graphics.DrawString("年龄：" + dgvOTypesetting.Rows[rowindex].Cells["patage"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 480, y));
            //    e.Graphics.DrawLine(black, x + 510, y1, x + 620, y1);
            //    y = y + 30; y1 = y + 15;
            //    e.Graphics.DrawString(" 核查患者身份、腕带；手术部位标识；交接药品并签名", SongTi, Brushes.Black, new Point(x + 30, y));
            //    y = y + 30; y1 = y + 15;
            //    e.Graphics.DrawString("接患者签名：", SongTi, Brushes.Black, new Point(x + 30, y));
            //    e.Graphics.DrawLine(black, x + 110, y1, x + 220, y1);
            //    e.Graphics.DrawString("病房护士签名：", SongTi, Brushes.Black, new Point(x + 230, y));
            //    e.Graphics.DrawLine(black, x + 320, y1, x + 420, y1);
            //    e.Graphics.DrawString("接患者时间：", SongTi, Brushes.Black, new Point(x + 430, y));
            //    e.Graphics.DrawLine(black, x + 520, y1, x + 620, y1);
            //    y = y + 30; y1 = y + 15;
            //    e.Graphics.DrawString("预麻醉护士签名：", SongTi, Brushes.Black, new Point(x + 30, y));
            //    e.Graphics.DrawLine(black, x + 130, y1, x + 220, y1);
            //    e.Graphics.DrawString("巡回护士签名：", SongTi, Brushes.Black, new Point(x + 230, y));
            //    e.Graphics.DrawLine(black, x + 300, y1, x + 420, y1);
            //    e.Graphics.DrawString("入手术间时间：", SongTi, Brushes.Black, new Point(x + 430, y));
            //    e.Graphics.DrawLine(black, x + 520, y1, x + 620, y1);
            //    y = y + 35; y1 = y + 15;
            //    rowindex++;
            //    if (rowindex % 5 == 0 && rowindex > 4)
            //    {
            //        e.HasMorePages = true;
            //        y = 0; y1 = 0;
            //        break;
            //    }
            //}
        }

        #endregion
   
        private void TextBoxDec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void dgvOTypesetting_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvOTypesetting.CurrentCell.ColumnIndex == 2)
                e.Control.KeyPress += new KeyPressEventHandler(TextBoxDec_KeyPress);
            else
                e.Control.KeyPress -= new KeyPressEventHandler(TextBoxDec_KeyPress);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dtDataTime.Value = dtDataTime.Value.AddDays(-1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dtDataTime.Value = dtDataTime.Value.AddDays(1);
        }

        private void TSJMWCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.SelectedCells.Count == 1)
            {
                string patID = this.dgvOTypesetting.CurrentRow.Cells["PATID"].Value.ToString();
                if (bll.GetMZJLDbyPatid(patID).Rows.Count == 0)
                {
                    string id = this.dgvOTypesetting.CurrentRow.Cells["ID"].Value.ToString();

                    string mzfs = this.dgvOTypesetting.CurrentRow.Cells["Amethod"].Value.ToString();
                    if (MessageBox.Show("确定这台手术完成了吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        if (mzfs == "局部浸润麻醉")
                        {
                            int b = bll.InsertMZJLD(patID, DateTime.Now);
                            int a = at.UpdateZT(id);
                            if (a > 0 && b > 0)
                            {
                                MessageBox.Show("局麻手术信息完成！");
                                string sqlWhere = "";
                                BindPaibanInfo(sqlWhere);//绑定列表
                            }
                            else
                                MessageBox.Show("失败");
                        }

                    }
                }
            }
        }
        private void TSQXSSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.SelectedCells.Count == 1)
            {
                string patid = dgvOTypesetting.CurrentRow.Cells["patid"].Value.ToString();
                string patname = dgvOTypesetting.CurrentRow.Cells["patname"].Value.ToString();
                string id = dgvOTypesetting.CurrentRow.Cells["ID"].Value.ToString();
                string mzfs = dgvOTypesetting.CurrentRow.Cells["Amethod"].Value.ToString();
                if (MessageBox.Show("确定取消这台手术吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    int num = at.ZTSS1(patid);
                    if (num > 0)
                    {
                        MessageBox.Show("手术已经开始或者已完成  操作失败");
                        return;
                    }
                    int a = dal.UpdatePaibanCancel(patid);
                    if (a > 0)
                    {
                        string msg = Program.Customer.user_name + " 取消了 " + patid + "--" + patname +
                            " 的手术，取消时间为：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        SaveLog(msg);
                        string sqlWhere = "";
                        BindPaibanInfo(sqlWhere);//绑定列表

                        #region 发送HL7
                        string message = AppendHL7stringCancelPaiban(patid);
                        UserFunction.SaveLogHL7(message);
                        string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];
                        //if (UserFunction.PingHost(HL7IPaddress))
                        if (true)
                        {
                            if (message.Length > 0)
                            {
                                string HL7port = ConfigurationManager.AppSettings["HL7port"];
                                SenderRoutingLib.SocketSender send = new SenderRoutingLib.SocketSender();
                                object objResult;
                                int iResult = 0;
                                int count = 1;
                                if (count < 10)
                                {
                                    new System.Threading.Thread(o =>
                                    {
                                        for (int i = 0; i < count; i++)
                                        {
                                            objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                            string ack = objResult == null ? string.Empty : objResult.ToString();
                                            if (ack.Contains("AA"))
                                            {
                                                iResult++;
                                                UserFunction.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                                                SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                            }
                                            else
                                            {
                                                iResult++;
                                                UserFunction.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                                SetText(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                            }
                                        }
                                    }).Start();
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("IP地址或端口错误");
                        }
                        #endregion
                        //BackToHisExitOper(patid);//反馈HIS手术取消
                    }
                    else
                        MessageBox.Show("操作失败");
                }
            }
        }
        public void SaveLog(string msg)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(Application.StartupPath + @"\CancelOper.txt"))
                {
                    sw.WriteLine(msg);
                    sw.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void BackToHisExitOper(string patid)
        {
            //string xmlString = hisWS.OperCancle(patid);//手术是否取消
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.LoadXml(xmlString);
            //string result = "";
            //XmlNodeList nodeList = xmldoc.SelectSingleNode("Szyy").ChildNodes;
            //foreach (XmlNode node in nodeList)//遍历所有子节点
            //{
            //    if (node.Name == "Status")
            //    {
            //        result = node.InnerText;
            //        break;
            //    }
            //}
            //if (result != "1")
            //    MessageBox.Show("反馈失败");
        }

        private void TSQRPBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (dgvOTypesetting.SelectedCells.Count == 1)
                {
                    string patid = dgvOTypesetting.CurrentRow.Cells["patid"].Value.ToString();
                    string Oroom = dgvOTypesetting.CurrentRow.Cells["Oroom"].Value.ToString();
                    string Osecond = dgvOTypesetting.CurrentRow.Cells["second"].Value.ToString();
                    if (Oroom == "" && Osecond == "")
                    {
                        MessageBox.Show("手术间和台次不能都为空");
                        return;
                    }

                    string message = AppendHL7stringConfig(patid);

                    UserFunction.SaveLogHL7(message);
                    string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];
                    //if (message.Length > 0 && UserFunction.PingHost(HL7IPaddress))
                    if (true)
                    {

                        string HL7port = ConfigurationManager.AppSettings["HL7port"];
                        SenderRoutingLib.SocketSender send = new SenderRoutingLib.SocketSender();
                        object objResult;
                        int iResult = 0;
                        int count = 1;
                        if (count < 10)
                        {
                            new System.Threading.Thread(o =>
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                    string ack = objResult == null ? string.Empty : objResult.ToString();
                                    if (ack.Contains("AA"))
                                    {
                                        iResult++;
                                        UserFunction.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                    else
                                    {
                                        iResult++;
                                        UserFunction.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        SetText(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                }
                            }).Start();
                        }
                        else
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                new System.Threading.Thread(o =>
                                {
                                    for (int i = 0; i < count / 10; i++)
                                    {
                                        objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                        string ack = objResult == null ? string.Empty : objResult.ToString();
                                        if (ack.Contains("AA"))
                                        {
                                            iResult++;
                                            SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                        else
                                        {
                                            iResult++;
                                            SetText(string.Format("\r\n发送失败，错误信息：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                    }
                                }).Start();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("IP地址或端口错误");
                    }
                }
                else
                    MessageBox.Show("请选择一位病人");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            #region
            //if (dgvOTypesetting.SelectedCells.Count == 1)
            //{
            //    DataGridViewRow dgvRow = dgvOTypesetting.CurrentRow;
            //    string patid = dgvRow.Cells["patid"].Value.ToString();
            //    string room = dgvRow.Cells["Oroom"].Value.ToString();
            //    string send = dgvRow.Cells["second"].Value.ToString();
            //    if (room != "" || send != "")
            //    {
            //        int ostateNum = Convert.ToInt32(dal.GetOstate(patid));
            //        if (ostateNum == 0)
            //        {
            //            int Jieguo = dal.UpdatePaibanInfo(1, patid);//修改成 已排班
            //            if (Jieguo < 1)
            //            {
            //                MessageBox.Show("排班确认失败！");
            //                return;
            //            }
            //            else
            //            {
            //                MessageBox.Show("排班确认成功！");
            //                string HISresult = BackToHisPaibanInfo(dgvRow);
            //                if (HISresult != "1")
            //                {
            //                    MessageBox.Show("反馈失败！");
            //                }
            //            }
            //        }
            //        else if (ostateNum > 0)
            //        {
            //            MessageBox.Show("已排班确认！");
            //            return;
            //        }                  
            //    }
            //    else
            //    {
            //        MessageBox.Show("手术间和台次不能为空！");
            //    }
            //}
            #endregion

        }

        delegate void MyInvoke(string str);
        private void SetText(string s)
        {
            if (lbOutPut.InvokeRequired)
            {
                MyInvoke _myInvoke = new MyInvoke(SetText);
                this.Invoke(_myInvoke, new object[] { s });
            }
            else
            {
                this.lbOutPut.Text = s;
                Application.DoEvents();
            }
        }

        private string AppendHL7stringSRM(DataGridViewRow dgvRow)
        {
            ORM_O01 orm = new ORM_O01();
            #region 组装消息头
            orm.MSH.MessageType.MessageType.Value = "SRM";
            orm.MSH.MessageType.TriggerEvent.Value = "S01";
            orm.MSH.MessageType.MessageStructure.Value = "SRM_S01";
            orm.MSH.FieldSeparator.Value = MessageConstant.FieldSeparator;
            orm.MSH.SendingApplication.NamespaceID.Value = "SSMZ1";
            orm.MSH.SendingFacility.NamespaceID.Value = "SSMZ1";
            orm.MSH.ReceivingApplication.NamespaceID.Value = "MediII";
            orm.MSH.ReceivingFacility.NamespaceID.Value = "MediII";
            orm.MSH.EncodingCharacters.Value = MessageConstant.EncodingCharacters;
            orm.MSH.VersionID.VersionID.Value = MessageConstant.VersionID;
            orm.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Now);
            orm.MSH.MessageControlID.Value = MediII.Common.GUIDHelper.NewGUID();
            orm.MSH.ProcessingID.ProcessingID.Value = MessageConstant.ProcessingID;

            #endregion

            #region ARQ
            String ARQ = "ARQ|";
            string PATID = dgvRow.Cells["patid"].Value.ToString();
            pbModel.PATID = PATID + "||||";
            ARQ += pbModel.PATID;
            DataTable dtResult = dal.GetPaiban(PATID);
            DataRow dr = dtResult.Rows[0];

            pbModel.Amethod = dtResult.Rows[0]["Amethod"].ToString() + "|";
            ARQ += pbModel.Amethod;
            pbModel.Olevel = dtResult.Rows[0]["Olevel"].ToString() + "||";
            ARQ += pbModel.Olevel;

            pbModel.ASAE = dtResult.Rows[0]["ASAE"].ToString();
            if (pbModel.ASAE == "1")
            {
                pbModel.ASAE = "1^急诊手术" + "|||";
            }
            else if (pbModel.ASAE == "2")
            {
                pbModel.ASAE = "2^择期手术" + "|||";
            }
            else
            {
                pbModel.ASAE = "3^限期手术" + "|||";
            }
            ARQ += pbModel.ASAE;
            pbModel.Odate = Convert.ToDateTime(dtResult.Rows[0]["Odate"].ToString()).ToString("yyyyMMddHHmmss") + "^";
            ARQ += pbModel.Odate;
            if (dtResult.Rows[0]["ApplyDate"].ToString() == "")
            {
                pbModel.ApplyDate = Convert.ToDateTime(dtResult.Rows[0]["Odate"].ToString()).AddDays(-1).ToString("yyyyMMddHHmmss") + "|";
            }
            else
            {
                pbModel.ApplyDate = Convert.ToDateTime(dtResult.Rows[0]["ApplyDate"].ToString()).ToString("yyyyMMddHHmmss") + "|";
            }
            ARQ += pbModel.ApplyDate;
            pbModel.GL = dtResult.Rows[0]["GL"].ToString() + "|||";
            ARQ += pbModel.GL;
            pbModel.ApplyMan = dtResult.Rows[0]["ApplyMan"].ToString() + "||||";
            ARQ += pbModel.ApplyMan;
            ARQ += dtResult.Rows[0]["ApplyMan"].ToString() + "|| ";
            #endregion
            #region PID
            String PID = "PID||";
            pbModel.applyID = dtResult.Rows[0]["applyID"].ToString() + "|||";
            PID += pbModel.applyID;

            pbModel.patName = dtResult.Rows[0]["patName"].ToString();
            PID += pbModel.patName + "||||||||||||||||||||| ";

            #region 姓名转全拼
            StringBuilder sb = new StringBuilder();
            foreach (char c in pbModel.patName)
            {
                if (ChineseChar.IsValidChar(c))
                {
                    ChineseChar pinYinConvert = new ChineseChar(c);
                    string pinYin = pinYinConvert.Pinyins[0];
                    if (!string.IsNullOrEmpty(pinYin))
                    {
                        string word = pinYin.Substring(0, pinYin.Length - 1);
                        //首字母大写，其他小写
                        StringBuilder sbW = new StringBuilder(word.Length);
                        for (int n = 0; n < word.Length; n++)
                        {
                            string s = word.Substring(n, 1);
                            if (n == 0)
                                sbW.Append(s.ToUpper());
                            else
                                sbW.Append(s.ToLower());
                        }
                        sb.Append(sbW.ToString());
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            #endregion

            PID += sb.ToString() + "^" + pbModel.patName + "|||";

            pbModel.patsex = dtResult.Rows[0]["patsex"].ToString();
            if (pbModel.patsex == "男")
            {
                pbModel.patsex = "M" + "||| ";
            }
            else
                pbModel.patsex = "F" + "||| ";

            PID += pbModel.patsex;
            #endregion
            #region PV1|
            String PV1 = "PV1||";
            if (dr["IsZhuYuan"].ToString() == "1")
            {
                PV1 += "I|||||||||||||||||";
            }
            else
            {
                PV1 += "O|||||||||||||||||";
            }
            #endregion
            #region OBX1+OBX2
            String OBX1 = "OBX|||";
            pbModel.Weight = dtResult.Rows[0]["PatWeight"].ToString() + "^BODY WEIGHT" + "||||||||F";
            OBX1 += pbModel.Weight;
            String OBX2 = "OBX|||";
            pbModel.Height = dtResult.Rows[0]["PatHeight"].ToString() + "^BODY HEIGHT" + "||||||||F";
            OBX2 += pbModel.Height;
            #endregion
            #region RGS|
            String RGS = "RGS|1";
            #endregion
            #region AIS|
            String AIS = "AIS|||";
            pbModel.Oname = dtResult.Rows[0]["Oname"].ToString() + "|||||||";
            AIS += pbModel.Oname;
            pbModel.GL = dtResult.Rows[0]["GL"].ToString();
            AIS += pbModel.GL;
            pbModel.Oroom = dtResult.Rows[0]["ORoom"].ToString() + "^";
            AIS += pbModel.Oroom;
            pbModel.Osecond = dtResult.Rows[0]["Second"].ToString() + "|";
            AIS += pbModel.Osecond;
            #endregion
            #region   AIP1
            String AIP1 = "AIP|1|||";
            pbModel.OS = dtResult.Rows[0]["OS"].ToString();
            AIP1 += pbModel.Oname;
            #endregion
            #region  AIP2
            String AIP2 = "AIP|2|||";
            pbModel.XSHS1 = dtResult.Rows[0]["ON1"].ToString();
            AIP2 += pbModel.XSHS1;
            #endregion
            #region AIP3
            String AIP3 = "AIP|3|||";
            pbModel.XSHS2 = dtResult.Rows[0]["ON2"].ToString();
            AIP3 += pbModel.XSHS2;
            #endregion
            #region AIP4
            String AIP4 = "AIP|4|||";
            pbModel.XHHS1 = dtResult.Rows[0]["SN1"].ToString();
            AIP4 += pbModel.XHHS1;
            #endregion
            #region AIP5
            String AIP5 = "AIP|5|||";
            pbModel.XHHS2 = dtResult.Rows[0]["SN2"].ToString();
            AIP5 += pbModel.XHHS2;
            #endregion
            #region AIP11
            String AIP11 = "AIP|11|||";
            pbModel.AP1 = dtResult.Rows[0]["AP1"].ToString();
            AIP11 += pbModel.AP1;
            #endregion
            #region AIP12
            String AIP12 = "AIP|12|||";
            pbModel.AP2 = dtResult.Rows[0]["AP2"].ToString();
            AIP12 += pbModel.AP2;
            #endregion
            #region AIP13
            String AIP13 = "AIP|13|||";
            pbModel.AP3 = dtResult.Rows[0]["AP3"].ToString();
            AIP13 += pbModel.AP3;
            #endregion

            #region 转换消息对象为字符串
            String hl7Message = ARQ + PID + PV1 + OBX1 + OBX2 + AIP1 + AIP2 + AIP3 + AIP4 + AIP5 + AIP11 + AIP12 + AIP13;
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string message = parser.Encode(orm) + hl7Message;
            return message;
            #endregion

        }

        /// <summary>
        /// 取消手术
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        private string AppendHL7stringCancelPaiban(string patid)
        {
            ORM_O01 orm = new ORM_O01();
            #region 组装消息头
            orm.MSH.MessageType.MessageType.Value = "SIU";
            orm.MSH.MessageType.TriggerEvent.Value = "S20";
            orm.MSH.MessageType.MessageStructure.Value = "SIU_S20";
            orm.MSH.FieldSeparator.Value = MessageConstant.FieldSeparator;
            orm.MSH.SendingApplication.NamespaceID.Value = "SSMZ1";
            orm.MSH.SendingFacility.NamespaceID.Value = "SSMZ1";
            orm.MSH.ReceivingApplication.NamespaceID.Value = "MediII";
            orm.MSH.ReceivingFacility.NamespaceID.Value = "MediII";
            orm.MSH.EncodingCharacters.Value = MessageConstant.EncodingCharacters;
            orm.MSH.VersionID.VersionID.Value = MessageConstant.VersionID;
            orm.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Now);
            orm.MSH.MessageControlID.Value = MediII.Common.GUIDHelper.NewGUID();
            orm.MSH.ProcessingID.ProcessingID.Value = MessageConstant.ProcessingID;

            #endregion

            DataTable dtResult = dal.GetPaiban(patid);
            DataRow dr = dtResult.Rows[0];


            #region SCH|
            String SCH = "SCH||" + patid;
            SCH += "|||^取消手术安排^^^ " + "|||||";
            SCH += "^^^" + DateTime.Now.ToString("yyyyMMddHHmmss") + "|||||";
            SCH += Program.Customer.userno + "^^" + Program.Customer.user_name + "|||||";
            SCH += Program.Customer.userno + "^^" + Program.Customer.user_name;
            #endregion


            #region 转换消息对象为字符串
            String hl7Message = SCH;
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string message = parser.Encode(orm) + hl7Message;
            return message;
            #endregion

        }
        /// <summary>
        /// 取消手术
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        private string AppendHL7stringCancelOper(string patid)
        {
            ORM_O01 orm = new ORM_O01();
            #region 组装消息头
            orm.MSH.MessageType.MessageType.Value = "SRM";
            orm.MSH.MessageType.TriggerEvent.Value = "S04";
            orm.MSH.MessageType.MessageStructure.Value = "SRM_S04";
            orm.MSH.FieldSeparator.Value = MessageConstant.FieldSeparator;
            orm.MSH.SendingApplication.NamespaceID.Value = "SSMZ1";
            orm.MSH.SendingFacility.NamespaceID.Value = "SSMZ1";
            orm.MSH.ReceivingApplication.NamespaceID.Value = "HIS";
            orm.MSH.ReceivingFacility.NamespaceID.Value = "HIS";
            orm.MSH.EncodingCharacters.Value = MessageConstant.EncodingCharacters;
            orm.MSH.VersionID.VersionID.Value = MessageConstant.VersionID;
            orm.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Now);
            orm.MSH.MessageControlID.Value = MediII.Common.GUIDHelper.NewGUID();
            orm.MSH.ProcessingID.ProcessingID.Value = MessageConstant.ProcessingID;

            #endregion

            DataTable dtResult = dal.GetPaiban(patid);
            DataRow dr = dtResult.Rows[0];

            String PID = "PID||||||||||||||||||||||||||||||||" + "\n";
            String PV1 = "PV1||";
            if (dr["IsZhuYuan"].ToString() == "1")
            {
                PV1 += "I||||||||||||||||||";
            }
            else
            {
                PV1 += "O||||||||||||||||||";
            }
            #region ARQ|
            String ARQ = "ARQ|" + dr["patid"].ToString() + "||||||||||";
            ARQ += DateTime.Now.ToString("yyyyMMddHHmmss") + "||||";
            ARQ += Program.Customer.userno + "||||";
            ARQ += Program.Customer.userno + "\n";
            #endregion


            #region 转换消息对象为字符串
            String hl7Message = ARQ + PID + PV1;
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string message = parser.Encode(orm) + hl7Message;
            return message;
            #endregion

        }
        /// <summary>
        /// 确定手术安排
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        private string AppendHL7stringConfig(string patid)
        {
            ORM_O01 orm = new ORM_O01();
            DataTable dtResult = dal.GetPaiban(patid);
            DataRow dr = dtResult.Rows[0];
            int ostateNum = UserFunction.ToInt32(dr["Ostate"].ToString());
            UserFunction.SaveLogHL7("状态" + dr["Ostate"].ToString());
            string SCH_1 = "";
            #region 组装消息头
            if (ostateNum == 0)
            {
                SCH_1 = "确定手术安排";
                orm.MSH.MessageType.TriggerEvent.Value = "S18";
                orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            }
            else
            {
                SCH_1 = "修改手术安排";
                orm.MSH.MessageType.TriggerEvent.Value = "S19";
                orm.MSH.MessageType.MessageStructure.Value = "SIU_S12";
            }


            orm.MSH.MessageType.MessageType.Value = "SIU";

            orm.MSH.FieldSeparator.Value = MessageConstant.FieldSeparator;
            orm.MSH.SendingApplication.NamespaceID.Value = "SSMZ1";
            orm.MSH.SendingFacility.NamespaceID.Value = "SSMZ1";
            orm.MSH.ReceivingApplication.NamespaceID.Value = "MediII";
            orm.MSH.ReceivingFacility.NamespaceID.Value = "MediII";
            orm.MSH.EncodingCharacters.Value = MessageConstant.EncodingCharacters;
            orm.MSH.VersionID.VersionID.Value = MessageConstant.VersionID;
            orm.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Now);
            orm.MSH.MessageControlID.Value = MediII.Common.GUIDHelper.NewGUID();
            orm.MSH.ProcessingID.ProcessingID.Value = MessageConstant.ProcessingID;

            #endregion

            int Jieguo = dal.UpdatePaibanConfig(patid);//修改成 已排班

            #region SCH|
            String SCH = "SCH||||||^" + SCH_1 + "^^^原因" + "|||||";
            SCH += "^^^" + Convert.ToDateTime(dr["odate"]).ToString("yyyyMMddHHmmss") + "|||||";
            SCH += Program.Customer.userno + "^^" + Program.Customer.user_name + "|||";
            SCH += "^^^^^^^^" + dr["patdpm"].ToString() + "|";
            SCH += Program.Customer.userno + "^^" + Program.Customer.user_name + "||||||";
            SCH += dr["patid"].ToString() + "\n";
            #endregion

            String PID = dr["PidInfo"].ToString();

            String PV1 = dr["Pv1Info"].ToString();
            #region RGS|
            String RGS = "RGS|1" + "\n";
            #endregion

            #region AIS|
            String AIS = "AIS|1||";
            AIS += dr["OperNo"].ToString() + "^" + dr["Oname"].ToString()+"|||||||";
            AIS += dr["Oroom"].ToString() + "^^^" + dr["Second"].ToString() + "|" + "\n";
            #endregion

         

            #region 手术医生
            String AIP = "AIP|1||";
            AIP += dr["OsNo"].ToString() + "^" + dr["OS"].ToString() + "|主刀医生" + "\n";
            #endregion

            #region 护士
            DataTable dt = dal.GetUserNoByName(dr["SN1"].ToString());
            string UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|2||";
            AIP += UserNO + "^" + dr["SN1"].ToString() + "|4^洗手护士" + "\n";

            dt = dal.GetUserNoByName(dr["SN2"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|3||";
            AIP += UserNO + "^" + dr["SN2"].ToString() + "|4^洗手护士" + "\n";

            dt = dal.GetUserNoByName(dr["ON1"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|4||";
            AIP += UserNO + "^" + dr["ON1"].ToString() + "|5^巡回护士" + "\n";

            dt = dal.GetUserNoByName(dr["ON2"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|5||";
            AIP += UserNO + "^" + dr["ON2"].ToString() + "|5^巡回护士" + "\n";

            dt = dal.GetUserNoByName(dr["ON3"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|6||";
            AIP += UserNO + "^" + dr["ON3"].ToString() + "|5^巡回护士" + "\n";
            #endregion

            #region 手术助手
            AIP += "AIP|7||";
            AIP += dr["OA1No"].ToString() + "^" + dr["OA1"].ToString() + "|2^助理医生" + "\n";
            AIP += "AIP|8||";
            AIP += dr["OA2No"].ToString() + "^" + dr["OA2"].ToString() + "|2^助理医生" + "\n";
            AIP += "AIP|9||";
            AIP += dr["OA3No"].ToString() + "^" + dr["OA3"].ToString() + "|2^助理医生" + "\n";
            #endregion
            #region 麻醉医生
            dt = dal.GetUserNoByName(dr["AP1"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|11||";
            AIP += UserNO + "^" + dr["AP1"].ToString() + "|麻醉医师" + "\n";
            dt = dal.GetUserNoByName(dr["AP2"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|12||";
            AIP += UserNO + "^" + dr["AP2"].ToString() + "|麻醉医师" + "\n";
            dt = dal.GetUserNoByName(dr["AP3"].ToString());
            UserNO = " ";
            if (dt.Rows.Count > 0) UserNO = dt.Rows[0][0].ToString();
            AIP += "AIP|13||";
            AIP += UserNO + "^" + dr["AP3"].ToString() + "|麻醉医师" + "\n";
            #endregion



            #region 转换消息对象为字符串
            String hl7Message = SCH + PID + PV1 + RGS + AIS + AIP;
            NHapi.Base.Parser.PipeParser parser = new NHapi.Base.Parser.PipeParser();
            string message = parser.Encode(orm) + hl7Message;
            return message;
            #endregion

        }
        OperAisthService hisWS = new OperAisthService();
        private string BackToHisPaibanInfo(DataGridViewRow dgvRow)
        {
            string result = "";
            string second = dgvRow.Cells["second"].Value.ToString();
            string oroom = dgvRow.Cells["oroom"].Value.ToString();
            string ApplyID = dgvRow.Cells["patid"].Value.ToString();
            var doc = new XDocument(
                                new XElement("Params",
                                        new XElement("ApplyID", ApplyID),
                                        new XElement("ApplyDate", dtDataTime.Value.ToString("yyyy-MM-dd")),
                                        new XElement("ReplyDate", dtDataTime.Value.ToString("yyyy-MM-dd HH:mm:00")),
                                        new XElement("OPPlaceName", oroom),
                                        new XElement("OPNo", second)
                                                )
                                        );
            string operinfo = doc.ToString();
            string resultXML = hisWS.OperationConfirm(operinfo);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(resultXML);

            XmlNodeList nodeList = xmldoc.SelectSingleNode("Szyy").ChildNodes;
            foreach (XmlNode node in nodeList)//遍历所有子节点
            {
                if (node.Name == "Status")
                {
                    result = node.InnerText;
                    break;
                }
            }
            return result;
        }

        private void btnHisRefresh_Click(object sender, EventArgs e)
        {
            bool pingIP = PingHost("10.0.100.87");
            if (pingIP == true)
            {
                HisDataBind();
            }
            else
                MessageBox.Show("网络中断请检查网络"); ;
        }
        private void btnAllConfig_Click(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            timer1.Start();

            Thread thead = null;
            thead = new Thread(new ThreadStart(SendHL7toHIS));
            thead.IsBackground = true;
            thead.Start();


            #region
            //int sucNo = 0; 
            //int dgvNo=0;
            //foreach (DataGridViewRow dgvRow in dgvOTypesetting.Rows)
            //{
            //    string room = dgvRow.Cells["Oroom"].Value.ToString();
            //    string send = dgvRow.Cells["second"].Value.ToString();
            //    if (room != "" || send != "")
            //    {
            //        string patid = dgvRow.Cells["patid"].Value.ToString();
            //        int ostateNum = Convert.ToInt32(dal.GetOstate(patid));
            //        if (ostateNum < 1)
            //        {
            //            int Jieguo = dal.UpdatePaibanInfo(1, patid);//修改成 已排班
            //            if (Jieguo < 1)
            //            {
            //                MessageBox.Show("排班确认失败！");
            //                return;
            //            }

            //            else
            //            {
            //                string HISresult = BackToHisPaibanInfo(dgvRow);
            //                if (HISresult == "1")
            //                {
            //                    sucNo++;
            //                }
            //            }
            //            dgvNo++;
            //        }

            //    }
            //}
            //if (sucNo == dgvNo)
            //{
            //    MessageBox.Show("排班确认成功！");
            //}
            //else
            //    MessageBox.Show("排班反馈HIS失败！");
            #endregion
        }

        bool Flag = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            if (Flag)
            {
                timer1.Stop();
                MessageBox.Show("发送成功！");
            }
        }
        public void SendHL7toHIS()
        {
            Flag = false;
            foreach (DataGridViewRow dgvRow in dgvOTypesetting.Rows)
            {
                string patid = dgvRow.Cells["patid"].Value.ToString();
                string Oroom = dgvRow.Cells["Oroom"].Value.ToString();
                string Osecond = dgvRow.Cells["second"].Value.ToString();
                if (Oroom != "" || Osecond != "")
                {
                    int ostateNum = Convert.ToInt32(dal.GetOstate(patid));
                    if (ostateNum == 0)
                    {
                        int Jieguo = dal.UpdatePaibanInfo(1, patid);//修改成 已排班

                    }
                    string message = AppendHL7stringConfig(patid);
                    UserFunction.SaveLogHL7(message);
                    string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];
                    if (true)
                    {
                        string HL7port = ConfigurationManager.AppSettings["HL7port"];
                        SenderRoutingLib.SocketSender send = new SenderRoutingLib.SocketSender();
                        object objResult;
                        int iResult = 1;
                        int count = 1;
                        if (count < 10)
                        {
                            new System.Threading.Thread(o =>
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                    string ack = objResult == null ? string.Empty : objResult.ToString();
                                    if (ack.Contains("AA"))
                                    {
                                        iResult++;
                                        UserFunction.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                    else
                                    {
                                        iResult--;
                                        UserFunction.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        SetText(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                }
                            }).Start();
                        }
                        else
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                new System.Threading.Thread(o =>
                                {
                                    for (int i = 0; i < count / 10; i++)
                                    {
                                        objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                        string ack = objResult == null ? string.Empty : objResult.ToString();
                                        if (ack.Contains("AA"))
                                        {
                                            iResult++;
                                            UserFunction.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                            SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                        else
                                        {
                                            iResult--;
                                            UserFunction.SaveLogHL7(string.Format("\r\n发送失败，错误信息：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                            SetText(string.Format("\r\n发送失败，错误信息：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                        }
                                    }
                                }).Start();
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show("IP地址或端口错误");
                    }
                    //Thread.Sleep(200);
                }
                Flag = true;
            }
        }

        private void hbbrsqdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.SelectedCells.Count == 1)
            {
                if (dgvOTypesetting.CurrentRow.Cells["asae"].Value.ToString() == "1")
                {
                    DataGridViewRow dgvRow = dgvOTypesetting.CurrentRow;
                    string patid = dgvRow.Cells["patid"].Value.ToString();
                    string patname = dgvRow.Cells["patname"].Value.ToString();
                    DateTime odate = DateTime.Parse(dtDataTime.Value.ToString("yyyy-MM-dd"));
                    DataTable dt = dal.GetPaibanDayAndName(patname, odate);
                    if (dt.Rows.Count == 2)
                    {
                        int flag1 = 0;
                        int flag2 = 0;
                        string PatID1 = dt.Rows[0]["PATID"].ToString();
                        string PatID2 = dt.Rows[1]["PATID"].ToString();
                        if (PatID1.Length > PatID2.Length)
                        {
                            flag2 = dal.UpdateMzjld_patid(PatID1, PatID2);
                            flag1 = dal.DeletePaibanPatidOdate(PatID2, odate);
                        }
                        else if (PatID1.Length < PatID2.Length)
                        {
                            flag2 = dal.UpdateMzjld_patid(PatID2, PatID1);
                            flag1 = dal.DeletePaibanPatidOdate(PatID1, odate);
                        }
                        if (flag1 > 0 && flag2 > 0)
                        {
                            dal.UpdatePaibanInfo(3, PatID1);
                            MessageBox.Show("整合完成！");
                            BindPaibanInfo("");
                        }
                    }
                    else
                        MessageBox.Show("请选择急诊病人！");
                }
            }
        }
        private void UpdateDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.SelectedCells.Count == 1)
            {
                DataGridViewRow dgvRow = dgvOTypesetting.CurrentRow;
                string patid = dgvRow.Cells["patid"].Value.ToString();
                UpdateOdate f1 = new UpdateOdate(patid);
                f1.ShowDialog();
                BindPaibanInfo("");
            }
        }
        private void dgvOTypesetting_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                lbPatName.Text = dgvOTypesetting.CurrentRow.Cells["patname"].Value.ToString();
                lbKeshi.Text = dgvOTypesetting.CurrentRow.Cells["patdpm"].Value.ToString();
                lbOS.Text = dgvOTypesetting.CurrentRow.Cells["os"].Value.ToString();
                lbSSMC.Text = dgvOTypesetting.CurrentRow.Cells["oname"].Value.ToString();
                if (dgvOTypesetting.CurrentRow.Cells["asae"].Value.ToString() == "1")
                    lbASAE.Text = "急诊手术";
                else
                    lbASAE.Text = "择期手术";
                int ColIndex = dgvOTypesetting.CurrentCell.ColumnIndex;
                if (ColIndex == 1)
                {

                    cmbXHHS.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZYS.Visible = false;
                    Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
                    cmbRoom.Left = rect.Left;
                    cmbRoom.Top = rect.Top;
                    cmbRoom.Width = rect.Width;
                    cmbRoom.Height = rect.Height;
                    cmbRoom.Visible = true;
                    cmbRoom.DroppedDown = true;
                }

                else if (ColIndex == 12 || ColIndex == 13 || ColIndex == 14 || ColIndex == 15)
                {
                    cmbXHHS.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZYS.Visible = false;
                    Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
                    cmbXHHS.Left = rect.Left;
                    cmbXHHS.Top = rect.Top;
                    cmbXHHS.Width = rect.Width;
                    cmbXHHS.Height = rect.Height;
                    cmbXHHS.Visible = true;

                    cmbXHHS.DroppedDown = true;
                }
                else if (ColIndex == 17 || ColIndex == 18 || ColIndex == 19)
                {
                    cmbXHHS.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZYS.Visible = false;
                    Rectangle rect = dgvOTypesetting.GetCellDisplayRectangle(dgvOTypesetting.CurrentCell.ColumnIndex, dgvOTypesetting.CurrentCell.RowIndex, false);
                    cmbMZYS.Left = rect.Left;
                    cmbMZYS.Top = rect.Top;
                    cmbMZYS.Width = rect.Width;
                    cmbMZYS.Height = rect.Height;
                    cmbMZYS.Visible = true;

                    cmbMZYS.DroppedDown = true;
                }
                else
                {
                    cmbXHHS.Visible = false;
                    cmbRoom.Visible = false;
                    cmbMZYS.Visible = false;
                }
            }
        }

        private void btnPrintSort_Click(object sender, EventArgs e)
        {
            string sqlwhere = " order by oroom,Convert(int,second) asc";
            ds = dal.GetPAIBAN(dtDataTime.Value.Date.ToString("yyyy-MM-dd"), sqlwhere);
            dgvOTypesetting.DataSource = ds.DefaultView;
        }

        private void cbEye_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEye.Checked)
            {
                BindPaibanInfo(whereEyeOper);
            }
            else
            {
                BindPaibanInfo(whereNotEyeOper);
            }
        }

        private void hcsydjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvRow = dgvOTypesetting.CurrentRow;
            string patid = dgvRow.Cells["patid"].Value.ToString();
            DataTable dt = dal.GetMZJLDbyPatID(patid);
            if (dt.Rows.Count > 0)
            {
                int mzjldId = Convert.ToInt32(dt.Rows[0]["id"]);
                MZJLDs.ConsumablesUseForm f1 = new MZJLDs.ConsumablesUseForm(mzjldId, patid);
                f1.ShowDialog();
            }
            else
            {
                MessageBox.Show("该病人不存在麻醉记录单，请先记录麻醉记录单");
            }
        }




    }
}