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

namespace main.用户安全
{
    public partial class PACU : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// 

        string MZID, PATID;
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        public PACU( string mzid, string patid)
        {
             MZID=mzid;
             PATID = patid;
            InitializeComponent();
        }
        public PACU()
        {
           
            InitializeComponent();
        }

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_MODEL.PACU_record record = new adims_MODEL.PACU_record();
        //  DataSet myset = new DataSet();
        //  SqlDataAdapter adp = new SqlDataAdapter();
        int pick_count = 0;
        int max_pickCount = 0;
        static string con_str = "server=.;database=BLT_database;uid=sa;password=sa123456";
        SqlConnection con = new SqlConnection(con_str);
        private void PACU_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“bLT_DatabaseDataSet.xueya_Data”中。您可以根据需要移动或删除它。
            
            comboBox1.Text = comboBox1.Items[0].ToString();
            BindPatInfo();
            bindPACUinfo();
            //BindPACUData();
            BindPatMZInfo();
           
          
        }
        private void BindPACUData() 
        {
           DataTable dt= bll.allPACU_data(MZID);
           dgvJC_Table.DataSource = dt.DefaultView;
        
        }
        private void bindPACUinfo()
        {

            DataTable dt = bll.PACU_info(MZID);
            if (dt.Rows.Count!=0)
            {
                observeTime_txt.Text = dt.Rows[0]["observeTime"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["visitDate"].ToString());
                rtbTeshuwenti.Text=dt.Rows[0]["cautions"].ToString();
                Jisonghuifu.Text = dt.Rows[0]["Jisonghuifu"].ToString();
                Keshoutunyan.Text = dt.Rows[0]["Keshoutunyan"].ToString();
                Taitou5miao.Text = dt.Rows[0]["Taitou5miao"].ToString();
                Yishi.Text = dt.Rows[0]["Yishi"].ToString();
                Qiguandaoguan.Text = dt.Rows[0]["Jisonghuifu"].ToString();
                Dingxiangli.Text = dt.Rows[0]["Qiguandaoguan"].ToString();
                MZpingmianUP.Text = dt.Rows[0]["MZpingmianUP"].ToString();
                MZpingmianDOWN.Text = dt.Rows[0]["MZpingmianDOWN"].ToString();
                Tengtong.Text = dt.Rows[0]["Tengtong"].ToString();
                Exin.Text = dt.Rows[0]["Exin"].ToString();
                Jingmaitongchang.Text = dt.Rows[0]["Jingmaitongchang"].ToString();
                ZhentongFF.Text = dt.Rows[0]["ZhentongFF"].ToString();  
                
            } 
        
        }

        private void BindPatInfo()
        {
            tbMZID.Text = MZID;
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(PATID);
            tbZhuyuanID.Text = dt.Rows[0]["PatZhuYuanID"].ToString();
            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbSex.Text = dt.Rows[0]["Patsex"].ToString();          
            tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
            Oname_txt.Text = dt.Rows[0]["Oname"].ToString();
            szzd_txt.Text = dt.Rows[0]["Pattmd"].ToString();
            
        }
        private void BindPatMZInfo()
        {
            
            DataTable dt = new DataTable();
            dt = DAL.GetMZJLD_Info(MZID);
            
            tbMZFF.Text = dt.Rows[0]["mzfa"].ToString();
            cmbJiSongHuiFu.Text = dt.Rows[0]["jisonghuifu"].ToString();
            cmbKeSouTunYan.Text = dt.Rows[0]["kesoutunyan"].ToString();
            cmbTaiTou5Miao.Text = dt.Rows[0]["taitou5miao"].ToString();
            cmbYishi.Text = dt.Rows[0]["yishi"].ToString();
            cmbQGDG.Text = dt.Rows[0]["Jisonghuifu"].ToString();
            cmbDingXiangLi.Text = dt.Rows[0]["dingxiangli"].ToString();
            cmbMaZuiPingMianDOWN.Text = dt.Rows[0]["mazuipingmianUP"].ToString();
            cmbMaZuiPingMianDOWN.Text = dt.Rows[0]["mazuipingmianDOWN"].ToString();
            cmbTengTong.Text = dt.Rows[0]["tengtong"].ToString();
            cmbEXinOuTu.Text = dt.Rows[0]["exinoutu"].ToString();
            cmbJingMaiTongChang.Text = dt.Rows[0]["jingmaitongchang"].ToString();
            this.cmbZhenTongFangFa.Text = dt.Rows[0]["zhentongfangfa"].ToString(); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string short_time = "";
            try
            {
               
                for (int i = 0; i < dgvJC_Table.Rows.Count; i++)
                {
                  
                    if (dgvJC_Table.Rows[i].Cells[0].Value != null)
                    {
                        Dictionary<string, string> pacu_data = new Dictionary<string, string>();

                        if (dgvJC_Table[0, i].Value != null)
                        {
                            pacu_data.Add("timeshort", dgvJC_Table.Rows[i].Cells[0].Value.ToString());
                            short_time = dgvJC_Table.Rows[i].Cells[0].Value.ToString();
                        }

                        else

                            pacu_data.Add("timeshort", "");
                            
                        
                        if (dgvJC_Table[1, i].Value != null)     
                        pacu_data.Add("xueya", dgvJC_Table.Rows[i].Cells[1].Value.ToString());
                        else
                            pacu_data.Add("xueya", "");
                        if (dgvJC_Table[2, i].Value != null) 
                        pacu_data.Add("miaobo", dgvJC_Table.Rows[i].Cells[2].Value.ToString());
                        else
                            pacu_data.Add("miaobo", "");
                        if (dgvJC_Table[3, i].Value != null) 
                        pacu_data.Add("huxi", dgvJC_Table.Rows[i].Cells[3].Value.ToString());
                        else
                            pacu_data.Add("huxi", "");
                        if (dgvJC_Table[4, i].Value != null) 
                        pacu_data.Add("spo2", dgvJC_Table.Rows[i].Cells[4].Value.ToString());
                        else
                            pacu_data.Add("spo2", "");
                        if (dgvJC_Table[5, i].Value != null) 
                        pacu_data.Add("ekg", dgvJC_Table.Rows[i].Cells[5].Value.ToString());
                        else
                            pacu_data.Add("ekg", "");
                        if (dgvJC_Table[6, i].Value != null) 
                        pacu_data.Add("huxio2", dgvJC_Table.Rows[i].Cells[6].Value.ToString());
                        else
                            pacu_data.Add("huxio2", "");
                        if (dgvJC_Table[7, i].Value != null) 
                        pacu_data.Add("MZpingmian", dgvJC_Table.Rows[i].Cells[7].Value.ToString());
                        else
                            pacu_data.Add("MZpingmian", "");
                        if (dgvJC_Table[8, i].Value != null) 
                        pacu_data.Add("XueQiFenXi", dgvJC_Table.Rows[i].Cells[8].Value.ToString());
                        else
                            pacu_data.Add("XueQiFenXi", "");
                        if (dgvJC_Table[9, i].Value != null) 
                        pacu_data.Add("YongYao", dgvJC_Table.Rows[i].Cells[9].Value.ToString());
                        else
                            pacu_data.Add("YongYao", "");
                        if (dgvJC_Table[10, i].Value != null) 
                        pacu_data.Add("Remark", dgvJC_Table.Rows[i].Cells[10].Value.ToString());
                        else
                            pacu_data.Add("Remark", "");
                        if (bll.PACU_data(MZID,short_time).Rows.Count>0)
                        {
                            bll.UpdatePACU_DATA(pacu_data, MZID);
                        }
                        else
                        
                        bll.SavaPACU_DATA(pacu_data, MZID);
                    }
                }

                Dictionary<string, string> ChuShiInfo = new Dictionary<string, string>();
                ChuShiInfo.Add("Jisonghuifu", Jisonghuifu.Text);
                ChuShiInfo.Add("Taitou5miao", Taitou5miao.Text);
                ChuShiInfo.Add("Keshoutunyan", Keshoutunyan.Text);
                ChuShiInfo.Add("Yishi", Yishi.Text);
                ChuShiInfo.Add("Qiguandaoguan", Qiguandaoguan.Text);
                ChuShiInfo.Add("Dingxiangli", Dingxiangli.Text);
                ChuShiInfo.Add("MZpingmianUP", MZpingmianUP.Text);
                ChuShiInfo.Add("MZpingmianDOWN.", MZpingmianDOWN.Text);
                ChuShiInfo.Add("Tengtong", Tengtong.Text);
                ChuShiInfo.Add("Exin", Exin.Text);
                ChuShiInfo.Add("Jingmaitongchang", Jingmaitongchang.Text);
                ChuShiInfo.Add("ZhentongFF", ZhentongFF.Text);
                ChuShiInfo.Add("visitDate", dateTimePicker1.Value.ToString());
                ChuShiInfo.Add("MZID", MZID);
               
                int flag = 0;
                if (bll.PACU_info(MZID).Rows.Count==0)
                {
                    flag = bll.SavaPACU(ChuShiInfo);
                }
                else
                {
                    flag = bll.UpdatePACU(ChuShiInfo);
                }
                if (flag==0)
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

        private void button3_Click(object sender, EventArgs e)
        {

            //打印文檔 
            PrintDocument pdDocument = new PrintDocument();
            //打印格式設置頁面 
            PageSetupDialog dlgPageSetup = new PageSetupDialog();
            //打印頁面 
            PrintDialog dlgPrint = new PrintDialog();
            //實例化打印預覽 
            PrintPreviewDialog dlgPrintPreview = new PrintPreviewDialog();
            pdDocument.PrintPage += new PrintPageEventHandler(myPrint);
            //頁面設置的打印文檔設置為需要打印的文檔 
            dlgPageSetup.Document = pdDocument;
            //打印界面的打印文檔設置為被打印文檔 
            dlgPrint.Document = pdDocument;
            //打印預覽的打印文檔設置為被打印文檔 
            dlgPrintPreview.Document = pdDocument;
            dlgPrintPreview.PrintPreviewControl.Zoom = 1.0;

            if (dlgPrintPreview.ShowDialog() == DialogResult.OK)
                pdDocument.Print();
        }
        public void myPrint(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 25;
            int y = 50;
            int y1 = y+15;//下划线坐标
            int width = e.MarginBounds.Width;
            int height = e.MarginBounds.Height;
            int w = dgvJC_Table.Width;   //非显示的宽度,实际为最大显示宽度，超过此宽度则会出现滚动条
            int h = dgvJC_Table.Height;  //非显示的高度，实际为最大显示高度，超过此高度则会出现分页
            double rate1 = (w * 1.00) / (width * 1.00);
            double rate2 = (h * 1.00) / (height * 1.00);
            double rate = (rate1 > rate2) ? rate1 : rate2;
            // w = (int)(w * 1.00 / rate);
            // h = (int)(h * 1.00 / rate);
            int col_count = dgvJC_Table.ColumnCount;
            int row_count = dgvJC_Table.RowCount;
            w = 0;
            for (int i = 0; i < col_count; i++) w += (int)(dgvJC_Table.Columns[i].Width * 1.00 / rate);
            int row_height = (int)(dgvJC_Table.Rows[0].Height * 1.00 / rate);
            h = row_count * row_height;
            Font myfont = dgvJC_Table.Font;
            Font title = new Font(new FontFamily("黑体"), 13);
            
            Font heiti = new Font(new FontFamily("黑体"), 9);
            Font textfront=new Font(new FontFamily("宋体"), 9);
            e.Graphics.DrawString("上海市中西医结合医院麻醉复苏记录" , title, System.Drawing.Brushes.Black, x+200, y-15);
            e.Graphics.DrawString("日期：" + dateTimePicker1.Text, heiti, System.Drawing.Brushes.Black, x, y - 5);
            e.Graphics.DrawLine(Pens.Black, x +30, y+10, x + 150, y+10);
            e.Graphics.DrawString("病区：" + tbBingqu.Text, heiti, System.Drawing.Brushes.Black, x + 520, y - 5);
            e.Graphics.DrawLine(Pens.Black, x + 550, y + 10, x + 630, y + 10);
            e.Graphics.DrawString("麻醉编号：" + tbMZID.Text, heiti, System.Drawing.Brushes.Black, x + 630, y - 5);
            e.Graphics.DrawLine(Pens.Black, x + 680, y + 10, x + 760, y + 10);
            
            y = y + 25; y1 = y+15;
            e.Graphics.DrawLine(Pens.Black, x - 5, y, x + 760, y);//区域分割横线
            y = y + 5; y1 = y + 15;
            e.Graphics.DrawString("姓名：" + tbPatname.Text, heiti, System.Drawing.Brushes.Black, x, y);
            e.Graphics.DrawLine(Pens.Black, x + 35, y1, x + 130, y1);
            e.Graphics.DrawString("性别：" + tbSex.Text, heiti, System.Drawing.Brushes.Black, x+140, y);
            e.Graphics.DrawLine(Pens.Black, x + 170, y1, x + 210, y1);
            e.Graphics.DrawString("年龄：" + tbAge.Text, heiti, System.Drawing.Brushes.Black, x+220, y);
            e.Graphics.DrawLine(Pens.Black, x + 250, y1, x + 290, y1);
            e.Graphics.DrawString("住院号：" + tbZhuyuanID.Text, heiti, System.Drawing.Brushes.Black, x + 300, y);
            e.Graphics.DrawLine(Pens.Black, x + 340, y1, x + 390, y1);
            e.Graphics.DrawString("床号：" + tbBedNO.Text, heiti, System.Drawing.Brushes.Black, x + 400, y);
            e.Graphics.DrawLine(Pens.Black, x + 430, y1, x + 480, y1);
            e.Graphics.DrawString("术中失血：" + tbShixue.Text, heiti, System.Drawing.Brushes.Black, x + 490, y);
            e.Graphics.DrawLine(Pens.Black, x + 545, y1, x + 600, y1);
            e.Graphics.DrawString("ml 尿量：" + tbNiaoliang.Text, heiti, System.Drawing.Brushes.Black, x + 600, y);
            e.Graphics.DrawLine(Pens.Black, x + 650, y1, x + 710, y1);
            e.Graphics.DrawString("ml" , heiti, System.Drawing.Brushes.Black, x + 710, y);
            
            //第二行
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("麻醉方法：" + tbMZFF.Text, heiti, System.Drawing.Brushes.Black, x, y);
            e.Graphics.DrawLine(Pens.Black, x + 55, y1, x + 350, y1);
            e.Graphics.DrawString("血型：" + tbXuexing.Text,heiti, System.Drawing.Brushes.Black, x + 360, y);
            e.Graphics.DrawLine(Pens.Black, x + 390, y1, x + 450, y1);
            e.Graphics.DrawString("术中输液：" + tbShuye.Text, heiti, System.Drawing.Brushes.Black, x + 460, y);
            e.Graphics.DrawLine(Pens.Black, x + 510, y1, x + 560, y1);
            e.Graphics.DrawString("ml 术中输血：" + tbShuxue.Text, heiti, System.Drawing.Brushes.Black, x + 560, y);
            e.Graphics.DrawLine(Pens.Black, x + 630, y1, x + 680, y1);
            e.Graphics.DrawString("ml", heiti, System.Drawing.Brushes.Black, x + 680, y);


            y = y + 25; 
            e.Graphics.DrawLine(Pens.Black, x - 5, y, x + 760, y);//区域分割横线
            y = y + 5; y1 = y + 15;
            e.Graphics.DrawString("\n入\n室\n情\n况" , heiti, System.Drawing.Brushes.Black, x-2, y);
            e.Graphics.DrawLine(Pens.Black, x + 17, y, x + 17, y+100);
            e.Graphics.DrawString("肌松恢复：" + cmbJiSongHuiFu.Text, heiti, System.Drawing.Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 75, y1, x + 115, y1);
            e.Graphics.DrawString("抬头五秒：" + cmbTaiTou5Miao.Text, heiti, System.Drawing.Brushes.Black, x + 125, y);
            e.Graphics.DrawLine(Pens.Black, x + 180, y1, x + 220, y1);
            e.Graphics.DrawString("咳嗽吞咽反应：" + cmbJiSongHuiFu.Text, heiti, System.Drawing.Brushes.Black, x + 230, y);
            e.Graphics.DrawLine(Pens.Black, x + 315, y1, x + 350, y1);
            e.Graphics.DrawString("意识：" + cmbYishi.Text, heiti, System.Drawing.Brushes.Black, x + 365, y);
            e.Graphics.DrawLine(Pens.Black, x + 395, y1, x + 435, y1); 
            e.Graphics.DrawString("气管导管：" + cmbQGDG.Text, heiti, System.Drawing.Brushes.Black, x + 445, y);
            e.Graphics.DrawLine(Pens.Black, x + 500, y1, x + 560, y1);
            e.Graphics.DrawString("定向能力：" + cmbDingXiangLi.Text, heiti, System.Drawing.Brushes.Black, x + 570, y);
            e.Graphics.DrawLine(Pens.Black, x + 620, y1, x + 670, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("麻醉平面：上：" + cmbMaZuiPingMianUP.Text, heiti, System.Drawing.Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 100, y1, x + 150, y1);
            e.Graphics.DrawString("下：" + cmbMaZuiPingMianDOWN.Text, heiti, System.Drawing.Brushes.Black, x + 152, y);
            e.Graphics.DrawLine(Pens.Black, x + 170, y1, x + 220, y1);
            e.Graphics.DrawString("恶心呕吐：" + cmbEXinOuTu.Text, heiti, System.Drawing.Brushes.Black, x + 230, y);
            e.Graphics.DrawLine(Pens.Black, x + 285, y1, x + 335, y1);
            e.Graphics.DrawString("静脉通畅：" + cmbJingMaiTongChang.Text, heiti, System.Drawing.Brushes.Black, x + 345, y);
            e.Graphics.DrawLine(Pens.Black, x + 400, y1, x + 450, y1);
            e.Graphics.DrawString("镇痛方法：" + cmbZhenTongFangFa.Text, heiti, System.Drawing.Brushes.Black, x + 460, y);
            e.Graphics.DrawLine(Pens.Black, x + 515, y1, x + 580, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("特殊问题：" + rtbTeshuwenti.Text, heiti, System.Drawing.Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 80, y1, x + 750, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x + 20, y1, x + 750, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x - 5, y, x + 760, y);//区域分割横线
            e.Graphics.DrawString(" 时 间", heiti, Brushes.Black, x + 5, y + 15);
            e.Graphics.DrawString(" 血 压" , heiti, Brushes.Black, x + 65, y + 5);
            e.Graphics.DrawString("(mmHg)" , heiti, Brushes.Black, x + 65, y + 25);
            e.Graphics.DrawString(" 脉 搏" , heiti, Brushes.Black, x + 125, y + 5);
            e.Graphics.DrawString("(次/分)" , heiti, Brushes.Black, x + 125, y + 25);
            e.Graphics.DrawString(" 呼 吸"  , heiti, Brushes.Black, x + 185, y + 5);
            e.Graphics.DrawString("(次/分)" , heiti, Brushes.Black, x + 185, y + 25);
            e.Graphics.DrawString(" SpO2" , heiti, Brushes.Black, x + 245, y + 5);
            e.Graphics.DrawString("  (%)" , heiti, Brushes.Black, x + 245, y + 25);            
            e.Graphics.DrawString("  EKG" , heiti, Brushes.Black, x + 305, y + 15);
            e.Graphics.DrawString("呼吸氧气", heiti, Brushes.Black, x + 365, y + 5);
            e.Graphics.DrawString("  %", heiti, Brushes.Black, x + 365, y + 25);
            e.Graphics.DrawString("麻醉平面" , heiti, Brushes.Black, x + 425, y + 15);
            e.Graphics.DrawString("血气分析" , heiti, Brushes.Black, x + 485, y + 15);
            e.Graphics.DrawString(" 用 药" , heiti, Brushes.Black, x + 545, y + 15);
            e.Graphics.DrawString(" 备 注" , heiti, Brushes.Black, x + 645, y + 15);

            e.Graphics.DrawLine(Pens.Black, x -5, y, x -5, y + 550);
            for (int j = 1; j < 10; j++)
            {
                e.Graphics.DrawLine(Pens.Black, x + 60 * j, y, x + 60 * j, y + 550);
            }
            e.Graphics.DrawLine(Pens.Black, x + 640, y, x + 640, y + 550);
            e.Graphics.DrawLine(Pens.Black, x + 760, y, x + 760, y + 550);

            y = y + 50; y1 = y + 15; 
            for (int i = 0; i <= 20; i++)
            {
                e.Graphics.DrawLine(Pens.Black, x-5, y + 25 * i, x + 760, y + 25 * i);
            }

            for (int i = 0; i < dgvJC_Table.Rows.Count; i++)
            {
                for (int j = 0; j < dgvJC_Table.Columns.Count-2; j++)
                {
                    if (dgvJC_Table.Rows[i].Cells[j].Value != null )
                    {
                        e.Graphics.DrawString(dgvJC_Table.Rows[i].Cells[j].Value.ToString(), textfront, Brushes.Black, x +5 + 60 * j, y + 5 + 25 * i);

                    }                    
                    
                }
              
            }
            for (int i = 0; i < dgvJC_Table.Rows.Count; i++)
            {
                for (int j = dgvJC_Table.Columns.Count-2; j < dgvJC_Table.Columns.Count; j++)
                {
                    if (dgvJC_Table.Rows[i].Cells[j].Value != null)
                    {
                        e.Graphics.DrawString(dgvJC_Table.Rows[i].Cells[j].Value.ToString(), textfront, Brushes.Black, x + 545 + 100 * (j - 9), y + 5 + 25 * i);

                    }

                }

            }


            //监测表下部分内容




            y = y + 25 * 20; y1 = y + 15;
            y = y + 15; y1 = y + 15;
            e.Graphics.DrawString("\n出\n室\n情\n况", heiti, System.Drawing.Brushes.Black, x-2, y);
            e.Graphics.DrawLine(Pens.Black, x + 17, y, x + 17, y + 100);
            e.Graphics.DrawString("肌松恢复：" + Jisonghuifu.Text, heiti, System.Drawing.Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 75, y1, x + 115, y1);
            e.Graphics.DrawString("抬头五秒：" + Taitou5miao.Text, heiti, System.Drawing.Brushes.Black, x + 125, y);
            e.Graphics.DrawLine(Pens.Black, x + 180, y1, x + 230, y1);
            e.Graphics.DrawString("咳嗽吞咽反应：" + Keshoutunyan.Text, heiti, System.Drawing.Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 310, y1, x + 360, y1);
            e.Graphics.DrawString("意识：" + Yishi.Text, heiti, System.Drawing.Brushes.Black, x + 370, y);
            e.Graphics.DrawLine(Pens.Black, x + 400, y1, x + 450, y1);
            e.Graphics.DrawString("气管导管：" + Qiguandaoguan.Text, heiti, System.Drawing.Brushes.Black, x + 460, y);
            e.Graphics.DrawLine(Pens.Black, x + 510, y1, x + 590, y1);
            e.Graphics.DrawString("定向能力：" + Dingxiangli.Text, heiti, System.Drawing.Brushes.Black, x + 600, y);
            e.Graphics.DrawLine(Pens.Black, x + 655, y1, x + 700, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("麻醉平面：上：" + MZpingmianUP.Text, heiti, System.Drawing.Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 100, y1, x + 150, y1);
            e.Graphics.DrawString("下：" + MZpingmianDOWN.Text, heiti, System.Drawing.Brushes.Black, x + 160, y);
            e.Graphics.DrawLine(Pens.Black, x + 180, y1, x + 230, y1);
            e.Graphics.DrawString("恶心呕吐：" + Exin.Text, heiti, System.Drawing.Brushes.Black, x + 240, y);
            e.Graphics.DrawLine(Pens.Black, x + 290, y1, x + 340, y1);
            e.Graphics.DrawString("静脉通畅：" + Jingmaitongchang.Text, heiti, System.Drawing.Brushes.Black, x + 350, y);
            e.Graphics.DrawLine(Pens.Black, x + 400, y1, x + 450, y1);
            e.Graphics.DrawString("镇痛方法：" + ZhentongFF.Text, heiti, System.Drawing.Brushes.Black, x + 460, y);
            e.Graphics.DrawLine(Pens.Black, x + 510, y1, x + 580, y1);


            y = y + 25; y1 = y + 15;
            e.Graphics.DrawString("特殊问题：" + rtbTeshuwenti.Text, heiti, System.Drawing.Brushes.Black, x + 20, y);
            e.Graphics.DrawLine(Pens.Black, x + 75, y1, x + 750, y1);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x + 20, y1, x + 750, y1);

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x - 5, y, x + 760, y);//区域分割横线
            e.Graphics.DrawLine(Pens.Black, x - 5, 75, x - 5, y );//竖线
            e.Graphics.DrawLine(Pens.Black, x + 760, 75, x + 760, y);//竖线
            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
         
            
            timer1.Enabled = true;
            pick_count = 0;
            max_pickCount = int.Parse(comboBox1.Text.Trim())*60;
            timer1.Interval = 1000 * int.Parse(comboBox1.Text);//改变timer的间隔时间
            
        }

        int dr = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000 * 60 * int.Parse(comboBox1.Text);//改变timer的间隔时间            
            Random rd = new Random();//随机模拟数据
            int xueya= rd.Next(60, 150);
            int miaobo= rd.Next(60, 90);
            int huxi = rd.Next(20, 60);
            int spo2 = rd.Next(90, 100);
            int ekg = rd.Next(20, 30);
            int huxio2 = rd.Next(90, 100);
                        
           
            dgvJC_Table.Rows.Add(1);
            dgvJC_Table.Rows[dr].Cells[0].Value = DateTime.Now.ToString("HH:mm");
            dgvJC_Table.Rows[dr].Cells[1].Value = xueya;
            dgvJC_Table.Rows[dr].Cells[2].Value = miaobo;
            dgvJC_Table.Rows[dr].Cells[3].Value = huxi;
            dgvJC_Table.Rows[dr].Cells[4].Value = spo2;
            dgvJC_Table.Rows[dr].Cells[5].Value = ekg;
            dgvJC_Table.Rows[dr].Cells[6].Value = huxio2;
            dr++;

            
            
        }


        public void clear()
        {
            dateTimePicker1.Value = DateTime.Now;
            rtbTeshuwenti.Text = "";
            observeTime_txt.Text = "";
            //this.cmbBreathe.Text = "";
            //liquid_txt.Text = "";
            //urin_txt.Text = "";           
            //recorder_txt.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            button5.Enabled = true;
            timer1.Enabled = false;
            
        }

        private void PACU_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (button2.Enabled == true)
            {
                MessageBox.Show( "检测没有结束，不能关闭！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

    }
}
