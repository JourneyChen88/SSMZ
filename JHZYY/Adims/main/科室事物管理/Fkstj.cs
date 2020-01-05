///************************************
///Updated By        : Senvi
///************************************

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
using System.Collections;

namespace main.科室事物管理
{
    public partial class Fkstj : Form
    {
        AdimsController adimsController = new AdimsController();
        AdimsProvider apro = new AdimsProvider();

        public Fkstj()
        {
            InitializeComponent();
        }



        private void Fkstj_Load(object sender, EventArgs e)
        {

            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            dateTimePicker1.Text = year + "/1/1";
            //初始化月份第一天
            dateTimePicker1.Text = year + "/" + month + "/1";
            Bind_dataGridView1Cell0();
            //button1_Click(null, null);
            #region 柱状图
            List<string> lstX = new List<string>();
            List<double> ltnumY = new List<double>();
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                lstX.Add(dr.Cells["patdpm"].Value.ToString());
                string[] SS = dr.Cells["ssNum"].Value.ToString().Split('例');
                string sss = "";
                foreach (string temp in SS)
                {
                    sss = sss + temp;
                }
                ltnumY.Add(Convert.ToDouble(sss));

            }
            chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧 
            chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线
            chart1.Series[0].Points.DataBindXY(lstX, ltnumY);
            #endregion

        }



        private void Bind_dataGridView1Cell0()//绑定统计出来的数据
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            dataGridView1.Rows.Clear();
            DataTable dt = apro.GetYSMZLbyOKeshi1(date1, date2);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("当前时间段没有数据！！");
            }            
           
            else
            {
                dataGridView1.Rows.Add(dt.Rows.Count);
                double SUM = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                    SUM = SUM + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[3].Value = Math.Round(Convert.ToDouble(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) * 100.0 / SUM), 2) + " %".ToString();
                }
            }

            #region MyRegion
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    //string ksName = dataGridView1.Rows[i].Cells[0].Value.ToString();
            //    DataTable dt2 = apro.GetYSMZLbyOKeshi1( date1, date2);
            //    if (dt2.Rows[0][0] == null)
            //    {
            //        dataGridView1.Rows[i].Cells[1].Value = 0;
            //    }
            //    if (dt2.Rows[0][1] == null)
            //    {
            //        dataGridView1.Rows[i].Cells[2].Value = 0;
            //    }
            //    if (dt2.Rows[0][2] == null)
            //    {
            //        dataGridView1.Rows[i].Cells[3].Value = 0;
            //    }

            //    dataGridView1.Rows[i].Cells[0].Value = dt2.Rows[0][0].ToString();
            //    dataGridView1.Rows[i].Cells[1].Value = dt2.Rows[0][1].ToString();
            //    dataGridView1.Rows[i].Cells[2].Value = dt2.Rows[0][2].ToString();
            //}

            #endregion
        }
        int dyi = 0;

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
                
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 80, y = 50;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;

            Font printFont = new Font("新宋体", 12);//rtbBook.Font;
            Font dgvFront = new Font("新宋体", 11);//打印dataGridView
            SolidBrush myBrush = new SolidBrush(Color.Black);

            e.Graphics.DrawString("各科室工作统计报表", new Font("新宋体", 16, FontStyle.Bold), Brushes.Black, new Point(250, 50));
            //e.Graphics.DrawString("麻醉单号：", new Font("新宋体", 9, FontStyle.Bold), Brushes.Black, new Point(x1, y1));
            //e.Graphics.DrawString(mzjldID, new Font("新宋体", 9, FontStyle.Underline), Brushes.Black, new Point(x1 + 60, y1));

            int x1 = 80, y1 = y + 50;
            int x2 = 680, y2 = y1;
            int x3 = 80, y3 = y1 + 30;
            ////画药品标题栏
            e.Graphics.DrawLine(Pens.Black, new Point(x1, y1), new Point(x2, y2));
            e.Graphics.DrawLine(Pens.Black, new Point(x1, y1), new Point(x1, y1 + 30));
            e.Graphics.DrawString("科室名称", dgvFront, Brushes.Black, new Point(x1 + 1, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 150, y1), new Point(x1 + 150, y1 + 30));
            e.Graphics.DrawString("麻醉时间(分)", dgvFront, Brushes.Black, new Point(x1 + 151, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 300, y1), new Point(x1 + 300, y1 + 30));
            e.Graphics.DrawString("手术台数", dgvFront, Brushes.Black, new Point(x1 + 301, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 450, y1), new Point(x1 + 450, y1 + 30));
            e.Graphics.DrawString("所占手术比列", dgvFront, Brushes.Black, new Point(x1 + 451, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 600, y2), new Point(x1 + 600, y1 + 30));

            //////
            e.Graphics.DrawLine(Pens.Black, new Point(x3, y3), new Point(x3 + 600, y3));//药品开始横线
            for (; dyi < dataGridView1.Rows.Count; dyi++)
            {
                //if (dataGridView1[0, dyi].Value != null)
                //{
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
            y3 = y3 + 20;
            Rectangle r = new Rectangle(x + 20, y3, chart1.Width, chart1.Height);
            chart1.Printing.PrintPaint(e.Graphics, r);
            myBrush.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
            int x = 30, x1 = 700, y = 0, y1 = 100;
            Font dgvFront = new Font("新宋体", 9);
            int Total = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Total = Total + Convert.ToInt32(dataGridView1[2, i].Value);
            }
            e.Graphics.DrawLine(Pens.Black, x, y, x, y1);
           
            ArrayList colors = new ArrayList();
            Random rnd = new Random();
            for (int i = 0; i < 2; i++)
                colors.Add(new SolidBrush(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))));

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bind_dataGridView1Cell0();
            #region 柱状图
            List<string> lstX = new List<string>();
            List<double> ltnumY = new List<double>();
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                lstX.Add(dr.Cells["patdpm"].Value.ToString());
                string[] SS = dr.Cells["ssNum"].Value.ToString().Split('例');
                string sss = "";
                foreach (string temp in SS)
                {
                    sss = sss + temp;
                }
                ltnumY.Add(Convert.ToDouble(sss));

            }
            chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧 
            chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线
            chart1.Series[0].Points.DataBindXY(lstX, ltnumY);
            #endregion
        }

        private void btnEXCEL_Click(object sender, EventArgs e)
        {
            DataGridviewShowToExcel(dataGridView1);
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
       

    }
}
