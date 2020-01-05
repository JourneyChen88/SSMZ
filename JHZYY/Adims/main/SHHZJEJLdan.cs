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
    public partial class SHHZJEJLdan : Form
    {
        adims_BLL.mz bll = new adims_BLL.mz();
        adims_DAL.mz dal = new adims_DAL.mz();
        string padID;
        bool IsRead = false;  
        public SHHZJEJLdan(string padID1)
        {
            padID = padID1;
            InitializeComponent();
        }

        private void SHHZJEJLdan_Load(object sender, EventArgs e)
        {
            info();
        }
        #region<<显示信息>>
        private void info()
        {
            DataTable dt = dal.Getjiaojiejilu(padID);
            DataRow dr1 = dt.Rows[0];
            textBoxname.Text = dr1["Patname"].ToString();
            textBoxsex.Text = dr1["Patsex"].ToString();
            textBoxage.Text = dr1["Patage"].ToString();
            textBoxkeshi.Text = dr1["Patdpm"].ToString();
            textBoxchaunghao.Text = dr1["Patbedno"].ToString();
            textBoxZYid.Text = dr1["PatZhuYuanID"].ToString();

            dt = dal.Getsqljiludan(padID);
                if(dt.Rows.Count>0)
                {
                   
                    checkBox1.Checked = (dt.Rows[0]["Hdxu1"].ToString().Contains("术前病区")) ?true : false;
                    checkBox2.Checked = (dt.Rows[0]["Hdxu1"].ToString().Contains("手术室")) ? true : false;
                    checkBox67.Checked = (dt.Rows[0]["Hdxu2"].ToString().Contains("手术室")) ? true : false;
                    checkBox68.Checked = (dt.Rows[0]["Hdxu2"].ToString().Contains("PACU")) ? true : false;
                    checkBox70.Checked = (dt.Rows[0]["Hdxu2"].ToString().Contains("ICU")) ? true : false;
                    checkBox69.Checked = (dt.Rows[0]["Hdxu2"].ToString().Contains("病区")) ? true : false;

                    checkBox111.Checked = (dt.Rows[0]["Hdxu3"].ToString().Contains("手术室")) ? true : false;
                    checkBox112.Checked = (dt.Rows[0]["Hdxu3"].ToString().Contains("PACU")) ? true : false;
                    checkBox81.Checked =  (dt.Rows[0]["Hdxu3"].ToString().Contains("ICU")) ? true : false;
                    checkBox82.Checked =  (dt.Rows[0]["Hdxu3"].ToString().Contains("病区")) ? true : false;

                    dateTimePicker2.Text = dt.Rows[0]["rate1"].ToString();
                    dateTimePicker3.Text = dt.Rows[0]["rate2"].ToString();
                    dateTimePicker4.Text = dt.Rows[0]["rate3"].ToString();

                    checkBox3.Checked = (dt.Rows[0]["hzshengfen1"].ToString().Contains("确认")) ? true : false;
                    checkBox66.Checked = (dt.Rows[0]["hzshengfen2"].ToString().Contains("确认")) ? true : false;
                    checkBox110.Checked = (dt.Rows[0]["hzshengfen3"].ToString().Contains("确认")) ? true : false;

                    checkBox4.Checked = (dt.Rows[0]["Gms1"].ToString().Contains("无")) ? true : false;
                    checkBox4.Checked = (dt.Rows[0]["Gms1"].ToString().Contains("不详")) ? true : false;
                    checkBox6.Checked = (dt.Rows[0]["Gms1"].ToString().Contains("有")) ? true : false;

                    checkBox65.Checked = (dt.Rows[0]["Gms2"].ToString().Contains("无")) ? true : false;
                    checkBox64.Checked = (dt.Rows[0]["Gms2"].ToString().Contains("不详")) ? true : false;
                    checkBox63.Checked = (dt.Rows[0]["Gms2"].ToString().Contains("有")) ? true : false;

                    checkBox7.Checked = (dt.Rows[0]["Shyongyao1"].ToString().Contains("无")) ? true : false;
                    checkBox8.Checked = (dt.Rows[0]["Shyongyao1"].ToString().Contains("有")) ? true : false;

                    checkBox10.Checked = (dt.Rows[0]["Pfwzx1"].ToString().Contains("完整")) ? true : false;
                    checkBox9.Checked = (dt.Rows[0]["Pfwzx1"].ToString().Contains("不完整")) ? true : false;
                    checkBox60.Checked = (dt.Rows[0]["Pfwzx2"].ToString().Contains("完整")) ? true : false;
                    checkBox59.Checked = (dt.Rows[0]["Pfwzx2"].ToString().Contains("不完整")) ? true : false;
                    checkBox106.Checked = (dt.Rows[0]["Pfwzx3"].ToString().Contains("完整")) ? true : false;
                    checkBox105.Checked = (dt.Rows[0]["Pfwzx3"].ToString().Contains("不完整")) ? true : false;

                    checkBox11.Checked = (dt.Rows[0]["GD1"].ToString().Contains("气管插管")) ? true : false;
                    checkBox20.Checked = (dt.Rows[0]["GD1"].ToString().Contains("气管切开套管")) ? true : false;
                    checkBox21.Checked = (dt.Rows[0]["GD1"].ToString().Contains("静脉置管")) ? true : false;
                    checkBox22.Checked = (dt.Rows[0]["GD1"].ToString().Contains("导尿管")) ? true : false;
                    checkBox23.Checked = (dt.Rows[0]["GD1"].ToString().Contains("引流管")) ? true : false;
                    checkBox24.Checked = (dt.Rows[0]["GD1"].ToString().Contains("胃管")) ? true : false;
                    checkBox25.Checked = (dt.Rows[0]["GD1"].ToString().Contains("其他导管")) ? true : false;
                    checkBox26.Checked = (dt.Rows[0]["GD1"].ToString().Contains("管道标识")) ? true : false;
                    checkBox27.Checked = (dt.Rows[0]["GD1"].ToString().Contains("管道通畅")) ? true : false;

                    checkBox58.Checked = (dt.Rows[0]["GD1"].ToString().Contains("气管插管")) ? true : false;
                    checkBox57.Checked = (dt.Rows[0]["GD1"].ToString().Contains("气管切开套管")) ? true : false;
                    checkBox50.Checked = (dt.Rows[0]["GD1"].ToString().Contains("静脉置管")) ? true : false;
                    checkBox49.Checked = (dt.Rows[0]["GD1"].ToString().Contains("导尿管")) ? true : false;
                    checkBox48.Checked = (dt.Rows[0]["GD1"].ToString().Contains("引流管")) ? true : false;
                    checkBox47.Checked = (dt.Rows[0]["GD1"].ToString().Contains("胃管")) ? true : false;
                    checkBox46.Checked = (dt.Rows[0]["GD1"].ToString().Contains("其他导管")) ? true : false;
                    checkBox45.Checked = (dt.Rows[0]["GD1"].ToString().Contains("管道标识")) ? true : false;
                    checkBox44.Checked = (dt.Rows[0]["GD1"].ToString().Contains("管道通畅")) ? true : false;

                    checkBox104.Checked = (dt.Rows[0]["GD1"].ToString().Contains("气管插管")) ? true : false;
                    checkBox103.Checked = (dt.Rows[0]["GD1"].ToString().Contains("气管切开套管")) ? true : false;
                    checkBox102.Checked = (dt.Rows[0]["GD1"].ToString().Contains("静脉置管")) ? true : false;
                    checkBox101.Checked = (dt.Rows[0]["GD1"].ToString().Contains("导尿管")) ? true : false;
                    checkBox100.Checked = (dt.Rows[0]["GD1"].ToString().Contains("引流管")) ? true : false;
                    checkBox99.Checked = (dt.Rows[0]["GD1"].ToString().Contains("胃管")) ? true : false;
                    checkBox98.Checked = (dt.Rows[0]["GD1"].ToString().Contains("其他导管")) ? true : false;
                    checkBox97.Checked = (dt.Rows[0]["GD1"].ToString().Contains("管道标识")) ? true : false;
                    checkBox96.Checked = (dt.Rows[0]["GD1"].ToString().Contains("管道通畅")) ? true : false;

                    checkBox12.Checked = (dt.Rows[0]["Jss1"].ToString().Contains("确认")) ? true : false;
                    checkBox13.Checked = (dt.Rows[0]["Pkpg1"].ToString().Contains("确认")) ? true : false;

                    checkBox14.Checked = (dt.Rows[0]["BL1"].ToString().Contains("确认")) ? true : false;
                    checkBox41.Checked = (dt.Rows[0]["BL2"].ToString().Contains("确认")) ? true : false;
                    checkBox95.Checked = (dt.Rows[0]["BL3"].ToString().Contains("确认")) ? true : false;

                    checkBox16.Checked = (dt.Rows[0]["YLWS1"].ToString().Contains("麻醉知情同意书")) ? true : false;
                    checkBox15.Checked = (dt.Rows[0]["YLWS1"].ToString().Contains("麻醉前访视单")) ? true : false;

                    checkBox40.Checked = (dt.Rows[0]["YLWS2"].ToString().Contains("手术安全核查表")) ? true : false;
                    checkBox39.Checked = (dt.Rows[0]["YLWS2"].ToString().Contains("手术风险评估表")) ? true : false;
                    checkBox43.Checked = (dt.Rows[0]["YLWS2"].ToString().Contains("手术护理记录单")) ? true : false;
                    checkBox42.Checked = (dt.Rows[0]["YLWS2"].ToString().Contains("体内植入物条码粘贴单")) ? true : false;
                    checkBox72.Checked = (dt.Rows[0]["YLWS2"].ToString().Contains("输血护理记录单")) ? true : false;
                    checkBox71.Checked = (dt.Rows[0]["YLWS2"].ToString().Contains("麻醉记录单")) ? true : false;
                    checkBox62.Checked = (dt.Rows[0]["YLWS2"].ToString().Contains("麻醉知情同意书")) ? true : false;
                    checkBox61.Checked = (dt.Rows[0]["YLWS2"].ToString().Contains("麻醉前后访视及计划单")) ? true : false;
                    checkBox73.Checked = (dt.Rows[0]["YLWS2"].ToString().Contains("麻醉临时医嘱单")) ? true : false;

                    checkBox94.Checked = (dt.Rows[0]["YLWS3"].ToString().Contains("手术安全核查表")) ? true : false;
                    checkBox93.Checked = (dt.Rows[0]["YLWS3"].ToString().Contains("手术风险评估表")) ? true : false;
                    checkBox80.Checked = (dt.Rows[0]["YLWS3"].ToString().Contains("手术护理记录单")) ? true : false;
                    checkBox79.Checked = (dt.Rows[0]["YLWS3"].ToString().Contains("体内植入物条码粘贴单")) ? true : false;
                    checkBox78.Checked = (dt.Rows[0]["YLWS3"].ToString().Contains("输血护理记录单")) ? true : false;
                    checkBox77.Checked = (dt.Rows[0]["YLWS3"].ToString().Contains("麻醉记录单")) ? true : false;
                    checkBox76.Checked = (dt.Rows[0]["YLWS3"].ToString().Contains("麻醉知情同意书")) ? true : false;
                    checkBox75.Checked = (dt.Rows[0]["YLWS3"].ToString().Contains("麻醉前后访视及计划单")) ? true : false;
                    checkBox74.Checked = (dt.Rows[0]["YLWS3"].ToString().Contains("麻醉临时医嘱单")) ? true : false;

                    checkBox18.Checked = (dt.Rows[0]["SHbj1"].ToString().Contains("已标记")) ? true : false;
                    checkBox17.Checked = (dt.Rows[0]["SHbj1"].ToString().Contains("未标记")) ? true : false;

                    checkBox51.Checked = (dt.Rows[0]["Xdwp1"].ToString().Contains("影像资料")) ? true : false;
                    checkBox52.Checked = (dt.Rows[0]["Xdwp1"].ToString().Contains("腹带")) ? true : false;
                    checkBox53.Checked = (dt.Rows[0]["Xdwp1"].ToString().Contains("胃管")) ? true : false;
                    checkBox54.Checked = (dt.Rows[0]["Xdwp1"].ToString().Contains("胸腔引流瓶")) ? true : false;
                    checkBox55.Checked = (dt.Rows[0]["Xdwp1"].ToString().Contains("导尿包")) ? true : false;
                    checkBox56.Checked = (dt.Rows[0]["Xdwp1"].ToString().Contains("其他")) ? true : false;

                    checkBox36.Checked = (dt.Rows[0]["Xdwp2"].ToString().Contains("影像资料")) ? true : false;
                    checkBox35.Checked = (dt.Rows[0]["Xdwp2"].ToString().Contains("腹带")) ? true : false;
                    checkBox34.Checked = (dt.Rows[0]["Xdwp2"].ToString().Contains("胃管")) ? true : false;
                    checkBox33.Checked = (dt.Rows[0]["Xdwp2"].ToString().Contains("胸腔引流瓶")) ? true : false;
                    checkBox32.Checked = (dt.Rows[0]["Xdwp2"].ToString().Contains("导尿包")) ? true : false;
                    checkBox31.Checked = (dt.Rows[0]["Xdwp2"].ToString().Contains("其他")) ? true : false;

                    checkBox92.Checked = (dt.Rows[0]["Xdwp3"].ToString().Contains("影像资料")) ? true : false;
                    checkBox91.Checked = (dt.Rows[0]["Xdwp3"].ToString().Contains("腹带")) ? true : false;
                    checkBox90.Checked = (dt.Rows[0]["Xdwp3"].ToString().Contains("胃管")) ? true : false;
                    checkBox89.Checked = (dt.Rows[0]["Xdwp3"].ToString().Contains("胸腔引流瓶")) ? true : false;
                    checkBox88.Checked = (dt.Rows[0]["Xdwp3"].ToString().Contains("导尿包")) ? true : false;
                    checkBox87.Checked = (dt.Rows[0]["Xdwp3"].ToString().Contains("其他")) ? true : false;

                    textBox2.Text = dt.Rows[0]["BZ1"].ToString();
                    textBox5.Text = dt.Rows[0]["BZ2"].ToString();
                    textBox25.Text = dt.Rows[0]["BZ3"].ToString();
                    checkBox108.Checked = (dt.Rows[0]["JIAOKESHI1"].ToString().Contains("交")) ? true : false;
                    checkBox107.Checked = (dt.Rows[0]["JIAOKESHI1"].ToString().Contains("接")) ? true : false;
                    checkBox113.Checked = (dt.Rows[0]["JIAOKESHI1"].ToString().Contains("术前病区")) ? true : false;
                    checkBox109.Checked = (dt.Rows[0]["JIAOKESHI1"].ToString().Contains("手术室")) ? true : false;

                    checkBox29.Checked = (dt.Rows[0]["JIAOKESHI2"].ToString().Contains("交")) ? true : false;
                    checkBox30.Checked = (dt.Rows[0]["JIAOKESHI2"].ToString().Contains("接")) ? true : false;
                    checkBox29.Checked = (dt.Rows[0]["JIAOKESHI2"].ToString().Contains("手术室/PACU")) ? true : false;
                    checkBox19.Checked = (dt.Rows[0]["JIAOKESHI2"].ToString().Contains("手术室/ICU")) ? true : false;
                    checkBox37.Checked = (dt.Rows[0]["JIAOKESHI2"].ToString().Contains("手术室/病区")) ? true : false;

                    checkBox84.Checked = (dt.Rows[0]["JIAOKESHI3"].ToString().Contains("交")) ? true : false;
                    checkBox83.Checked = (dt.Rows[0]["JIAOKESHI3"].ToString().Contains("接")) ? true : false;
                    checkBox86.Checked = (dt.Rows[0]["JIAOKESHI3"].ToString().Contains("PACU")) ? true : false;
                    checkBox85.Checked = (dt.Rows[0]["JIAOKESHI3"].ToString().Contains("术后病区")) ? true : false;

                    checkBox108.Checked = (dt.Rows[0]["JIEKESHI1"].ToString().Contains("交")) ? true : false;
                    checkBox107.Checked = (dt.Rows[0]["JIEKESHI1"].ToString().Contains("接")) ? true : false;
                    checkBox113.Checked = (dt.Rows[0]["JIEKESHI1"].ToString().Contains("术前病区")) ? true : false;
                    checkBox109.Checked = (dt.Rows[0]["JIEKESHI1"].ToString().Contains("手术室")) ? true : false;

                    checkBox29.Checked = (dt.Rows[0]["JIEKESHI2"].ToString().Contains("交")) ? true : false;
                    checkBox30.Checked = (dt.Rows[0]["JIEKESHI2"].ToString().Contains("接")) ? true : false;
                    checkBox29.Checked = (dt.Rows[0]["JIEKESHI2"].ToString().Contains("手术室/PACU")) ? true : false;
                    checkBox19.Checked = (dt.Rows[0]["JIEKESHI2"].ToString().Contains("手术室/ICU")) ? true : false;
                    checkBox37.Checked = (dt.Rows[0]["JIEKESHI2"].ToString().Contains("手术室/病区")) ? true : false;

                    checkBox84.Checked = (dt.Rows[0]["JIAOKESHI3"].ToString().Contains("交")) ? true : false;
                    checkBox83.Checked = (dt.Rows[0]["JIAOKESHI3"].ToString().Contains("接")) ? true : false;
                    checkBox86.Checked = (dt.Rows[0]["JIAOKESHI3"].ToString().Contains("PACU")) ? true : false;
                    checkBox85.Checked = (dt.Rows[0]["JIAOKESHI3"].ToString().Contains("术后病区")) ? true : false;

                    textBox32.Text = dt.Rows[0]["JIAOQIANMING1"].ToString();
                    textBox3.Text = dt.Rows[0]["JIAOQIANMING2"].ToString();
                    textBox24.Text = dt.Rows[0]["JIAOQIANMING3"].ToString();

                    textBox31.Text = dt.Rows[0]["JIEQIANMING1"].ToString();
                    textBox4.Text = dt.Rows[0]["JIEQIANMING1"].ToString();
                    textBox23.Text = dt.Rows[0]["JIEQIANMING1"].ToString();
                    dateTimePicker1.Text = dt.Rows[0]["rate"].ToString();






                }

        }
        #endregion
        #region<<保存信息>>
        private void buttonbaocun_Click(object sender, EventArgs e)
        {
            baocun();
        }
        private void baocun()
        {
          Dictionary<string, string> SQF = new Dictionary<string, string>();
          int result = 0;
          if (buttonbaocun.Enabled)
          {
              SQF.Add("IsRead", "0");
          }
          else
          {
              SQF.Add("IsRead", "1");
          }
          SQF.Add("ZhuYuanID", padID);
          SQF.Add("name", textBoxname.Text);
          SQF.Add("keshi", textBoxkeshi.Text);
          SQF.Add("bedID", textBoxchaunghao.Text);
          SQF.Add("sex", textBoxsex.Text);
          SQF.Add("age", textBoxage.Text);
          string AddItem = "";
          AddItem = "";
          if (checkBox1.Checked) AddItem += "术前病区";
          if (checkBox2.Checked) AddItem += "手术室";
          SQF.Add("Hdxu1", AddItem);

          string AddItem1= "";
          AddItem1 = "";
          if (checkBox67.Checked) AddItem1 += "手术室";
          if (checkBox68.Checked) AddItem1 += "PACU";
          if (checkBox70.Checked) AddItem1 += "ICU";
          if (checkBox69.Checked) AddItem1 += "病区";
          SQF.Add("Hdxu2", AddItem1);

          string AddItem2 = "";
          AddItem2 = ""; 
          if (checkBox68.Checked) AddItem2 += "PACU";
          if (checkBox69.Checked) AddItem2 += "术后病区";
          SQF.Add("Hdxu3", AddItem2);

          SQF.Add("rate1", Convert.ToDateTime(dateTimePicker2.Value.ToString()).ToString("HH-mm"));
          SQF.Add("rate2", Convert.ToDateTime(dateTimePicker3.Value.ToString()).ToString("HH-mm"));
          SQF.Add("rate3", Convert.ToDateTime(dateTimePicker4.Value.ToString()).ToString("HH-mm"));

          string AddItem3 = "";
          AddItem3 = "";
          if (checkBox3.Checked) AddItem3 += "确认";
          SQF.Add("hzshengfen1", AddItem3);
          string AddItem4 = "";
          AddItem4 = "";
          if (checkBox66.Checked) AddItem4 += "确认";
          SQF.Add("hzshengfen2", AddItem4);
          string AddItem5 = "";
          AddItem4 = "";
          if (checkBox110.Checked) AddItem5 += "确认";
          SQF.Add("hzshengfen3", AddItem5);

          string AddItem6 = "";
          AddItem6 = "";
          if (checkBox4.Checked) AddItem6 += "无";
          if (checkBox5.Checked) AddItem6 += "不详";
          if (checkBox6.Checked) AddItem6 += "有";
          SQF.Add("Gms1" + textBox1.Text, AddItem6);

          string AddItem7 = "";
          AddItem7 = "";
          if (checkBox65.Checked) AddItem7 += "无";
          if (checkBox64.Checked) AddItem7 += "不详";
          if (checkBox63.Checked) AddItem7 += "有";
          SQF.Add("Gms2" + textBox10.Text, AddItem7);

          string AddItem8 = "";
          AddItem8 = "";
          if (checkBox8.Checked) AddItem8 += "无";
          if (checkBox7.Checked) AddItem8 += "有";
          SQF.Add("Shyongyao1" , AddItem8);

          string AddItem9 = "";
          AddItem9 = "";
          if (checkBox10.Checked) AddItem9 += "完整";
          if (checkBox9.Checked) AddItem9 += "不完整";
          SQF.Add("Pfwzx1", AddItem9);

          string AddItem10 = "";
          AddItem10 = "";
          if (checkBox60.Checked) AddItem10 += "完整";
          if (checkBox59.Checked) AddItem10 += "不完整";
          SQF.Add("Pfwzx2", AddItem10);

          string AddItem11 = "";
          AddItem11 = "";
          if (checkBox106.Checked) AddItem11 += "完整";
          if (checkBox105.Checked) AddItem11 += "不完整";
          SQF.Add("Pfwzx3", AddItem11);

          string AddItem12 = "";
          AddItem12 = "";
          if (checkBox11.Checked) AddItem12 += "气管插管";
          if (checkBox20.Checked) AddItem12 += "气管切开套管";
          if (checkBox21.Checked) AddItem12 += "静脉置管" + textBox11.Text;
          if (checkBox22.Checked) AddItem12 += "导尿管";
          if (checkBox23.Checked) AddItem12 += "引流管" + textBox12.Text;
          if (checkBox24.Checked) AddItem12 += "胃管";
          if (checkBox25.Checked) AddItem12 += "其他导管" + textBox13.Text;
          if (checkBox26.Checked) AddItem12 += "管道标识";
          if (checkBox27.Checked) AddItem12 += "管道通畅";  
          SQF.Add("GD1", AddItem12);

          string AddItem13 = "";
          AddItem13 = "";
          if (checkBox58.Checked) AddItem13 += "气管插管";
          if (checkBox57.Checked) AddItem13 += "气管切开套管";
          if (checkBox50.Checked) AddItem13 += "静脉置管" + textBox9.Text;
          if (checkBox49.Checked) AddItem13 += "导尿管";
          if (checkBox48.Checked) AddItem13 += "引流管" + textBox8.Text;
          if (checkBox47.Checked) AddItem13 += "胃管";
          if (checkBox46.Checked) AddItem13 += "其他导管" + textBox7.Text;
          if (checkBox45.Checked) AddItem13 += "管道标识";
          if (checkBox44.Checked) AddItem13 += "管道通畅";
          SQF.Add("GD2", AddItem13);

          string AddItem14 = "";
          AddItem14 = "";
          if (checkBox104.Checked) AddItem14 += "气管插管";
          if (checkBox103.Checked) AddItem14 += "气管切开套管";
          if (checkBox102.Checked) AddItem14 += "静脉置管" + textBox29.Text;
          if (checkBox101.Checked) AddItem14 += "导尿管";
          if (checkBox100.Checked) AddItem14 += "引流管" + textBox28.Text;
          if (checkBox99.Checked) AddItem14 += "胃管";
          if (checkBox98.Checked) AddItem14 += "其他导管" + textBox27.Text;
          if (checkBox97.Checked) AddItem14 += "管道标识";
          if (checkBox96.Checked) AddItem14 += "管道通畅";
          SQF.Add("GD3", AddItem14);

          string AddItem15 = "";
          AddItem15 = "";
          if (checkBox12.Checked) AddItem15 += "确认";
          SQF.Add("Jss1", AddItem15);

          string AddItem16 = "";
          AddItem16 = "";
          if (checkBox13.Checked) AddItem16 += "确认";
          SQF.Add("Pkpg1", AddItem16);

          string AddItem17 = "";
          AddItem17 = "";
          if (checkBox14.Checked) AddItem17 += "确认";
          SQF.Add("BL1", AddItem17);

          string AddItem18 = "";
          AddItem18 = "";
          if (checkBox41.Checked) AddItem18 += "确认";
          SQF.Add("BL2", AddItem18);

          string AddItem19 = "";
          AddItem19 = "";
          if (checkBox95.Checked) AddItem19 += "确认";
          SQF.Add("BL3", AddItem19);

          string AddItem20 = "";
          AddItem20 = "";
          if (checkBox16.Checked) AddItem20 += "麻醉知情同意书";
          if (checkBox15.Checked) AddItem20 += "麻醉前访视单";
          SQF.Add("YLWS1", AddItem20);

          string AddItem21 = "";
          AddItem21 = "";
          if (checkBox40.Checked) AddItem21 += "手术安全核查表";
          if (checkBox39.Checked) AddItem21 += "手术风险评估表";
          if (checkBox43.Checked) AddItem21 += "手术护理记录单";
          if (checkBox42.Checked) AddItem21 += "体内植入物条码粘贴单";
          if (checkBox72.Checked) AddItem21 += "输血护理记录单";
          if (checkBox71.Checked) AddItem21 += "麻醉记录单";
          if (checkBox62.Checked) AddItem21 += "麻醉知情同意书";
          if (checkBox61.Checked) AddItem21 += "麻醉前后访视及计划单";
          if (checkBox73.Checked) AddItem21 += "麻醉临时医嘱单";
          SQF.Add("YLWS2", AddItem21);

          string AddItem22 = "";
          AddItem22 = "";
          if (checkBox94.Checked) AddItem22 += "手术安全核查表";
          if (checkBox93.Checked) AddItem22 += "手术风险评估表";
          if (checkBox80.Checked) AddItem22 += "手术护理记录单";
          if (checkBox79.Checked) AddItem22 += "体内植入物条码粘贴单";
          if (checkBox78.Checked) AddItem22 += "输血护理记录单";
          if (checkBox77.Checked) AddItem22 += "麻醉记录单";
          if (checkBox76.Checked) AddItem22 += "麻醉知情同意书";
          if (checkBox75.Checked) AddItem22 += "麻醉前后访视及计划单";
          if (checkBox74.Checked) AddItem22 += "麻醉临时医嘱单";
          SQF.Add("YLWS3", AddItem22);

          string AddItem23 = "";
          AddItem23 = "";
          if (checkBox18.Checked) AddItem23 += "已标记";
          if (checkBox17.Checked) AddItem23 += "未标记";
          SQF.Add("SHbj1", AddItem23);

          string AddItem24 = "";
          AddItem24 = "";
          if (checkBox51.Checked) AddItem24 += "影像资料" + textBox20.Text;
          if (checkBox52.Checked) AddItem24 += "腹带";
          if (checkBox53.Checked) AddItem24 += "胃管";
          if (checkBox54.Checked) AddItem24 += "胸腔引流瓶";
          if (checkBox55.Checked) AddItem24 += "导尿包";
          if (checkBox56.Checked) AddItem24 += "其他";
          SQF.Add("Xdwp1", AddItem24);

          string AddItem25 = "";
          AddItem25 = "";
          if (checkBox36.Checked) AddItem25 += "影像资料" + textBox6.Text;
          if (checkBox35.Checked) AddItem25 += "腹带";
          if (checkBox34.Checked) AddItem25 += "胃管";
          if (checkBox33.Checked) AddItem25 += "胸腔引流瓶";
          if (checkBox32.Checked) AddItem25 += "导尿包";
          if (checkBox31.Checked) AddItem25 += "其他";
          SQF.Add("Xdwp2", AddItem25);

          string AddItem26 = "";
          AddItem26 = "";
          if (checkBox92.Checked) AddItem26 += "影像资料" + textBox26.Text;
          if (checkBox91.Checked) AddItem26 += "腹带";
          if (checkBox90.Checked) AddItem26 += "胃管";
          if (checkBox89.Checked) AddItem26 += "胸腔引流瓶";
          if (checkBox88.Checked) AddItem26 += "导尿包";
          if (checkBox87.Checked) AddItem26 += "其他";
          SQF.Add("Xdwp3", AddItem26);

          SQF.Add("BZ1", textBox2.Text);
          SQF.Add("BZ2", textBox5.Text);
          SQF.Add("BZ3", textBox25.Text);

          string AddItem27 = "";
          AddItem27 = "";
          if (checkBox108.Checked) AddItem27 += "交";
          if (checkBox107.Checked) AddItem27 += "接";
          if (checkBox113.Checked) AddItem27 += "术前病区";
          if (checkBox109.Checked) AddItem27 += "手术室";
          SQF.Add("JIAOKESHI1", AddItem27);

          string AddItem28 = "";
          AddItem28 = "";
          if (checkBox29.Checked) AddItem28 += "交";
          if (checkBox30.Checked) AddItem28 += "接";
          if (checkBox28.Checked) AddItem28 += "手术室/PACU";
          if (checkBox19.Checked) AddItem28 += "手术室/ICU";
          if (checkBox37.Checked) AddItem28 += "手术室/病区";
          SQF.Add("JIAOKESHI2", AddItem28);

          string AddItem29 = "";
          AddItem29 = "";
          if (checkBox84.Checked) AddItem29 += "交";
          if (checkBox83.Checked) AddItem29 += "接";
          if (checkBox86.Checked) AddItem29 += "PACU";
          if (checkBox85.Checked) AddItem29 += "术后病区";
          SQF.Add("JIAOKESHI3", AddItem29);
          SQF.Add("JIEKESHI1", AddItem27);
          SQF.Add("JIEKESHI2", AddItem28);
          SQF.Add("JIEKESHI3", AddItem29);

          SQF.Add("JIAOQIANMING1", textBox32.Text);
          SQF.Add("JIAOQIANMING2", textBox3.Text);
          SQF.Add("JIAOQIANMING3", textBox24.Text);
          SQF.Add("JIEQIANMING1", textBox31.Text);
          SQF.Add("JIEQIANMING2", textBox4.Text);
          SQF.Add("JIEQIANMING3", textBox23.Text);

          SQF.Add("rate", Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

          DataTable dt = dal.Getsqljiludan(padID);

          if (dt.Rows.Count > 0)
          {
              result = dal.Updatejiludan(SQF);
              MessageBox.Show("修改成功");
          }
          else
          {
              result = dal.Insertjiludan(SQF);
              MessageBox.Show("保存成功！");
          }

        





         




        }
        #endregion
        #region<<打印信息>>
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
              
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize =
            //    new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("宋体", 11);//普通字体
            Font ptzt1= new Font("新宋体", 10);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 22, FontStyle.Bold);//加粗14号
            Font ptzt4 = new Font("宋体", 14, FontStyle.Bold);//加粗11号
            Font ptzt5 = new Font("宋体", 16, FontStyle.Bold);//加粗16号
            Font ptzt11 = new Font("宋体", 11);//普通字体 
            Font ptzt6 = new Font("宋体", 12);//普通字体
            int y = 30; int x = 65; int y1 = 0;
     
            string title = "新疆医科大学附属第五医院";
            e.Graphics.DrawString(title, ptzt4, Brushes.Black, new Point(x + 200, y));
            y = y + 30;
            string title1 = "手术患者交接记录单";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 180, y));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawString("姓名：" + textBoxname.Text.Trim(), ptzt6, Brushes.Black, new Point(x , y));
            e.Graphics.DrawString("性别:" + textBoxsex.Text.Trim(), ptzt6, Brushes.Black, new Point(x + 200, y));
            e.Graphics.DrawString("年龄:" + textBoxage.Text.Trim(), ptzt6, Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawString("科室:" + textBoxkeshi.Text.Trim(), ptzt6, Brushes.Black, new Point(x + 455, y));
            y = y + 25;
            e.Graphics.DrawString("床号:" + textBoxchaunghao.Text.Trim(), ptzt6, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("住院号:" + textBoxZYid.Text.Trim(), ptzt6, Brushes.Black, new Point(x + 184, y));
            e.Graphics.DrawString("日期:" + dateTimePicker1.Value.ToString("yyyy-MM-dd "), ptzt6, Brushes.Black, new Point(x + 340, y));
            y = y + 20;
            //竖线
            e.Graphics.DrawLine(Pens.Black, x, y + 0, x, y + 925);
            e.Graphics.DrawLine(Pens.Black, x + 660, y + 0, x + 660, y + 925);
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 0, x + 125, y + 925);
            e.Graphics.DrawLine(Pens.Black, x + 303, y + 0, x + 303, y + 925);
            e.Graphics.DrawLine(Pens.Black, x + 481, y + 0, x + 481, y + 925);
            //横线
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 900, x + 660, y + 900);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 0, x + 660, y + 0);
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 25, x + 660, y + 25);//短1
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 50, x + 660, y + 50);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 75, x + 660, y + 75);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 100, x + 481, y + 100);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 125, x + 660, y + 125);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 150, x + 660, y + 150);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 175, x + 660, y + 175);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 660, x + 660, y + 660);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 685, x + 660, y + 685);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 710, x + 660, y + 710);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 735, x + 660, y + 735);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 760, x + 660, y + 760);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 785, x + 660, y + 785);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 810, x + 660, y + 810);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 835, x + 660, y + 835);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 925, x + 660, y + 925);//



            e.Graphics.DrawLine(Pens.Black, x + 125, y + 200, x + 660, y + 200);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 225, x + 660, y + 225);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 250, x + 660, y + 250);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 275, x + 660, y + 275);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 300, x + 660, y + 300);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 325, x + 660, y + 325);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 350, x + 660, y + 350);//
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 375, x + 660, y + 375);//

            e.Graphics.DrawLine(Pens.Black, x + 0, y + 400, x + 660, y + 400);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 425, x + 660, y + 425);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 450, x + 660, y + 450);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 475, x + 660, y + 475);//
            e.Graphics.DrawLine(Pens.Black, x + 161, y + 123, x + 249, y + 123);//
            e.Graphics.DrawLine(Pens.Black, x + 336, y + 123, x + 424, y + 123);//


            e.Graphics.DrawString("核对项目", ptzt11, Brushes.Black, new Point(x + 25, y + 15));
            e.Graphics.DrawString("术前病区/手术室", ptzt11, Brushes.Black, new Point(x + 155, y + 5));
            e.Graphics.DrawString("手术室/PACU、ICU、病区", ptzt11, Brushes.Black, new Point(x + 305, y + 5));
            e.Graphics.DrawString("PACU/术后病区", ptzt11, Brushes.Black, new Point(x + 505, y + 5));
            y = y + 30;
            e.Graphics.DrawString("时间：" + dateTimePicker2.Value.ToString("HH:mm:ss"), ptzt11, Brushes.Black, new Point(x + 130, y ));
            e.Graphics.DrawString("时间:" + dateTimePicker3.Value.ToString("HH:mm:ss"), ptzt11, Brushes.Black, new Point(x + 305, y ));
            e.Graphics.DrawString("时间:" + dateTimePicker4.Value.ToString("HH:mm:ss"), ptzt11, Brushes.Black, new Point(x + 485, y ));
            y = y + 25;
            e.Graphics.DrawString("患者身份", ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("□确认", ptzt, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□确认", ptzt, Brushes.Black, new Point(x + 305,y));
            e.Graphics.DrawString("□确认", ptzt, Brushes.Black, new Point(x + 485, y));
            if (checkBox3.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox66.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox110.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString(" 过敏史", ptzt11, Brushes.Black, new Point(x + 25, y + 10));
            e.Graphics.DrawString("□无      □不详", ptzt11, Brushes.Black, new Point(x + 130, y ));
            e.Graphics.DrawString("□无      □不详", ptzt11, Brushes.Black, new Point(x + 305, y ));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 555, y + 15));
            if (checkBox4.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox5.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 210, y));
            if (checkBox65.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox64.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 385, y));
            y = y + 25;
            e.Graphics.DrawString("□有"+textBox1.Text, ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("过敏史", ptzt11, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawString("□有" + textBox10.Text, ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("过敏史", ptzt11, Brushes.Black, new Point(x + 425, y));
            if (checkBox6.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox63.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            y = y + 25;
            e.Graphics.DrawString("手术用药", ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("□无      □有", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 375, y ));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 555, y ));
            if (checkBox8.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox7.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 210, y));
            y = y + 25;
            e.Graphics.DrawString("皮肤完整性", ptzt11, Brushes.Black, new Point(x + 22, y ));
            e.Graphics.DrawString("□完整    □不完整", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□完整    □不完整", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□完整    □不完整", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox10.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox9.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 210, y));
            if (checkBox60.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox59.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 385, y));
            if (checkBox106.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            if (checkBox105.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 565, y));
            y = y + 25;
            e.Graphics.DrawString("□气管插管", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□气管插管", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□气管插管", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox11.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox58.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox104.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□气管切开套管", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□气管切开套管", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□气管切开套管", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox20.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox57.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox103.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□静脉置管 "+ textBox11.Text, ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□静脉置管 "+  textBox9.Text, ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□静脉置管 "+ textBox29.Text, ptzt11, Brushes.Black, new Point(x + 485, y));
            e.Graphics.DrawString("根", ptzt11, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawString("根", ptzt11, Brushes.Black, new Point(x + 425, y));
            e.Graphics.DrawString("根", ptzt11, Brushes.Black, new Point(x + 605, y));
            if (checkBox21.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox50.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox102.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□导尿管", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□导尿管", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□导尿管", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox22.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox49.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox101.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("  管道", ptzt11, Brushes.Black, new Point(x + 35, y));
            e.Graphics.DrawString("□引流管 " + textBox12.Text, ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□引流管 " + textBox8.Text, ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□引流管 " + textBox28.Text, ptzt11, Brushes.Black, new Point(x + 485, y));
            e.Graphics.DrawString("根", ptzt11, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawString("根", ptzt11, Brushes.Black, new Point(x + 425, y));
            e.Graphics.DrawString("根", ptzt11, Brushes.Black, new Point(x + 605, y));
            if (checkBox23.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox48.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox100.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□胃管", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□胃管", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□胃管", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox24.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox47.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox99.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□其他导管 " + textBox13.Text, ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□其他导管 " + textBox7.Text, ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□其他导管 " + textBox27.Text, ptzt11, Brushes.Black, new Point(x + 485, y));
            e.Graphics.DrawString("根", ptzt11, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawString("根", ptzt11, Brushes.Black, new Point(x + 425, y));
            e.Graphics.DrawString("根", ptzt11, Brushes.Black, new Point(x + 605, y));
            if (checkBox25.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox46.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox98.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□管道标识", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□管道标识", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□管道标识", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox26.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox45.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox97.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□管道通畅", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□管道通畅", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□管道通畅", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox27.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox44.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox96.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString(" 禁食水", ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("□确认", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 375, y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 555, y));
            if (checkBox12.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            y = y + 25;
            e.Graphics.DrawString("排空膀胱", ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("□确认", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 375, y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 555, y));
            if (checkBox13.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            y = y + 25;
            e.Graphics.DrawString("  病历", ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("□确认", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□确认", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□确认", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox14.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox41.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox95.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□手术安全核查表", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□手术安全核查表", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox40.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox94.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 20;
            e.Graphics.DrawString("□手术风险评估表", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□手术风险评估表", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox39.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox93.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 20;
            e.Graphics.DrawString("□手术护理记录单", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□手术护理记录单", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox43.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox80.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 20;
            e.Graphics.DrawString("手术室、麻醉科", ptzt11, Brushes.Black, new Point(x + 5, y + 5));
            e.Graphics.DrawString("□麻醉知情同意书", ptzt11, Brushes.Black, new Point(x + 130, y + 5));
            e.Graphics.DrawString("□体内植入物条码粘贴单", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□体内植入物条码粘贴单", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox16.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y + 5));
            if (checkBox42.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox79.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 20;
            e.Graphics.DrawString("相关医疗文书", ptzt11, Brushes.Black, new Point(x + 5, y + 5));
            e.Graphics.DrawString("□麻醉前访视单", ptzt11, Brushes.Black, new Point(x + 130, y + 5));
            e.Graphics.DrawString("□输血护理记录单", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□输血护理记录单", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox15.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y + 5));
            if (checkBox72.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox78.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 20;
            e.Graphics.DrawString("□麻醉记录单", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□麻醉记录单", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox71.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox77.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 20;
            e.Graphics.DrawString("□麻醉知情同意书", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□麻醉知情同意书", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox62.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox76.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 20;
            e.Graphics.DrawString("□麻醉前后访视及计划单", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□麻醉前后访视及计划单", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox61.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox75.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 20;
            e.Graphics.DrawString("□麻醉临时医嘱单", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□麻醉临时医嘱单", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox73.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox74.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("手术部位标记", ptzt11, Brushes.Black, new Point(x + 5, y));
            e.Graphics.DrawString("□已标记  □未标记", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 375, y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 555, y));
            if (checkBox18.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox17.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 210, y));
            y = y + 25;
            e.Graphics.DrawString("□影像资料 " + textBox20.Text, ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□影像资料 " + textBox6.Text, ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□影像资料 " + textBox26.Text, ptzt11, Brushes.Black, new Point(x + 485, y));
            e.Graphics.DrawString("张", ptzt11, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawString("张", ptzt11, Brushes.Black, new Point(x + 425, y));
            e.Graphics.DrawString("张", ptzt11, Brushes.Black, new Point(x + 605, y));
            if (checkBox51.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox36.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox92.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□腹带    □胃管", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□腹带    □胃管", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□腹带    □胃管", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox52.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox53.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 210, y));
            if (checkBox35.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox34.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 385, y));
            if (checkBox91.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            if (checkBox90.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 565, y));
            y = y + 25;
            e.Graphics.DrawString("携带物品", ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("□胸腔引流瓶", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□胸腔引流瓶", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□胸腔引流瓶", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox54.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox33.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox89.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□导尿包", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□导尿包", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□导尿包", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox55.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox32.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox88.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□其他", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□其他", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("□其他", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox56.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 130, y));
            if (checkBox31.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            if (checkBox87.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString(" 备注", ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString(textBox2.Text, ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString(textBox5.Text, ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString(textBox25.Text, ptzt11, Brushes.Black, new Point(x + 485, y));
            y = y + 25;
            e.Graphics.DrawString("□手术室/PACU", ptzt11, Brushes.Black, new Point(x + 305, y));
            if (checkBox28.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            y = y + 20;
            e.Graphics.DrawString("交/接科室", ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("术前病区/手术室", ptzt11, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("□手术室/ICU", ptzt11, Brushes.Black, new Point(x + 305, y));
            e.Graphics.DrawString("PACU/术后病区", ptzt11, Brushes.Black, new Point(x + 485, y));
            if (checkBox19.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
          
            y = y + 20;
            e.Graphics.DrawString("□手术室/病区", ptzt11, Brushes.Black, new Point(x + 305, y));
            if (checkBox37.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 305, y));
            y = y + 25;
            e.Graphics.DrawString(" 交/接人员签名", ptzt11, Brushes.Black, new Point(x , y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 200, y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 375, y));
            e.Graphics.DrawString("/", ptzt11, Brushes.Black, new Point(x + 555, y));
            y = y + 25;
            e.Graphics.DrawString("备注：术前病区/手术室交接有病区护士填写，手术室/PACU、ICU、病区交接由手术室护士填写，PACU/", ptzt1, Brushes.Black, new Point(x, y));
            y = y + 20;
            e.Graphics.DrawString("术后病区交接由PACU护士填写，经双方核对无误后签名。", ptzt1, Brushes.Black, new Point(x, y));
            y = y + 20;
         


         





            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认退出?", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            { 
                Close();
            }

{

   //delete

}
           
          
        }

    }
        #endregion

     

    

     
}
