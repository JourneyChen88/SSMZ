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
using System.IO;

namespace main.科室事物管理
{
    public partial class FSSJTJ : Form
    {
        AdimsController adimsController = new AdimsController();
        AdimsProvider apro = new AdimsProvider();
        public FSSJTJ()
        {
            InitializeComponent();
        }

        private void FSSJTJ_Load(object sender, EventArgs e)
        {
            //ReportDocument rptDoc = new ReportDocument();
            //rptDoc.SetDataSource ( apro.GetSSJname());
            //crystalReportViewer1.ReportSource = rptDoc;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            dateTimePicker1.Text = year + "/1/1";
            //初始化月份第一天
            dateTimePicker1.Text = year + "/" + month + "/1";
            Bind_dataGridView1Cell0();
            #region 柱状图
            List<string> lst = new List<string>();
            List<double> ltnum = new List<double>();
            if (dataGridView1.Rows.Count>0)
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    lst.Add(dr.Cells["oroom"].Value.ToString());
                    string[] SS = dr.Cells["ssNum"].Value.ToString().Split('例');
                    string sss = "";
                    foreach (string temp in SS)
                    {
                        sss = sss + temp;
                    }
                    ltnum.Add(Convert.ToDouble(sss));

                }
                chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧 
                chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                chart1.Series[0].Points.DataBindXY(lst, ltnum);
            }
            
            #endregion

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


        private void Bind_dataGridView1Cell0()//绑定统计出来的数据
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            //sdataGridView1.Rows.Clear();
            DataTable dt1 = apro.GetAllMZLbyTime(date1, date2);
            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("当前时间段没有数据！！");
            }
            else
            {
                int SumAll = Convert.ToInt32(dt1.Rows[0][0].ToString());
                DataTable dt2 = apro.GetYSMZLbyOroom1(SumAll, date1, date2);
                dataGridView1.DataSource = dt2;
            }
            
            //DataTable dt = apro.GetSSJname();
            //DataTable dt1 = apro.GetAllMZLbyTime(date1, date2);
            //if (dt1.Rows.Count == 0)
            //{
            //    MessageBox.Show("当前时间段没有数据！！");
            //}
            //else
            //{
            //    int SumAll = Convert.ToInt32(dt1.Rows[0][0].ToString());
            //    dataGridView1.Rows.Add(dt.Rows.Count - 1);
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
            //    }
            //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        string ysName = dataGridView1.Rows[i].Cells[0].Value.ToString();
            //        DataTable dt2 = apro.GetYSMZLbyOroom1(SumAll, date1, date2, ysName);
            //        if (dt2.Rows[0][0] == null)
            //        {
            //            dataGridView1.Rows[i].Cells[1].Value = 0;
            //        }
            //        if (dt2.Rows[0][1] == null)
            //        {
            //            dataGridView1.Rows[i].Cells[2].Value = 0;
            //        }
            //        if (dt2.Rows[0][2] == null)
            //        {
            //            dataGridView1.Rows[i].Cells[3].Value = 0;
            //        }

            //        dataGridView1.Rows[i].Cells[1].Value = dt2.Rows[0][0].ToString();
            //        dataGridView1.Rows[i].Cells[2].Value = dt2.Rows[0][1].ToString();
            //        dataGridView1.Rows[i].Cells[3].Value = dt2.Rows[0][2].ToString();

            //    }
            //}

        }
        int dyi = 0;
        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 30, y = 50;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;

            Font printFont = new Font("新宋体", 12);//rtbBook.Font;
            Font dgvFront = new Font("新宋体", 11);//打印dataGridView
            SolidBrush myBrush = new SolidBrush(Color.Black);

            e.Graphics.DrawString("手术间使用情况统计报表", new Font("新宋体", 16, FontStyle.Bold), Brushes.Black, new Point(250, 50));
            //e.Graphics.DrawString("麻醉单号：", new Font("新宋体", 9, FontStyle.Bold), Brushes.Black, new Point(x1, y1));
            //e.Graphics.DrawString(mzjldID, new Font("新宋体", 9, FontStyle.Underline), Brushes.Black, new Point(x1 + 60, y1));

            int x1 = 80, y1 = x + 50;
            int x2 = 680, y2 = y1;
            int x3 = 80, y3 = y1 + 30;
            
            ////画药品标题栏
            e.Graphics.DrawLine(Pens.Black, new Point(x1, y1), new Point(x2, y2));
            e.Graphics.DrawLine(Pens.Black, new Point(x1, y1), new Point(x1, y1 + 30));
            e.Graphics.DrawString("手术间名称", dgvFront, Brushes.Black, new Point(x1 + 1, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 150, y1), new Point(x1 + 150, y1 + 30));
            e.Graphics.DrawString("麻醉时间(分)", dgvFront, Brushes.Black, new Point(x1 + 151, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 300, y1), new Point(x1 + 300, y1 + 30));
            e.Graphics.DrawString("手术台数", dgvFront, Brushes.Black, new Point(x1 + 301, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 450, y1), new Point(x1 + 450, y1 + 30));
            e.Graphics.DrawString("手术间使用率", dgvFront, Brushes.Black, new Point(x1 + 451, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 600, y2), new Point(x1 + 600, y1 + 30));

            e.Graphics.DrawLine(Pens.Black, new Point(x3, y3), new Point(x3 + 600, y3));//药品开始横线
            for (; dyi < dataGridView1.Rows.Count; dyi++)
            {
                e.Graphics.DrawLine(Pens.Black, new Point(x3, y3), new Point(x3, y3 + 30));
                e.Graphics.DrawString(dataGridView1[0, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 1, y3 + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x3 + 150, y3), new Point(x3 + 150, y3 + 30));
                e.Graphics.DrawString(dataGridView1[1, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 151, y3 + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x3 + 300, y3), new Point(x3 + 300, y3 + 30));
                e.Graphics.DrawString(dataGridView1[2, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 301, y3 + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x3 + 450, y3), new Point(x3 + 450, y3 + 30));
                e.Graphics.DrawString(dataGridView1[3, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 451, y3 + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x3 + 600, y3), new Point(x3 + 600, y3 + 30));
                e.Graphics.DrawLine(Pens.Black, new Point(x3, y3 + 30), new Point(x3 + 600, y3 + 30));//画底部截止线
                y3 = y3 + 30;
            }

            Rectangle r = new Rectangle(x+140,y3+30,chart1.Width,chart1.Height);
            chart1.Printing.PrintPaint(e.Graphics, r);
            myBrush.Dispose();
            dyi = 0;
        }


        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bind_dataGridView1Cell0();

            #region 柱状图
            List<string> lst = new List<string>();
            List<double> ltnum = new List<double>();
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                lst.Add(dr.Cells["oroom"].Value.ToString());
                string[] SS = dr.Cells["ssNum"].Value.ToString().Split('例');
                string sss = "";
                foreach (string temp in SS)
                {
                    sss = sss + temp;
                }
                ltnum.Add(Convert.ToDouble(sss));

            }
            chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧 
            chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            chart1.Series[0].Points.DataBindXY(lst, ltnum);
            #endregion
        }

        private void btnEXCEL_Click(object sender, EventArgs e)
        {
            DataGridviewShowToExcel(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //myReader = new StringReader(patName);//传值
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = document;
                printPreviewDialog.FormBorderStyle = FormBorderStyle.Fixed3D;
                printPreviewDialog.PrintPreviewControl.Zoom = 1.0;
                if (printPreviewDialog.ShowDialog() != DialogResult.OK) return;
                document.Print();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        
        
    }
}
