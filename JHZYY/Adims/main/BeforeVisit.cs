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
using adims_BLL;

namespace main
{
    public partial class BeforeVisit : Form
    {
        #region <<Members>>

        AdimsController bll = new AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();

        #endregion

        #region <<Constructors>>

        string patID;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BeforeVisit(string patid)
        {
             patID=patid;
            InitializeComponent();
           
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BeforeVisit()
        {
            
            InitializeComponent();

        }

        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeVisit_Load(object sender, EventArgs e)
        {
            BindPatInfo();
            SetEditValue();
            this.txtPhysician.Controls[0].Text = Program.yh;
            this.txtAMethod.Controls[0].DoubleClick += new System.EventHandler(this.txtAMethod_DoubleClick);
            this.txtADrug.Controls[0].DoubleClick += new System.EventHandler(this.txtADrug_DoubleClick);
            this.txtMProjects.Controls[0].DoubleClick += new System.EventHandler(this.txtMProjects_DoubleClick);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.SetVisibleCore(false);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.SetVisibleCore(true);
        }

        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(patID);
            cmbASAClassification.Text = dt.Rows[0]["asa"].ToString();
            if (dt.Rows[0]["asa"].ToString()=="1")
            {
                chkE.Checked = true;
            }
            cmbASA1.Text = dt.Rows[0]["asa"].ToString();
            txtPatid.Controls[0].Text = dt.Rows[0]["patid"].ToString();
            txtPatname.Controls[0].Text = dt.Rows[0]["Patname"].ToString();
            txtPatage.Controls[0].Text = dt.Rows[0]["Patage"].ToString();
            txtPatsex.Controls[0].Text = dt.Rows[0]["Patsex"].ToString();
            txtPatdpm.Controls[0].Text = dt.Rows[0]["patdpm"].ToString();
            txtBednumber.Controls[0].Text = dt.Rows[0]["Patbedno"].ToString();
            txtNssss.Controls[0].Text = dt.Rows[0]["Oname"].ToString();
            txtSqzd.Controls[0].Text = dt.Rows[0]["Pattmd"].ToString();
           
        }

        private void SETVALUES()
        {
            
            try
            {

                txtPatid.Controls[0].Text = "";
                txtPatname.Controls[0].Text = "";
                txtPatage.Controls[0].Text = "";
                txtPatsex.Controls[0].Text = "";
                txtPatdpm.Controls[0].Text = "";
                txtBednumber.Controls[0].Text = "";
                txtNssss.Controls[0].Text = "";
                txtSqzd.Controls[0].Text = "";                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBeforeVisit();
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintView_Click(object sender, EventArgs e)
        {
           
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            string ssMode = string.Empty; // 择期:急诊
            string headNeck = string.Empty; // 头颈部
            if (rdoZq.Checked == true) ssMode = "择期";
            else ssMode = "急诊";
            if (chkWyc.Checked == true) headNeck += "无异常  ";
            if (chkBh.Checked == true) headNeck += "疤痕  ";
            if (chkJd.Checked == true) headNeck += "颈短  ";
            if (chkJbzw.Checked == true) headNeck += "颈部肿物  ";
            if (chkHykn.Checked == true) headNeck += "后仰困难";

            Font ptzt = new Font("新宋体", 10);//普通字体
            int JG = 25;
            int y = 130;
            int x = 50;
            int y1 = 90;
            int x1 = 120;

            e.Graphics.DrawString("择期", ptzt, Brushes.Black, new Point(x1, y1));
            e.Graphics.DrawString("急诊", ptzt, Brushes.Black, new Point(x1+70 , y1));

            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            if (rdoZq.Checked == true)
            {
                e.Graphics.DrawEllipse(pblue2, x1 - 10, y1-5, 50, 20);

            }
            else
            {
                e.Graphics.DrawEllipse(pblue2, x1 + 60, y1-5, 50, 20);
                //e.Graphics.DrawLine(pblue2, x + 120, y-30, x + 280, y-30);
                //e.Graphics.DrawLine(pblue2, x + 120, y-30, x + 120, y-30);
            }

            e.Graphics.DrawString("嘉定区中医医院麻醉前访视记录", new Font("新宋体", 16, FontStyle.Bold), Brushes.Black, new Point(220, 40));
            e.Graphics.DrawString("打印日期：" + Convert.ToString(DateTime.Now), ptzt, Brushes.Black, 520, 90);
            //第一行
            int row = 0;
            e.Graphics.DrawString("姓名： " + txtPatname.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15, x + 140, y + 15);
            e.Graphics.DrawString("床号： " + txtBednumber.Controls[0].Text.Trim() , ptzt, Brushes.Black, new Point(x+150, y));
            e.Graphics.DrawLine(Pens.Black, x + 185, y + 15, x + 290, y + 15);
            e.Graphics.DrawString("住院号： " + txtPatsex.Controls[0].Text.Trim() , ptzt, Brushes.Black, new Point(x+300, y));
            e.Graphics.DrawLine(Pens.Black, x + 345, y + 15, x + 440, y + 15);
            e.Graphics.DrawString("手术日期： " + txtPatage.Controls[0].Text.Trim() , ptzt, Brushes.Black, new Point(x+450, y));
            e.Graphics.DrawLine(Pens.Black, x + 520, y + 15, x + 620, y + 15);
            e.Graphics.DrawString("ASA： " + cmbASAClassification.Text.Trim(), ptzt, Brushes.Black, new Point(x+630, y));
            if (chkE.Checked==true)
            {
                 e.Graphics.DrawString("，E" , ptzt, Brushes.Black, new Point(x+630, y));
           
            }     
            e.Graphics.DrawLine(Pens.Black, x + 655, y + 15, x + 700, y + 15);
            row++;
            
            //第二行
            e.Graphics.DrawString("术前诊断： " + txtSqzd.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 60, y + 15 + JG * row, x + 340, y + 15 + JG * row);
            e.Graphics.DrawString("拟施手术： " + txtNssss.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 350, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 410, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;
            row++;

            //第三行
            Font ptzt1 = new Font("楷体", 11);
            e.Graphics.DrawString("(一)经查阅病史及体检后，目前病人情况", ptzt1, Brushes.Black, new Point(x, y + JG * row));
            row++;

            //第四行
            e.Graphics.DrawString("体重：" + txtWeight.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15 + JG * row, x + 150, y + 15 + JG * row);
            e.Graphics.DrawString("kg  血压：" + txtBlood.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 150, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 220, y + 15 + JG * row, x + 300, y + 15 + JG * row);
            e.Graphics.DrawString("mmHg  心率：" + txtHeartRate.Controls[0].Text, ptzt, Brushes.Black, new Point(x + 300, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 380, y + 15 + JG * row, x + 450, y + 15 + JG * row);
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 450, y + JG * row));
            row++;

            //第五行
            e.Graphics.DrawString("脉搏：" + txtPulse.Controls[0].Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15 + JG * row, x + 150, y + 15 + JG * row);
            e.Graphics.DrawString("次/分  呼吸：" + txtBreathing.Controls[0].Text, ptzt, Brushes.Black, new Point(x + 150, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 235, y + 15 + JG * row, x + 300, y + 15 + JG * row);
            e.Graphics.DrawString("次/分  体温：" + txtBT.Controls[0].Text, ptzt, Brushes.Black, new Point(x + 300, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 387, y + 15 + JG * row, x + 450, y + 15 + JG * row);
            e.Graphics.DrawString("℃", ptzt, Brushes.Black, new Point(x + 450, y + JG * row));
            row++;

            //第六行
            e.Graphics.DrawString("意识：" + cmbAwareness.Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15 + JG * row, x + 150, y + 15 + JG * row);
         
            row++;

            //第七行
            e.Graphics.DrawString("系统病史及治疗药物：" + rtbBSZLW.Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 135, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;
            e.Graphics.DrawLine(Pens.Black, x + 135, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;

            //第八行
            if (rdoIsAHY.Checked == true)
            {
                e.Graphics.DrawString("手术麻醉史： 是" , ptzt, Brushes.Black, new Point(x, y + JG * row));

            }
            else
            {
                e.Graphics.DrawString("手术麻醉史： 无" , ptzt, Brushes.Black, new Point(x, y + JG * row));

            }
                e.Graphics.DrawString("合并症：" + txtAnesthesiaHistoryRemark.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x+130, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 80, y + 15 + JG * row, x + 120, y + 15 + JG * row);
            
            e.Graphics.DrawLine(Pens.Black, x + 180, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;

            //第九行
            if (rdoY.Checked == true)
            {
                e.Graphics.DrawString("过敏史： 有", ptzt, Brushes.Black, new Point(x, y + JG * row));

            }
            else
            {
                e.Graphics.DrawString("过敏史： 无", ptzt, Brushes.Black, new Point(x, y + JG * row));

            }
                e.Graphics.DrawLine(Pens.Black, x + 50, y + 15 + JG * row, x + 100, y + 15 + JG * row);
            e.Graphics.DrawString("过敏物：" + txtAllergyHistoryRemark.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x +120, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 170, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;

            //第十行
            e.Graphics.DrawString("头颈部：" + headNeck, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 15 + JG * row, x + 200, y + 15 + JG * row);
            row++;

            //第十一行
            e.Graphics.DrawString("口腔： 张口： " + txtDehisce.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 80, y + 15 + JG * row, x + 120, y + 15 + JG * row);
            e.Graphics.DrawString("指， 牙齿： " + cmbTooth.Text.Trim(), ptzt, Brushes.Black, new Point(x + 120, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 190, y + 15 + JG * row, x + 250, y + 15 + JG * row);
            row++;

            //第十二行
            e.Graphics.DrawString("心肺听诊： " + txtHLAuscultation.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 65, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;

            //第十三行
            e.Graphics.DrawString("肌力及感觉： " + cmbMuscleFeeling.Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 80, y + 15 + JG * row, x + 170, y + 15 + JG * row);
            e.Graphics.DrawString("感觉异常： " + cmbFeelingAbnormal.Text.Trim(), ptzt, Brushes.Black, new Point(x+200, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 265, y + 15 + JG * row, x + 350, y + 15 + JG * row);
            e.Graphics.DrawString("肌力感觉： " + cmbMuscleDrop.Text.Trim(), ptzt, Brushes.Black, new Point(x+400, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 465, y + 15 + JG * row, x + 550, y + 15 + JG * row);
            row++;
            //第十四行
            e.Graphics.DrawString("外周静脉： " + cmbPeripheralVenous.Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 65, y + 15 + JG * row, x + 170, y + 15 + JG * row);
            row++;
            //第十五行
            e.Graphics.DrawString("脊柱状况： " + cmbSpineCondition.Text+" "+txtSpineConditionOther.Controls[0].Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 65, y + 15 + JG * row, x + 220, y + 15 + JG * row);
            e.Graphics.DrawString("心电图： " + txtECG.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 240, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 290, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;

            //第十六行
            e.Graphics.DrawString("心功能分级： " + cmbHLClassification.Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 75, y + 15 + JG * row, x + 170, y + 15 + JG * row);
            e.Graphics.DrawString("其他： " + txtECG.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 235, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;

            //第十七行
            e.Graphics.DrawString("肺功能： " + cmbLungFunction.Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 15 + JG * row, x + 170, y + 15 + JG * row);
            e.Graphics.DrawString("胸片或胸透： " + txtChest.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 275, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;

            //第十八行
            e.Graphics.DrawString("肝功能： " + cmbLiverFunction.Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 15 + JG * row, x + 165, y + 15 + JG * row);
            e.Graphics.DrawString(", "+ txtLiverFunctionRemark.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 170, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 180, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;

            //第十九行
            e.Graphics.DrawString("肾功能： " + cmbKidneyFunction.Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 15 + JG * row, x + 165, y + 15 + JG * row);
            e.Graphics.DrawString(", " + txtKidneyFunctionRemark.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 170, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 180, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;

            //第二十行
            e.Graphics.DrawString("血红蛋白： " + txtHemoglobin.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 65, y + 15 + JG * row, x + 145, y + 15 + JG * row);
            e.Graphics.DrawString("g/L 红细胞： " + txtErythrocyte.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 145, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 220, y + 15 + JG * row, x + 300, y + 15 + JG * row);
            e.Graphics.DrawString("10³/L 血细胞比容： " + txtHematocrit.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 415, y + 15 + JG * row, x + 495, y + 15 + JG * row);
            e.Graphics.DrawString("% 血小板： " + txtBTB.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 495, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 555, y + 15 + JG * row, x + 635, y + 15 + JG * row);
            e.Graphics.DrawString("10³/L", ptzt, Brushes.Black, new Point(x + 635, y + JG * row));
            row++;

            //第二十一行
            e.Graphics.DrawString("出血时间： " + txtFG.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 65, y + 15 + JG * row, x + 145, y + 15 + JG * row);
            e.Graphics.DrawString(" 凝血时间： " + txtAPTT.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 145, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 215, y + 15 + JG * row, x + 295, y + 15 + JG * row);
            e.Graphics.DrawString(" 凝血酶原时间： " + txtThrombinDate.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 295, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 395, y + 15 + JG * row, x + 475, y + 15 + JG * row);
            e.Graphics.DrawString("S  血钾： " + txtPotassium.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 475, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 530, y + 15 + JG * row, x + 615, y + 15 + JG * row);
            e.Graphics.DrawString("mmol/L", ptzt, Brushes.Black, new Point(x + 615, y + JG * row));
            row++;

            //第二十二行
            e.Graphics.DrawString("血钠： " + txtHyponatremia.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15 + JG * row, x + 120, y + 15 + JG * row);
            e.Graphics.DrawString("mmol/L 血氯： " + txtSerumChloride.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 120, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 200, y + 15 + JG * row, x + 290, y + 15 + JG * row);
            e.Graphics.DrawString("mmol/L 血糖： " + txtBloodSugar.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x + 290, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 375, y + 15 + JG * row, x + 465, y + 15 + JG * row);
            e.Graphics.DrawString("mmol/L", ptzt, Brushes.Black, new Point(x + 465, y + JG * row));
            row++;

            //第二十三行
            e.Graphics.DrawString("其他实验检查异常：(血气分析等) " + txtOtherAbnormal.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 210, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            row++;
            row++;

            //第二十四行
            e.Graphics.DrawString("(二)根据目前病情，麻醉方法选择： ", ptzt1, Brushes.Black, new Point(x, y + JG * row));
            row++;

            //第二十五行
            e.Graphics.DrawString("" , ptzt, Brushes.Black, new Point(x, y + JG * row));
            row++;
            e.Graphics.DrawString("全麻", ptzt, Brushes.Black, new Point(x + 30, y + JG * row));
            e.Graphics.DrawString("硬膜外麻醉", ptzt, Brushes.Black, new Point(x + 140, y + JG * row));
            e.Graphics.DrawString("骶管麻醉", ptzt, Brushes.Black, new Point(x + 260, y + JG * row));
            e.Graphics.DrawString("神经阻滞", ptzt, Brushes.Black, new Point(x + 380, y + JG * row));
            e.Graphics.DrawString("联合麻醉", ptzt, Brushes.Black, new Point(x + 500, y + JG * row));

            if (chkQm.Checked==true)
            {
                e.Graphics.DrawEllipse(pblue2, x + 30 - 5, y + JG * row - 5, 50, 25);
            }
            if (chkYmwzz.Checked == true)
            {
                e.Graphics.DrawEllipse(pblue2, x + 140 - 5, y + JG * row - 5, 90, 25);
            }
            if (chkJm.Checked == true)
            {
                e.Graphics.DrawEllipse(pblue2, x + 260 - 5, y + JG * row - 5, 70, 25);
            }
            if (chkSjzz.Checked == true)
            {
                e.Graphics.DrawEllipse(pblue2, x + 380 - 5, y + JG * row - 5, 70, 25);
            }

            if (chkLhmz.Checked == true)
            {
                e.Graphics.DrawEllipse(pblue2, x + 500 - 5, y + JG * row - 5, 70, 25);
            } 
            row++;
            row++;

            //第二十七行
            e.Graphics.DrawString("(三)术中困难估计及防范措施： ", ptzt1, Brushes.Black, new Point(x, y + JG * row));
            row++;

           //第二十八行

            e.Graphics.DrawString(tbTime.Text + "麻醉讨论，患者一般情况" + cmbGood.Text + "，ASA " + cmbASA1.Text + " 级，拟行上述麻醉，严格无菌操作，术中加强监测。", ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x, y + 15 + JG * row, x + 700, y + 15 + JG * row);
            
            row++; 
            row++;

            //第二十九行
            e.Graphics.DrawString("医师：" + txtPhysician.Controls[0].Text.Trim() , ptzt, Brushes.Black, new Point(x+350, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 385, y + 15 + JG * row, x + 490, y + 15 + JG * row);
            e.Graphics.DrawString("访视日期：" + dtAccessDate.Text.ToString(), ptzt, Brushes.Black, new Point(x + 500, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 565, y + 15 + JG * row, x + 700, y + 15 + JG * row);
           
        
        }

        #endregion

        #region <<Methods>>

        /// <summary>
        /// 清空
        /// </summary>
        private void ClearEdit()
        {
           
            txtWeight.Controls[0].Text = string.Empty;
            txtBlood.Controls[0].Text = string.Empty;
            txtHeartRate.Controls[0].Text = string.Empty;
            txtPulse.Controls[0].Text = string.Empty;
            txtBreathing.Controls[0].Text = string.Empty;
            txtBT.Controls[0].Text = string.Empty;
            rtbBSZLW.Text = string.Empty;
            txtAnesthesiaHistoryRemark.Controls[0].Text = string.Empty;
            txtKidneyFunctionRemark.Controls[0].Text = string.Empty;
            txtLiverFunctionRemark.Controls[0].Text = string.Empty;
            txtDehisce.Controls[0].Text = string.Empty;
            txtHLAuscultation.Controls[0].Text = string.Empty;
            txtOther.Controls[0].Text = string.Empty;
            txtECG.Controls[0].Text = string.Empty;
            txtChest.Controls[0].Text = string.Empty;
            txtHemoglobin.Controls[0].Text = string.Empty;
            txtErythrocyte.Controls[0].Text = string.Empty;
            txtHematocrit.Controls[0].Text = string.Empty;
            txtBTB.Controls[0].Text = string.Empty;
            txtFG.Controls[0].Text = string.Empty;
            txtAPTT.Controls[0].Text = string.Empty;
            txtThrombinDate.Controls[0].Text = string.Empty;
            txtPotassium.Controls[0].Text = string.Empty;
            txtHyponatremia.Controls[0].Text = string.Empty;
            txtSerumChloride.Controls[0].Text = string.Empty;
            txtBloodSugar.Controls[0].Text = string.Empty;
            txtOtherAbnormal.Controls[0].Text = string.Empty;
            txtAllergyHistoryRemark.Controls[0].Text = string.Empty;
            txtProtectiveMeasures.Controls[0].Text = string.Empty;
            txtAMethod.Controls[0].Text = string.Empty;
            txtADrug.Controls[0].Text = string.Empty;
            txtMProjects.Controls[0].Text = string.Empty;
            txtProblemHandle.Text = string.Empty;
            //txtPhysician.Controls[0].Text = string.Empty;
            rdoY.Checked = false;
            rdoN.Checked = false;
            rdoZq.Checked = false;
            rdoJz.Checked = false;
            rdoIsAHY.Checked = false;
            rdoIsAHN.Checked = false;
            chkWyc.Checked = false;
            chkBh.Checked = false;
            chkJd.Checked = false;
            chkJbzw.Checked = false;
            chkHykn.Checked = false;
            chkQm.Checked = false;
            chkYmwzz.Checked = false;
            chkJm.Checked = false;
            chkSjzz.Checked = false;
            chkLhmz.Checked = false;
            cmbTooth.Text = string.Empty;
            cmbAwareness.Text = string.Empty;
            cmbASAClassification.Text = string.Empty;
            chkE.Text = string.Empty;
            cmbMuscleFeeling.Text = string.Empty;
            cmbFeelingAbnormal.Text = string.Empty;
            cmbMuscleDrop.Text = string.Empty;
            cmbPeripheralVenous.Text = string.Empty;
            cmbSpineCondition.Text = string.Empty;
            cmbHLClassification.Text = string.Empty;
            cmbLungFunction.Text = string.Empty;
            cmbLiverFunction.Text = string.Empty;
            cmbKidneyFunction.Text = string.Empty;
            dtAccessDate.Text = string.Empty;
        }

        /// <summary>
        /// 控件赋值
        /// </summary>
        private void SetEditValue()
        {
            ClearEdit();
            DataTable dt = bll.GetBeforeVisitList("PatID = '" + patID + "'");
            if (dt.Rows.Count == 0)
                return;
            else
            {
            DataRow dr = dt.Rows[0];
            txtWeight.Controls[0].Text = Convert.ToString(dr["Weight"]);
            txtBlood.Controls[0].Text = Convert.ToString(dr["Blood"]);
            txtHeartRate.Controls[0].Text = Convert.ToString(dr["HeartRate"]);
            txtPulse.Controls[0].Text = Convert.ToString(dr["Pulse"]);
            txtBreathing.Controls[0].Text = Convert.ToString(dr["Breathing"]);
            txtBT.Controls[0].Text = Convert.ToString(dr["BT"]);
            rtbBSZLW.Text = Convert.ToString(dr["HistoryDrugs"]);
            txtAnesthesiaHistoryRemark.Controls[0].Text = Convert.ToString(dr["AnesthesiaHistoryRemark"]);
            txtKidneyFunctionRemark.Controls[0].Text = Convert.ToString(dr["KidneyFunctionRemark"]);
            txtLiverFunctionRemark.Controls[0].Text = Convert.ToString(dr["LiverFunctionRemark"]);
            txtDehisce.Controls[0].Text = Convert.ToString(dr["Dehisce"]);
            txtHLAuscultation.Controls[0].Text = Convert.ToString(dr["HLAuscultation"]);
            txtOther.Controls[0].Text = Convert.ToString(dr["Other"]);
            txtECG.Controls[0].Text = Convert.ToString(dr["ECG"]);
            txtChest.Controls[0].Text = Convert.ToString(dr["Chest"]);
            txtHemoglobin.Controls[0].Text = Convert.ToString(dr["Hemoglobin"]);
            txtErythrocyte.Controls[0].Text = Convert.ToString(dr["Erythrocyte"]);
            txtHematocrit.Controls[0].Text = Convert.ToString(dr["Hematocrit"]);
            txtBTB.Controls[0].Text = Convert.ToString(dr["BTB"]);
            txtFG.Controls[0].Text = Convert.ToString(dr["FG"]);
            txtAPTT.Controls[0].Text = Convert.ToString(dr["APTT"]);
            txtThrombinDate.Controls[0].Text = Convert.ToString(dr["ThrombinDate"]);
            txtPotassium.Controls[0].Text = Convert.ToString(dr["Potassium"]);
            txtHyponatremia.Controls[0].Text = Convert.ToString(dr["Hyponatremia"]);
            txtSerumChloride.Controls[0].Text = Convert.ToString(dr["SerumChloride"]);
            txtBloodSugar.Controls[0].Text = Convert.ToString(dr["BloodSugar"]);
            txtOtherAbnormal.Controls[0].Text = Convert.ToString(dr["OtherAbnormal"]);
            txtAllergyHistoryRemark.Controls[0].Text = Convert.ToString(dr["AllergyHistoryRemark"]);
            txtProtectiveMeasures.Controls[0].Text = Convert.ToString(dr["ProtectiveMeasures"]);
            txtPhysician.Controls[0].Text = Convert.ToString(dr["Physician"]);
            txtAMethod.Controls[0].Text = Convert.ToString(dr["AMethod"]);
            txtADrug.Controls[0].Text = Convert.ToString(dr["ADrug"]);
            txtMProjects.Controls[0].Text = Convert.ToString(dr["MProjects"]);
            txtProblemHandle.Text = Convert.ToString(dr["ProblemHandle"]);
            if (Convert.ToInt32(dr["IsAllergyHistory"]) == 1) rdoY.Checked = true;
            else rdoN.Checked = true;
            if (Convert.ToInt32(dr["SSMode"]) == 0) rdoZq.Checked = true;
            else rdoJz.Checked = true;
            if (Convert.ToInt32(dr["IsAnesthesiaHistory"]) == 1) rdoIsAHY.Checked = true;
            else rdoIsAHN.Checked = true;

            string headNeck = Convert.ToString(dr["HeadNeck"]);
            if (headNeck.Contains("0")) chkWyc.Checked = true;
            if (headNeck.Contains("1")) chkBh.Checked = true;
            if (headNeck.Contains("2")) chkJd.Checked = true;
            if (headNeck.Contains("3")) chkJbzw.Checked = true;
            if (headNeck.Contains("4")) chkHykn.Checked = true;

            string aProgram = Convert.ToString(dr["AProgram"]);
            if (aProgram.Contains("0")) chkQm.Checked = true;
            if (aProgram.Contains("1")) chkYmwzz.Checked = true;
            if (aProgram.Contains("2")) chkJm.Checked = true;
            if (aProgram.Contains("3")) chkSjzz.Checked = true;
            if (aProgram.Contains("4")) chkLhmz.Checked = true;
            if (Convert.ToInt32(dr["E"]) == 0) chkE.Checked = false;
            else chkE.Checked = true;

            cmbTooth.Text = Convert.ToString(dr["Tooth"]);
            cmbAwareness.Text = Convert.ToString(dr["Awareness"]);
            cmbASAClassification.Text = Convert.ToString(dr["ASAClassification"]);
            cmbMuscleFeeling.Text = Convert.ToString(dr["MuscleFeeling"]);
            cmbFeelingAbnormal.Text = Convert.ToString(dr["FeelingAbnormal"]);
            cmbMuscleDrop.Text = Convert.ToString(dr["MuscleDrop"]);
            cmbPeripheralVenous.Text = Convert.ToString(dr["PeripheralVenous"]);
            cmbSpineCondition.Text = Convert.ToString(dr["SpineCondition"]);
            txtSpineConditionOther.Controls[0].Text = Convert.ToString(dr["SpineConditionOther"]);
            cmbHLClassification.Text = Convert.ToString(dr["HLClassification"]);
            cmbLungFunction.Text = Convert.ToString(dr["LungFunction"]);
            cmbLiverFunction.Text = Convert.ToString(dr["LiverFunction"]);
            cmbKidneyFunction.Text = Convert.ToString(dr["KidneyFunction"]);
            dtAccessDate.Value = Convert.ToDateTime(dr["AccessDate"]);
            }
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <returns></returns>
        private void SaveBeforeVisit()
        {
            if (!ValiDateBeforeVisit()) return;
            Dictionary<string, string> beforeVisit = new Dictionary<string, string>();
            int result = 0;
            try
            {
                beforeVisit.Add("Weight", txtWeight.Controls[0].Text);
                beforeVisit.Add("Blood", txtBlood.Controls[0].Text);
                beforeVisit.Add("HeartRate", txtHeartRate.Controls[0].Text);
                beforeVisit.Add("Pulse", txtPulse.Controls[0].Text);
                beforeVisit.Add("Breathing", txtBreathing.Controls[0].Text);
                beforeVisit.Add("BT", txtBT.Controls[0].Text);
                beforeVisit.Add("Awareness", cmbAwareness.Text);
                if (rdoZq.Checked == true) beforeVisit.Add("SSMode", "0");
                else beforeVisit.Add("SSMode", "1");
                beforeVisit.Add("HistoryDrugs", rtbBSZLW.Text);
                if (rdoIsAHN.Checked == true) beforeVisit.Add("IsAnesthesiaHistory", "0");
                else beforeVisit.Add("IsAnesthesiaHistory", "1");
                beforeVisit.Add("AnesthesiaHistoryRemark", txtAnesthesiaHistoryRemark.Controls[0].Text);
                if (rdoN.Checked == true) beforeVisit.Add("IsAllergyHistory", "0");
                else beforeVisit.Add("IsAllergyHistory", "1");
                beforeVisit.Add("AllergyHistoryRemark", txtAllergyHistoryRemark.Controls[0].Text);
                string headNeck = string.Empty;
                if (chkWyc.Checked == true) headNeck += "0";
                if (chkBh.Checked == true) headNeck += "1";
                if (chkJd.Checked == true) headNeck += "2";
                if (chkJbzw.Checked == true) headNeck += "3";
                if (chkHykn.Checked == true) headNeck += "4";
                beforeVisit.Add("HeadNeck", headNeck);
                beforeVisit.Add("Dehisce", txtDehisce.Controls[0].Text);
                beforeVisit.Add("Tooth", cmbTooth.Text);
                beforeVisit.Add("HLAuscultation", txtHLAuscultation.Controls[0].Text);
                beforeVisit.Add("ASAClassification", cmbASAClassification.Text);
                if (chkE.Checked == false) beforeVisit.Add("E", "0");
                else beforeVisit.Add("E", "1");
                beforeVisit.Add("MuscleFeeling", cmbMuscleFeeling.Text);
                beforeVisit.Add("FeelingAbnormal", cmbFeelingAbnormal.Text);
                beforeVisit.Add("MuscleDrop", cmbMuscleDrop.Text);
                beforeVisit.Add("Other", txtOther.Controls[0].Text);
                beforeVisit.Add("PeripheralVenous", cmbPeripheralVenous.Text);
                beforeVisit.Add("SpineCondition", cmbSpineCondition.Text);
                beforeVisit.Add("HLClassification", cmbHLClassification.Text);
                beforeVisit.Add("LungFunction", cmbLungFunction.Text);
                beforeVisit.Add("ECG", txtECG.Controls[0].Text);
                beforeVisit.Add("Chest", txtChest.Controls[0].Text);
                beforeVisit.Add("LiverFunction", cmbLiverFunction.Text);
                beforeVisit.Add("LiverFunctionRemark", txtLiverFunctionRemark.Controls[0].Text);
                beforeVisit.Add("KidneyFunction", cmbKidneyFunction.Text);
                beforeVisit.Add("KidneyFunctionRemark", txtKidneyFunctionRemark.Controls[0].Text);
                beforeVisit.Add("Hemoglobin", txtHemoglobin.Controls[0].Text);
                beforeVisit.Add("Erythrocyte", txtErythrocyte.Controls[0].Text);
                beforeVisit.Add("Hematocrit", txtHematocrit.Controls[0].Text);
                beforeVisit.Add("BTB", txtBTB.Controls[0].Text);
                beforeVisit.Add("FG", txtFG.Controls[0].Text);
                beforeVisit.Add("APTT", txtAPTT.Controls[0].Text);
                beforeVisit.Add("ThrombinDate", txtThrombinDate.Controls[0].Text);
                beforeVisit.Add("Potassium", txtPotassium.Controls[0].Text);
                beforeVisit.Add("Hyponatremia", txtHyponatremia.Controls[0].Text);
                beforeVisit.Add("SerumChloride", txtSerumChloride.Controls[0].Text);
                beforeVisit.Add("BloodSugar", txtBloodSugar.Controls[0].Text);
                beforeVisit.Add("OtherAbnormal", txtOtherAbnormal.Controls[0].Text);
                string aProgram = string.Empty;
                if (chkQm.Checked == true) aProgram += "0";
                if (chkYmwzz.Checked == true) aProgram += "1";
                if (chkJm.Checked == true) aProgram += "2";
                if (chkSjzz.Checked == true) aProgram += "3";
                if (chkLhmz.Checked == true) aProgram += "4";
                beforeVisit.Add("AProgram", aProgram);
                beforeVisit.Add("AMethod", txtAMethod.Controls[0].Text);
                beforeVisit.Add("ADrug", txtADrug.Controls[0].Text);
                beforeVisit.Add("MProjects", txtMProjects.Controls[0].Text);
                beforeVisit.Add("ProblemHandle", txtProblemHandle.Text);
                beforeVisit.Add("ProtectiveMeasures", txtProtectiveMeasures.Controls[0].Text);
                beforeVisit.Add("Physician", txtPhysician.Controls[0].Text);
                beforeVisit.Add("AccessDate", dtAccessDate.Value.ToString());
                beforeVisit.Add("Patid", txtPatid.Controls[0].Text);
                beforeVisit.Add("SpineConditionOther", txtSpineConditionOther.Controls[0].Text);
                
                if (!bll.GetBeforeVisitCount(" PatID = '" + txtPatid.Controls[0].Text + "'"))
                    result = bll.InsertBeforeVisit(beforeVisit);
                else
                    result = bll.UpdateBeforeVisit(beforeVisit);
                if (result > 0)
                    MessageBox.Show("保存成功！");
                else
                    MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool ValiDateBeforeVisit()
        {
            if (!ValidationRegex.ValidteData(txtWeight.Controls[0].Text) && !ValidationRegex.ValidteDouble(txtWeight.Controls[0].Text))
            {
                MessageBox.Show("体重值填写有误，请检查！");
                txtWeight.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtBlood.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtBlood.Controls[0].Text))
            {
                MessageBox.Show("血压值填写有误，请检查！");
                txtBlood.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtHeartRate.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtHeartRate.Controls[0].Text))
            {
                MessageBox.Show("心率值填写有误，请检查！");
                txtHeartRate.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtPulse.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtPulse.Controls[0].Text))
            {
                MessageBox.Show("脉搏值填写有误，请检查！");
                txtPulse.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtBreathing.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtBreathing.Controls[0].Text))
            {
                MessageBox.Show("呼吸值填写有误，请检查！");
                txtBreathing.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtBT.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtBT.Controls[0].Text))
            {
                MessageBox.Show("体温值填写有误，请检查！");
                txtBT.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtHemoglobin.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtHemoglobin.Controls[0].Text))
            {
                MessageBox.Show("血红蛋白值填写有误，请检查！");
                txtHemoglobin.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtErythrocyte.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtErythrocyte.Controls[0].Text))
            {
                MessageBox.Show("红细胞值填写有误，请检查！");
                txtErythrocyte.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtHematocrit.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtHematocrit.Controls[0].Text))
            {
                MessageBox.Show("血细胞比容值填写有误，请检查！");
                txtHematocrit.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtBTB.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtBTB.Controls[0].Text))
            {
                MessageBox.Show("血小板值填写有误，请检查！");
                txtBTB.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtFG.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtFG.Controls[0].Text))
            {
                MessageBox.Show("FG值填写有误，请检查！");
                txtFG.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtAPTT.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtAPTT.Controls[0].Text))
            {
                MessageBox.Show("APTT值填写有误，请检查！");
                txtAPTT.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtThrombinDate.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtThrombinDate.Controls[0].Text))
            {
                MessageBox.Show("凝血酶原时间值填写有误，请检查！");
                txtThrombinDate.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtPotassium.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtPotassium.Controls[0].Text))
            {
                MessageBox.Show("血钾值填写有误，请检查！");
                txtPotassium.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtHyponatremia.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtHyponatremia.Controls[0].Text))
            {
                MessageBox.Show("血钠值填写有误，请检查！");
                txtHyponatremia.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtSerumChloride.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtSerumChloride.Controls[0].Text))
            {
                MessageBox.Show("血氯值填写有误，请检查！");
                txtSerumChloride.Focus();
                return false;
            }

            if (!ValidationRegex.ValidteData(txtBloodSugar.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtBloodSugar.Controls[0].Text))
            {
                MessageBox.Show("血糖值填写有误，请检查！");
                txtBloodSugar.Focus();
                return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// 双击麻醉方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAMethod_DoubleClick(object sender, EventArgs e)
        {
            //CommMaster frmCommMaster = new CommMaster("mazuifangan");
            //if (frmCommMaster.ShowDialog() == DialogResult.OK)
            //{
            //    if (!string.IsNullOrEmpty(txtAMethod.Controls[0].Text))
            //        txtAMethod.Controls[0].Text = txtAMethod.Controls[0].Text + "，" + frmCommMaster.values;
            //    else
            //        txtAMethod.Controls[0].Text = frmCommMaster.values;

            //}
        }

        /// <summary>
        /// 双击麻醉用药
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtADrug_DoubleClick(object sender, EventArgs e)
        {
            //CommMaster frmCommMaster = new CommMaster("yaopinmingcheng");
            //if (frmCommMaster.ShowDialog() == DialogResult.OK)
            //{
            //    if (!string.IsNullOrEmpty(txtADrug.Controls[0].Text))
            //        txtADrug.Controls[0].Text = txtADrug.Controls[0].Text + "，" + frmCommMaster.values;
            //    else
            //        txtADrug.Controls[0].Text = frmCommMaster.values;
            //}
        }

        /// <summary>
        /// 双击监护项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMProjects_DoubleClick(object sender, EventArgs e)
        {
            //CommMaster frmCommMaster = new CommMaster("jianhuxiangmu");
            //if (frmCommMaster.ShowDialog() == DialogResult.OK)
            //{
            //    if (!string.IsNullOrEmpty(txtMProjects.Controls[0].Text))
            //        txtMProjects.Controls[0].Text = txtMProjects.Controls[0].Text + "，" + frmCommMaster.values;
            //    else
            //        txtMProjects.Controls[0].Text = frmCommMaster.values;
            //}
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }
    }
}
