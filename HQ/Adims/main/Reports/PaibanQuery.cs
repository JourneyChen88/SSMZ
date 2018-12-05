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
    public partial class PaibanResult : Form
    {
        int dgvRowCount;
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        public PaibanResult()
        {
            InitializeComponent();
        }

       
        private void dtDataTime_ValueChanged_1(object sender, EventArgs e)
        {
            //BindPaibanResult();
        }
        private void BindPaibanResult()
        {
            string sql1 = "order by convert(int,oroom) ";
            string sql2 = " asc";
            if (cbSecond.Checked)
                sql1 += ",Convert(int,second) ";
            if (cbTime.Checked)
                sql1 += ",starttime ";
            if (cbKeshi.Checked)
                sql1 += ",patdpm ";
            string sql = sql1 + sql2;
            DataTable dt = dal.GetPaibanResult(1, dtDataTime.Value.Date.ToString("yyyy-MM-dd"),sql);
            dgvOTypesetting.DataSource = dt.DefaultView;
        }
        private void PaibanResult_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dtDataTime.Value = DateTime.Now.AddDays(1);
            BindPaibanResult();
            foreach (DataGridViewColumn item in dgvOTypesetting.Columns)	
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            numericUpDown1.Value = 3;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            BindPaibanResult();
        }


        private void btnPrintResult_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {

                if (numericUpDown1.Text != "0")
                {
                    short DaYinNum = short.Parse(numericUpDown1.Text.Trim());//打印的份数
                    PrintDataGridView.Print(dgvOTypesetting, "天津红桥医院手术通知单", "   手术日期：" + dtDataTime.Text, DaYinNum);

                }
                else MessageBox.Show("请填入份数");

            } 
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {           
            ExportExcel(dtDataTime.Value.ToString("yyyyMMdd"), dgvOTypesetting);
        }
        private void ExportExcel(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }           
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            Microsoft.Office.Interop.Excel.Range range = null;

            Microsoft.Office.Interop.Excel.Range range1 = worksheet.get_Range("B2", "S3");
            range1.Select();
            range1.Merge();
            range1.Font.Size = 15;
            range1.Borders.LineStyle = 1;
            range1.Value2 = "昌 吉 州 人 民 医 院 手 术 通 知 单";
            range1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range range2 = worksheet.get_Range("B4", "S4");
            range2.Select();
            range2.Merge();
            range2.Font.Size = 11;
            range2.Value2 = "      手术日期：" + dtDataTime.Text;
            range2.Borders.LineStyle = 1;
            Microsoft.Office.Interop.Excel.Range excelRange = worksheet.get_Range("A6", "U6");
            excelRange.Select();
            xlApp.ActiveWindow.FreezePanes = true;

            //写入标题
            //int ColCount = 0;
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[5, i + 2] = myDGV.Columns[i].HeaderText;
                range = xlApp.Cells[5, i + 2];
                range.Font.Bold = true;
                range.RowHeight = 25;
                range.Interior.ColorIndex = 34;
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                range.Borders.LineStyle = 1;
                if (i == 3 || i == 8 || i == 9)
                {
                    range.ColumnWidth = 10;
                }
                else range.EntireColumn.AutoFit();

            }

            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 6, i + 2] = myDGV.Rows[r].Cells[i].Value;
                    range = worksheet.Cells[r + 6, i + 2];
                    range.Font.Size = 9;
                    range.WrapText = true;
                    int[] a = { 1, 1, 2, 4, 5, 6, 7, 8, 9 };
                    foreach (int dr in a)
                    {
                        if (i == dr)
                        {
                            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        }
                        else
                            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                    }
                    range.EntireRow.AutoFit();//行高自适应
                    range.Borders.LineStyle = 1;

                }
                System.Windows.Forms.Application.DoEvents();
            }
            //worksheet.Columns.EntireColumn.Width = 40;//列宽自适应
            //worksheet.Rows.AutoFilter();
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                    ProgressBar pbar = new ProgressBar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }

            }
            xlApp.Quit();
            GC.Collect();//强行销毁
            MessageBox.Show("文件： " + fileName + ".xls 保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvOTypesetting.Rows.Count > 0)
            {
                printPreviewDialog1.Document = printDocument1;
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    printDocument1.Print();
                //printDocument2.PaperOrientation = PaperOrientation.Landscape;
                printDocument1.DefaultPageSettings.Landscape = false;
            }    
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.pdDocument.DefaultPageSettings.Landscape = true;//横向打印
            this.printDocument1.DefaultPageSettings.Color = true;//彩色打印   
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            dgvRowCount = dgvOTypesetting.Rows.Count;
        }
        int rowindex = 0;
        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {            
            Pen black = new Pen(Brushes.Black);
            black.Width = 1;
            Font HeiTi = new System.Drawing.Font("黑体", 9);
            Font SongTi = new System.Drawing.Font("宋体", 9);
            Font Kti = new System.Drawing.Font("楷体", 7);
            int x = 50, y = 0, y1 = 0;
            for (int i = rowindex; i < dgvRowCount; i++)
            {
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawString("昌吉州人民医院接手术病人通知单", new Font("宋体", 14, FontStyle.Bold), Brushes.Black, new Point(x + 120, y));
                y = y + 35; y1 = y + 15;
                e.Graphics.DrawString("日期：" + dtDataTime.Value.ToShortDateString(), SongTi, Brushes.Black, new Point(x + 30, y));
                e.Graphics.DrawLine(black, x + 60, y1, x + 170, y1);
                e.Graphics.DrawString("科室：" + dgvOTypesetting.Rows[rowindex].Cells["patdpm"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 180, y));
                e.Graphics.DrawLine(black, x + 210, y1, x + 320, y1);
                e.Graphics.DrawString("手术间：" + dgvOTypesetting.Rows[rowindex].Cells["oroom"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 330, y));
                e.Graphics.DrawLine(black, x + 370, y1, x + 470, y1);
                e.Graphics.DrawString("台次：" + dgvOTypesetting.Rows[rowindex].Cells["second"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 480, y));
                e.Graphics.DrawLine(black, x + 510, y1, x + 620, y1);
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawString("姓名：" + dgvOTypesetting.Rows[rowindex].Cells["patname"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 30, y));
                e.Graphics.DrawLine(black, x + 60, y1, x + 170, y1);
                e.Graphics.DrawString("床号：" + dgvOTypesetting.Rows[rowindex].Cells["bedno"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 180, y));
                e.Graphics.DrawLine(black, x + 210, y1, x + 320, y1);
                e.Graphics.DrawString("性别：" + dgvOTypesetting.Rows[rowindex].Cells["patsex"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 330, y));
                e.Graphics.DrawLine(black, x + 360, y1, x + 470, y1);
                e.Graphics.DrawString("年龄：" + dgvOTypesetting.Rows[rowindex].Cells["patage"].Value.ToString(), SongTi, Brushes.Black, new Point(x + 480, y));
                e.Graphics.DrawLine(black, x + 510, y1, x + 620, y1);
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawString("核查患者身份、腕带；手术部位标识；交接药品并签名", SongTi, Brushes.Black, new Point(x + 30, y));
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawString("接患者签名：", SongTi, Brushes.Black, new Point(x + 30, y));
                e.Graphics.DrawLine(black, x + 110, y1, x + 220, y1);
                e.Graphics.DrawString("病房护士签名：", SongTi, Brushes.Black, new Point(x + 230, y));
                e.Graphics.DrawLine(black, x + 320, y1, x + 420, y1);
                e.Graphics.DrawString("接患者时间：", SongTi, Brushes.Black, new Point(x + 430, y));
                e.Graphics.DrawLine(black, x + 520, y1, x + 620, y1);
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawString("预麻醉护士签名：", SongTi, Brushes.Black, new Point(x + 30, y));
                e.Graphics.DrawLine(black, x + 130, y1, x + 220, y1);
                e.Graphics.DrawString("巡回护士签名：", SongTi, Brushes.Black, new Point(x + 230, y));
                e.Graphics.DrawLine(black, x + 300, y1, x + 420, y1);
                e.Graphics.DrawString("入手术间时间：", SongTi, Brushes.Black, new Point(x + 430, y));
                e.Graphics.DrawLine(black, x + 520, y1, x + 620, y1);
                y = y + 35; y1 = y + 15;
                rowindex++;
                if (rowindex % 5 == 0 && rowindex > 4)
                {
                    e.HasMorePages = true;
                    y = 0; y1 = 0;
                    break;
                }
            }
        }

        private void btnBefore_Click(object sender, EventArgs e)
        {
           dtDataTime.Value= dtDataTime.Value.AddDays(-1);
        }

        private void btnAfter_Click(object sender, EventArgs e)
        {
           dtDataTime.Value= dtDataTime.Value.AddDays(1);
        }

       

    }
}
