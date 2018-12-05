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

namespace main.Reports
{
    public partial class OperImplantQuery : Form
    {
        OperImplantDal _SszrwDal = new OperImplantDal();
        adims_DAL.AdimsProvider opr = new adims_DAL.AdimsProvider();
        AdimsController adimsController = new AdimsController();
        string conditon = null;
        public OperImplantQuery()
        {
            InitializeComponent();
        }

        private void SSSZR_TJ_Load(object sender, EventArgs e)
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
            DataTable dt0 = opr.GetAllCJ();

            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    treeView1.Nodes[0].Nodes.Add(dt0.Rows[i][0].ToString());
                }
            }
        }
        private void BindDataGridView()
        {
            DateTime date1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd 0:00"));
            DateTime date2 = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            System.Data.DataTable dt = _SszrwDal.GetOperImplant_ByTimeSpan("",date1, date2);
            dataGridView1.DataSource = dt.DefaultView;
         
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
                System.Data.DataTable dt = _SszrwDal.GetOperImplant_ByTimeSpan(TreenodeText, date1, date2);
                dataGridView1.DataSource = dt.DefaultView;
              
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
                PrintTJ.Print(dataGridView1, "天津红桥医院手术室植入物信息", "", 1);
            } 
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            BindDataGridView();
        }
    }
}
