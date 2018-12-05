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

namespace main.科室事物管理
{
    public partial class QJJBTJ : Form
    {
        public QJJBTJ()
        {
            InitializeComponent();
        }
        AdimsProvider adimsController = new AdimsProvider();

        private void QJJBTJ_Load(object sender, EventArgs e)
        {
          
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            dateTimePicker1.Text = year + "/1/1";
            //初始化月份第一天
            dateTimePicker1.Text = year + "/" + month + "/1";
            //初始化年第一天
            dateTimePicker3.Text = year + "/1/1";
            //初始化月份第一天
            dateTimePicker3.Text = year + "/" + month + "/1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime time1 = dateTimePicker1.Value;
            DateTime time2 = dateTimePicker2.Value;
            DataTable dt = adimsController.GetJBZS(time1, time2);
            dataGridView1.DataSource = dt.DefaultView;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime time3 = dateTimePicker3.Value;
            DateTime time4 = dateTimePicker4.Value;
            DataTable dt2 = adimsController.GetQJZS(time3, time4);
            dataGridView2.DataSource = dt2.DefaultView;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count>0)
            {
                DataGridviewShowToExcel(dataGridView2);                
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridviewShowToExcel(dataGridView1);
            }
        }

      

      
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
                for (int i = 1; i < dgv.RowCount; i++)
                {
                    for (int j = 1; j < dgv.ColumnCount; j++)
                    {
                        if (dgv[j, i].ValueType == typeof(string))
                            excel.Cells[i + 1, j + 1] = "'" + dgv[j, i].Value.ToString();
                        else
                            excel.Cells[i + 1, j + 1] = dgv[j, i].Value.ToString();
                    }
                }
                excel.Visible = true;
            }
            catch
            {
                MessageBox.Show("导出Execl出现异常,请检查!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string flag = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                DateTime time5 = dateTimePicker1.Value;
                DateTime time6 = dateTimePicker2.Value;
                DataTable dt4 = adimsController.GetJBMX(time5, time6, flag);
                dataGridView4.DataSource = dt4.DefaultView;

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string flag = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                DateTime time5 = dateTimePicker3.Value;
                DateTime time6 = dateTimePicker4.Value;
                DataTable dt3 = adimsController.GetQJMX(time5, time6, flag);
                dataGridView3.DataSource = dt3.DefaultView;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Fprint fprint = new Fprint();
            fprint.ShowDialog();
        }

       

     

      
    }
}
