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
    public partial class MazuiZhengtong : Form
    {
        string MZID, PATID;
        private ComboBox cmb_VAS = new ComboBox();
        private ComboBox cmb_zhenjing = new ComboBox();
        private ComboBox cmb_exin = new ComboBox();
        private ComboBox cmb_outu = new ComboBox();
        private ComboBox cmb_yundong = new ComboBox();
        private ComboBox cmb_niaozhuliu = new ComboBox();    
        adims_DAL.AdimsProvider apro = new adims_DAL.AdimsProvider();

        public MazuiZhengtong(string mzjldID, string patid)
        {
            PATID = patid;
            MZID = mzjldID;
            InitializeComponent();
        }
        private void Bind_VAS()
        {
            cmb_VAS.Items.Add("0—翻身、咳嗽时不痛");
            cmb_VAS.Items.Add("1—安静平卧不痛、翻身咳嗽时疼痛");            
            cmb_VAS.Items.Add("2—咳嗽疼痛，深呼吸不痛");
            cmb_VAS.Items.Add("3—安静平卧不痛、咳嗽深呼吸疼痛");
            cmb_VAS.Items.Add("5—安静平卧时持续疼痛");
            cmb_VAS.Items.Add("6—安静平卧时疼痛较重");
            cmb_VAS.Items.Add("7—疼痛较重，翻转不安、疲乏");
            cmb_VAS.Items.Add("8—持续疼痛难忍，全身大汗");
            cmb_VAS.Items.Add("9—剧烈疼痛无法忍受，有生不如死感");

            cmb_exin.Items.Add("0—无恶心");
            cmb_exin.Items.Add("1—休息时无恶心，运动时稍有恶心感");
            cmb_exin.Items.Add("2—休息时有间断恶心感");
            cmb_exin.Items.Add("3—休息时有持续性恶心感，运动时有严重恶心感");

           
            cmb_outu.Items.Add("0—无呕吐");
            cmb_outu.Items.Add("1—轻度呕吐(1~2次/天)");
            cmb_outu.Items.Add("2—中度呕吐(3~5次/天)");
            cmb_outu.Items.Add("3—重度呕吐(6次以上/天)");


            cmb_zhenjing.Items.Add("1—不安静，烦躁");
            cmb_zhenjing.Items.Add("2—安静，合作)");
            cmb_zhenjing.Items.Add("3—嗜睡，能听从指令");
            cmb_zhenjing.Items.Add("4—睡眠状态但可唤醒");
            cmb_zhenjing.Items.Add("5—呼吸反应迟钝");
            cmb_zhenjing.Items.Add("6—深睡觉状态，呼吸不醒");

            cmb_yundong.Items.Add("0—可伸屈大腿");
            cmb_yundong.Items.Add("1—可伸屈膝关节");
            cmb_yundong.Items.Add("2—可伸屈裸关节)");
            cmb_yundong.Items.Add("3—不能移动下肢");

            cmb_niaozhuliu.Items.Add("0—无尿潴留");
            cmb_niaozhuliu.Items.Add("1—排尿轻度困难，排尿时间稍延长");
            cmb_niaozhuliu.Items.Add("2—排尿明显困难，尿成滴状");
            cmb_niaozhuliu.Items.Add("3—尿液不能排出，需导尿");

        }
     
        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> mzztong = new Dictionary<string, string>();
            int result = 0, result2 = 0;
            try
            {
                mzztong.Add("szFenTaini", txtFenTaiNi.Controls[0].Text);
                mzztong.Add("szRuiFenTaini", txtRuiFenTaiNi.Controls[0].Text);
                mzztong.Add("lastMedic", timeLastMedic.Value.ToLongTimeString());
                mzztong.Add("ZhenTongFS", cmbZhenTongFS.Text);
                mzztong.Add("DGWZT", txtDGWZT.Controls[0].Text);
                mzztong.Add("DGWZL", txtDGWZL.Controls[0].Text);
                mzztong.Add("JMDGWZ", cmbJMDGWZ.Text);
                mzztong.Add("JiXing", cmbJiXing.Text);
                mzztong.Add("MaFei", txtMaFei.Controls[0].Text);
                mzztong.Add("FenTaiNi", txtFenTaiNiAfter.Controls[0].Text);
                mzztong.Add("ShuFenTaiNi", txtShuFenTaiNi.Controls[0].Text);
                mzztong.Add("QuMaDuo", txtQuMaDuo.Controls[0].Text);
                mzztong.Add("BuBiKaYin",txtBuBiKaYin.Controls[0].Text);
                mzztong.Add("LuoPaiKaYin", txtLuoPaiKaYin.Controls[0].Text);
                mzztong.Add("OtherMedic", txtOtherMedic.Controls[0].Text);
                mzztong.Add("ZongRongLiang", txtZongRongLiang.Controls[0].Text);
                mzztong.Add("dayStart", dayStart.Value.ToLongTimeString());
                mzztong.Add("dayEnd", dayEnd.Value.ToLongTimeString());
                mzztong.Add("FirstJL", txtFirstJL.Controls[0].Text);
                mzztong.Add("ChiXuJL", txtChiXuJL.Controls[0].Text);
                mzztong.Add("PCAJL", txtPCAJL.Controls[0].Text);
                mzztong.Add("SuoDingTime", txtSuoDingTime.Controls[0].Text);
                mzztong.Add("DaoGuanFlag", cmbDaoGuanFlag.Text);
                mzztong.Add("ManYiDu", cmbManYiDu.Text);
                mzztong.Add("mzjldID", MZID);
                mzztong.Add("someHour", dataGridView1.Columns[2].HeaderText);
                mzztong.Add("remarkDay", dataGridView1.Columns[5].HeaderText);  
                DataTable dt2 = apro.GetmzzhentongCount(MZID);
                if (dt2.Rows.Count == 0)
                    result = apro.Insertmazuizhentong(mzztong);
                else
                    result = apro.Updatemazuizhentong(mzztong);
                string dataType="",CHUSHI="",SOMEhour="",firstDAY="",SECONDday="",REMARKday="";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1[0, i].Value != null)
                    {
                        dataType = dataGridView1[0, i].Value.ToString();
                    }
                    else
                        dataType = "";
                    if (dataGridView1[1, i].Value != null)
                    {
                        CHUSHI = dataGridView1[1, i].Value.ToString();
                    }
                    else
                        CHUSHI = "";
                    if (dataGridView1[2, i].Value != null)
                    {
                        SOMEhour = dataGridView1[2, i].Value.ToString();
                    }
                    else
                        SOMEhour = "";
                    if (dataGridView1[3,i].Value != null)
                    {
                        firstDAY = dataGridView1[3, i].Value.ToString();
                    }
                    else
                        firstDAY = "";
                    if (dataGridView1[4,i].Value != null)
                    {
                        SECONDday = dataGridView1[4,i].Value.ToString();
                    }
                    else
                        SECONDday = "";
                    if (dataGridView1[5,i].Value != null)
                    {
                        REMARKday = dataGridView1[5, i].Value.ToString();
                    }
                    else
                        REMARKday = "";


                    DataTable dt3 = apro.GetmzzhentonginfoCount(MZID, dataType);
                    if (dt3.Rows.Count == 0)
                    {
                        apro.InsertmazuizhentongINFO(MZID, dataType, CHUSHI, SOMEhour, firstDAY, SECONDday, REMARKday);
                        result2++;
                    }
                    else
                    {
                        apro.UpdatemazuizhentongInfo(MZID, dataType, CHUSHI, SOMEhour, firstDAY, SECONDday, REMARKday);
                        result2++;
                    }
                }
                if (result > 0 && result2>0)
                    MessageBox.Show("保存成功！");
                else
                    MessageBox.Show("保存失败！");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }

        # region<<打印>>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font title = new Font("黑体", 16);//标题字体
            Font TagLable = new Font("新宋体", 9);//普通字体
            Font FillIn = new Font("宋体", 9);//普通字体
            Font FillIn3 = new Font("黑体", 10);//普通字体
            Font FillIn2 = new Font("宋体", 8);//普通字体
           
            Pen black2 = new Pen(Brushes.Black);
            black2.Width = 2;
            Pen black = new Pen(Brushes.Black);
            black.Width = 1;

            int x = 30, y = 100, x1 = 790;
            e.Graphics.DrawString("上海市嘉定区中医医院麻醉科术后镇痛记录", title, Brushes.Black, x + 150, y - 60);

            e.Graphics.DrawString("开始日期：" + dtStart.Value.ToShortDateString(), FillIn, Brushes.Black, x + 5, y - 25);
            e.Graphics.DrawLine(black, new Point(x + 70, y - 10), new Point(x + 160, y - 10));
            e.Graphics.DrawString("麻醉编号：" + txtMZID.Controls[0].Text, FillIn, Brushes.Black, x + 180, y - 25);
            e.Graphics.DrawLine(black, new Point(x + 240, y - 10), new Point(x + 340, y - 10));
            
            //↓横线1
            e.Graphics.DrawLine(black2, new Point(x, y), new Point(x1, y));
                     //↓行1
            int yy1 = y + 5, underLine_Y = y + 20;//行横坐标和行下划线坐标；
            e.Graphics.DrawString("姓名：" + txtName.Controls[0].Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 40, underLine_Y), new Point(x + 120, underLine_Y));
            e.Graphics.DrawString("性别：" + this.txtSex.Controls[0].Text, FillIn, Brushes.Black, x + 130, yy1);
            e.Graphics.DrawLine(black, new Point(x + 160, underLine_Y), new Point(x + 210, underLine_Y));
            e.Graphics.DrawString("年龄：" + this.txtAge.Controls[0].Text, FillIn, Brushes.Black, x + 220, yy1);
            e.Graphics.DrawLine(black, new Point(x + 255, underLine_Y), new Point(x + 300, underLine_Y));
            e.Graphics.DrawString("体重：" + this.txtAge.Controls[0].Text, FillIn, Brushes.Black, x + 310, yy1);
            e.Graphics.DrawLine(black, new Point(x + 345, underLine_Y), new Point(x + 390, underLine_Y));
            e.Graphics.DrawString("KG  住院号：" + this.txtZhuYuanNo.Controls[0].Text, FillIn, Brushes.Black, x + 392, yy1);
            e.Graphics.DrawLine(black, new Point(x + 465, underLine_Y), new Point(x + 550, underLine_Y));
            e.Graphics.DrawString("床号：" + this.txtBedNo.Controls[0].Text, FillIn, Brushes.Black, x + 560, yy1);
            e.Graphics.DrawLine(black, new Point(x + 595, underLine_Y), new Point(x + 680, underLine_Y));
            
                    //↓行2
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标；
            e.Graphics.DrawString("术后诊断：" + this.txtSZZD.Controls[0].Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 70, underLine_Y), new Point(x + 370, underLine_Y));
            e.Graphics.DrawString("ASA：" + this.txtASA.Controls[0].Text, FillIn, Brushes.Black, x + 380, yy1);
            e.Graphics.DrawLine(black, new Point(x + 410, underLine_Y), new Point(x + 500, underLine_Y));
           
                    //↓行3
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标；
            e.Graphics.DrawString("其他系统疾病：" + this.txtOtherJB.Controls[0].Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 90, underLine_Y), new Point(x + 750, underLine_Y));
           
            //↓行4
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标；
            e.Graphics.DrawString("手术名称：" + this.txtSSMC.Controls[0].Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 70, underLine_Y), new Point(x + 400, underLine_Y));
            e.Graphics.DrawString("麻醉方式：" + this.txtMZFS.Controls[0].Text, FillIn, Brushes.Black, x + 410, yy1);
            e.Graphics.DrawLine(black, new Point(x + 470, underLine_Y), new Point(x + 750, underLine_Y));
            //↓行5
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标；
            e.Graphics.DrawString("术中情况：" + this.txtSZQK.Controls[0].Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 70, underLine_Y), new Point(x + 400, underLine_Y));
            e.Graphics.DrawString("麻醉医生：" + this.txtMZYS.Controls[0].Text, FillIn, Brushes.Black, x + 410, yy1);
            e.Graphics.DrawLine(black, new Point(x + 470, underLine_Y), new Point(x + 750, underLine_Y));

            
             //↓行6
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标；
            e.Graphics.DrawString("术中镇痛药：芬太尼：" + this.txtFenTaiNi.Controls[0].Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 135, underLine_Y), new Point(x + 200, underLine_Y));
            e.Graphics.DrawString("mg  瑞芬太尼：" + this.txtRuiFenTaiNi.Controls[0].Text, FillIn, Brushes.Black, x + 202, yy1);
            e.Graphics.DrawLine(black, new Point(x + 285, underLine_Y), new Point(x + 360, underLine_Y));
            e.Graphics.DrawString("mg  末次给药时间：" + this.timeLastMedic.Text, FillIn, Brushes.Black, x + 530, yy1);
            e.Graphics.DrawLine(black, new Point(x + 640, underLine_Y), new Point(x + 695, underLine_Y));
         
            //↓横线2
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标；
            e.Graphics.DrawLine(black2, new Point(x, yy1), new Point(x1, yy1));//横线2
            //↓横线3
            e.Graphics.DrawLine(black, new Point(x, yy1 + 25), new Point(x1, yy1 + 25));//横线3
            
            yy1 = yy1 + 5; underLine_Y = underLine_Y + 5;//行横坐标和行下划线坐标；
            //↓行1
            e.Graphics.DrawString("镇痛方式：" + this.cmbZhenTongFS.Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 70, underLine_Y), new Point(x + 260, underLine_Y));
            e.Graphics.DrawString("硬膜外导管位置：T" + this.txtDGWZT.Controls[0].Text, FillIn, Brushes.Black, x + 270, yy1);
            e.Graphics.DrawLine(black, new Point(x + 380, underLine_Y), new Point(x + 460, underLine_Y));
            e.Graphics.DrawString("L" + this.txtDGWZL.Controls[0].Text, FillIn, Brushes.Black, x + 462, yy1);
            e.Graphics.DrawLine(black, new Point(x + 470, underLine_Y), new Point(x + 550, underLine_Y));
            e.Graphics.DrawString("; 静脉导管位置：" + this.cmbJMDGWZ.Text, FillIn, Brushes.Black, x + 552, yy1);
            e.Graphics.DrawLine(black, new Point(x + 650, underLine_Y), new Point(x + 735, underLine_Y));

            e.Graphics.DrawLine(black, new Point(x + 60, yy1 + 20), new Point(x + 60, yy1 + 100));//竖线3
            e.Graphics.DrawLine(black, new Point(x + 245, yy1 + 20), new Point(x + 245, yy1 + 100));//竖线4
            e.Graphics.DrawLine(black, new Point(x + 285, yy1 + 20), new Point(x + 285, yy1 + 100));//竖线5
            
            //↓行2
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标   
           
            e.Graphics.DrawString("镇痛泵", FillIn, Brushes.Black, x + 10, yy1+10);
            e.Graphics.DrawString(" 机型：", FillIn, Brushes.Black, x + 10, yy1 + 35);
            
            e.Graphics.DrawString( this.cmbJiXing.Text, FillIn, Brushes.Black, x + 65, yy1 + 35);
            e.Graphics.DrawLine(black, new Point(x + 65, underLine_Y + 20), new Point(x + 230, underLine_Y + 20));
            e.Graphics.DrawString("配方", FillIn, Brushes.Black, x + 250, yy1+35);
            e.Graphics.DrawString("吗啡：" + this.txtMaFei.Controls[0].Text, FillIn, Brushes.Black, x + 300, yy1);
            e.Graphics.DrawLine(black, new Point(x + 340, underLine_Y), new Point(x + 400, underLine_Y));
            e.Graphics.DrawString("mg  芬太尼：" + this.txtFenTaiNiAfter.Controls[0].Text, FillIn, Brushes.Black, x + 400, yy1);
            e.Graphics.DrawLine(black, new Point(x + 480, underLine_Y), new Point(x + 540, underLine_Y));
            e.Graphics.DrawString("mg  舒芬太尼：" + this.txtShuFenTaiNi.Controls[0].Text, FillIn, Brushes.Black, x + 540, yy1);
            e.Graphics.DrawLine(black, new Point(x + 625, underLine_Y), new Point(x + 700, underLine_Y));
            e.Graphics.DrawString("mg", FillIn, Brushes.Black, x + 700, yy1);
            

            //↓行3
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标
            e.Graphics.DrawString("曲马多：" + this.txtQuMaDuo.Controls[0].Text, FillIn, Brushes.Black, x + 300, yy1);
            e.Graphics.DrawLine(black, new Point(x + 350, underLine_Y), new Point(x + 400, underLine_Y));
            e.Graphics.DrawString("mg  布比卡因：" + this.txtBuBiKaYin.Controls[0].Text, FillIn, Brushes.Black, x + 400, yy1);
            e.Graphics.DrawLine(black, new Point(x + 490, underLine_Y), new Point(x + 550, underLine_Y));
            e.Graphics.DrawString("mg  罗派卡因：" + this.txtLuoPaiKaYin.Controls[0].Text, FillIn, Brushes.Black, x + 560, yy1);
            e.Graphics.DrawLine(black, new Point(x + 640, underLine_Y), new Point(x + 700, underLine_Y));
            e.Graphics.DrawString("mg", FillIn, Brushes.Black, x + 700, yy1);
            

            //↓行4
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标
            e.Graphics.DrawString("其他：" + this.txtOtherMedic.Controls[0].Text, FillIn, Brushes.Black, x + 300, yy1);
            e.Graphics.DrawLine(black, new Point(x + 340, underLine_Y), new Point(x + 560, underLine_Y));
            e.Graphics.DrawString("总容量：" + this.txtZongRongLiang.Controls[0].Text, FillIn, Brushes.Black, x + 570, yy1);
            e.Graphics.DrawLine(black, new Point(x + 615, underLine_Y), new Point(x + 700, underLine_Y));
            e.Graphics.DrawString("ml", FillIn, Brushes.Black, x + 700, yy1);
            
            //↓横线4
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标            
            e.Graphics.DrawLine(black, new Point(x, yy1), new Point(x1, yy1));//横线4
            
            //↓行1
            yy1 = yy1 + 5; underLine_Y = underLine_Y + 5;//行横坐标和行下划线坐标
            e.Graphics.DrawString("镇痛泵开机时间：" + this.dayStart.Value.ToShortTimeString(), FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 110, underLine_Y), new Point(x + 350, underLine_Y));
            e.Graphics.DrawString("镇痛泵停止时间：" + this.dayEnd.Value.ToShortTimeString(), FillIn, Brushes.Black, x + 370, yy1);
            e.Graphics.DrawLine(black, new Point(x + 465, underLine_Y), new Point(x + 730, underLine_Y));

            //↓行2
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标 
            e.Graphics.DrawString("首次剂量：" + this.txtFirstJL.Controls[0].Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 70, underLine_Y), new Point(x + 160, underLine_Y));
            e.Graphics.DrawString("ml", FillIn, Brushes.Black, x + 160, yy1);
            e.Graphics.DrawString("持续剂量：" + this.txtChiXuJL.Controls[0].Text, FillIn, Brushes.Black, x + 180, yy1);
            e.Graphics.DrawLine(black, new Point(x + 240, underLine_Y), new Point(x + 340, underLine_Y));
            e.Graphics.DrawString("ml/h", FillIn, Brushes.Black, x + 340, yy1);
            e.Graphics.DrawString("PCA剂量：" + this.txtPCAJL.Controls[0].Text, FillIn, Brushes.Black, x + 380, yy1);
            e.Graphics.DrawLine(black, new Point(x + 430, underLine_Y), new Point(x + 520, underLine_Y));
            e.Graphics.DrawString("ml", FillIn, Brushes.Black, x + 520, yy1);
            e.Graphics.DrawString("锁定时间：" + this.txtSuoDingTime.Controls[0].Text, FillIn, Brushes.Black, x + 540, yy1);
            e.Graphics.DrawLine(black, new Point(x + 600, underLine_Y), new Point(x + 700, underLine_Y));
            e.Graphics.DrawString("min", FillIn, Brushes.Black, x + 700, yy1);
            
            //↓横线5
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标 
            e.Graphics.DrawLine(black2, new Point(x, yy1 ), new Point(x1, yy1 ));//datagridview表格打印打印
           
            e.Graphics.DrawString(dataGridView1.Columns[0].HeaderText.ToString(), FillIn, Brushes.Black, x + 10 , yy1 + 5);
            e.Graphics.DrawString(dataGridView1.Columns[1].HeaderText.ToString(), FillIn, Brushes.Black, x + 190, yy1 + 5);
            e.Graphics.DrawString(dataGridView1.Columns[2].HeaderText.ToString(), FillIn, Brushes.Black, x + 300, yy1 + 5);
            e.Graphics.DrawString(dataGridView1.Columns[3].HeaderText.ToString(), FillIn, Brushes.Black, x + 410, yy1 + 5);
            e.Graphics.DrawString(dataGridView1.Columns[4].HeaderText.ToString(), FillIn, Brushes.Black, x + 520, yy1 + 5);
            e.Graphics.DrawString(dataGridView1.Columns[5].HeaderText.ToString(), FillIn, Brushes.Black, x + 630, yy1 + 5);

            for (int i = 0; i < 12; i++)
            {
                e.Graphics.DrawLine(black, new Point(x, yy1 + 25 * i), new Point(x1, yy1 + 25 * i));//datagridview表格打印打印
                for (int j = 0; j < 6; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value==null)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = "";
                    }
                   
                }
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[0].Value.ToString(), FillIn, Brushes.Black, x + 10, yy1 + 5 + 25 * i + 25);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), FillIn, Brushes.Black, x + 190, yy1 + 5 + 25 * i + 25);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), FillIn, Brushes.Black, x + 300, yy1 + 5 + 25 * i + 25);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), FillIn, Brushes.Black, x + 410, yy1 + 5 + 25 * i + 25);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[4].Value.ToString(), FillIn, Brushes.Black, x + 520, yy1 + 5 + 25 * i + 25);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[5].Value.ToString(), FillIn, Brushes.Black, x + 630, yy1 + 5 + 25 * i + 25);
                
            }
            e.Graphics.DrawLine(black, new Point(x, yy1 + 25 * 12), new Point(x1, yy1 + 25 * 12));//datagridview表格打印打印
                
            for (int i = 0; i < 5; i++)
            {
                e.Graphics.DrawLine(black, new Point(x + 180 + 110 * i, yy1), new Point(x + 180 + 110 * i, yy1 + 25 * 13));//datagridview表格打印打印
     
            }
            
            //↓横线6
            yy1 = yy1 + 25*13; underLine_Y = underLine_Y + 25*13;//行横坐标和行下划线坐标            
            e.Graphics.DrawLine(black2, new Point(x, yy1), new Point(x1, yy1));
            yy1 = yy1 + 5 ; underLine_Y = underLine_Y + 5;//行横坐标和行下划线坐标
            e.Graphics.DrawString("阵痛术毕导管：" + this.cmbDaoGuanFlag.Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 90, underLine_Y), new Point(x + 150, underLine_Y));
            e.Graphics.DrawString("总体满意度：" + this.cmbManYiDu.Text, FillIn, Brushes.Black, x + 160, yy1);
            e.Graphics.DrawLine(black, new Point(x + 230, underLine_Y), new Point(x + 490, underLine_Y));

            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标
            e.Graphics.DrawString("备注：" + this.rtbRemark.Text, FillIn, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawLine(black, new Point(x + 50, underLine_Y), new Point(x + 750, underLine_Y));
           
           
            //↓横线7
            yy1 = yy1 + 50; underLine_Y = underLine_Y + 50;//行横坐标和行下划线坐标            
            e.Graphics.DrawLine(black2, new Point(x, yy1), new Point(x1, yy1));
            
            e.Graphics.DrawLine(black2, new Point(x + 1, y), new Point(x + 1, yy1));//竖线1
            e.Graphics.DrawLine(black2, new Point(x1 - 1, y), new Point(x1 - 1, yy1));//竖线2

            yy1 = yy1 + 20; underLine_Y = underLine_Y + 20;//行横坐标和行下划线坐标 
            e.Graphics.DrawString("VAS 评分", FillIn3, Brushes.Black, x + 10, yy1);
            e.Graphics.DrawString("运动阻滞 评级", FillIn3, Brushes.Black, x + 250, yy1);
            e.Graphics.DrawString("呕吐 评分", FillIn3, Brushes.Black, x + 250, yy1 + 90);
            e.Graphics.DrawString("恶心 评分", FillIn3, Brushes.Black, x + 250, yy1 + 180);
            e.Graphics.DrawString("Ramesay 评分", FillIn3, Brushes.Black, x + 520, yy1);
            e.Graphics.DrawString("尿潴留", FillIn3, Brushes.Black, x + 520, yy1 + 160);
            
       
            yy1 = yy1 + 25; underLine_Y = underLine_Y + 25;//行横坐标和行下划线坐标    
            for (int i = 0; i < cmb_VAS.Items.Count; i++)
            {
                e.Graphics.DrawString(this.cmb_VAS.Items[i].ToString(), FillIn2, Brushes.Black, x + 10, yy1 + 15 * i);
               
            }
            for (int i = 0; i < this.cmb_yundong.Items.Count; i++)
            {
                e.Graphics.DrawString(this.cmb_yundong.Items[i].ToString(), FillIn2, Brushes.Black, x + 250, yy1 + 15 * i);

            }
            for (int i = 0; i < this.cmb_outu.Items.Count; i++)
            {
                e.Graphics.DrawString(this.cmb_outu.Items[i].ToString(), FillIn2, Brushes.Black, x + 250, yy1 + 90 + 15 * i);

            }
            for (int i = 0; i < this.cmb_exin.Items.Count; i++)
            {
                e.Graphics.DrawString(this.cmb_exin.Items[i].ToString(), FillIn2, Brushes.Black, x + 250, yy1 + 180 + 15 * i);

            }
            for (int i = 0; i < this.cmb_niaozhuliu.Items.Count; i++)
            {
                e.Graphics.DrawString(this.cmb_niaozhuliu.Items[i].ToString(), FillIn2, Brushes.Black, x + 520, yy1 + 160 + 15 * i);

            }

            for (int i = 0; i < this.cmb_zhenjing.Items.Count; i++)
            {
                e.Graphics.DrawString(this.cmb_zhenjing.Items[i].ToString(), FillIn2, Brushes.Black, x + 520, yy1 + 15 * i);

            }
           
          
           
        }
        # endregion

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

        private void MZH_ZhengTong_Load(object sender, EventArgs e)
        {
            
            // 绑定性别下拉列表框
            Bind_VAS();

            // 设置下拉列表框不可见
            cmb_VAS.Visible = false;
            cmb_exin.Visible = false;
            cmb_outu.Visible = false;
            cmb_zhenjing.Visible = false;
            cmb_yundong.Visible = false;
            cmb_niaozhuliu.Visible = false;

            // 添加下拉列表框事件
            cmb_VAS.SelectedIndexChanged += new EventHandler(cmb_VAS_SelectedIndexChanged);
            cmb_exin.SelectedIndexChanged += new EventHandler(cmb_exin_SelectedIndexChanged);
            cmb_outu.SelectedIndexChanged += new EventHandler(cmb_outu_SelectedIndexChanged);
            cmb_zhenjing.SelectedIndexChanged += new EventHandler(cmb_zhenjing_SelectedIndexChanged);
            cmb_yundong.SelectedIndexChanged += new EventHandler(cmb_yundong_SelectedIndexChanged);
            cmb_niaozhuliu.SelectedIndexChanged += new EventHandler(cmb_niaozhuliu_SelectedIndexChanged);

            // 将下拉列表框加入到DataGridView控件中
            Chushihua_dataGridView1();
            this.dataGridView1.Controls.Add(cmb_VAS);
            this.dataGridView1.Controls.Add(cmb_exin);
            this.dataGridView1.Controls.Add(cmb_outu);
            this.dataGridView1.Controls.Add(cmb_zhenjing);
            this.dataGridView1.Controls.Add(cmb_yundong);
            this.dataGridView1.Controls.Add(cmb_niaozhuliu);

            
            SetValues();
            txtMZID.Controls[0].Text = MZID;
            BindPatInfo();
            BindmzjldInfo();
           
            
        }
        private void BindmzjldInfo()
        {
            DataTable dt = new DataTable();
            dt = apro.GetMZJLD_Info(MZID);
            if (dt.Rows.Count>0)
            {
                txtSZZD.Controls[0].Text = dt.Rows[0]["szzd"].ToString();
                txtMZYS.Controls[0].Text = dt.Rows[0]["mzys"].ToString();
                txtSZQK.Controls[0].Text = dt.Rows[0]["mzfa"].ToString();
                txtMZFS.Controls[0].Text = dt.Rows[0]["mzfa"].ToString();
           
            } 
 
        }
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = apro.GetALLPAIBAN(PATID);
            txtName.Controls[0].Text = dt.Rows[0]["Patname"].ToString();
            txtWeight.Controls[0].Text = dt.Rows[0]["PatWeight"].ToString();
            txtASA.Controls[0].Text = dt.Rows[0]["asa"].ToString();
           
            txtAge.Controls[0].Text = dt.Rows[0]["Patage"].ToString();
            txtSex.Controls[0].Text = dt.Rows[0]["Patsex"].ToString();
            txtBedNo.Controls[0].Text = dt.Rows[0]["Patbedno"].ToString();
            txtZhuYuanNo.Controls[0].Text = dt.Rows[0]["PatZhuYuanID"].ToString();
            txtSSMC.Controls[0].Text = dt.Rows[0]["Oname"].ToString();
            txtMZFS.Controls[0].Text = dt.Rows[0]["Amethod"].ToString();

        }
        private void Chushihua_dataGridView1()//初始化dataGridView1数据
        {
            DataTable dt1 = apro.Getmzzhentonginfo(MZID);
             
             if (dt1.Rows.Count == 0)
             {                                  
                 dataGridView1.Rows.Add(11);
                 dataGridView1.Rows[0].Cells[0].Value = "SpO2 （次/分）";
                 dataGridView1.Rows[1].Cells[0].Value = "心率 （次/分）";
                 dataGridView1.Rows[2].Cells[0].Value = "呼吸次数 （次/分）";
                 dataGridView1.Rows[3].Cells[0].Value = "VAS 评分";
                 dataGridView1.Rows[4].Cells[0].Value = "镇静 评级";
                 dataGridView1.Rows[5].Cells[0].Value = "运动阻滞 评级";
                 dataGridView1.Rows[6].Cells[0].Value = "恶心 评分";
                 dataGridView1.Rows[7].Cells[0].Value = "呕吐 评分";
                 dataGridView1.Rows[8].Cells[0].Value = "尿潴留(保留导管)";
                 dataGridView1.Rows[9].Cells[0].Value = "其他不良反应";
                 dataGridView1.Rows[10].Cells[0].Value = "用量(持续+PCA)（ml）";
                 dataGridView1.Rows[11].Cells[0].Value = "随访签名";
             }
             else
             {
                 DataTable dt3 = apro.Getmzzhentonginfo(MZID);
                 dataGridView1.DataSource = dt3.DefaultView;
               

             }
             if (dataGridView1.Rows.Count>12)
             {
                  dataGridView1.Rows[12].ReadOnly = true;
             }
           
            dataGridView1.Rows[11].ReadOnly = true;
            dataGridView1.Columns[0].ReadOnly = true;
            
            
         
        }
        private void SetValues()//控件赋值
        {
            DataTable dt2 = apro.GetmzzhentongCount(MZID);
        
            if (dt2.Rows.Count != 0)
            {
                txtFenTaiNi.Controls[0].Text=dt2.Rows[0]["szFenTaini"].ToString();
                txtRuiFenTaiNi.Controls[0].Text=dt2.Rows[0]["szRuiFenTaini"].ToString();
                timeLastMedic.Value =Convert.ToDateTime(dt2.Rows[0]["lastMedic"]);
                cmbZhenTongFS.Text = dt2.Rows[0]["ZhenTongFS"].ToString();
                txtDGWZT.Controls[0].Text = dt2.Rows[0]["DGWZT"].ToString();
                txtDGWZL.Controls[0].Text = dt2.Rows[0]["DGWZL"].ToString();
                cmbJMDGWZ.Text = dt2.Rows[0]["JMDGWZ"].ToString();
                cmbJMDGWZ.Text = dt2.Rows[0]["JiXing"].ToString();
                txtMaFei.Controls[0].Text = dt2.Rows[0]["MaFei"].ToString();
                txtFenTaiNiAfter.Controls[0].Text = dt2.Rows[0]["FenTaiNi"].ToString();
                txtShuFenTaiNi.Controls[0].Text = dt2.Rows[0]["ShuFenTaiNi"].ToString();
                txtQuMaDuo.Controls[0].Text = dt2.Rows[0]["QuMaDuo"].ToString();
                txtBuBiKaYin.Controls[0].Text = dt2.Rows[0]["BuBiKaYin"].ToString();
                txtBuBiKaYin.Controls[0].Text = dt2.Rows[0]["LuoPaiKaYin"].ToString();
                txtOtherMedic.Controls[0].Text = dt2.Rows[0]["OtherMedic"].ToString();
                txtZongRongLiang.Controls[0].Text = dt2.Rows[0]["ZongRongLiang"].ToString();
                dayStart.Value = Convert.ToDateTime(dt2.Rows[0]["dayStart"]);
                dayEnd.Value = Convert.ToDateTime(dt2.Rows[0]["dayEnd"]);
                txtFirstJL.Controls[0].Text = dt2.Rows[0]["FirstJL"].ToString();
                txtChiXuJL.Controls[0].Text = dt2.Rows[0]["ChiXuJL"].ToString();
                txtSuoDingTime.Controls[0].Text = dt2.Rows[0]["SuoDingTime"].ToString();
                txtPCAJL.Controls[0].Text = dt2.Rows[0]["PCAJL"].ToString();
                cmbDaoGuanFlag.Text = dt2.Rows[0]["DaoGuanFlag"].ToString();
                cmbManYiDu.Text = dt2.Rows[0]["ManYiDu"].ToString();
                dataGridView1.Columns[2].HeaderText = dt2.Rows[0]["someHour"].ToString();
                dataGridView1.Columns[5].HeaderText = dt2.Rows[0]["remarkDay"].ToString();
                if (dt2.Rows[0]["UpdateTime"].ToString()!="")
                {
                    this.dtStart.Value = Convert.ToDateTime(dt2.Rows[0]["UpdateTime"]);
                }
                else
                {
                    this.dtStart.Value = Convert.ToDateTime(dt2.Rows[0]["AddTime"]);
                
                }

            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Columns["Column3"].HeaderText = tbSetHour.Text + "小时";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns["Column6"].HeaderText = tbYuLiuLie.Text;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex==11)
            {
                if (e.ColumnIndex >= 1 && e.ColumnIndex <= 6)
                {
                    //selaa selaa = new selaa(dataGridView1, e.RowIndex, e.ColumnIndex);
                    //selaa.ShowDialog();
                }
            }
        
           
        }
        
        int drow, dcol;//dataGridView1的行号，列号
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            drow = e.RowIndex;
            dcol = e.ColumnIndex;
        }
      

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {

            try
            {
                if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() == "VAS 评分" && dataGridView1.CurrentCell.ColumnIndex > 0)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);

                    cmb_VAS.Left = rect.Left-50;
                    cmb_VAS.Top = rect.Top;
                    cmb_VAS.Width = 200;
                    cmb_VAS.Height = rect.Height;
                    cmb_VAS.Visible = true;
                }
                else
                {
                    cmb_VAS.Visible = false;
                }
                if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() == "恶心 评分" && dataGridView1.CurrentCell.ColumnIndex > 0)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cmb_exin.Left = rect.Left - 100;
                    cmb_exin.Top = rect.Top;
                    cmb_exin.Width = 250;
                    cmb_exin.Height = rect.Height;
                    cmb_exin.Visible = true;
                }
                else
                {
                    cmb_exin.Visible = false;
                }
                if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() == "呕吐 评分" && dataGridView1.CurrentCell.ColumnIndex > 0)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cmb_outu.Left = rect.Left;
                    cmb_outu.Top = rect.Top;
                    cmb_outu.Width = 150;
                    cmb_outu.Height = rect.Height;
                    cmb_outu.Visible = true;
                }
                else
                {
                    cmb_outu.Visible = false;
                }
                if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() == "运动阻滞 评级" && dataGridView1.CurrentCell.ColumnIndex > 0)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cmb_yundong.Left = rect.Left;
                    cmb_yundong.Top = rect.Top;
                    cmb_yundong.Width = 150;
                    cmb_yundong.Height = rect.Height;
                    cmb_yundong.Visible = true;
                }
                else
                {
                    cmb_yundong.Visible = false;
                }
                if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() == "镇静 评级" && dataGridView1.CurrentCell.ColumnIndex > 0)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cmb_zhenjing.Left = rect.Left;
                    cmb_zhenjing.Top = rect.Top;
                    cmb_zhenjing.Width = 150;
                    cmb_zhenjing.Height = rect.Height;
                    cmb_zhenjing.Visible = true;
                }
                else
                {
                    cmb_zhenjing.Visible = false;
                }
                if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() == "尿潴留(保留导管)" && dataGridView1.CurrentCell.ColumnIndex > 0)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);

                    cmb_niaozhuliu.Left = rect.Left;
                    cmb_niaozhuliu.Top = rect.Top;
                    cmb_niaozhuliu.Width = 150;
                    cmb_niaozhuliu.Height = rect.Height;
                    cmb_niaozhuliu.Visible = true;
                }
                else
                {
                    cmb_niaozhuliu.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void cmb_VAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = cmb_VAS.SelectedIndex.ToString();
         
        }
        private void cmb_exin_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = cmb_exin.SelectedIndex.ToString();

        }
        private void cmb_niaozhuliu_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = cmb_niaozhuliu.SelectedIndex.ToString();

        }
        private void cmb_outu_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = cmb_outu.SelectedIndex.ToString();

        }
        private void cmb_yundong_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = cmb_yundong.SelectedIndex.ToString();

        }
        private void cmb_zhenjing_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = (cmb_zhenjing.SelectedIndex+1).ToString();

        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }

        private void picBox_Click(object sender, EventArgs e)
        {

        }
       
    }
}