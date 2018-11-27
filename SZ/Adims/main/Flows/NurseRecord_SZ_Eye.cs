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
    public partial class NurseRecord_SZ_Eye : Form
    {
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();

        adims_DAL.GysHelp gysHelp = new adims_DAL.GysHelp();
        BardCodeHooK BarCode = new BardCodeHooK();
        string PATID, MZID;
        public NurseRecord_SZ_Eye(string patid, string mzid)
        {
            PATID = patid;
            MZID = mzid;
            InitializeComponent();
        }
        public NurseRecord_SZ_Eye()
        {

            InitializeComponent();
        }
        int BCCount = 0;

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            SaveNurseRecord_QXQD();
            SaveNurseRecord();
        }
        private void SaveNurseRecord_QXQD()//保存方法
        {
            try
            {
                DAL.DeleteNurseRecord_SZ_QXQD(PATID);
                for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (dgvNurseRecord.Rows[i].Cells[j].Value == null)
                        {
                            dgvNurseRecord.Rows[i].Cells[j].Value = "";
                        }
                    }
                    DataGridViewRow drow = dgvNurseRecord.Rows[i];
                    DAL.InsertNurseRecord_SZ_QXQD(PATID, drow.Cells[0].Value.ToString(), drow.Cells[1].Value.ToString(),
                        drow.Cells[2].Value.ToString(), drow.Cells[3].Value.ToString());
                    DAL.InsertNurseRecord_SZ_QXQD(PATID, drow.Cells[4].Value.ToString(), drow.Cells[5].Value.ToString(),
                       drow.Cells[6].Value.ToString(), drow.Cells[7].Value.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("器械清点数据保存失败");
            }
            
        }
        private void SaveNurseRecord()//保存方法
        {
            Dictionary<string, string> A_VISIT = new Dictionary<string, string>();
            int result = 0;
            try
            {
                A_VISIT.Add("Jingmaitonglu", cmbJingmaitonglu.Text);
                A_VISIT.Add("Baonuan", cmbBaonuan.Text);
                A_VISIT.Add("Tiwei", cmbTiwei.Text);
                A_VISIT.Add("Dianjiban", tbSZXY.Text);
                A_VISIT.Add("TiweiGaibian", tbXinlv.Text);
                A_VISIT.Add("SQpifu",this.tbSZXY.Text);
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
                A_VISIT.Add("SHHL", this.txtSSHL.Text);
                A_VISIT.Add("BB", this.txtBB.Text);
                A_VISIT.Add("BefWaterlow", this.nupBefWaterlow.Text);
                A_VISIT.Add("AfterWaterlow", this.nupAfterWaterlow.Text);
                A_VISIT.Add("QmDoctor", this.tbQmDoctor.Text);
                result = DAL.UpdateNurseRecord_SZ(A_VISIT);
                if (result > 0)
                {
                    MessageBox.Show("基本信息保存成功！");
                    BCCount++;
                }
                else
                    MessageBox.Show("基本信息保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }
        private void BindNurseRecord()
        {
            DataTable dt = DAL.GetNurseRecord_SZ(PATID);
            if (dt.Rows.Count>0)
            {
           
            cmbJingmaitonglu.Text = dt.Rows[0]["Jingmaitonglu"].ToString() ;
            cmbBaonuan.Text=dt.Rows[0]["Baonuan"].ToString() ;
             cmbTiwei.Text=dt.Rows[0]["Tiwei"].ToString() ;
             tbSZXY.Text=dt.Rows[0]["Dianjiban"].ToString() ;
             tbXinlv.Text=dt.Rows[0]["TiweiGaibian"].ToString() ;
             this.tbSZXY.Text=dt.Rows[0]["SQpifu"].ToString() ;
             cmbSHpifu.Text=dt.Rows[0]["SHpifu"].ToString() ;
             tbFusushiXZP.Text=dt.Rows[0]["FusushiXZP"].ToString() ;
             tbBingfangXZP.Text=dt.Rows[0]["BingfangXZP"].ToString() ;
             cmbYinliu.Text=dt.Rows[0]["Yinliu"].ToString() ;
             tbZhiruwu.Text=dt.Rows[0]["Zhiruwu"].ToString() ;
             tbRemark.Text=dt.Rows[0]["Remark"].ToString() ;
             cmbSSYSqian.Text=dt.Rows[0]["SSYSqian"].ToString() ;
             cmbSSYShou.Text=dt.Rows[0]["SSYShou"].ToString() ;
             cmbXSHSqian.Text=dt.Rows[0]["XSHSqian"].ToString() ;
             cmbXSHShou.Text=dt.Rows[0]["XSHShou"].ToString() ;
             cmbXHHSqian.Text=dt.Rows[0]["XHHSqian"].ToString() ;
             cmbXHHShou.Text=dt.Rows[0]["XHHShou"].ToString() ;
             dtVisitDate.Value = Convert.ToDateTime(dt.Rows[0]["VisitDate"].ToString());
             this.txtSSHL.Text=dt.Rows[0]["SHHL"].ToString() ;
             this.txtBB.Text=dt.Rows[0]["BB"].ToString() ;
             this.nupBefWaterlow.Text=dt.Rows[0]["BefWaterlow"].ToString() ;
             this.nupAfterWaterlow.Text=dt.Rows[0]["AfterWaterlow"].ToString() ;
             this.tbQmDoctor.Text = dt.Rows[0]["QmDoctor"].ToString();

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
            cmbXHHShou.Items.Clear();

            DataTable dt = new DataTable();
            dt = DAL.GetAll_hushi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbXSHSqian.Items.Add(dt.Rows[i][0]);
                cmbXSHShou.Items.Add(dt.Rows[i][0]);
                cmbXHHSqian.Items.Add(dt.Rows[i][0]);
                cmbXHHShou.Items.Add(dt.Rows[i][0]);

            }
        }

        //private void BindCombox2()
        //{
        //    cmbSSYSqian.Items.Clear();
        //    cmbSSYShou.Items.Clear();

        //    DataTable dt = new DataTable();
        //    dt = DAL.GetAllMZYS();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        cmbSSYSqian.Items.Add(dt.Rows[i][0]);
        //        cmbSSYShou.Items.Add(dt.Rows[i][0]);
        //    }
        //}
        private void DataGridBind()
        {
            string kongge = "";
            for (int i = 0; i < 20; i++)
            {
                dgvNurseRecord.Rows[i].Cells[0].Value = kongge;
                dgvNurseRecord.Rows[i].Cells[5].Value = kongge;

            }



        }

     

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);

        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Pen ptp = Pens.Black;//普通画笔   
            Font textfront = new Font("宋体", 8);
            Font heti = new Font("黑体", 8);
            Font tagfont = new Font("宋体", 13);
            Pen pblack = Pens.Black;

            #region
           
            int x = 25, y = 30, y1 = y + 70;
            string title1 = "江  苏  盛  泽  医  院";
            string title2 = "江苏省人民医院盛泽分院";
            string title3 = "    手术护理记录单";
           
            e.Graphics.DrawString(title1, tagfont, Brushes.Black, x + 220, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title2, tagfont, Brushes.Black, x + 220, y);
            y = y + 30; y1 = y + 18;
            e.Graphics.DrawString(title3, tagfont, Brushes.Black, x + 220, y);

            y = y + 40; y1 = y + 15;
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
            y = y + 25; y1 = y + 15;
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
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("诊断:" + tbSZZD.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 50, y1, x + 300, y1);
            //为空画斜杠
            if (tbSZZD.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
            }
            e.Graphics.DrawString("手术名称:" + tbShoushuName.Text, textfront, Brushes.Black, x + 340, y);
            e.Graphics.DrawLine(Pens.Black, x + 400, y1, x + 640, y1);
            //为空画斜杠
            if (tbShoushuName.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(450 + x, y + 13), new Point(470 + x, y + 2));
            }
          
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("术中护理:" + txtSSHL.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 75, y1, x + 640, y1);
            //为空画斜杠
            if (txtSSHL.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("静脉通路位置： " + cmbJingmaitonglu.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 90, y1, x + 300, y1);
            //为空画斜杠
            if (cmbJingmaitonglu.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
            }
            e.Graphics.DrawString("保暖方法：" + cmbBaonuan.Text, textfront, Brushes.Black, x + 360, y);
            e.Graphics.DrawLine(Pens.Black, x + 408, y1, x + 640, y1);
            //为空画斜杠
            if (cmbBaonuan.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(460 + x, y + 13), new Point(480 + x, y + 2));
            }
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("手术体位：" + cmbTiwei.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 68, y1, x + 300, y1);
            //为空画斜杠
            if (cmbTiwei.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(150 + x, y + 13), new Point(170 + x, y + 2));
            }
            
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("术中血压：" + tbSZXY.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 70, y1, x + 200, y1);
            e.Graphics.DrawString("mmHg", textfront, Brushes.Black, x + 200, y);
            //为空画斜杠
            if (tbSZXY.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(127 + x, y + 13), new Point(147 + x, y + 2));
            }
            e.Graphics.DrawString("心率：" + tbXinlv.Text, textfront, Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 270, y1, x + 370, y1);
            e.Graphics.DrawString("次/分", textfront, Brushes.Black, x + 370, y);
            //为空画斜杠
            if (tbXinlv.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(300 + x, y + 13), new Point(320 + x, y + 2));
            }
          
            e.Graphics.DrawString("脉氧：" + tbMaiYang.Text, textfront, Brushes.Black, x + 420, y);
            e.Graphics.DrawLine(Pens.Black, x + 450, y1, x + 550, y1);
            e.Graphics.DrawString("%", textfront, Brushes.Black, x + 550, y);
            //为空画斜杠
            if (tbMaiYang.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(480 + x, y + 13), new Point(520 + x, y + 2));
            }
          
            y = y + 25; y1 = y + 35;

            #region// 画器械表
            e.Graphics.DrawLine(ptp, new Point(20 + x, y), new Point(680 + x, y));
            e.Graphics.DrawLine(ptp, new Point(20 + x, y), new Point(20 + x, y + 22 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(170 + x, y), new Point(170 + x, y + 22 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(230 + x, y), new Point(230 + x, y + 22 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(290 + x, y), new Point(290 + x, y + 22 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(350 + x, y), new Point(350 + x, y + 22 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(500 + x, y), new Point(500 + x, y + 22 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(560 + x, y), new Point(560 + x, y + 22 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(620 + x, y), new Point(620 + x, y + 22 * (dgvNurseRecord.Rows.Count + 1)));
            e.Graphics.DrawLine(ptp, new Point(680 + x, y), new Point(680 + x, y + 22 * (dgvNurseRecord.Rows.Count + 1)));
            //e.Graphics.DrawLine(ptp, new Point(20 + x, y), new Point(20 + x, y + 25 * (dgvNurseRecord.Columns.Count + 1)));
            e.Graphics.DrawString(dgvNurseRecord.Columns[0].HeaderText, heti, Brushes.Black, x + 55, y + 5);
            e.Graphics.DrawString(dgvNurseRecord.Columns[1].HeaderText, heti, Brushes.Black, x + 175, y + 5);
            e.Graphics.DrawString(dgvNurseRecord.Columns[2].HeaderText, heti, Brushes.Black, x + 235, y + 5);
            e.Graphics.DrawString(dgvNurseRecord.Columns[3].HeaderText, heti, Brushes.Black, x + 295, y + 5);
            e.Graphics.DrawString(dgvNurseRecord.Columns[4].HeaderText, heti, Brushes.Black, x + 375, y + 5);
            e.Graphics.DrawString(dgvNurseRecord.Columns[5].HeaderText, heti, Brushes.Black, x + 505, y + 5);
            e.Graphics.DrawString(dgvNurseRecord.Columns[6].HeaderText, heti, Brushes.Black, x + 565, y + 5);
            e.Graphics.DrawString(dgvNurseRecord.Columns[7].HeaderText, heti, Brushes.Black, x + 625, y + 5);

            y = y + 25; y1 = y + 25;
            for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (dgvNurseRecord.Rows[i].Cells[j].Value == null)
                        dgvNurseRecord.Rows[i].Cells[j].Value = "";
                }
                e.Graphics.DrawLine(Pens.Black, x + 20, y + 22 * i, x + 680, y + 22 * i);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[0].Value.ToString(), textfront, Brushes.Black, x + 25, y + 3 + i * 22);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[1].Value.ToString(), textfront, Brushes.Black, x + 195, y + 3 + i * 22);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[2].Value.ToString(), textfront, Brushes.Black, x + 255, y + 3 + i * 22);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[3].Value.ToString(), textfront, Brushes.Black, x + 315, y + 3 + i * 22);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[4].Value.ToString(), textfront, Brushes.Black, x + 355, y + 3 + i * 22);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[5].Value.ToString(), textfront, Brushes.Black, x + 525, y + 3 + i * 22);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[6].Value.ToString(), textfront, Brushes.Black, x + 585, y + 3 + i * 22);
                e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[7].Value.ToString(), textfront, Brushes.Black, x + 645, y + 3 + i * 22);

            }
            e.Graphics.DrawLine(Pens.Black, x + 20, y + 22 * dgvNurseRecord.Rows.Count, x + 680, y + 22 * dgvNurseRecord.Rows.Count);
            #endregion

            y = y + 10 + dgvNurseRecord.Rows.Count * 22; y1 = y + 15;
           
            e.Graphics.DrawString("植入性产品：" + tbZhiruwu.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 80, y1, x + 670, y1);
            //为空画斜杠
            if (tbZhiruwu.Text == "")
            {
                e.Graphics.DrawLine(ptp, new Point(80 + x, y + 13), new Point(120 + x, y + 2));
            }
           
            y = y + 25; y1 = y + 15;           
            e.Graphics.DrawString("手术医生术前：" + cmbSSYSqian.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 95, y1, x + 220, y1);
            e.Graphics.DrawString("洗手护士术前：" + cmbXSHSqian.Text, textfront, Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 315, y1, x + 440, y1);
            e.Graphics.DrawString("巡回护士术前：" + cmbXHHSqian.Text, textfront, Brushes.Black, x + 460, y);
            e.Graphics.DrawLine(Pens.Black, x + 535, y1, x + 670, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("手术医生术后：" + cmbSSYShou.Text, textfront, Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 95, y1, x + 220, y1);
            e.Graphics.DrawString("洗手护士术后：" + cmbXSHShou.Text, textfront, Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 315, y1, x + 440, y1);
            e.Graphics.DrawString("巡回护士术后：" + cmbXHHShou.Text, textfront, Brushes.Black, x + 460, y);
            e.Graphics.DrawLine(Pens.Black, x + 535, y1, x + 670, y1);

          

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
                    SaveNurseRecord();
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
            cmbQXMB.Items.Clear();
            DataTable dt = DAL.GetallQxModel();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbQXMB.Items.Add(dt.Rows[i][0]);
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
                int a1 = UserFunction.ToInt32(dgvNurseRecord.Rows[i].Cells[1].Value);
                int a2 = UserFunction.ToInt32(dgvNurseRecord.Rows[i].Cells[2].Value);
                int a3 = UserFunction.ToInt32(dgvNurseRecord.Rows[i].Cells[3].Value);
                int a5 = UserFunction.ToInt32(dgvNurseRecord.Rows[i].Cells[5].Value);
                int a6 = UserFunction.ToInt32(dgvNurseRecord.Rows[i].Cells[6].Value);
                int a7 = UserFunction.ToInt32(dgvNurseRecord.Rows[i].Cells[7].Value);
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
                MessageBox.Show(jinggao + "数量不正确");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = DAL.SelectqxmcInmodel(cmbQXMB.Text);
            int j = dt.Rows.Count / 2;
            int x = dt.Rows.Count;
            dgvNurseRecord.Rows.Clear();
            dgvNurseRecord.Rows.Add(j + 1);
            int rows = 0;
            for (int i = 0; i < x; i++)
            {
                if (i % 2 == 0)
                {
                    rows = i / 2;
                    dgvNurseRecord.Rows[rows].Cells[0].Value = dt.Rows[i][0];
                }
                else
                {
                    rows = i / 2 ;
                    dgvNurseRecord.Rows[rows].Cells[4].Value = dt.Rows[i][0];
                }
                
                //dgvNurseRecord.Rows[i].Cells[1].Value = dt.Rows[i][1];

            }
          
            //for (int i = 0; i < j; i++)
            //{
            //    dgvNurseRecord.Rows[i].Cells[0].Value = dt.Rows[i][0];
            //    //dgvNurseRecord.Rows[i].Cells[1].Value = dt.Rows[i][1];

            //}
            //for (int i = 0; i < dt.Rows.Count - j; i++)
            //{
            //    dgvNurseRecord.Rows[i].Cells[4].Value = dt.Rows[i + j][0];
            //    //dgvNurseRecord.Rows[i].Cells[5].Value = dt.Rows[i + j][1];
            //}
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
                    SaveNurseRecord();
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

        private void 添加三行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dgvNurseRecord.Rows.Add(1);
        }

        private void NurseRecord_SZ_Load(object sender, EventArgs e)
        {
           // dtEnterTime.Format = DateTimePickerFormat.Time;
            dtEnterTime.CustomFormat ="HH:mm";
            dtLeaveTime.CustomFormat = "HH:mm"; 
            DataTable dtNR = DAL.GetNurseRecord_SZ(PATID);
            if (dtNR.Rows.Count == 0)
            {
                DAL.InsertNurseRecord_SZ(PATID, DateTime.Now, 0);
            }
            else
            { 
                DataTable dt = DAL.GetNurseRecord_SZ_QXQD(PATID);
                int j = dt.Rows.Count / 2;
                int x = dt.Rows.Count;
                dgvNurseRecord.Rows.Clear();
                dgvNurseRecord.Rows.Add(j + 1);
                int rows = 0;
                for (int i = 0; i < x; i++)
                {
                    if (i % 2 == 0)
                    {
                        rows = i / 2;
                        dgvNurseRecord.Rows[rows].Cells[0].Value = dt.Rows[i][0];
                        dgvNurseRecord.Rows[rows].Cells[1].Value = dt.Rows[i][1];
                        dgvNurseRecord.Rows[rows].Cells[2].Value = dt.Rows[i][2];
                        dgvNurseRecord.Rows[rows].Cells[3].Value = dt.Rows[i][3];
                    }
                    else
                    {
                        rows = i / 2;
                        dgvNurseRecord.Rows[rows].Cells[4].Value = dt.Rows[i][0];
                        dgvNurseRecord.Rows[rows].Cells[5].Value = dt.Rows[i][1];
                        dgvNurseRecord.Rows[rows].Cells[6].Value = dt.Rows[i][2];
                        dgvNurseRecord.Rows[rows].Cells[7].Value = dt.Rows[i][3];
                    }
                  

                }
                //for (int i = 0; i < dt.Rows.Count - j; i++)
                //{
                //    dgvNurseRecord.Rows[i].Cells[4].Value = dt.Rows[i + j][0];
                //    dgvNurseRecord.Rows[i].Cells[5].Value = dt.Rows[i + j][1];
                //    dgvNurseRecord.Rows[i].Cells[6].Value = dt.Rows[i + j][2];
                //    dgvNurseRecord.Rows[i].Cells[7].Value = dt.Rows[i + j][3];
                //}
            }

           
            Bind_comboBox1();
            BindPatInfo();
            BindNurseRecord();
            BindCombox3();
           
            BarCode.BarCodeEvent += new BardCodeHooK.BardCodeDeletegate(BarCode_BarCodeEvent);
            BarCode.Start();
        }

        private void tbFusushiXZP_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e); 
        }

        private void tbBingfangXZP_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e); 
        }

        private void txtBB_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e); 
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
        
        private void BarCode_BarCodeEvent(BardCodeHooK.BarCodes barCode)
        {            
            ShowInfo(barCode);
        }
        private delegate void ShowInfoDelegate(BardCodeHooK.BarCodes barCode);

        string KeyName, VirtKey, ScanCode, Ascll, Chr, BarCode1;
        private void ShowInfo(BardCodeHooK.BarCodes barCode)
        {
            
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowInfoDelegate(ShowInfo), new object[] { barCode });
            }
            else
            {
                KeyName = barCode.KeyName;
                VirtKey = barCode.VirtKey.ToString();
                ScanCode = barCode.ScanCode.ToString();
                Ascll = barCode.Ascll.ToString();
                Chr = barCode.Chr.ToString();
                BarCode1 = barCode.IsValid ? barCode.BarCode : BarCode1;//是否为扫描枪输入，如果为true则是 否则为键盘输入
               
                
            }
        }
        
        private void NurseRecord_SZ_FormClosed(object sender, FormClosedEventArgs e)
        {
            BarCode.Stop();
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
                      
            if (e.KeyCode == Keys.Enter)
            {
               DataTable dtGYS= gysHelp.GetAdims_GongYS(txtBarCode.Text);
               if (dtGYS.Rows.Count > 0)
               {
                   int j = dtGYS.Rows.Count / 2;
                   dgvNurseRecord.Rows.Clear();
                   dgvNurseRecord.Rows.Add(j + 1);
                   for (int i = 0; i < j; i++)
                   {
                       dgvNurseRecord.Rows[i].Cells[0].Value = dtGYS.Rows[i][0];
                       dgvNurseRecord.Rows[i].Cells[1].Value = dtGYS.Rows[i][1];

                   }
                   for (int i = 0; i < dtGYS.Rows.Count - j; i++)
                   {
                       dgvNurseRecord.Rows[i].Cells[4].Value = dtGYS.Rows[i + j][0];
                       dgvNurseRecord.Rows[i].Cells[5].Value = dtGYS.Rows[i + j][1];
                   }
               }
               else
                   MessageBox.Show("条形码不正确");


              
               
            }
        }

        //C#中判断扫描枪输入与键盘输入 
        
        private DateTime _dt = DateTime.Now;

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
            TimeSpan ts = tempDt.Subtract(_dt);     //获取时间间隔
            if (ts.Milliseconds > 50)                           //判断时间间隔，如果时间间隔大于50毫秒，则将TextBox清空
                txtBarCode.Text = "";
            _dt = tempDt;
        }  //定义一个成员函数用于保存每次的时间点
       

     

    }
}
