using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL.Dics;
using adims_DAL.Flows;


namespace main
{
    public partial class NurseRecord_HQ : Form
    {
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        DataDicDal _DataDicDal = new DataDicDal();
        NurseRecordDal _NurseRecordDal = new NurseRecordDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_DAL.PacuDal pdal = new adims_DAL.PacuDal();
        string  _MzjldID, _PatId;
        string Odate;
        public NurseRecord_HQ(string mzid,string patid,string date)
        {
            _MzjldID = mzid;
            _PatId = patid;
            Odate = date;
            InitializeComponent();
        }
        int BCCount = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            SaveQXQD();
        }
        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            Dictionary<string, string> A_Visit = new Dictionary<string, string>();
            int result = 0;
            try
            {
                A_Visit.Add("mzjldid", _MzjldID);
                A_Visit.Add("XueType", cmbXueType.Text);
                A_Visit.Add("XYCFName", cmbXYCFName.Text);
                A_Visit.Add("XL", tbXL.Text);
                A_Visit.Add("SXDW", cmbSXDW.Text);
                A_Visit.Add("QXHS", cmbQXHS.Text.Trim());
                A_Visit.Add("XHHS", cmbXHHS.Text.Trim());
                A_Visit.Add("QXHS1", cmbQXHS1.Text.Trim());
                A_Visit.Add("XHHS1", cmbXHHS1.Text.Trim());
                A_Visit.Add("QXHS2", cmbQXHS2.Text.Trim());
                A_Visit.Add("XHHS2", cmbXHHS2.Text.Trim());
                if (dgvQXQD.Rows.Count>0)
                {
                    A_Visit.Add("qxbName", comboBox1.Text);
                }
                else
                {
                    A_Visit.Add("qxbName","");
                }
                A_Visit.Add("Odate", Convert.ToDateTime(Odate).ToString("yyyy-MM-dd"));           
                result = _NurseRecordDal.UpdateNurseRecord_HQ(A_Visit);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");
                    BCCount++;
                }
                else
                    MessageBox.Show("保存失败");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        private void NurseRecord_HQ_Load(object sender, EventArgs e)
        {
            try
            {
                Bind_QxModel();
                Load_info();
                Bind_QX_XHHS();
                dgvBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
        /// <summary>
        /// 加载选择病人信息
        /// </summary>
        public void Load_info()
        {
            DataTable dt1 = _PaibanDal.GetPaibanByPatId(_PatId);
            DataRow dr1 = dt1.Rows[0];
            tbKeshi.Text = dr1["patdpm"].ToString();
            tbPatname.Text = dr1["Patname"].ToString();
            tbSex.Text = dr1["patsex"].ToString();
            tbAge.Text = dr1["patage"].ToString();
            tbZhuyuanID.Text = dr1["patZhuyuanID"].ToString();
            tbShoushuName.Text = dr1["oname"].ToString();
            DataTable dt = _NurseRecordDal.GetNurseRecord_HQ(_MzjldID);
            if (dt.Rows.Count == 0)
            {
                _NurseRecordDal.InsertNurseRecord_HQ(_MzjldID);              
            }
            else
            {
                DataRow dr = dt.Rows[0];
                cmbXueType.Text = dr["xueType"].ToString();
                cmbXYCFName.Text = dr["XYCFName"].ToString();
                tbXL.Text = dr["XL"].ToString();
                cmbSXDW.Text = dr["SXDW"].ToString();
                cmbXHHS.Text = dr["XHHS"].ToString();
                cmbQXHS.Text = dr["QXHS"].ToString();
                cmbXHHS1.Text = dr["XHHS1"].ToString();
                cmbQXHS1.Text = dr["QXHS1"].ToString();
                cmbXHHS2.Text = dr["XHHS2"].ToString();
                cmbQXHS2.Text = dr["QXHS2"].ToString();
                if (Convert.ToInt32(dr["IsRead"].ToString()) == 0)
                    btnSave.Enabled = true;
                else
                    btnSave.Enabled = false;
                if (dr["qxbName"].ToString()!="")
                {
                    //QXType(dr["qxbName"].ToString());
                    comboBox1.Text = dr["qxbName"].ToString();
                }
            }
        }
        private void Bind_QxModel()
        {
            comboBox1.Items.Clear();
            DataTable dt = DAL.GetallQxModel();
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "qxbType";
        }
        private void Bind_QX_XHHS()
        {
            cmbQXHS.Items.Clear();
            cmbXHHS.Items.Clear();
            cmbQXHS1.Items.Clear();
            cmbXHHS1.Items.Clear();
            cmbQXHS2.Items.Clear();
            cmbXHHS2.Items.Clear();
            DataTable dt = new DataTable();
            dt = _DataDicDal.GetAll_hushi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbQXHS.Items.Add(dt.Rows[i][0]);
                cmbXHHS.Items.Add(dt.Rows[i][0]);
                cmbQXHS1.Items.Add(dt.Rows[i][0]);
                cmbXHHS1.Items.Add(dt.Rows[i][0]);
                cmbQXHS2.Items.Add(dt.Rows[i][0]);
                cmbXHHS2.Items.Add(dt.Rows[i][0]);
            }
        }
        /// <summary>
        /// 绑定DGV
        /// </summary>
        private void dgvBind()
        {
            try
            {
                DataTable tb = DAL.GetBindQX(_MzjldID);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (i >= dgvQXQD.Rows.Count)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="QXType"></param>
        private void QXType(string QXType) 
        {
            if (QXType=="")
            {
                return;                
            }
            if (dgvQXQD.Rows.Count > 0)
            {
                dgvQXQD.Rows.Clear();
            }
            int i = 0;
            //DataTable dtss = DAL.GetallQxModelss();
            //string name = dtss.Rows[0][1].ToString();
            DataTable dts = DAL.SelectqxmcInmodelss(QXType,"敷料类");
            int dtCounts = dts.Rows.Count;
            int js = dts.Rows.Count / 3;
            dgvQXQD.Rows.Add(js + 1);
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
            DataTable dt = DAL.SelectqxmcInmodelss(QXType,"物品类");
            int dtCount = dt.Rows.Count;
            int s = dt.Rows.Count / 3;
            //dgvQXQD.Rows.Clear();
            dgvQXQD.Rows.Add(s + 1);
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

            DataTable dt2 = DAL.SelectqxmcInmodelss(QXType, "器械类");
            int dtCount2 = dt2.Rows.Count;
            int s2 = dt2.Rows.Count / 3;
            //dgvQXQD.Rows.Clear();
            dgvQXQD.Rows.Add(s2 + 1);
            for (int j = 0; j < dtCount2; j++)
            {
                if (i % 3 == 0)
                {
                    dgvQXQD.Rows[i / 3].Cells[0].Value = dt2.Rows[j][0];
                    dgvQXQD.Rows[i / 3].Cells[0].Style.ForeColor = Color.Blue;
                    dgvQXQD.Rows[i / 3].Cells[1].Value = dt2.Rows[j][1];
                }
                else if (i % 3 == 1)
                {
                    dgvQXQD.Rows[i / 3].Cells[4].Value = dt2.Rows[j][0];
                    dgvQXQD.Rows[i / 3].Cells[5].Value = dt2.Rows[j][1];
                    dgvQXQD.Rows[i / 3].Cells[4].Style.ForeColor = Color.Blue;
                }
                else if (i % 3 == 2)
                {
                    dgvQXQD.Rows[i / 3].Cells[8].Value = dt2.Rows[j][0];
                    dgvQXQD.Rows[i / 3].Cells[9].Value = dt2.Rows[j][1];
                    dgvQXQD.Rows[i / 3].Cells[8].Style.ForeColor = Color.Blue;
                }
                i++;
            } 
        }
        private void btnConfig_Click(object sender, EventArgs e)
        {
            QXType(comboBox1.Text);          
        }
       
        private void button1_Click(object sender, EventArgs e)
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
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 740, 1020);  
            //printDocument1.DefaultPageSettings.PaperSize =
            //         new System.Drawing.Printing.PaperSize("A4", 830, 1040);//("A4", 1487, 2105);
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {            
            Font zt8 = new Font("宋体", 8);
            Font zt9 = new Font("宋体", 9);
            Font ht9 = new Font("黑体", 9);
            Font ht14 = new Font("黑体", 12);
            Pen pb1 = new Pen(Brushes.Black);
            Pen pb2 = new Pen(Brushes.Black, 2);//普通画笔  
            Brush bp1 = Brushes.Black;
            int x = 60, y = 0, y1 = 0;
            y = y + 30;
            string title1 = "红桥医院手术护理记录单";
            e.Graphics.DrawString(title1, ht14, Brushes.Black, x + 200, y);
            y = y + 30;
            e.Graphics.DrawString("（" + comboBox1.Text + "）", zt9, Brushes.Black, x + 260, y);
            y = y + 20; y1 = y;
            e.Graphics.DrawLine(pb1, x, y, x + 630, y);
            e.Graphics.DrawString("科别: " + tbKeshi.Text.Trim(), zt9, Brushes.Black, x + 5, y + 5);
            e.Graphics.DrawString("姓名: " + tbPatname.Text.Trim(), zt9, Brushes.Black, x + 130, y + 5);
            e.Graphics.DrawString("性别: " + tbSex.Text.Trim(), zt9, Brushes.Black, x + 240, y + 5);
            e.Graphics.DrawString("年龄: " + tbAge.Text.Trim() + "  岁", zt9, Brushes.Black, x + 320, y + 5);
            e.Graphics.DrawString("住院号: " + tbZhuyuanID.Text.Trim(), zt9, Brushes.Black, x + 430, y + 5);
            y = y + 20;
            e.Graphics.DrawLine(pb1, x, y, x + 630, y);
            e.Graphics.DrawString("手术日期： " + Convert.ToDateTime(dtVisitDate.Text.Trim()).ToString("yyyy年MM月dd日"), zt9, Brushes.Black, x + 5, y + 5);
            e.Graphics.DrawString("手术名称: " + tbShoushuName.Text.Trim(), zt9, Brushes.Black, x + 200, y + 5);
            y = y + 20; int YY = y;
            int FZYY = y;
            int FZYY1 = y;
            int FZYY2 = y;
            int cots = 0; bool res = false;
            int cots1 = 0; bool res1 = false;
            int cots2 = 0; bool res2 = false;
            #region 赋值
            int hangshu = 0;//判断行数
            DataTable dt1 = DAL.SelectqxmcInmodelss(comboBox1.Text, "敷料类");
            DataTable dt2 = DAL.SelectqxmcInmodelss(comboBox1.Text, "物品类");
            DataTable dt3 = DAL.SelectqxmcInmodelss(comboBox1.Text, "器械类");
            int count1 = dt1.Rows.Count;
            int count2 = dt2.Rows.Count;
            int count3 = dt3.Rows.Count;
            if (count1 - count2 > 0)
                hangshu = count1;
            else
                hangshu = count2;
            if (hangshu < 8)
                hangshu = 8;
            else
                hangshu++;
            e.Graphics.DrawString("敷料类", zt9, Brushes.Black, x + 30, FZYY + 5);
            e.Graphics.DrawString("术前", zt9, Brushes.Black, x + 125, FZYY + 5);
            e.Graphics.DrawString("关前", zt9, Brushes.Black, x + 195, FZYY + 5);
            e.Graphics.DrawString("关后", zt9, Brushes.Black, x + 265, FZYY + 5);
            e.Graphics.DrawString("物品类", zt9, Brushes.Black, x + 345, FZYY + 5);
            e.Graphics.DrawString("术前", zt9, Brushes.Black, x + 440, FZYY + 5);
            e.Graphics.DrawString("关前", zt9, Brushes.Black, x + 510, FZYY + 5);
            e.Graphics.DrawString("关后", zt9, Brushes.Black, x + 580, FZYY + 5);

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                DataTable dt11 = DAL.GetBindQX(_MzjldID, dt1.Rows[i][0].ToString());
                if (dt11.Rows.Count==0)
                {
                    break;
                }
                for (int s = 0; s < dt11.Columns.Count; s++)
                {
                    if (dt11.Rows[0][s].ToString() == dt1.Rows[i][0].ToString())
                    {
                        cots = s;
                        res = true;
                    }
                }
                if (dt11.Rows.Count > 0&&res)
                {
                    FZYY = FZYY + 20;
                    e.Graphics.DrawString(dt11.Rows[0][cots].ToString(), zt9, Brushes.Black, x + 5, FZYY + 5);
                    e.Graphics.DrawString(dt11.Rows[0][cots+1].ToString(), zt9, Brushes.Black, x + 110, FZYY + 5);
                    e.Graphics.DrawString(dt11.Rows[0][cots+2].ToString(), zt9, Brushes.Black, x + 180, FZYY + 5);
                    e.Graphics.DrawString(dt11.Rows[0][cots+3].ToString(), zt9, Brushes.Black, x + 250, FZYY + 5);
                }
            }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                DataTable dt22 = DAL.GetBindQX(_MzjldID, dt2.Rows[i][0].ToString());
                if (dt22.Rows.Count == 0)
                {
                    break;
                }
                for (int s = 0; s < dt22.Columns.Count; s++)
                {
                    if (dt22.Rows[0][s].ToString() == dt2.Rows[i][0].ToString())
                    {
                        cots1 = s;
                        res1 = true;
                    }
                }
                if (dt22.Rows.Count > 0&&res1)
                {
                    FZYY1 = FZYY1 + 20;
                    e.Graphics.DrawString(dt22.Rows[0][cots1].ToString(), zt9, Brushes.Black, x + 320, FZYY1 + 5);
                    e.Graphics.DrawString(dt22.Rows[0][cots1 + 1].ToString(), zt9, Brushes.Black, x + 425, FZYY1 + 5);
                    e.Graphics.DrawString(dt22.Rows[0][cots1 + 2].ToString(), zt9, Brushes.Black, x + 495, FZYY1 + 5);
                    e.Graphics.DrawString(dt22.Rows[0][cots1 + 3].ToString(), zt9, Brushes.Black, x + 565, FZYY1 + 5);
                }
            }
            int FZYY3 = 0;
            FZYY2 = FZYY2 + hangshu * 20;
            FZYY3 = FZYY2;
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                DataTable dt33 = DAL.GetBindQX(_MzjldID, dt3.Rows[i][0].ToString());
                if (dt33.Rows.Count == 0)
                {
                    break;
                }
                for (int s = 0; s < dt33.Columns.Count; s++)
                {
                    if (dt33.Rows[0][s].ToString() == dt3.Rows[i][0].ToString())
                    {
                        cots2 = s;
                        res2 = true;
                    }
                }
                if (dt33.Rows.Count > 0 && res2)
                {
                    if (hangshu<36)
                    {
                        FZYY2 = FZYY2 + 20;
                        e.Graphics.DrawString(dt33.Rows[0][cots2].ToString(), zt9, Brushes.Black, x + 5, FZYY2 + 5);
                        e.Graphics.DrawString(dt33.Rows[0][cots2 + 1].ToString(), zt9, Brushes.Black, x + 110, FZYY2 + 5);
                        e.Graphics.DrawString(dt33.Rows[0][cots2 + 2].ToString(), zt9, Brushes.Black, x + 180, FZYY2 + 5);
                        e.Graphics.DrawString(dt33.Rows[0][cots2 + 3].ToString(), zt9, Brushes.Black, x + 250, FZYY2 + 5);
                    }
                    else
                    {
                        FZYY3 = FZYY3 + 20;
                        e.Graphics.DrawString(dt33.Rows[0][cots2].ToString(), zt9, Brushes.Black, x + 320, FZYY3 + 5);
                        e.Graphics.DrawString(dt33.Rows[0][cots2 + 1].ToString(), zt9, Brushes.Black, x + 425, FZYY3 + 5);
                        e.Graphics.DrawString(dt33.Rows[0][cots2 + 2].ToString(), zt9, Brushes.Black, x + 495, FZYY3 + 5);
                        e.Graphics.DrawString(dt33.Rows[0][cots2 + 3].ToString(), zt9, Brushes.Black, x + 565, FZYY3 + 5);
                    }                 
                }
                hangshu++;
            }
            #endregion
            #region 画表格
            for (int i = 0; i < 42; i++)
            {
                if (i == 38)
                {
                    e.Graphics.DrawLine(pb1, x + 90, y, x + 630, y);
                    //画竖线
                    e.Graphics.DrawLine(pb1, x + 90, y - 20, x + 90, y + 60);
                    e.Graphics.DrawLine(pb1, x + 180, y , x + 180, y + 60);
                    e.Graphics.DrawLine(pb1, x + 270, y - 20, x + 270, y + 60);
                    e.Graphics.DrawLine(pb1, x + 360, y , x + 360, y + 60);
                    e.Graphics.DrawLine(pb1, x + 450, y - 20, x + 450, y + 60);
                    e.Graphics.DrawLine(pb1, x + 540, y, x + 540, y + 60);

                }
                else if (i == 39)
                {
                    e.Graphics.DrawLine(pb1, x + 90, y, x + 630, y);
                }
                else if (i == 40)
                {
                }
                else
                {
                    e.Graphics.DrawLine(pb1, x, y, x + 630, y);
                }
                y = y + 20;
            }
            y = y - 20;
            e.Graphics.DrawLine(pb1, x, y1, x, y);
            e.Graphics.DrawLine(pb1, x + 105, YY, x + 105, y - 80);
            e.Graphics.DrawLine(pb1, x + 175, YY, x + 175, y - 80);
            e.Graphics.DrawLine(pb1, x + 245, YY, x + 245, y - 80);
            e.Graphics.DrawLine(pb1, x + 315, YY, x + 315, y - 80);
            e.Graphics.DrawLine(pb1, x + 420, YY, x + 420, y - 80);
            e.Graphics.DrawLine(pb1, x + 490, YY, x + 490, y - 80);
            e.Graphics.DrawLine(pb1, x + 560, YY, x + 560, y - 80);
            e.Graphics.DrawLine(pb1, x + 630, y1, x + 630, y);
            e.Graphics.DrawString("清点人", zt9, Brushes.Black, x + 20,y-80+35);
            e.Graphics.DrawString("术前", zt9, Brushes.Black, x + 170, y - 80 +5);
            e.Graphics.DrawString("关前", zt9, Brushes.Black, x + 350, y - 80 + 5);
            e.Graphics.DrawString("关后", zt9, Brushes.Black, x + 530, y - 80 + 5);
            e.Graphics.DrawString("器械护士", zt9, Brushes.Black, x + 100+10, y - 80 + 5+20);
            e.Graphics.DrawString("巡回护士", zt9, Brushes.Black, x + 190 + 5, y - 80 + 5 + 20);
            e.Graphics.DrawString("器械护士", zt9, Brushes.Black, x + 280 + 5, y - 80 + 5 + 20);
            e.Graphics.DrawString("巡回护士", zt9, Brushes.Black, x + 370 + 5, y - 80 + 5 + 20);
            e.Graphics.DrawString("器械护士", zt9, Brushes.Black, x + 460 + 5, y - 80 + 5 + 20);
            e.Graphics.DrawString("巡回护士", zt9, Brushes.Black, x + 550 + 5, y - 80 + 5 + 20);
            e.Graphics.DrawString(cmbQXHS.Text, zt9, Brushes.Black, x + 100, y - 80 + 5 + 50);
            e.Graphics.DrawString(cmbXHHS.Text, zt9, Brushes.Black, x + 190 , y - 80 + 5 + 50);
            e.Graphics.DrawString(cmbQXHS1.Text, zt9, Brushes.Black, x + 280, y - 80 + 5 + 50);
            e.Graphics.DrawString(cmbXHHS1.Text, zt9, Brushes.Black, x + 370, y - 80 + 5 + 50);
            e.Graphics.DrawString(cmbQXHS2.Text, zt9, Brushes.Black, x + 460, y - 80 + 5 + 50);
            e.Graphics.DrawString(cmbXHHS2.Text, zt9, Brushes.Black, x + 550, y - 80 + 5 + 50);
            #endregion
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
            DataTable dt = _NurseRecordDal.GetNurseRecord_HQ(_MzjldID);
            if (dt.Rows.Count > 0)
            {
                result = _NurseRecordDal.UpdateNurseRecord_HQ(_MzjldID, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    btnSave.Enabled = false;
                }
            }
        }          
        /// <summary>
        /// 器械清点
        /// </summary>
        private void SaveQXQD()
        {
            DataTable dt = DAL.GetNurseRecordQX(_MzjldID);
            if (dt.Rows.Count > 0)
            {
                //存在数据就删除
                int s = DAL.DeleteNurseRecordQX(_MzjldID, _PatId);
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
                    QXList.Add("patid", _PatId);
                    QXList.Add("mzjldid", _MzjldID);
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
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = _NurseRecordDal.GetNurseRecord_HQ(_MzjldID);
            if (dt.Rows.Count > 0)
            {
                result = _NurseRecordDal.UpdateNurseRecord_HQ(_MzjldID, 0);
                if (result > 0)
                {
                    MessageBox.Show("解锁成功！");
                    btnSave.Enabled = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Save();
                this.Close();
            }
            else
                this.Close();

        }
    }
}
