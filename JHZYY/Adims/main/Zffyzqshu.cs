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
    public partial class Zffyzqshu : Form
    {
        adims_BLL.mz bll = new adims_BLL.mz();
        adims_DAL.mz dal = new adims_DAL.mz();
        string padID;

        public Zffyzqshu(string padID1)
        {
            padID = padID1;
            InitializeComponent();
        }

        private void Zffyzqshu_Load(object sender, EventArgs e)
        {
            info();
        }
        #region<<显示信息>>
        private void info()//基本信息
        {  
            DataTable dt = dal.GetZFFzhiqingtys(padID);
            DataRow dr1 = dt.Rows[0];
            textBoxname.Text = dr1["Patname"].ToString();
            textBoxsex.Text = dr1["Patsex"].ToString();
            textBoxage.Text = dr1["Patage"].ToString();
            textBoxzubie.Text = dr1["PatNation"].ToString();
            textBoxpadID.Text = dr1["PatID"].ToString();
            textchuanghao.Text = dr1["patbedno"].ToString();
            textBoxJBzd.Text = dr1["pattmd"].ToString();
        }
        #endregion
        #region<<保存信息>>
        private void buttonbaocun_Click(object sender, EventArgs e)
        {
           
            //if (textBoxRuanlb.Text == "")
            //{
            //    MessageBox.Show("人员类别不能为空!");
            //    textBoxRuanlb.Focus();//获得焦点
            //    return;
            //}
            //if (textBoxJBzd.Text == "")
            //{
            //    MessageBox.Show("疾病诊断不能为空!");
            //    textBoxJBzd.Focus();//获得焦点
            //    return;
            //}
            //if (textBoxYwname.Text == "")
            //{
            //    MessageBox.Show("药物名称不能为空!");

            //    textBoxYwname.Focus();//获得焦点
            //    return;
            //}
            //if (textBoxinfobyx.Text == "")
            //{
            //    MessageBox.Show("必要性不能为空!");
            //    textBoxinfobyx.Focus();//获得焦点
            //    return;
            //}
            //if (textBoxywname2.Text == "")
            //{
            //    MessageBox.Show("药物名称不能为空!");
            //    textBoxywname2.Focus();//获得焦点
            //    return;
            //}
            //if (textBoxinfobyx2.Text == "")
            //{
            //    MessageBox.Show("必要性不能为空!");
            //    textBoxinfobyx2.Focus();//获得焦点
            //    return;
            //}
            //if (textBoxzxkeshi.Text == "")
            //{
            //    MessageBox.Show("执行科室不能为空!");
            //    textBoxzxkeshi.Focus();//获得焦点
            //    return;
            //}
            //if (textBox7.Text == "")
            //{
            //    MessageBox.Show("单价金额不能为空!");
            //    textBox7.Focus();
            //    return;
            //}
            //if (textBox8.Text == "")
            //{
            //    MessageBox.Show("患者（亲属）签字为空!");
            //    textBox8.Focus();
            //    return;
            //} if (textBoxJCXMname.Text == "")
            //{
            //    MessageBox.Show("检查项目名称不能为空!");
            //    textBoxJCXMname.Focus();
            //    return;
            //}
            //if (textBox17.Text == "")
            //{
            //    MessageBox.Show("必要性不能为空!");
            //    textBox17.Focus();//获得焦点
            //    return;
            //}
            //if (textBox16.Text == "")
            //{
            //    MessageBox.Show("风险性不能为空!");
            //    textBox16.Focus();//获得焦点
            //    return;
            //}
            //if (textBox20.Text == "")
            //{
            //    MessageBox.Show("单价金额不能为空!");
            //    textBox20.Focus();//获得焦点
            //    return;
            //}
            //if (textBox21.Text == "")
            //{
            //    MessageBox.Show("患者（亲属）签字为空!");
            //    textBox21.Focus();//获得焦点
            //    return;
            //}
            //if (textBoxYlxiangmu.Text == "")
            //{
            //    MessageBox.Show("医疗项目为空!");
            //    textBoxYlxiangmu.Focus();//获得焦点
            //    return;
            //}
            //if (textBox19.Text == "")
            //{
            //    MessageBox.Show("必要性不能为空!");
            //    textBox19.Focus();//获得焦点
            //    return;
            //}
            //if (textBox18.Text == "")
            //{
            //    MessageBox.Show("说明不能为空!");
            //    textBox18   .Focus();//获得焦点
            //    return;
            //}
            //if (textBox14.Text == "")
            //{
            //    MessageBox.Show("单价金额不能为空!");
            //    textBox14.Focus();//获得焦点
            //    return;
            //}
            //if (textBox15.Text == "")
            //{
            //    MessageBox.Show("患者（亲属）签字为空!");
            //    textBox15.Focus();//获得焦点
            //    return;
            //}
            //if (textBox23.Text == "")
            //{
            //    MessageBox.Show("患者（亲属）签字为空!");
            //    textBox23.Focus();//获得焦点
            //    return;
            //} if (textBox24.Text == "")
            //{
            //    MessageBox.Show("医师签字为空!");
            //    textBox24.Focus();//获得焦点
            //    return;
            //}
            //if(textBoxphone.Text == "")
            //{
            //    MessageBox.Show("联系电话不能为空!");
            //    textBoxphone.Focus();
            //    return;
            //}
            //if (textBoxphone.TextLength < 11 || textBoxphone.TextLength > 11)
            //{
            //    MessageBox.Show("联系电话有误!");
            //    textBoxphone.Focus();
            //    return;
            //}
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
            SQF.Add("PatZYid", padID);
            SQF.Add("name", textBoxname.Text);
            SQF.Add("sex", textBoxsex.Text);
            SQF.Add("age", textBoxage.Text);
            SQF.Add("zubie", textBoxzubie.Text);
            SQF.Add("Rlbie", textBoxRuanlb.Text);
            SQF.Add("jbzhenduan", textBoxJBzd.Text);
            SQF.Add("Ywname1", textBoxYwname.Text);
            SQF.Add("byx1", textBoxinfobyx.Text);
            SQF.Add("Ywname2", textBoxywname2.Text);
            SQF.Add("byx2", textBoxinfobyx2.Text);
            SQF.Add("phone", textBoxphone.Text);
            SQF.Add("zxkeshi", textBoxzxkeshi.Text);
            SQF.Add("yaowudj", textBox7.Text);
            SQF.Add("huanzheqianzi", textBox8.Text);
            SQF.Add("JcXmname", textBoxJCXMname.Text);
            SQF.Add("byx3", textBox17.Text);
            SQF.Add("fx1", textBox16.Text);
            SQF.Add("xmdj", textBox14.Text);
            SQF.Add("xmhzqianzi", textBox15.Text);
            SQF.Add("Ylxm", textBoxYlxiangmu.Text);
            SQF.Add("byx4", textBox19.Text);
            SQF.Add("shuoming", textBox18.Text);
            SQF.Add("ylxmdj", textBox20.Text);
            SQF.Add("ylxmqianzi", textBox21.Text);
            SQF.Add("zhHZqianzi", textBox23.Text);
            SQF.Add("yishiqianzi", textBox24.Text);
            SQF.Add("huanzherate", Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            SQF.Add("yishirate", Convert.ToDateTime(dateTimePicker2.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            SQF.Add("yaoping2danj", comboBox1.Text);
            
            DataTable dt = dal.GetbaocZffzhiqingtys(padID);

            if (dt.Rows.Count > 0)
                result = dal.Updatezffzhiqingtys(SQF);
            else
                result = dal.Insertzffzhiqingtys(SQF);
            {
                MessageBox.Show("保存成功！");

            }


        }

        #endregion   
        #region<<打印信息>>
        private void button2_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
           
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
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
            Font ptzt = new Font("宋体", 12);//普通字体
            Font ptzt1 = new Font("新宋体", 8);//普通字体
            Font ptzt5 = new Font("新宋体", 14);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 16, FontStyle.Bold);//加粗14号
            Font ptzt4 = new Font("新宋体", 19, FontStyle.Bold);//加粗16号
            int y = 40; int x = 65; int y1 = 0;
            y = y + 50;
            string title1 = "新疆医科大学第五附属医院";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 195, y));
            y = y + 30;
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 5, x + 660, y + 5);
            y = y + 20;
            string title2 = "住院患者（参保患者）";
            e.Graphics.DrawString(title2, ptzt5, Brushes.Black, new Point(x, y));
            y = y + 30;
            string title3 = "用药、检查、治疗、高值耗材自付费用知情同意书           床号：" + textchuanghao.Text;
            e.Graphics.DrawString(title3, ptzt5, Brushes.Black, new Point(x, y));
           
            //竖线
            e.Graphics.DrawLine(Pens.Black, x, y + 40, x, y + 830);
            e.Graphics.DrawLine(Pens.Black, x + 660, y + 40, x + 660, y + 830);
            e.Graphics.DrawLine(Pens.Black, x + 120, y + 40, x + 120, y + 675);
            e.Graphics.DrawLine(Pens.Black, x + 290, y + 40, x + 290, y + 75);
            e.Graphics.DrawLine(Pens.Black, x + 350, y + 40, x + 350, y + 75);
            e.Graphics.DrawLine(Pens.Black, x + 410, y + 40, x + 410, y + 75);
            e.Graphics.DrawLine(Pens.Black, x + 470, y + 40, x + 470, y + 75);
            e.Graphics.DrawLine(Pens.Black, x + 530, y + 40, x + 530, y + 75);
            e.Graphics.DrawLine(Pens.Black, x + 570, y + 40, x + 570, y + 75);
            e.Graphics.DrawLine(Pens.Black, x + 410, y + 75, x + 410, y + 675);
            e.Graphics.DrawLine(Pens.Black, x + 530, y + 75, x + 530, y + 675);

            //横线
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 830, x + 660, y + 830);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 40, x + 660, y + 40);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 75, x + 660, y + 75);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 115, x + 660, y + 115);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 155, x + 660, y + 155);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 200, x + 660, y + 200);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 245, x + 410, y + 245);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 290, x + 410, y + 290);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 335, x + 660, y + 335);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 415, x + 660, y + 415);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 460, x + 410, y + 460);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 505, x + 660, y + 505);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 585, x + 660, y + 585);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 630, x + 410, y + 630);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 675, x + 660, y + 675);
            y = y + 50;
            e.Graphics.DrawString("姓名", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("[ " + textBoxname.Text.Trim() + " ]", ptzt, Brushes.Black, new Point(x+125, y));
            e.Graphics.DrawString("性别", ptzt, Brushes.Black, new Point(x + 300, y));
            e.Graphics.DrawString("[ " + textBoxsex.Text.Trim() + " ]", ptzt, Brushes.Black, new Point(x + 350, y));
            e.Graphics.DrawString("年龄", ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawString("[ " + textBoxage.Text.Trim() + " ]", ptzt, Brushes.Black, new Point(x + 470, y));
            e.Graphics.DrawString("族别", ptzt, Brushes.Black, new Point(x + 530, y));
            e.Graphics.DrawString("[" + textBoxzubie.Text.Trim() + "]", ptzt, Brushes.Black, new Point(x + 570, y));
            //e.Graphics.DrawString("姓名：" + textBoxname.Text.Trim(), ptzt, Brushes.Black, new Point(x + 175, y));
            //e.Graphics.DrawString("性别：" + textBoxsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 363, y));
            //e.Graphics.DrawString("年龄：" + this.textBoxage.Text.Trim(), ptzt, Brushes.Black, new Point(x + 440, y));
            y = y + 35;
            e.Graphics.DrawString("人员类别", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(textBoxRuanlb.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            e.Graphics.DrawString("家庭联系电话", ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawString(textBoxphone.Text.Trim(), ptzt, Brushes.Black, new Point(x + 540, y));
            y = y + 45;
            e.Graphics.DrawString("疾病诊断", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(textBoxJBzd.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            e.Graphics.DrawString("执行科室", ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawString(textBoxzxkeshi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 540, y));
            y = y + 45;
            e.Graphics.DrawString("药品名称", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(textBoxYwname.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            e.Graphics.DrawString("单价（金额）", ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawString("患者(亲属)签字", ptzt, Brushes.Black, new Point(x + 530, y));
            y = y + 45;
            e.Graphics.DrawString("必要性", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(textBoxinfobyx.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            e.Graphics.DrawString(textBox7.Text.Trim(), ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawString(textBox8.Text.Trim(), ptzt, Brushes.Black, new Point(x + 540, y));
            y = y + 45;
            e.Graphics.DrawString("药品名称", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(comboBox1.Text, ptzt, Brushes.Black, new Point(x + 420, y));
            
            e.Graphics.DrawString(textBoxywname2.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            y = y + 45;
            e.Graphics.DrawString("必要性", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(textBoxinfobyx2.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            y = y + 55;
            e.Graphics.DrawString("检查项目名称", ptzt, Brushes.Black, new Point(x + 10, y));
            string str = "";
            int a = textBoxJCXMname.Text.Length / 16;
            for (int i = 0; i <= textBoxJCXMname.Text.Length / 16; i++)
            {
                if (i < a)
                {
                    str += textBoxJCXMname.Text.Substring(i * 16, 16) + Environment.NewLine;
                }
                else
                {
                    str += textBoxJCXMname.Text.Substring(i * 16);
                }
            }
            e.Graphics.DrawString(str, ptzt, Brushes.Black, new Point(x + 125, y));
            e.Graphics.DrawString("单价（金额）", ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawString("患者(亲属)签字", ptzt, Brushes.Black, new Point(x + 530, y));
            y = y + 60;
            e.Graphics.DrawString("1、必要性", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(textBox17.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            e.Graphics.DrawString(textBox14.Text.Trim(), ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawString(textBox15.Text.Trim(), ptzt, Brushes.Black, new Point(x + 540, y));
            y = y + 45;
            e.Graphics.DrawString("2、风险性", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(textBox16.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            y = y + 55;
            e.Graphics.DrawString("医疗项目（含", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("单价（金额）", ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawString("患者(亲属)签字", ptzt, Brushes.Black, new Point(x + 530, y));
            string str1 = "";
            int b = textBoxYlxiangmu.Text.Length / 16;
            for (int i = 0; i <= textBoxJCXMname.Text.Length / 16; i++)
            {
                if (i < b)
                {
                    str1 += textBoxYlxiangmu.Text.Substring(i * 16, 16) + Environment.NewLine;
                }
                else
                {
                    str1 += textBoxYlxiangmu.Text.Substring(i * 16);
                }
            }
            e.Graphics.DrawString(str1, ptzt, Brushes.Black, new Point(x + 125, y));
            y = y + 20;
            e.Graphics.DrawString("材料及床位", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 55;
            e.Graphics.DrawString("1、必要性", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(textBox19.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            e.Graphics.DrawString(textBox20.Text.Trim(), ptzt, Brushes.Black, new Point(x + 420, y));
            e.Graphics.DrawString(textBox21.Text.Trim(), ptzt, Brushes.Black, new Point(x + 540, y));
            y = y + 45;
            e.Graphics.DrawString("2、风险性", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(textBox18.Text.Trim(), ptzt, Brushes.Black, new Point(x + 125, y));
            y = y + 35;
            e.Graphics.DrawString("根据自治区及乌鲁木齐市劳动保障部门的有关文件要求，上述用药、检查、治疗等", ptzt, Brushes.Black, new Point(x + 45, y));
            y = y + 20;
            e.Graphics.DrawString("全部或部分自费的项目已向患者本人或亲属交待清楚，同意自愿支付该款。", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 45;
            e.Graphics.DrawString("患者(亲属)签字："+ textBox23.Text.Trim(), ptzt, Brushes.Black, new Point(x + 30, y));
            e.Graphics.DrawString("医师签字：" + textBox24.Text.Trim(), ptzt, Brushes.Black, new Point(x + 410, y));
            y = y + 55;
            e.Graphics.DrawString("年     月     日" , ptzt, Brushes.Black, new Point(x + 145, y));
            e.Graphics.DrawString("年     月     日" , ptzt, Brushes.Black, new Point(x + 470, y));
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)//退出按钮
        {
            this.Close();
        }

      
      

       

      
    }
}
