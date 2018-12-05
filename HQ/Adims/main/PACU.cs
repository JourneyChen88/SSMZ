using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using adims_BLL;
using main.PACU_LEVEL;

namespace main.用户安全
{
    public partial class PACU : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// 

        string MZID, PATID;
        DateTime AddTIME = new DateTime();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        public PACU(string patid, string mzid)
        {
            MZID = mzid;
            PATID = patid;
            InitializeComponent();
           
        }
        public PACU()
        {

            InitializeComponent();
        }

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_MODEL.PACU_record record = new adims_MODEL.PACU_record();
        int pick_count = 0;
        int max_pickCount = 0;
        //DateTimePicker dtPicker = new DateTimePicker();
        ComboBox cmbYishi = new ComboBox();
        ComboBox cmbZZHX = new ComboBox();
        ComboBox cmbKCYS = new ComboBox();
        ComboBox cmbSKQK = new ComboBox();
        ComboBox cmbQDQK = new ComboBox();
        ComboBox cmbEXOT = new ComboBox();
        ComboBox cmbTTPF = new ComboBox();
        ComboBox cmbRecorder1 = new ComboBox();
        ComboBox cmbZZHXS = new ComboBox();
        
        private void PACU_Load(object sender, EventArgs e)
        {

            cmbJCSJJG.Text = cmbJCSJJG.Items[0].ToString();
            cmbYishi.Visible = false;
            cmbYishi.DropDownStyle = ComboBoxStyle.DropDown;
            cmbYishi.Items.Add("0");
            cmbYishi.Items.Add("1");
            cmbYishi.Items.Add("2");
            dgvJHB.Controls.Add(cmbYishi);
            cmbYishi.SelectedIndexChanged += new EventHandler(cmbYishi_SelectedIndexChanged);
            cmbYishi.MouseLeave += new EventHandler(cmbYishi_MouseLeave);

            cmbPACUYS.DataSource = bll.GetMZYS(1);
            cmbPACUYS.DisplayMember = "user_name";

            cmbMZYS.DataSource = bll.GetMZYS(1);
            cmbMZYS.DisplayMember = "user_name";

            cmbRecorder.DataSource = bll.GetMZYS(2);
            cmbRecorder.DisplayMember = "user_name";
            //dtPicker.Visible = false;
            //this.dtPicker.Format = DateTimePickerFormat.Custom;
            //this.dtPicker.CustomFormat = "HH:mm";        

            //dgvJHB.Controls.Add(dtPicker);
            //dtPicker.ValueChanged += new EventHandler(dtPicker_ValueChanged);
             
            cmbKCYS.Visible = false;
            cmbKCYS.DropDownStyle = ComboBoxStyle.DropDown;
            cmbKCYS.Items.Add("0");
            cmbKCYS.Items.Add("1");
            cmbKCYS.Items.Add("2");
            dgvJHB.Controls.Add(cmbKCYS);
            cmbKCYS.SelectedIndexChanged += new EventHandler(cmbKCYS_SelectedIndexChanged);
            cmbKCYS.MouseLeave += new EventHandler(cmbKCYS_MouseLeave); 

            cmbSKQK.Visible = false;
            cmbSKQK.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSKQK.Items.Add("0");
            cmbSKQK.Items.Add("1");
            cmbSKQK.Items.Add("2");
            dgvJHB.Controls.Add(cmbSKQK);
            cmbSKQK.SelectedIndexChanged += new EventHandler(cmbSKQK_SelectedIndexChanged);
            cmbSKQK.MouseLeave += new EventHandler(cmbSKQK_MouseLeave); 

            cmbQDQK.Visible = false;
            cmbQDQK.DropDownStyle = ComboBoxStyle.DropDown;
            cmbQDQK.Items.Add("0");
            cmbQDQK.Items.Add("1");
            cmbQDQK.Items.Add("2");
            dgvJHB.Controls.Add(cmbQDQK);
            cmbQDQK.SelectedIndexChanged += new EventHandler(cmbQDQK_SelectedIndexChanged);
            cmbQDQK.MouseLeave += new EventHandler(cmbQDQK_MouseLeave); 

            cmbEXOT.Visible = false;
            cmbEXOT.DropDownStyle = ComboBoxStyle.DropDown;
            cmbEXOT.Items.Add("0");
            cmbEXOT.Items.Add("1");
            dgvJHB.Controls.Add(cmbEXOT);
            cmbEXOT.SelectedIndexChanged += new EventHandler(cmbEXOT_SelectedIndexChanged);
            cmbEXOT.MouseLeave += new EventHandler(cmbEXOT_MouseLeave); 

            cmbTTPF.Visible = false;
            cmbTTPF.DropDownStyle = ComboBoxStyle.DropDown;
            cmbTTPF.Items.Add("0");
            cmbTTPF.Items.Add("1");
            cmbTTPF.Items.Add("2");
            cmbTTPF.Items.Add("3");
            cmbTTPF.Items.Add("4");
            cmbTTPF.Items.Add("5");
            dgvJHB.Controls.Add(cmbTTPF);
            cmbTTPF.SelectedIndexChanged += new EventHandler(cmbTTPF_SelectedIndexChanged);
            cmbTTPF.MouseLeave += new EventHandler(cmbTTPF_MouseLeave); 

            cmbRecorder1.Visible = false;
            cmbRecorder1.DropDownStyle = ComboBoxStyle.DropDown;
            DataTable dt = DAL.GetAllMZYS();
            foreach (DataRow dr in dt.Rows)
            {
                cmbRecorder1.Items.Add(dr[0].ToString());
            }
            dgvJHB.Controls.Add(cmbRecorder1);
            cmbRecorder1.SelectedIndexChanged += new EventHandler(cmbRecorder1_SelectedIndexChanged);
            cmbRecorder1.MouseLeave += new EventHandler(cmbRecorder1_MouseLeave);


            cmbZZHXS.Visible = false;
            cmbZZHXS.DropDownStyle = ComboBoxStyle.DropDown;
            cmbZZHXS.Items.Add("0");
            cmbZZHXS.Items.Add("1");
            dgvJHB.Controls.Add(cmbZZHXS);
            cmbZZHXS.SelectedIndexChanged += new EventHandler(cmbZZHXS_SelectedIndexChanged);
            cmbZZHXS.MouseLeave += new EventHandler(cmbZZHXS_MouseLeave); 

            BindPatInfo();
            bindPACUinfo();
            BindPACUData();
            BindPatMZInfo();
           // BindcmbPACUYS();
            //BindcmbMZYS();
            this.dgvJHB.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJHB_CellValueChanged);
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
        //private void BindcmbPACUYS()
        //{
        //    DataTable dt = DAL.GetAllMZYS();
        //    cmbPACUYS.Items.Clear();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        cmbPACUYS.Items.Add(dr[0].ToString());
        //    }
        //}
        //private void BindcmbMZYS()
        //{
        //    DataTable dt = DAL.GetAllMZYS();
        //    cmbMZYS.Items.Clear();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        cmbMZYS.Items.Add(dr[0].ToString());
        //    }
        //}
        private void cmbYishi_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvJHB.CurrentCell.Value = cmbYishi.SelectedItem.ToString();
            cmbYishi.Visible = false;
        }
        private void cmbYishi_MouseLeave(object sender, EventArgs e)
        {           
            cmbYishi.Visible = false;
        }

        //private void dtPicker_ValueChanged(object sender, EventArgs e)
        //{
        //    dgvJHB.CurrentCell.Value = dtPicker.Value.ToString("HH：mm");
        //    dtPicker.Visible = false;
        //}
        private void cmbKCYS_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvJHB.CurrentCell.Value = cmbKCYS.SelectedItem.ToString();
            cmbKCYS.Visible = false;
        }
        private void cmbKCYS_MouseLeave(object sender, EventArgs e)
        {
            cmbKCYS.Visible = false;
        }
        private void cmbSKQK_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvJHB.CurrentCell.Value = cmbSKQK.SelectedItem.ToString();
            cmbSKQK.Visible = false;
        }
        private void cmbSKQK_MouseLeave(object sender, EventArgs e)
        {
            cmbSKQK.Visible = false;
        }
        private void cmbQDQK_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvJHB.CurrentCell.Value = cmbQDQK.SelectedItem.ToString();
            cmbQDQK.Visible = false;
        }
        private void cmbQDQK_MouseLeave(object sender, EventArgs e)
        {
            cmbQDQK.Visible = false;
        }
        private void cmbEXOT_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvJHB.CurrentCell.Value = cmbEXOT.SelectedItem.ToString();
            cmbEXOT.Visible = false;
        }
        private void cmbEXOT_MouseLeave(object sender, EventArgs e)
        {
            cmbEXOT.Visible = false;
        }
        private void cmbTTPF_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvJHB.CurrentCell.Value = cmbTTPF.SelectedItem.ToString();
            cmbTTPF.Visible = false;
        }
        private void cmbTTPF_MouseLeave(object sender, EventArgs e)
        {
            cmbTTPF.Visible = false;
        }
        private void cmbZZHXS_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvJHB.CurrentCell.Value = cmbZZHXS.SelectedItem.ToString();
            cmbZZHXS.Visible = false;
        }
        private void cmbZZHXS_MouseLeave(object sender, EventArgs e)
        {
            cmbZZHXS.Visible = false;
        }
        private void cmbRecorder1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvJHB.CurrentCell.Value = cmbRecorder1.SelectedItem.ToString();
            cmbRecorder1.Visible = false;
        }
        private void cmbRecorder1_MouseLeave(object sender, EventArgs e)
        {
            //cmbRecorder1.DropDownClosed = false;
            cmbRecorder1.Visible = false;
        }
        private void BindPACUData()
        {
            DataTable dt = bll.GET_PACU_data(MZID);
            dgvJHB.DataSource = dt.DefaultView;

        }
        private void bindPACUinfo()
        {
            DataTable dt = bll.PACU_info(MZID);
            if (dt.Rows.Count != 0)
            {
                dtBGtime.Text = dt.Rows[0]["BGtime"].ToString();
                dtDate.Value = Convert.ToDateTime(dt.Rows[0]["AddTime"].ToString());
                tbZongRL.Text = dt.Rows[0]["ZongRL"].ToString();
                tbNiaoliang.Text = dt.Rows[0]["Niaoliang"].ToString();
                string qgcg = dt.Rows[0]["SSGD"].ToString();
                if (qgcg.Contains("1"))
                    cbQGCG.Checked = true;
                if (qgcg.Contains("2"))
                    cbSXTD.Checked = true;
                cmbCGweizhi.Text = dt.Rows[0]["CGweizhi"].ToString();
                cmbJCSJJG.Text = dt.Rows[0]["JCSJJG"].ToString();
                cmbBRQX.Text = dt.Rows[0]["BRQX"].ToString();
                cmbRecorder.Text = dt.Rows[0]["Recorder"].ToString();
                cmbMZYS.Text = dt.Rows[0]["MZYS"].ToString();
                tbVisitTime.Text = dt.Rows[0]["VisitTime"].ToString();
                rtbXiaojie.Text = dt.Rows[0]["Xiaojie"].ToString();
                DTRSTime.Text = dt.Rows[0]["RSTime"].ToString();
                DTLSTime.Text = dt.Rows[0]["LKTime"].ToString();
                cmbQXCD.Text = dt.Rows[0]["QXCD"].ToString();
                cmbHXDTC.Text = dt.Rows[0]["HXDTC"].ToString();
                cmbZTHD.Text = dt.Rows[0]["ZTHD"].ToString();
                txtZF.Text = dt.Rows[0]["PF"].ToString();
                dtBGtime.Text = dt.Rows[0]["BGtime"].ToString();
                cmbPACUYS.Text = dt.Rows[0]["PACUYS"].ToString();
            }
            if (Convert.ToInt32(dt.Rows[0]["IsRead"]) == 1)
            {
                btnSave.Enabled = false;
            }

        }

        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(PATID);
            if (dt.Rows.Count > 0)
            {
                tbZhuyuanNo.Text = dt.Rows[0]["PatZhuYuanid"].ToString();
                tbPatname.Text = dt.Rows[0]["Patname"].ToString();
                tbBingqu.Text = dt.Rows[0]["Patdpm"].ToString();
                tbSex.Text = dt.Rows[0]["Patsex"].ToString();
                tbAge.Text = dt.Rows[0]["Patage"].ToString();
                tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
                tbSSMC.Text = dt.Rows[0]["Oname"].ToString();
                tbSZZD.Text = dt.Rows[0]["Pattmd"].ToString();
            }

        }
        private void BindPatMZInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetMZJLD_Info(MZID);
            if (dt.Rows.Count > 0)
            {
                dtDate.Value = Convert.ToDateTime(dt.Rows[0]["otime"]);
                tbMZFF.Text = dt.Rows[0]["mazuifs"].ToString();
                tbSSMC.Text = dt.Rows[0]["shoushufs"].ToString();
                tbSZZD.Text = dt.Rows[0]["sqzd"].ToString();  
            }
        }
        /// <summary>
        /// 得到麻醉医生所对应的手术信息
        /// </summary>
        /// <param name="YiSheng">麻醉医生</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public DataTable GetShouShu(string YiSheng, string date)
        {
            DataTable dt;
            dt = adims_BLL.AdimsController.getPacu1(YiSheng, date);
            //adims_BLL.AdimsController.getPacu1();
            return dt;
        }

        int dr = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000 * 60 * int.Parse(cmbJCSJJG.Text);//改变timer的间隔时间            
            Random rd = new Random();//随机模拟数据
            string time = DateTime.Now.ToString("HH:mm");
            int xueya1 = rd.Next(60, 80);
            int xueya2 = rd.Next(100, 150);
            string xueya = xueya1.ToString() + "/" + xueya2.ToString();
            int miaobo = rd.Next(60, 90);
            int huxi = rd.Next(20, 60);
            int spo2 = rd.Next(90, 100);
            int xx=  bll.AddPACU_DATA(MZID, time, xueya, miaobo, huxi, spo2);
            if (xx>0)
            {
                BindPACUData();
            } 
        }
        public void clear()
        {
            dtDate.Value = DateTime.Now;
            tbVisitTime.Text = "";
        }


        private void PACU_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnEnd.Enabled == true)
            {
                MessageBox.Show("监测没有结束，不能关闭！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            if (dgvJHB.Rows.Count > 0)
            {
                if (dgvJHB.CurrentCell.IsInEditMode == true)
                {
                    MessageBox.Show("表格的单元格处于编辑状态，无法关闭！");
                    e.Cancel = true;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize = 
            //    new PaperSize("16K", 737, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
               new PaperSize("A4",1160,820);
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font zt9 = new Font(new FontFamily("宋体"), 9);
            Font ht12 = new Font(new FontFamily("黑体"), 12);
            //Font ht9 = new Font(new FontFamily("黑体"), 9);
            Pen ptp = new Pen(Brushes.Black);
            Pen pb2 = new Pen(Brushes.Black, 2);
            int x = 50, y = 30, y1 = y + 15;
            e.Graphics.DrawString("新疆昌吉州人名医院麻醉科恢复室（PACU）观察记录单", ht12, Brushes.Black, x + 300, y);
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawString("姓    名：" + tbPatname.Text, zt9, Brushes.Black, x, y);
            e.Graphics.DrawLine(ptp,new Point(x+50,y1),new Point(x+150,y1));
            e.Graphics.DrawString("性别：", zt9, Brushes.Black, x+175, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 215, y, 12, 12);
            e.Graphics.DrawString("男", zt9, Brushes.Black, x + 235, y);
            if (tbSex.Text=="男")
            {
                e.Graphics.DrawLine(pb2, x + 210, y, x + 220, y + 12);
                e.Graphics.DrawLine(pb2, x + 220, y + 12, x + 235, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 255, y, 12, 12);
            e.Graphics.DrawString("女", zt9, Brushes.Black, x + 275, y);
            if (tbSex.Text == "女")
            {
                e.Graphics.DrawLine(pb2, x + 250, y, x + 260, y + 12);
                e.Graphics.DrawLine(pb2, x + 260, y + 12, x + 275, y - 3);
            }
            e.Graphics.DrawLine(ptp, new Point(x + 205, y1), new Point(x + 300,y1));
            e.Graphics.DrawString("年龄：" + tbAge.Text, zt9, Brushes.Black, x + 335, y);
            e.Graphics.DrawLine(ptp, new Point(x + 370, y1), new Point(x + 405, y1));
            e.Graphics.DrawString("科室：" + tbBingqu.Text, zt9, Brushes.Black, x + 435, y);
            e.Graphics.DrawLine(ptp, new Point(x + 465, y1), new Point(x + 515, y1));
            e.Graphics.DrawString("床号：" + tbBedNO.Text, zt9, Brushes.Black, x + 545, y);
            e.Graphics.DrawLine(ptp, new Point(x + 575, y1), new Point(x + 605, y1));
            e.Graphics.DrawString("住院号：" + tbZhuyuanNo.Text, zt9, Brushes.Black, x + 635, y);
            e.Graphics.DrawLine(ptp, new Point(x + 675, y1), new Point(x + 765, y1));
            e.Graphics.DrawString("拔管（喉罩）时间：" + dtBGtime.Text, zt9, Brushes.Black, x + 795, y);
            e.Graphics.DrawLine(ptp, new Point(x + 895, y1), new Point(x + 985, y1));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("麻醉医师："+cmbMZYS.Text, zt9, Brushes.Black, x, y);
            e.Graphics.DrawLine(ptp, new Point(x + 50, y1), new Point(x + 150, y1));
            e.Graphics.DrawString("诊断：" + tbSZZD.Text, zt9, Brushes.Black, x + 175, y);
            e.Graphics.DrawLine(ptp, new Point(x + 205, y1), new Point(x + 515, y1));
            e.Graphics.DrawString("手术名称：" + tbSSMC.Text, zt9, Brushes.Black, x + 545, y);
            e.Graphics.DrawLine(ptp, new Point(x + 595, y1), new Point(x + 985, y1));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("麻醉方式：" , zt9, Brushes.Black, x, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 12, 12);
            e.Graphics.DrawString("气管插管全麻", zt9, Brushes.Black, x + 100, y);
            if (tbMZFF.Text == "气管插管全麻")
            {
                e.Graphics.DrawLine(pb2, x + 75, y, x + 85, y + 12);
                e.Graphics.DrawLine(pb2, x + 85, y + 12, x + 100, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 12, 12);
            e.Graphics.DrawString("气管插管全麻 + 椎管内麻醉", zt9, Brushes.Black, x + 220, y);
            if (tbMZFF.Text == "气管插管全麻+椎管内麻醉")
            {
                e.Graphics.DrawLine(pb2, x + 195, y, x + 205, y + 12);
                e.Graphics.DrawLine(pb2, x + 205, y + 12, x + 220, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 430, y, 12, 12);
            e.Graphics.DrawString("喉罩全麻", zt9, Brushes.Black, x + 450, y);
            if (tbMZFF.Text == "喉罩全麻")
            {
                e.Graphics.DrawLine(pb2, x + 425, y, x + 435, y + 12);
                e.Graphics.DrawLine(pb2, x + 435, y + 12, x + 450, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 520, y, 12, 12);
            e.Graphics.DrawString("双腔气管插管全麻", zt9, Brushes.Black, x + 540, y);
            if (tbMZFF.Text == "双腔气管插管全麻")
            {
                e.Graphics.DrawLine(pb2, x + 515, y, x + 525, y + 12);
                e.Graphics.DrawLine(pb2, x + 525, y + 12, x + 540, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 670, y, 12, 12);
            e.Graphics.DrawString("椎管内麻醉", zt9, Brushes.Black, x + 690, y);
            if (tbMZFF.Text == "椎管内麻醉")
            {
                e.Graphics.DrawLine(pb2, x + 665, y, x + 675, y + 12);
                e.Graphics.DrawLine(pb2, x + 675, y + 12, x + 690, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 800, y, 12, 12);
            e.Graphics.DrawString("静吸复合全麻", zt9, Brushes.Black, x + 820, y);
            if (tbMZFF.Text == "静吸复合全麻")
            {
                e.Graphics.DrawLine(pb2, x + 795, y, x + 805, y + 12);
                e.Graphics.DrawLine(pb2, x + 805, y + 12, x + 820, y - 3);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("入室时间：" + DTRSTime.Text, zt9, Brushes.Black, x, y);
            e.Graphics.DrawLine(ptp, new Point(x + 50, y1), new Point(x + 250, y1));
            e.Graphics.DrawString("随时管道 ： ", zt9, Brushes.Black, x+450, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 520, y, 12, 12);
            e.Graphics.DrawString("气管插管", zt9, Brushes.Black, x + 540, y);
            if (cbQGCG.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 515, y, x + 525, y + 12);
                e.Graphics.DrawLine(pb2, x + 525, y + 12, x + 540, y - 3);
            }
            e.Graphics.DrawString("（口腔   鼻腔   双腔   喉罩 ）", zt9, Brushes.Black, x + 590, y);
            if (cmbCGweizhi.Text=="口腔")
            {
                e.Graphics.DrawLine(pb2, x + 603, y, x + 613, y + 14);
                e.Graphics.DrawLine(pb2, x + 613, y + 14, x + 628, y - 3);
            }
            if (cmbCGweizhi.Text == "鼻腔")
            {
                e.Graphics.DrawLine(pb2, x + 646, y, x + 656, y + 14);
                e.Graphics.DrawLine(pb2, x + 656, y + 14, x + 671, y - 3);
            }
            if (cmbCGweizhi.Text == "双腔")
            {
                e.Graphics.DrawLine(pb2, x + 688, y, x + 698, y + 14);
                e.Graphics.DrawLine(pb2, x + 698, y + 14, x + 713, y - 3);
            }
            if (cmbCGweizhi.Text == "喉罩")
            {
                e.Graphics.DrawLine(pb2, x + 731, y, x + 741, y + 14);
                e.Graphics.DrawLine(pb2, x + 741, y + 14, x + 756, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 800, y, 12, 12);
            e.Graphics.DrawString("输液通道", zt9, Brushes.Black, x + 820, y);
            if (cbSXTD.Checked)
            {
                e.Graphics.DrawLine(pb2, x + 795, y, x + 805, y + 12);
                e.Graphics.DrawLine(pb2, x + 805, y + 12, x + 820, y - 3);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x-10, y), new Point(1100 , y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(x - 10, y + 20));
            e.Graphics.DrawString("口唇颜色情况", zt9, Brushes.Black, x , y+5);
            e.Graphics.DrawLine(ptp, new Point(x+90, y), new Point(x+90, y + 20));
            e.Graphics.DrawString("伤口情况", zt9, Brushes.Black, x+130, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 230, y), new Point(x + 230, y + 20));
            e.Graphics.DrawString("自主呼吸", zt9, Brushes.Black, x + 260, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 340, y), new Point(x + 340, y + 20));
            e.Graphics.DrawString("气道情况", zt9, Brushes.Black, x + 405, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 520, y), new Point(x + 520, y + 20));
            e.Graphics.DrawString("疼痛评分", zt9, Brushes.Black, x + 600, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 20));
            e.Graphics.DrawString("恶心呕吐", zt9, Brushes.Black, x + 770, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 870, y), new Point(x + 870, y + 20));
            e.Graphics.DrawString("意识状态", zt9, Brushes.Black, x + 920, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 1050, y), new Point(x + 1050, y + 20));
            int SX = y;
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(1100, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(x - 10, y + 60));
            e.Graphics.DrawString("0=发紫", zt9, Brushes.Black, x, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 90, y), new Point(x + 90, y + 60));
            e.Graphics.DrawString("0=敷药脱落，有渗液", zt9, Brushes.Black, x + 100, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 230, y), new Point(x + 230, y + 60));
            e.Graphics.DrawString("0=无自主呼吸", zt9, Brushes.Black, x + 240, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 340, y), new Point(x + 340, y + 60));
            e.Graphics.DrawString("0=人工气道，呼吸机支持", zt9, Brushes.Black, x + 350, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 520, y), new Point(x + 520, y + 60));
            e.Graphics.DrawString("0=无痛       1=轻度疼痛", zt9, Brushes.Black, x + 530, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 60));
            e.Graphics.DrawString("0=无恶心呕吐", zt9, Brushes.Black, x + 730, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 870, y), new Point(x + 870, y + 60));
            e.Graphics.DrawString("0=无反应", zt9, Brushes.Black, x + 920, y + 5);
            e.Graphics.DrawLine(ptp, new Point(x + 1050, y), new Point(x + 1050, y + 60));
            y = y + 20; y1 = y + 15;
           // e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(x - 10, y + 100));
            e.Graphics.DrawString("1=苍白", zt9, Brushes.Black, x, y + 5);
           // e.Graphics.DrawLine(ptp, new Point(x + 90, y), new Point(x + 90, y + 100));
            e.Graphics.DrawString("1=包扎好，有渗液", zt9, Brushes.Black, x + 100, y + 5);
           // e.Graphics.DrawLine(ptp, new Point(x + 230, y), new Point(x + 230, y + 100));
            e.Graphics.DrawString("1=有自主呼吸", zt9, Brushes.Black, x + 240, y + 5);
            //e.Graphics.DrawLine(ptp, new Point(x + 340, y), new Point(x + 340, y + 100));
            e.Graphics.DrawLine(ptp, new Point(x +520, y-2), new Point(x+720, y-2));//画横线
            e.Graphics.DrawString("1=通气/面罩支持", zt9, Brushes.Black, x + 350, y + 5);
           // e.Graphics.DrawLine(ptp, new Point(x + 520, y), new Point(x + 520, y + 100));
            e.Graphics.DrawString("2=轻/中度疼痛 3=中度疼痛", zt9, Brushes.Black, x + 530, y + 5);
            //e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 100));
            e.Graphics.DrawString("1=恶心/呕吐", zt9, Brushes.Black, x + 730, y + 5);
           // e.Graphics.DrawLine(ptp, new Point(x + 870, y), new Point(x + 870, y + 100));
            e.Graphics.DrawString("1=有反应", zt9, Brushes.Black, x + 920, y + 5);
           // e.Graphics.DrawLine(ptp, new Point(x + 1050, y), new Point(x + 1050, y + 100));
            y = y + 20; y1 = y + 15;
           // e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(x - 10, y + 100));
            e.Graphics.DrawString("2=红润", zt9, Brushes.Black, x, y + 5);
           // e.Graphics.DrawLine(ptp, new Point(x + 90, y), new Point(x + 90, y + 100));
            e.Graphics.DrawString("2=包扎好，无渗液", zt9, Brushes.Black, x + 100, y + 5);
          //  e.Graphics.DrawLine(ptp, new Point(x + 230, y), new Point(x + 230, y + 100));
            //e.Graphics.DrawString("1=有自主呼吸", zt9, Brushes.Black, x + 240, y + 5);
           // e.Graphics.DrawLine(ptp, new Point(x + 340, y), new Point(x + 340, y + 100));
            e.Graphics.DrawString("2=通气通畅", zt9, Brushes.Black, x + 350, y + 5);
          //  e.Graphics.DrawLine(ptp, new Point(x + 520, y), new Point(x + 520, y + 100));
            e.Graphics.DrawLine(ptp, new Point(x + 520, y - 2), new Point(x + 720, y - 2));//画横线
            e.Graphics.DrawString("4=重度疼痛 5=最严重疼痛", zt9, Brushes.Black, x + 530, y + 5);
           // e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 100));
            //e.Graphics.DrawString("1=恶心/呕吐", zt9, Brushes.Black, x + 730, y + 5);
           // e.Graphics.DrawLine(ptp, new Point(x + 870, y), new Point(x + 870, y + 100));
            e.Graphics.DrawString("2=清醒", zt9, Brushes.Black, x + 920, y + 5);
            //e.Graphics.DrawLine(ptp, new Point(x + 1050, y), new Point(x + 1050, y + 100));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(1100, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(x - 10, y + 80));
            e.Graphics.DrawString("时", zt9, Brushes.Black, x + 20, y + 20);
            e.Graphics.DrawString("间", zt9, Brushes.Black, x + 20, y + 40);
            e.Graphics.DrawLine(ptp, new Point(x +50, y), new Point(x+50, y + 80));
            e.Graphics.DrawString("生命体征", zt9, Brushes.Black, x + 130, y);
            e.Graphics.DrawLine(ptp, new Point(x + 50, y+20), new Point(x + 270, y+20));//画横线
            e.Graphics.DrawString("BP", zt9, Brushes.Black, x + 70, y + 30);
            e.Graphics.DrawString("mmHg", zt9, Brushes.Black, x + 65, y + 50);
            e.Graphics.DrawLine(ptp, new Point(x + 110, y+20), new Point(x + 110, y + 80));
            e.Graphics.DrawString("HR", zt9, Brushes.Black, x + 130, y + 30);
            e.Graphics.DrawString("次/分", zt9, Brushes.Black, x + 125, y + 50);
            e.Graphics.DrawLine(ptp, new Point(x + 170, y + 20), new Point(x + 170, y + 80));
            e.Graphics.DrawString("R", zt9, Brushes.Black, x + 195, y + 30);
            e.Graphics.DrawString("次/分", zt9, Brushes.Black, x + 185, y + 50);
            e.Graphics.DrawLine(ptp, new Point(x + 230, y + 20), new Point(x + 230, y + 80));
            e.Graphics.DrawString("SPO2", zt9, Brushes.Black, x + 240, y + 45);
            e.Graphics.DrawLine(ptp, new Point(x + 270, y), new Point(x + 270, y + 80));
            e.Graphics.DrawString("吸氧", zt9, Brushes.Black, x + 275, y);
            e.Graphics.DrawString("浓度", zt9, Brushes.Black, x + 275, y + 20);
            e.Graphics.DrawString("L/分", zt9, Brushes.Black, x + 275, y + 40);
            e.Graphics.DrawLine(ptp, new Point(x + 310, y), new Point(x + 310, y + 80));
            e.Graphics.DrawString("口", zt9, Brushes.Black, x + 320, y);
            e.Graphics.DrawString("唇", zt9, Brushes.Black, x + 320, y + 20);
            e.Graphics.DrawString("浓", zt9, Brushes.Black, x + 320, y + 40);
            e.Graphics.DrawString("度", zt9, Brushes.Black, x + 320, y + 60);
            e.Graphics.DrawLine(ptp, new Point(x + 340, y), new Point(x + 340, y + 80));
            e.Graphics.DrawString("伤", zt9, Brushes.Black, x + 350, y);
            e.Graphics.DrawString("口", zt9, Brushes.Black, x + 350, y + 20);
            e.Graphics.DrawString("情", zt9, Brushes.Black, x + 350, y + 40);
            e.Graphics.DrawString("况", zt9, Brushes.Black, x + 350, y + 60);
            e.Graphics.DrawLine(ptp, new Point(x + 370, y), new Point(x + 370, y + 80));
            e.Graphics.DrawString("自", zt9, Brushes.Black, x + 380, y);
            e.Graphics.DrawString("主", zt9, Brushes.Black, x + 380, y + 20);
            e.Graphics.DrawString("呼", zt9, Brushes.Black, x + 380, y + 40);
            e.Graphics.DrawString("吸", zt9, Brushes.Black, x + 380, y + 60);
            e.Graphics.DrawLine(ptp, new Point(x + 400, y), new Point(x + 400, y + 80));
            e.Graphics.DrawString("气", zt9, Brushes.Black, x + 410, y);
            e.Graphics.DrawString("道", zt9, Brushes.Black, x + 410, y + 20);
            e.Graphics.DrawString("情", zt9, Brushes.Black, x + 410, y + 40);
            e.Graphics.DrawString("况", zt9, Brushes.Black, x + 410, y + 60);
            e.Graphics.DrawLine(ptp, new Point(x + 430, y), new Point(x + 430, y + 80));
            e.Graphics.DrawString("疼", zt9, Brushes.Black, x + 440, y);
            e.Graphics.DrawString("痛", zt9, Brushes.Black, x + 440, y + 20);
            e.Graphics.DrawString("评", zt9, Brushes.Black, x + 440, y + 40);
            e.Graphics.DrawString("分", zt9, Brushes.Black, x + 440, y + 60);
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 80));
            e.Graphics.DrawString("恶", zt9, Brushes.Black, x + 470, y);
            e.Graphics.DrawString("心", zt9, Brushes.Black, x + 470, y + 20);
            e.Graphics.DrawString("呕", zt9, Brushes.Black, x + 470, y + 40);
            e.Graphics.DrawString("吐", zt9, Brushes.Black, x + 470, y + 60);
            e.Graphics.DrawLine(ptp, new Point(x + 490, y), new Point(x + 490, y + 80));
            e.Graphics.DrawString("意", zt9, Brushes.Black, x + 500, y);
            e.Graphics.DrawString("识", zt9, Brushes.Black, x + 500, y + 20);
            e.Graphics.DrawString("状", zt9, Brushes.Black, x + 500, y + 40);
            e.Graphics.DrawString("态", zt9, Brushes.Black, x + 500, y + 60);
            e.Graphics.DrawLine(ptp, new Point(x + 520, y), new Point(x + 520, y + 80));
            e.Graphics.DrawString("液体/用药", zt9, Brushes.Black, x + 600, y + 30);
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 80));
            e.Graphics.DrawLine(ptp, new Point(x -10, y + 80), new Point(x + 720, y + 80));//画横线
            e.Graphics.DrawString("出室状况：", zt9, Brushes.Black, x + 730, y);
            e.Graphics.DrawString("Steward苏醒评分：", zt9, Brushes.Black, x + 730, y+20);
            e.Graphics.DrawString("清醒程度：", zt9, Brushes.Black, x + 730, y + 40);
            e.Graphics.DrawRectangle(Pens.Black, x + 760, y+60, 12, 12);
            e.Graphics.DrawString("完全苏醒 2", zt9, Brushes.Black, x + 780, y+60);
            if (cmbQXCD.Text == "完全苏醒 2")
            {
                e.Graphics.DrawLine(pb2, x + 755, y+60, x + 765, y+60 + 12);
                e.Graphics.DrawLine(pb2, x + 765, y +60+ 12, x + 780, y+60 - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 760, y + 80, 12, 12);
            e.Graphics.DrawString("对刺激有反应 1", zt9, Brushes.Black, x + 780, y + 80);
            if (cmbQXCD.Text == "对刺激有反应 1")
            {
                e.Graphics.DrawLine(pb2, x + 755, y + 80, x + 765, y + 80 + 12);
                e.Graphics.DrawLine(pb2, x + 765, y + 80 + 12, x + 780, y + 80 - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 760, y + 100, 12, 12);
            e.Graphics.DrawString("对刺激无反应 0", zt9, Brushes.Black, x + 780, y + 100);
            if (cmbQXCD.Text == "对刺激无反应 0")
            {
                e.Graphics.DrawLine(pb2, x + 755, y + 100, x + 765, y + 100 + 12);
                e.Graphics.DrawLine(pb2, x + 765, y + 100 + 12, x + 780, y + 100 - 3);
            }
            e.Graphics.DrawString("呼吸道通畅程度：", zt9, Brushes.Black, x + 730, y + 120);
            e.Graphics.DrawRectangle(Pens.Black, x + 760, y + 140, 12, 12);
            e.Graphics.DrawString("可按医师吩咐咳嗽 2", zt9, Brushes.Black, x + 780, y + 140);
            if (cmbHXDTC.Text == "可按医师吩咐咳嗽 2")
            {
                e.Graphics.DrawLine(pb2, x + 755, y + 140, x + 765, y + 140 + 12);
                e.Graphics.DrawLine(pb2, x + 765, y + 140 + 12, x + 780, y + 140 - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 760, y + 160, 12, 12);
            e.Graphics.DrawString("可自主维持呼吸道通畅 1", zt9, Brushes.Black, x + 780, y + 160);
            if (cmbHXDTC.Text == "可自主维持呼吸道通畅 1")
            {
                e.Graphics.DrawLine(pb2, x + 755, y + 160, x + 765, y + 160 + 12);
                e.Graphics.DrawLine(pb2, x + 765, y + 160 + 12, x + 780, y + 160 - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 760, y + 180, 12, 12);
            e.Graphics.DrawString("呼吸道需要予以支持 0", zt9, Brushes.Black, x + 780, y + 180);
            if (cmbHXDTC.Text == "呼吸道需要予以支持 0")
            {
                e.Graphics.DrawLine(pb2, x + 755, y + 180, x + 765, y + 180 + 12);
                e.Graphics.DrawLine(pb2, x + 765, y + 180 + 12, x + 780, y + 180 - 3);
            }
            e.Graphics.DrawString("肢体活动度：", zt9, Brushes.Black, x + 730, y + 200);
            e.Graphics.DrawRectangle(Pens.Black, x + 760, y + 220, 12, 12);
            e.Graphics.DrawString("肢体能做有意识的活动 2", zt9, Brushes.Black, x + 780, y + 220);
            if (cmbZTHD.Text == "肢体能做有意识的活动 2")
            {
                e.Graphics.DrawLine(pb2, x + 755, y + 220, x + 765, y + 220 + 12);
                e.Graphics.DrawLine(pb2, x + 765, y + 220 + 12, x + 780, y + 220 - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 760, y + 240, 12, 12);
            e.Graphics.DrawString("肢体无意识活动 1", zt9, Brushes.Black, x + 780, y + 240);
            if (cmbZTHD.Text == "肢体无意识活动 1")
            {
                e.Graphics.DrawLine(pb2, x + 755, y + 240, x + 765, y + 240 + 12);
                e.Graphics.DrawLine(pb2, x + 765, y + 240 + 12, x + 780, y + 240 - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 760, y + 260, 12, 12);
            e.Graphics.DrawString("肢体无活动 0", zt9, Brushes.Black, x + 780, y + 260);
            if (cmbZTHD.Text == "肢体无意识活动 0")
            {
                e.Graphics.DrawLine(pb2, x + 755, y + 260, x + 765, y + 260 + 12);
                e.Graphics.DrawLine(pb2, x + 765, y + 260 + 12, x + 780, y + 260 - 3);
            }
            e.Graphics.DrawString("总分：" + txtZF.Text, zt9, Brushes.Black, x + 930, y + 260);
            e.Graphics.DrawLine(ptp, new Point(x - 10, y+280), new Point(x + 1050, y+280));//画横线
            int YY = y + 280;
            YY = YY + 10;
            string str1YY = "";
            int StrLengthYY = rtbXiaojie.Text.Trim().Length;
            int rowYY = StrLengthYY / 20;
            e.Graphics.DrawString("备注：", zt9, Brushes.Black, x + 730, YY);
            for (int i = 0; i <= rowYY; i++)//20个字符就换行
            {
                if (i < rowYY)
                    str1YY = rtbXiaojie.Text.Trim().ToString().Substring(i * 20, 20); //从第i*20个开始，截取20个字符串
                else
                    str1YY = rtbXiaojie.Text.Trim().ToString().Substring(i * 20);
                e.Graphics.DrawString(str1YY, zt9, Brushes.Black, x + 760, YY);
                YY = YY + 20; 
            }
            y = y + 80; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(x - 10, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 50, y), new Point(x + 50, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 110, y), new Point(x + 110, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 170, y), new Point(x + 170, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 230, y), new Point(x + 230, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 270, y), new Point(x + 270, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 310, y), new Point(x + 310, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 340, y), new Point(x + 340, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 370, y), new Point(x + 370, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 400, y), new Point(x + 400, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 430, y), new Point(x + 430, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 460, y), new Point(x + 460, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 490, y), new Point(x + 490, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 520, y), new Point(x + 520, y + 280));
            e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 280));
            //  PrintString = Convert.ToDateTime(dgvJHB.Rows[i].Cells[j].Value).ToString("HH:mm");
            //   e.Graphics.DrawString(PrintString, heiti8, Brushes.Black, x + 2, y + 5 + 25 * i);
            for (int i = 0; i < 6; i++)
            {
                if (i< dgvJHB.Rows.Count )
                {
                    e.Graphics.DrawString(Convert.ToDateTime(dgvJHB[1, i].Value).ToString("HH:mm"), zt9, Brushes.Black, x + 5, y + 10);
                    e.Graphics.DrawString(dgvJHB[2, i].Value.ToString(), zt9, Brushes.Black, x + 60, y + 10);
                    e.Graphics.DrawString(dgvJHB[3, i].Value.ToString(), zt9, Brushes.Black, x + 120, y + 10);
                    e.Graphics.DrawString(dgvJHB[4, i].Value.ToString(), zt9, Brushes.Black, x + 180, y + 10);
                    e.Graphics.DrawString(dgvJHB[5, i].Value.ToString(), zt9, Brushes.Black, x + 240, y + 10);
                    e.Graphics.DrawString(dgvJHB[7, i].Value.ToString(), zt9, Brushes.Black, x + 275, y + 10);
                    e.Graphics.DrawString(dgvJHB[9, i].Value.ToString(), zt9, Brushes.Black, x + 320, y + 10);
                    e.Graphics.DrawString(dgvJHB[10, i].Value.ToString(), zt9, Brushes.Black, x + 350, y + 10);
                    e.Graphics.DrawString(dgvJHB[8, i].Value.ToString(), zt9, Brushes.Black, x + 380, y + 10);
                    e.Graphics.DrawString(dgvJHB[11, i].Value.ToString(), zt9, Brushes.Black, x + 410, y + 10);
                    e.Graphics.DrawString(dgvJHB[12, i].Value.ToString(), zt9, Brushes.Black, x + 440, y + 10);
                    e.Graphics.DrawString(dgvJHB[13, i].Value.ToString(), zt9, Brushes.Black, x + 470, y + 10);
                    e.Graphics.DrawString(dgvJHB[6, i].Value.ToString(), zt9, Brushes.Black, x + 500, y + 10);
                    //计算用药的换行
                    int BZYY = y, BZYY1 = y1;                   
                    string str1 = "";
                    int StrLength = dgvJHB[14, i].Value.ToString().Length;
                    int row = StrLength / 20;
                    //e.Graphics.DrawString("备注：", zt9, Brushes.Black, x + 550, BZYY);
                    for (int ii = 0; ii <= row; ii++)//11个字符就换行
                    {
                        if (ii < row)
                            str1 = dgvJHB[14, i].Value.ToString().Substring(ii * 20, 20); //从第i*11个开始，截取11个字符串
                        else
                            str1 = dgvJHB[14, i].Value.ToString().Substring(ii * 20);
                        e.Graphics.DrawString(str1, zt9, Brushes.Black, x + 530, BZYY);
                        BZYY = BZYY + 15;
                    }
                    //e.Graphics.DrawString(dgvJHB[14, i].Value.ToString(), zt9, Brushes.Black, x + 530, y + 10);
                    y = y + 40; y1 = y + 15;
                    e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(x + 720, y));//画横线
                }
                else
                {
                    y = y + 40; y1 = y + 15;
                    e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(x + 720, y ));//画横线
                }          
            }
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(ptp, new Point(x - 10, y), new Point(x + 1050, y));//画横线
            e.Graphics.DrawLine(ptp, new Point(x + 1050, SX), new Point(x + 1050, y));//画竖线
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("PACU医师：" + cmbPACUYS.Text, zt9, Brushes.Black, x, y);
            e.Graphics.DrawLine(ptp, new Point(x + 60, y1), new Point(x + 250, y1));
            e.Graphics.DrawString("PACU护士：" + cmbRecorder.Text, zt9, Brushes.Black, x + 300, y);
            e.Graphics.DrawLine(ptp, new Point(x + 360, y1), new Point(x + 500, y1));
            e.Graphics.DrawString("转归：", zt9, Brushes.Black, x+550, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 600, y, 12, 12);
            e.Graphics.DrawString("病房", zt9, Brushes.Black, x + 630, y);
            if (cmbBRQX.Text == "病房")
            {
                e.Graphics.DrawLine(pb2, x + 595, y, x + 605, y + 12);
                e.Graphics.DrawLine(pb2, x + 605, y + 12, x + 620, y - 3);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 700, y, 12, 12);
            e.Graphics.DrawString("ICU", zt9, Brushes.Black, x + 730, y);
            if (cmbBRQX.Text == "ICU")
            {
                e.Graphics.DrawLine(pb2, x + 695, y, x + 705, y + 12);
                e.Graphics.DrawLine(pb2, x + 705, y + 12, x + 720, y - 3);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("出室总入量：" + tbZongRL.Text, zt9, Brushes.Black, x, y);
            e.Graphics.DrawLine(ptp, new Point(x + 70, y1), new Point(x + 250, y1));
            e.Graphics.DrawString("尿管出室量：" + tbNiaoliang.Text, zt9, Brushes.Black, x + 300, y);
            e.Graphics.DrawLine(ptp, new Point(x + 370, y1), new Point(x + 500, y1));
            e.Graphics.DrawString("离开复苏室的时间：" + DTLSTime.Text, zt9, Brushes.Black, x + 550, y);
            e.Graphics.DrawLine(ptp, new Point(x + 660, y1), new Point(x + 870, y1));
            //int x = 25;
            //int y = 0;
            //int y1 = y + 15;//下划线坐标
            //int width = e.MarginBounds.Width;
            //int height = e.MarginBounds.Height;
            //int w = dgvJHB.Width;   //非显示的宽度,实际为最大显示宽度，超过此宽度则会出现滚动条
            //int h = dgvJHB.Height;  //非显示的高度，实际为最大显示高度，超过此高度则会出现分页
            //double rate1 = (w * 1.00) / (width * 1.00);
            //double rate2 = (h * 1.00) / (height * 1.00);
            //double rate = (rate1 > rate2) ? rate1 : rate2;
            //// w = (int)(w * 1.00 / rate);
            //// h = (int)(h * 1.00 / rate);
            //int col_count = dgvJHB.ColumnCount;
            //int row_count = dgvJHB.RowCount;
            //w = 0;
            //for (int i = 0; i < col_count; i++) w += (int)(dgvJHB.Columns[i].Width * 1.00 / rate);
            //int row_height = (int)(dgvJHB.Rows[0].Height * 1.00 / rate);
            //h = row_count * row_height;
            //Font myfont = dgvJHB.Font;
            //Font title = new Font(new FontFamily("黑体"), 13);
            //Font heiti = new Font(new FontFamily("楷体"), 9);
            //Font heiti8 = new Font(new FontFamily("楷体"), 8);
            //Font textfront = new Font(new FontFamily("宋体"), 9);
            //Pen pb2 = new Pen(Brushes.Black, 2);

            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawString("昌吉州人民医院麻醉恢复室(PACU)观察记录单", title, System.Drawing.Brushes.Black, x + 200, y);
            //y = y + 25; y1 = y + 15;
            //e.Graphics.DrawString("手术日期：" + dtDate.Text, heiti, System.Drawing.Brushes.Black, x + 140, y);
            //e.Graphics.DrawLine(Pens.Black, x + 200, y1, x + 300, y1);
            //e.Graphics.DrawString("住院号：" + tbZhuyuanNo.Text, heiti, System.Drawing.Brushes.Black, x + 350, y);
            //e.Graphics.DrawLine(Pens.Black, x + 390, y1, x + 540, y1);
            //y = y + 25; y1 = y + 15;
            //e.Graphics.DrawLine(Pens.Black, x, y, x + 760, y);//区域分割横线
            //y = y + 5; y1 = y + 15;
            //e.Graphics.DrawString("姓名：" + tbPatname.Text, heiti, System.Drawing.Brushes.Black, x, y);
            //e.Graphics.DrawLine(Pens.Black, x + 35, y1, x + 150, y1);
            //e.Graphics.DrawString("性别：" + tbSex.Text, heiti, System.Drawing.Brushes.Black, x + 160, y);
            //e.Graphics.DrawLine(Pens.Black, x + 190, y1, x + 240, y1);
            //e.Graphics.DrawString("年龄：" + tbAge.Text, heiti, System.Drawing.Brushes.Black, x + 260, y);
            //e.Graphics.DrawLine(Pens.Black, x + 290, y1, x + 440, y1);
            //e.Graphics.DrawString("床号：" + tbBedNO.Text, heiti, System.Drawing.Brushes.Black, x + 460, y);
            //e.Graphics.DrawLine(Pens.Black, x + 490, y1, x + 560, y1);
            //e.Graphics.DrawString("拔管(吼罩)时间：" + dtBGtime.Value.ToString("HH:mm"), heiti, System.Drawing.Brushes.Black, x + 580, y);
            //e.Graphics.DrawLine(Pens.Black, x + 670, y1, x + 740, y1);

            ////第二行
            //y = y + 25; y1 = y + 15;
            //e.Graphics.DrawString("诊断：" + tbSZZD.Text, heiti, System.Drawing.Brushes.Black, x, y);
            //e.Graphics.DrawLine(Pens.Black, x + 35, y1, x + 740, y1);

            //y = y + 25; y1 = y + 15;
            //e.Graphics.DrawString("麻醉方式：" + tbMZFF.Text, heiti, System.Drawing.Brushes.Black, x, y);
            //e.Graphics.DrawLine(Pens.Black, x + 55, y1, x + 300, y1);
            //e.Graphics.DrawString("手术名称：" + tbSSMC.Text, heiti, System.Drawing.Brushes.Black, x + 310, y);
            //e.Graphics.DrawLine(Pens.Black, x + 370, y1, x + 740, y1);
            //y = y + 25; y1 = y + 15;
            //e.Graphics.DrawString("随时管道：", heiti, System.Drawing.Brushes.Black, x + 5, y);

            //e.Graphics.DrawRectangle(Pens.Black, x + 105, y, 12, 12);
            //if (cbQGCG.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 100, y, x + 110, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 110, y + 12, x + 125, y - 2);
            //}
            //e.Graphics.DrawString("气管插管   位置：" + cmbCGweizhi.Text, heiti, System.Drawing.Brushes.Black, x + 130, y);
            //e.Graphics.DrawLine(Pens.Black, x + 230, y1, x + 300, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 335, y, 12, 12);
            //if (cbSXTD.Checked)
            //{
            //    e.Graphics.DrawLine(pb2, x + 330, y, x + 340, y + 12);
            //    e.Graphics.DrawLine(pb2, x + 340, y + 12, x + 355, y - 2);
            //}
            //e.Graphics.DrawString("输液通道", heiti, System.Drawing.Brushes.Black, x + 360, y);

            //y = y + 25; y1 = y + 15;

            //e.Graphics.DrawLine(Pens.Black, x, y, x + 760, y);//区域分割横线
            //y = y + 10; y1 = y + 15;
            //int SK = y, QDQK = y, ZT = y, EX = y, KC = y;
            //e.Graphics.DrawString("口唇颜色", heiti, System.Drawing.Brushes.Black, x + 10, KC);
            //KC = KC + 15;

            //foreach (string item in lisBox1.Items)
            //{
            //    e.Graphics.DrawString(item, heiti, System.Drawing.Brushes.Black, x + 10, KC);
            //    KC = KC + 15;
            //}
            //e.Graphics.DrawLine(Pens.Black, x + 80, y - 10, x + 80, y + 65);
            //e.Graphics.DrawString("伤口情况", heiti, System.Drawing.Brushes.Black, x + 100, SK);
            //SK = SK + 15;
            //foreach (string item in listBox1.Items)
            //{
            //    e.Graphics.DrawString(item, heiti, System.Drawing.Brushes.Black, x + 100, SK);
            //    SK = SK + 15;
            //}
            //e.Graphics.DrawLine(Pens.Black, x + 240, y - 10, x + 240, y + 65);
            //e.Graphics.DrawString("气道情况", heiti, System.Drawing.Brushes.Black, x + 245, QDQK);
            //QDQK = QDQK + 15;
            //foreach (string item in listBox2.Items)
            //{
            //    e.Graphics.DrawString(item, heiti, System.Drawing.Brushes.Black, x + 245, QDQK);
            //    QDQK = QDQK + 15;
            //}
            //e.Graphics.DrawLine(Pens.Black, x + 400, y - 10, x + 400, y + 65);
            //e.Graphics.DrawString("       疼痛评分", heiti, System.Drawing.Brushes.Black, x + 420, ZT);
            //ZT = ZT + 15;
            //e.Graphics.DrawString("0—无痛         1—轻度疼痛", heiti, System.Drawing.Brushes.Black, x + 420, ZT);
            //ZT = ZT + 15;
            //e.Graphics.DrawString("2—轻/中度疼痛  3—中度疼痛", heiti, System.Drawing.Brushes.Black, x + 420, ZT);
            //ZT = ZT + 15;
            //e.Graphics.DrawString("4—重度疼痛     5—最严重疼痛", heiti, System.Drawing.Brushes.Black, x + 420, ZT);
            //ZT = ZT + 15;
            //e.Graphics.DrawLine(Pens.Black, x + 630, y - 10, x + 630, y + 65);
            //e.Graphics.DrawString("恶心呕吐", heiti, System.Drawing.Brushes.Black, x + 640, EX);
            //EX = EX + 15;
            //foreach (string item in listBox4.Items)
            //{
            //    e.Graphics.DrawString(item, heiti, System.Drawing.Brushes.Black, x + 640, EX);
            //    EX = EX + 15;
            //}

            //y = y + 65; y1 = y + 15;
            //e.Graphics.DrawLine(Pens.Black, x, y, x + 760, y);//区域分割横线
            //e.Graphics.DrawString("时间", heiti, Brushes.Black, x + 5, y + 15);
            //e.Graphics.DrawString("血压\nmmHg", heiti, Brushes.Black, x + 45, y + 5);
            //e.Graphics.DrawString("脉搏\n次/分", heiti, Brushes.Black, x + 85, y + 5);
            //e.Graphics.DrawString("呼吸\n次/分", heiti, Brushes.Black, x + 125, y + 5);
            //e.Graphics.DrawString("SpO2\n (%)", heiti, Brushes.Black, x + 165, y + 5);
            //e.Graphics.DrawString("意识\n状态", heiti, Brushes.Black, x + 205, y + 5);
            //e.Graphics.DrawString("吸氧\n浓度", heiti, Brushes.Black, x + 245, y + 5);
            //e.Graphics.DrawString("自主\n呼吸", heiti, Brushes.Black, x + 285, y + 5);
            
            //e.Graphics.DrawString("口唇\n颜色", heiti, Brushes.Black, x + 325, y + 5);
            //e.Graphics.DrawString("伤口\n情况", heiti, Brushes.Black, x + 365, y + 5);
            //e.Graphics.DrawString("气道\n情况", heiti, Brushes.Black, x + 405, y + 5);
            //e.Graphics.DrawString("恶心\n呕吐", heiti, Brushes.Black, x + 445, y + 5);
            //e.Graphics.DrawString("疼痛\n评分", heiti, Brushes.Black, x + 485, y + 5);
            //e.Graphics.DrawString("液体用药", heiti, Brushes.Black, x + 555, y + 15);
            //e.Graphics.DrawString("签名", heiti, Brushes.Black, x + 715, y + 15);

            ////e.Graphics.DrawLine(Pens.Black, x, y, x, y + 550);
            //for (int j = 1; j < 14; j++)
            //{
            //    e.Graphics.DrawLine(Pens.Black, x + 40 * j, y, x + 40 * j, y + 715);
            //}
            //e.Graphics.DrawLine(Pens.Black, x + 710, y, x + 710, y + 715);
            //y = y + 40; y1 = y + 15;
            //for (int i = 0; i <= 27; i++)
            //{
            //    e.Graphics.DrawLine(Pens.Black, x, y + 25 * i, x + 760, y + 25 * i);
            //}
            //string PrintString = "";
            //for (int i = 0; i < dgvJHB.Rows.Count; i++)
            //{
            //    for (int j = 1; j < dgvJHB.Columns.Count; j++)
            //    {
            //        if (dgvJHB.Rows[i].Cells[j].Value != null)
            //        {
            //            if (j == 1)
            //            {
            //                PrintString = Convert.ToDateTime(dgvJHB.Rows[i].Cells[j].Value).ToString("HH:mm");
            //                e.Graphics.DrawString(PrintString, heiti8, Brushes.Black, x + 2, y + 5 + 25 * i);
            //            }
            //            else if (j == 15)
            //            {
            //                PrintString = dgvJHB.Rows[i].Cells[j].Value.ToString();
            //                e.Graphics.DrawString(PrintString, heiti8, Brushes.Black, x + 712, y + 5 + 25 * i);
            //            }
            //            else
            //            {
            //                PrintString = dgvJHB.Rows[i].Cells[j].Value.ToString();
            //                e.Graphics.DrawString(PrintString, heiti8, Brushes.Black, x - 38 + 40 * j, y + 5 + 25 * i);
            //            }
            //        }
            //    }
            //}


            //y = y + 25 * 27; y1 = y + 15;    



            //y = y + 25; y1 = y + 15;
            //e.Graphics.DrawString("出室小结：", heiti, System.Drawing.Brushes.Black, x + 10, y);
            //if (rtbXiaojie.Text.Length > 50)
            //{
            //    e.Graphics.DrawString(rtbXiaojie.Text.Substring(0, 50), heiti, System.Drawing.Brushes.Black, x + 70, y);
            //    e.Graphics.DrawString(rtbXiaojie.Text.Substring(51), heiti, System.Drawing.Brushes.Black, x + 70, y + 25);
            //}
            //else
            //{
            //    e.Graphics.DrawString(rtbXiaojie.Text, heiti, System.Drawing.Brushes.Black, x + 70, y);

            //}
            //e.Graphics.DrawLine(Pens.Black, x + 65, y1, x + 750, y1);
            //y = y + 25; y1 = y + 15;
            //e.Graphics.DrawLine(Pens.Black, x + 10, y1, x + 750, y1);
            //y = y + 25; y1 = y + 15;
            //e.Graphics.DrawString("麻醉医师：" + cmbMZYS.Text.Trim(), heiti, System.Drawing.Brushes.Black, x + 10, y);
            //e.Graphics.DrawLine(Pens.Black, x + 65, y1, x + 160, y1);
            //e.Graphics.DrawString("PACU记录者：" + cmbRecorder.Text.Trim(), heiti, System.Drawing.Brushes.Black, x + 200, y);
            //e.Graphics.DrawLine(Pens.Black, x + 275, y1, x + 380, y1);
            //e.Graphics.DrawString("病人去向：" + cmbBRQX.Text.Trim(), heiti, System.Drawing.Brushes.Black, x + 420, y);
            //e.Graphics.DrawLine(Pens.Black, x + 480, y1, x + 560, y1);
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawLine(Pens.Black, x, y, x + 760, y);//区域分割横线
            //e.Graphics.DrawLine(Pens.Black, x, 80, x, y);//竖线
            //e.Graphics.DrawLine(Pens.Black, x + 760, 80, x + 760, y);//竖线
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnEnd.Enabled = true;
            btnStart.Enabled = false;
            btnPrint.Enabled = false;
            btnSave.Enabled = false;
            timer1.Enabled = true;
            pick_count = 0;
            max_pickCount = int.Parse(cmbJCSJJG.Text.Trim()) * 60;
            timer1.Interval = 1000 * int.Parse(cmbJCSJJG.Text);//改变timer的间隔时间
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnEnd.Enabled = false;
            btnPrint.Enabled = true;
            btnSave.Enabled = true;
            timer1.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                Dictionary<string, string> ChuShiInfo = new Dictionary<string, string>();
                ChuShiInfo.Add("mzjldid", MZID);
                ChuShiInfo.Add("patid", PATID);
                string ssgd = "";
                if (cbQGCG.Checked)
                    ssgd += "1";
                if (cbSXTD.Checked)
                    ssgd += "2";
                ChuShiInfo.Add("SSGD", ssgd);
                ChuShiInfo.Add("CGweizhi", cmbCGweizhi.Text.Trim());
                ChuShiInfo.Add("ZongRL", tbZongRL.Text.Trim());
                ChuShiInfo.Add("Niaoliang", tbNiaoliang.Text.Trim());
                ChuShiInfo.Add("VisitTime", tbVisitTime.Text.Trim());
                ChuShiInfo.Add("Xiaojie.", rtbXiaojie.Text.Trim());
                ChuShiInfo.Add("MZYS", cmbMZYS.Text.Trim());
                ChuShiInfo.Add("BRQX", cmbBRQX.Text.Trim());
                ChuShiInfo.Add("Recorder", cmbRecorder.Text.Trim());
                ChuShiInfo.Add("IsRead", "0");
                ChuShiInfo.Add("AddTime", DateTime.Now.ToString());
                ChuShiInfo.Add("QXCD",cmbQXCD.Text.Trim());
                ChuShiInfo.Add("HXDTC", cmbHXDTC.Text.Trim());
                ChuShiInfo.Add("ZTHD", cmbZTHD.Text.Trim());
                ChuShiInfo.Add("PF",txtZF.Text.Trim());
                ChuShiInfo.Add("LKTime",Convert.ToDateTime(DTLSTime.Text.Trim()).ToString());
                ChuShiInfo.Add("RSTime",Convert.ToDateTime(DTRSTime.Text.Trim()).ToString());
                ChuShiInfo.Add("BGtime",Convert.ToDateTime(dtBGtime.Text.Trim()).ToString());
                ChuShiInfo.Add("PACUYS", cmbPACUYS.Text.Trim().ToString());
                int flag = 0;
                if (bll.PACU_info(MZID).Rows.Count == 0)
                    flag = bll.InsertPACU(ChuShiInfo);
                else
                    flag = bll.UpdatePACU(ChuShiInfo);
                if (flag == 0)
                {
                    MessageBox.Show("保存失败");
                }
                else
                    MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void dgvJHB_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
             try
            {
                if (dgvJHB.Rows.Count > 0)
                {
                   
                    if (e.ColumnIndex==1)
                    {
                        if (dgvJHB.CurrentCell.Value.ToString()=="")
                        {
                            MessageBox.Show("时间不能为空，请重新修改");
                            return;
                        }                       
                    }                   
                        DateTime dtime = Convert.ToDateTime(dgvJHB.CurrentRow.Cells["Timeshort"].Value);
                        string ID = dgvJHB.CurrentRow.Cells["ID"].Value.ToString();
                        string RoomName = dgvJHB.Columns[dgvJHB.CurrentCell.ColumnIndex].Name;
                        string Value_Info = dgvJHB.CurrentCell.Value.ToString();
                        bll.UpdatePACU_Data(ID, RoomName, Value_Info);
                    
                  
                } 
            }
            catch (Exception)
            {
                MessageBox.Show("时间输入格式不正确！\n输入格式为HH:MM");
                BindPACUData();
                
            }   
        }
        //private void dgvJHB_CellValueChanged(object sender, EventArgs e)
        //{
           

        //}

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvJHB.Rows.Count==0)
            {
                string dtTime = DateTime.Now.ToString("HH:mm");
                bll.AddPACU_DATA(MZID, dtTime);
                BindPACUData();
            }
            else  if (dgvJHB.Rows.Count > 0)
            {
                if (dgvJHB.CurrentCell.IsInEditMode == false)
                {
                    string dtTime = DateTime.Now.ToString("HH:mm");
                    bll.AddPACU_DATA(MZID, dtTime);
                    BindPACUData();
                }
                else MessageBox.Show("单元格内不能为编辑状态！");
            }
                       
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string idno = dgvJHB.CurrentRow.Cells["id"].Value.ToString();
            bll.DeletePACU_DATA(idno);
            BindPACUData();
        }
        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string idno = dgvJHB.CurrentRow.Cells["id"].Value.ToString();
            PACU_Add_JCD f1 = new PACU_Add_JCD(MZID, idno, 2);
            f1.ShowDialog();
            BindPACUData();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            int i= bll.UpdatePACU(MZID,1);
            if (i>0)
            {
                btnSave.Enabled = false;
                btnLock.Enabled = false;
                btnUnlock.Enabled = true;
                DeleteToolStripMenuItem.Enabled = false;
                AddToolStripMenuItem.Enabled = false;

            }
            
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            int i = bll.UpdatePACU(MZID, 0);
            if (i > 0)
            {
                btnSave.Enabled = true;
                btnLock.Enabled = true;
                btnUnlock.Enabled = false;
                DeleteToolStripMenuItem.Enabled = true;
                AddToolStripMenuItem.Enabled = true;
            }
        }

        private void cmbJCSJJG_SelectedIndexChanged(object sender, EventArgs e)
        {
            bll.UpdatePACU(MZID, cmbJCSJJG.Text.Trim());
        }
        private void dgvJHB_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJHB.Rows.Count > 0)
            {
                if (dgvJHB.CurrentCell.ColumnIndex == 14)
                {
                    int rowindex=dgvJHB.CurrentCell.RowIndex;
                    int columIndex=dgvJHB.CurrentCell.ColumnIndex;
                    int mzid=int.Parse(MZID);
                    string theValue="";
                    string ID = dgvJHB.CurrentRow.Cells["ID"].Value.ToString();
                    string RoomName = dgvJHB.Columns[dgvJHB.CurrentCell.ColumnIndex].Name;
                    string Value_Info = dgvJHB.CurrentCell.Value.ToString();
                    PACU_AddSY f1 = new PACU_AddSY(mzid, ID,RoomName, Value_Info);
                    f1.ShowDialog();
                    BindPACUData();
                }
            }

        }
        private void dgvJHB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvJHB.Rows.Count > 0)
            {
                //if (dgvJHB.CurrentCell.ColumnIndex == 1)
                //{
                //    Rectangle rect = dgvJHB.GetCellDisplayRectangle(dgvJHB.CurrentCell.ColumnIndex, dgvJHB.CurrentCell.RowIndex, false);
                //    dtPicker.Left = rect.Left; dtPicker.Top = rect.Top;
                //    dtPicker.Width = rect.Width; dtPicker.Height = rect.Height;
                //    dtPicker.Visible = true; 
                //}

                if (dgvJHB.CurrentCell.ColumnIndex == 6)
                {
                    Rectangle rect = dgvJHB.GetCellDisplayRectangle(dgvJHB.CurrentCell.ColumnIndex, dgvJHB.CurrentCell.RowIndex, false);
                    cmbYishi.Left = rect.Left; cmbYishi.Top = rect.Top;
                    cmbYishi.Width = rect.Width; cmbYishi.Height = rect.Height;
                    cmbYishi.Visible = true; cmbYishi.Text = "";
                }
                else if (dgvJHB.CurrentCell.ColumnIndex == 8)
                {
                    Rectangle rect = dgvJHB.GetCellDisplayRectangle(dgvJHB.CurrentCell.ColumnIndex, dgvJHB.CurrentCell.RowIndex, false);
                    cmbZZHXS.Left = rect.Left; cmbZZHXS.Top = rect.Top;
                    cmbZZHXS.Width = rect.Width; cmbZZHXS.Height = rect.Height;
                    cmbZZHXS.Visible = true; cmbZZHXS.Text = "";
                }
                else if (dgvJHB.CurrentCell.ColumnIndex == 9)
                {
                    Rectangle rect = dgvJHB.GetCellDisplayRectangle(dgvJHB.CurrentCell.ColumnIndex, dgvJHB.CurrentCell.RowIndex, false);
                    cmbKCYS.Left = rect.Left;cmbKCYS.Top = rect.Top;
                    cmbKCYS.Width = rect.Width;cmbKCYS.Height = rect.Height;
                    cmbKCYS.Visible = true; cmbKCYS.Text = "";
                }
                else if (dgvJHB.CurrentCell.ColumnIndex == 10)
                {
                    Rectangle rect = dgvJHB.GetCellDisplayRectangle(dgvJHB.CurrentCell.ColumnIndex, dgvJHB.CurrentCell.RowIndex, false);
                    cmbSKQK.Left = rect.Left;cmbSKQK.Top = rect.Top;
                    cmbSKQK.Width = rect.Width; cmbSKQK.Height = rect.Height;
                    cmbSKQK.Visible = true; cmbSKQK.Text = "";
                }
                else if (dgvJHB.CurrentCell.ColumnIndex == 11)
                {
                    Rectangle rect = dgvJHB.GetCellDisplayRectangle(dgvJHB.CurrentCell.ColumnIndex, dgvJHB.CurrentCell.RowIndex, false);
                    cmbQDQK.Left = rect.Left; cmbQDQK.Top = rect.Top;
                    cmbQDQK.Width = rect.Width; cmbQDQK.Height = rect.Height;
                    cmbQDQK.Visible = true; cmbQDQK.Text = "";
                }
                else if (dgvJHB.CurrentCell.ColumnIndex == 12)
                {
                    Rectangle rect = dgvJHB.GetCellDisplayRectangle(dgvJHB.CurrentCell.ColumnIndex, dgvJHB.CurrentCell.RowIndex, false);
                    cmbEXOT.Left = rect.Left; cmbEXOT.Top = rect.Top;
                    cmbEXOT.Width = rect.Width; cmbEXOT.Height = rect.Height;
                    cmbEXOT.Visible = true; cmbEXOT.Text = "";
                }
                else if (dgvJHB.CurrentCell.ColumnIndex == 13)
                {
                    Rectangle rect = dgvJHB.GetCellDisplayRectangle(dgvJHB.CurrentCell.ColumnIndex, dgvJHB.CurrentCell.RowIndex, false);
                    cmbTTPF.Left = rect.Left; cmbTTPF.Top = rect.Top;
                    cmbTTPF.Width = rect.Width; cmbTTPF.Height = rect.Height;
                    cmbTTPF.Visible = true; cmbTTPF.Text = "";

                }
                else if (dgvJHB.CurrentCell.ColumnIndex == 15)
                {
                    Rectangle rect = dgvJHB.GetCellDisplayRectangle(dgvJHB.CurrentCell.ColumnIndex, dgvJHB.CurrentCell.RowIndex, false);
                    cmbRecorder1.Left = rect.Left; cmbRecorder1.Top = rect.Top;
                    cmbRecorder1.Width = rect.Width; cmbRecorder1.Height = rect.Height;
                    cmbRecorder1.Visible = true; cmbRecorder1.Text = "";
                    
                }
                else
                {
                    cmbRecorder1.Visible = false;
                    cmbEXOT.Visible = false;
                    cmbTTPF.Visible = false;
                    cmbSKQK.Visible = false;
                    cmbKCYS.Visible = false;
                    cmbYishi.Visible = false;
                    cmbZZHXS.Visible = false;
                }

            }
        }

        private void PACU_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                panel1.Location = new Point(130, 10);
            }
            else if (WindowState == FormWindowState.Normal)
            {
                panel1.Location = new Point(10, 10);
            } 
        }

        private void tbZongRL_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e); 
        }

        private void tbNiaoliang_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e); 
        }

        private void tbVisitTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e); 
        }
        /// <summary>
        /// 评分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPF_Click(object sender, EventArgs e)
        {
            int PF = 0;
            if (cmbQXCD.SelectedIndex==2)
            {
                PF += 2;
            }
            else if (cmbQXCD.SelectedIndex == 1)
            {
                PF += 1;
            }
            else
            {
                PF += 0;
            }

            if (cmbHXDTC.SelectedIndex == 2)
            {
                PF += 2;
            }
            else if (cmbHXDTC.SelectedIndex == 1)
            {
                PF += 1;
            }
            else
            {
                PF += 0;
            }
            if (cmbZTHD.SelectedIndex == 2)
            {
                PF += 2;
            }
            else if (cmbZTHD.SelectedIndex == 1)
            {
                PF += 1;
            }
            else
            {
                PF += 0;
            }
            txtZF.Text = PF.ToString();
           
        }

   

     

      

        

                
    }
}

