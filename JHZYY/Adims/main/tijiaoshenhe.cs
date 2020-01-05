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
    public partial class tijiaoshenhe : Form
    {
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        string zhuyuanhao;
        string biandan;
        string brname;
        public tijiaoshenhe(string ZYNumber1, string biaodanneir)
        {
            zhuyuanhao = ZYNumber1;
            biandan = biaodanneir;
            InitializeComponent();
        }

        private void tijiaoshenhe_Load(object sender, EventArgs e)
        {

            try
            {
                string bb = "0";
                int vr = DAL.inserttijiao(zhuyuanhao, Program.customer.user_name, textBox2.Text, biandan, bb, DateTime.Now);
                if (vr > 0)
                {
                    MessageBox.Show("提交成功");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("提交失败" + ex);
            }
        }

        private void btndayin_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("K16", 740, 1020);
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
            Font ptzt14 = new Font("新宋体", 4);
            Font ptzt7 = new Font("新宋体", 7);//普通字体
            Font ptzt8 = new Font("新宋体", 8);//普通字体
            Font ptzt1 = new Font("新宋体", 9);//普通字体
            Font ptzt = new Font("新宋体", 12);//普通字体                                                                                                                                                                                                                                                                            
            Font ptzt11 = new Font("新宋体", 11);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 18, FontStyle.Bold);
            Font ptzt5 = new Font("新宋体", 13, FontStyle.Bold);//加粗14号
            Font ptzt4 = new Font("新宋体", 16, FontStyle.Bold);//加粗16号
            int y = 50; int x = 80; int y1 = 0; int y2 = 0;
            string title1 = "库车县人民医院手麻解锁申请表";

            e.Graphics.DrawString(title1, ptzt4, Brushes.Black, new Point(x + 140, y));
            y = y + 50;
            e.Graphics.DrawString("住院号:" + textBox1.Text, ptzt, Brushes.Black, new Point(x + 10, y + 5));
            e.Graphics.DrawString("病人姓名:" + brname, ptzt, Brushes.Black, new Point(x + 140, y + 5));
            e.Graphics.DrawString("申请科室:手麻科", ptzt, Brushes.Black, new Point(x + 350, y + 5));

            e.Graphics.DrawLine(ptp, new Point(x + 10, y), new Point(x + 600, y));
            e.Graphics.DrawLine(ptp, new Point(x + 10, y), new Point(x + 10, y + 400));
            e.Graphics.DrawLine(ptp, new Point(x + 10, y + 400), new Point(x + 600, y + 400));
            e.Graphics.DrawLine(ptp, new Point(x + 600, y), new Point(x + 600, y + 400));
            y = y + 30; y1 = y + 200;
            e.Graphics.DrawLine(ptp, new Point(x + 10, y), new Point(x + 600, y));
            e.Graphics.DrawString("解锁原因：", ptzt, Brushes.Black, new Point(x + 10, y + 5));
            y = y + 30;
            string strSS1 = "";
            int StrLengthSS = textBox2.Text.Trim().Length;
            int rowSS = StrLengthSS / 30;
            for (int i = 0; i <= rowSS; i++)//40个字符就换行
            {
                if (i < rowSS)
                    strSS1 = textBox2.Text.Trim().ToString().Substring(i * 30, 30); //从第i*40个开始，截取40个字符串
                else
                    strSS1 = textBox2.Text.Trim().ToString().Substring(i * 30);

                e.Graphics.DrawString(strSS1, ptzt, Brushes.Black, x + 30, y);
                y = y + 20;
            }
            y1 = y1 + 30;
            e.Graphics.DrawString("医生签字：", ptzt, Brushes.Black, new Point(x + 10, y1));
            e.Graphics.DrawString("科主任签字:", ptzt, Brushes.Black, new Point(x + 300, y1));
            y1 = y1 + 30;
            e.Graphics.DrawString("年    月    日", ptzt, Brushes.Black, new Point(x + 400, y1));
            y1 = y1 + 30;
            e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 600, y1));
            e.Graphics.DrawString("医务部（总值班）审签：", ptzt, Brushes.Black, new Point(x + 10, y1));
            y1 = y1 + 30;
            e.Graphics.DrawString("年    月    日", ptzt, Brushes.Black, new Point(x + 400, y1));
            y1 = y1 + 30;
            e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 600, y1));
            e.Graphics.DrawString("信息科存档", ptzt, Brushes.Black, new Point(x + 500, y1));

            y = 600;
            e.Graphics.DrawString(title1, ptzt4, Brushes.Black, new Point(x + 125, y));


            y = y + 50;
            e.Graphics.DrawString("住院号:" + textBox1.Text, ptzt, Brushes.Black, new Point(x + 10, y + 5));
            e.Graphics.DrawString("病人姓名:" + brname, ptzt, Brushes.Black, new Point(x + 140, y + 5));
            e.Graphics.DrawString("申请科室:手麻科", ptzt, Brushes.Black, new Point(x + 350, y + 5));
            e.Graphics.DrawLine(ptp, new Point(x + 10, y), new Point(x + 600, y));
            e.Graphics.DrawLine(ptp, new Point(x + 10, y), new Point(x + 10, y + 400));
            e.Graphics.DrawLine(ptp, new Point(x + 10, y + 400), new Point(x + 600, y + 400));
            e.Graphics.DrawLine(ptp, new Point(x + 600, y), new Point(x + 600, y + 400));
            y = y + 30; y1 = y + 200;
            e.Graphics.DrawLine(ptp, new Point(x + 10, y), new Point(x + 600, y));
            e.Graphics.DrawString("解锁原因：", ptzt, Brushes.Black, new Point(x + 10, y + 5));
            //string strSS1 = "";
            //int StrLengthSS = textBox2.Text.Trim().Length;
            //int rowSS = StrLengthSS / 30;
            y = y + 30;
            for (int i = 0; i <= rowSS; i++)//40个字符就换行
            {
                if (i < rowSS)
                    strSS1 = textBox2.Text.Trim().ToString().Substring(i * 30, 30); //从第i*40个开始，截取40个字符串
                else
                    strSS1 = textBox2.Text.Trim().ToString().Substring(i * 30);

                e.Graphics.DrawString(strSS1, ptzt, Brushes.Black, x + 30, y);
                y = y + 20;
            }
            y1 = y1 + 30;
            e.Graphics.DrawString("医生签字：", ptzt, Brushes.Black, new Point(x + 10, y1));
            e.Graphics.DrawString("科主任签字:", ptzt, Brushes.Black, new Point(x + 300, y1));
            y1 = y1 + 30;
            e.Graphics.DrawString("年    月    日", ptzt, Brushes.Black, new Point(x + 400, y1));
            y1 = y1 + 30;
            e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 600, y1));
            e.Graphics.DrawString("医务部（总值班）审签：", ptzt, Brushes.Black, new Point(x + 10, y1));
            y1 = y1 + 30;
            e.Graphics.DrawString("年    月    日", ptzt, Brushes.Black, new Point(x + 400, y1));
            y1 = y1 + 30;
            e.Graphics.DrawLine(ptp, new Point(x + 10, y1), new Point(x + 600, y1));
            e.Graphics.DrawString("医务科存档", ptzt, Brushes.Black, new Point(x + 500, y1));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
