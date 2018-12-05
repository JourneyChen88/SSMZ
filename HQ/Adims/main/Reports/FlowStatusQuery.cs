using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_BLL;
using main.科室事物管理;
using adims_DAL.Flows;

namespace main.科室事物管理
{
    public partial class FlowStatusQuery : Form
    {
        OperStatisticsDal _OperStatisticsDal = new OperStatisticsDal();
        string conditon = null;
        public FlowStatusQuery()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_YZBD_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            dateTimePicker1.Text = year + "/1/1";
            //初始化月份第一天
            dateTimePicker1.Text = year + "/" + month + "/1";
            BindDataGridView1();
        }
        /// <summary>
        /// 护士术前访视
        /// </summary>
        private void BindDataGridView4()
        {
            DateTime date1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00"));
            DateTime date2 = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            System.Data.DataTable dt = _OperStatisticsDal.GetBeforeVisit_HS_Status(date1, date2);
            dataGridView1.DataSource = dt.DefaultView;
            this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
            lblCX.Text = "护士术前访视";
            tsmiMZZJ.Visible = false;
            tsmiSQFS.Visible = false;
            tsmiSHFS.Visible = false;
            tsmiHS.Visible = true;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["yizuo"].Value.ToString() == "未做")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
        /// <summary>
        /// 医师术前访视
        /// </summary>
        private void BindDataGridView1()
        {
            DateTime date1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00"));
            DateTime date2 = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            System.Data.DataTable dt = _OperStatisticsDal.GetBeforeVisit_YS_Status(date1, date2);
            dataGridView1.DataSource = dt.DefaultView;
            this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
            lblCX.Text = "医师术前访视查询";
            tsmiMZZJ.Visible = false;
            tsmiSQFS.Visible = true;
            tsmiSHFS.Visible = false;
            tsmiHS.Visible = false;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["yizuo"].Value.ToString() == "未做")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
        /// <summary>
        /// 医师术后访视
        /// </summary>
        private void BindDataGridView3()
        {
            DateTime date1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00"));
            DateTime date2 = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            System.Data.DataTable dt = _OperStatisticsDal.GetAfterVisit_YS_Status(date1, date2);
            dataGridView1.DataSource = dt.DefaultView;
            this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
            lblCX.Text = "医师术后访视查询";
            tsmiSHFS.Visible = true;
            tsmiMZZJ.Visible = false;
            tsmiSQFS.Visible = false;
            tsmiHS.Visible = false;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["yizuo"].Value.ToString() == "未做")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
        /// <summary>
        /// 医师麻醉总结
        /// </summary>
        private void BindDataGridView2()
        {
            DateTime date1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00"));
            DateTime date2 = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            System.Data.DataTable dt = _OperStatisticsDal.GetMzzj_Status(date1, date2);
            dataGridView1.DataSource = dt.DefaultView;
            this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
            lblCX.Text = "医师麻醉总结查询";
            tsmiMZZJ.Visible = true;
            tsmiSQFS.Visible = false;
            tsmiSHFS.Visible = false;
            tsmiHS.Visible = false;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["yizuo"].Value.ToString() == "未做")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BindDataGridView1();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Level == 0)
            {
                string TreenodeText = e.Node.Text;
                if (TreenodeText == "医师术前访视查询")
                {
                    BindDataGridView1();
                }
                else if (TreenodeText == "医师术后访视查询")
                {
                    BindDataGridView3();
                }
                else if (TreenodeText == "医师麻醉总结查询")
                {
                    BindDataGridView2();
                }
                else if (TreenodeText == "护士术前访视")
                {
                    BindDataGridView4();
                }
            }
        }

        private void btnPrintResult_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                PrintTJ.Print(dataGridView1, "天津红桥医院手术信息", "", 1);
            }
        }
        /// <summary>
        /// 术前访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSQFS_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                BeforeVisit_HQ f2 = new BeforeVisit_HQ(dataGridView1.CurrentRow.Cells["PatZhuYuanID"].Value.ToString(), dataGridView1.CurrentRow.Cells["otime"].Value.ToString());
                f2.ShowDialog();
            }

        }
        AnesthesiaSummaryDal _MzzjDal = new AnesthesiaSummaryDal();
        MzjldDal _MzjldDal = new MzjldDal();
        /// <summary>
        /// 麻醉总结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMZZJ_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string patid = dataGridView1.CurrentRow.Cells["PatZhuYuanID"].Value.ToString();
                string date = dataGridView1.CurrentRow.Cells["otime"].Value.ToString();
                DataTable dts = _MzzjDal.GetMzzjByPatID(patid);
                if (dts.Rows.Count > 0)
                {
                    string mzjld = dts.Rows[0]["mzjldID"].ToString();
                    AnesthesiaSummary f1 = new AnesthesiaSummary(mzjld.ToString(), dataGridView1.CurrentRow.Cells["PatZhuYuanID"].Value.ToString(), date);
                    f1.ShowDialog();
                }
                else
                {
                    DataTable dtMZjld = _MzjldDal.GetMzjldByPatId(patid);
                    if (dtMZjld.Rows.Count > 0)
                    {
                        string mzjld = dtMZjld.Rows[0]["id"].ToString();
                        AnesthesiaSummary f1 = new AnesthesiaSummary(mzjld.ToString(), dataGridView1.CurrentRow.Cells["PatZhuYuanID"].Value.ToString(), date);
                        f1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("改病人没有记录过麻醉记录单，不能进行麻醉总结！");
                    }
                }

            }
        }
        /// <summary>
        /// 术后访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSHFS_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.Rows.Count > 0)
            //{       
            //    int       
            //    string patid = dataGridView1.CurrentRow.Cells["PatZhuYuanID"].Value.ToString();            
            //    AfterVisit_HQ f2 = new AfterVisit_HQ(patid, dataGridView1.CurrentRow.Cells["otime"].Value.ToString());
            //    f2.ShowDialog();
            //}
        }
        /// <summary>
        /// 护士术前访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiHS_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string patid = dataGridView1.CurrentRow.Cells["PatZhuYuanID"].Value.ToString();
                BeforeVisit_HS_HQ f2 = new BeforeVisit_HS_HQ(patid, dataGridView1.CurrentRow.Cells["otime"].Value.ToString());
                f2.ShowDialog();
            }
        }
    }
}
