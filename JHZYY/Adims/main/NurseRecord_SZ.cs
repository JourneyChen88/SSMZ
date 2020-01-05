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
    public partial class NurseRecord_SZ : Form
    {
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        string PATID, MZID;
        public NurseRecord_SZ(string patid, string mzid)
        {
            PATID = patid;
            MZID = mzid;
            InitializeComponent();
        }
        public NurseRecord_SZ()
        {

            InitializeComponent();
        }
        int BCCount = 0;

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SAVE();

        }

        private void SAVE()//保存方法
        {
            Dictionary<string, string> A_VISIT = new Dictionary<string, string>();
            int result = 0;
            try
            {
                A_VISIT.Add("Jingmaitonglu", cmbShenzhi.Text);
                A_VISIT.Add("Baonuan", cmbJMCC.Text);
                A_VISIT.Add("Tiwei", cmbDaoniao.Text);
                A_VISIT.Add("Dianjiban", cmbPifuRS.Text);
                A_VISIT.Add("TiweiGaibian", tbTiweiGaibian.Text);
                A_VISIT.Add("SQpifu", cmbTiwei.Text);
                A_VISIT.Add("SHpifu", cmbSHpifu.Text);
                A_VISIT.Add("FusushiXZP", tbFusushiXZP.Text);
                A_VISIT.Add("BingfangXZP", tbBingfangXZP.Text);
                A_VISIT.Add("Yinliu", cmbYinliu.Text);
                A_VISIT.Add("Zhiruwu", tbZhiruwu.Text);
                A_VISIT.Add("Remark", tbRemark.Text);
                A_VISIT.Add("SSYSqian", cmbSSYSqian.Text);
                A_VISIT.Add("SSYShou", cmbSSYShou.Text);
                A_VISIT.Add("XSHSqian", cmbXSHSqian.Text);
                A_VISIT.Add("XSHShou", cmbXSHShou.Text);
                A_VISIT.Add("XHHSqian", cmbXHHSqian.Text);
                A_VISIT.Add("XHHShou", cmbXHHShou.Text);
                A_VISIT.Add("VisitDate", dtVisitDate.Value.ToString());
                A_VISIT.Add("PatID", PATID);

                result = DAL.UpdateNurseRecord_SZ(A_VISIT);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！"); 
                    BCCount++;
                }
                else
                    MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(PATID);

            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["PatZhuYuanID"].ToString();
            tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
            tbShoushuName.Text = dt.Rows[0]["Oname"].ToString();
            tbSZZD.Text = dt.Rows[0]["Pattmd"].ToString();
            tbSex.Text = dt.Rows[0]["patsex"].ToString();
            tbAge.Text = dt.Rows[0]["patage"].ToString();
            cmbMZFF.Text = dt.Rows[0]["Amethod"].ToString();
        }
        private void BindCombox3()
        {
            cmbXSHSqian.Items.Clear();
            cmbXSHShou.Items.Clear();
            cmbXHHSqian.Items.Clear();
            cmbXHHSqian.Items.Clear();

            DataTable dt = new DataTable();
            dt = DAL.GetAll_hushi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbXSHSqian.Items.Add(dt.Rows[i][0]);
                cmbXSHShou.Items.Add(dt.Rows[i][0]);
                cmbXHHSqian.Items.Add(dt.Rows[i][0]);
                cmbXHHSqian.Items.Add(dt.Rows[i][0]);

            }
        }

        private void BindCombox2()
        {
            cmbSSYSqian.Items.Clear();
            cmbSSYShou.Items.Clear();

            DataTable dt = new DataTable();
            dt = DAL.GetAllMZYS();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbSSYSqian.Items.Add(dt.Rows[i][0]);
                cmbSSYShou.Items.Add(dt.Rows[i][0]);
            }
        }

        private void NurseRecord_SZ_Load(object sender, EventArgs e)
        {
            dgvNurseRecord.Rows.Add(16);
            dgvNurseRecord.RowsDefaultCellStyle.BackColor = Color.Silver;
            dgvNurseRecord.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            BindPatInfo();
            BindCombox3();
            BindCombox2();
            DataTable dt = DAL.GetNurseRecord_SZ(PATID);
            if (dt.Rows.Count == 0)
            {
                DAL.InsertNurseRecord_SZ(PATID, dtVisitDate.Value, 0);
            }
            else
            {
                cmbShenzhi.Text = dt.Rows[0]["Jingmaitonglu"].ToString();
                cmbJMCC.Text = dt.Rows[0]["Baonuan"].ToString();
                cmbDaoniao.Text = dt.Rows[0]["Tiwei"].ToString();
                cmbPifuRS.Text = dt.Rows[0]["Dianjiban"].ToString();
                tbTiweiGaibian.Text = dt.Rows[0]["TiweiGaibian"].ToString();
                cmbTiwei.Text = dt.Rows[0]["SQpifu"].ToString();
                cmbSHpifu.Text = dt.Rows[0]["SHpifu"].ToString();
                tbFusushiXZP.Text = dt.Rows[0]["FusushiXZP"].ToString();
                cmbYinliu.Text = dt.Rows[0]["Yinliu"].ToString();
                tbZhiruwu.Text = dt.Rows[0]["Zhiruwu"].ToString();
                tbRemark.Text = dt.Rows[0]["Remark"].ToString();
                cmbSSYSqian.Text = dt.Rows[0]["SSYSqian"].ToString();
                cmbSSYShou.Text = dt.Rows[0]["SSYShou"].ToString();
                cmbXSHSqian.Text = dt.Rows[0]["XSHSqian"].ToString();
                cmbXSHShou.Text = dt.Rows[0]["XSHShou"].ToString();
                cmbXHHSqian.Text = dt.Rows[0]["XHHSqian"].ToString();
                cmbXHHShou.Text = dt.Rows[0]["XHHShou"].ToString();
                if (dt.Rows[0]["VisitDate"] != null)
                {
                    dtVisitDate.Value = Convert.ToDateTime(dt.Rows[0]["VisitDate"]);
                }

                if (Convert.ToInt32(dt.Rows[0]["IsRead"]) == 1)
                {
                    SaveToolStripMenuItem.Enabled = false;
                }
            }
            Bind_comboBox1();
            comboBox1.SelectedIndex = 0;
            button3_Click(null, null);


        }
        private void DataGridBind()
        {
            string kongge = "";
            for (int i = 0; i < 20; i++)
            {
                dgvNurseRecord.Rows[i].Cells[0].Value = kongge;
                dgvNurseRecord.Rows[i].Cells[5].Value = kongge;
            }



        }

        private void printStripMenuItem_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);  
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);  
            
        }

        //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{

        //    Pen ptp = Pens.Black;//普通画笔          
        //    #region 2014/03/18修改后的代码
        //    Font textfront =new Font("宋体",8);
        //    Font tagfont = new Font("宋体", 13);
        //    Pen pblack = Pens.Black;

        //    int x = 50, y = 20, y1 = y + 70;
        //    string title1 = "江  苏  盛  泽  医  院";
        //    string title2 = "江苏省人民医院盛泽分院";
        //    string title3 = "      术后访视单";
        //    y = y + 30; y1 = y + 18;
        //    e.Graphics.DrawString(title1, tagfont, Brushes.Black, x + 200, y);
        //    y = y + 30; y1 = y + 18;
        //    e.Graphics.DrawString(title2, tagfont, Brushes.Black, x + 200, y);
        //    y = y + 30; y1 = y + 18;
        //    e.Graphics.DrawString(title3, tagfont, Brushes.Black, x + 200, y);

        //    y = y + 50; y1 = y + 15;
        //    e.Graphics.DrawString("姓名: " + tbPatname.Text, textfront, Brushes.Black, x + 20, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 51, y1, x + 150, y1);
        //    //为空画斜杠
        //    if (tbPatname.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(90 + x, y + 13), new Point(110 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("性别: " + tbSex.Text, textfront, Brushes.Black, x + 149, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 179, y1, x + 260, y1);
        //    //为空画斜杠
        //    if (tbSex.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(220 + x, y + 13), new Point(240 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("年龄: " + tbAge.Text, textfront, Brushes.Black, x + 265, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 294, y1, x + 370, y1);
        //    //为空画斜杠
        //    if (tbAge.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(330 + x, y + 13), new Point(350 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("床号: " + tbBedNO.Text, textfront, Brushes.Black, x + 376, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 403, y1, x + 480, y1);
        //    //为空画斜杠
        //    if (tbPatname.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(440 + x, y + 13), new Point(460 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("日期: " + dtVisitDate.Text, textfront, Brushes.Black, x + 488, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 516, y1, x + 638, y1);
        //    //为空画斜杠
        //    if (dtVisitDate.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(550 + x, y + 13), new Point(570 + x, y + 2));
        //    }
        //    y = y + 35; y1 = y + 15;
        //    e.Graphics.DrawString("麻醉方法: " + cmbMZFF.Text, textfront, Brushes.Black, x + 20, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 82, y1, x + 230, y1);
        //    //为空画斜杠
        //    if (cmbMZFF.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(100 + x, y + 13), new Point(120 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("手术开始时间: " + dtEnterTime.Text, textfront, Brushes.Black, x + 230, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 316, y1, x + 430, y1);
        //    //为空画斜杠
        //    if (dtEnterTime.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(350 + x, y + 13), new Point(370 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("手术结束时间: " + dtLeaveTime.Text, textfront, Brushes.Black, x + 436, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 526, y1, x + 638, y1);
        //    //为空画斜杠
        //    if (dtLeaveTime.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(560 + x, y + 13), new Point(540 + x, y + 2));
        //    }


        //    /*
             
        //     */
        //    y = y + 35; y1 = y + 15;
        //    e.Graphics.DrawString("术中处理:", textfront, Brushes.Black, x + 20, y);
        //    y = y + 35; y1 = y + 15;
        //    e.Graphics.DrawString("静脉通路位置：" + cmbJingmaitonglu.Text, textfront, Brushes.Black, x + 20, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 114, y1, x + 280, y1);
        //    //为空画斜杠
        //    if (cmbJingmaitonglu.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("保暖方法：" + cmbBaonuan.Text, textfront, Brushes.Black, x + 360, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 420, y1, x + 640, y1);
        //    //为空画斜杠
        //    if (cmbBaonuan.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(460 + x, y + 13), new Point(480 + x, y + 2));
        //    }
        //    y = y + 35; y1 = y + 15;
        //    e.Graphics.DrawString("手术体位：" + cmbTiwei.Text, textfront, Brushes.Black, x + 20, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 88, y1, x + 292, y1);
        //    //为空画斜杠
        //    if (cmbTiwei.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("术中体位改变情况：" + tbTiweiGaibian.Text, textfront, Brushes.Black, x + 320, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 440, y1, x + 640, y1);
        //    //为空画斜杠
        //    if (tbTiweiGaibian.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(460 + x, y + 13), new Point(480 + x, y + 2));
        //    }
        //    y = y + 35; y1 = y + 15;
        //    e.Graphics.DrawString("电极板粘贴部位：" + cmbDianjiban.Text, textfront, Brushes.Black, x + 20, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 122, y1, x + 310, y1);
        //    //为空画斜杠
        //    if (cmbDianjiban.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(147 + x, y + 13), new Point(167 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("术前皮肤评估：" + cmbSQpifu.Text, textfront, Brushes.Black, x + 340, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 427, y1, x + 640, y1);
        //    //为空画斜杠
        //    if (cmbSQpifu.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(460 + x, y + 13), new Point(480 + x, y + 2));
        //    }
        //    y = y + 35; y1 = y + 35;

        //    #region// 画器械表         
        //    e.Graphics.DrawLine(ptp, new Point(20 + x, y), new Point(680 + x, y));
        //    e.Graphics.DrawLine(ptp, new Point(20 + x, y), new Point(20 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
        //    e.Graphics.DrawLine(ptp, new Point(140 + x, y), new Point(140 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
        //    e.Graphics.DrawLine(ptp, new Point(210 + x, y), new Point(210 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
        //    e.Graphics.DrawLine(ptp, new Point(280 + x, y), new Point(280 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
        //    e.Graphics.DrawLine(ptp, new Point(350 + x, y), new Point(350 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
        //    e.Graphics.DrawLine(ptp, new Point(470 + x, y), new Point(470 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
        //    e.Graphics.DrawLine(ptp, new Point(540 + x, y), new Point(540 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
        //    e.Graphics.DrawLine(ptp, new Point(610 + x, y), new Point(610 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
        //    e.Graphics.DrawLine(ptp, new Point(680 + x, y), new Point(680 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
            
        //    e.Graphics.DrawLine(ptp, new Point(20 + x, y), new Point(20 + x, y+25*(dgvNurseRecord.Columns.Count+1)));
        //    e.Graphics.DrawString(dgvNurseRecord.Columns[0].HeaderText, textfront, Brushes.Black, x + 25, y + 3);
        //    e.Graphics.DrawString(dgvNurseRecord.Columns[1].HeaderText, textfront, Brushes.Black, x + 145, y + 3);
        //    e.Graphics.DrawString(dgvNurseRecord.Columns[2].HeaderText, textfront, Brushes.Black, x + 215, y + 3);
        //    e.Graphics.DrawString(dgvNurseRecord.Columns[3].HeaderText, textfront, Brushes.Black, x + 285, y + 3);
        //    e.Graphics.DrawString(dgvNurseRecord.Columns[4].HeaderText, textfront, Brushes.Black, x + 355, y + 3);
        //    e.Graphics.DrawString(dgvNurseRecord.Columns[5].HeaderText, textfront, Brushes.Black, x + 475, y + 3);
        //    e.Graphics.DrawString(dgvNurseRecord.Columns[6].HeaderText, textfront, Brushes.Black, x + 545, y + 3);
        //    e.Graphics.DrawString(dgvNurseRecord.Columns[7].HeaderText, textfront, Brushes.Black, x + 605, y + 3);
           
        //    y = y + 25; y1 = y + 25;
        //    for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < 8; j++)
        //        {
        //            if (dgvNurseRecord.Rows[i].Cells[j].Value == null)
        //                dgvNurseRecord.Rows[i].Cells[j].Value = "";
        //        }
        //        e.Graphics.DrawLine(Pens.Black, x + 20, y + 25 * i, x + 680, y + 25 * i);
        //        e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[0].Value.ToString(), textfront, Brushes.Black, x + 25, y + 3 + i * 25);
        //        e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[1].Value.ToString(), textfront, Brushes.Black, x + 170, y + 3 + i * 25);
        //        e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[2].Value.ToString(), textfront, Brushes.Black, x + 240, y + 3 + i * 25);
        //        e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[3].Value.ToString(), textfront, Brushes.Black, x + 310, y + 3 + i * 25);
        //        e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[4].Value.ToString(), textfront, Brushes.Black, x + 355, y + 3 + i * 25);
        //        e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[5].Value.ToString(), textfront, Brushes.Black, x + 500, y + 3 + i * 25);
        //        e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[6].Value.ToString(), textfront, Brushes.Black, x + 570, y + 3 + i * 25);
        //        e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[7].Value.ToString(), textfront, Brushes.Black, x + 640, y + 3 + i * 25);

        //    }
        //    e.Graphics.DrawLine(Pens.Black, x + 20, y + 25 * dgvNurseRecord.Rows.Count, x + 680, y + 25 * dgvNurseRecord.Rows.Count);
        //    #endregion

        //    y = y + 35+dgvNurseRecord.Rows.Count*25; y1 = y + 15;
        //    e.Graphics.DrawString("术后皮肤评估：" + cmbSHpifu.Text, textfront, Brushes.Black, x + 20, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 110, y1, x + 220, y1);
        //    //为空画斜杠
        //    if (cmbSQpifu.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(130 + x, y + 13), new Point(150 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("带至复苏室血制品：" + tbFusushiXZP.Text, textfront, Brushes.Black, x + 240, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 357, y1, x + 402, y1);
        //    //为空画斜杠
        //    if (tbFusushiXZP.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(370 + x, y + 13), new Point(390 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("袋，带至病房血制品：" + tbFusushiXZP.Text, textfront, Brushes.Black, x + 400, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 540, y1, x + 620, y1);
        //    //为空画斜杠
        //    if (tbFusushiXZP.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(560 + x, y + 13), new Point(580 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("袋", textfront, Brushes.Black, x + 620, y);
        //    y = y + 35; y1 = y + 15;
        //    e.Graphics.DrawString("术后引流：" + cmbYinliu.Text, textfront, Brushes.Black, x + 20, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 84, y1, x + 180, y1);
        //    //为空画斜杠
        //    if (cmbYinliu.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));
        //    }
        //    e.Graphics.DrawString("植入性产品：" + tbZhiruwu.Text, textfront, Brushes.Black, x + 240, y);
        //    e.Graphics.DrawLine(Pens.Black, x + 319, y1, x + 580, y1);
        //    //为空画斜杠
        //    if (tbZhiruwu.Text == "")
        //    {
        //        e.Graphics.DrawLine(ptp, new Point(360 + x, y + 13), new Point(380 + x, y + 2));
        //    }

        //    //多出来的显示进行注释

        //    y = y + 35; y1 = y + 15;
        //    e.Graphics.DrawLine(Pens.Black, x, y, x + 700, y);
        //    e.Graphics.DrawLine(Pens.Black, x, y + 70, x + 700, y + 70);
        //    e.Graphics.DrawLine(Pens.Black, x, y, x, y + 70);
        //    e.Graphics.DrawLine(Pens.Black, x + 90, y, x + 90, y + 70);
        //    e.Graphics.DrawLine(Pens.Black, x + 230, y, x + 230, y + 70);
        //    e.Graphics.DrawLine(Pens.Black, x + 310, y, x + 310, y + 70);
        //    e.Graphics.DrawLine(Pens.Black, x + 440, y, x + 440, y + 70);
        //    e.Graphics.DrawLine(Pens.Black, x + 560, y, x + 560, y + 70);
        //    e.Graphics.DrawLine(Pens.Black, x + 650, y, x + 650, y + 70);
        //    y = y + 5; y1 = y + 15;
        //    e.Graphics.DrawString("手术医生", textfront, Brushes.Black, x + 5, y);
        //    e.Graphics.DrawString("洗手护士", textfront, Brushes.Black, x + 95, y);
        //    e.Graphics.DrawString("巡回医生", textfront, Brushes.Black, x + 315, y);
        //    #endregion
        //}
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Pen ptp = Pens.Black;//普通画笔          
            #region 2014/03/18修改后的代码
            Font textfront = new Font("宋体", 8);
            Font tagfont = new Font("宋体", 13);
            Pen pblack = Pens.Black;

            int x = 25, y = 20, y1 = y + 70;
            string title1 = "江  苏  盛  泽  医  院";
            string title2 = "江苏省人民医院盛泽分院";
            string title3 = "      术后访视单";
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title1, tagfont, Brushes.Black, x + 220, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title2, tagfont, Brushes.Black, x + 220, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title3, tagfont, Brushes.Black, x + 220, y);

            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("姓名: " + tbPatname.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 47, y1, x + 150, y1);
            //为空画斜杠
            if (tbPatname.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(90 + x, y + 13), new Point(110 + x, y + 2));
            }
            e.Graphics.DrawString("性别: " + tbSex.Text, textfront, Brushes.Black, x + 149, y);
            e.Graphics.DrawLine(Pens.Black, x + 175, y1, x + 260, y1);
            //为空画斜杠
            if (tbSex.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(220 + x, y + 13), new Point(240 + x, y + 2));
            }
            e.Graphics.DrawString("年龄: " + tbAge.Text, textfront, Brushes.Black, x + 265, y);
            e.Graphics.DrawLine(Pens.Black, x + 291, y1, x + 370, y1);
            //为空画斜杠
            if (tbAge.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(330 + x, y + 13), new Point(350 + x, y + 2));
            }
            e.Graphics.DrawString("床号: " + tbBedNO.Text, textfront, Brushes.Black, x + 376, y);
            e.Graphics.DrawLine(Pens.Black, x + 403, y1, x + 480, y1);
            //为空画斜杠
            if (tbPatname.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(440 + x, y + 13), new Point(460 + x, y + 2));
            }
            e.Graphics.DrawString("日期: " + dtVisitDate.Text, textfront, Brushes.Black, x + 488, y);
            e.Graphics.DrawLine(Pens.Black, x + 516, y1, x + 638, y1);
            //为空画斜杠
            if (dtVisitDate.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(550 + x, y + 13), new Point(570 + x, y + 2));
            }
            y = y + 35; y1 = y + 15;
            e.Graphics.DrawString("麻醉方法: " + cmbMZFF.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 74, y1, x + 230, y1);
            //为空画斜杠
            if (cmbMZFF.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(100 + x, y + 13), new Point(120 + x, y + 2));
            }
            e.Graphics.DrawString("手术开始时间: " + dtEnterTime.Text, textfront, Brushes.Black, x + 230, y);
            e.Graphics.DrawLine(Pens.Black, x + 305, y1, x + 430, y1);
            //为空画斜杠
            if (dtEnterTime.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(350 + x, y + 13), new Point(370 + x, y + 2));
            }
            e.Graphics.DrawString("手术结束时间: " + dtLeaveTime.Text, textfront, Brushes.Black, x + 436, y);
            e.Graphics.DrawLine(Pens.Black, x + 512, y1, x + 638, y1);
            //为空画斜杠
            if (dtLeaveTime.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(560 + x, y + 13), new Point(540 + x, y + 2));
            }


            /*
             
             */
            y = y + 35; y1 = y + 15;
            e.Graphics.DrawString("术中处理:", textfront, Brushes.Black, x + 20, y);
            y = y + 35; y1 = y + 15;
            e.Graphics.DrawString("静脉通路位置：" + cmbShenzhi.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 90, y1, x + 280, y1);
            //为空画斜杠
            if (cmbShenzhi.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
            }
            e.Graphics.DrawString("保暖方法：" + cmbJMCC.Text, textfront, Brushes.Black, x + 360, y);
            e.Graphics.DrawLine(Pens.Black, x + 405, y1, x + 640, y1);
            //为空画斜杠
            if (cmbJMCC.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(460 + x, y + 13), new Point(480 + x, y + 2));
            }
            y = y + 35; y1 = y + 15;
            e.Graphics.DrawString("手术体位：" + cmbDaoniao.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 68, y1, x + 292, y1);
            //为空画斜杠
            if (cmbDaoniao.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
            }
            e.Graphics.DrawString("术中体位改变情况：" + tbTiweiGaibian.Text, textfront, Brushes.Black, x + 320, y);
            e.Graphics.DrawLine(Pens.Black, x + 415, y1, x + 640, y1);
            //为空画斜杠
            if (tbTiweiGaibian.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(460 + x, y + 13), new Point(480 + x, y + 2));
            }
            y = y + 35; y1 = y + 15;
            e.Graphics.DrawString("电极板粘贴部位：" + cmbPifuRS.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 105, y1, x + 310, y1);
            //为空画斜杠
            if (cmbPifuRS.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(147 + x, y + 13), new Point(167 + x, y + 2));
            }
            e.Graphics.DrawString("术前皮肤评估：" + cmbTiwei.Text, textfront, Brushes.Black, x + 340, y);
            e.Graphics.DrawLine(Pens.Black, x + 412, y1, x + 640, y1);
            //为空画斜杠
            if (cmbTiwei.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(460 + x, y + 13), new Point(480 + x, y + 2));
            }
            y = y + 35; y1 = y + 35;

            #region// 画器械表
            e.Graphics.DrawLine(ptp, new Point(20 + x, y), new Point(680 + x, y));
            e.Graphics.DrawLine(ptp, new Point(20 + x, y), new Point(20 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(140 + x, y), new Point(140 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(210 + x, y), new Point(210 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(280 + x, y), new Point(280 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(350 + x, y), new Point(350 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(470 + x, y), new Point(470 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(540 + x, y), new Point(540 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(610 + x, y), new Point(610 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(680 + x, y), new Point(680 + x, y + 25 * (dgvNurseRecord.Rows.Count + 1)));

            e.Graphics.DrawLine(ptp, new Point(20 + x, y), new Point(20 + x, y + 25 * (dgvNurseRecord.Columns.Count + 1)));
            e.Graphics.DrawString(dgvNurseRecord.Columns[0].HeaderText, textfront, Brushes.Black, x + 25, y + 3);
            e.Graphics.DrawString(dgvNurseRecord.Columns[1].HeaderText, textfront, Brushes.Black, x + 145, y + 3);
            e.Graphics.DrawString(dgvNurseRecord.Columns[2].HeaderText, textfront, Brushes.Black, x + 215, y + 3);
            e.Graphics.DrawString(dgvNurseRecord.Columns[3].HeaderText, textfront, Brushes.Black, x + 285, y + 3);
            e.Graphics.DrawString(dgvNurseRecord.Columns[4].HeaderText, textfront, Brushes.Black, x + 355, y + 3);
            e.Graphics.DrawString(dgvNurseRecord.Columns[5].HeaderText, textfront, Brushes.Black, x + 475, y + 3);
            e.Graphics.DrawString(dgvNurseRecord.Columns[6].HeaderText, textfront, Brushes.Black, x + 545, y + 3);
            e.Graphics.DrawString(dgvNurseRecord.Columns[7].HeaderText, textfront, Brushes.Black, x + 605, y + 3);

            y = y + 25; y1 = y + 25;
            for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (dgvNurseRecord.Rows[i].Cells[j].Value == null)
                        dgvNurseRecord.Rows[i].Cells[j].Value = "";
                }
                e.Graphics.DrawLine(Pens.Black, x + 20, y + 25 * i, x + 680, y + 25 * i);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[0].Value.ToString(), textfront, Brushes.Black, x + 25, y + 3 + i * 25);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[1].Value.ToString(), textfront, Brushes.Black, x + 170, y + 3 + i * 25);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[2].Value.ToString(), textfront, Brushes.Black, x + 240, y + 3 + i * 25);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[3].Value.ToString(), textfront, Brushes.Black, x + 310, y + 3 + i * 25);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[4].Value.ToString(), textfront, Brushes.Black, x + 355, y + 3 + i * 25);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[5].Value.ToString(), textfront, Brushes.Black, x + 500, y + 3 + i * 25);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[6].Value.ToString(), textfront, Brushes.Black, x + 570, y + 3 + i * 25);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[7].Value.ToString(), textfront, Brushes.Black, x + 640, y + 3 + i * 25);

            }
            e.Graphics.DrawLine(Pens.Black, x + 20, y + 25 * dgvNurseRecord.Rows.Count, x + 680, y + 25 * dgvNurseRecord.Rows.Count);
            #endregion

            y = y + 35 + dgvNurseRecord.Rows.Count * 25; y1 = y + 15;
            e.Graphics.DrawString("术后皮肤评估：" + cmbSHpifu.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 90, y1, x + 220, y1);
            //为空画斜杠
            if (cmbTiwei.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(130 + x, y + 13), new Point(150 + x, y + 2));
            }
            e.Graphics.DrawString("带至复苏室血制品：" + tbFusushiXZP.Text, textfront, Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 337, y1, x + 400, y1);
            //为空画斜杠
            if (tbFusushiXZP.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(350 + x, y + 13), new Point(370 + x, y + 2));
            }
            e.Graphics.DrawString("袋，带至病房血制品：" + tbFusushiXZP.Text, textfront, Brushes.Black, x + 400, y);
            e.Graphics.DrawLine(Pens.Black, x + 500, y1, x + 620, y1);
            //为空画斜杠
            if (tbFusushiXZP.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(520 + x, y + 13), new Point(540 + x, y + 2));
            }
            e.Graphics.DrawString("袋", textfront, Brushes.Black, x + 620, y);
            y = y + 35; y1 = y + 15;
            e.Graphics.DrawString("术后引流：" + cmbYinliu.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 180, y1);
            //为空画斜杠
            if (cmbYinliu.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(110 + x, y + 13), new Point(130 + x, y + 2));
            }
            e.Graphics.DrawString("植入性产品：" + tbZhiruwu.Text, textfront, Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 300, y1, x + 580, y1);
            //为空画斜杠
            if (tbZhiruwu.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(339 + x, y + 13), new Point(359 + x, y + 2));
            }
            y = y + 45; y1 = y + 15;
            e.Graphics.DrawString("（手术医生签名）", textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawString("术前：" + cmbSSYSqian.Text, textfront, Brushes.Black, x + 103, y);
            e.Graphics.DrawLine(Pens.Black, x + 130, y1, x + 280, y1);
            //为空画斜杠
            if (cmbSSYSqian.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
            }
            e.Graphics.DrawString("术后：" + cmbSSYShou.Text, textfront, Brushes.Black, x + 365, y);
            e.Graphics.DrawLine(Pens.Black, x + 392, y1, x + 590, y1);
            //为空画斜杠
            if (cmbSSYShou.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(415 + x, y + 13), new Point(435 + x, y + 2));
            }
            y = y + 55; y1 = y + 15;
            e.Graphics.DrawString("(洗手护士签名)", textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawString("术前：" + cmbXSHSqian.Text, textfront, Brushes.Black, x + 100, y);
            e.Graphics.DrawLine(Pens.Black, x + 127, y1, x + 280, y1);
            //为空画斜杠
            if (cmbXSHSqian.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
            }
            e.Graphics.DrawString("术后：" + cmbXSHShou.Text, textfront, Brushes.Black, x + 365, y);
            e.Graphics.DrawLine(Pens.Black, x + 392, y1, x + 590, y1);
            //为空画斜杠
            if (cmbXSHShou.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(415 + x, y + 13), new Point(435 + x, y + 2));
            }
            y = y + 65; y1 = y + 15;
            e.Graphics.DrawString("(巡回护士签名)", textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawString("术前：" + cmbXHHSqian.Text, textfront, Brushes.Black, x + 100, y);
            e.Graphics.DrawLine(Pens.Black, x + 127, y1, x + 280, y1);
            //为空画斜杠
            if (cmbXHHSqian.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
            }
            e.Graphics.DrawString("术后：" + cmbXHHShou.Text, textfront, Brushes.Black, x + 365, y);
            e.Graphics.DrawLine(Pens.Black, x + 392, y1, x + 590, y1);
            //为空画斜杠
            if (cmbXHHShou.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(415 + x, y + 13), new Point(435 + x, y + 2));
            }
            #endregion
        }



        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_SZ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SAVE();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_SZ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_SZ_FormClosing);
                    this.Close();
                }

            }
        }

        private void cunDangStripMenuItem_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetNurseRecord_SZ(PATID);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("术前访视信息需要先保存到数据库，才能存档！");
            }
            else
            {
                result = DAL.UpdateNurseRecord_SZ_STATE(PATID);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    SaveToolStripMenuItem.Enabled = false;
                }
            }
        }


        private void Bind_comboBox1()
        {
            comboBox1.Items.Clear();
            DataTable dt = DAL.GetallQxModel();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][0]);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = DAL.SlectqxModel(textBox1.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("器械模板已存在，请检查！");
            }
            else
            {
                int j = 0, k = 0, flag = 0;
                string qxmbm = textBox1.Text.Trim();
                try
                {
                    for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
                    {
                        if (dgvNurseRecord.Rows[i].Cells[0].Value != null)
                        {
                            string qzmz = dgvNurseRecord.Rows[i].Cells[0].Value.ToString();
                            int qxCount = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[1].Value);
                            flag = DAL.InsertqxqdModel(qzmz, qxmbm, qxCount);
                            j++;

                        }

                    }
                    for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
                    {
                        if (dgvNurseRecord.Rows[i].Cells[4].Value != null)
                        {
                            string qzmz = dgvNurseRecord.Rows[i].Cells[4].Value.ToString();
                            int qxCount = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[5].Value);
                            flag = DAL.InsertqxqdModel(qzmz, qxmbm, qxCount);
                            k++;
                        }
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("添加模板失败，请重新添加！");

                }
                if (j > 0 || k > 0)
                {
                    MessageBox.Show("添加模板成功！");
                }

                Bind_comboBox1();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string jinggao = "";
            int flag = 0;//清点成功标志
            for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
            {
                for (int j = 0; j < dgvNurseRecord.Columns.Count; j++)
                {
                    dgvNurseRecord.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                    dgvNurseRecord.Rows[i].Cells[4].Style.ForeColor = Color.Black;
                }
                int a1 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[1].Value);
                int a2 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[2].Value);
                int a3 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[3].Value);
                //int a4 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[4].Value);
                int a5 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[5].Value);
                int a6 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[6].Value);
                int a7 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[7].Value);
                //int a8 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[9].Value);
                if (a1 != a2 && a1 != a3)
                {
                    flag++;
                    jinggao = jinggao + dgvNurseRecord.Rows[i].Cells[0].Value.ToString() + "\n";
                    dgvNurseRecord.Rows[i].Cells[0].Style.ForeColor = Color.Red;                    
                }
                else if (a5 != a6 && a5 != a7)
                {
                    flag++;
                    jinggao = jinggao + dgvNurseRecord.Rows[i].Cells[4].Value.ToString() + "\n";
                    dgvNurseRecord.Rows[i].Cells[4].Style.ForeColor = Color.Red;
                }
            }
            if (flag == 0)
                MessageBox.Show("清点成功！");
            else
                MessageBox.Show(jinggao+"数量不正确");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = DAL.SelectqxmcInmodel(comboBox1.Text);
            int j = dt.Rows.Count / 2;
            dgvNurseRecord.Rows.Clear();
            dgvNurseRecord.Rows.Add(j + 1);
            for (int i = 0; i < j; i++)
            {
                dgvNurseRecord.Rows[i].Cells[0].Value = dt.Rows[i][0];
                dgvNurseRecord.Rows[i].Cells[1].Value = dt.Rows[i][1];

            }
            for (int i = 0; i < dt.Rows.Count - j; i++)
            {
                dgvNurseRecord.Rows[i].Cells[4].Value = dt.Rows[i + j][0];
                dgvNurseRecord.Rows[i].Cells[5].Value = dt.Rows[i + j][1];
            }
        }

        private void NurseRecord_SZ_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_SZ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SAVE();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_SZ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_SZ_FormClosing);
                    this.Close();
                }

            }
        }
    }
}
