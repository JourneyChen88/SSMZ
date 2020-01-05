using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class hushizhangtongji : Form
    {
        adims_DAL.AdimsProvider opr = new adims_DAL.AdimsProvider();
        public hushizhangtongji()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataGridView();
        }
        private void BindDataGridView()
        {
            System.Data.DataTable dt = opr.GetMzjldList手术室(dateTimePicker3.Value.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt.DefaultView;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataGridviewShowToExcel(dataGridView1);
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

        private void button3_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = opr.GetMzjldListshoushushigzl(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00"), dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = opr.GetMzjldListshoushushissjl(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00"), dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = opr.GetMzjldListsankeshitongjisudufenji(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00"), dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            dataGridView1.DataSource = dt.DefaultView;
        }
    }
}
