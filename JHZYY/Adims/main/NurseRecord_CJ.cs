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
    public partial class NurseRecord_CJ : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_DAL.PACU_DAL pdal = new adims_DAL.PACU_DAL();
        string PATID, MZID;
        public NurseRecord_CJ(string patid, string mzid)
        {
            PATID = patid;
            MZID = mzid;
            InitializeComponent();
        }
        public NurseRecord_CJ()
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
            Dictionary<string, string> A_Visit = new Dictionary<string, string>();
            int result = 0;
            try
            {
                A_Visit.Add("mzjldid", MZID);
                A_Visit.Add("patid", PATID);  
                A_Visit.Add("EnterTime", dtEnterTime.Value.ToString("yyyy-MM-dd HH:mm"));
                A_Visit.Add("LeaveTime", dtLeaveTime.Value.ToString("yyyy-MM-dd HH:mm"));
                A_Visit.Add("Shenzhi", cmbShenzhi.Text.Trim());
                A_Visit.Add("JMCC", cmbJMCC.Text.Trim());
                A_Visit.Add("Daoniao", cmbDaoniao.Text.Trim());
                A_Visit.Add("PifuIn", cmbPifuIn.Text.Trim());
                A_Visit.Add("Weight", tbWeight.Text.Trim());
                A_Visit.Add("Kangshengsu", cmbKangshengsu.Text.Trim());
                A_Visit.Add("Guominshi", tbGuominshi.Text.Trim());
                A_Visit.Add("Shuye", tbShuye.Text.Trim());
                A_Visit.Add("Zhixuedai", cmbZhixuedai.Text.Trim());
                A_Visit.Add("Tiwei", cmbTiwei.Text.Trim());
                A_Visit.Add("Xuexing", cmbXuexing.Text.Trim());
                if(cbZitixue.Checked)
                    A_Visit.Add("Zitixue", "1");
                else
                    A_Visit.Add("Zitixue", "0");                
                A_Visit.Add("ZitixueJL", tbZitixueJL.Text.Trim());
                A_Visit.Add("ZitixueDW", cmbZitixueDW.Text.Trim());
                if (cbYitixue.Checked)
                    A_Visit.Add("Yitixue", "1");
                else
                    A_Visit.Add("Yitixue", "0");
                A_Visit.Add("YitixueType", cmbYitixueType.Text.Trim());
                A_Visit.Add("YitixueJL", tbYitixueJL.Text.Trim());
                A_Visit.Add("YitixueDW", cmbYitixueDW.Text.Trim());
                A_Visit.Add("Yishi", cmbYishi.Text.Trim());
                A_Visit.Add("PifuOut", cmbPifuOut.Text.Trim());
                A_Visit.Add("DaoniaoOut", cmbDaoniaoOut.Text.Trim());
                A_Visit.Add("Niaoliang", tbNiaoliang.Text.Trim());
                A_Visit.Add("Yinliu", cmbYinliu.Text.Trim());
                A_Visit.Add("BRQX", cmbBRQX.Text.Trim());
                A_Visit.Add("RemarkOut", tbRemarkOut.Text.Trim());
                A_Visit.Add("Wujunbao", cmbWujunbao.Text.Trim());
                A_Visit.Add("XHHS", cmbXHHS.Text.Trim());
                A_Visit.Add("QXHS", cmbQXHS.Text.Trim());
                A_Visit.Add("RemarkLast", tbRemarkLast.Text.Trim());
                A_Visit.Add("Biaoben", cmbBiaoben.Text.Trim());
                if (cbDiandao.Checked)
                    A_Visit.Add("Diandao","1");
                else
                    A_Visit.Add("Diandao", "0");               
                A_Visit.Add("Dianjiban", tbDianjiban.Text.Trim());
                A_Visit.Add("UpdateTime", dtVisitDate.Value.ToString()); 

                result = DAL.UpdateNurseRecord(A_Visit);
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
            cmbOroom.Text = dt.Rows[0]["Oroom"].ToString();
            tbMingzu.Text = dt.Rows[0]["PatNation"].ToString();
            tbKeshi.Text = dt.Rows[0]["patdpm"].ToString();
            cmbMZFF.Text = dt.Rows[0]["Amethod"].ToString();
        }
        private void BindCombox3()
        {
            cmbQXHS.Items.Clear();
            cmbXHHS.Items.Clear();
            DataTable dt = new DataTable();
            dt = DAL.GetAll_hushi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbQXHS.Items.Add(dt.Rows[i][0]);
                cmbXHHS.Items.Add(dt.Rows[i][0]);
            }
        }
        /// <summary>
        /// 绑定手术间
        /// </summary>
        private void BindcmbRoom() {
           DataTable dt1 = DAL.GetOROOM();
           cmbOroom.DataSource = dt1; 
           cmbOroom.ValueMember = "NAME"; 
        }
        private void BindYaoPin()
        {
            DataTable dt = pdal.GetAdims_YaoPinByType("输血",this.cmbYitixueType.Text.Trim());
            this.cmbYitixueType.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbYitixueType.Items.Add(dt.Rows[i]["ypname"]);
            }          
        }
        private void NurseRecord_SZ_Load(object sender, EventArgs e)
        {
            BindcmbRoom();
            dgvQXQD.Rows.Add(10);
            dgvQXQD.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvQXQD.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.dtEnterTime.Format = DateTimePickerFormat.Custom;
            this.dtEnterTime.CustomFormat = "MM-dd HH:mm";
            this.dtLeaveTime.Format = DateTimePickerFormat.Custom;
            this.dtLeaveTime.CustomFormat = "MM-dd HH:mm";
            BindPatInfo();
            BindCombox3();
            BindYaoPin();
            DataTable dt = DAL.GetNurseRecord(MZID);
            if (dt.Rows.Count == 0)
            {
                DAL.InsertNurseRecord(MZID,PATID, dtVisitDate.Value, 1);
            }
            else
            {
                DataRow dr = dt.Rows[0];
                if (dr["EnterTime"].ToString() != "")           
                    dtEnterTime.Value=Convert.ToDateTime(dr["EnterTime"]);
                if (dr["LeaveTime"].ToString() != "")  
                    dtLeaveTime.Value=Convert.ToDateTime(dr["LeaveTime"]);
                cmbShenzhi.Text = Convert.ToString(dr["Shenzhi"]);    
                cmbJMCC.Text=Convert.ToString(dr["JMCC"]);
                cmbDaoniao.Text=Convert.ToString(dr["Daoniao"]);
                cmbPifuIn.Text=Convert.ToString(dr["PifuIn"]);
                tbWeight.Text=Convert.ToString(dr["Weight"]);
                cmbKangshengsu.Text=Convert.ToString(dr["Kangshengsu"]);
                tbGuominshi.Text=Convert.ToString(dr["Guominshi"]);
                tbShuye.Text=Convert.ToString(dr["Shuye"]);
                cmbZhixuedai.Text=Convert.ToString(dr["Zhixuedai"]);
                cmbTiwei.Text=Convert.ToString(dr["Tiwei"]);
                cmbXuexing.Text=Convert.ToString(dr["Xuexing"]);
                if (Convert.ToString(dr["diandao"]) == "1")
                    cbDiandao.Checked = true;
                else
                    cbDiandao.Checked = false;
                tbDianjiban.Text = Convert.ToString(dr["dianjiban"]);
                if (Convert.ToString(dr["Zitixue"]) == "1")
                    cbZitixue.Checked = true;
                else
                    cbZitixue.Checked = false;
                tbZitixueJL.Text = Convert.ToString(dr["ZitixueJL"]);
                cmbZitixueDW.Text = Convert.ToString(dr["ZitixueDW"]);
                if (Convert.ToString(dr["Yitixue"]) == "1")
                    cbYitixue.Checked = true;
                else
                    cbYitixue.Checked = false;
                cmbYitixueType.Text = Convert.ToString(dr["Yitixuetype"]);
                cmbYitixueDW.Text = Convert.ToString(dr["YitixueDW"]);               
                tbYitixueJL.Text=Convert.ToString(dr["YitixueJL"]);
                cmbYishi.Text=Convert.ToString(dr["Yishi"]);
                cmbPifuOut.Text=Convert.ToString(dr["PifuOut"]);
                cmbDaoniaoOut.Text=Convert.ToString(dr["DaoniaoOut"]);
                tbNiaoliang.Text=Convert.ToString(dr["Niaoliang"]);
                cmbYinliu.Text=Convert.ToString(dr["Yinliu"]);
                cmbBRQX.Text=Convert.ToString(dr["BRQX"]);
                tbRemarkOut.Text=Convert.ToString(dr["RemarkOut"]);
                cmbWujunbao.Text=Convert.ToString(dr["Wujunbao"]);
                cmbXHHS.Text=Convert.ToString(dr["XHHS"]);
                cmbQXHS.Text=Convert.ToString(dr["QXHS"]);
                cmbBiaoben.Text = Convert.ToString(dr["Biaoben"]);                
                tbRemarkLast.Text=Convert.ToString(dr["RemarkLast"]);
                if (dr["UpdateTime"].ToString() != "")
                    dtVisitDate.Value = Convert.ToDateTime(dr["UpdateTime"]);
                else if (dr["AddTime"].ToString() != "")
                    dtVisitDate.Value = Convert.ToDateTime(dr["AddTime"]);
                if (Convert.ToInt32(dt.Rows[0]["IsRead"]) == 0)
                    btnSave.Enabled = false;
            }
            Bind_QxModel();
            //comboBox1.SelectedIndex = 0;
            //button3_Click(null, null);
            dgvBind();
            string jurisdiction = bll.Get_user_jurisdiction(Program.customer);
            if (jurisdiction.Contains("8"))
            {
                btnUnlock.Visible = true;
            }
            else
            {
                btnUnlock.Visible = false;                
            }         

        }
        /// <summary>
        /// 绑定DGV
        /// </summary>
        private void dgvBind()
        {
            try
            {
                DataTable tb = DAL.GetBindQX(MZID);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (i>=dgvQXQD.Rows.Count)
                    {
                        dgvQXQD.Rows.Add();
                    }
                    dgvQXQD.Rows[i].Cells[0].Value = tb.Rows[i]["QXname"];
                    dgvQXQD.Rows[i].Cells[1].Value = tb.Rows[i]["SQ"];
                    dgvQXQD.Rows[i].Cells[2].Value = tb.Rows[i]["GQ"];
                    dgvQXQD.Rows[i].Cells[3].Value = tb.Rows[i]["GH"];
                    dgvQXQD.Rows[i].Cells[4].Value = tb.Rows[i]["QXname1"];
                    dgvQXQD.Rows[i].Cells[5].Value = tb.Rows[i]["SQ1"];
                    dgvQXQD.Rows[i].Cells[6].Value = tb.Rows[i]["GQ1"];
                    dgvQXQD.Rows[i].Cells[7].Value = tb.Rows[i]["GH1"]; ;
                    dgvQXQD.Rows[i].Cells[8].Value = tb.Rows[i]["QXname2"];
                    dgvQXQD.Rows[i].Cells[9].Value = tb.Rows[i]["SQ2"];
                    dgvQXQD.Rows[i].Cells[10].Value = tb.Rows[i]["GQ2"];
                    dgvQXQD.Rows[i].Cells[11].Value = tb.Rows[i]["GH2"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("器械包清点加载失败");
            }
        
        }
        private void DataGridBind()
        {
            string kongge = "";
            for (int i = 0; i < 20; i++)
            {
                dgvQXQD.Rows[i].Cells[0].Value = kongge;
                dgvQXQD.Rows[i].Cells[4].Value = kongge;
                dgvQXQD.Rows[i].Cells[8].Value = kongge;
            }
        }


        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;           
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("K16", 737, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
                     new System.Drawing.Printing.PaperSize("a5", 820, 1160);  
            
        }

       private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {     
            Font zt8 = new Font("宋体", 8);
            Font zt9 = new Font("宋体", 9);
            Font ht9= new Font("黑体", 9);
            Font ht14 = new Font("黑体", 14);
            Pen pb1 = new Pen(Brushes.Black);
            Pen pb2 = new Pen(Brushes.Black,2);//普通画笔   
            Brush bp1=Brushes.Black;
            int x = 30, y = 0,y1=0;

            y = y + 50; y1 = y + 18;
            string title1 = "昌吉州人民医院手术护理记录单";
            e.Graphics.DrawString(title1, ht14, Brushes.Black, x + 220, y);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("日期：" + dtVisitDate.Text.Trim(), zt9, bp1, x + 50, y);
            e.Graphics.DrawLine(Pens.Black, x + 80, y1, x + 180, y1);
            if (tbKeshi.Text == "")
                e.Graphics.DrawLine(pb1, new Point(90 + x, y + 13), new Point(110 + x, y + 2));
            e.Graphics.DrawString("科室: " + tbKeshi.Text.Trim(), zt9, Brushes.Black, x + 200, y);
            e.Graphics.DrawLine(Pens.Black, x + 230, y1, x + 400, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(pb2, x, y, x + 740, y);
            e.Graphics.DrawLine(pb2, x, y, x, y + 950);
            e.Graphics.DrawLine(pb2, x + 740, y, x + 740, y + 950);
            e.Graphics.DrawLine(pb2, x, y + 950, x + 740, y + 950);            
          
            y = y + 5; y1 = y + 15;
           e.Graphics.DrawString("床号: " + tbBedNO.Text.Trim(), zt9, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 35, y1, x + 100, y1);
            e.Graphics.DrawString("姓名: " + tbPatname.Text.Trim(), zt9, Brushes.Black, x + 120, y);
            e.Graphics.DrawLine(Pens.Black, x + 150, y1, x + 250, y1);
            e.Graphics.DrawString("民族: " + tbMingzu.Text.Trim(), zt9, Brushes.Black, x + 270, y);
            e.Graphics.DrawLine(Pens.Black, x + 290, y1, x + 340, y1);
            e.Graphics.DrawString("性别: " + tbSex.Text.Trim(), zt9, Brushes.Black, x + 360, y);
            e.Graphics.DrawLine(Pens.Black, x + 390, y1, x + 440, y1);
            e.Graphics.DrawString("年龄: " + tbAge.Text.Trim(), zt9, Brushes.Black, x + 460, y);
            e.Graphics.DrawLine(Pens.Black, x + 490, y1, x + 540, y1);
            e.Graphics.DrawString("岁   住院号: " + tbZhuyuanID.Text.Trim(), zt9, Brushes.Black, x + 540, y);
            e.Graphics.DrawLine(Pens.Black, x + 620, y1, x + 730, y1);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("术前诊断: " + tbSZZD.Text.Trim(), zt9, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 55, y1, x + 730, y1);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("手术名称: " + tbShoushuName.Text.Trim(), zt9, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 60, y1, x + 730, y1);

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("麻醉方式: " + cmbMZFF.Text.Trim(), zt9, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 60, y1, x + 360, y1);
            e.Graphics.DrawString("手术室: " + cmbOroom.Text.Trim(), zt9, Brushes.Black, x + 390, y);
            e.Graphics.DrawLine(Pens.Black, x + 425, y1, x + 500, y1);
            e.Graphics.DrawString("入室时间:   " + dtEnterTime.Value.ToString("HH:mm"), zt9, Brushes.Black, x + 530, y);
            e.Graphics.DrawLine(Pens.Black, x + 590, y1, x + 700, y1);

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x + 0, y, x + 740, y);

            y = y + 5; y1 = y + 15;
            e.Graphics.DrawString("护\n\n理\n\n情\n\n况", ht9, Brushes.Black, x + 5, y + 30);
            e.Graphics.DrawString("入室\n评估", ht9, Brushes.Black, x + 45, y+12);
            e.Graphics.DrawString("神志: " + cmbShenzhi.Text.Trim(), zt9, Brushes.Black, x + 100, y);
            e.Graphics.DrawLine(Pens.Black, x + 135, y1, x + 240, y1);
            e.Graphics.DrawString("静脉穿刺: " + cmbJMCC.Text.Trim(), zt9, Brushes.Black, x + 260, y);
            e.Graphics.DrawLine(Pens.Black, x + 320, y1, x + 400, y1);
            e.Graphics.DrawString("导尿: " + cmbDaoniao.Text.Trim(), zt9, Brushes.Black, x + 420, y);
            e.Graphics.DrawLine(Pens.Black, x + 450, y1, x + 560, y1);
            e.Graphics.DrawString("皮肤: " + cmbPifuIn.Text.Trim(), zt9, Brushes.Black, x + 580, y);
            e.Graphics.DrawLine(Pens.Black, x + 610, y1, x + 720, y1);

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("体重：" + tbWeight.Text.Trim(), zt9, Brushes.Black, x + 100, y);
            e.Graphics.DrawLine(Pens.Black, x + 135, y1, x + 220, y1);
            e.Graphics.DrawString("千克    抗生素: " + cmbKangshengsu.Text.Trim(), zt9, Brushes.Black, x + 220, y);
            e.Graphics.DrawLine(Pens.Black, x + 310, y1, x + 400, y1);
            e.Graphics.DrawString("药物过敏史: " + tbGuominshi.Text.Trim(), zt9, Brushes.Black, x + 420, y);
            e.Graphics.DrawLine(Pens.Black, x + 500, y1, x + 730, y1);           
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("术中", ht9, Brushes.Black, x + 45, y+30);            
            e.Graphics.DrawString("输液：" + tbShuye.Text.Trim(), zt9, Brushes.Black, x + 100, y);
            e.Graphics.DrawLine(Pens.Black, x + 135, y1, x + 220, y1);
            e.Graphics.DrawString("毫升  止血带：" + cmbZhixuedai.Text.Trim(), zt9, Brushes.Black, x + 220, y);
            e.Graphics.DrawLine(Pens.Black, x + 300, y1, x + 400, y1);
            e.Graphics.DrawString("体位：" + cmbTiwei.Text.Trim(), zt9, Brushes.Black, x + 420, y);
            e.Graphics.DrawLine(Pens.Black, x + 450, y1, x + 560, y1);
         
            y = y + 30; y1 = y + 15;            
            e.Graphics.DrawString("血型：" + cmbXuexing.Text.Trim(), zt9, Brushes.Black, x + 100, y);
            e.Graphics.DrawLine(Pens.Black, x + 135, y1, x + 240, y1);
            string zitixue="无";
            if (cbZitixue.Checked)
            {
                zitixue=tbZitixueJL.Text.Trim() + cmbZitixueDW.Text.Trim();
            }
            e.Graphics.DrawString("输自体血: " + zitixue, zt9, Brushes.Black, x + 260, y);
            e.Graphics.DrawLine(Pens.Black, x + 310, y1, x + 450, y1);
            e.Graphics.DrawString("输异体血: " +cmbYitixueType.Text.Trim()+" " + tbYitixueJL.Text.Trim() + cmbYitixueDW.Text.Trim(), zt9, Brushes.Black, x + 470, y);
            e.Graphics.DrawLine(Pens.Black, x + 520, y1, x + 700, y1);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("送检标本：" + cmbBiaoben.Text.Trim(), zt9, Brushes.Black, x + 100, y);
            e.Graphics.DrawLine(Pens.Black, x + 160, y1, x + 240, y1);
            string diandao="";
            if (cbZitixue.Checked)
                diandao = "是";
            else
                diandao = "否";
            e.Graphics.DrawString("使用电刀: " + diandao, zt9, Brushes.Black, x + 260, y);
            e.Graphics.DrawLine(Pens.Black, x + 318, y1, x + 400, y1);
            e.Graphics.DrawString("电极板位置: " + tbDianjiban.Text.Trim(), zt9, Brushes.Black, x + 420, y);
            e.Graphics.DrawLine(Pens.Black, x + 490, y1, x + 710, y1);

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("出室\n评估", ht9, Brushes.Black, x + 45, y + 12);
            e.Graphics.DrawString("意识情况：" + cmbYishi.Text.Trim(), zt9, Brushes.Black, x + 100, y);
            e.Graphics.DrawLine(Pens.Black, x + 160, y1, x + 240, y1);
            e.Graphics.DrawString("皮肤: " + cmbPifuOut.Text.Trim(), zt9, Brushes.Black, x + 260, y);
            e.Graphics.DrawLine(Pens.Black, x + 290, y1, x + 400, y1);
            e.Graphics.DrawString("导尿: " + cmbDaoniaoOut.Text.Trim(), zt9, Brushes.Black, x + 420, y);
            e.Graphics.DrawLine(Pens.Black, x + 450, y1, x + 520, y1);
            e.Graphics.DrawString("尿量: " + tbNiaoliang.Text.Trim(), zt9, Brushes.Black, x + 540, y);
            e.Graphics.DrawLine(Pens.Black, x + 570, y1, x + 650, y1);
            e.Graphics.DrawString("毫升" , zt9, Brushes.Black, x + 650, y);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("引流：" + cmbYinliu.Text.Trim(), zt9, Brushes.Black, x + 100, y);
            e.Graphics.DrawLine(Pens.Black, x + 135, y1, x + 240, y1);
            e.Graphics.DrawString("术后送入: " + cmbBRQX.Text.Trim(), zt9, Brushes.Black, x + 260, y);
            e.Graphics.DrawLine(Pens.Black, x + 320, y1, x + 400, y1);
            e.Graphics.DrawString("出室时间: " + dtLeaveTime.Value.ToString("HH:mm"), zt9, Brushes.Black, x + 420, y);
            e.Graphics.DrawLine(Pens.Black, x + 480, y1, x + 550, y1);
            y = y + 30; y1 = y + 15;
            string beizhu1 = "";
            string beizhu2 = "";
            if (tbRemarkOut.Text.Trim().Length > 40)
            {
                beizhu1 = tbRemarkOut.Text.Trim().Substring(0, 40);
                beizhu2 = tbRemarkOut.Text.Trim().Substring(41);
                e.Graphics.DrawString("备注：" + beizhu1, zt9, Brushes.Black, x + 100, y);
                e.Graphics.DrawString( beizhu2, zt9, Brushes.Black, x + 100, y+30);
            }
            else
            {
                e.Graphics.DrawString("备注：" + tbRemarkOut.Text.Trim(), zt9, Brushes.Black, x + 100, y);
            }
           
            e.Graphics.DrawLine(Pens.Black, x + 135, y1, x + 730, y1);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x + 98, y1, x + 730, y1);
           
            y = y + 30; y1 = y + 15;
            #region// 画器械表
            e.Graphics.DrawLine(Pens.Black, x, y, x + 740, y);
            int xx = x; 
            int rowCount = dgvQXQD.Rows.Count + 2;
            List<String> TagName = new List<string>();
            TagName.Add("    器 械\n    品 名");
            TagName.Add("术前\n清点");
            TagName.Add("关前\n清点");
            TagName.Add("关后\n清点");
            TagName.Add("    器 械\n    品 名");
            TagName.Add("术前\n清点");
            TagName.Add("关前\n清点");
            TagName.Add("关后\n清点");
            TagName.Add("    器 械\n    品 名");
            TagName.Add("术前\n清点");
            TagName.Add("关前\n清点");
            TagName.Add("关后\n清点");
            for (int i = 0; i < 12; i++)
            {            
                if (i == 1 || i == 5 || i == 9)
                    xx = xx + 50;
                e.Graphics.DrawString(TagName[i].ToString(), ht9, Brushes.Black, xx + 10, y + 10);    
                e.Graphics.DrawLine(pb1, new Point(xx, y), new Point(xx, y + 25 * 22));
                xx = xx + 50;
            }           
            y = y + 50; y1 = y + 25;
            //if (i % 3 == 0)
            //{
            //    dgvQXQD.Rows[i / 3].Cells[0].Value = dts.Rows[i][0];
            //    dgvQXQD.Rows[i / 3].Cells[1].Value = dts.Rows[i][1];
            //}
             DataTable dtss = DAL.GetallQxModelss();
            string name = dtss.Rows[0][1].ToString();
            DataTable dts = DAL.SelectqxmcInmodels(name);
            int dtCounts = dts.Rows.Count;
            DataTable dt1 = DAL.SelectqxmcInmodels(comboBox1.Text);
            int dtCount = dt1.Rows.Count;        
            //dgvQXQD.Rows.Clear();
            int Num = 0;
            int Num2 = 0;
            int Num3 = 0;
            int Num4 = 0;
            for (int i = 0; i < 20; i++)
            {
                e.Graphics.DrawLine(Pens.Black, x, y + 25 * i, x + 740, y + 25 * i);
                if (i < dgvQXQD.Rows.Count)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (dgvQXQD.Rows[i].Cells[j].Value == null)
                            dgvQXQD.Rows[i].Cells[j].Value = "";
                    }
                    if (dtCounts <=20)
                    {
                        if (i * 3 < dtCounts)
                        {
                            e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[0].Value.ToString(), zt8, Brushes.Black, x + 5, y + 5 + Num * 25);
                            e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 120, y + 5 + Num * 25);
                            e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 170, y + 5 + Num * 25);
                            e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 220, y + 5 + Num * 25);
                            Num++;
                            if (Num != dtCounts)
                            {
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 5, y + 5 + Num * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 120, y + 5 + Num * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 170, y + 5 + Num * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 220, y + 5 + Num * 25);
                                Num++;
                            }
                            else
                            {
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                Num2++;
                            }
                            if (Num != dtCounts)
                            {
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 5, y + 5 + Num * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 120, y + 5 + Num * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 170, y + 5 + Num * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 220, y + 5 + Num * 25);
                                Num++;
                            }
                            else
                            {
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                Num2++;
                            }                        

                          
                        }
                        else
                        {
                            if (dtCount >20)
                            {
                                if (Num2<20)
                                {
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[0].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                Num2++;
                                if (Num2<20)
                                {
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                    Num2++;
                                }
                                else
                                {

                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                    Num3++;
                                }
                              
                                
                                if (Num2<20)
                                {
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                    Num2++;
                                }
                                else
                                {
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                    Num3++;
                                }
                               
                              
                                }
                                else
                                {                                    
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[0].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                    Num3++;
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                    Num3++;

                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                    Num3++;
                                }
                            }
                            else
                            {
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[0].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                Num2++;
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                Num2++;

                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                Num2++;
                            }
                        }

                    }
                    else
                    {
                        if (Num4<20)
                        {
                            e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[0].Value.ToString(), zt8, Brushes.Black, x + 5, y + 5 + Num4 * 25);
                            e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 120, y + 5 + Num4 * 25);
                            e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 170, y + 5 + Num4 * 25);
                            e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 220, y + 5 + Num4 * 25);
                            Num4++;
                            if (Num4<20)
                            {

                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 5, y + 5 + Num4 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 120, y + 5 + Num4 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 170, y + 5 + Num4 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 220, y + 5 + Num4 * 25);
                                Num4++;
                            }
                            else
                            {
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                Num2++;
                            }

                            if (Num4<20)
                            {
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 5, y + 5 + Num4 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 120, y + 5 + Num4 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 170, y + 5 + Num4 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 220, y + 5 + Num4 * 25);
                                Num4++;
                            }
                            else
                            {
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                Num2++;
                            }
                    
                        }
                        else
                        {
                            if (Num2 < 20)
                            {
                                if (Num4+1==dtCounts)
                                {
                                    Num2++;
                                }

                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[0].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                Num2++;
                                Num4++;
                                if (Num4+1 == dtCounts)
                                {
                                    Num2++;
                                }
                                if (Num2 < 20)
                                {
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                    Num2++;
                                    Num4++;
                                }
                                else
                                {

                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                    Num3++;
                                }

                                if (Num4+1 == dtCounts)
                                {
                                    Num2++;
                                }
                                if (Num2 < 20)
                                {
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + Num2 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + Num2 * 25);
                                    Num2++;
                                    Num4++;
                                }
                                else
                                {
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                    e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                    Num3++;
                                }
                                if (Num4+1 == dtCounts)
                                {
                                    Num2++;
                                }

                            }
                            else
                            {
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[0].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                Num3++;
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                Num3++;

                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + Num3 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + Num3 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + Num3 * 25);
                                e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + Num3 * 25);
                                Num3++;
                            }
                        }
                    }

                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[0].Value.ToString(), zt8, Brushes.Black, x + 5, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 120, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 170, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 220, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[4].Value.ToString(), zt8, Brushes.Black, x + 255, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 370, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 420, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 470, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[8].Value.ToString(), zt8, Brushes.Black, x + 505, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 620, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + i * 25);
                    //e.Graphics.DrawString(dgvQXQD.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 720, y + 5 + i * 25);
                }
            }
            Num = 0;
            Num2 = 0;
            Num3 = 0;
            #endregion
            //y = y + 25 * dgvQXQD.Rows.Count;
            y = y + 25 * 20;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 740, y);
            y = y + 15; y1 = y + 15;
            e.Graphics.DrawString("巡回护士签名：" + cmbXHHS.Text.Trim(), zt9, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 95, y1, x + 300, y1);
            e.Graphics.DrawString("器械护士签名: " + cmbQXHS.Text.Trim(), zt9, Brushes.Black, x + 320, y);
            e.Graphics.DrawLine(Pens.Black, x + 410, y1, x + 600, y1);
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("备注: " + tbDianjiban.Text.Trim(), zt9, Brushes.Black, x + 5, y);
            e.Graphics.DrawLine(Pens.Black, x + 35, y1, x + 730, y1);
        }



        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_CJ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SAVE();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_CJ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_CJ_FormClosing);
                    this.Close();
                }

            }
        }

        private void Bind_QxModel()
        {
            comboBox1.Items.Clear();
            DataTable dts = DAL.GetallQxModelss();

            string name = dts.Rows[0][1].ToString();
            DataTable dt = DAL.GetallQxModelss(name);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "qxbType";
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    comboBox1.Items.Add(dt.Rows[i][0]);
            //}
        }

        private void button1_Click(object sender, EventArgs e)//器械清点
        {
            try
            {

                string jinggao = "";
                int flag = 0;//清点成功标志
                //int a1 = 0, a2 = 0, a3 = 0, a4 = 0, a5 = 0, a6 = 0, a7 = 0, a8 = 0, a9 = 0, a10 = 0, a11 = 0;
                for (int i = 0; i < dgvQXQD.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvQXQD.Columns.Count; j++)
                    {
                        dgvQXQD.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                        dgvQXQD.Rows[i].Cells[4].Style.ForeColor = Color.Black;
                        dgvQXQD.Rows[i].Cells[8].Style.ForeColor = Color.Black;
                    }                    
                    int a1 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[1].Value);
                    int a2 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[2].Value);
                    int a3 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[3].Value);
                    //int a4 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[4].Value);
                    int a5 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[5].Value);
                    int a6 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[6].Value);
                    int a7 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[7].Value);
                    //int a8 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[8].Value);
                    int a9 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[9].Value);
                    int a10 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[10].Value);
                    int a11 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[11].Value);
                    //if (dgvQXQD.Rows[i].Cells[1].Value.ToString()!="")
                    //{
                    //    a1 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[1].Value);
                    //}
                    //if (dgvQXQD.Rows[i].Cells[2].Value.ToString() != "")
                    //{
                    //    a2 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[2].Value);
                    //}
                    //if (dgvQXQD.Rows[i].Cells[3].Value.ToString()!= "")
                    //{
                    //    a3 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[3].Value);
                    //}
                    //if (dgvQXQD.Rows[i].Cells[5].Value.ToString()!= "")
                    //{
                    //    a5 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[5].Value);
                    //}
                    //if (dgvQXQD.Rows[i].Cells[6].Value.ToString()!= "")
                    //{
                    //    a6 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[6].Value);
                    //}
                    //if (dgvQXQD.Rows[i].Cells[6].Value.ToString()!= "")
                    //{
                    //    a7 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[7].Value);
                    //}
                    //if (dgvQXQD.Rows[i].Cells[6].Value.ToString()!= "")
                    //{
                    //    a9 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[9].Value);
                    //}
                    //if (dgvQXQD.Rows[i].Cells[6].Value.ToString()!= "")
                    //{
                    //    a10 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[10].Value);
                    //}
                    //if (dgvQXQD.Rows[i].Cells[6].Value.ToString()!= "")
                    //{
                    //    a11 = Convert.ToInt32(dgvQXQD.Rows[i].Cells[11].Value);
                    //}
                    //int a4 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[4].Value);          
                    //int a8 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[8].Value);              
                    if (a1 != a2 || a1 != a3)
                    {
                        flag++;
                        jinggao = jinggao + dgvQXQD.Rows[i].Cells[0].Value.ToString() + "\n";
                        dgvQXQD.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    }
                    if (a5 != a6 || a5 != a7)
                    {
                        flag++;
                        jinggao = jinggao + dgvQXQD.Rows[i].Cells[4].Value.ToString() + "\n";
                        dgvQXQD.Rows[i].Cells[4].Style.ForeColor = Color.Red;
                    }
                    if (a9 != a11 || a9 != a10)
                    {
                        flag++;
                        jinggao = jinggao + dgvQXQD.Rows[i].Cells[8].Value.ToString() + "\n";
                        dgvQXQD.Rows[i].Cells[8].Style.ForeColor = Color.Red;
                    }
                }
                if (flag == 0)
                    MessageBox.Show("清点成功！");
                else
                    MessageBox.Show(jinggao + "数量不正确");

            }

            catch (Exception)
            {

                MessageBox.Show("请填写完整的术前、关前、关后核对信息！");
            }
        }

        private void button3_Click(object sender, EventArgs e)//绑定器械
        {
            if (dgvQXQD.Rows.Count>0)
            {
                dgvQXQD.Rows.Clear();
            }            
            int i = 0;
            DataTable dtss = DAL.GetallQxModelss();
            string name = dtss.Rows[0][1].ToString();
            DataTable dts = DAL.SelectqxmcInmodels(name);
            int dtCounts = dts.Rows.Count;
            int js = dts.Rows.Count / 3;
            dgvQXQD.Rows.Add(js+1);
            for (; i < dtCounts; i++)
            {
                if (i % 3 == 0)
                {
                    dgvQXQD.Rows[i / 3].Cells[0].Value = dts.Rows[i][0];
                    dgvQXQD.Rows[i / 3].Cells[1].Value = dts.Rows[i][1];
                }
                else if (i % 3 == 1)
                {
                    dgvQXQD.Rows[i / 3].Cells[4].Value = dts.Rows[i][0];
                    dgvQXQD.Rows[i / 3].Cells[5].Value = dts.Rows[i][1];
                }
                else if (i % 3 == 2)
                {
                    dgvQXQD.Rows[i / 3].Cells[8].Value = dts.Rows[i][0];
                    dgvQXQD.Rows[i / 3].Cells[9].Value = dts.Rows[i][1];
                }
            }            
            DataTable dt = DAL.SelectqxmcInmodels(comboBox1.Text);
            int dtCount = dt.Rows.Count;
            int s = dt.Rows.Count / 3;           
            //dgvQXQD.Rows.Clear();
            dgvQXQD.Rows.Add(s+1);           
            for (int j = 0; j < dtCount; j++)
            {
                
                if (i % 3 == 0)
                {
                    dgvQXQD.Rows[i / 3].Cells[0].Value = dt.Rows[j][0];
                    dgvQXQD.Rows[i / 3].Cells[0].Style.ForeColor = Color.Green;
                    dgvQXQD.Rows[i / 3].Cells[1].Value = dt.Rows[j][1];
                    
                }
                else if (i % 3 == 1)
                {
                    dgvQXQD.Rows[i / 3].Cells[4].Value = dt.Rows[j][0];
                    dgvQXQD.Rows[i / 3].Cells[5].Value = dt.Rows[j][1];
                    dgvQXQD.Rows[i / 3].Cells[4].Style.ForeColor = Color.Green;
                }
                else if (i % 3 == 2)
                {
                    dgvQXQD.Rows[i / 3].Cells[8].Value = dt.Rows[j][0];
                    dgvQXQD.Rows[i / 3].Cells[9].Value = dt.Rows[j][1];
                    dgvQXQD.Rows[i / 3].Cells[8].Style.ForeColor = Color.Green;
                }
                i++;
            } 
           
        }

        private void NurseRecord_CJ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_CJ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SAVE();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_CJ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_CJ_FormClosing);
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SAVE();
            SaveQXQD();
        }
        /// <summary>
        /// 器械清点
        /// </summary>
        private void SaveQXQD()
        {
            DataTable dt = DAL.GetNurseRecordQX(MZID);
            if (dt.Rows.Count > 0)
            {
                //存在数据就删除
                int s = DAL.DeleteNurseRecordQX(MZID, PATID);
            }
            ///保存数据库
            try
            {
                for (int i = 0; i < dgvQXQD.Rows.Count; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (dgvQXQD.Rows[i].Cells[j].Value == null)
                            dgvQXQD.Rows[i].Cells[j].Value = "";
                    }
                    Dictionary<string, string> QXList = new Dictionary<string, string>();
                    QXList.Clear();
                    int result = 0;
                    QXList.Add("patid", PATID);
                    QXList.Add("mzjldid", MZID);
                    QXList.Add("QXB", comboBox1.Text);
                    QXList.Add("QXname", dgvQXQD.Rows[i].Cells[0].Value.ToString());
                    QXList.Add("SQ", dgvQXQD.Rows[i].Cells[1].Value.ToString());
                    QXList.Add("GQ", dgvQXQD.Rows[i].Cells[2].Value.ToString());
                    QXList.Add("GH", dgvQXQD.Rows[i].Cells[3].Value.ToString());
                    QXList.Add("QXname1", dgvQXQD.Rows[i].Cells[4].Value.ToString());
                    QXList.Add("SQ1", dgvQXQD.Rows[i].Cells[5].Value.ToString());
                    QXList.Add("GQ1", dgvQXQD.Rows[i].Cells[6].Value.ToString());
                    QXList.Add("GH1", dgvQXQD.Rows[i].Cells[7].Value.ToString());
                    QXList.Add("QXname2", dgvQXQD.Rows[i].Cells[8].Value.ToString());
                    QXList.Add("SQ2", dgvQXQD.Rows[i].Cells[9].Value.ToString());
                    QXList.Add("GQ2", dgvQXQD.Rows[i].Cells[10].Value.ToString());
                    QXList.Add("GH2", dgvQXQD.Rows[i].Cells[11].Value.ToString());
                    result = DAL.InsertNurseRecordQX(QXList);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("保存失败");
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetNurseRecord(MZID);
            if (dt.Rows.Count > 0)
            {
                result = DAL.UpdateNurseRecord_State(MZID, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    btnSave.Enabled = false;
                }
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.GetNurseRecord(MZID);
            if (dt.Rows.Count > 0)
            {
                result = DAL.UpdateNurseRecord_State(MZID, 0);
                if (result > 0)
                {
                    MessageBox.Show("解锁成功！");
                    btnSave.Enabled = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (BCCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_CJ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SAVE();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_CJ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.NurseRecord_CJ_FormClosing);
                    this.Close();
                }

            }
        }
        #region //限制输入
        private void tbNiaoliang_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }             

        private void tbShuye_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void tbYitixue_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void cmbZitixue_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }
        private void tbWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void cbZitixue_CheckedChanged(object sender, EventArgs e)
        {
            if (cbZitixue.Checked)
            {
                tbZitixueJL.Text = "";
                tbZitixueJL.Visible = true;
                cmbZitixueDW.Visible = true;
            }
            else
            {
                tbZitixueJL.Visible = false;
                cmbZitixueDW.Visible = false;
            }
            
        }

        private void cbDiandao_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDiandao.Checked)
            {
                lbDianjiban.Visible = true;
                tbDianjiban.Visible = true;
            }
            else
            {
                tbDianjiban.Visible = false;
                lbDianjiban.Visible = false;
            }
        }

        private void cbYitixue_CheckedChanged(object sender, EventArgs e)
        {
            if (cbYitixue.Checked)
            {
                cmbYitixueType.Visible = true;
                tbYitixueJL.Visible = true;
                cmbYitixueDW.Visible = true;
            }
            else
            {
                cmbYitixueType.Visible = false;
                tbYitixueJL.Visible = false;
                cmbYitixueDW.Visible = false;
            }

        }



        #endregion

       

    }
}
