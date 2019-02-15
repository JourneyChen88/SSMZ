using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_DAL.Flows;
using adims_BLL;

namespace main
{
    public partial class BeforeVisit_HQ : Form
    {
        adims_DAL.Dics.DataDicDal _DataDicDal = new adims_DAL.Dics.DataDicDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.PacuDal _PacuDal = new adims_DAL.PacuDal();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        string PatId, Odate;
        bool isRead = false;
        public BeforeVisit_HQ(string patid, string date)
        {
            PatId = patid;
            Odate = date;
            InitializeComponent();
        }

        private void BeforeVisit_HQ_Load(object sender, EventArgs e)
        {
            if (_BeforeVistDal.GetBeforeVist_YS(PatId).Rows.Count == 0)
            {
                _BeforeVistDal.InsertBeforeVisit_YS(PatId, 0, DateTime.Now.ToString("yyyy-MM-dd"));
            }
            Load_info();
            BindMZYY();
            cmbMzys.Text = Program.customer.user_name;
            BindMZCC();
            LodSQFS();

        }
        /// <summary>
        /// 绑定麻醉医师
        /// </summary>
        private void BindMZYY()
        {
            DataTable dtMZYS = _DataDicDal.GetAllMZYS();
            cmbMzys.Items.Clear();
            for (int i = 0; i < dtMZYS.Rows.Count; i++)
            {
                this.cmbMzys.Items.Add(dtMZYS.Rows[i][0]);
            }
        }
        private void BindMZCC()
        {
            DataTable dtMZYS = _DataDicDal.GetAllMZCC();
            cmbZgCC.Items.Clear();
            for (int i = 0; i < dtMZYS.Rows.Count; i++)
            {
                this.cmbZgCC.Items.Add(dtMZYS.Rows[i][0]);
            }
        }
        /// <summary>
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
            tbSQZD.Text = dr1["pattmd"].ToString();
            txtch.Text = dr1["Patbedno"].ToString();
            tbNDSS.Text = dr1["oname"].ToString();
            txtHeith.Text = dr1["PatHeight"].ToString();
            txtWeith.Text = dr1["PatWeight"].ToString();
            dtVisitDate.Text = Odate;
        }
        int SaveCount = 0;
        /// <summary>
        /// 赋值
        /// </summary>
        BeforeVisitDal _BeforeVistDal = new BeforeVisitDal();
        private void LodSQFS()
        {
            DataTable dt = new DataTable();
            dt = _BeforeVistDal.GetBeforeVist_YS(PatId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (dr["Sqzd"].ToString() != "")
                {
                    tbSQZD.Text = dr["Sqzd"].ToString();
                }
                if (dr["Nsss"].ToString() != "")
                {
                    tbNDSS.Text = dr["Nsss"].ToString();
                }

                cmbGms.Text = dr["Gms"].ToString();
                if (Convert.ToString(dr["Gxz"]).Contains("1")) Gxz1.Checked = true;
                if (Convert.ToString(dr["Gxz"]).Contains("2")) Gxz2.Checked = true;
                if (Convert.ToString(dr["Gxz"]).Contains("3")) Gxz3.Checked = true;
                txtGqt.Text = dr["Gqt"].ToString();
                cmbMzs.Text = dr["Mzs"].ToString();
                if (Convert.ToString(dr["Mxz"]).Contains("1")) Mxz1.Checked = true;
                if (Convert.ToString(dr["Mxz"]).Contains("2")) Mxz2.Checked = true;
                txtMqt.Text = dr["Mqt"].ToString();
                cmbJws.Text = dr["Jws"].ToString();
                if (Convert.ToString(dr["Jxz"]).Contains("1")) Jxz1.Checked = true;
                if (Convert.ToString(dr["Jxz"]).Contains("2")) Jxz2.Checked = true;
                if (Convert.ToString(dr["Jxz"]).Contains("3")) Jxz3.Checked = true;
                if (Convert.ToString(dr["Jxz"]).Contains("4")) Jxz4.Checked = true;
                if (Convert.ToString(dr["Jxz"]).Contains("5")) Jxz5.Checked = true;
                txtJqt.Text = dr["Jqt"].ToString();
                cmbTsyy.Text = dr["Tsyy"].ToString();
                if (Convert.ToString(dr["Txz"]).Contains("1")) Txz1.Checked = true;
                if (Convert.ToString(dr["Txz"]).Contains("2")) Txz2.Checked = true;
                if (Convert.ToString(dr["Txz"]).Contains("3")) Txz3.Checked = true;
                txtTqt.Text = dr["Tqt"].ToString();
                cmbJzhdqk.Text = dr["Jzhdqk"].ToString();
                cmbZkkn.Text = dr["Zkkn"].ToString();
                cmbMallampati.Text = dr["Mallampati"].ToString();
                cmbKnqd.Text = dr["Knqd"].ToString();
                txtYuanyin.Text = dr["Yuanyin"].ToString();
                cmbHxgnpg.Text = dr["Hxgnpg"].ToString();
                cmbNYHA.Text = dr["NYHA"].ToString();
                cmbxcg.Text = dr["xcg"].ToString();
                cmbGgn.Text = dr["Ggn"].ToString();
                cmbNxgn.Text = dr["Nxgn"].ToString();
                cmbxt.Text = dr["xt"].ToString();
                cmbDjz.Text = dr["Djz"].ToString();
                cmbXdt.Text = dr["Xdt"].ToString();
                if (Convert.ToString(dr["XlXZ"]).Contains("1")) XlXZ1.Checked = true;
                if (Convert.ToString(dr["XlXZ"]).Contains("2")) XlXZ2.Checked = true;
                if (Convert.ToString(dr["XlXZ"]).Contains("3")) XlXZ3.Checked = true;
                txtXlQT.Text = dr["XlQT"].ToString();
                txtTsjc.Text = dr["Tsjc"].ToString();
                cmbASA.Text = dr["ASA"].ToString();
                if (dr["E"].ToString() == "1")
                {
                    ckE.Checked = true;
                }
                txtMzqyy.Text = dr["Mzqyy"].ToString();
                if (Convert.ToString(dr["Yd"]).Contains("1")) Yd1.Checked = true;
                if (Convert.ToString(dr["Yd"]).Contains("2")) Yd2.Checked = true;
                if (Convert.ToString(dr["Yd"]).Contains("3")) Yd3.Checked = true;
                if (Convert.ToString(dr["Yd"]).Contains("4")) Yd4.Checked = true;
                txtYY.Text = dr["YY"].ToString();
                if (Convert.ToString(dr["Cg"]).Contains("a")) Cg1.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("b")) Cg2.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("c")) Cg3.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("d")) Cg4.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("e")) Cg5.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("f")) Cg6.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("g")) Cg7.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("h")) Cg8.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("y")) Cg9.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("j")) Cg10.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("k")) Cg11.Checked = true;
                if (Convert.ToString(dr["Cg"]).Contains("l")) Cg12.Checked = true;
                txtDgzb.Text = dr["Dgzb"].ToString();
                if (Convert.ToString(dr["Jmmzy"]).Contains("1")) Jmmzy1.Checked = true;
                if (Convert.ToString(dr["Jmmzy"]).Contains("2")) Jmmzy2.Checked = true;
                if (Convert.ToString(dr["Jmmzy"]).Contains("3")) Jmmzy3.Checked = true;

                if (Convert.ToString(dr["Xrmzy"]).Contains("1")) Xrmzy1.Checked = true;
                if (Convert.ToString(dr["Xrmzy"]).Contains("2")) Xrmzy2.Checked = true;
                if (Convert.ToString(dr["Xrmzy"]).Contains("3")) Xrmzy3.Checked = true;

                if (Convert.ToString(dr["zty"]).Contains("1")) zty1.Checked = true;
                if (Convert.ToString(dr["zty"]).Contains("2")) zty2.Checked = true;
                if (Convert.ToString(dr["zty"]).Contains("3")) zty3.Checked = true;
                if (Convert.ToString(dr["zty"]).Contains("4")) zty4.Checked = true;

                if (Convert.ToString(dr["Jsy"]).Contains("1")) Jsy1.Checked = true;
                if (Convert.ToString(dr["Jsy"]).Contains("2")) Jsy2.Checked = true;
                if (Convert.ToString(dr["Jsy"]).Contains("3")) Jsy3.Checked = true;

                if (Convert.ToString(dr["Zgn"]).Contains("1")) Zgn1.Checked = true;
                if (Convert.ToString(dr["Zgn"]).Contains("2")) Zgn2.Checked = true;
                if (Convert.ToString(dr["Zgn"]).Contains("3")) Zgn3.Checked = true;
                if (Convert.ToString(dr["Zgn"]).Contains("4")) Zgn4.Checked = true;
                cmbZgCC.Text = dr["ZgCC"].ToString();
                if (Convert.ToString(dr["Qtmz"]).Contains("1")) Qtmz1.Checked = true;
                if (Convert.ToString(dr["Qtmz"]).Contains("2")) Qtmz2.Checked = true;
                if (Convert.ToString(dr["Qtmz"]).Contains("3")) Qtmz3.Checked = true;
                if (Convert.ToString(dr["Qtmz"]).Contains("4")) Qtmz4.Checked = true;
                txtmzqt.Text = dr["mzqt"].ToString();
                if (Convert.ToString(dr["Nxjcx"]).Contains("a")) Nxjcx1.Checked = true;
                if (Convert.ToString(dr["Nxjcx"]).Contains("b")) Nxjcx2.Checked = true;
                if (Convert.ToString(dr["Nxjcx"]).Contains("c")) Nxjcx3.Checked = true;
                if (Convert.ToString(dr["Nxjcx"]).Contains("d")) Nxjcx4.Checked = true;
                if (Convert.ToString(dr["Nxjcx"]).Contains("e")) Nxjcx5.Checked = true;
                if (Convert.ToString(dr["Nxjcx"]).Contains("f")) Nxjcx6.Checked = true;
                if (Convert.ToString(dr["Nxjcx"]).Contains("g")) Nxjcx7.Checked = true;
                if (Convert.ToString(dr["Nxjcx"]).Contains("h")) Nxjcx8.Checked = true;
                if (Convert.ToString(dr["Nxjcx"]).Contains("y")) Nxjcx9.Checked = true;
                txtNxjcxQT.Text = dr["NxjcxQT"].ToString();
                txtSzwt.Text = dr["Szwt"].ToString();
                txtsjysjjwt.Text = dr["sjysjjwt"].ToString();
                txtKntlyj.Text = dr["Kntlyj"].ToString();
                if (dr["Mzys"].ToString() != "")
                {
                    cmbMzys.Text = dr["Mzys"].ToString();
                }
                dtVisitDate.Text = dr["VisitDate"].ToString();
                cmbMzfxpg.Text = dr["Mzfxpg"].ToString();
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
                UserRoleBll _UserRoleBll = new UserRoleBll();
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
            else
            {
                Gxz1.Enabled = false;
                Gxz2.Enabled = false;
                Gxz3.Enabled = false;
                txtGqt.Enabled = false;
                Mxz1.Enabled = false;
                Mxz2.Enabled = false;
                txtMqt.Enabled = false;
                Jxz1.Enabled = false;
                Jxz2.Enabled = false;
                Jxz3.Enabled = false;
                Jxz4.Enabled = false;
                Jxz5.Enabled = false;
                txtJqt.Enabled = false;
                Txz1.Enabled = false;
                Txz2.Enabled = false;
                Txz3.Enabled = false;
                txtTqt.Enabled = false;
                txtYuanyin.Enabled = false;
                //XlXZ1.Enabled = false;
                //XlXZ2.Enabled = false;
                //XlXZ3.Enabled = false;
                //txtXlQT.Enabled = false;
            }
            this.cmbTsyy.SelectedIndexChanged += new System.EventHandler(this.cmbTsyy_SelectedIndexChanged);
            this.cmbJws.SelectedIndexChanged += new System.EventHandler(this.cmbJws_SelectedIndexChanged);
            this.cmbMzs.SelectedIndexChanged += new System.EventHandler(this.cmbMzs_SelectedIndexChanged);
            this.cmbGms.SelectedIndexChanged += new System.EventHandler(this.cmbGms_SelectedIndexChanged);
            this.cmbKnqd.SelectedIndexChanged += new System.EventHandler(this.cmbKnqd_SelectedIndexChanged);
            this.cmbXdt.SelectedIndexChanged += new System.EventHandler(this.cmbXdt_SelectedIndexChanged);
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            try
            {
                DataTable dts = _PaibanDal.GetPaibanByPatId(PatId);
                if (dts.Rows.Count > 0)
                {
                    int res = _PaibanDal.UpdatePaiban(txtHeith.Text, txtWeith.Text, PatId);
                }
                Dictionary<string, string> SQFS = new Dictionary<string, string>();
                int result = 0;
                string AddItem = "";
                SQFS.Add("PatId", PatId);
                if (btnSave.Enabled)
                {
                    SQFS.Add("IsRead", "0");
                }
                else
                {
                    SQFS.Add("IsRead", "1");
                }

                SQFS.Add("Sqzd", tbSQZD.Text);
                SQFS.Add("Nsss", tbNDSS.Text);
                SQFS.Add("Gms", cmbGms.Text);
                AddItem = "";
                if (Gxz1.Checked) AddItem += "1";
                if (Gxz2.Checked) AddItem += "2";
                if (Gxz3.Checked) AddItem += "3";
                SQFS.Add("Gxz", AddItem);
                SQFS.Add("Gqt", txtGqt.Text);
                SQFS.Add("Mzs", cmbMzs.Text);
                AddItem = "";
                if (Mxz1.Checked) AddItem += "1";
                if (Mxz2.Checked) AddItem += "2";
                SQFS.Add("Mxz", AddItem);
                SQFS.Add("Mqt", txtMqt.Text);
                SQFS.Add("Jws", cmbJws.Text);
                AddItem = "";
                if (Jxz1.Checked) AddItem += "1";
                if (Jxz2.Checked) AddItem += "2";
                if (Jxz3.Checked) AddItem += "3";
                if (Jxz4.Checked) AddItem += "4";
                if (Jxz5.Checked) AddItem += "5";
                SQFS.Add("Jxz", AddItem);
                SQFS.Add("Jqt", txtJqt.Text);
                SQFS.Add("Tsyy", cmbTsyy.Text);
                AddItem = "";
                if (Txz1.Checked) AddItem += "1";
                if (Txz2.Checked) AddItem += "2";
                if (Txz3.Checked) AddItem += "3";
                SQFS.Add("Txz", AddItem);
                SQFS.Add("Tqt", txtTqt.Text);
                SQFS.Add("Jzhdqk", cmbJzhdqk.Text);
                SQFS.Add("Zkkn", cmbZkkn.Text);
                SQFS.Add("Mallampati", cmbMallampati.Text);
                SQFS.Add("Knqd", cmbKnqd.Text);
                SQFS.Add("Yuanyin", txtYuanyin.Text);
                SQFS.Add("Hxgnpg", cmbHxgnpg.Text);
                SQFS.Add("NYHA", cmbNYHA.Text);
                SQFS.Add("xcg", cmbxcg.Text);
                SQFS.Add("Ggn", cmbGgn.Text);
                SQFS.Add("Nxgn", cmbNxgn.Text);
                SQFS.Add("xt", cmbxt.Text);
                SQFS.Add("Djz", cmbDjz.Text);
                SQFS.Add("Xdt", cmbXdt.Text);
                AddItem = "";
                if (XlXZ1.Checked) AddItem += "1";
                if (XlXZ2.Checked) AddItem += "2";
                if (XlXZ3.Checked) AddItem += "3";
                SQFS.Add("XlXZ", AddItem);
                SQFS.Add("XlQT", txtXlQT.Text);
                SQFS.Add("Tsjc", txtTsjc.Text);
                SQFS.Add("ASA", cmbASA.Text);
                if (ckE.Checked)
                {
                    SQFS.Add("E", "1");
                }
                else
                {
                    SQFS.Add("E", "0");
                }
                SQFS.Add("Mzqyy", txtMzqyy.Text);
                AddItem = "";
                if (Yd1.Checked) AddItem += "1";
                if (Yd2.Checked) AddItem += "2";
                if (Yd3.Checked) AddItem += "3";
                if (Yd4.Checked) AddItem += "4";
                SQFS.Add("Yd", AddItem);
                SQFS.Add("YY", txtYY.Text);
                AddItem = "";
                if (Cg1.Checked) AddItem += "a";
                if (Cg2.Checked) AddItem += "b";
                if (Cg3.Checked) AddItem += "c";
                if (Cg4.Checked) AddItem += "d";
                if (Cg5.Checked) AddItem += "e";
                if (Cg6.Checked) AddItem += "f";
                if (Cg7.Checked) AddItem += "g";
                if (Cg8.Checked) AddItem += "h";
                if (Cg9.Checked) AddItem += "y";
                if (Cg10.Checked) AddItem += "j";
                if (Cg11.Checked) AddItem += "k";
                if (Cg12.Checked) AddItem += "l";
                SQFS.Add("Cg", AddItem);
                SQFS.Add("Dgzb", txtDgzb.Text);
                AddItem = "";
                if (Jmmzy1.Checked) AddItem += "1";
                if (Jmmzy2.Checked) AddItem += "2";
                if (Jmmzy3.Checked) AddItem += "3";
                SQFS.Add("Jmmzy", AddItem);
                AddItem = "";
                if (Xrmzy1.Checked) AddItem += "1";
                if (Xrmzy2.Checked) AddItem += "2";
                if (Xrmzy3.Checked) AddItem += "3";
                SQFS.Add("Xrmzy", AddItem);
                AddItem = "";
                if (zty1.Checked) AddItem += "1";
                if (zty2.Checked) AddItem += "2";
                if (zty3.Checked) AddItem += "3";
                if (zty4.Checked) AddItem += "4";
                SQFS.Add("zty", AddItem);
                AddItem = "";
                if (Jsy1.Checked) AddItem += "1";
                if (Jsy2.Checked) AddItem += "2";
                if (Jsy3.Checked) AddItem += "3";
                SQFS.Add("Jsy", AddItem);
                AddItem = "";
                if (Zgn1.Checked) AddItem += "1";
                if (Zgn2.Checked) AddItem += "2";
                if (Zgn3.Checked) AddItem += "3";
                if (Zgn4.Checked) AddItem += "4";
                SQFS.Add("Zgn", AddItem);
                SQFS.Add("ZgCC", cmbZgCC.Text);
                AddItem = "";
                if (Qtmz1.Checked) AddItem += "1";
                if (Qtmz2.Checked) AddItem += "2";
                if (Qtmz3.Checked) AddItem += "3";
                if (Qtmz4.Checked) AddItem += "4";
                SQFS.Add("Qtmz", AddItem);
                SQFS.Add("mzqt", txtmzqt.Text);
                AddItem = "";
                if (Nxjcx1.Checked) AddItem += "a";
                if (Nxjcx2.Checked) AddItem += "b";
                if (Nxjcx3.Checked) AddItem += "c";
                if (Nxjcx4.Checked) AddItem += "d";
                if (Nxjcx5.Checked) AddItem += "e";
                if (Nxjcx6.Checked) AddItem += "f";
                if (Nxjcx7.Checked) AddItem += "g";
                if (Nxjcx8.Checked) AddItem += "h";
                if (Nxjcx9.Checked) AddItem += "y";
                SQFS.Add("Nxjcx", AddItem);
                SQFS.Add("NxjcxQT", txtNxjcxQT.Text);
                SQFS.Add("Szwt", txtSzwt.Text);
                SQFS.Add("sjysjjwt", txtsjysjjwt.Text);
                SQFS.Add("Kntlyj", txtKntlyj.Text);
                SQFS.Add("Mzys", cmbMzys.Text);
                SQFS.Add("VisitDate", Convert.ToDateTime(dtVisitDate.Value.ToString()).ToString("yyyy-MM-dd"));
                SQFS.Add("Odate", Convert.ToDateTime(Odate).ToString("yyyy-MM-dd"));
                SQFS.Add("Mzfxpg", cmbMzfxpg.Text);
                result = _BeforeVistDal.UpdateBeforeVist_YS(SQFS);
                if (result > 0)
                {
                    SaveCount++; //MessageBox.Show("保存成功！");
                }
                else MessageBox.Show("保存失败！");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void btnDY_Click(object sender, EventArgs e)
        {
            if (SaveCount == 0)
            {
                Save();
            }
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
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("A4", 820, 1160); 
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("新宋体", 8);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            int y = 40; int x = 80; int y1 = 0;
            string title1 = "天津红桥医院麻醉术前访视及计划";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 160, y));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("性别：" + tbPatsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 80, y));
            e.Graphics.DrawString("年龄：" + this.tbPatage.Text.Trim() + "岁", ptzt, Brushes.Black, new Point(x + 130, y));
            e.Graphics.DrawString("科别：" + this.tbKeshi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
            e.Graphics.DrawString("床号：" + this.txtch.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
            e.Graphics.DrawString("身高：" + this.txtHeith.Text.Trim() + "cm", ptzt, Brushes.Black, new Point(x + 380, y));
            e.Graphics.DrawString("体重：" + this.txtWeith.Text.Trim() + "kg", ptzt, Brushes.Black, new Point(x + 450, y));
            e.Graphics.DrawString("住院号：" + this.tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 520, y));
            y = y + 20;
            string str1_zd = "";
            int StrLength_zd = tbSQZD.Text.Trim().Length;
            int row_zd = StrLength_zd / 50;
            e.Graphics.DrawString("术前诊断：", ptzt, Brushes.Black, x + 10, y);
            for (int i = 0; i <= row_zd;)//50个字符就换行
            {
                if (i < row_zd)
                    str1_zd = tbSQZD.Text.ToString().Substring(i * 50, 50); //从第i*50个开始，截取50个字符串
                else
                    str1_zd = tbSQZD.Text.ToString().Substring(i * 50);
                e.Graphics.DrawString(str1_zd, ptzt, Brushes.Black, x + 60, y);
                i++;
                if (i > row_zd)
                {

                }
                else
                {
                    y = y + 15;
                }

            }
            //e.Graphics.DrawString("术前诊断：" + this.tbSQZD.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("拟实手术：" + this.tbNDSS.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("病史：", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("过敏史：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 60, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 10, 10);
            if (cmbGms.Text == "无")
            {
                e.Graphics.DrawLine(pblue2, x + 80, y, x + 85, y + 10);
                e.Graphics.DrawLine(pblue2, x + 85, y + 10, x + 90, y);
            }
            e.Graphics.DrawString("有", ptzt, Brushes.Black, x + 100, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 10, 10);
            if (cmbGms.Text == "有")
            {
                e.Graphics.DrawLine(pblue2, x + 120, y, x + 125, y + 10);
                e.Graphics.DrawLine(pblue2, x + 125, y + 10, x + 130, y);
            }
            e.Graphics.DrawString("（ ", ptzt, Brushes.Black, x + 150, y);
            e.Graphics.DrawString("皮疹", ptzt, Brushes.Black, x + 170, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 10, 10);
            if (Gxz1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 200, y, x + 205, y + 10);
                e.Graphics.DrawLine(pblue2, x + 205, y + 10, x + 210, y);
            }
            e.Graphics.DrawString("呼吸困难", ptzt, Brushes.Black, x + 220, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 10, 10);
            if (Gxz2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 270, y, x + 275, y + 10);
                e.Graphics.DrawLine(pblue2, x + 275, y + 10, x + 280, y);
            }
            e.Graphics.DrawString("血压降低", ptzt, Brushes.Black, x + 290, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 340, y, 10, 10);
            if (Gxz2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 340, y, x + 345, y + 10);
                e.Graphics.DrawLine(pblue2, x + 345, y + 10, x + 350, y);
            }
            e.Graphics.DrawString("其它:" + this.txtGqt.Text, ptzt, Brushes.Black, x + 370, y);
            e.Graphics.DrawString(") ", ptzt, Brushes.Black, x + 600, y);
            y = y + 20;
            e.Graphics.DrawString("麻醉史：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 60, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 10, 10);
            if (cmbMzs.Text == "无")
            {
                e.Graphics.DrawLine(pblue2, x + 80, y, x + 85, y + 10);
                e.Graphics.DrawLine(pblue2, x + 85, y + 10, x + 90, y);
            }
            e.Graphics.DrawString("有", ptzt, Brushes.Black, x + 100, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 10, 10);
            if (cmbMzs.Text == "有")
            {
                e.Graphics.DrawLine(pblue2, x + 120, y, x + 125, y + 10);
                e.Graphics.DrawLine(pblue2, x + 125, y + 10, x + 130, y);
            }
            e.Graphics.DrawString("（ ", ptzt, Brushes.Black, x + 150, y);
            e.Graphics.DrawString("全身麻醉", ptzt, Brushes.Black, x + 170, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 10, 10);
            if (Mxz1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 220, y, x + 225, y + 10);
                e.Graphics.DrawLine(pblue2, x + 225, y + 10, x + 230, y);
            }
            e.Graphics.DrawString("椎管内麻醉", ptzt, Brushes.Black, x + 240, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (Mxz2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("其它:" + this.txtMqt.Text, ptzt, Brushes.Black, x + 330, y);
            e.Graphics.DrawString(") ", ptzt, Brushes.Black, x + 600, y);
            y = y + 20;
            e.Graphics.DrawString("既往史：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 60, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 10, 10);
            if (cmbJws.Text == "无")
            {
                e.Graphics.DrawLine(pblue2, x + 80, y, x + 85, y + 10);
                e.Graphics.DrawLine(pblue2, x + 85, y + 10, x + 90, y);
            }
            e.Graphics.DrawString("有", ptzt, Brushes.Black, x + 100, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 10, 10);
            if (cmbJws.Text == "有")
            {
                e.Graphics.DrawLine(pblue2, x + 120, y, x + 125, y + 10);
                e.Graphics.DrawLine(pblue2, x + 125, y + 10, x + 130, y);
            }
            e.Graphics.DrawString("（ ", ptzt, Brushes.Black, x + 150, y);
            e.Graphics.DrawString("冠心病", ptzt, Brushes.Black, x + 170, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
            if (Jxz1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 210, y, x + 215, y + 10);
                e.Graphics.DrawLine(pblue2, x + 215, y + 10, x + 220, y);
            }
            e.Graphics.DrawString("高血压", ptzt, Brushes.Black, x + 230, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 10, 10);
            if (Jxz2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 270, y, x + 275, y + 10);
                e.Graphics.DrawLine(pblue2, x + 275, y + 10, x + 280, y);
            }
            e.Graphics.DrawString("糖尿病", ptzt, Brushes.Black, x + 290, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 10, 10);
            if (Jxz3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 330, y, x + 335, y + 10);
                e.Graphics.DrawLine(pblue2, x + 335, y + 10, x + 340, y);
            }
            e.Graphics.DrawString("肺部疾病", ptzt, Brushes.Black, x + 350, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 400, y, 10, 10);
            if (Jxz4.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 400, y, x + 405, y + 10);
                e.Graphics.DrawLine(pblue2, x + 405, y + 10, x + 410, y);
            }
            e.Graphics.DrawString("脑血管病", ptzt, Brushes.Black, x + 420, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 470, y, 10, 10);
            if (Jxz5.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 470, y, x + 475, y + 10);
                e.Graphics.DrawLine(pblue2, x + 475, y + 10, x + 480, y);
            }
            e.Graphics.DrawString("其它:" + this.txtJqt.Text, ptzt, Brushes.Black, x + 500, y);
            e.Graphics.DrawString(") ", ptzt, Brushes.Black, x + 600, y);
            y = y + 20;
            e.Graphics.DrawString("特殊用药：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 70, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            if (cmbTsyy.Text == "无")
            {
                e.Graphics.DrawLine(pblue2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pblue2, x + 95, y + 10, x + 100, y);
            }
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 110, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 130, y, 10, 10);
            if (cmbTsyy.Text == "有")
            {
                e.Graphics.DrawLine(pblue2, x + 130, y, x + 135, y + 10);
                e.Graphics.DrawLine(pblue2, x + 135, y + 10, x + 140, y);
            }
            e.Graphics.DrawString("（ ", ptzt, Brushes.Black, x + 160, y);
            e.Graphics.DrawString("激素", ptzt, Brushes.Black, x + 180, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
            if (Txz1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 210, y, x + 215, y + 10);
                e.Graphics.DrawLine(pblue2, x + 215, y + 10, x + 220, y);
            }
            e.Graphics.DrawString("抗凝药", ptzt, Brushes.Black, x + 230, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 10, 10);
            if (Txz2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 270, y, x + 275, y + 10);
                e.Graphics.DrawLine(pblue2, x + 275, y + 10, x + 280, y);
            }
            e.Graphics.DrawString("精神类药物", ptzt, Brushes.Black, x + 290, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 10, 10);
            if (Txz3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 350, y, x + 355, y + 10);
                e.Graphics.DrawLine(pblue2, x + 355, y + 10, x + 360, y);
            }
            e.Graphics.DrawString("其它:" + this.txtTqt.Text, ptzt, Brushes.Black, x + 380, y);
            e.Graphics.DrawString(") ", ptzt, Brushes.Black, x + 600, y);
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("体格检查:", ptzt, Brushes.Black, x + 10, y);
            y = y + 20;
            e.Graphics.DrawString("气道评估：颈椎活动情况:", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x + 150, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 180, y, 10, 10);
            if (cmbJzhdqk.Text == "正常")
            {
                e.Graphics.DrawLine(pblue2, x + 180, y, x + 185, y + 10);
                e.Graphics.DrawLine(pblue2, x + 185, y + 10, x + 190, y);
            }
            e.Graphics.DrawString("异常 ", ptzt, Brushes.Black, x + 200, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 230, y, 10, 10);
            if (cmbJzhdqk.Text == "异常")
            {
                e.Graphics.DrawLine(pblue2, x + 230, y, x + 235, y + 10);
                e.Graphics.DrawLine(pblue2, x + 235, y + 10, x + 240, y);
            }
            e.Graphics.DrawString("张口困难:", ptzt, Brushes.Black, x + 250, y);
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 310, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 330, y, 10, 10);
            if (cmbZkkn.Text == "无")
            {
                e.Graphics.DrawLine(pblue2, x + 330, y, x + 335, y + 10);
                e.Graphics.DrawLine(pblue2, x + 335, y + 10, x + 340, y);
            }
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 350, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 10, 10);
            if (cmbZkkn.Text == "有")
            {
                e.Graphics.DrawLine(pblue2, x + 370, y, x + 375, y + 10);
                e.Graphics.DrawLine(pblue2, x + 375, y + 10, x + 380, y);
            }
            e.Graphics.DrawString("Mallampati分级:", ptzt, Brushes.Black, x + 390, y);
            e.Graphics.DrawString("Ⅰ ", ptzt, Brushes.Black, x + 480, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 500, y, 10, 10);
            if (cmbMallampati.Text == "Ⅰ")
            {
                e.Graphics.DrawLine(pblue2, x + 500, y, x + 505, y + 10);
                e.Graphics.DrawLine(pblue2, x + 505, y + 10, x + 510, y);
            }
            e.Graphics.DrawString("Ⅱ ", ptzt, Brushes.Black, x + 520, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 540, y, 10, 10);
            if (cmbMallampati.Text == "Ⅱ")
            {
                e.Graphics.DrawLine(pblue2, x + 540, y, x + 545, y + 10);
                e.Graphics.DrawLine(pblue2, x + 545, y + 10, x + 550, y);
            }
            e.Graphics.DrawString("Ⅲ ", ptzt, Brushes.Black, x + 560, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 580, y, 10, 10);
            if (cmbMallampati.Text == "Ⅲ")
            {
                e.Graphics.DrawLine(pblue2, x + 580, y, x + 585, y + 10);
                e.Graphics.DrawLine(pblue2, x + 585, y + 10, x + 590, y);
            }
            e.Graphics.DrawString("Ⅳ ", ptzt, Brushes.Black, x + 600, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 620, y, 10, 10);
            if (cmbMallampati.Text == "Ⅳ")
            {
                e.Graphics.DrawLine(pblue2, x + 620, y, x + 625, y + 10);
                e.Graphics.DrawLine(pblue2, x + 625, y + 10, x + 630, y);
            }
            y = y + 20;
            e.Graphics.DrawString("困难气道：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("无 ", ptzt, Brushes.Black, x + 70, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            if (cmbKnqd.Text == "无")
            {
                e.Graphics.DrawLine(pblue2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pblue2, x + 95, y + 10, x + 100, y);
            }
            e.Graphics.DrawString("有 ", ptzt, Brushes.Black, x + 110, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 130, y, 10, 10);
            if (cmbKnqd.Text == "有")
            {
                e.Graphics.DrawLine(pblue2, x + 130, y, x + 135, y + 10);
                e.Graphics.DrawLine(pblue2, x + 135, y + 10, x + 140, y);
            }
            e.Graphics.DrawString("可疑 ", ptzt, Brushes.Black, x + 150, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 180, y, 10, 10);
            if (cmbKnqd.Text == "可疑")
            {
                e.Graphics.DrawLine(pblue2, x + 180, y, x + 185, y + 10);
                e.Graphics.DrawLine(pblue2, x + 185, y + 10, x + 190, y);
            }
            e.Graphics.DrawString("（ ", ptzt, Brushes.Black, x + 210, y);
            e.Graphics.DrawString("原因：" + txtYuanyin.Text, ptzt, Brushes.Black, x + 230, y);
            e.Graphics.DrawString(") ", ptzt, Brushes.Black, x + 500, y);
            y = y + 20;
            e.Graphics.DrawString("呼吸功能评估：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("正常 ", ptzt, Brushes.Black, x + 90, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 10, 10);
            if (cmbHxgnpg.Text == "正常")
            {
                e.Graphics.DrawLine(pblue2, x + 120, y, x + 125, y + 10);
                e.Graphics.DrawLine(pblue2, x + 125, y + 10, x + 130, y);
            }
            e.Graphics.DrawString("轻度异常", ptzt, Brushes.Black, x + 140, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 190, y, 10, 10);
            if (cmbHxgnpg.Text == "轻度异常")
            {
                e.Graphics.DrawLine(pblue2, x + 190, y, x + 195, y + 10);
                e.Graphics.DrawLine(pblue2, x + 195, y + 10, x + 200, y);
            }
            e.Graphics.DrawString("中度异常", ptzt, Brushes.Black, x + 210, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 260, y, 10, 10);
            if (cmbHxgnpg.Text == "中度异常")
            {
                e.Graphics.DrawLine(pblue2, x + 260, y, x + 265, y + 10);
                e.Graphics.DrawLine(pblue2, x + 265, y + 10, x + 270, y);
            }
            e.Graphics.DrawString("重度异常", ptzt, Brushes.Black, x + 280, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 340, y, 10, 10);
            if (cmbHxgnpg.Text == "重度异常")
            {
                e.Graphics.DrawLine(pblue2, x + 340, y, x + 345, y + 10);
                e.Graphics.DrawLine(pblue2, x + 345, y + 10, x + 350, y);
            }
            y = y + 20;
            e.Graphics.DrawString("心功能分级（NYHA）：", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("Ⅰ", ptzt, Brushes.Black, x + 120, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 140, y, 10, 10);
            if (cmbNYHA.Text == "Ⅰ")
            {
                e.Graphics.DrawLine(pblue2, x + 140, y, x + 145, y + 10);
                e.Graphics.DrawLine(pblue2, x + 145, y + 10, x + 150, y);
            }
            e.Graphics.DrawString("Ⅱ", ptzt, Brushes.Black, x + 160, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 180, y, 10, 10);
            if (cmbNYHA.Text == "Ⅱ")
            {
                e.Graphics.DrawLine(pblue2, x + 180, y, x + 185, y + 10);
                e.Graphics.DrawLine(pblue2, x + 185, y + 10, x + 190, y);
            }
            e.Graphics.DrawString("Ⅲ", ptzt, Brushes.Black, x + 200, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 10, 10);
            if (cmbNYHA.Text == "Ⅲ")
            {
                e.Graphics.DrawLine(pblue2, x + 220, y, x + 225, y + 10);
                e.Graphics.DrawLine(pblue2, x + 225, y + 10, x + 230, y);
            }
            e.Graphics.DrawString("Ⅳ", ptzt, Brushes.Black, x + 240, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 260, y, 10, 10);
            if (cmbNYHA.Text == "Ⅳ")
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("实验室检查:", ptzt, Brushes.Black, x + 10, y);
            y = y + 20;
            e.Graphics.DrawString("血常规:", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("正常", ptzt, Brushes.Black, x + 60, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            if (cmbxcg.Text == "正常")
            {
                e.Graphics.DrawLine(pblue2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pblue2, x + 95, y + 10, x + 100, y);
            }
            e.Graphics.DrawString("基本正常", ptzt, Brushes.Black, x + 110, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 10, 10);
            if (cmbxcg.Text == "基本正常")
            {
                e.Graphics.DrawLine(pblue2, x + 160, y, x + 165, y + 10);
                e.Graphics.DrawLine(pblue2, x + 165, y + 10, x + 170, y);
            }
            e.Graphics.DrawString("异常", ptzt, Brushes.Black, x + 180, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
            if (cmbxcg.Text == "异常")
            {
                e.Graphics.DrawLine(pblue2, x + 210, y, x + 215, y + 10);
                e.Graphics.DrawLine(pblue2, x + 215, y + 10, x + 220, y);
            }
            e.Graphics.DrawString("凝血功能：", ptzt, Brushes.Black, x + 240, y);
            e.Graphics.DrawString("正常", ptzt, Brushes.Black, x + 290, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 10, 10);
            if (cmbNxgn.Text == "正常")
            {
                e.Graphics.DrawLine(pblue2, x + 320, y, x + 325, y + 10);
                e.Graphics.DrawLine(pblue2, x + 325, y + 10, x + 330, y);
            }
            e.Graphics.DrawString("基本正常", ptzt, Brushes.Black, x + 340, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 10, 10);
            if (cmbNxgn.Text == "基本正常")
            {
                e.Graphics.DrawLine(pblue2, x + 390, y, x + 395, y + 10);
                e.Graphics.DrawLine(pblue2, x + 395, y + 10, x + 400, y);
            }
            e.Graphics.DrawString("异常", ptzt, Brushes.Black, x + 410, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 440, y, 10, 10);
            if (cmbNxgn.Text == "异常")
            {
                e.Graphics.DrawLine(pblue2, x + 440, y, x + 445, y + 10);
                e.Graphics.DrawLine(pblue2, x + 445, y + 10, x + 450, y);
            }
            e.Graphics.DrawString("血糖：", ptzt, Brushes.Black, x + 470, y);
            e.Graphics.DrawString("正常", ptzt, Brushes.Black, x + 510, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 540, y, 10, 10);
            if (cmbxt.Text == "正常")
            {
                e.Graphics.DrawLine(pblue2, x + 540, y, x + 545, y + 10);
                e.Graphics.DrawLine(pblue2, x + 545, y + 10, x + 550, y);
            }
            e.Graphics.DrawString("异常", ptzt, Brushes.Black, x + 560, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 590, y, 10, 10);
            if (cmbxt.Text == "异常")
            {
                e.Graphics.DrawLine(pblue2, x + 590, y, x + 595, y + 10);
                e.Graphics.DrawLine(pblue2, x + 595, y + 10, x + 600, y);
            }
            y = y + 20;
            e.Graphics.DrawString("肝功能:", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("正常", ptzt, Brushes.Black, x + 60, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            if (cmbGgn.Text == "正常")
            {
                e.Graphics.DrawLine(pblue2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pblue2, x + 95, y + 10, x + 100, y);
            }
            e.Graphics.DrawString("基本正常", ptzt, Brushes.Black, x + 110, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 10, 10);
            if (cmbGgn.Text == "基本正常")
            {
                e.Graphics.DrawLine(pblue2, x + 160, y, x + 165, y + 10);
                e.Graphics.DrawLine(pblue2, x + 165, y + 10, x + 170, y);
            }
            e.Graphics.DrawString("异常", ptzt, Brushes.Black, x + 180, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
            if (cmbGgn.Text == "异常")
            {
                e.Graphics.DrawLine(pblue2, x + 210, y, x + 215, y + 10);
                e.Graphics.DrawLine(pblue2, x + 215, y + 10, x + 220, y);
            }

            e.Graphics.DrawString("电解质：", ptzt, Brushes.Black, x + 250, y);
            e.Graphics.DrawString("正常", ptzt, Brushes.Black, x + 290, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 320, y, 10, 10);
            if (cmbDjz.Text == "正常")
            {
                e.Graphics.DrawLine(pblue2, x + 320, y, x + 325, y + 10);
                e.Graphics.DrawLine(pblue2, x + 325, y + 10, x + 330, y);
            }
            e.Graphics.DrawString("基本正常", ptzt, Brushes.Black, x + 340, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 10, 10);
            if (cmbDjz.Text == "基本正常")
            {
                e.Graphics.DrawLine(pblue2, x + 390, y, x + 395, y + 10);
                e.Graphics.DrawLine(pblue2, x + 395, y + 10, x + 400, y);
            }
            e.Graphics.DrawString("异常", ptzt, Brushes.Black, x + 410, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 440, y, 10, 10);
            if (cmbDjz.Text == "异常")
            {
                e.Graphics.DrawLine(pblue2, x + 440, y, x + 445, y + 10);
                e.Graphics.DrawLine(pblue2, x + 445, y + 10, x + 450, y);
            }
            y = y + 20;
            e.Graphics.DrawString("心电图:", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("正常", ptzt, Brushes.Black, x + 60, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            if (cmbXdt.Text == "正常")
            {
                e.Graphics.DrawLine(pblue2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pblue2, x + 95, y + 10, x + 100, y);
            }
            e.Graphics.DrawString("心肌缺血", ptzt, Brushes.Black, x + 110, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 10, 10);
            if (cmbXdt.Text == "心肌缺血")
            {
                e.Graphics.DrawLine(pblue2, x + 160, y, x + 165, y + 10);
                e.Graphics.DrawLine(pblue2, x + 165, y + 10, x + 170, y);
            }
            e.Graphics.DrawString("心梗", ptzt, Brushes.Black, x + 180, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
            if (cmbXdt.Text == "心梗")
            {
                e.Graphics.DrawLine(pblue2, x + 210, y, x + 215, y + 10);
                e.Graphics.DrawLine(pblue2, x + 215, y + 10, x + 220, y);
            }
            e.Graphics.DrawString("心律失常", ptzt, Brushes.Black, x + 230, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 10, 10);
            if (cmbXdt.Text == "心律失常")
            {
                e.Graphics.DrawLine(pblue2, x + 280, y, x + 285, y + 10);
                e.Graphics.DrawLine(pblue2, x + 285, y + 10, x + 290, y);
            }
            e.Graphics.DrawString("（ ", ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawString("室性心律失常", ptzt, Brushes.Black, x + 320, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 10, 10);
            if (XlXZ1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 390, y, x + 395, y + 10);
                e.Graphics.DrawLine(pblue2, x + 395, y + 10, x + 400, y);
            }
            e.Graphics.DrawString("房颤", ptzt, Brushes.Black, x + 410, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 440, y, 10, 10);
            if (XlXZ2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 440, y, x + 445, y + 10);
                e.Graphics.DrawLine(pblue2, x + 445, y + 10, x + 450, y);
            }
            e.Graphics.DrawString("传导阻滞", ptzt, Brushes.Black, x + 460, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 510, y, 10, 10);
            if (XlXZ3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 510, y, x + 515, y + 10);
                e.Graphics.DrawLine(pblue2, x + 515, y + 10, x + 520, y);
            }
            e.Graphics.DrawString("其它 " + this.txtXlQT.Text, ptzt, Brushes.Black, x + 530, y);
            e.Graphics.DrawString(")", ptzt, Brushes.Black, x + 620, y);
            y = y + 20;
            e.Graphics.DrawString("特殊检查：" + txtTsjc.Text, ptzt, Brushes.Black, x + 10, y);
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("ASA分级:", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("Ⅰ", ptzt, Brushes.Black, x + 70, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            if (cmbASA.Text == "Ⅰ")
            {
                e.Graphics.DrawLine(pblue2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pblue2, x + 95, y + 10, x + 100, y);
            }
            e.Graphics.DrawString("Ⅱ", ptzt, Brushes.Black, x + 110, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 130, y, 10, 10);
            if (cmbASA.Text == "Ⅱ")
            {
                e.Graphics.DrawLine(pblue2, x + 130, y, x + 135, y + 10);
                e.Graphics.DrawLine(pblue2, x + 135, y + 10, x + 140, y);
            }
            e.Graphics.DrawString("Ⅲ", ptzt, Brushes.Black, x + 150, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 10, 10);
            if (cmbASA.Text == "Ⅲ")
            {
                e.Graphics.DrawLine(pblue2, x + 170, y, x + 175, y + 10);
                e.Graphics.DrawLine(pblue2, x + 175, y + 10, x + 180, y);
            }
            e.Graphics.DrawString("Ⅳ", ptzt, Brushes.Black, x + 190, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
            if (cmbASA.Text == "Ⅳ")
            {
                e.Graphics.DrawLine(pblue2, x + 210, y, x + 215, y + 10);
                e.Graphics.DrawLine(pblue2, x + 215, y + 10, x + 220, y);
            }
            e.Graphics.DrawString("Ⅴ", ptzt, Brushes.Black, x + 230, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 250, y, 10, 10);
            if (cmbASA.Text == "Ⅴ")
            {
                e.Graphics.DrawLine(pblue2, x + 250, y, x + 255, y + 10);
                e.Graphics.DrawLine(pblue2, x + 255, y + 10, x + 260, y);
            }
            e.Graphics.DrawString("E", ptzt, Brushes.Black, x + 270, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 290, y, 10, 10);
            if (ckE.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 290, y, x + 295, y + 10);
                e.Graphics.DrawLine(pblue2, x + 295, y + 10, x + 300, y);
            }
            e.Graphics.DrawString("麻醉风险评估：", ptzt, Brushes.Black, x + 320, y);
            e.Graphics.DrawString("低风险", ptzt, Brushes.Black, x + 400, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 440, y, 10, 10);
            if (cmbMzfxpg.Text == "低风险")
            {
                e.Graphics.DrawLine(pblue2, x + 440, y, x + 445, y + 10);
                e.Graphics.DrawLine(pblue2, x + 445, y + 10, x + 450, y);
            }
            e.Graphics.DrawString("中风险", ptzt, Brushes.Black, x + 460, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 500, y, 10, 10);
            if (cmbMzfxpg.Text == "中风险")
            {
                e.Graphics.DrawLine(pblue2, x + 500, y, x + 505, y + 10);
                e.Graphics.DrawLine(pblue2, x + 505, y + 10, x + 510, y);
            }
            e.Graphics.DrawString("高风险", ptzt, Brushes.Black, x + 520, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 560, y, 10, 10);
            if (cmbMzfxpg.Text == "高风险")
            {
                e.Graphics.DrawLine(pblue2, x + 560, y, x + 565, y + 10);
                e.Graphics.DrawLine(pblue2, x + 565, y + 10, x + 570, y);
            }
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("麻醉前用药：" + txtMzqyy.Text, ptzt, Brushes.Black, x + 10, y);
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("全麻", ptzt, Brushes.Black, x + 10, y);
            y = y + 20;
            e.Graphics.DrawString("诱导：", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("快诱导", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 90, y, 10, 10);
            if (Yd1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 90, y, x + 95, y + 10);
                e.Graphics.DrawLine(pblue2, x + 95, y + 10, x + 100, y);
            }
            e.Graphics.DrawString("慢诱导", ptzt, Brushes.Black, x + 110, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (Yd2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("吸入诱导", ptzt, Brushes.Black, x + 170, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 10, 10);
            if (Yd3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 220, y, x + 225, y + 10);
                e.Graphics.DrawLine(pblue2, x + 225, y + 10, x + 230, y);
            }
            e.Graphics.DrawString("清醒插管", ptzt, Brushes.Black, x + 240, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 290, y, 10, 10);
            if (Yd4.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 290, y, x + 295, y + 10);
                e.Graphics.DrawLine(pblue2, x + 295, y + 10, x + 300, y);
            }
            e.Graphics.DrawString("用药：" + txtYY.Text, ptzt, Brushes.Black, x + 310, y);
            y = y + 20;
            e.Graphics.DrawString("插管：", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("经口", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 10, 10);
            if (Cg1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 80, y, x + 85, y + 10);
                e.Graphics.DrawLine(pblue2, x + 85, y + 10, x + 90, y);
            }
            e.Graphics.DrawString("经鼻", ptzt, Brushes.Black, x + 100, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 130, y, 10, 10);
            if (Cg2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 130, y, x + 135, y + 10);
                e.Graphics.DrawLine(pblue2, x + 135, y + 10, x + 140, y);
            }
            e.Graphics.DrawString("左", ptzt, Brushes.Black, x + 150, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 10, 10);
            if (Cg3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 170, y, x + 175, y + 10);
                e.Graphics.DrawLine(pblue2, x + 175, y + 10, x + 180, y);
            }
            e.Graphics.DrawString("右", ptzt, Brushes.Black, x + 190, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
            if (Cg4.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 210, y, x + 215, y + 10);
                e.Graphics.DrawLine(pblue2, x + 215, y + 10, x + 220, y);
            }
            e.Graphics.DrawString("盲插", ptzt, Brushes.Black, x + 230, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 260, y, 10, 10);
            if (Cg5.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 260, y, x + 265, y + 10);
                e.Graphics.DrawLine(pblue2, x + 265, y + 10, x + 270, y);
            }
            e.Graphics.DrawString("双腔", ptzt, Brushes.Black, x + 280, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 310, y, 10, 10);
            if (Cg6.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 310, y, x + 315, y + 10);
                e.Graphics.DrawLine(pblue2, x + 315, y + 10, x + 320, y);
            }
            e.Graphics.DrawString("左", ptzt, Brushes.Black, x + 330, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 10, 10);
            if (Cg7.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 350, y, x + 355, y + 10);
                e.Graphics.DrawLine(pblue2, x + 355, y + 10, x + 360, y);
            }
            e.Graphics.DrawString("右", ptzt, Brushes.Black, x + 370, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 10, 10);
            if (Cg8.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 390, y, x + 395, y + 10);
                e.Graphics.DrawLine(pblue2, x + 395, y + 10, x + 400, y);
            }
            e.Graphics.DrawString("喉罩", ptzt, Brushes.Black, x + 410, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 440, y, 10, 10);
            if (Cg9.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 440, y, x + 445, y + 10);
                e.Graphics.DrawLine(pblue2, x + 445, y + 10, x + 450, y);
            }
            e.Graphics.DrawString("咽喉导管", ptzt, Brushes.Black, x + 460, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 510, y, 10, 10);
            if (Cg10.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 510, y, x + 515, y + 10);
                e.Graphics.DrawLine(pblue2, x + 515, y + 10, x + 520, y);
            }
            e.Graphics.DrawString("气管切开", ptzt, Brushes.Black, x + 530, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 580, y, 10, 10);
            if (Cg11.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 580, y, x + 585, y + 10);
                e.Graphics.DrawLine(pblue2, x + 585, y + 10, x + 590, y);
            }

            y = y + 20;
            e.Graphics.DrawString("纤维支气管镜协助", ptzt, Brushes.Black, x + 50, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (Cg12.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("导管准备：" + txtDgzb.Text, ptzt, Brushes.Black, x + 180, y);
            y = y + 20;
            e.Graphics.DrawString("维持：静脉麻醉药：", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("丙泊酚", ptzt, Brushes.Black, x + 120, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 10, 10);
            if (Jmmzy1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 160, y, x + 160, y + 10);
                e.Graphics.DrawLine(pblue2, x + 165, y + 10, x + 170, y);
            }
            e.Graphics.DrawString("咪唑", ptzt, Brushes.Black, x + 180, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 210, y, 10, 10);
            if (Jmmzy2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 210, y, x + 215, y + 10);
                e.Graphics.DrawLine(pblue2, x + 215, y + 10, x + 220, y);
            }
            e.Graphics.DrawString("氯胺酮", ptzt, Brushes.Black, x + 230, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 270, y, 10, 10);
            if (Jmmzy3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 270, y, x + 275, y + 10);
                e.Graphics.DrawLine(pblue2, x + 275, y + 10, x + 280, y);
            }
            e.Graphics.DrawString("吸入麻醉药：", ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawString("七氟醚", ptzt, Brushes.Black, x + 370, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 410, y, 10, 10);
            if (Xrmzy1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 410, y, x + 415, y + 10);
                e.Graphics.DrawLine(pblue2, x + 415, y + 10, x + 420, y);
            }
            e.Graphics.DrawString("异氟醚", ptzt, Brushes.Black, x + 430, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 470, y, 10, 10);
            if (Xrmzy2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 470, y, x + 475, y + 10);
                e.Graphics.DrawLine(pblue2, x + 475, y + 10, x + 480, y);
            }
            e.Graphics.DrawString("地氟烷", ptzt, Brushes.Black, x + 490, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 530, y, 10, 10);
            if (Xrmzy3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 530, y, x + 535, y + 10);
                e.Graphics.DrawLine(pblue2, x + 535, y + 10, x + 540, y);
            }
            y = y + 20;
            e.Graphics.DrawString("镇痛药：", ptzt, Brushes.Black, x + 30, y);
            e.Graphics.DrawString("芬太尼", ptzt, Brushes.Black, x + 70, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 110, y, 10, 10);
            if (zty1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 110, y, x + 115, y + 10);
                e.Graphics.DrawLine(pblue2, x + 115, y + 10, x + 120, y);
            }
            e.Graphics.DrawString("舒芬", ptzt, Brushes.Black, x + 130, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y, 10, 10);
            if (zty2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 160, y, x + 165, y + 10);
                e.Graphics.DrawLine(pblue2, x + 165, y + 10, x + 170, y);
            }
            e.Graphics.DrawString("雷米芬太尼", ptzt, Brushes.Black, x + 180, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (zty3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("杜冷丁", ptzt, Brushes.Black, x + 260, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (zty4.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("肌松药：", ptzt, Brushes.Black, x + 330, y);
            e.Graphics.DrawString("顺苯磺酸阿曲库铵", ptzt, Brushes.Black, x + 370, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 470, y, 10, 10);
            if (Jsy1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 470, y, x + 475, y + 10);
                e.Graphics.DrawLine(pblue2, x + 475, y + 10, x + 480, y);
            }
            e.Graphics.DrawString("罗库", ptzt, Brushes.Black, x + 490, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 520, y, 10, 10);
            if (Jsy2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 520, y, x + 525, y + 10);
                e.Graphics.DrawLine(pblue2, x + 525, y + 10, x + 530, y);
            }
            e.Graphics.DrawString("维库", ptzt, Brushes.Black, x + 540, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 570, y, 10, 10);
            if (Jsy3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 570, y, x + 575, y + 10);
                e.Graphics.DrawLine(pblue2, x + 575, y + 10, x + 580, y);
            }
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("椎管内麻醉", ptzt, Brushes.Black, x + 10, y);
            y = y + 20;
            e.Graphics.DrawString("硬膜外麻醉", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 70, y, 10, 10);
            if (Zgn1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 70, y, x + 75, y + 10);
                e.Graphics.DrawLine(pblue2, x + 75, y + 10, x + 80, y);
            }
            e.Graphics.DrawString("腰麻", ptzt, Brushes.Black, x + 90, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 10, 10);
            if (Zgn2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 120, y, x + 125, y + 10);
                e.Graphics.DrawLine(pblue2, x + 125, y + 10, x + 130, y);
            }
            e.Graphics.DrawString("腰硬联合", ptzt, Brushes.Black, x + 140, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 190, y, 10, 10);
            if (Zgn3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 190, y, x + 195, y + 10);
                e.Graphics.DrawLine(pblue2, x + 195, y + 10, x + 200, y);
            }
            e.Graphics.DrawString("连续麻醉", ptzt, Brushes.Black, x + 210, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 260, y, 10, 10);
            if (Zgn4.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 260, y, x + 265, y + 10);
                e.Graphics.DrawLine(pblue2, x + 265, y + 10, x + 270, y);
            }

            e.Graphics.DrawString("（穿刺点" + cmbZgCC.Text, ptzt, Brushes.Black, x + 290, y);
            e.Graphics.DrawString(")", ptzt, Brushes.Black, x + 400, y);
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("其它", ptzt, Brushes.Black, x + 10, y);
            y = y + 20;
            e.Graphics.DrawString("颈丛神经麻醉", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 10, 10);
            if (Qtmz1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 80, y, x + 85, y + 10);
                e.Graphics.DrawLine(pblue2, x + 85, y + 10, x + 90, y);
            }
            e.Graphics.DrawString("臂丛神经麻醉", ptzt, Brushes.Black, x + 100, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 10, 10);
            if (Qtmz2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 170, y, x + 175, y + 10);
                e.Graphics.DrawLine(pblue2, x + 175, y + 10, x + 180, y);
            }
            e.Graphics.DrawString("监护麻醉（MAC）", ptzt, Brushes.Black, x + 190, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 10, 10);
            if (Qtmz3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 280, y, x + 285, y + 10);
                e.Graphics.DrawLine(pblue2, x + 285, y + 10, x + 290, y);
            }
            e.Graphics.DrawString("静脉麻醉", ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 350, y, 10, 10);
            if (Qtmz4.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 350, y, x + 355, y + 10);
                e.Graphics.DrawLine(pblue2, x + 355, y + 10, x + 360, y);
            }
            e.Graphics.DrawString("其它 " + txtmzqt.Text, ptzt, Brushes.Black, x + 380, y);
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawString("拟选监测项目：", ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("NIBP", ptzt, Brushes.Black, x + 90, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 120, y, 10, 10);
            if (Nxjcx1.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 120, y, x + 125, y + 10);
                e.Graphics.DrawLine(pblue2, x + 125, y + 10, x + 130, y);
            }
            e.Graphics.DrawString("ECG", ptzt, Brushes.Black, x + 140, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 10, 10);
            if (Nxjcx2.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 170, y, x + 175, y + 10);
                e.Graphics.DrawLine(pblue2, x + 175, y + 10, x + 180, y);
            }
            e.Graphics.DrawString("SPO2", ptzt, Brushes.Black, x + 190, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y, 10, 10);
            if (Nxjcx3.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 220, y, x + 225, y + 10);
                e.Graphics.DrawLine(pblue2, x + 225, y + 10, x + 230, y);
            }
            e.Graphics.DrawString("RR", ptzt, Brushes.Black, x + 240, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 260, y, 10, 10);
            if (Nxjcx4.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 260, y, x + 265, y + 10);
                e.Graphics.DrawLine(pblue2, x + 265, y + 10, x + 270, y);
            }
            e.Graphics.DrawString("T", ptzt, Brushes.Black, x + 280, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (Nxjcx5.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("PETCO2", ptzt, Brushes.Black, x + 320, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 10, 10);
            if (Nxjcx6.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 370, y, x + 375, y + 10);
                e.Graphics.DrawLine(pblue2, x + 375, y + 10, x + 380, y);
            }
            e.Graphics.DrawString("IBP", ptzt, Brushes.Black, x + 390, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y, 10, 10);
            if (Nxjcx7.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 420, y, x + 425, y + 10);
                e.Graphics.DrawLine(pblue2, x + 425, y + 10, x + 430, y);
            }
            e.Graphics.DrawString("尿量", ptzt, Brushes.Black, x + 440, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 470, y, 10, 10);
            if (Nxjcx8.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 470, y, x + 475, y + 10);
                e.Graphics.DrawLine(pblue2, x + 475, y + 10, x + 480, y);
            }
            e.Graphics.DrawString("麻醉气体监测", ptzt, Brushes.Black, x + 490, y);
            e.Graphics.DrawRectangle(Pens.Black, x + 560, y, 10, 10);
            if (Nxjcx9.Checked)
            {
                e.Graphics.DrawLine(pblue2, x + 560, y, x + 565, y + 10);
                e.Graphics.DrawLine(pblue2, x + 565, y + 10, x + 570, y);
            }
            y = y + 20;
            e.Graphics.DrawString("其它 " + txtNxjcxQT.Text, ptzt, Brushes.Black, x + 90, y);
            y = y + 20;
            int BZYYSS = y;
            string strSS1 = "";
            int StrLengthSS = txtSzwt.Text.Trim().Length;
            int rowSS = StrLengthSS / 54;
            e.Graphics.DrawString("术中可能出现的问题及对策：", ptzt, Brushes.Black, x + 10, y);
            for (int i = 0; i <= rowSS; i++)//54个字符就换行
            {
                if (i < rowSS)
                    strSS1 = txtSzwt.Text.Trim().ToString().Substring(i * 54, 54); //从第i*54个开始，截取54个字符串
                else
                    strSS1 = txtSzwt.Text.Trim().ToString().Substring(i * 54);

                BZYYSS = BZYYSS + 20;
                e.Graphics.DrawString(strSS1, ptzt, Brushes.Black, x + 10, BZYYSS);
            }
            y = y + 80;
            int BZYYSS2 = y;
            string strSS12 = "";
            int StrLengthSS2 = txtsjysjjwt.Text.Trim().Length;
            int rowSS2 = StrLengthSS2 / 54;
            e.Graphics.DrawString("需要上级医师解决的问题：", ptzt, Brushes.Black, x + 10, y);
            for (int i = 0; i <= rowSS2; i++)
            {
                if (i < rowSS2)
                    strSS12 = txtsjysjjwt.Text.Trim().ToString().Substring(i * 54, 54);
                else
                    strSS12 = txtsjysjjwt.Text.Trim().ToString().Substring(i * 54);

                BZYYSS2 = BZYYSS2 + 20;
                e.Graphics.DrawString(strSS12, ptzt, Brushes.Black, x + 10, BZYYSS2);
            }
            y = y + 80;
            e.Graphics.DrawString("科内讨论意见：" + txtKntlyj.Text, ptzt, Brushes.Black, x + 10, y);
            e.Graphics.DrawString("麻醉医师：" + cmbMzys.Text, ptzt, Brushes.Black, x + 300, y);
            e.Graphics.DrawString("日期：" + dtVisitDate.Value.Date.ToString("yyyy年MM月dd日"), ptzt, Brushes.Black, x + 450, y);
            y = y + 25;
            e.Graphics.DrawLine(Pens.Black, x, y - 5, x + 640, y - 5);
            e.Graphics.DrawLine(Pens.Black, x, 80 - 5, x, y - 5);
            e.Graphics.DrawLine(Pens.Black, x + 640, 80 - 5, x + 640, y - 5);

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
        /// 过敏史
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGms.Text == "有")
            {
                Gxz1.Enabled = true;
                Gxz2.Enabled = true;
                Gxz3.Enabled = true;
                txtGqt.Enabled = true;
            }
            else
            {
                Gxz1.Enabled = false;
                Gxz2.Enabled = false;
                Gxz3.Enabled = false;
                txtGqt.Enabled = false;
            }
        }
        /// <summary>
        /// 麻醉史
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMzs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMzs.Text == "有")
            {
                Mxz1.Enabled = true;
                Mxz2.Enabled = true;
                txtMqt.Enabled = true;
            }
            else
            {
                Mxz1.Enabled = false;
                Mxz2.Enabled = false;
                txtMqt.Enabled = false;
            }
        }
        /// <summary>
        /// 既往史
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbJws_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbJws.Text == "有")
            {

                Jxz1.Enabled = true;
                Jxz2.Enabled = true;
                Jxz3.Enabled = true;
                Jxz4.Enabled = true;
                Jxz5.Enabled = true;
                txtJqt.Enabled = true;
            }
            else
            {
                Jxz1.Enabled = false;
                Jxz2.Enabled = false;
                Jxz3.Enabled = false;
                Jxz4.Enabled = false;
                Jxz5.Enabled = false;
                txtJqt.Enabled = false;
            }
        }
        /// <summary>
        /// 特殊用药
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTsyy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTsyy.Text == "有")
            {
                Txz1.Enabled = true;
                Txz2.Enabled = true;
                Txz3.Enabled = true;
                txtTqt.Enabled = true;
            }
            else
            {
                Txz1.Enabled = false;
                Txz2.Enabled = false;
                Txz3.Enabled = false;
                txtTqt.Enabled = false;
            }
        }
        /// <summary>
        /// 困难气道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbKnqd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKnqd.Text == "有" || cmbKnqd.Text == "可疑")
            {
                txtYuanyin.Enabled = true;
            }
            else
            {
                txtYuanyin.Enabled = false;
            }
        }
        /// <summary>
        /// 心电图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbXdt_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbXdt.Text=="心律失常")
            //{
            //    XlXZ1.Enabled = true;
            //    XlXZ2.Enabled = true;
            //    XlXZ3.Enabled = true;
            //    txtXlQT.Enabled = true;
            //}
            //else
            //{
            //    XlXZ1.Enabled = false;
            //    XlXZ2.Enabled = false;
            //    XlXZ3.Enabled = false;
            //    txtXlQT.Enabled = false;
            //}
        }
        /// <summary>
        /// 存档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCD_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = _BeforeVistDal.GetBeforeVist_YS(PatId);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = _BeforeVistDal.UpdateBeforeVist_YS_HQ(PatId, 1);
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
            DataTable dt = _BeforeVistDal.GetBeforeVist_YS(PatId);
            if (dt.Rows[0]["isRead"].ToString() == "1")
            {
                result = _BeforeVistDal.UpdateBeforeVist_YS_HQ(PatId, 0);
                if (result > 0)
                {
                    MessageBox.Show("解锁成功！");
                    btnSave.Enabled = true;
                    isRead = false;
                    btnCD.Enabled = true;
                }
            }
        }

        private void BeforeVisit_HQ_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRead)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HQ_FormClosing);
                this.Close();
            }
            else if (SaveCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HQ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HQ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HQ_FormClosing);
                    this.Close();
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
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HQ_FormClosing);
                this.Close();
            }
            else if (SaveCount > 0)
            {
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HQ_FormClosing);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("保存退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Save();
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HQ_FormClosing);
                    this.Close();
                }
                else
                {
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.BeforeVisit_HQ_FormClosing);
                    this.Close();
                }
            }
        }

        private void txtYY_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectYD_YY F1 = new SelectYD_YY(txtYY);
            F1.ShowDialog();
        }






    }
}
