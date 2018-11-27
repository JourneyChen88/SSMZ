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
using System.Net.NetworkInformation;
using System.Threading;
using main.HisWebReference;
using System.Xml.Linq;
using System.Xml;

namespace main
{
    public partial class PaiBan_SD : Form
    {
        string operAddress="";
        DB2help db2 = new DB2help();
        AdimsController bll = new AdimsController();
        AdimsProvider DAL = new AdimsProvider();
        admin_T_SQL at = new admin_T_SQL();
        DB2help his = new DB2help();
        public PaiBan_SD()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int SAVE()
        {
            Dictionary<string, string> beforeVisit = new Dictionary<string, string>();
            int result = 0;
            try
            {
                beforeVisit.Add("txtPatid", txtPatid.Controls[0].Text.Trim());
                beforeVisit.Add("zhuyuanid", txtZhuYuanNo.Controls[0].Text);
                beforeVisit.Add("txtPatname", txtPatname.Controls[0].Text);
                beforeVisit.Add("txtPatage", txtPatage.Controls[0].Text);
                beforeVisit.Add("cmbSex", txtSex.Controls[0].Text);
                beforeVisit.Add("txtDepartment", this.txtBingQu.Controls[0].Text);
                beforeVisit.Add("txtBednumber", txtBednumber.Controls[0].Text);
                beforeVisit.Add("txtWeight", txtWeight.Controls[0].Text);
                beforeVisit.Add("txtHeight", txtHeight.Controls[0].Text);
                beforeVisit.Add("txtNssss", txtNssss.Controls[0].Text);
                beforeVisit.Add("txtSqzd", txtSqzd.Controls[0].Text);
                beforeVisit.Add("cmbOroom", cmbOroom.Text.Trim());
                beforeVisit.Add("txtSecond", cmbSecond.Text.Trim());
                beforeVisit.Add("txtMZFF", cmbMZFF.Text);
                beforeVisit.Add("txtMZYS", this.txtMZYS.Controls[0].Text);
                beforeVisit.Add("txtQXHS", this.txtQXHS.Controls[0].Text);
                beforeVisit.Add("txtXHHS", this.txtXHHS.Controls[0].Text);
                beforeVisit.Add("dtOSdate", dtOSDate.Value.ToString());
                beforeVisit.Add("OS", txtOS.Controls[0].Text);
                beforeVisit.Add("ostate", "0");
                beforeVisit.Add("applyId", txtSQDH.Controls[0].Text);
                beforeVisit.Add("Cardno", txtCardID.Controls[0].Text);
                beforeVisit.Add("ReplyDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                if (DAL.GetALLPAIBAN(txtPatid.Controls[0].Text).Rows.Count == 0)
                    result = DAL.InsertPAIBAN(beforeVisit);
                else
                    result = DAL.UpdatePAIBAN(beforeVisit);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
                //MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }
        OperAisthService hisWS = new OperAisthService();
        private string BackToHisPaibanInfo()
        {
            string ApplyID = txtPatid.Controls[0].Text.Trim();

            var doc = new XDocument(
                                new XElement("Params",
                                        new XElement("ApplyID", ApplyID),
                                        new XElement("ApplyDate", dtOSDate.Value.ToString("yyyy-MM-dd")),
                                        new XElement("ReplyDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:00")),
                                        new XElement("OPPlaceName", cmbOroom.Text),
                                        new XElement("OPNo", cmbSecond.Text)
                                             )
                                        );
            //DataTable dtConfirm = DAL.SelectOperConfirm(ApplyID);


            string operinfo = doc.ToString();
            string resultXML = hisWS.OperationConfirm(operinfo);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(resultXML);
            string result = "";
            XmlNodeList nodeList = xmldoc.SelectSingleNode("Szyy").ChildNodes;
            foreach (XmlNode node in nodeList)//遍历所有子节点
            {
                if (node.Name == "Status")
                {
                    result = node.InnerText;
                    break;
                }
            }
            return result;

            //DataTable dtEnd = DAL.SelectOperEnd(patID);
            ////string operinfo2 = DatatableToXml(dtEnd);//xml文本格式
            ////his.OperationEnd(operinfo2);//手术结束信息
            //his.OperCancle(patID);//手术是否取消
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPatid.Controls[0].Text != "")
            {
                if (cmbOroom.Text.Trim() != "" && cmbSecond.Text.Trim() != "")
                {
                    DataTable dt = DAL.GetpaibanByOroomandTaici(cmbOroom.Text.Trim(), cmbSecond.Text.Trim(), dtOSDate.Value, txtPatid.Controls[0].Text.Trim());
                    if (dt.Rows.Count == 0)
                    {
                        int result = SAVE();
                        if (result > 0)
                        {
                            paibanDataBind();
                            //string HISresult = BackToHisPaibanInfo();
                            //if (HISresult!="1")
                            //{
                            //    MessageBox.Show("反馈失败！");
                            //}
                        }
                        else
                            MessageBox.Show("保存失败！");
                    }
                    else MessageBox.Show("此手术间当前台次已存在！");

                }
                else MessageBox.Show("手术间和台次不能为空！");
            }
        }

        private void PAIBAN_SGShuRu_Load(object sender, EventArgs e)
        {
            operAddress = Program.customer.yiyuanType;
            bool pingIP = PingHost("10.0.100.87");
            if (pingIP == true)
            {
                HisDataBind();
            }
            else
                MessageBox.Show("网络中断请检查网络");
            paibanDataBind();
            this.txtMZYS.Controls[0].DoubleClick += new System.EventHandler(this.txtMZYS_DoubleClick);
            this.txtQXHS.Controls[0].DoubleClick += new System.EventHandler(this.txtQXHS_DoubleClick);
            this.txtXHHS.Controls[0].DoubleClick += new System.EventHandler(this.txtXHHS_DoubleClick);
            BindcmbMZFF();//绑定麻醉方法

            BindcmbOrrom();//手术室号
            BindCombox2();
            BindCombox3();
        }

        private void HisDataBind()
        {

            try
            {
                DataTable dt1 = new DataTable();
                dt1 = db2.GetPAIBAN(dtOSDate.Value.Date.ToString("yyyy-MM-dd"));
                dgvHisInfo.DataSource = dt1.DefaultView;
            }
            catch (Exception E)
            {
                throw E;
            }

        }
        private void paibanDataBind()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetPAIBAN(dtOSDate.Value.Date.ToString("yyyy-MM-dd"),operAddress,"");
            dgvOTypesetting.DataSource = dt.DefaultView;
        }
        //手术室号
        private void BindcmbOrrom()
        {
            cmbOroom.Items.Clear();
            DataTable dt1 = new DataTable();
            dt1 = DAL.GetOROOM_all();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbOroom.Items.Add(dt1.Rows[i][0]);
            }
        }
        private void BindcmbMZFF()
        {
            cmbMZFF.Items.Clear();
            DataTable dt1 = his.GetHisMzff("");
            cmbMZFF.DataSource = dt1;
            cmbMZFF.DisplayMember = "Mzff_Name";
            cmbMZFF.ValueMember = "Mzff_No";
        }
        private void BindCombox3()
        {
            cmbXHHS1.Items.Clear();
            cmbXHHS2.Items.Clear();
            cmbXSHS1.Items.Clear();
            cmbXSHS2.Items.Clear();

            DataTable dt = new DataTable();
            dt = DAL.GetAll_hushi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbXHHS1.Items.Add(dt.Rows[i][0]);
                cmbXHHS2.Items.Add(dt.Rows[i][0]);
                cmbXSHS1.Items.Add(dt.Rows[i][0]);
                cmbXSHS2.Items.Add(dt.Rows[i][0]);

            }
        }

        private void BindCombox2()
        {
            cmbMZYS.Items.Clear();
            cmbFMYS1.Items.Clear();
            cmbFMYS2.Items.Clear();

            DataTable dt = new DataTable();
            dt = DAL.GetAllMZYS();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbMZYS.Items.Add(dt.Rows[i][0]);
                cmbFMYS1.Items.Add(dt.Rows[i][0]);
                cmbFMYS2.Items.Add(dt.Rows[i][0]);
            }
        }

        #region<<d打印>>
        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = pdDocument;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                pdDocument.Print();
            //pdDocument.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            pdDocument.DefaultPageSettings.Landscape = true;
        }

        private void pdDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //C#打印设置之设置横向打印   
            this.pdDocument.DefaultPageSettings.Landscape = true;
            //C#打印设置之设置彩色打印   
            this.pdDocument.DefaultPageSettings.Color = true;

            //C#打印设置之设置打印纸张类型和大小  
            this.pdDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("16K", 740, 1020);

            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }
        int dyi = 0;
        private void pdDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 100, y = 50;
            Pen black = new Pen(Brushes.Black);
            black.Width = 1;
            Font HeiTi = new System.Drawing.Font("黑体", 9);
            Font Kti = new System.Drawing.Font("宋体", 7);
            e.Graphics.DrawString("手 术 通 知 单", new Font("宋体", 16, FontStyle.Bold), Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawString("手术日期：" + dtOSDate.Value.ToLongDateString(), HeiTi, Brushes.Black, new Point(x + 348, y + 30));
            e.Graphics.DrawString("共 " + dgvOTypesetting.Rows.Count.ToString() + " 台", HeiTi, Brushes.Black, new Point(x + 390, y + 45));
            y = y + 60;
            e.Graphics.DrawLine(black, new Point(x, y), new Point(946, y));
            e.Graphics.DrawString("手术间", HeiTi, Brushes.Black, new Point(x + 8, y + 5));
            e.Graphics.DrawString("台次", HeiTi, Brushes.Black, new Point(x + 65, y + 5));
            e.Graphics.DrawString("姓名", HeiTi, Brushes.Black, new Point(x + 105, y + 5));
            e.Graphics.DrawString("性别", HeiTi, Brushes.Black, new Point(x + 155, y + 5));
            e.Graphics.DrawString("年龄", HeiTi, Brushes.Black, new Point(x + 196, y + 5));
            e.Graphics.DrawString("床号", HeiTi, Brushes.Black, new Point(x + 237, y + 5));
            e.Graphics.DrawString("手术名称", HeiTi, Brushes.Black, new Point(x + 300, y + 5));
            e.Graphics.DrawString("手术医师", HeiTi, Brushes.Black, new Point(x + 430, y + 5));
            e.Graphics.DrawString("麻醉方法", HeiTi, Brushes.Black, new Point(x + 525, y + 5));
            e.Graphics.DrawString("麻醉医师", HeiTi, Brushes.Black, new Point(x + 630, y + 5));
            e.Graphics.DrawString("器械/巡回护士", HeiTi, Brushes.Black, new Point(x + 730, y + 5));
            y = y + 25;
            for (; dyi < dgvOTypesetting.Rows.Count; dyi++)
            {
                e.Graphics.DrawLine(black, new Point(x, y), new Point(945, y));
                e.Graphics.DrawString(dgvOTypesetting[0, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 5, y + 5));//手术间
                e.Graphics.DrawString(dgvOTypesetting[1, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 75, y + 5));//台次
                e.Graphics.DrawString(dgvOTypesetting[3, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 105, y + 5));//姓名
                e.Graphics.DrawString(dgvOTypesetting[5, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 160, y + 5));//性别
                e.Graphics.DrawString(dgvOTypesetting[4, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 200, y + 3));//年龄
                e.Graphics.DrawString(dgvOTypesetting[6, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 235, y + 5));//床号
                e.Graphics.DrawString(dgvOTypesetting[8, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 283, y + 5));//手术名称
                e.Graphics.DrawString(dgvOTypesetting[7, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 435, y + 5));//手术医师
                e.Graphics.DrawString(dgvOTypesetting[10, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 500, y + 5));//麻醉方法
                e.Graphics.DrawString(dgvOTypesetting[11, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 620, y + 3));//麻醉医师
                e.Graphics.DrawString(dgvOTypesetting[14, dyi].Value.ToString() + "/" + dgvOTypesetting[16, dyi].Value.ToString(), Kti, Brushes.Black, new Point(x + 700, y + 5));//器械/巡回护士
                y = y + 25;
            }
            e.Graphics.DrawLine(black, new Point(x, y), new Point(945, y));
            int hangshu = dgvOTypesetting.Rows.Count + 1;
            //
            e.Graphics.DrawLine(black, new Point(x, y - hangshu * 25), new Point(x, y));
            //手术间
            e.Graphics.DrawLine(black, new Point(x + 50, y - hangshu * 25), new Point(x + 50, y));
            //台次
            e.Graphics.DrawLine(black, new Point(x + 100, y - hangshu * 25), new Point(x + 100, y));
            //姓名
            e.Graphics.DrawLine(black, new Point(x + 150, y - hangshu * 25), new Point(x + 150, y));
            //性别
            e.Graphics.DrawLine(black, new Point(x + 190, y - hangshu * 25), new Point(x + 190, y));
            //年龄
            e.Graphics.DrawLine(black, new Point(x + 230, y - hangshu * 25), new Point(x + 230, y));
            //床号
            e.Graphics.DrawLine(black, new Point(x + 280, y - hangshu * 25), new Point(x + 280, y));
            //手术名称
            e.Graphics.DrawLine(black, new Point(x + 430, y - hangshu * 25), new Point(x + 430, y));
            //手术医生
            e.Graphics.DrawLine(black, new Point(x + 490, y - hangshu * 25), new Point(x + 490, y));
            //麻醉方法
            e.Graphics.DrawLine(black, new Point(x + 610, y - hangshu * 25), new Point(x + 610, y));
            //麻醉医生
            e.Graphics.DrawLine(black, new Point(x + 700, y - hangshu * 25), new Point(x + 700, y));
            //器械巡回
            e.Graphics.DrawLine(black, new Point(x + 845, y - hangshu * 25), new Point(x + 845, y));
            dyi = 0;
        }
        #endregion

        private void dtOSDate_ValueChanged(object sender, EventArgs e)
        {
            HisDataBind();
            paibanDataBind();
        }

        private void dgvOTypesetting_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                string PATID = dgvOTypesetting.SelectedCells[18].Value.ToString();
                DataTable dt = new DataTable();
                dt = DAL.GetALLPAIBAN(PATID);
                //applyId
                txtPatid.Controls[0].Text = dt.Rows[0]["PATID"].ToString();
                txtZhuYuanNo.Controls[0].Text = dt.Rows[0]["PatZhuYuanID"].ToString();
                txtPatname.Controls[0].Text = dt.Rows[0]["Patname"].ToString();
                txtPatage.Controls[0].Text = dt.Rows[0]["Patage"].ToString();
                txtSex.Controls[0].Text = dt.Rows[0]["Patsex"].ToString();
                txtBingQu.Controls[0].Text = dt.Rows[0]["Patdpm"].ToString();
                txtBednumber.Controls[0].Text = dt.Rows[0]["Patbedno"].ToString();
                txtWeight.Controls[0].Text = dt.Rows[0]["PatWeight"].ToString();
                txtHeight.Controls[0].Text = dt.Rows[0]["PatHeight"].ToString();
                txtNssss.Controls[0].Text = dt.Rows[0]["Oname"].ToString();
                txtSqzd.Controls[0].Text = dt.Rows[0]["Pattmd"].ToString();
                cmbOroom.Text = dt.Rows[0]["Oroom"].ToString();
                cmbSecond.Text = dt.Rows[0]["Second"].ToString();
                cmbMZFF.Text = dt.Rows[0]["Amethod"].ToString();
                this.txtMZYS.Controls[0].Text = dt.Rows[0]["AP1"].ToString();
                this.txtQXHS.Controls[0].Text = dt.Rows[0]["ON1"].ToString();
                this.txtXHHS.Controls[0].Text = dt.Rows[0]["SN1"].ToString();
                dtOSDate.Value = Convert.ToDateTime(dt.Rows[0]["Odate"]);
                this.txtOS.Controls[0].Text = dt.Rows[0]["OS"].ToString();

            }
        }

        private void dgvHisInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHisInfo.Rows.Count > 0)
            {
                txtPatid.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["申请单号"].Value.ToString();
                txtZhuYuanNo.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["住院号"].Value.ToString();
                txtPatname.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["病人姓名"].Value.ToString();
                txtPatage.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["病人年龄"].Value.ToString();
                txtSex.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["病人性别"].Value.ToString();
                txtBingQu.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["病区"].Value.ToString();
                txtBednumber.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["床号"].Value.ToString();
                txtSQDH.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["病人编号"].Value.ToString();
                txtNssss.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["手术名称"].Value.ToString();
                txtSqzd.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["术前诊断"].Value.ToString();
                //cmbOroom.Text = dgvHisInfo.CurrentRow.Cells["手术间"].Value.ToString();
                cmbMZFF.Text = dgvHisInfo.CurrentRow.Cells["麻醉方法"].Value.ToString();
                txtOS.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["手术医生"].Value.ToString();
                dtOSDate.Value = Convert.ToDateTime(dgvHisInfo.CurrentRow.Cells["手术日期"].Value);
                txtCardID.Controls[0].Text = dgvHisInfo.CurrentRow.Cells["就诊卡号"].Value.ToString();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //PingIP();
            bool pingIP = PingHost("10.0.100.87");
            if (pingIP == true)
            {
                HisDataBind();
            }
            else
                MessageBox.Show("网络中断请检查网络");
        }
        /// <summary>
        /// ping 连通服务器
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="TimeOut"></param>
        /// <returns></returns>
        public bool PingHost(string Address, int TimeOut = 1000)
        {
            try
            {
                using (System.Net.NetworkInformation.Ping PingSender = new System.Net.NetworkInformation.Ping())
                {
                    PingOptions Options = new PingOptions();
                    Options.DontFragment = true;
                    string Data = "test";
                    byte[] DataBuffer = Encoding.ASCII.GetBytes(Data);
                    PingReply Reply = PingSender.Send(Address, TimeOut, DataBuffer, Options);
                    if (Reply.Status == IPStatus.Success)
                        return true;
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("网络连接异常");
                return false;
            }

        }
        #region MyRegion

        //public void PingIP() {
        //    Ping p = new Ping();//创建Ping对象p
        //    PingReply pr = p.Send("10.0.100.87");//服务器端IP
        //    int i;
        //    if (pr.Status == IPStatus.Success)//如果ping成功            
        //    {
        //        HisDataBind();
        //    }
        //    else
        //    {
        //        for (i = 0; i <= 12; i++)
        //        {
        //            Thread.Sleep(6000);//等待十分钟     
        //            pr = p.Send("10.0.100.87");
        //            Console.WriteLine(pr.Status);
        //        }
        //        MessageBox.Show("重连失败");
        //    }

        //    while (pr.Status != IPStatus.Success) ;
        //    HisDataBind();
        //    i = 0;//连接成功，重新连接次数清为0;
        //}

        #endregion
        private void txtQXHS_DoubleClick(object sender, EventArgs e)
        {
            SelectYSorHS F1 = new SelectYSorHS(2, txtQXHS);
            F1.ShowDialog();
        }

        private void txtMZYS_DoubleClick(object sender, EventArgs e)
        {
            SelectYSorHS F1 = new SelectYSorHS(1, txtMZYS);
            F1.ShowDialog();
        }

        private void txtXHHS_DoubleClick(object sender, EventArgs e)
        {
            SelectYSorHS F1 = new SelectYSorHS(2, txtXHHS);
            F1.ShowDialog();
        }

      
        private void 暂停手术ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string patid = this.dgvOTypesetting.SelectedRows[0].Cells["病人ID"].Value.ToString();
            string id = this.dgvOTypesetting.SelectedRows[0].Cells["排班编号"].Value.ToString();
            string mzfs = this.dgvOTypesetting.SelectedRows[0].Cells["麻醉方法"].Value.ToString();

            if (MessageBox.Show("确定停止这台手术吗吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                int num = at.ZTSS1(patid);
                if (num > 0)
                {
                    MessageBox.Show("手术已经开始或者已完成  操作失败");
                    return;
                }
                int a = DAL.UpdatePaibanCancel(patid);
                if (a > 0)
                {
                    paibanDataBind();
                   

                }
                else

                    MessageBox.Show("操作失败");

            }
        }

        private void 局麻完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = this.dgvOTypesetting.SelectedRows[0].Cells["排班编号"].Value.ToString();
            string mzfs = this.dgvOTypesetting.SelectedRows[0].Cells["麻醉方法"].Value.ToString();
            if (MessageBox.Show("确定这台手术完成了吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (mzfs == "局部浸润麻醉")
                {
                    int a = at.UpdateZT(id);
                    if (a > 0)
                    {
                        MessageBox.Show("手术已完成");

                        paibanDataBind();
                    }
                    else
                    {
                        MessageBox.Show("失败");
                    }
                }
                else
                {
                    MessageBox.Show("此台手术自动鉴别是否完成！ 操作失败");
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        //private void txtSqzd_DoubleClick(object sender, EventArgs e)
        //{
        //    sqzd sqzdform = new sqzd(txtSqzd);
        //    sqzdform.ShowDialog();
        //}

        //private void txtNssss_DoubleClick(object sender, EventArgs e)
        //{
        //    osel oselform = new osel(txtNssss);
        //    oselform.ShowDialog();
        //}

        //private void txtMZFF_DoubleClick(object sender, EventArgs e)
        //{
        //    mzff mzffform = new mzff(txtMZFF);
        //    mzffform.ShowDialog();
        //}




    }
}
