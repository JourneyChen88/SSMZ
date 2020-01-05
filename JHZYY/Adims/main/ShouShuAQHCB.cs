using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.CGG
{
    public partial class ShouShuAQHCB : Form
    {
        adims_BLL.mz bll = new adims_BLL.mz();
        adims_DAL.mz dal = new adims_DAL.mz();
        string padID;
        bool IsRead = false;  
        public ShouShuAQHCB(string id)
        {
            padID = id;
            InitializeComponent();
        }

        private void ShouShuAQHCB_Load(object sender, EventArgs e)
        {
            this.Text = "手术安全核查表";
            info();
            yishi();
            mazuiyishi();

        }
        #region<<显示信息>>
        private void info()
        {
            try
            {
                DataTable dt = dal.GetShaqhcbiao(padID);
                DataRow dr1 = dt.Rows[0];
                txtbKeShi.Text = dr1["Patdpm"].ToString();
                txtbChuangHao.Text = dr1["Patbedno"].ToString();
                txtbHZName.Text = dr1["Patname"].ToString();
                txtbZYH.Text = dr1["patid"].ToString();
                txtbMZSSMC.Text = dr1["Oname"].ToString();
            }
            // DataTable dt1 = dal.GetbaocunShaqhcbiao(padID);

             // if (dt.Rows.Count > 0)
            //{
            //    DataRow dr = dt1.Rows[0];
            //    if (Convert.ToString(dr["SHqshenfen"]).Contains("Y")) checkBo1.Checked = true;

              //    if (Convert.ToString(dr["SHqbuwei"]).Contains("Y")) checkBox1.Checked = true;
            //    if (Convert.ToString(dr["SHqmingcheng"]).Contains("Y")) checkBox3.Checked = true;
            //    if (Convert.ToString(dr["SHqtongyi"]).Contains("Y")) checkBox2.Checked = true;
            //    if (Convert.ToString(dr["SHqbiaoshi"]).Contains("是")) checkBox4.Checked = true;
            //    if (Convert.ToString(dr["SHqbiaoshi"]).Contains("否")) checkBox5.Checked = true;
            //    if (Convert.ToString(dr["SHqjcwc"]).Contains("Y")) checkBox6.Checked = true;

              //    if (Convert.ToString(dr["SHqjcjl"]).Contains("是")) checkBox7.Checked = true;
            //    if (Convert.ToString(dr["SHqjcjl"]).Contains("否")) checkBox8.Checked = true;

              //    if (Convert.ToString(dr["SHqwzxjc"]).Contains("是")) checkBox10.Checked = true;
            //    if (Convert.ToString(dr["SHqwzxjc"]).Contains("否")) checkBox9.Checked = true;

              //    if (Convert.ToString(dr["SHqpfzb"]).Contains("是")) checkBox12.Checked = true;
            //    if (Convert.ToString(dr["SHqpfzb"]).Contains("否")) checkBox11.Checked = true;

              //    if (Convert.ToString(dr["SHqjmtdjl"]).Contains("是")) checkBox14.Checked = true;
            //    if (Convert.ToString(dr["SHqjmtdjl"]).Contains("否")) checkBox13.Checked = true;

              //    if (Convert.ToString(dr["SHqhzgms"]).Contains("有")) checkBox16.Checked = true;
            //    if (Convert.ToString(dr["SHqhzgms"]).Contains("无")) checkBox15.Checked = true;

              //    if (Convert.ToString(dr["SHqqdhuxizangai"]).Contains("有")) checkBox18.Checked = true;
            //    if (Convert.ToString(dr["SHqqdhuxizangai"]).Contains("无")) checkBox19.Checked = true;
            //    if (Convert.ToString(dr["SHqqdhuxizangai"]).Contains("提供支持")) checkBox62.Checked = true;

              //    if (Convert.ToString(dr["SHqkjywpsqk"]).Contains("是")) checkBox20.Checked = true;
            //    if (Convert.ToString(dr["SHqkjywpsqk"]).Contains("否")) checkBox17.Checked = true;

              //    if (Convert.ToString(dr["SHqsqbx"]).Contains("计划字体")) checkBox22.Checked = true;
            //    if (Convert.ToString(dr["SHqsqbx"]).Contains("异体输血")) checkBox21.Checked = true;
            //    if (Convert.ToString(dr["SHqsqbx"]).Contains("是")) checkBox24.Checked = true;
            //    if (Convert.ToString(dr["SHqsqbx"]).Contains("否")) checkBox23.Checked = true;

              //    if (Convert.ToString(dr["SHqqita"]).Contains("假体")) checkBox65.Checked = true;
            //    if (Convert.ToString(dr["SHqqita"]).Contains("植入物")) checkBox64.Checked = true;
            //    if (Convert.ToString(dr["SHqqita"]).Contains("影像学资料")) checkBox63.Checked = true;



              //    comboBox1.Text = dr["shoushuyisheng1"].ToString();
            //    comboBox2.Text = dr["mazuiyisheng1"].ToString();
            //    comboBox3.Text = dr["xunhuihushi1"].ToString();
            //    if (Convert.ToString(dr["SHpiqshenfen"]).Contains("Y")) checkBox27.Checked = true;
            //    if (Convert.ToString(dr["SHpiqshmc"]).Contains("Y")) checkBox26.Checked = true;
            //    if (Convert.ToString(dr["SHpiqshbw"]).Contains("Y")) checkBox25.Checked = true;

              //    if (Convert.ToString(dr["SHpiqyscs"]).Contains("预计手术时间")) checkBox28.Checked = true;
            //    if (Convert.ToString(dr["SHpiqyscs"]).Contains("预计失血量")) checkBox29.Checked = true;
            //    if (Convert.ToString(dr["SHpiqyscs"]).Contains("强调关注点")) checkBox30.Checked = true;
            //    if (Convert.ToString(dr["SHpiqmzys"]).Contains("强调关注点")) checkBox32.Checked = true;
            //    if (Convert.ToString(dr["SHpiqmzys"]).Contains("应对方案")) checkBox31.Checked = true;

              //    if (Convert.ToString(dr["SHpiqshhs"]).Contains("物品灭菌合格")) checkBox35.Checked = true;
            //    if (Convert.ToString(dr["SHpiqshhs"]).Contains("应对方案")) checkBox34.Checked = true;
            //    if (Convert.ToString(dr["SHpiqshhs"]).Contains("仪器设备完好")) checkBox33.Checked = true;

              //    if (Convert.ToString(dr["SHpiqqita"]).Contains("有")) checkBox37.Checked = true;
            //    if (Convert.ToString(dr["SHpiqqita"]).Contains("无")) checkBox36.Checked = true;

              //    comboBox5.Text = dr["mazuiyishi2"].ToString();
            //    comboBox4.Text = dr["xunhuihushi2"].ToString();
            //    if (Convert.ToString(dr["SHhshenfen"]).Contains("Y")) checkBox40.Checked = true;
            //    if (Convert.ToString(dr["SHhsumc"]).Contains("Y")) checkBox39.Checked = true;
            //    if (Convert.ToString(dr["SHhszyy"]).Contains("Y")) checkBox38.Checked = true;
            //    if (Convert.ToString(dr["SHhsx"]).Contains("Y")) checkBox41.Checked = true;
            //    if (Convert.ToString(dr["SHhqdyw"]).Contains("Y")) checkBox43.Checked = true;
            //    if (Convert.ToString(dr["SHhqdyw"]).Contains("N")) checkBox42.Checked = true;
            //    if (Convert.ToString(dr["SHhqdyw"]).Contains("X-ray和签名")) checkBox44.Checked = true;


              //    if (Convert.ToString(dr["SHhbbqr"]).Contains("有")) checkBox46.Checked = true;
            //    if (Convert.ToString(dr["SHhbbqr"]).Contains("无")) checkBox45.Checked = true;
            //    if (Convert.ToString(dr["SHhhuanzhexm"]).Contains("Y")) checkBox48.Checked = true;

              //    if (Convert.ToString(dr["SHhZyID"]).Contains("Y")) checkBox47.Checked = true;
            //    if (Convert.ToString(dr["SHhWZ"]).Contains("Y")) checkBox50.Checked = true;
            //    if (Convert.ToString(dr["SHhWZ"]).Contains("N")) checkBox49.Checked = true;

              //    if (Convert.ToString(dr["SHhdmtl"]).Contains("是")) checkBox52.Checked = true;
            //    if (Convert.ToString(dr["SHhdmtl"]).Contains("否")) checkBox51.Checked = true;

              //    if (Convert.ToString(dr["SHhYlg"]).Contains("是")) checkBox54.Checked = true;
            //    if (Convert.ToString(dr["SHhYlg"]).Contains("否")) checkBox53.Checked = true;
            //    if (Convert.ToString(dr["SHhNG"]).Contains("是")) checkBox56.Checked = true;
            //    if (Convert.ToString(dr["SHhNG"]).Contains("否")) checkBox55.Checked = true;

              //    textBox1.Text = dr["SHhqitaguanlu"].ToString();

              //    if (Convert.ToString(dr["SHhBRquxiang"]).Contains("PACU")) checkBox59.Checked = true;
            //    if (Convert.ToString(dr["SHhBRquxiang"]).Contains("回病房")) checkBox58.Checked = true;
            //    if (Convert.ToString(dr["SHhBRquxiang"]).Contains("ICU")) checkBox57.Checked = true;

              //    if (Convert.ToString(dr["SHhqita"]).Contains("有")) checkBox61.Checked = true;
            //    if (Convert.ToString(dr["SHhqita"]).Contains("无")) checkBox60.Checked = true;
            //    comboBox8.Text = dr["shoushuyisheng3"].ToString();
            //    comboBox7.Text = dr["mazuiyisheng3"].ToString();
            //    comboBox6.Text = dr["xunhuihushi3"].ToString();
            // }
            // }
            catch
            {

            }

        }
        #endregion
        //绑定医师
        private void yishi()
        {
            DataTable dtMZYS = dal.gettljhushi();
            //comboBox3.Items.Clear();
            //comboBox4.Items.Clear();
            comboBox6.Items.Clear();
            foreach (DataRow dr in dtMZYS.Rows)
            {
                //comboBox3.Items.Add(dr["user_name"].ToString());
                //comboBox4.Items.Add(dr["user_name"].ToString());
                comboBox6.Items.Add(dr["user_name"].ToString());
            }
        }
        private void mazuiyishi()
        {
            DataTable dtMZYS = dal.GetAllMZYS();
          //  comboBox2.Items.Clear();
            comboBox5.Items.Clear();
          //  comboBox7.Items.Clear();
            foreach (DataRow dr in dtMZYS.Rows)
            {
               // comboBox2.Items.Add(dr["user_name"].ToString());
                comboBox5.Items.Add(dr["user_name"].ToString());
              //  comboBox7.Items.Add(dr["user_name"].ToString());
            }
        }
        #region<<保存信息>>
        private void baocun()
        {
            //Dictionary<string, string> SQF = new Dictionary<string, string>();
            //int result = 0;
            //if (buttonbaocun.Enabled)
            //{
            //    SQF.Add("IsRead", "0");
            //}
            //else
            //{
            //    SQF.Add("IsRead", "1");
            //}
            //SQF.Add("ZhuYuanID", padID);
            //SQF.Add("rate", Convert.ToDateTime(dateTimeP.Value.ToString()).ToString("yyyy-MM-dd"));
            //SQF.Add("keshi", txtbKeShi.Text);
            //SQF.Add("chaunghao", txtbChuangHao.Text);
            //SQF.Add("name", txtbHZName.Text);
            //SQF.Add("shoushuname", txtbMZSSMC.Text);
            //string AddItem = "";
            //AddItem = "";
            //if (checkBo1.Checked) AddItem += "Y";
            //SQF.Add("SHqshenfen", AddItem);
            //string AddItem1 = "";
            //AddItem1 = "";
            //if (checkBox1.Checked) AddItem1 += "Y";
            //SQF.Add("SHqbuwei", AddItem1);
            //string AddItem2 = "";
            //AddItem2 = "";
            //if (checkBox3.Checked) AddItem2 += "Y";
            //SQF.Add("SHqmingcheng", AddItem2);
            //string AddItem3 = "";
            //AddItem3 = "";
            //if (checkBox2.Checked) AddItem3 += "Y";
            //SQF.Add("SHqtongyi", AddItem3);
            //string AddItem4 = "";
            //AddItem4 = "";
            //if (checkBox4.Checked) AddItem4 += "是";
            //if (checkBox5.Checked) AddItem4 += "否";
            //SQF.Add("SHqbiaoshi", AddItem4);
            //string AddItem5 = "";
            //AddItem5 = "";
            //if (checkBox6.Checked) AddItem5 += "Y";
            //SQF.Add("SHqjcwc", AddItem5);
            //string AddItem6 = "";
            //AddItem6 = "";
            //if (checkBox7.Checked) AddItem6 += "是";
            //if (checkBox8.Checked) AddItem6 += "否";
            //SQF.Add("SHqjcjl", AddItem6);
            //string AddItem7 = "";
            //AddItem7 = "";
            //if (checkBox10.Checked) AddItem7 += "是";
            //if (checkBox9.Checked) AddItem7 += "否";
            //SQF.Add("SHqwzxjc", AddItem7);
            //string AddItem8 = "";
            //AddItem8 = "";
            //if (checkBox12.Checked) AddItem8 += "是";
            //if (checkBox11.Checked) AddItem8 += "否";
            //SQF.Add("SHqpfzb", AddItem8);
            //string AddItem9 = "";
            //AddItem9 = "";
            //if (checkBox14.Checked) AddItem9 += "是";
            //if (checkBox13.Checked) AddItem9 += "否";
            //SQF.Add("SHqjmtdjl", AddItem9);
            //string AddItem10 = "";
            //AddItem10 = "";
            //if (checkBox16.Checked) AddItem10 += "有";
            //if (checkBox15.Checked) AddItem10 += "无";
            //SQF.Add("SHqhzgms", AddItem10);
            //string AddItem11 = "";
            //AddItem11 = "";
            //if (checkBox18.Checked) AddItem11 += "有";
            //if (checkBox19.Checked) AddItem11 += "无";
            //if (checkBox62.Checked) AddItem11 += "提供支持";
            //SQF.Add("SHqqdhuxizangai", AddItem11);
            //string AddItem12 = "";
            //AddItem12 = "";
            //if (checkBox20.Checked) AddItem12 += "是";
            //if (checkBox17.Checked) AddItem12 += "否";
            //SQF.Add("SHqkjywpsqk", AddItem12);
            //string AddItem13 = "";
            //AddItem13 = "";
            //if (checkBox22.Checked) AddItem13 += "计划字体";
            //if (checkBox21.Checked) AddItem13 += "异体输血";
            //if (checkBox24.Checked) AddItem13 += "是";
            //if (checkBox23.Checked) AddItem13 += "否";
            //SQF.Add("SHqsqbx", AddItem13);
            //string AddItem14 = "";
            //AddItem14 = "";
            //if (checkBox65.Checked) AddItem14 += "假体";
            //if (checkBox64.Checked) AddItem14 += "植入物";
            //if (checkBox63.Checked) AddItem14 += "影像学资料";
            //SQF.Add("SHqqita", AddItem14);
            //SQF.Add("shoushuyisheng1", comboBox1.Text);
            //SQF.Add("mazuiyisheng1", comboBox2.Text);
            //SQF.Add("xunhuihushi1", comboBox3.Text);
            //string AddItem15 = "";
            //AddItem15 = "";
            //if (checkBox27.Checked) AddItem15 += "Y";
            //SQF.Add("SHpiqshenfen", AddItem15);
            //string AddItem16 = "";
            //AddItem16 = "";
            //if (checkBox26.Checked) AddItem16 += "Y";
            //SQF.Add("SHpiqshmc", AddItem16);
            //string AddItem17 = "";
            //AddItem17 = "";
            //if (checkBox25.Checked) AddItem17 += "Y";
            //SQF.Add("SHpiqshbw", AddItem17);
            //string AddItem18 = "";
            //AddItem18 = "";
            //if (checkBox28.Checked) AddItem18 += "预计手术时间";
            //if (checkBox29.Checked) AddItem18 += "预计失血量";
            //if (checkBox30.Checked) AddItem18 += "强调关注点";
            //SQF.Add("SHpiqyscs", AddItem18);
            //string AddItem19 = "";
            //AddItem19 = "";
            //if (checkBox32.Checked) AddItem19 += "强调关注点";
            //if (checkBox31.Checked) AddItem19 += "应对方案";
            //SQF.Add("SHpiqmzys", AddItem19);
            //string AddItem20 = "";
            //AddItem20 = "";
            //if (checkBox35.Checked) AddItem20 += "物品灭菌合格";
            //if (checkBox34.Checked) AddItem20 += "应对方案";
            //if (checkBox33.Checked) AddItem20 += "仪器设备完好";
            //SQF.Add("SHpiqshhs", AddItem20);
            //string AddItem21 = "";
            //AddItem21 = "";
            //if (checkBox37.Checked) AddItem21 += "有";
            //if (checkBox36.Checked) AddItem21 += "无";
            //SQF.Add("SHpiqqita", AddItem21);
            //SQF.Add("mazuiyishi2", comboBox5.Text);
            //SQF.Add("xunhuihushi2", comboBox4.Text);
            //string AddItem22 = "";
            //AddItem22= "";
            //if (checkBox40.Checked) AddItem22+= "Y";
            //SQF.Add("SHhshenfen", AddItem22);
            //string AddItem23 = "";
            //AddItem23 = "";
            //if (checkBox39.Checked) AddItem23 += "Y";
            //SQF.Add("SHhsumc", AddItem23);
            //string AddItem24 = "";
            //AddItem24 = "";
            //if (checkBox38.Checked) AddItem24 += "Y";
            //SQF.Add("SHhszyy", AddItem24);
            //string AddItem25 = "";
            //AddItem25 = "";
            //if (checkBox41.Checked) AddItem25 += "Y";
            //SQF.Add("SHhsx", AddItem25);
            //string AddItem26 = "";
            //AddItem26 = "";
            //if (checkBox43.Checked) AddItem26 += "Y";
            //if (checkBox42.Checked) AddItem26 += "N";
            //if (checkBox44.Checked) AddItem26 += "X-ray和签名";
            //SQF.Add("SHhqdyw", AddItem26);
            //string AddItem27 = "";
            //AddItem27 = "";
            //if (checkBox46.Checked) AddItem27 += "有";
            //if (checkBox45.Checked) AddItem27 += "无";
            //SQF.Add("SHhbbqr", AddItem27);
            //string AddItem28 = "";
            //AddItem28 = "";
            //if (checkBox48.Checked) AddItem28 += "Y";
            //SQF.Add("SHhhuanzhexm", AddItem28);
            //string AddItem29 = "";
            //AddItem29 = "";
            //if (checkBox47.Checked) AddItem29 += "Y";
            //SQF.Add("SHhZyID", AddItem29);
            //string AddItem30 = "";
            //AddItem30 = "";
            //if (checkBox50.Checked) AddItem30 += "Y";
            //if (checkBox49.Checked) AddItem30 += "N";
            //SQF.Add("SHhWZ", AddItem30);
            //string AddItem31 = "";
            //AddItem31= "";
            //if (checkBox52.Checked) AddItem31 += "是";
            //if (checkBox51.Checked) AddItem31 += "否";
            //SQF.Add("SHhdmtl", AddItem31);
            //string AddItem32 = "";
            //AddItem32 = "";
            //if (checkBox54.Checked) AddItem32 += "是";
            //if (checkBox53.Checked) AddItem32 += "否";
            //SQF.Add("SHhYlg", AddItem32);
            //string AddItem33 = "";
            //AddItem33 = "";
            //if (checkBox56.Checked) AddItem33 += "是";
            //if (checkBox55.Checked) AddItem33 += "否";
            //SQF.Add("SHhNG", AddItem33);
            //SQF.Add("SHhqitaguanlu", textBox1.Text);
            //string AddItem34 = "";
            //AddItem34 = "";
            //if (checkBox59.Checked) AddItem34 += "PACU";
            //if (checkBox58.Checked) AddItem34 += "回病房";
            //if (checkBox57.Checked) AddItem34 += "ICU";
            //SQF.Add("SHhBRquxiang", AddItem34);
            //string AddItem35 = "";
            //AddItem35 = "";
            //if (checkBox61.Checked) AddItem35 += "有";
            //if (checkBox60.Checked) AddItem35 += "无";
            //SQF.Add("SHhqita", AddItem35);
            //SQF.Add("SHhheduiwanb","");
            //SQF.Add("shoushuyisheng3", comboBox8.Text);
            //SQF.Add("mazuiyisheng3", comboBox7.Text);
            //SQF.Add("xunhuihushi3", comboBox6.Text);


            //DataTable dt = dal.GetbaocunShaqhcbiao(padID);

            //if (dt.Rows.Count > 0)
            //{
            //    result = dal.UpdateShaqhcbiao(SQF);
            //    MessageBox.Show("修改成功");
            //}
            //else
            //{
            //    result = dal.InsertShaqhcbiao(SQF);
            //    MessageBox.Show("保存成功！");
            //}
            
        }
        #endregion
        #region <<打印信息>>
        private void buttonbaocun_Click(object sender, EventArgs e)
        {
            baocun();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
             Pen ptp = Pens.Black;
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Font ptzt = new Font("宋体", 11);//普通字体
            Font ptzt6 = new Font("宋体", 12);//普通字体
            Font ptzt7 = new Font("宋体", 14);//普通字体
            Font ptzt1 = new Font("宋体", 10, FontStyle.Bold);
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号    
            Font ptzt4 = new Font("宋体", 11, FontStyle.Bold);//加粗11号
            Font ptzt5 = new Font("宋体", 16, FontStyle.Bold);//加粗16号
            int y = 40; int x = 80-30; int y1 = 0;
            y = y + 10;
            string title = "新 疆 医 科 大 学 第 五 附 属 医 院 手 术 安 全 核 查 表";
            e.Graphics.DrawString(title, ptzt5, Brushes.Black, new Point(x + 200, y));
            y = y + 40;
            e.Graphics.DrawString("日期:" + dateTimeP.Value.ToString("yyyy-MM-dd "), ptzt6, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("科室:" + txtbKeShi.Text.Trim(), ptzt6, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawString("床号:" + txtbChuangHao.Text.Trim(), ptzt6, Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawString("姓名:" + txtbHZName.Text.Trim(), ptzt6, Brushes.Black, new Point(x + 415, y));
            e.Graphics.DrawString("住院号:" + txtbZYH.Text.Trim(), ptzt6, Brushes.Black, new Point(x + 700, y));
            e.Graphics.DrawLine(Pens.Black, x + 55, y + 20, x + 160, y + 20);
            e.Graphics.DrawLine(Pens.Black, x + 205, y + 20, x + 340, y + 20);
            e.Graphics.DrawLine(Pens.Black, x + 385, y + 20, x + 410, y + 20);
            e.Graphics.DrawLine(Pens.Black, x + 460, y + 20, x + 700, y + 20);
            e.Graphics.DrawLine(Pens.Black, x + 760, y + 20, x + 835, y + 20);
            y = y + 25;
            e.Graphics.DrawString("实施手术名称:" + txtbMZSSMC.Text.Trim(), ptzt6, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawLine(Pens.Black, x + 100, y + 20, x + 1050, y + 20);
          
           
            y = y + 35;
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 630, x + 1050, y + 630);//横
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 0, x + 1050, y + 0);//横
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 40, x + 330, y + 40);//横
            e.Graphics.DrawLine(Pens.Black, x + 360, y + 40, x + 690, y + 40);//横
            e.Graphics.DrawLine(Pens.Black, x + 720, y + 40, x + 1050, y + 40);//横
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 530, x + 330, y + 530);
            e.Graphics.DrawLine(Pens.Black, x + 360, y + 530, x + 690, y + 530);//横
            e.Graphics.DrawLine(Pens.Black, x + 720, y + 530, x + 1050, y + 530);//横
           // e.Graphics.DrawLine(Pens.Black, x + 720, y + 470, x + 1050, y + 470);
            //e.Graphics.DrawLine(Pens.Black, x + 830, y + 360, x + 1030, y + 360);
            //Q
            //e.Graphics.DrawLine(Pens.Black, x + 100, y + 565, x + 310, y + 565);
           e.Graphics.DrawLine(Pens.Black, x + 100, y + 595, x + 310, y + 595);
           //e.Graphics.DrawLine(Pens.Black, x + 100, y + 625, x + 310, y + 625);
            //e.Graphics.DrawLine(Pens.Black, x + 460, y + 565, x + 670, y + 565);
            e.Graphics.DrawLine(Pens.Black, x + 460, y + 595, x + 670, y + 595);
           // e.Graphics.DrawLine(Pens.Black, x + 820, y + 565, x + 1030, y + 565);
            e.Graphics.DrawLine(Pens.Black, x + 820, y + 595, x + 1030, y + 595);
            //e.Graphics.DrawLine(Pens.Black, x + 820, y + 625, x + 1030, y + 625);
            
            e.Graphics.DrawLine(Pens.Black, x + 330, y + 0, x + 330, y + 630);//竖
            e.Graphics.DrawLine(Pens.Black, x + 360, y + 0, x + 360, y + 630);//竖
            e.Graphics.DrawLine(Pens.Black, x + 690, y + 0, x + 690, y + 630);//竖
            e.Graphics.DrawLine(Pens.Black, x + 720, y + 0, x + 720, y + 630);//竖
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 0, x + 0, y + 630);//竖
            e.Graphics.DrawLine(Pens.Black, x + 1050, y + 0, x + 1050, y + 630);//竖
            e.Graphics.DrawString("1、患者麻醉手术前(开始)", ptzt7, Brushes.Black, new Point(x + 50, y + 10));
            e.Graphics.DrawString("2、皮肤切开之前(暂停)", ptzt7, Brushes.Black, new Point(x + 410, y + 10));
            e.Graphics.DrawString("3、患者离开手术室之前(结束)", ptzt7, Brushes.Black, new Point(x + 750, y + 10));
            y = y + 45;
            e.Graphics.DrawString("■   手术医师、麻醉医师及护士共同确认", ptzt, Brushes.Black, new Point(x , y ));
            e.Graphics.DrawString("■   手术医师、麻醉医师及护士共同确认", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("■   手术医师、麻醉医师及护士共同确认", ptzt, Brushes.Black, new Point(x + 720, y));
            y = y + 25;
            e.Graphics.DrawString("     ◮ 患者身份       □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("     ◮ 患者身份       □", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("     ◮ 记录实施手术的名称□", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBo1.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 174, y));
            //if (checkBox27.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 533, y  ));
            //if (checkBox27.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 917, y));
            y = y + 20;
            e.Graphics.DrawString("     ◮ 手术部位       □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("     ◮ 手术部位       □", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("     ◮ 清点手术用物", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox1.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 174, y));
            //if (checkBox26.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 533, y));
            //if (checkBox39.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 917, y));
            y = y + 25;
            e.Graphics.DrawString("     ◮ 手术方式       □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("     ◮ 手术方式       □", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("        数量正确    □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox3.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 174, y));
            //if (checkBox25.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 533, y));
            //if (checkBox38.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 917, y));
            y = y + 20;
            e.Graphics.DrawString("     ◮ 知情同意医疗文书齐备 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("     ◮ 手术体位       □", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("     数量不正确  □（X-ray和签名 □）", ptzt, Brushes.Black, new Point(x + 720, y));








            y = y + 20;
            e.Graphics.DrawString("■   手术部位标识 是 □  否 □  不适用 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("■   手术风险预警", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("■   手术标本确认   有 □   无 □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox2.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 174, y));
            //if (checkBox41.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 917, y));
            y = y + 20;
            e.Graphics.DrawString("■   麻醉安全检查完成    是 □   否 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("      手术医师陈述：预计手术时间   □", ptzt, Brushes.Black, new Point(x + 360, y));
            //e.Graphics.DrawString("□", ptzt, Brushes.Black, new Point(x + 619, y));
            e.Graphics.DrawString("       患者姓名  □    住院号  □", ptzt, Brushes.Black, new Point(x + 720, y));
           // if (checkBox28.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x +619, y));
            y = y + 20;
            e.Graphics.DrawString("■   生命体征监测建立    是 □   否 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("                    预计失血量     □", ptzt, Brushes.Black, new Point(x + 360, y));
            //e.Graphics.DrawString("□", ptzt, Brushes.Black, new Point(x + 619, y));
            e.Graphics.DrawString("■   皮肤完整性检查 是 □   否 □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox4.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 78, y));
            //if (checkBox5.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 174, y));
            //if (checkBox29.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 619, y));
            //if (checkBox43.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 876, y));
            y = y + 25;
            e.Graphics.DrawString("■   患者过敏史      有 □   无 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("                    强调关注点     □", ptzt, Brushes.Black, new Point(x + 360, y));
            //e.Graphics.DrawString("□", ptzt, Brushes.Black, new Point(x + 619, y));
            e.Graphics.DrawString("■   动静脉通路     有 □   无 □", ptzt, Brushes.Black, new Point(x + 720, y));
            //e.Graphics.DrawString(" （X-ray和签名 □）", ptzt, Brushes.Black, new Point(x + 890, y));
            //if (checkBox6.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 188, y));
            //if (checkBox30.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 619, y));
            //if (checkBox42.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 877, y));
            //if (checkBox44.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 1008  , y));
            y = y + 25;
            e.Graphics.DrawString("■   气道障碍或呼吸功能障碍", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("      麻醉医师陈述：强调关注点     □", ptzt, Brushes.Black, new Point(x + 360, y));
            //e.Graphics.DrawString("□", ptzt, Brushes.Black, new Point(x + 619, y));
            e.Graphics.DrawString("■   引流管         有 □   无 □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox7.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 211, y));
            //if (checkBox8.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 268, y));
            //if (checkBox32.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 619, y));
            //if (checkBox46.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 901, y));
            //if (checkBox45.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 956, y));
            y = y + 25;
            e.Graphics.DrawString("     有 □    设备/提供支持 □   无 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("                    应对方案       □", ptzt, Brushes.Black, new Point(x + 360, y));
            //e.Graphics.DrawString("□", ptzt, Brushes.Black, new Point(x + 619, y));
            e.Graphics.DrawString("■   尿管           有 □   无 □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox10.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 197, y));
            //if (checkBox9.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 252, y));
            //if (checkBox31.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 619, y));
            //if (checkBox48.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 838, y));
            //if (checkBox47.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 924, y));
            y = y + 25;
            e.Graphics.DrawString("■   术野皮肤准备     是 □  否 □", ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawString("     手术护士称述：物品灭菌合格", ptzt, Brushes.Black, new Point(x + 360, y));
           // e.Graphics.DrawString("□", ptzt, Brushes.Black, new Point(x + 619, y));
           // e.Graphics.DrawString("■   皮肤完整性检查", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox12.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 197, y));
            //if (checkBox11.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 252, y));
            //if (checkBox35.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 619, y));
            y = y + 25;
            e.Graphics.DrawString("■   静脉通道建立完成    是 □   否 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("      手术护士陈述：物品灭菌合格   □", ptzt, Brushes.Black, new Point(x + 360, y));
           // e.Graphics.DrawString("□", ptzt, Brushes.Black, new Point(x + 619, y));
            e.Graphics.DrawString("■   其他管路：                      ", ptzt, Brushes.Black, new Point(x + 720, y));
            e.Graphics.DrawString("               __________________________", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox34.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 619, y));
            //if (checkBox50.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 797, y));
            //if (checkBox49.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 894, y));
            y = y + 25;
            e.Graphics.DrawString("■   皮肤完整性检查      是 □   否 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("                    应对方案       □", ptzt, Brushes.Black, new Point(x + 360, y));
            //e.Graphics.DrawString("□", ptzt, Brushes.Black, new Point(x + 619, y));
            e.Graphics.DrawString("■   病人去向  ", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox14.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 78, y));
            //if (checkBox13.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 174, y));
            //if (checkBox33.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 619, y));
            //if (checkBox52.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 894, y));
            //if (checkBox51.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 948, y));
            y = y + 25;
            e.Graphics.DrawString("■   术前备血情况        是 □   否 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("                    仪器设备完好   □", ptzt, Brushes.Black, new Point(x +360, y));
            e.Graphics.DrawString("     ◮  PACU      □", ptzt, Brushes.Black, new Point(x + 720, y));
            y = y + 25;
            e.Graphics.DrawString("     计划自体  □/异体输血  □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("■   术前60分钟内给予预防性抗生素  ", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("     ◮  回病房    □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox56.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 894, y));
            //if (checkBox55.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 948, y));
            y = y + 25;
            //dsaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            e.Graphics.DrawString("■   假体 □/植入物 □/金属 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("                  是 □       否 □", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("     ◮  ICU       □" + textBox1.Text.Trim(), ptzt, Brushes.Black, new Point(x + 720, y));
            //e.Graphics.DrawString(".", ptzt, Brushes.Black, new Point(x + 1035, y));
            //if (checkBox18.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 79, y));
            //if (checkBox62.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 229, y));
            //if (checkBox37.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 532, y));
            //if (checkBox36.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 588, y));
            y = y + 25;
            e.Graphics.DrawString("■   影像和检查结果齐备  是 □   否 □", ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("■   其他：   有 □   无 □", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("■   其他：         有 □   无 □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox19.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 79, y));
          
            y = y + 25;
            e.Graphics.DrawString("■   术前是否VTE预防     是 □   否 □", ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawString("     ◮ PACU     □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox59.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 846, y));
          
            y = y + 20;
            e.Graphics.DrawString("■   抗菌药物皮试情况    是 □   否 □", ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawString("     ◮ 回病房   □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox20.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 78, y));
            //if (checkBox17.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 174, y));
            //if (checkBox58.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 846, y));
            e.Graphics.DrawString("在与核对项目相应的框内“□”打钩“√”", ptzt, Brushes.Black, new Point(x + 720, y));
            y = y + 20;
            //e.Graphics.DrawString("■   术前备血情况", ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawString("     ◮ ICU      □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox57.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 846, y));
            e.Graphics.DrawString("即可完成！", ptzt, Brushes.Black, new Point(x + 720, y));
            y = y + 20;
           
            //e.Graphics.DrawString("     计划字体 □ /异体输血 □", ptzt, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawString("■   其他：  有 □  无 □", ptzt, Brushes.Black, new Point(x + 720, y));
            //if (checkBox22.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 111, y));
            //if (checkBox21.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 213, y));
            //if (checkBox61.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 845, y));
            //if (checkBox60.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 902, y));
           
            //e.Graphics.DrawString("     ◮ 是 □       否 □", ptzt, Brushes.Black, new Point(x, y));
            //if (checkBox24.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 78, y));
            //if (checkBox23.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 174, y));
           
          //  e.Graphics.DrawString("■   假体 □/植入物 □/影像学资料 □ ", ptzt, Brushes.Black, new Point(x, y));
           
            //if (checkBox65.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 78, y));
            //if (checkBox64.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 156, y));
            //if (checkBox63.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 268, y));
          
           
            y = y + 40;
            e.Graphics.DrawString("手术医生签名：", ptzt, Brushes.Black, new Point(x + 0, y));
            e.Graphics.DrawString(".", ptzt, Brushes.Black, new Point(x + 313, y + 5));
            e.Graphics.DrawString("麻醉医生签名：", ptzt, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString(".", ptzt, Brushes.Black, new Point(x + 673, y + 5));
            e.Graphics.DrawString("巡回护士签名：", ptzt, Brushes.Black, new Point(x + 720, y));
            e.Graphics.DrawString(".", ptzt, Brushes.Black, new Point(x + 1033, y + 5));
            y = y + 30;
            e.Graphics.DrawString("核查时间:      年    月    日     时    分", ptzt, Brushes.Black, new Point(x + 0, y));
           // e.Graphics.DrawString(".", ptzt, Brushes.Black, new Point(x + 313, y + 5));
            e.Graphics.DrawString("核查时间:      年    月    日     时    分", ptzt, Brushes.Black, new Point(x + 360, y));
          //  e.Graphics.DrawString(".", ptzt, Brushes.Black, new Point(x + 673, y + 5));
            e.Graphics.DrawString("核查时间:      年    月    日     时    分", ptzt, Brushes.Black, new Point(x + 720, y));
            //e.Graphics.DrawString(".", ptzt, Brushes.Black, new Point(x + 1033, y + 5));
            //y = y + 30;
            //e.Graphics.DrawString("巡回护士签名：", ptzt, Brushes.Black, new Point(x + 0, y));
            //e.Graphics.DrawString(".", ptzt, Brushes.Black, new Point(x + 313, y + 5));
            //e.Graphics.DrawString("巡回护士签名：", ptzt, Brushes.Black, new Point(x + 720, y));
            //e.Graphics.DrawString(".", ptzt, Brushes.Black, new Point(x + 1033, y + 5));
                //】
                //e.Graphics.DrawString("病理编号：" + textBoxblbh.Text.Trim(), ptzt, Brushes.Black, new Point(x + 400, y));
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //           //printDocument1.DefaultPageSettings.PaperSize =
            //    new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            printDocument1.DefaultPageSettings.Landscape = true;
          
        }

       #endregion

        private void button2_Click(object sender, EventArgs e)
        {
             this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
    }
}
