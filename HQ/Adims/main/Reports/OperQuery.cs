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
using adims_DAL.Dics;

namespace main
{
    public partial class SSTSTJ : Form
    {
        adims_DAL.Flows.AnesthesiaSummaryDal _MzzjDal = new adims_DAL.Flows.AnesthesiaSummaryDal();
        OperStatisticsDal _OperStatisticsDal = new OperStatisticsDal();
        adims_DAL.Flows.MzjldDal _MzjldDal = new adims_DAL.Flows.MzjldDal();
        DataDicDal _DataDicDal = new DataDicDal();
        public SSTSTJ()
        {
            InitializeComponent();
        }
        adims_DAL.AdimsProvider opr = new adims_DAL.AdimsProvider();
        AdimsController adimsController = new AdimsController();
        string conditon = null;

        private void SSTSTJ_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.TransparencyKey = this.BackColor;
            this.WindowState = FormWindowState.Maximized;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            dateTimePicker1.Text = year + "/1/1";
            //初始化月份第一天
            dateTimePicker1.Text = year + "/" + month + "/1";


            //string sqlWhere = " 1=1 ";
            //System.Data.DataTable dt = adimsController.GetMzjldList(sqlWhere);
            BindDataGridView();
            DataTable dt0 = _DataDicDal.GetAllMZYS();

            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    treeView.Nodes[0].Nodes.Add(dt0.Rows[i][0].ToString());
                }
            }

            DataTable dt1 = new DataTable();
            dt1 = _DataDicDal.GetOroom();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    treeView.Nodes[1].Nodes.Add(dt1.Rows[i][0].ToString());
                }
            }

            DataTable dt2 = _DataDicDal.GetKeshi();
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    treeView.Nodes[2].Nodes.Add(dt2.Rows[i][0].ToString());
                }
            }
            List<string> list = new List<string>();
            list.Add("Ⅰ");
            list.Add("Ⅱ");
            list.Add("Ⅲ");
            list.Add("Ⅳ");
            list.Add("Ⅴ");
            //DataTable dt3 = opr.GetKShiname();
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    treeView.Nodes[3].Nodes.Add(list[i].ToString());
                }
            }
            DataTable dt4 = _DataDicDal.GetMazuiFangfaAll();
            if (dt4.Rows.Count > 0)
            {
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    treeView.Nodes[4].Nodes.Add(dt4.Rows[i][0].ToString());
                }
            }
            List<string> list5 = new List<string>();
            list5.Add("优");
            list5.Add("良");
            list5.Add("差");
            if (list5.Count > 0)
            {
                for (int i = 0; i < list5.Count; i++)
                {
                    treeView.Nodes[5].Nodes.Add(list5[i].ToString());
                }
            }
            DataTable dt6 = _DataDicDal.GetAll_hushi();

            if (dt6.Rows.Count > 0)
            {
                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    treeView.Nodes[6].Nodes.Add(dt6.Rows[i][0].ToString());
                }
            }
            DataTable dt7 = _DataDicDal.GetAllSSJB();

            if (dt7.Rows.Count > 0)
            {
                for (int i = 0; i < dt7.Rows.Count; i++)
                {
                    treeView.Nodes[7].Nodes.Add(dt7.Rows[i][0].ToString());
                }
            }
            DataTable dt8 = _DataDicDal.GetAllFXPG();

            if (dt8.Rows.Count > 0)
            {
                for (int i = 0; i < dt8.Rows.Count; i++)
                {
                    treeView.Nodes[8].Nodes.Add(dt8.Rows[i][0].ToString());
                }
            }
            DataTable dt9 = _DataDicDal.GetAllSSYS();

            if (dt9.Rows.Count > 0)
            {
                for (int i = 0; i < dt9.Rows.Count; i++)
                {
                    treeView.Nodes[9].Nodes.Add(dt9.Rows[i][0].ToString());
                }
            }
            DataTable dt10 = _DataDicDal.GetAllSF();

            if (dt10.Rows.Count > 0)
            {
                for (int i = 0; i < dt10.Rows.Count; i++)
                {
                    treeView.Nodes[10].Nodes.Add(dt10.Rows[i][0].ToString());
                }
            }
            DataTable dt11 = _DataDicDal.GetAllSSLB();
            if (dt11.Rows.Count > 0)
            {
                for (int i = 0; i < dt11.Rows.Count; i++)
                {
                    treeView.Nodes[11].Nodes.Add(dt11.Rows[i][0].ToString());
                }
            }
            //if (dt6.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt6.Rows.Count; i++)
            //    {
            //        treeView1.Nodes[7].Nodes.Add(dt6.Rows[i][0].ToString());
            //    }
            //}
        }

        private void BindDataGridView()
        {
            DateTime date1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00"));
            DateTime date2 = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            DataTable dt = _OperStatisticsDal.GetFromStartToEndTime(date1, date2);
            dataGridView1.DataSource = dt.DefaultView;
            this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Level == 1)
            {
                string TreenodeText = e.Node.Text;
                string TreenodeName = e.Node.Name;
                DateTime date1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00"));
                DateTime date2 = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
                System.Data.DataTable dt = _OperStatisticsDal.GetByYSName(TreenodeText, date1, date2);
                dataGridView1.DataSource = dt.DefaultView;
                this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string where1 = treeView.SelectedNode.Text.ToString();
            conditon = where1;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fmzystj F1 = new Fmzystj();
            F1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fkstj F1 = new Fkstj();
            F1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FSSJTJ F1 = new FSSJTJ();
            F1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BindDataGridView();

        }

        private void btnPrintResult_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                PrintTJ.Print(dataGridView1, "天津红桥医院手术信息", "", 1);
            }
        }
        /// <summary>
        /// 护士统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            FHSTJ f1 = new FHSTJ();
            f1.ShowDialog();
        }
        /// <summary>
        /// 手术医生统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            SSYSTJ f1 = new SSYSTJ();
            f1.ShowDialog();
        }

        private void btnEXCEL_Click(object sender, EventArgs e)
        {
            DataGridviewShowToExcel(dataGridView1);
        }

        /// <summary>
        /// 导出execl
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="isShowExcle"></param>
        /// <returns></returns>
        public void DataGridviewShowToExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0) return;
            try
            {
                //建立Excel对象   
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                //excel.Application.Workbooks.Add(true);
                if (excel == null)
                {
                    MessageBox.Show("Excel 无法启动");
                    return;
                }
                Microsoft.Office.Interop.Excel.Workbook xlBook = excel.Workbooks.Add(true);
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                int cols = dgv.Rows[0].Cells.Count;
                int rows = dgv.Rows.Count;
                Microsoft.Office.Interop.Excel.Range range = null;

                // 列头
                for (int k = 0; k < cols; k++)
                {
                    excel.Cells[1, k + 1] = dgv.Columns[k].HeaderText;
                    range = excel.Cells[1, k + 1];
                    range.Font.Bold = true;
                    range.Interior.ColorIndex = 15;
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    range.EntireColumn.AutoFit();
                }
                //填充数据  
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    for (int j = 0; j < dgv.ColumnCount; j++)
                    {
                        if (dgv[j, i].ValueType == typeof(string))
                            excel.Cells[i + 2, j + 1] = "'" + dgv[j, i].Value.ToString();
                        else
                            excel.Cells[i + 2, j + 1] = dgv[j, i].Value.ToString();
                    }
                }
                excel.Visible = true;
            }
            catch
            {
                MessageBox.Show("导出Execl出现异常,请检查!");
            }
        }
        /// <summary>
        /// 进入麻醉记录单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMZJLD_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["PatZhuYuanID"].Value);
                string oroom = dataGridView1.SelectedRows[0].Cells["Oroom"].Value.ToString();
                DateTime odate = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["Column2"].Value.ToString());
                int mzjldid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["mzid"].Value);
                //this.Close();
                MzjldEdit mzjld1 = new MzjldEdit(patid, oroom, odate, mzjldid, true);
                mzjld1.Show();
            }

        }

        private void 进入护士术前访视记录单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["PatZhuYuanID"].Value);
                string odate = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["Column2"].Value.ToString()).ToString("yyyy-MM-dd");
                BeforeVisit_HS_HQ f2 = new BeforeVisit_HS_HQ(patid, odate);
                f2.ShowDialog();
            }
        }

        private void 进入医师术前访视记录单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["PatZhuYuanID"].Value);
                string odate = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["Column2"].Value.ToString()).ToString("yyyy-MM-dd");
                BeforeVisit_HQ f2 = new BeforeVisit_HQ(patid, odate);
                f2.ShowDialog();
            }
        }

        private void 进入医师术后访视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int mzjldid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["mzjldid"].Value);
                string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["patid"].Value);
                AfterVisit_HQ f2 = new AfterVisit_HQ(mzjldid, patid);
                f2.ShowDialog();
            }
        }

        private void 进入麻醉总结ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string patid = dataGridView1.CurrentRow.Cells["patid"].Value.ToString();
                string date = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["Column2"].Value.ToString()).ToString("yyyy-MM-dd");
                DataTable dts = _MzzjDal.GetMzzjByPatID(patid);
                if (dts.Rows.Count > 0)
                {
                    string mzjld = dts.Rows[0]["mzjldID"].ToString();
                    AnesthesiaSummary f1 = new AnesthesiaSummary(mzjld.ToString(), patid, date);
                    f1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("该病人不存在麻醉记录单，不能进行麻醉总结！");
                }

            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }
    }
}
