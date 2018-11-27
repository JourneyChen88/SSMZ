using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_DAL;

namespace main
{
    public partial class MZZJ_ZXYJH : Form
    {
        string mzjldID, patID;
        AdimsController bll = new AdimsController();
        AdimsProvider apro = new AdimsProvider();
        public MZZJ_ZXYJH( string mzid,string patid)
        {
            mzjldID = patid; patID =  mzid;
            
            InitializeComponent();
        }
        private void MZZJ_ZXYJH_Load(object sender, EventArgs e)
        {
            txtpatID.Controls[0].Text = patID;
            txtMzjldid.Controls[0].Text = mzjldID;
            DataTable dt = new DataTable();
            dt = apro.GetALLPAIBAN(patID);            
            txtName.Controls[0].Text = dt.Rows[0]["Patname"].ToString();
            txtAge.Controls[0].Text = dt.Rows[0]["Patage"].ToString();
            txtSex.Controls[0].Text = dt.Rows[0]["Patsex"].ToString();
            SetValues();
            DataFromMZJLD();
           
        }
        private void DataFromMZJLD()
        {
            //DataTable dt = apro.GetMZJLD_Info(mzjldID);
            //if (dt.Rows.Count == 0)
            //{
            //    return;
            //}
            //else
            //{
            //    txtmzpmssStartUp.Controls[0].Text = dt.Rows[0]["mazuipingmianUP"].ToString();
            //    txtmzpmssStartDown.Controls[0].Text = dt.Rows[0]["mazuipingmianDOWN"].ToString();
            //}
        }

        private void SetValues()
        {
            DataTable dt = new DataTable();
            dt = apro.GetmazuizongjieCount(mzjldID);
            if (dt.Rows.Count==0)
	        {
		         return;
	        }
            else
            {
 
               rtbshoushuHistory.Text=dt.Rows[0]["shoushuHistory"].ToString();
               cmbHeartClass.Text=dt.Rows[0]["HeartClass"].ToString();
               cmbgxyHistory.Text=dt.Rows[0]["gxyHistory"].ToString();
               cmbtnbHistory.Text=dt.Rows[0]["tnbHistory"].ToString();
               cmbhxFunction.Text=dt.Rows[0]["hxFunction"].ToString();
               txtTSCZ.Controls[0].Text = dt.Rows[0]["gsFunction"].ToString();
               cmbydcgSpeed.Text=dt.Rows[0]["ydcgSpeed"].ToString();
               cmbydcgStatus.Text=dt.Rows[0]["ydcgStatus"].ToString();
               cmbydcgPosition.Text=dt.Rows[0]["ydcgPosition"].ToString();
               cmbydcgSee.Text=dt.Rows[0]["ydcgSee"].ToString();
               cmbydcgSelction.Text=dt.Rows[0]["ydcgSelction"].ToString();
               txtcghTingzhen.Controls[0].Text=dt.Rows[0]["cghTingzhen"].ToString();
               rtbcgRemark.Text=dt.Rows[0]["cgRemark"].ToString();
               cmbjzccPostural.Text=dt.Rows[0]["jzccPostural"].ToString();
               txtymwccUp.Controls[0].Text=dt.Rows[0]["ymwccUp"].ToString();
               cmbymwccUpSelect.Text=dt.Rows[0]["ymwccUpSelect"].ToString();
               txtymwccUpLong.Controls[0].Text=dt.Rows[0]["ymwccUpLong"].ToString();
               txtymwccDown.Controls[0].Text=dt.Rows[0]["ymwccDown"].ToString();
               cmbymwccDownSelect.Text=dt.Rows[0]["ymwccDownSelect"].ToString();
               txtymwccDownLong.Controls[0].Text = dt.Rows[0]["ymwccDownLong"].ToString();
               txtzwmxqDrug.Controls[0].Text = dt.Rows[0]["zwmxqDrug"].ToString();
               txtzwmxqDosage.Controls[0].Text = dt.Rows[0]["zwmxqDosage"].ToString();
               cmbzwmxqDiluteBy.Text=dt.Rows[0]["zwmxqDiluteBy"].ToString();
               txtzwmxqDiluteDosage.Controls[0].Text=dt.Rows[0]["zwmxqDiluteDosage"].ToString();
               txtzwmxqDrugSpeed.Controls[0].Text=dt.Rows[0]["zwmxqDrugSpeed"].ToString();
               txtzwmxqDrugTime.Controls[0].Text=dt.Rows[0]["zwmxqDrugTime"].ToString();
               txtdiguan.Controls[0].Text=dt.Rows[0]["diguan"].ToString();  
               txtmzpmssStartUp.Controls[0].Text=dt.Rows[0]["mzpmssStartUp"].ToString();  
               txtmzpmssStartDown.Controls[0].Text=dt.Rows[0]["mzpmssStartDown"].ToString();  
               txtsjzzRemark.Controls[0].Text=dt.Rows[0]["sjzzRemark"].ToString();  
               cmbjcsjzzSelect1.Text=dt.Rows[0]["jcsjzzSelect1"].ToString();  
               cmbjcsjzzSelect2.Text=dt.Rows[0]["jcsjzzSelect2"].ToString();  
               cmbjcsjzzSelectqc.Text=dt.Rows[0]["jcsjzzSelectqc"].ToString();  
               cmbjcsjzzSelectsc.Text=dt.Rows[0]["jcsjzzSelectsc"].ToString();  
               cmbbcsjzzSelect.Text=dt.Rows[0]["bcsjzzSelect"].ToString();  
               cmbOthersjzzSelect.Text=dt.Rows[0]["OthersjzzSelect"].ToString();  
               txtothersszz.Controls[0].Text=dt.Rows[0]["othersszz"].ToString();  
               cmbccqk.Text=dt.Rows[0]["ccqk"].ToString();  
               cmbccqkyc.Text=dt.Rows[0]["ccqkyc"].ToString();  
               txtyaowu.Controls[0].Text=dt.Rows[0]["yaowu"].ToString();  
               cmbzzEffect.Text=dt.Rows[0]["zzEffect"].ToString();  
               txtRemark.Controls[0].Text=dt.Rows[0]["Remark"].ToString();  
               txtjmHocus.Controls[0].Text=dt.Rows[0]["jmHocus"].ToString();  
               txtMAC.Controls[0].Text=dt.Rows[0]["MAC"].ToString();  
               txtsjmcc.Controls[0].Text=dt.Rows[0]["sjmcc"].ToString();  
               txtccdm.Controls[0].Text=dt.Rows[0]["ccdm"].ToString();  
               txtccdmOther.Controls[0].Text=dt.Rows[0]["ccdmOther"].ToString();  
               txtccdmRemark.Controls[0].Text=dt.Rows[0]["ccdmRemark"].ToString();
               txtmafei.Controls[0].Text = dt.Rows[0]["mafei"].ToString(); 
               txtfentaini.Controls[0].Text=dt.Rows[0]["fentaini"].ToString(); 
               txtqumaduo.Controls[0].Text=dt.Rows[0]["qumaduo"].ToString(); 
               txtbubikayin.Controls[0].Text=dt.Rows[0]["bubikayin"].ToString(); 
               txtnailepin.Controls[0].Text=dt.Rows[0]["nailepin"].ToString(); 
               txtdingkayin.Controls[0].Text=dt.Rows[0]["dingkayin"].ToString(); 
               txtfupailiduo.Controls[0].Text=dt.Rows[0]["fupailiduo"].ToString(); 
               txtzongliang.Controls[0].Text=dt.Rows[0]["zongliang"].ToString(); 
               txtchixujiliang.Controls[0].Text=dt.Rows[0]["chixujiliang"].ToString(); 
               txtPAC.Controls[0].Text=dt.Rows[0]["PCA"].ToString(); 
               txtPACtime.Controls[0].Text=dt.Rows[0]["PCAtime"].ToString(); 
               rtbmzSpecialSituation.Text=dt.Rows[0]["mzSpecialSituation"].ToString(); 
               txtmzys.Controls[0].Text=dt.Rows[0]["mzys"].ToString();
                cmbShunLi.Text=dt.Rows[0]["shunli"].ToString();
                cmbYigan.Text = dt.Rows[0]["yigan"].ToString();
            }

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen green = new Pen(Brushes.Black);
            green.Width = 3;
            Pen green2 = new Pen(Brushes.Black);
            green2.Width = 2;
            e.Graphics.DrawLine(green, new Point(55, 50), new Point(55, 1030));
            e.Graphics.DrawLine(green, new Point(55, 50), new Point(935, 50));
            e.Graphics.DrawLine(green, new Point(935, 50), new Point(935, 1030));
            e.Graphics.DrawLine(green, new Point(55, 1030), new Point(935, 1030));
            e.Graphics.DrawLine(green2, new Point(55, 163), new Point(935, 163));
            e.Graphics.DrawLine(green2, new Point(55, 298), new Point(935, 298));
            e.Graphics.DrawLine(green2, new Point(55, 534), new Point(935, 534));
            e.Graphics.DrawLine(green2, new Point(55, 708), new Point(935, 708));
            e.Graphics.DrawLine(green2, new Point(55, 905), new Point(935, 905));
            e.Graphics.DrawLine(green2, new Point(95, 50), new Point(95, 1030));
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   

        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> mzzj = new Dictionary<string, string>();
            int result = 0;
            try
            {
                mzzj.Add("shoushuHistory", rtbshoushuHistory.Text);
                mzzj.Add("HeartClass", cmbHeartClass.Text);
                mzzj.Add("gxyHistory", cmbgxyHistory.Text);
                mzzj.Add("tnbHistory", cmbtnbHistory.Text);
                mzzj.Add("hxFunction", cmbhxFunction.Text);
                mzzj.Add("gsFunction", txtTSCZ.Controls[0].Text);
                mzzj.Add("ydcgSpeed", cmbydcgSpeed.Text);
                mzzj.Add("ydcgStatus", cmbydcgStatus.Text);
                mzzj.Add("ydcgPosition", cmbydcgPosition.Text);
                mzzj.Add("ydcgSee", cmbydcgSee.Text);
                mzzj.Add("ydcgSelction", cmbydcgSelction.Text);
                mzzj.Add("cghTingzhen", txtcghTingzhen.Controls[0].Text);
                mzzj.Add("cgRemark", rtbcgRemark.Text);
                mzzj.Add("jzccPostural", cmbjzccPostural.Text);          
                mzzj.Add("ymwccUp", txtymwccUp.Controls[0].Text);
                mzzj.Add("ymwccUpSelect", cmbymwccUpSelect.Text);
                mzzj.Add("ymwccUpLong", txtymwccUpLong.Controls[0].Text);
                mzzj.Add("ymwccDown", txtymwccDown.Controls[0].Text);
                mzzj.Add("ymwccDownSelect", cmbymwccDownSelect.Text);
                mzzj.Add("ymwccDownLong", txtymwccDownLong.Controls[0].Text);
                mzzj.Add("zwmxqDrug", txtzwmxqDrug.Controls[0].Text);
                mzzj.Add("zwmxqDosage", txtzwmxqDosage.Controls[0].Text);
                mzzj.Add("zwmxqDiluteBy", cmbzwmxqDiluteBy.Text);
                mzzj.Add("zwmxqDiluteDosage", txtzwmxqDiluteDosage.Controls[0].Text);
                mzzj.Add("zwmxqDrugSpeed", txtzwmxqDrugSpeed.Controls[0].Text);
                mzzj.Add("zwmxqDrugTime", txtzwmxqDrugTime.Controls[0].Text);
                mzzj.Add("diguan", txtdiguan.Controls[0].Text);
                mzzj.Add("mzpmssStartUp", txtmzpmssStartUp.Controls[0].Text);
                mzzj.Add("mzpmssStartDown", txtmzpmssStartDown.Controls[0].Text);
                mzzj.Add("sjzzRemark", txtsjzzRemark.Controls[0].Text);
                mzzj.Add("jcsjzzSelect1", cmbjcsjzzSelect1.Text);
                mzzj.Add("jcsjzzSelect2", cmbjcsjzzSelect2.Text);
                mzzj.Add("jcsjzzSelectqc", cmbjcsjzzSelectqc.Text);
                mzzj.Add("jcsjzzSelectsc", cmbjcsjzzSelectsc.Text);
                mzzj.Add("bcsjzzSelect", cmbbcsjzzSelect.Text);
                mzzj.Add("OthersjzzSelect", cmbOthersjzzSelect.Text);
                mzzj.Add("othersszz", txtothersszz.Controls[0].Text);
                mzzj.Add("ccqk", cmbccqk.Text);
                mzzj.Add("ccqkyc", cmbccqkyc.Text);
                mzzj.Add("yaowu", txtyaowu.Controls[0].Text);
                mzzj.Add("zzEffect", cmbzzEffect.Text);
                mzzj.Add("Remark", txtRemark.Controls[0].Text);
                mzzj.Add("jmHocus", txtjmHocus.Controls[0].Text);
                mzzj.Add("MAC", txtMAC.Controls[0].Text);
                mzzj.Add("sjmcc", txtsjmcc.Controls[0].Text);
                mzzj.Add("ccdm", txtccdm.Controls[0].Text);
                mzzj.Add("ccdmOther", txtccdmOther.Controls[0].Text);
                mzzj.Add("ccdmRemark", txtccdmRemark.Controls[0].Text);
                mzzj.Add("mafei", txtmafei.Controls[0].Text);
                mzzj.Add("fentaini", txtfentaini.Controls[0].Text);
                mzzj.Add("qumaduo", txtqumaduo.Controls[0].Text);
                mzzj.Add("bubikayin", txtbubikayin.Controls[0].Text);
                mzzj.Add("nailepin", txtnailepin.Controls[0].Text);
                mzzj.Add("dingkayin", txtdingkayin.Controls[0].Text);
                mzzj.Add("fupailiduo", txtfupailiduo.Controls[0].Text);
                mzzj.Add("zongliang", txtzongliang.Controls[0].Text);
                mzzj.Add("chixujiliang", txtchixujiliang.Controls[0].Text);
                mzzj.Add("PAC", txtPAC.Controls[0].Text);
                mzzj.Add("PACtime", txtPACtime.Controls[0].Text);
                mzzj.Add("mzSpecialSituation", rtbmzSpecialSituation.Text);
                mzzj.Add("mzys", txtmzys.Controls[0].Text);
                mzzj.Add("mzjldID", mzjldID);
                mzzj.Add("patID", patID);
                mzzj.Add("shunli", cmbShunLi.Text);
                mzzj.Add("yigan", cmbYigan.Text);
               
                DataTable dt2 = apro.GetmazuizongjieCount(mzjldID);
                if (dt2.Rows.Count==0)
                    result = apro.Insertmazuizongjie(mzzj);
                else
                    result = apro.Updatemazuizongjie(mzzj);
                if (result > 0)
                    MessageBox.Show("保存成功！");
                else
                    MessageBox.Show("保存失败！");
            }
            catch(Exception ee)
            { MessageBox.Show(ee.Message); }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            
            
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            Font TagLable = new Font("新宋体", 9);//普通字体
            Font FillIn = new Font("宋体", 9);//普通字体
            Font TagLable1 = new Font("新宋体", 10);//普通字体
            Pen green = new Pen(Brushes.Blue);
            green.Width = 1;
            Pen green2 = new Pen(Brushes.Blue);
            green2.Width = 2;
            Pen black = new Pen(Brushes.Black);

            //↓打印病人基本信息
            int x3 = 50, y3 = 55;
            e.Graphics.DrawString("病人姓名 " + txtName.Controls[0].Text, TagLable1, Brushes.Black, x3, y3);
            e.Graphics.DrawLine(black, new Point(x3 + 55, y3 + 17), new Point(x3 + 120, y3 + 17));
            e.Graphics.DrawString("性别 " + txtSex.Controls[0].Text, TagLable1, Brushes.Black, x3 + 130, y3);
            e.Graphics.DrawLine(black, new Point(x3 + 160, y3 + 17), new Point(x3 + 210, y3 + 17));
            e.Graphics.DrawString("年龄 " + txtAge.Controls[0].Text, TagLable1, Brushes.Black, x3 + 220, y3);
            e.Graphics.DrawLine(black, new Point(x3 + 250, y3 + 17), new Point(x3 + 300, y3 + 17));
            e.Graphics.DrawString("病案号 " + txtpatID.Controls[0].Text, TagLable1, Brushes.Black, x3 + 310, y3);
            e.Graphics.DrawLine(black, new Point(x3 + 355, y3 + 17), new Point(x3 + 410, y3 + 17));
            e.Graphics.DrawString("麻醉编号 " + txtMzjldid.Controls[0].Text, TagLable1, Brushes.Black, x3 + 420, y3);
            e.Graphics.DrawLine(black, new Point(x3 + 480, y3 + 17), new Point(x3 + 540, y3 + 17));
            e.Graphics.DrawString("日期 " + dateTimePicker1.Text, TagLable1, Brushes.Black, x3 + 540, y3);
            e.Graphics.DrawLine(black, new Point(x3 + 580, y3 + 17), new Point(x3 + 670, y3 + 17));

            //↓画边框
            int x = 20, y = 80;
            
            

          
            e.Graphics.DrawString("麻醉总结", new Font("新宋体", 12, FontStyle.Bold), Brushes.Black, new Point(350, 20));
            
            
            //↓打印特殊情况区域
            int x1 = 50, y1 = 80;
            e.Graphics.DrawLine(green2, new Point(x, y), new Point(790, y));//横线一
            e.Graphics.DrawString("\n术\n前\n特\n殊\n情\n况", TagLable, Brushes.Black, 25, 83);
            e.Graphics.DrawString(lb1.Text, TagLable, Brushes.Black, x1 + 5, y1 + 5);
            for (int i = 1; i <=4; i++)
            {
                e.Graphics.DrawLine(black, new Point(x + 30, y + 23 * i), new Point(790, y + 23 * i));//病史栏
            
            }
            if (rtbshoushuHistory.Text != "")
            {
                if (rtbshoushuHistory.Text.Length>55&&110>=rtbshoushuHistory.Text.Length)
                {
                    e.Graphics.DrawString(rtbshoushuHistory.Text.Substring(0,55), FillIn, Brushes.Black, x1 + 5, y1 + 25);
                    e.Graphics.DrawString(rtbshoushuHistory.Text.Substring(56), FillIn, Brushes.Black, x1 + 5, y1 + 50);
            
                }
                if (rtbshoushuHistory.Text.Length > 110 && 165 >= rtbshoushuHistory.Text.Length)
                {
                    e.Graphics.DrawString(rtbshoushuHistory.Text.Substring(0, 55), FillIn, Brushes.Black, x1 + 5, y1 + 25);
                    e.Graphics.DrawString(rtbshoushuHistory.Text.Substring(56, 55), FillIn, Brushes.Black, x1 + 5, y1 + 50);
                    e.Graphics.DrawString(rtbshoushuHistory.Text.Substring(111), FillIn, Brushes.Black, x1 + 5, y1 + 75);

                }
                else
                {
                    e.Graphics.DrawString(rtbshoushuHistory.Text, FillIn, Brushes.Black, x1 + 5, y1 + 25);
                }
              
            }
            
            e.Graphics.DrawString("麻醉分级:_______________    特殊手术麻醉及操作:____________________________________________________  ", TagLable, Brushes.Black, x1 + 5, y1 + 100);
            e.Graphics.DrawString("           " + cmbgxyHistory.Text, FillIn, Brushes.Black, x1 + 5, y1 + 98);
            e.Graphics.DrawString("                                                 " + txtTSCZ.Controls[0].Text, FillIn, Brushes.Black, x1 + 5, y1 + 98);
            //e.Graphics.DrawString("                                             " + cmbtnbHistory.Text, FillIn, Brushes.Black, x1 + 5, y1 + 98);
            //e.Graphics.DrawString("                                                                    " + cmbhxFunction.Text, FillIn, Brushes.Black, x1 + 5, y1 + 98);
            //e.Graphics.DrawString("                                                                                           " + cmbgsFunction.Text, FillIn, Brushes.Black, x1 + 5, y1 + 98);


            //↓打印气管插管全身麻醉
            y1 = y1 + 125;
            e.Graphics.DrawLine(green, new Point(x, y1), new Point(790, y1 ));//横线二
            

            e.Graphics.DrawString("气\n管\n插\n管\n全\n身\n麻\n醉", TagLable, Brushes.Black, 25, y1+3);
            e.Graphics.DrawString("诱导插管:   " , TagLable, Brushes.Black, x1 + 3, y1 + 5);
            e.Graphics.DrawString(cmbydcgSpeed.Text + "，" + cmbydcgStatus.Text + "，" + cmbydcgPosition.Text
                                + "，" + cmbydcgSee.Text + "，" + cmbydcgSelction.Text, FillIn, Brushes.Black, x1 + 80, y1 + 5);
            e.Graphics.DrawLine(black, new Point(x1 + 70, y1 + 20), new Point(500, y1 + 20));
            //↑诱导插管
            
            e.Graphics.DrawString(lb8.Text , TagLable, Brushes.Black, x1 + 5, y1 + 25);
            e.Graphics.DrawString(txtcghTingzhen.Controls[0].Text, FillIn, Brushes.Black, x1 + 150, y1 + 25);
            e.Graphics.DrawLine(black, new Point(x1 + 140, y1 + 38), new Point(780, y1 + 38));
            
            //↑插管后听诊
            e.Graphics.DrawString("备注：  " , TagLable, Brushes.Black, x1 + 5, y1 + 45);
            e.Graphics.DrawLine(black, new Point(x1 + 30, y1 + 58), new Point(780, y1 + 58));
            e.Graphics.DrawLine(black, new Point(x1 + 30, y1 + 83), new Point(780, y1 + 83));
            e.Graphics.DrawLine(black, new Point(x1 + 30, y1 + 108), new Point(780, y1 + 108));
            
            if (rtbcgRemark.Text!="")
            {
                if (rtbcgRemark.Text.Length > 50 && rtbcgRemark.Text.Length <= 100)
                {
                    e.Graphics.DrawString(rtbcgRemark.Text.Substring(0, 50), FillIn, Brushes.Black, x1 + 45, y1 + 45);
                    e.Graphics.DrawString(rtbcgRemark.Text.Substring(51), FillIn, Brushes.Black, x1 + 45, y1 + 70);
            
                }
                if (rtbcgRemark.Text.Length > 100 && rtbcgRemark.Text.Length <= 150)
                {
                    e.Graphics.DrawString(rtbcgRemark.Text.Substring(0, 50), FillIn, Brushes.Black, x1 + 45, y1 + 45);
                    e.Graphics.DrawString(rtbcgRemark.Text.Substring(51, 50), FillIn, Brushes.Black, x1 + 45, y1 + 70);
                    e.Graphics.DrawString(rtbcgRemark.Text.Substring(101), FillIn, Brushes.Black, x1 + 45, y1 + 95);

                }
                else
                {
                    e.Graphics.DrawString(rtbcgRemark.Text, FillIn, Brushes.Black, x1 + 45, y1 + 45);

                }
               
            }
            
             //↑备注


            //打印硬膜外蛛网膜下腔神经阻滞
            y1 = y1 + 125;
            e.Graphics.DrawLine(green, new Point(x, y1), new Point(790, y1));//横线三
            e.Graphics.DrawString("硬\n膜\n外\n蛛\n网\n膜\n下\n腔\n神\n经\n阻\n滞", TagLable, Brushes.Black, 25, y1 + 3);
            //打印第一行
            e.Graphics.DrawString("脊髓麻醉穿刺时体位:_____________", TagLable, Brushes.Black, x1 + 3, y1 + 5);
            e.Graphics.DrawString("                    "+cmbjzccPostural.Text, FillIn, Brushes.Black, x1 + 3, y1 + 3);
           
            //打印第二行
            e.Graphics.DrawString("1、1) 硬膜外穿刺点: ①上:_______________        导管留置_________cm", TagLable, Brushes.Black, x1 + 3, y1 + 25);
            e.Graphics.DrawString("                          " + txtymwccUp.Controls[0].Text + "," + cmbymwccUpSelect.Text, FillIn, Brushes.Black, x1 + 3, y1 + 23);
            e.Graphics.DrawString("                                                          " + txtymwccUpLong.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 23);
            
            //打印第三行
            e.Graphics.DrawString("            穿刺点: ②下:_______________        导管留置_________cm", TagLable, Brushes.Black, x1 + 3, y1 + 45);
            e.Graphics.DrawString("                          "+txtymwccDown.Controls[0].Text + "," + cmbymwccDownSelect.Text, FillIn, Brushes.Black, x1 + 3, y1 + 43);
            e.Graphics.DrawString("                                                          "+txtymwccDownLong.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 43);
            
            //打印第四行
            e.Graphics.DrawString("      顺利:_______          穿刺、置管时异常:__________", TagLable, Brushes.Black, x1 + 3, y1 + 65);
            e.Graphics.DrawString("            " + cmbShunLi.Text , FillIn, Brushes.Black, x1 + 3, y1 + 63);
            e.Graphics.DrawString("                                              " + cmbYigan.Text, FillIn, Brushes.Black, x1 + 3, y1 + 63);

            //打印第五行
            e.Graphics.DrawString("   2)蛛网膜下腔麻醉药:_______________ 剂量:_________mg；用________________稀释________ml", TagLable, Brushes.Black, x1 + 3, y1 + 85);
            e.Graphics.DrawString("                       " + txtzwmxqDrug.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 83);
            e.Graphics.DrawString("                                             " + txtzwmxqDosage.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 83);
            e.Graphics.DrawString("                                                            " + cmbzwmxqDiluteBy.Text, FillIn, Brushes.Black, x1 + 3, y1 + 83);
            e.Graphics.DrawString("                                                                               " + txtzwmxqDiluteDosage.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 83);

            //打印第6行
            e.Graphics.DrawString("      注射速度___________sec ; 注入后___________min翻身", TagLable, Brushes.Black, x1 + 3, y1 + 105);
            e.Graphics.DrawString("               " + txtzwmxqDrugSpeed.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 103);
            e.Graphics.DrawString("                                        " + txtzwmxqDrugTime.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 103);

            //打印第7行
            e.Graphics.DrawString("   3)骶管:_______________________________________________________________________________", TagLable, Brushes.Black, x1 + 3, y1 + 125);
            e.Graphics.DrawString("           " + txtdiguan.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 123);
            
            //打印第8行
            e.Graphics.DrawString("2、麻醉平面手术开始: 上_____________________  下_________________________ ；", TagLable, Brushes.Black, x1 + 3, y1 + 145);
            e.Graphics.DrawString("                          " + txtmzpmssStartUp.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 143);
            e.Graphics.DrawString("                                                       " + txtmzpmssStartDown.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 143);

            //打印第9行
            e.Graphics.DrawString("3、备注:_________________________________________________________________________________", TagLable, Brushes.Black, x1 + 3, y1 + 165);
            e.Graphics.DrawString("         " + txtsjzzRemark.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 163);


            //打印神经阻滞
            y1 = y1 + 185;
            e.Graphics.DrawLine(green, new Point(x, y1), new Point(790, y1));//横线四
            e.Graphics.DrawString("\n\n\n神\n经\n阻\n滞", TagLable, Brushes.Black, 25, y1+5);
            //打印第一行
            e.Graphics.DrawString("1、1) 颈丛神经阻滞:________________   浅丛:________________    深丛:______________", TagLable, Brushes.Black, x1 + 3, y1 + 5);
            e.Graphics.DrawString("                    " + cmbjcsjzzSelect1.Text + cmbjcsjzzSelect2.Text, FillIn, Brushes.Black, x1 + 3, y1 + 3);
            e.Graphics.DrawString("                                           " + cmbjcsjzzSelectqc.Text, FillIn, Brushes.Black, x1 + 3, y1 + 3);
            e.Graphics.DrawString("                                                                     " + cmbjcsjzzSelectsc.Text, FillIn, Brushes.Black, x1 + 3, y1 + 3);
           

            //打印第二行
            e.Graphics.DrawString("   2) 臂丛神经阻滞:_____________________", TagLable, Brushes.Black, x1 + 3, y1 + 25);
            e.Graphics.DrawString("                      " + cmbbcsjzzSelect.Text , FillIn, Brushes.Black, x1 + 3, y1 + 23);
            
            //打印第三行
            e.Graphics.DrawString("   3) __________________________", TagLable, Brushes.Black, x1 + 3, y1 + 45);
            e.Graphics.DrawString("        " + cmbOthersjzzSelect.Text, FillIn, Brushes.Black, x1 + 3, y1 + 43);
                      
            //打印第4行
            e.Graphics.DrawString("   4) 其他:______________________________________________________________________________", TagLable, Brushes.Black, x1 + 3, y1 + 65);
            e.Graphics.DrawString("            " + txtothersszz.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 63);
            
            //打印第5行
            e.Graphics.DrawString("2、穿刺情况:________________    异感:__________", TagLable, Brushes.Black, x1 + 3, y1 + 85);
            e.Graphics.DrawString("             " + cmbccqk.Text, FillIn, Brushes.Black, x1 + 3, y1 + 81);
            e.Graphics.DrawString("                                      " + cmbccqkyc.Text, FillIn, Brushes.Black, x1 + 3, y1 + 83);

            //打印第6行
            e.Graphics.DrawString("3、药物:_________________________________________________________________________________", TagLable, Brushes.Black, x1 + 3, y1 + 105);
            e.Graphics.DrawString("        " + txtyaowu.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 103);

            //打印第7行
            e.Graphics.DrawString("4、阻滞效果______________", TagLable, Brushes.Black, x1 + 3, y1 + 125);
            e.Graphics.DrawString("              " + cmbzzEffect.Text, FillIn, Brushes.Black, x1 + 3, y1 + 123);
           
            //打印第8行
            e.Graphics.DrawString("5、备注:_________________________________________________________________________________", TagLable, Brushes.Black, x1 + 3, y1 + 145);
            e.Graphics.DrawString("         " + txtRemark.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 143);


            //打印其他栏
            y1 = y1 + 165;
            e.Graphics.DrawLine(green, new Point(x, y1), new Point(790, y1));//横线六
            e.Graphics.DrawString("\n\n\n\n\n其\n它", TagLable, Brushes.Black, 25, y1 + 5);
            //打印第一行
            e.Graphics.DrawString("1、静脉麻醉:______________________________________________________________________________", TagLable, Brushes.Black, x1 + 3, y1 + 5);
            e.Graphics.DrawString("             " + txtjmHocus.Controls[0].Text + cmbjcsjzzSelect2.Text, FillIn, Brushes.Black, x1 + 3, y1 + 3);
    

            //打印第二行
            e.Graphics.DrawString("1、麻醉监护(MAC):_________________________________________________________________________", TagLable, Brushes.Black, x1 + 3, y1 + 25);
            e.Graphics.DrawString("                  " + txtMAC.Controls[0].Text , FillIn, Brushes.Black, x1 + 3, y1 + 23);
            
            //打印第三行
            e.Graphics.DrawString("有创操作: 深静脉穿刺:________________ 穿刺动脉:________________ 其它:______________________", TagLable, Brushes.Black, x1 + 3, y1 + 45);
            e.Graphics.DrawString("                      " + txtsjmcc.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 43);
            e.Graphics.DrawString("                                               " + txtccdm.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 43);
            e.Graphics.DrawString("                                                                      " + txtccdmOther.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 43);

            //打印第4行
            e.Graphics.DrawString("备注:______________________________________________________________________________", TagLable, Brushes.Black, x1 + 3, y1 + 65);
            e.Graphics.DrawString("      " + txtccdmRemark.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 63);

            //打印第5行
            e.Graphics.DrawString("镇痛:昂司丹琼_________mg   芬太尼_________mg   曲多马_________mg  布比卡因_________mg", TagLable, Brushes.Black, x1 + 3, y1 + 85);
            e.Graphics.DrawString("              " + txtmafei.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 83);
            e.Graphics.DrawString("                                  " + txtfentaini.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 83);
            e.Graphics.DrawString("                                                       " + txtqumaduo.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 83);
            e.Graphics.DrawString("                                                                            " + txtbubikayin.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 83);

            //打印第6行
            e.Graphics.DrawString("       耐乐品_________mg   氟哌利多_________mg", TagLable, Brushes.Black, x1 + 3, y1 + 105);
            e.Graphics.DrawString("              " + txtnailepin.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 103);
            //e.Graphics.DrawString("                                 " + txtdingkayin.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 103);
            e.Graphics.DrawString("                                     " + txtfupailiduo.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 103);

            //打印第7行
            e.Graphics.DrawString("       总量__________mg；初始设备: 持续剂量__________ml/h；PAC__________ml；PAC锁定时间_________min", TagLable, Brushes.Black, x1 + 3, y1 + 125);
            e.Graphics.DrawString("            " + txtzongliang.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 123);
            e.Graphics.DrawString("                                             " + txtchixujiliang.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 123);
            e.Graphics.DrawString("                                                                " + txtPAC.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 123);
            e.Graphics.DrawString("                                                                                        " + txtPACtime.Controls[0].Text, FillIn, Brushes.Black, x1 + 3, y1 + 123);

            

            //打印神经阻滞
            y1 = y1 + 145;
            e.Graphics.DrawLine(green, new Point(x, y1), new Point(790, y1));//横线三
            e.Graphics.DrawString("麻\n醉\n中\n特\n殊\n情\n况", TagLable, Brushes.Black, 25, y1 + 3);
          
            if (rtbmzSpecialSituation.Text != "")
            {
                
                if (rtbmzSpecialSituation.Text.Length>55&&rtbmzSpecialSituation.Text.Length<=110)
                {
                    e.Graphics.DrawString(rtbmzSpecialSituation.Text.Substring(0, 55).ToString(), TagLable, Brushes.Black, x1 + 5, y1 + 5);
                    e.Graphics.DrawString(rtbmzSpecialSituation.Text.Substring(56).ToString(), TagLable, Brushes.Black, x1 + 5, y1 + 25);

                    
                }
                if (rtbmzSpecialSituation.Text.Length > 110 && rtbmzSpecialSituation.Text.Length <= 165)
                {
                    e.Graphics.DrawString(rtbmzSpecialSituation.Text.Substring(0, 55).ToString(), TagLable, Brushes.Black, x1 + 5, y1 + 5);
                    e.Graphics.DrawString(rtbmzSpecialSituation.Text.Substring(56, 55).ToString(), TagLable, Brushes.Black, x1 + 5, y1 + 25);
                    e.Graphics.DrawString(rtbmzSpecialSituation.Text.Substring(111).ToString(), TagLable, Brushes.Black, x1 + 5, y1 + 45);

                }
                else
                {
                    e.Graphics.DrawString(rtbmzSpecialSituation.Text, TagLable, Brushes.Black, x1 + 5, y1 + 5);
                }
                
            }
            
            //打印第四行
            e.Graphics.DrawString("麻醉医生", TagLable, Brushes.Black, x1 + 580, y1 + 85);
            e.Graphics.DrawString("          " + txtmzys.Controls[0].Text, FillIn, Brushes.Black, x1 + 580, y1 + 80);
            e.Graphics.DrawLine(black, new Point(x1 + 635, y1 + 95), new Point(x1 + 720, y1 + 95));

            y1 = y1 + 105;
            e.Graphics.DrawLine(green2, new Point(x, y1), new Point(790, y1));//横线八

            e.Graphics.DrawLine(green2, new Point(x, y), new Point(x, y1));//竖线一
            e.Graphics.DrawLine(green, new Point(x + 30, y), new Point(x + 30, y1));//竖线二
            //e.Graphics.DrawLine(green, new Point(x + 550, y + 720), new Point(x + 550, y + 840));//竖线三
            //e.Graphics.DrawLine(green, new Point(x+570, y+720), new Point(x+570, y + 840));//竖线四
            e.Graphics.DrawLine(green2, new Point(790, y), new Point(790, y1));//竖线五
            
            y1 = y1 + 10;
            e.Graphics.DrawString("打印日期：" + Convert.ToString(DateTime.Now), TagLable, Brushes.Black, x1 + 300, y1 + 5);


           

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text=="静脉麻醉"||comboBox1.Text=="全麻")
            {
                rtbmzSpecialSituation.Text = "患者入室心电监护，一般生命体征   ，常规麻醉诱导顺利；术中患者生命体征平稳，手术顺利，术后送麻醉复苏室";
            }
            else
            {
                rtbmzSpecialSituation.Text = "患者一般情况   ，行腰椎联合麻醉穿刺顺利，麻醉平面符合手术要求；术中患者生命体征平稳，手术顺利，术后送病房";
           
            }
        }

       
        
    }
}