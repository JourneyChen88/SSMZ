using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL.Flows;
using adims_BLL;
using adims_Utility;

namespace main
{
    public partial class AfterAnalgesia : Form
    {
        string MZID, PatId, Odate;
        bool isRead = false;

        MzjldDal _MzjldDal = new MzjldDal();
        adims_DAL.Flows.AfterAnalgesia _ShztDal = new adims_DAL.Flows.AfterAnalgesia();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        UserRoleBll _UserRoleBll = new UserRoleBll();


        public AfterAnalgesia(string mzid, string patid, string date)
        {
            MZID = mzid;
            PatId = patid;
            Odate = date;
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MZSHZT_Load(object sender, EventArgs e)
        {
            try
            {
                Load_info();
                LodAfterAnalgesia();
                BindAfterAnalgesiaDetail();
                this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        /// 加载选择病人信息
        /// </summary>
        public void Load_info()
        {
            DataTable dt1 = _PaibanDal.GetPaibanByPatId(PatId);
            DataRow dr1 = dt1.Rows[0];
            tbKeshi.Text = dr1["patdpm"].ToString();
            tbPatname.Text = dr1["Patname"].ToString();
            tbPatsex.Text = dr1["patsex"].ToString();
            tbPatage.Text = dr1["patage"].ToString();
            tbZhuyuanID.Text = dr1["patZhuyuanID"].ToString();
            dtVisitDate.Text = Odate;
            dtZTKssj.Text = Odate;
            dtZTjssj.Text = Odate;
            DataTable dt2 = _MzjldDal.GetMzjldByMzjldId(MZID);
            if (dt2.Rows.Count > 0)
            {
                DataRow dr2 = dt2.Rows[0];
                txtSsmc.Text = dr2["ShoushuFS"].ToString();
            }
        }

        int BCcount = 0;
        /// <summary>
        /// 赋值
        /// </summary>
        private void LodAfterAnalgesia()
        {
            DataTable dt = _ShztDal.GetAfterAnalgesia(MZID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtSsmc.Text = dr["Ssmc"].ToString();
                cmbMzff.Text = dr["Mzff"].ToString();
                cmbZtff.Text = dr["Ztff"].ToString();
                txtZtpf.Text = dr["Ztpf"].ToString();
                lineZTsj.Text = dr["ZTsj"].ToString();
                lineZTGysd.Text = dr["ZTGysd"].ToString();
                lineZTDczj.Text = dr["ZTDczj"].ToString();
                lineZTSdsj.Text = dr["ZTSdsj"].ToString();
                dtZTKssj.Text = dr["ZTKssj"].ToString();
                dtZTjssj.Text = dr["ZTjssj"].ToString();
                txtBfzcl.Text = dr["Bfzcl"].ToString();
                dtVisitDate.Text = dr["VisitDate"].ToString();
                if (Convert.ToString(dr["IsRead"]) == "0")
                {
                    btnSave.Enabled = true;
                    isRead = false;
                }
                if (Convert.ToString(dr["IsRead"]) == "1")
                {
                    isRead = true;
                    btnSave.Enabled = false;
                }
                string jurisdiction = _UserRoleBll.GetUserRole(Program.customer);
                if (jurisdiction.Contains("8"))
                {
                    btnJS.Visible = true;
                }
                else
                {
                    btnJS.Visible = false;
                }
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            Dictionary<string, string> SHFS = new Dictionary<string, string>();
            int result = 0;
            try
            {
                SHFS.Add("Mzjld", MZID);
                if (btnSave.Enabled)
                {
                    SHFS.Add("IsRead", "0");
                }
                else
                {
                    SHFS.Add("IsRead", "1");
                }
                SHFS.Add("PatId", PatId);
                SHFS.Add("Ssmc", txtSsmc.Text);
                SHFS.Add("Mzff", cmbMzff.Text);
                SHFS.Add("Ztff", cmbZtff.Text);
                SHFS.Add("Ztpf", txtZtpf.Text);
                SHFS.Add("ZTsj", lineZTsj.Text);
                SHFS.Add("ZTGysd", lineZTGysd.Text);
                SHFS.Add("ZTDczj", lineZTDczj.Text);
                SHFS.Add("ZTSdsj", lineZTSdsj.Text);
                SHFS.Add("ZTKssj", dtZTKssj.Text);
                SHFS.Add("ZTjssj", dtZTjssj.Text);
                SHFS.Add("Bfzcl", txtBfzcl.Text);
                SHFS.Add("VASpf", cmbVASpf.Text);
                SHFS.Add("VisitDate", Convert.ToDateTime(dtVisitDate.Value.ToString()).ToString("yyyy-MM-dd"));
                SHFS.Add("Odate", Convert.ToDateTime(Odate).ToString("yyyy-MM-dd"));
                DataTable dt = _ShztDal.GetAfterAnalgesia(MZID);
                if (dt.Rows.Count > 0)
                    result = _ShztDal.UpdateAfterAnalgesia(SHFS);
                else
                    result = _ShztDal.InsertAfterAnalgesia(SHFS);
                if (result > 0)
                {
                    BCcount++; MessageBox.Show("保存成功！");
                }
                else MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常，请检查网络！" + ex.ToString());
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        /// <summary>
        /// 存档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCD_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = _ShztDal.GetAfterAnalgesiaDetail(MZID);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = _ShztDal.UpdateAfterAnalgesia(MZID, (int)EnumCreator.ReadModel.只读);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    isRead = true;
                    btnSave.Enabled = false;
                    btnCD.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJS_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = _ShztDal.GetAfterAnalgesiaDetail(MZID);
            if (dt.Rows[0]["isRead"].ToString() == "1")
            {
                result = _ShztDal.UpdateAfterAnalgesia(MZID, (int)EnumCreator.ReadModel.可修改);
                if (result > 0)
                {
                    MessageBox.Show("解锁成功！");
                    btnSave.Enabled = true;
                    isRead = false;
                    btnCD.Enabled = true;
                }
            }
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (isRead)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZSHZT_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZSHZT_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZSHZT_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZSHZT_FormClosing);
                    this.Close();
                }
            }
        }

        private void MZSHZT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRead)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZSHZT_FormClosing);
                this.Close();
            }
            else if (BCcount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZSHZT_FormClosing);
                this.Close();
            }

            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZSHZT_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.MZSHZT_FormClosing);
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                string dtTime = DateTime.Now.ToString("HH:mm");
                _ShztDal.InsertAfterAnalgesiaDetail(MZID, dtTime);
                BindAfterAnalgesiaDetail();
            }
            else if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.CurrentCell.IsInEditMode == false)
                {
                    string dtTime = DateTime.Now.ToString("HH:mm");
                    _ShztDal.InsertAfterAnalgesiaDetail(MZID, dtTime);
                    BindAfterAnalgesiaDetail();
                }
                else MessageBox.Show("单元格内不能为编辑状态！");
            }
        }
        /// <summary>
        /// 绑定
        /// </summary>
        private void BindAfterAnalgesiaDetail()
        {
            DataTable dt = _ShztDal.GetAfterAnalgesiaDetail(MZID);
            dataGridView1.DataSource = dt.DefaultView;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string idno = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            _ShztDal.DeleteAfterAnalgesiaDetail(idno);
            BindAfterAnalgesiaDetail();
        }
        /// <summary>
        /// 编辑dgv的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (e.ColumnIndex == 3)
                    {
                        if (dataGridView1.CurrentCell.Value.ToString() == "")
                        {
                            MessageBox.Show("时间不能为空，请重新修改");
                            return;
                        }
                    }
                    //DateTime dtime = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["Timeshort"].Value);
                    string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                    string RoomName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
                    string Value_Info = dataGridView1.CurrentCell.Value.ToString();
                    _ShztDal.UpdateAfterAnalgesiaDetail(ID, RoomName, Value_Info);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("时间输入格式不正确！\n输入格式为HH:MM");
                BindAfterAnalgesiaDetail();
            }
        }
        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDY_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 740, 1020);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("新宋体", 10);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            int x = 40; int y = 40, y1 = 0;
            string title1 = "天津红桥医院术后镇痛观察记录单";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 150, y));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawString("姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("性别：" + tbPatsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 120, y));
            e.Graphics.DrawString("年龄：" + this.tbPatage.Text.Trim() + "岁", ptzt, Brushes.Black, new Point(x + 210, y));
            e.Graphics.DrawString("科别：" + this.tbKeshi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
            e.Graphics.DrawString("住院号：" + this.tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 400, y));
            e.Graphics.DrawString("日期：" + this.dtVisitDate.Value.Date.ToString("yyyy年MM月dd日"), ptzt, Brushes.Black, new Point(x + 520, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("手术名称：" + txtSsmc.Text, ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("麻醉方法：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 12, 12);
            e.Graphics.DrawString("全身麻醉", ptzt, Brushes.Black, x + 100, y);
            if (cmbMzff.Text == "全身麻醉")
            {
                e.Graphics.DrawLine(pblue2, x + 80, y, x + 85, y + 12);
                e.Graphics.DrawLine(pblue2, x + 85, y + 12, x + 92, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 12, 12);
            e.Graphics.DrawString("椎管内麻醉", ptzt, Brushes.Black, x + 190, y);
            if (cmbMzff.Text == "椎管内麻醉")
            {
                e.Graphics.DrawLine(pblue2, x + 170, y, x + 175, y + 12);
                e.Graphics.DrawLine(pblue2, x + 175, y + 12, x + 182, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 12, 12);
            e.Graphics.DrawString("神经阻滞麻醉", ptzt, Brushes.Black, x + 290, y);
            if (cmbMzff.Text == "神经阻滞麻醉")
            {
                e.Graphics.DrawLine(pblue2, x + 270, y, x + 275, y + 12);
                e.Graphics.DrawLine(pblue2, x + 275, y + 12, x + 282, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 12, 12);
            e.Graphics.DrawString("MAC", ptzt, Brushes.Black, x + 410, y);
            if (cmbMzff.Text == "MAC")
            {
                e.Graphics.DrawLine(pblue2, x + 390, y, x + 395, y + 12);
                e.Graphics.DrawLine(pblue2, x + 395, y + 12, x + 402, y - 1);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("镇痛方法：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 12, 12);
            e.Graphics.DrawString("PCEA", ptzt, Brushes.Black, x + 100, y);
            if (cmbZtff.Text == "PCEA")
            {
                e.Graphics.DrawLine(pblue2, x + 80, y, x + 85, y + 12);
                e.Graphics.DrawLine(pblue2, x + 85, y + 12, x + 92, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 140, y, 12, 12);
            e.Graphics.DrawString("PCIA", ptzt, Brushes.Black, x + 160, y);
            if (cmbZtff.Text == "PCIA")
            {
                e.Graphics.DrawLine(pblue2, x + 140, y, x + 145, y + 12);
                e.Graphics.DrawLine(pblue2, x + 145, y + 12, x + 152, y - 1);
            }
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 12, 12);
            e.Graphics.DrawString("其它", ptzt, Brushes.Black, x + 220, y);
            if (cmbZtff.Text == "其它")
            {
                e.Graphics.DrawLine(pblue2, x + 200, y, x + 205, y + 12);
                e.Graphics.DrawLine(pblue2, x + 205, y + 12, x + 212, y - 1);
            }
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("镇痛配方：" + txtZtpf.Text, ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("镇痛泵参数：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("首剂 " + lineZTsj.Text, ptzt, Brushes.Black, new Point(x + 90, y));
            e.Graphics.DrawLine(Pens.Black, x + 120, y1, x + 170, y1);
            e.Graphics.DrawString("ml；持续给药速度 " + lineZTGysd.Text, ptzt, Brushes.Black, new Point(x + 175, y));
            e.Graphics.DrawLine(Pens.Black, x + 290, y1, x + 330, y1);
            e.Graphics.DrawString("ml/h；单次追加量 " + lineZTDczj.Text, ptzt, Brushes.Black, new Point(x + 335, y));
            e.Graphics.DrawLine(Pens.Black, x + 450, y1, x + 490, y1);
            e.Graphics.DrawString("ml；锁定时间 " + lineZTSdsj.Text, ptzt, Brushes.Black, new Point(x + 495, y));
            e.Graphics.DrawLine(Pens.Black, x + 580, y1, x + 630, y1);
            e.Graphics.DrawString("min", ptzt, Brushes.Black, new Point(x + 635, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("开始时间  " + dtZTKssj.Text, ptzt, Brushes.Black, new Point(x + 90, y));
            e.Graphics.DrawLine(Pens.Black, x + 150, y1, x + 240, y1);
            e.Graphics.DrawString("；结束时间  " + dtZTjssj.Text, ptzt, Brushes.Black, new Point(x + 245, y));
            e.Graphics.DrawLine(Pens.Black, x + 320, y1, x + 435, y1);
            y = y + 50; y1 = y + 15;
            int YY = y;
            #region 画表格
            ///画横线
            for (int i = 0; i < 20; i++)
            {

                if (i == 1)
                {
                    e.Graphics.DrawLine(Pens.Black, x + 160, YY, x + 630, YY);
                }
                else if (i == 3 || i == 4 || i == 5 || i == 11 || i == 12 || i == 13 || i == 14)
                {
                    e.Graphics.DrawLine(Pens.Black, x + 50, YY, x + 630, YY);
                }
                else if (i == 7)
                {
                    e.Graphics.DrawLine(Pens.Black, x + 70, YY, x + 630, YY);
                }
                else if (i == 17 || i == 18)
                { }
                else
                {
                    e.Graphics.DrawLine(Pens.Black, x + 10, YY, x + 630, YY);
                }
                YY = YY + 20;
            }
            //画竖线
            e.Graphics.DrawLine(Pens.Black, x + 10, y, x + 10, y + 20 * 19);
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 20 * 2, x + 50, y + 20 * 6);
            e.Graphics.DrawLine(Pens.Black, x + 70, y + 20 * 6, x + 70, y + 20 * 8);
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 20 * 10, x + 50, y + 20 * 15);
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 20 * 16, x + 50, y + 20 * 19);
            e.Graphics.DrawLine(Pens.Black, x + 160, y, x + 160, y + 20 * 16);
            e.Graphics.DrawLine(Pens.Black, x + 160 + 117, y + 20, x + 160 + 117, y + 20 * 16);
            e.Graphics.DrawLine(Pens.Black, x + 160 + 117 * 2, y + 20, x + 160 + 117 * 2, y + 20 * 16);
            e.Graphics.DrawLine(Pens.Black, x + 160 + 117 * 3, y + 20, x + 160 + 117 * 3, y + 20 * 16);
            e.Graphics.DrawLine(Pens.Black, x + 630, y, x + 630, y + 20 * 19);
            e.Graphics.DrawString("观察指标", ptzt, Brushes.Black, new Point(x + 40, y + 10 + 3));
            e.Graphics.DrawString("随  访  时  间", ptzt, Brushes.Black, new Point(x + 330, y + 3));
            e.Graphics.DrawString("身命", ptzt, Brushes.Black, new Point(x + 10, y + 60 + 3));
            e.Graphics.DrawString("体征", ptzt, Brushes.Black, new Point(x + 10, y + 80 + 3));
            e.Graphics.DrawString("血压（mmHg）", ptzt, Brushes.Black, new Point(x + 60, y + 40 + 3));
            e.Graphics.DrawString("呼吸（次/分）", ptzt, Brushes.Black, new Point(x + 60, y + 60 + 3));
            e.Graphics.DrawString("脉搏（次/分）", ptzt, Brushes.Black, new Point(x + 60, y + 80 + 3));
            e.Graphics.DrawString("SpO2（%）", ptzt, Brushes.Black, new Point(x + 60, y + 100 + 3));
            e.Graphics.DrawString("VAS镇痛", ptzt, Brushes.Black, new Point(x + 10, y + 120 + 3));
            e.Graphics.DrawString("评分", ptzt, Brushes.Black, new Point(x + 25, y + 140 + 3));
            e.Graphics.DrawString("静息", ptzt, Brushes.Black, new Point(x + 90, y + 120 + 3));
            e.Graphics.DrawString("运动", ptzt, Brushes.Black, new Point(x + 90, y + 140 + 3));
            e.Graphics.DrawString("镇静状态评分", ptzt, Brushes.Black, new Point(x + 30, y + 160 + 3));
            e.Graphics.DrawString("肌力评分", ptzt, Brushes.Black, new Point(x + 40, y + 180 + 3));
            e.Graphics.DrawString("不良", ptzt, Brushes.Black, new Point(x + 10, y + 230 + 3));
            e.Graphics.DrawString("反应", ptzt, Brushes.Black, new Point(x + 10, y + 250 + 3));
            e.Graphics.DrawString("恶心、呕吐", ptzt, Brushes.Black, new Point(x + 60, y + 200 + 3));
            e.Graphics.DrawString("皮肤瘙痒", ptzt, Brushes.Black, new Point(x + 60, y + 220 + 3));
            e.Graphics.DrawString("头晕、头痛", ptzt, Brushes.Black, new Point(x + 60, y + 240 + 3));
            e.Graphics.DrawString("尿潴留", ptzt, Brushes.Black, new Point(x + 70, y + 260 + 3));
            e.Graphics.DrawString("其它", ptzt, Brushes.Black, new Point(x + 70, y + 280 + 3));
            e.Graphics.DrawString("麻醉医师签字", ptzt, Brushes.Black, new Point(x + 30, y + 300 + 3));
            e.Graphics.DrawString("并发", ptzt, Brushes.Black, new Point(x + 10, y + 320 + 3));
            e.Graphics.DrawString("症处", ptzt, Brushes.Black, new Point(x + 10, y + 340 + 3));
            e.Graphics.DrawString("理", ptzt, Brushes.Black, new Point(x + 15, y + 360 + 3));
            int xx = 160 + x;
            ///表格赋值
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i > 4)
                {
                    return;
                }
                e.Graphics.DrawString(dtVisitDate.Value.ToString("yyyy/MM/dd") + " " + dataGridView1.Rows[i].Cells["VisitTime"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 20 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Xueya"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 40 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Huxi"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 60 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Maibo"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 80 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["SPO2"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 100 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["VAS_jx"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 120 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["VAS_yd"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 140 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Zjztpf"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 160 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Jlpf"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 180 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Exot"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 200 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Pfsy"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 220 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Tytt"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 240 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Nzl"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 260 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Qt"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 280 + 3));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["Mzys"].Value.ToString(), ptzt, Brushes.Black, new Point(xx + 10, y + 300 + 3));
                xx = xx + 117;

            }
            ///并发症处理
            int BZYYSS = y + 320;
            string strSS1 = "";
            int StrLengthSS = txtBfzcl.Text.Trim().Length;
            int rowSS = StrLengthSS / 40;
            for (int i = 0; i <= rowSS; i++)//40个字符就换行
            {
                if (i < rowSS)
                    strSS1 = txtBfzcl.Text.Trim().ToString().Substring(i * 40, 40); //从第i*40个开始，截取40个字符串
                else
                    strSS1 = txtBfzcl.Text.Trim().ToString().Substring(i * 40);

                e.Graphics.DrawString(strSS1, ptzt, Brushes.Black, x + 50, BZYYSS + 3);
                BZYYSS = BZYYSS + 20;
            }
            #endregion
            y = y + 400;
            e.Graphics.DrawString("注：（1）VAS镇痛评分0-无痛；1-3为轻度；4-6为中度；7-10为重度。", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("（2）静脉评分：0分=清醒；1分=呼之睁眼；2分=摇能睁眼；3分=不能唤醒。", ptzt, Brushes.Black, new Point(x + 38, y));
            y = y + 20;
            e.Graphics.DrawString("（3）肌力：0完全瘫痪；1可收缩；2不能抗重力；3抗重力不抗阻力；4可抗弱阻力；5正常。", ptzt, Brushes.Black, new Point(x + 38, y));
            y = y + 20;
            e.Graphics.DrawString("（4）恶心呕吐：0无;1轻度，可忍受；2难以忍受，需治疗甚至终止PCA。", ptzt, Brushes.Black, new Point(x + 38, y));
            y = y + 20;
            e.Graphics.DrawString("（5）皮肤瘙痒：0无；1轻度，可忍受；2严重，需治疗甚至终止PCA。", ptzt, Brushes.Black, new Point(x + 38, y));
            y = y + 20;
            e.Graphics.DrawString("（6）头晕、头痛：0无；1轻度，可忍受；2严重。", ptzt, Brushes.Black, new Point(x + 38, y));
            y = y + 20;
            e.Graphics.DrawString("（7）尿潴留：0无；1有。", ptzt, Brushes.Black, new Point(x + 38, y));
            y = y + 20;
            e.Graphics.DrawString("VAS评分数：" + cmbVASpf.Text, ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 50;
            e.Graphics.DrawString("0", ptzt, Brushes.Black, new Point(x + 110, y - 30));
            e.Graphics.DrawString("2", ptzt, Brushes.Black, new Point(x + 190, y - 30));
            e.Graphics.DrawString("4", ptzt, Brushes.Black, new Point(x + 280, y - 30));
            e.Graphics.DrawString("6", ptzt, Brushes.Black, new Point(x + 360, y - 30));
            e.Graphics.DrawString("8", ptzt, Brushes.Black, new Point(x + 450, y - 30));
            e.Graphics.DrawString("10", ptzt, Brushes.Black, new Point(x + 530, y - 30));
            Image temp = new Bitmap(Application.StartupPath + "\\VAS.png");
            Graphics g = Graphics.FromImage(temp);
            int width = temp.Width;
            int height = temp.Height;
            Rectangle destRect = new Rectangle(x + 80, y, width, height);
            e.Graphics.DrawImage(temp, destRect, 0, 0, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);
        }


    }
}
