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
    public partial class TNzuTiaoxingma : Form
    {
        adims_BLL.mz bll = new adims_BLL.mz();
        adims_DAL.mz dal = new adims_DAL.mz();
        string ZYNumber1,payTime1;
        bool IsRead = false;     
        public TNzuTiaoxingma(string ZYNumber)
        {  
            ZYNumber1=ZYNumber;
           // payTime1=payTime;
            InitializeComponent();
        }
        private void TNzuTiaoxingma_Load(object sender, EventArgs e)
        {
            information();
            yishi();
            Hushi();
        
        }
        #region<<显示信息>>
        private void information()//基本信息
        {
            DataTable info = dal.GetTiaoma(ZYNumber1);
            DataRow dt = info.Rows[0];
            textBoxname.Text = dt["Patname"].ToString();
            textBoxkebie.Text = dt["Patdpm"].ToString();
            textBoxsex.Text = dt["Patsex"].ToString();
            textBoxage.Text = dt["patage"].ToString();
            textBoxzhuyuanID.Text = dt["patID"].ToString();
            textBoxrate.Text = Convert.ToDateTime(dt["Odate"]).ToString("yyyy-MM-dd");
            textBoxmingcheng.Text = dt["Oname"].ToString();
        }
        private void yishi()//绑定医师
        {
            DataTable yishi = dal.GetAllshoushuyishi();
            comboBoxYSname.Items.Clear();
            for (int i = 0; i < yishi.Rows.Count; i++)
            {
                this.comboBoxYSname.Items.Add(yishi.Rows[i][0]);
            }
        }
        private void Hushi()//绑定护士
        {
            DataTable hushi = dal.GetAllshoushuhushi();
            comboBoxHSname.Items.Clear();
            for (int i = 0; i < hushi.Rows.Count; i++)
            {
                this.comboBoxHSname.Items.Add(hushi.Rows[i][0]);
            }
        }
        #endregion
        #region<<保存信息>>
        private void buttonbaocun_Click(object sender, EventArgs e)
        {
            baocun();
        }
        private void baocun()//保存信息字段顺序不能弄错
        {
            Dictionary<string, string> SQFS = new Dictionary<string, string>();
            int result = 0;

            SQFS.Add("PatZYid", ZYNumber1);  
            SQFS.Add("name", textBoxname.Text);
            SQFS.Add("sex", textBoxsex.Text);
            SQFS.Add("age", textBoxage.Text);
            SQFS.Add("keshi", textBoxkebie.Text);
            SQFS.Add("Ssrate", textBoxrate.Text);
            SQFS.Add("SsMingchen", textBoxmingcheng.Text);
            SQFS.Add("TiaoMA", textBoxtiaoma.Text);
            SQFS.Add("YSname", comboBoxYSname.Text);
            SQFS.Add("HSname", comboBoxHSname.Text);
            if (buttonbaocun.Enabled)
            {
                SQFS.Add("IsRead", "0");
            }
            else
            {
                SQFS.Add("IsRead", "1");
            }
            SQFS.Add("rate", Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));

            DataTable dt = dal.GetTiaomadan(ZYNumber1);

            if (dt.Rows.Count > 0)
                result = dal.UpdateTiaomadan(SQFS);
            else
                result = dal.InsertTiaomadan(SQFS);
            {
                MessageBox.Show("保存成功！");

            }
        }
        #endregion
        #region<<打印信息>>
        private void button2_Click(object sender, EventArgs e)//保存按钮
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                        new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        //打印信息
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("宋体", 12);//普通字体
            Font ptzt1 = new Font("新宋体", 8);//普通字体
            Font ptzt5 = new Font("新宋体", 14);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            Font ptzt4 = new Font("新宋体", 19, FontStyle.Bold);//加粗16号
            int y = 40; int x = 65; int y1 = 0;
            //短横线
            e.Graphics.DrawLine(Pens.Black, x + 42, y + 137, x + 175, y + 137);//科别
            e.Graphics.DrawLine(Pens.Black, x + 216, y + 137, x + 363, y + 137); //姓名
            e.Graphics.DrawLine(Pens.Black, x + 406, y + 137, x + 441, y + 137);//性别
            e.Graphics.DrawLine(Pens.Black, x + 480, y + 137, x + 520, y + 137);//年龄
            e.Graphics.DrawLine(Pens.Black, x + 586, y + 137, x + 659, y + 137);//住院号
            e.Graphics.DrawLine(Pens.Black, x + 81, y + 162, x + 230, y + 162);//手术日期
            e.Graphics.DrawLine(Pens.Black, x + 312, y + 162, x + 659, y + 162);//手术名称
            
            y = y + 50;
            string title1 = "新疆医科大学第五附属医院";
            e.Graphics.DrawString(title1, ptzt5, Brushes.Black, new Point(x + 205, y));
            y = y + 30;
            string title2 = "体 内 植 入 物 条 码 粘 贴 单";
            e.Graphics.DrawString(title2, ptzt4, Brushes.Black, new Point(x + 130, y));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawString("科别：" + textBoxkebie.Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("姓名：" + textBoxname.Text.Trim(), ptzt, Brushes.Black, new Point(x + 175, y));
            e.Graphics.DrawString("性别：" + textBoxsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 363, y));
            e.Graphics.DrawString("年龄：" + this.textBoxage.Text.Trim(), ptzt, Brushes.Black, new Point(x + 440, y));
            e.Graphics.DrawString("住院号：" + textBoxzhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 520, y));
            y = y + 25;
            e.Graphics.DrawString("手术日期：" + textBoxrate.Text, ptzt, Brushes.Black, new Point(x , y));
            e.Graphics.DrawString("手术名称：" + textBoxmingcheng.Text, ptzt, Brushes.Black, new Point(x + 236, y));
            y = y + 25;
          

            //竖线
            e.Graphics.DrawLine(Pens.Black, x, y + 0, x, y + 710);
            e.Graphics.DrawLine(Pens.Black, x + 660, y - 0, x + 660, y + 710);
            //横线
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 710, x + 660, y + 710);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 0, x + 660, y + 0);
            e.Graphics.DrawLine(Pens.Black, x + 110, y + 783, x + 250, y + 783);
            e.Graphics.DrawLine(Pens.Black, x + 122, y + 814, x + 262, y + 814);
            y = y + 10;
            e.Graphics.DrawString("体内植入物条码粘贴处：", ptzt, Brushes.Black, new Point(x, y));
            y = y + 25;
            string str = "";
            int a = textBoxtiaoma.TextLength / 35;
            for (int i = 0; i <= textBoxtiaoma.TextLength / 35; i++)
            {
                if(i<a)
                {
                    str += textBoxtiaoma.Text.Substring(i * 35, 35) + Environment.NewLine;
                }
                else
                {
                    str+=textBoxtiaoma.Text.Substring(i*35);
                }
            }
             e.Graphics.DrawString(str,ptzt, Brushes.Black, new Point(x + 30, y));

   y = y + 730;
            e.Graphics.DrawString("手术医师签名：" + comboBoxYSname.Text, ptzt, Brushes.Black, new Point(x, y));
            y = y + 30; 
            e.Graphics.DrawString("手术室护士签名：" + comboBoxHSname.Text, ptzt, Brushes.Black, new Point(x, y));
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
