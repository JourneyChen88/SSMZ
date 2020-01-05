using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class menzhencj : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int Mzjldid;//麻醉ID
        //string CreateTime;//采集时间
        string RUTime;//入室的时间
        string jiange;//时间间隔
        public menzhencj()
        {
            InitializeComponent();
        }
        public menzhencj(int mzid, string RS, string JG)
        {
            Mzjldid = mzid;          
            jiange = JG;
            RUTime = RS;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" )
            {
                try
                {

                    TimeSpan t = new TimeSpan();
                    Random rd = new Random();
                    DateTime dt1 = Convert.ToDateTime(dtStart.Value.ToString("yyyy/MM/dd HH:mm"));
                    DateTime dt2 = Convert.ToDateTime(dtEnd.Value.ToString("yyyy/MM/dd HH:mm"));
                    int JGs = Convert.ToInt32(jiange);
                    t = dt2 - dt1;
                    string NIBPS;//收缩压
                    string NIBPD;//舒张压
                    string RRC;//呼吸
                    //int HR;//心率
                    string Pulse;//脉搏
                    string SpO2;//血氧
                    int ETCO2;//呼吸二氧化碳
                    string TEMP;//温度
                    //string BIS;//bis
                    //string cvp;
                    int X = (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) / JGs;
                    for (int i = 0; i <= X; i++)
                    {
                        NIBPS = textBox1.Text;
                        NIBPD = textBox2.Text;
                        RRC = textBox3.Text;
                        //HR = new Random().Next(31, 35);
                        Pulse = textBox4.Text;
                        SpO2 = textBox5.Text;
                        ETCO2 = new Random().Next(31, 35);
                        TEMP = "0";
                        
                        if (bll.GetPointSinglePACU(Mzjldid, dt1).Rows.Count == 0)
                        {
                            int result = bll.AddPointmenzhen(Mzjldid, dt1, NIBPS.ToString(), NIBPD.ToString(), RRC.ToString(), Pulse.ToString(), SpO2.ToString(), ETCO2.ToString(),TEMP.ToString());
                        }
                        //else
                        //    MessageBox.Show("该时间点数据已存在，不能重复添加");
                        dt1 = dt1.AddMinutes(JGs);
                    }
                    MessageBox.Show("设置完成");
                    this.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("设置失败！请重试！");
                }
            }
            else
            {
                MessageBox.Show("如果没有数据请填写0");
            }
        }

        private void menzhencj_Load(object sender, EventArgs e)
        {
            dtStart.Text = RUTime;
            dtEnd.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtEnd.Format = DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                TimeSpan t = new TimeSpan();
                Random rd = new Random();
                DateTime dt1 = Convert.ToDateTime(dtStart.Value.ToString("yyyy/MM/dd HH:mm"));
                DateTime dt2 = Convert.ToDateTime(dtEnd.Value.ToString("yyyy/MM/dd HH:mm"));
                int JGs = Convert.ToInt32(jiange);
                t = dt2 - dt1;
                int NIBPS;//收缩压
                int NIBPD;//舒张压
                int RRC;//呼吸
                //int HR;//心率
                int Pulse;//脉搏
                int SpO2;//血氧
                int ETCO2;//呼吸二氧化碳
                string TEMP;//温度
                //string BIS;//bis
                //string cvp;
                int X = (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes) / JGs;
                for (int i = 0; i <= X; i++)
                {
                    NIBPS = new Random().Next(110, 120);
                    NIBPD = new Random().Next(65, 70);
                    RRC = new Random().Next(16, 20);
                    //HR = new Random().Next(31, 35);
                    Pulse = new Random().Next(70, 80);
                    SpO2 = 100;
                    ETCO2 = new Random().Next(31, 35);
                    TEMP = "0";

                    if (bll.GetPointSinglePACU(Mzjldid, dt1).Rows.Count == 0)
                    {
                        int result = bll.AddPointmenzhen(Mzjldid, dt1, NIBPS.ToString(), NIBPD.ToString(), RRC.ToString(), Pulse.ToString(), SpO2.ToString(), ETCO2.ToString(), TEMP.ToString());
                    }
                    //else
                    //    MessageBox.Show("该时间点数据已存在，不能重复添加");
                    dt1 = dt1.AddMinutes(JGs);
                }
                MessageBox.Show("设置完成");
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("设置失败！请重试！");
            }
        }
    }
}
