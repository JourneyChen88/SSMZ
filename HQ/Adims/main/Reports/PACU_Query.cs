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

namespace main.Reports
{
    public partial class PACU_Query : Form
    {
        OperStatisticsDal _OperStatisticsDal = new OperStatisticsDal();
        PacuDal _PacuDal = new PacuDal();
        DataDicDal _DataDicDal = new DataDicDal();
        adims_DAL.AdimsProvider opr = new adims_DAL.AdimsProvider();
        AdimsController adimsController = new AdimsController();
        string conditon = null;
        public PACU_Query()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PACU_TJ_Load(object sender, EventArgs e)
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
                    treeView1.Nodes[0].Nodes.Add(dt0.Rows[i][0].ToString());
                }
            }

            DataTable dt1 = new DataTable();
            dt1 = _DataDicDal.GetOroom();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    treeView1.Nodes[1].Nodes.Add(dt1.Rows[i][0].ToString());
                }
            }

            DataTable dt2 = _DataDicDal.GetKeshi();
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    treeView1.Nodes[2].Nodes.Add(dt2.Rows[i][0].ToString());
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
                    treeView1.Nodes[3].Nodes.Add(list[i].ToString());
                }
            }
            DataTable dt4 = _DataDicDal.GetMazuiFangfaAll();
            if (dt4.Rows.Count > 0)
            {
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    treeView1.Nodes[4].Nodes.Add(dt4.Rows[i][0].ToString());
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
                    treeView1.Nodes[5].Nodes.Add(list5[i].ToString());
                }
            }
            DataTable dt6 = _DataDicDal.GetAll_hushi();

            if (dt6.Rows.Count > 0)
            {
                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    treeView1.Nodes[6].Nodes.Add(dt6.Rows[i][0].ToString());
                }
            }
            DataTable dt7 = _DataDicDal.GetAllSSJB();

            if (dt7.Rows.Count > 0)
            {
                for (int i = 0; i < dt7.Rows.Count; i++)
                {
                    treeView1.Nodes[7].Nodes.Add(dt7.Rows[i][0].ToString());
                }
            }
           
            DataTable dt8 = _DataDicDal.GetAllFXPG();

            if (dt8.Rows.Count > 0)
            {
                for (int i = 0; i < dt8.Rows.Count; i++)
                {
                    treeView1.Nodes[8].Nodes.Add(dt8.Rows[i][0].ToString());
                }
            }
            DataTable dt9 = _DataDicDal.GetAllSSYS();

            if (dt9.Rows.Count > 0)
            {
                for (int i = 0; i < dt9.Rows.Count; i++)
                {
                    treeView1.Nodes[9].Nodes.Add(dt9.Rows[i][0].ToString());
                }
            }
            DataTable dt10 = _DataDicDal.GetAllSF();

            if (dt10.Rows.Count > 0)
            {
                for (int i = 0; i < dt10.Rows.Count; i++)
                {
                    treeView1.Nodes[10].Nodes.Add(dt10.Rows[i][0].ToString());
                }
            }
            DataTable dt11 = _DataDicDal.GetAllSSLB();
            if (dt11.Rows.Count > 0)
            {
                for (int i = 0; i < dt11.Rows.Count; i++)
                {
                    treeView1.Nodes[11].Nodes.Add(dt11.Rows[i][0].ToString());
                }
            }
        }
        private void BindDataGridView()
        {
            DateTime date1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00"));
            DateTime date2 = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            System.Data.DataTable dt = _PacuDal.GetPACUList(date1, date2);
            dataGridView1.DataSource = dt.DefaultView;
            this.toolStripStatusLabel1.Text = "进入复苏室总量：" + (dataGridView1.Rows.Count).ToString();
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
                System.Data.DataTable dt = _OperStatisticsDal.GetPACU_Name(TreenodeText, date1, date2);
                dataGridView1.DataSource = dt.DefaultView;
                this.toolStripStatusLabel1.Text = "进入复苏室总量：" + (dataGridView1.Rows.Count).ToString();
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string where1 = treeView1.SelectedNode.Text.ToString();
            conditon = where1;
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintResult_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                PrintTJ.Print(dataGridView1, "天津红桥医院手术信息", "", 1);
            } 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BindDataGridView();
        }
        /// <summary>
        /// 进入PACU单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPACUs_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells["PatZhuYuanID"].Value.ToString();
                string mzID = dataGridView1.CurrentRow.Cells["mzid"].Value.ToString();
                string date = dataGridView1.CurrentRow.Cells["Column2"].Value.ToString();
                PACU_HQ pacuform = new PACU_HQ(patID, mzID, date);
                pacuform.Show();
            }
        }
    }
}
