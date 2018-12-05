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
using WindowsFormsControlLibrary5;
using adims_Utility;
using adims_DAL.Flows;

namespace main
{
    public partial class PaibanSG : Form
    {
        MzjldDal _MzjldDal = new MzjldDal();
        DB2help db2 = new DB2help();
        AdimsController bll = new AdimsController();
        AdimsProvider DAL = new AdimsProvider();
        adims_DAL.Flows.OperScheduleDal _PaibanDal = new adims_DAL.Flows.OperScheduleDal();
        adims_DAL.Dics.DataDicDal _DataDicDal = new adims_DAL.Dics.DataDicDal();
        public PaibanSG()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SAVE()
        {
            string str = "";
            Dictionary<string, string> beforeVisit = new Dictionary<string, string>();
            int result = 0;
            try
            {            
                beforeVisit.Add("txtPatid", linePatid.Text.Trim());
                beforeVisit.Add("zhuyuanid", linePatid.Text.Trim());
                beforeVisit.Add("txtPatname", lineName.Text);
                beforeVisit.Add("txtPatage", lineAge.Text);
                beforeVisit.Add("cmbSex", cmbSex.Text);
                beforeVisit.Add("keshi", this.cmbKeshi.Text);
                beforeVisit.Add("txtBednumber", lineBedno.Text);
                beforeVisit.Add("txtWeight", lineBedno.Text);
                beforeVisit.Add("txtHeight", lineBedno.Text);
                beforeVisit.Add("txtNssss", lineNSSSS.Text);
                beforeVisit.Add("txtSqzd", lineSQZD.Text);
                beforeVisit.Add("cmbOroom", cmbOroom.Text.Trim());
                beforeVisit.Add("txtSecond", lineSecond.Text.Trim());
                beforeVisit.Add("txtMZFF", cmbMZFF.Text);               
                beforeVisit.Add("txtMZYS", this.lineMZYS.Text);
                beforeVisit.Add("txtQXHS", this.lineQXHS.Text);
                beforeVisit.Add("txtXHHS", this.lineXHHS.Text);               
                beforeVisit.Add("dtOSdate", dtOSDate.Value.ToString("yyyy-MM-dd HH:mm"));
                beforeVisit.Add("OS", lineSSYS.Text);
                if (cbJizhen.Checked)
                    beforeVisit.Add("isjizhen", "1");
                else 
                    beforeVisit.Add("isjizhen", "0");
                beforeVisit.Add("startTime", dtOSDate.Value.ToString("HH:mm"));
                beforeVisit.Add("remarks", txtBZ.Text.Trim());
                beforeVisit.Add("GR", cmbGR.Text.Trim());
                beforeVisit.Add("tiwei", cmbTiwei.Text.Trim());
                beforeVisit.Add("nation", txtPatNation.Text.Trim());
                if (_PaibanDal.GetPaibanByPatId(linePatid.Text.Trim()).Rows.Count==0)
                    result = _PaibanDal.InsertPAIBAN(beforeVisit);
                else
                    result = _PaibanDal.UpdatePAIBAN(beforeVisit);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");                    
                    paibanDataBind();
                }
                else
                    MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (linePatid.Text.Trim() != "")
            {
                if (cmbOroom.Text.Trim() != "" && lineSecond.Text.Trim()!="")
                {
                    DataTable dt = _PaibanDal.GetPaibanByOroomAndTaici(cmbOroom.Text.Trim(), lineSecond.Text.Trim(), dtOSDate.Value, linePatid.Text.Trim());
                    if (dt.Rows.Count == 0)
                    {
                        SAVE();
                    }
                    else MessageBox.Show("此手术间当前台次已存在！");                          
                }
                else MessageBox.Show("手术间和手术台次不能为空！"); 
            }            
        }
        
        private void PAIBAN_SGShuRu_Load(object sender, EventArgs e)
        {
             cbJizhen.Checked = true;
             this.dtOSDate.Format = DateTimePickerFormat.Custom;
             this.dtOSDate.CustomFormat = "yyyy-MM-dd HH:mm";
             paibanDataBind();

            this.lineSSYS.LineBoxDoubleClick += new EventHandler(lineOS_DoubleClick);
            this.lineMZYS.LineBoxDoubleClick += new EventHandler(lineMZYS_DoubleClick);
             this.lineQXHS.LineBoxDoubleClick += new EventHandler(lineQXHS_DoubleClick);
             this.lineXHHS.LineBoxDoubleClick += new EventHandler(lineXHHS_DoubleClick);
             this.lineSQZD.LineBoxDoubleClick += new EventHandler(lineSqzd_DoubleClick);
             this.lineNSSSS.LineBoxDoubleClick += new EventHandler(lineNssss_DoubleClick);        
             BindAllComBox();            
             
        }       
        private void paibanDataBind()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetPAIBAN(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));  
            dgvJizhenPaiban.DataSource = dt.DefaultView;
        }
        private void BindAllComBox()
        {
            cmbOroom.Items.Clear();
            DataTable dt = _DataDicDal.GetOroom();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.cmbOroom.Items.Add(dt.Rows[i][0].ToString());
            }
            cmbMZFF.Items.Clear();
            DataTable dt1 = _DataDicDal.GetMazuiFangfaAll();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbMZFF.Items.Add(dt1.Rows[i][0].ToString());
            }
            cmbKeshi.Items.Clear();
            DataTable dt2 = _DataDicDal.GetKeshi();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                this.cmbKeshi.Items.Add(dt2.Rows[i][0].ToString());
            }
            cmbTiwei.Items.Clear();
            DataTable dt3 = _DataDicDal.GetTiwei();
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                this.cmbTiwei.Items.Add(dt3.Rows[i][0].ToString());
            }
        }
          
       
        
        #region<<打印>>
        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = pdDocument;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                pdDocument.Print();
            //pdDocument.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            pdDocument.DefaultPageSettings.Landscape = true;
        }

        private void pdDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        { //C#打印设置之设置横向打印   
            this.pdDocument.DefaultPageSettings.Landscape = true;
            //C#打印设置之设置彩色打印   
            this.pdDocument.DefaultPageSettings.Color = true;

            //C#打印设置之设置打印纸张类型和大小  
            this.pdDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 800, 1100);

            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }
        int dyi = 0;
        private void pdDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 20, y = 50;
            Pen black = new Pen(Brushes.Black);
            black.Width = 1;
            Font HeiTi = new System.Drawing.Font("黑体", 9);
            Font Kti = new System.Drawing.Font("宋体", 7);
            e.Graphics.DrawString("手 术 通 知 单", new Font("宋体", 16, FontStyle.Bold), Brushes.Black, new Point(x + 200, y));
            e.Graphics.DrawString("手术日期：" + dtOSDate.Value.ToLongDateString(), HeiTi, Brushes.Black, new Point(x + 30, y + 30));
            e.Graphics.DrawString("共 " + dgvJizhenPaiban.Rows.Count.ToString() + " 台", HeiTi, Brushes.Black, new Point(x + 230, y + 30));
            y = y + 60;
            e.Graphics.DrawLine(black, new Point(x, y), new Point(1070, y));
            e.Graphics.DrawString("手术间", HeiTi, Brushes.Black, new Point(x + 5, y + 5));
            e.Graphics.DrawString("台次", HeiTi, Brushes.Black, new Point(x + 65, y + 5));
            e.Graphics.DrawString("科室", HeiTi, Brushes.Black, new Point(x + 105, y + 5));
            e.Graphics.DrawString("姓名", HeiTi, Brushes.Black, new Point(x + 155, y + 5));
            e.Graphics.DrawString("性别", HeiTi, Brushes.Black, new Point(x + 215, y + 5));
            e.Graphics.DrawString("年龄", HeiTi, Brushes.Black, new Point(x + 255, y + 5));
            e.Graphics.DrawString("床号", HeiTi, Brushes.Black, new Point(x + 295, y + 5));
            e.Graphics.DrawString("拟施手术", HeiTi, Brushes.Black, new Point(x + 335, y + 5));
            e.Graphics.DrawString("手术医师", HeiTi, Brushes.Black, new Point(x + 435, y + 5));
            e.Graphics.DrawString("麻醉方法", HeiTi, Brushes.Black, new Point(x + 535, y + 5));
            e.Graphics.DrawString("麻醉医师", HeiTi, Brushes.Black, new Point(x + 635, y + 5));
            e.Graphics.DrawString("术前诊断", HeiTi, Brushes.Black, new Point(x + 735, y + 5));

            y = y + 25;
            for (; dyi < dgvJizhenPaiban.Rows.Count; dyi++)
            {
                e.Graphics.DrawLine(black, new Point(x, y), new Point(1070, y));
                e.Graphics.DrawString(dgvJizhenPaiban[0, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 5, y + 5));
                e.Graphics.DrawString(dgvJizhenPaiban[1, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 65, y + 5));
                e.Graphics.DrawString(dgvJizhenPaiban[3, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 105, y + 5));
                e.Graphics.DrawString(dgvJizhenPaiban[4, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 155, y + 5));
                e.Graphics.DrawString(dgvJizhenPaiban[5, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 215, y + 5));
                e.Graphics.DrawString(dgvJizhenPaiban[6, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 255, y + 3));
                e.Graphics.DrawString(dgvJizhenPaiban[7, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 295, y + 5));
                e.Graphics.DrawString(dgvJizhenPaiban[10, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 335, y + 5));
                e.Graphics.DrawString(dgvJizhenPaiban[9, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 435, y + 5));
                e.Graphics.DrawString(dgvJizhenPaiban[12, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 535, y + 5));
                e.Graphics.DrawString(dgvJizhenPaiban[13, dyi].Value.ToString() + "、" + dgvJizhenPaiban[15, dyi].Value.ToString() + "、" + dgvJizhenPaiban[14, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 635, y + 3));
                e.Graphics.DrawString(dgvJizhenPaiban[11, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 735, y + 5));
                y = y + 25;
            }
            e.Graphics.DrawLine(black, new Point(x, y), new Point(1070, y));
            int hangshu = dgvJizhenPaiban.Rows.Count + 1;
            e.Graphics.DrawLine(black, new Point(x, y - hangshu * 25), new Point(x, y));
            e.Graphics.DrawLine(black, new Point(x + 60, y - hangshu * 25), new Point(x + 60, y));
            e.Graphics.DrawLine(black, new Point(x + 100, y - hangshu * 25), new Point(x + 100, y));
            e.Graphics.DrawLine(black, new Point(x + 150, y - hangshu * 25), new Point(x + 150, y));
            e.Graphics.DrawLine(black, new Point(x + 210, y - hangshu * 25), new Point(x + 210, y));
            e.Graphics.DrawLine(black, new Point(x + 250, y - hangshu * 25), new Point(x + 250, y));
            e.Graphics.DrawLine(black, new Point(x + 290, y - hangshu * 25), new Point(x + 290, y));
            e.Graphics.DrawLine(black, new Point(x + 330, y - hangshu * 25), new Point(x + 330, y));
            e.Graphics.DrawLine(black, new Point(x + 430, y - hangshu * 25), new Point(x + 430, y));
            e.Graphics.DrawLine(black, new Point(x + 530, y - hangshu * 25), new Point(x + 530, y));
            e.Graphics.DrawLine(black, new Point(x + 630, y - hangshu * 25), new Point(x + 630, y));
            e.Graphics.DrawLine(black, new Point(x + 730, y - hangshu * 25), new Point(x + 730, y));
            e.Graphics.DrawLine(black, new Point(x + 1050, y - hangshu * 25), new Point(x + 1050, y));
            dyi = 0;
        }
        #endregion

        private void lineMZYS_DoubleClick(object sender, EventArgs e)
        {
            SelectMZYSandHushi F1 = new SelectMZYSandHushi(1, lineMZYS);
            F1.ShowDialog();
        }

        private void lineQXHS_DoubleClick(object sender, EventArgs e)
        {
            SelectMZYSandHushi F1 = new SelectMZYSandHushi(1, lineQXHS);
            F1.ShowDialog();
        }

        private void lineOS_DoubleClick(object sender, EventArgs e)
        {
            SelectMZYSandHushi F1 = new SelectMZYSandHushi(1, lineSSYS);
            F1.ShowDialog();
        }
        
        private void lineXHHS_DoubleClick(object sender, EventArgs e)
        {
            SelectMZYSandHushi F1 = new SelectMZYSandHushi(2, lineXHHS);
            F1.ShowDialog();
        }
        private void lineSqzd_DoubleClick(object sender, EventArgs e)
        {
            SelectZhenDuan F1 = new SelectZhenDuan(lineSQZD);
            F1.ShowDialog();
        }
        private void lineNssss_DoubleClick(object sender, EventArgs e)
        {
            SelectShoushu F1 = new SelectShoushu(lineNSSSS);
            F1.ShowDialog();
        }
        private void tbSecond_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender,e);
        }


        private void dgvJizhenPaiban_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJizhenPaiban.Rows.Count > 0)
            {
                string PATID = dgvJizhenPaiban.CurrentRow.Cells["PatZhuYuanID"].Value.ToString();
                DataTable dt = new DataTable();
                dt = _PaibanDal.GetPaibanByPatId(PATID);
                linePatid.Text = dt.Rows[0]["PatID"].ToString();
                txtPatNation.Text = dt.Rows[0]["PatNation"].ToString();
                lineName.Text = dt.Rows[0]["Patname"].ToString();
                lineAge.Text = dt.Rows[0]["Patage"].ToString();
                cmbSex.Text = dt.Rows[0]["Patsex"].ToString();
                cmbKeshi.Text = dt.Rows[0]["Patdpm"].ToString();
                lineBedno.Text = dt.Rows[0]["Patbedno"].ToString();
                lineBedno.Text = dt.Rows[0]["PatWeight"].ToString();
                lineBedno.Text = dt.Rows[0]["PatHeight"].ToString();
                lineNSSSS.Text = dt.Rows[0]["Oname"].ToString();
                lineSQZD.Text = dt.Rows[0]["Pattmd"].ToString();
                cmbOroom.Text = dt.Rows[0]["Oroom"].ToString();
                lineSecond.Text = dt.Rows[0]["Second"].ToString();
                cmbMZFF.Text = dt.Rows[0]["Amethod"].ToString();
                cmbTiwei.Text = dt.Rows[0]["tiwei"].ToString();
                this.lineMZYS.Text = dt.Rows[0]["AP1"].ToString();
                this.lineQXHS.Text = dt.Rows[0]["ON1"].ToString();
                this.lineXHHS.Text = dt.Rows[0]["SN1"].ToString();
                dtOSDate.Value = Convert.ToDateTime(dt.Rows[0]["Odate"]);
                this.lineSSYS.Text = dt.Rows[0]["OS"].ToString();
                this.txtBZ.Text = dt.Rows[0]["remarks"].ToString();
                if (dt.Rows[0]["isjizhen"].ToString()=="1")
                {
                    cbJizhen.Checked = true;
                }
                else
                    cbZeqi.Checked = true;
                
            }
        }

        private void EnterMZD_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvJizhenPaiban.Rows.Count >0)
            {
                string PatID = dgvJizhenPaiban.CurrentRow.Cells["PatZhuYuanID"].Value.ToString();
                if (string.IsNullOrEmpty(PatID))
                {
                    MessageBox.Show("住院号不能为空。");
                    return;
                }
                string Oroom = dgvJizhenPaiban.CurrentRow.Cells["oroom"].Value.ToString();
                if (string.IsNullOrEmpty(Oroom))
                {
                    MessageBox.Show("手术间号不能为空。");
                    return;
                }               
                int mzjldID = 0;
                string Odate = dtOSDate.Value.ToString("yyyy-MM-dd HH:mm");
                DataTable dt = _MzjldDal.GetMzjldByPatId(PatID);
                if (dt.Rows.Count > 0)
                    mzjldID = Convert.ToInt32(dt.Rows[0][0]);
                MzjldEdit F1 = new MzjldEdit(PatID, Oroom, DateTime.Parse(Odate), mzjldID);
                F1.ShowDialog();
                this.Close();
                new PaibanForm().Close(); 
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            paibanDataBind();
        }
        adims_DAL.HisDB_Help HisHelp = new adims_DAL.HisDB_Help();
        private void btnCheckHis_Click(object sender, EventArgs e)
        {
            if (linePatid.Text.Trim().Length != 6)
            {
                MessageBox.Show("请输入六位完整住院号");
            }
            else
            {
                string IPaddress = "132.147.160.60";
                bool flag = TextValueLimit.PingHost(IPaddress, 1000);
                if (flag == true)
                {
                    DataTable dt = HisHelp.GetHisInfoByPatID(linePatid.Text.Trim());
                    if (dt.Rows.Count == 1)
                    {
                        lineBedno.Text = dt.Rows[0]["BedNo"].ToString();
                        lineName.Text = dt.Rows[0]["patname"].ToString();
                        lineAge.Text = dt.Rows[0]["patage"].ToString();
                        txtPatNation.Text = dt.Rows[0]["patNation"].ToString();
                        cmbSex.Text = dt.Rows[0]["patsex"].ToString();
                        cmbKeshi.Text = dt.Rows[0]["Patdpm"].ToString();
                        lineSQZD.Text = dt.Rows[0]["SQZD"].ToString();
                        lineTSQK.Text = dt.Rows[0]["bz"].ToString();
                        lineNSSSS.Text = dt.Rows[0]["oname"].ToString();
                        cmbMZFF.Text = dt.Rows[0]["mzff"].ToString();
                    }
                    else
                        MessageBox.Show("无此住院号病人信息,请检查");
                }
                else
                    MessageBox.Show("HIS网络连接错误，请检查");
            }
        }

       
    }
}
