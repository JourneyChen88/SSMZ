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
    public partial class BLbbsjdan : Form
    {
        adims_DAL.mz dal = new adims_DAL.mz();
        adims_BLL.mz bll = new adims_BLL.mz();
        string patID;
        public BLbbsjdan(string patID1)
        {
            patID = patID1;
            InitializeComponent();
        }

        private void BLbbsjdan_Load(object sender, EventArgs e)
        {
            Info();
        }
        #region<<显示信息>>
        private void Info()
        {
            DataTable dt = dal.GetBlbesjdan(patID);
            DataRow dr1 = dt.Rows[0];
            textBoxname.Text = dr1["Patname"].ToString();
            textBoxsex.Text = dr1["Patsex"].ToString();
            textBoxage.Text = dr1["Patage"].ToString();
            textBoxzubie.Text = dr1["PatNation"].ToString();
            textBoxzhuyuanID.Text = dr1["PatID"].ToString();
            textBoxkebie.Text = dr1["Patdpm"].ToString();


        }
        #endregion
        #region<<保存信息>>
        private void buttonbaocun_Click(object sender, EventArgs e)
        {
            baocun();
        }
        private void baocun()
        {
            string AddItem = "";
            string AddItem1 = "";
            string AddItem2 = "";
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
            SQF.Add("ZhuYuanID", patID);
            SQF.Add("rate", Convert.ToDateTime(dtVisitDate.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
            SQF.Add("BLID", textBoxblbh.Text);
            SQF.Add("name", textBoxname.Text);
            SQF.Add("sex", textBoxsex.Text);
            SQF.Add("age", textBoxage.Text);
            SQF.Add("zubie", textBoxzubie.Text);
            SQF.Add("city", textBoxjiguan.Text);
            SQF.Add("job", textBoxzhiye.Text);
            SQF.Add("hospital", textBoxyiyuan.Text);
            SQF.Add("kebie", textBoxkebie.Text);
            SQF.Add("mzhenid", textBoxmzh.Text);
            AddItem = "";
            if (checkBox1.Checked) AddItem += "区保";
            if (checkBox2.Checked) AddItem += "市保";
            if (checkBox4.Checked) AddItem += "铁保";
            if (checkBox3.Checked) AddItem += "商保";
            if (checkBox6.Checked) AddItem += "兵保";
            if (checkBox5.Checked) AddItem += "农合";
            if (checkBox8.Checked) AddItem += "非医保";
            if (checkBox7.Checked) AddItem += "非农合";
            SQF.Add("shebao", AddItem);
            SQF.Add("Sjcl1", textBox5.Text);
            SQF.Add("Sjcl2", textBox6.Text);
            SQF.Add("Sjcl3", textBox7.Text);
            SQF.Add("SJmd", textBox8.Text);
            SQF.Add("SJrq", Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
            AddItem1 = "";
            if (checkBox11.Checked) AddItem1 += "普通";
            if (checkBox10.Checked) AddItem1 += "加急";
            if (checkBox9.Checked) AddItem1 += "冰冻";
            SQF.Add("Jcfa", AddItem1);
            SQF.Add("JStz", textBox9.Text);
            SQF.Add("bfshsj", textBox10.Text);
            SQF.Add("ZLtime", Convert.ToDateTime(dateTimePicker2.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
            SQF.Add("ZLdaxiao", textBox11.Text);
            SQF.Add("Buwei", textBox12.Text);
            SQF.Add("ZYqk", textBox13.Text);
            SQF.Add("hunfou", textBox15.Text);
            SQF.Add("yuejingzhouqi", textBox14.Text);
            SQF.Add("mociyuejing", Convert.ToDateTime(dateTimePicker3.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
            SQF.Add("Qtjiancha", textBox16.Text);
            SQF.Add("linchuangzhenduan", textBox18.Text);
            SQF.Add("songjianyishi", textBox17.Text);
            SQF.Add("fuyan", textBox20.Text);
            SQF.Add("qchuojianhaoma", textBox19.Text);
            SQF.Add("dtryjianchaju", textBox21.Text);
            AddItem2 = "";
            if (checkBox11.Checked) AddItem2 += "A";
            if (checkBox11.Checked) AddItem2 += "B";
            if (checkBox11.Checked) AddItem2 += "C";
            if (checkBox11.Checked) AddItem2 += "D";
            if (checkBox11.Checked) AddItem2 += "E";
            if (checkBox11.Checked) AddItem2 += "F";
            SQF.Add("ZzKshu", AddItem2);
            SQF.Add("jianchazhe", textBox22.Text);
            SQF.Add("binglizhenduan", textBox23.Text);
            SQF.Add("baogaozhe", textBoxBGz.Text);
            SQF.Add("baogaotime", Convert.ToDateTime(dateTimePicker4.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
            DataTable dt = dal.GetbcBlbesjdan(patID);

            if (dt.Rows.Count > 0)
            {
                result = dal.UpdateBlbesjdan(SQF);
                MessageBox.Show("修改成功！");
            }
            else
            {
                result = dal.InsertBlbesjdan(SQF);
                MessageBox.Show("保存成功！");
            }
           


        }
        #endregion
        #region<<打印信息>>
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tab = 0;
            //printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            //printPreviewDialog1.WindowState = FormWindowState.Maximized;
            ////           //printDocument1.DefaultPageSettings.PaperSize =
            ////    new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
        }
        int tab = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Font ptzt = new Font("宋体", 11);//普通字体
            Font ptzt1 = new Font("宋体", 10, FontStyle.Bold);
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号    
            Font ptzt4 = new Font("宋体", 11, FontStyle.Bold);//加粗11号
            Font ptzt5 = new Font("宋体", 16, FontStyle.Bold);//加粗16号
            int y = 50; int x = 80-30; int y1 = 0;
            if (tab < 1)
            {
                y = y + 70;
                e.Graphics.DrawLine(Pens.Black, x + 415, y + 195, x + 415, y + 305);

                e.Graphics.DrawLine(Pens.Black, x + 100, y + 0, x + 100, y + 900);//竖线
                //e.Graphics.DrawLine(Pens.Black, x + 660, y + 0, x + 660, y + 900);//竖线
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 105, x + 680, y + 105);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 135, x + 680, y + 135);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 165, x + 680, y + 165);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 195, x + 680, y + 195);
                e.Graphics.DrawLine(Pens.Black, x + 415, y + 230, x + 680, y + 230);
                e.Graphics.DrawLine(Pens.Black, x + 415, y + 265, x + 680, y + 265);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 305, x + 680, y + 305);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 335, x + 680, y + 335);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 500, x + 680, y + 500);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 590, x + 680, y + 590);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 690, x + 680, y + 690);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 720, x + 680, y + 720);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 790, x + 680, y + 790);
                e.Graphics.DrawLine(Pens.Black, x + 100, y + 840, x + 680, y + 840);

                string title = "新疆医科大学附属第五医院";
                e.Graphics.DrawString(title, ptzt3, Brushes.Black, new Point(x + 240, y));
                y = y + 40;
                string title1 = "病 理 标 本 送 检 单";
                e.Graphics.DrawString(title1, ptzt5, Brushes.Black, new Point(x + 240, y));
                y = y + 45; y1 = y + 15;
                e.Graphics.DrawString("收到日期：" + dtVisitDate.Value.ToString("yyyy-MM-dd "), ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawString("病理编号：" + textBoxblbh.Text.Trim(), ptzt, Brushes.Black, new Point(x + 400, y));
                y = y + 30;
                e.Graphics.DrawString("姓名:" + textBoxname.Text.Trim(), ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawString("性别:" + textBoxsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 240, y));
                e.Graphics.DrawString("年龄:" + textBoxage.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
                e.Graphics.DrawString("族别:" + textBoxzubie.Text.Trim(), ptzt, Brushes.Black, new Point(x + 360, y));
                e.Graphics.DrawString("籍贯:" + textBoxjiguan.Text.Trim(), ptzt, Brushes.Black, new Point(x + 460, y));
                e.Graphics.DrawString("职业:" + textBoxzhiye.Text.Trim(), ptzt, Brushes.Black, new Point(x + 580, y));
                y = y + 30;
                e.Graphics.DrawString("医院:" + textBoxyiyuan.Text.Trim(), ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawString("科别:" + textBoxkebie.Text.Trim(), ptzt, Brushes.Black, new Point(x + 280, y));
                e.Graphics.DrawString("门诊号:" + textBoxmzh.Text.Trim(), ptzt, Brushes.Black, new Point(x + 440, y));
                e.Graphics.DrawString("住院号:" + textBoxzhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 540, y));
                y = y + 30;
                e.Graphics.DrawString("□区保、□市保、□铁保、□商保、□兵保、□农合、□非医保、□非农合", ptzt, Brushes.Black, new Point(x + 100, y));
                if (checkBox1.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 100, y));
                if (checkBox2.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 163, y));
                if (checkBox4.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 225, y));
                if (checkBox3.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 287, y));
                if (checkBox6.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 347, y));
                if (checkBox5.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 410, y));
                if (checkBox8.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 470, y));
                if (checkBox7.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 548, y));
                y = y + 30;
                e.Graphics.DrawString("请切实填写表", ptzt, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("送检材料", ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawString("1." + textBox5.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
                e.Graphics.DrawString("送检目的：" + textBox8.Text.Trim(), ptzt, Brushes.Black, new Point(x + 420, y));
                y = y + 40;
                e.Graphics.DrawString("格内各项，如", ptzt, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("及取材部位：", ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawString("2." + textBox6.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
                e.Graphics.DrawString("送检日期：" + dateTimePicker1.Value.ToString("yyyy-MM-dd "), ptzt, Brushes.Black, new Point(x + 420, y));
                y = y + 38;
                e.Graphics.DrawString("系肿瘤详细写", ptzt, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("3." + textBox7.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
                y = y + 30;
                e.Graphics.DrawString("明肿瘤之部位", ptzt, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("检查方法：□普通     □加急     □冰冻", ptzt, Brushes.Black, new Point(x + 100, y));
                if (checkBox11.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 178, y));
                if (checkBox10.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 263, y));
                if (checkBox9.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 347, y));
                y = y + 30;
                e.Graphics.DrawString("大小及病程，", ptzt, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("临床简史及体征：", ptzt, Brushes.Black, new Point(x + 100, y));
                y = y + 20;
                e.Graphics.DrawString("字体务求清楚", ptzt, Brushes.Black, new Point(x, y));
                string str = "";
                int a = textBox9.TextLength / 35;
                for (int i = 0; i <= textBox9.TextLength / 35; i++)
                {
                    if (i < a)
                    {
                        str += textBox9.Text.Substring(i * 35, 35) + Environment.NewLine;
                    }
                    else
                    {
                        str += textBox9.Text.Substring(i * 35);
                    }
                }
                e.Graphics.DrawString(str, ptzt, Brushes.Black, new Point(x + 100, y));
                y = y + 20;
                e.Graphics.DrawString("  整洁。", ptzt, Brushes.Black, new Point(x, y));
                y = y + 120;
                e.Graphics.DrawString("标本要求及时", ptzt, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("剖腹探查或手术所见：", ptzt, Brushes.Black, new Point(x + 100, y));
                y = y + 20;
                e.Graphics.DrawString(" 固定在10%", ptzt, Brushes.Black, new Point(x, y));
                string str1 = "";
                int b = textBox10.TextLength / 35;
                for (int i = 0; i <= textBox10.TextLength / 35; i++)
                {
                    if (i < b)
                    {
                        str1 += textBox10.Text.Substring(i * 35, 35) + Environment.NewLine;
                    }
                    else
                    {
                        str1 += textBox10.Text.Substring(i * 35);
                    }
                }
                e.Graphics.DrawString(str1, ptzt, Brushes.Black, new Point(x + 100, y));
                y = y + 20;
                e.Graphics.DrawString(" Formalin液", ptzt, Brushes.Black, new Point(x, y));
                y = y + 20;
                e.Graphics.DrawString("中,以防腐败.", ptzt, Brushes.Black, new Point(x, y));
                y = y + 30;
                e.Graphics.DrawString("肿瘤发现时间：" + dateTimePicker2.Value.ToString("yyyy-MM-dd "), ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawString("大小" + textBox11.Text.Trim(), ptzt, Brushes.Black, new Point(x + 400, y));
                e.Graphics.DrawString("部位" + textBox12.Text.Trim(), ptzt, Brushes.Black, new Point(x + 550, y));
                y = y + 30;
                e.Graphics.DrawString("转移情况：", ptzt, Brushes.Black, new Point(x + 100, y));
                y = y + 20;
                string str2 = "";
                int c = textBox13.TextLength / 35;
                for (int i = 0; i <= textBox13.TextLength / 35; i++)
                {
                    if (i < c)
                    {
                        str1 += textBox13.Text.Substring(i * 35, 35) + Environment.NewLine;
                    }
                    else
                    {
                        str1 += textBox13.Text.Substring(i * 35);
                    }
                }
                e.Graphics.DrawString(str2, ptzt, Brushes.Black, new Point(x + 100, y));
                y = y + 55;
                e.Graphics.DrawString("婚否:" + textBox15.Text.Trim(), ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawString("月经周期:" + textBox14.Text.Trim(), ptzt, Brushes.Black, new Point(x + 250, y));
                e.Graphics.DrawString("末次月经:" + dateTimePicker3.Value.ToString("yyyy-MM-dd "), ptzt, Brushes.Black, new Point(x + 450, y));
                y = y + 30;
                e.Graphics.DrawString("实验室检查及其他有关检查：", ptzt, Brushes.Black, new Point(x + 100, y));
                y = y + 20;
                string str3 = "";
                int d = textBox16.TextLength / 35;
                for (int i = 0; i <= textBox16.TextLength / 35; i++)
                {
                    if (i < d)
                    {
                        str3 += textBox16.Text.Substring(i * 35, 35) + Environment.NewLine;
                    }
                    else
                    {
                        str3 += textBox16.Text.Substring(i * 35);
                    }
                }
                e.Graphics.DrawString(str3, ptzt, Brushes.Black, new Point(x + 100, y));
                y = y + 60;
                e.Graphics.DrawString("临床诊断:" + textBox18.Text.Trim(), ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawString("送检医师:" + textBox17.Text.Trim(), ptzt, Brushes.Black, new Point(x + 400, y));
                y = y + 40;
                e.Graphics.DrawString("附言:" + textBox20.Text.Trim(), ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawString("前次病理活检号码:" + textBox19.Text.Trim(), ptzt, Brushes.Black, new Point(x + 400, y));

            }
            for(int m=0; m<1;m++)
            {
            if (tab  < 1)
            {
                tab++;
                e.HasMorePages = true;
                return;
            }
            
            else if (tab > 0)
            {
                y = y + 60;
                e.Graphics.DrawString("  病理检查记录" , ptzt, Brushes.Black, new Point(x + 50, y));
                y = y + 20;
                e.Graphics.DrawLine(Pens.Black, x + 550, y + 0, x + 550, y + 900);//竖线
                e.Graphics.DrawLine(Pens.Black, x , y + 0, x + 680, y + 0);
                e.Graphics.DrawLine(Pens.Black, x, y + 500, x + 550, y + 500);
                y = y + 10;
                e.Graphics.DrawString("大体肉眼观察及显微镜下检查记录：", ptzt, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("组织块数：", ptzt, Brushes.Black, new Point(x + 555, y));
                y = y + 20;
                string str4 = "";
                int f = textBox21.TextLength / 33;
                for (int i = 0; i <= textBox21.TextLength / 33; i++)
                {
                    if (i < f)
                    {
                        str4 += textBox21.Text.Substring(i * 33, 33) + Environment.NewLine;
                    }
                    else
                    {
                        str4 += textBox21.Text.Substring(i * 33);
                    }
                }
                e.Graphics.DrawString(str4, ptzt, Brushes.Black, new Point(x + 20, y));
                e.Graphics.DrawString("□A", ptzt, Brushes.Black, new Point(x + 555, y));
                e.Graphics.DrawString("□B", ptzt, Brushes.Black, new Point(x + 555, y + 20));
                e.Graphics.DrawString("□C", ptzt, Brushes.Black, new Point(x + 555, y + 40));
                e.Graphics.DrawString("□D", ptzt, Brushes.Black, new Point(x + 555, y + 60));
                e.Graphics.DrawString("□E", ptzt, Brushes.Black, new Point(x + 555, y + 80));
                e.Graphics.DrawString("□F", ptzt, Brushes.Black, new Point(x + 555, y + 100));
                if (checkBox12.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 555, y ));
                if (checkBox13.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 555, y + 20));
                if (checkBox15.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 555, y + 40));
                if (checkBox14.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 555, y + 60));
                if (checkBox17.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 555, y + 80));
                if (checkBox16.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 555, y + 100));
                y = y + 450;
                e.Graphics.DrawString("检查者：" + textBox22.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
                y = y + 30;
                e.Graphics.DrawString("病理诊断：", ptzt, Brushes.Black, new Point(x , y));
                e.Graphics.DrawString("请做：", ptzt, Brushes.Black, new Point(x + 555, y));
                y = y + 20;
                string str5 = "";
                int g = textBox23.TextLength / 33;
                for (int i = 0; i <= textBox23.TextLength / 33; i++)
                {
                    if (i < g)
                    {
                        str5 += textBox23.Text.Substring(i * 33, 33) + Environment.NewLine;
                    }
                    else
                    {
                        str5 += textBox23.Text.Substring(i * 33);
                    }
                }
                e.Graphics.DrawString(str5, ptzt, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("范吉森染色", ptzt, Brushes.Black, new Point(x + 555, y));
                e.Graphics.DrawString("弹力纤维染色", ptzt, Brushes.Black, new Point(x + 555, y + 20));
                e.Graphics.DrawString("网织染色", ptzt, Brushes.Black, new Point(x + 555, y + 40));
                e.Graphics.DrawString("马劳瑞染色", ptzt, Brushes.Black, new Point(x + 555, y + 60));
                e.Graphics.DrawString("脂肪染色", ptzt, Brushes.Black, new Point(x + 555, y + 80));
                e.Graphics.DrawString("粘液染色", ptzt, Brushes.Black, new Point(x + 555, y + 100));
                e.Graphics.DrawString("方太那染色", ptzt, Brushes.Black, new Point(x + 555, y + 120));
                e.Graphics.DrawString("革兰氏染色", ptzt, Brushes.Black, new Point(x + 555, y + 140));
                e.Graphics.DrawString("抗酸染色", ptzt, Brushes.Black, new Point(x + 555, y + 160));
                e.Graphics.DrawString("深切", ptzt, Brushes.Black, new Point(x + 555, y + 180));
                e.Graphics.DrawString("补切", ptzt, Brushes.Black, new Point(x + 555, y + 200));
                e.Graphics.DrawString("蜡块数", ptzt, Brushes.Black, new Point(x + 555, y + 220));
                e.Graphics.DrawString("切片数", ptzt, Brushes.Black, new Point(x + 555, y + 240));
                y = y + 300;
                e.Graphics.DrawString("报告者：" + textBoxBGz.Text.Trim(), ptzt, Brushes.Black, new Point(x + 150, y ));
                e.Graphics.DrawString("      年    月    日 ", ptzt, Brushes.Black, new Point(x + 350, y));


            }
            }
        }
        #endregion
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
             
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Close();
        }

     

       

    } 
}
