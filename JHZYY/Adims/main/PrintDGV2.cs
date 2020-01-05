using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using adims_BLL;
using adims_DAL;
using System.IO;
using System.Drawing.Printing;

namespace main
{

    public static class PrintDGV2
    {

        static DataGridView dgv;

        //标题名称

        static string titleName = "";

        //第二标题名称

        static string titleName2 = "";

        //当前行

        static int rowIndex = 0;

        //当前页

        static int page = 1;

        //每页显示多少行

        static int rowsPerPage = 0;

        /// <summary>

        /// 打印DataGridView

        /// </summary>

        /// <param name="dataGridView">要打印的DataGridView</param>

        /// <param name="title">标题</param>

        /// /// <param name="title2">第二标题,可以为null</param>

        public static void Print(DataGridView dataGridView, string title, string title2, short DaYinNum)
        {
            try
            {
                if (dataGridView == null)
                    return;
                titleName = title;
                titleName2 = title2;
                dgv = dataGridView;
                PrintPreviewDialog ppvw = new PrintPreviewDialog();

                //显示比例为100%

                ppvw.PrintPreviewControl.Zoom = 1.0;
                PrintDocument printDoc = new PrintDocument();

                //A4纸
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 800, 1100);

                //设置横向打印
                printDoc.DefaultPageSettings.Landscape = false;
                //设置边距
                printDoc.DefaultPageSettings.Margins = new Margins(50, 50, 60, 60);

                //设置要打印的文档

                ppvw.Document = printDoc;
                //设置打印的份数
                ppvw.Document.PrinterSettings.Copies = DaYinNum;

                //最大化
                ((Form)ppvw).WindowState = FormWindowState.Maximized;

                //当前行
                rowIndex = 0;

                //当前页
                page = 1;

                //打印事件
                printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
                printDoc.EndPrint += new PrintEventHandler(printDoc_EndPrint);

                //打开预览
                ppvw.ShowDialog();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void printDoc_EndPrint(object sender, PrintEventArgs e)
        {

            //当前行
            rowIndex = 0;
            //当前页
            page = 1;
            //每页显示多少行
            rowsPerPage = 0;
        }
        private static void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {

            //标题字体
            Font titleFont = new Font("宋体", 15, FontStyle.Bold);
            Font titleFont2 = new Font("宋体", 10, FontStyle.Bold);

            //标题尺寸
            SizeF titleSize = e.Graphics.MeasureString(titleName, titleFont, e.MarginBounds.Width);

            //x坐标
            int x = e.MarginBounds.Left;

            //y坐标
            int y = Convert.ToInt32(e.MarginBounds.Top - titleSize.Height)+30;
            int Ycount = y;

            //边距以内纸张宽度
            int pagerWidth = e.MarginBounds.Width;

            //画标题
            e.Graphics.DrawString(titleName, titleFont, Brushes.Black, x + (pagerWidth - titleSize.Width) / 2, y);
            y += (int)titleSize.Height + 20;
            if (titleName2 != null && titleName2 != "")
            {
                //画第二标题
                e.Graphics.DrawString(titleName2, titleFont2, Brushes.Black, x, y);
                Ycount = y;

                //第二标题尺寸
                SizeF titleSize2 = e.Graphics.MeasureString(titleName2, dgv.Font, e.MarginBounds.Width);

                y += (int)titleSize2.Height; ;
            }

            //表头高度
            int headerHeight = 0;

            //纵轴上 内容与线的距离
            int padding = 5;

            //所有显示列的宽度
            int columnsWidth = 0;

            //计算所有显示列的宽度
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                //隐藏列返回
                if (!column.Visible) continue;
                //所有显示列的宽度
                columnsWidth += column.Width;
            }

            //计算表头高度
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                //列宽
                int columnWidth = 0;
                columnWidth = (int)(Math.Floor((double)column.Width / (double)columnsWidth * (double)pagerWidth));

                //表头高度
                int temp = (int)e.Graphics.MeasureString(column.HeaderText, column.InheritedStyle.Font,
                columnWidth).Height + 2 * padding;

                if (temp > headerHeight) headerHeight = temp;
            }

            //画表头
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                //隐藏列返回
                if (!column.Visible) continue;

                //列宽
                int columnWidth = (int)(Math.Floor((double)column.Width / (double)columnsWidth * (double)pagerWidth));

                //内容居中要加的宽度
                float cenderWidth = (columnWidth - e.Graphics.MeasureString(column.HeaderText,
                column.InheritedStyle.Font, columnWidth).Width) / 2;

                if (cenderWidth < 0) cenderWidth = 0;

                //内容居中要加的高度
                float cenderHeight = (headerHeight + padding - e.Graphics.MeasureString(column.HeaderText,
                column.InheritedStyle.Font, columnWidth).Height) / 2;

                if (cenderHeight < 0) cenderHeight = 0;
                //画背景
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(x, y, columnWidth, headerHeight));

                //画边框

                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(x, y, columnWidth, headerHeight));

                //画内容
                e.Graphics.DrawString(column.HeaderText, column.InheritedStyle.Font, new SolidBrush
                (column.InheritedStyle.ForeColor), new RectangleF(x + cenderWidth, y + cenderHeight, columnWidth, headerHeight));
                x += columnWidth;
            }

            x = e.MarginBounds.Left;
            y += headerHeight;

            //遍历行
            while (rowIndex < dgv.Rows.Count)
            {
                //当前行
                DataGridViewRow row = dgv.Rows[rowIndex];
                if (row.Visible)
                {
                    //行高
                    int rowHeight = 0;
                    //计算行高
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //当前列
                        DataGridViewColumn column = dgv.Columns[cell.ColumnIndex];
                        //隐藏列返回
                        if (!column.Visible || cell.Value == null) continue;
                        //列宽
                        int tmpWidth = (int)(Math.Floor((double)column.Width / (double)columnsWidth * (double)pagerWidth));

                        //行高
                        int temp = (int)e.Graphics.MeasureString(cell.Value.ToString(), column.InheritedStyle.Font,
                        tmpWidth).Height + 2 * padding +10;

                        if (temp > rowHeight) rowHeight = temp;

                    }

                    //遍历列
                    foreach (DataGridViewCell cell in row.Cells)
                    {

                        //当前列
                        DataGridViewColumn column = dgv.Columns[cell.ColumnIndex];

                        //隐藏列返回
                        if (!column.Visible) continue;

                        //列宽
                        int columnWidth = (int)(Math.Floor((double)column.Width / (double)columnsWidth * (double)
                        pagerWidth));

                        //画边框
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(x, y, columnWidth, rowHeight));

                        if (cell.Value != null)
                        {
                            //内容居中要加的宽度
                            float cenderWidth = (columnWidth - e.Graphics.MeasureString(cell.Value.ToString(),
                            cell.InheritedStyle.Font, columnWidth).Width) / 2;

                            if (cenderWidth < 0) cenderWidth = 0;

                            //内容居中要加的高度
                            float cenderHeight = (rowHeight + padding - e.Graphics.MeasureString(cell.Value.ToString(),
                            cell.InheritedStyle.Font, columnWidth).Height) / 2;

                            if (cenderHeight < 0) cenderHeight = 0;


                            //画内容
                            e.Graphics.DrawString(cell.Value.ToString(), column.InheritedStyle.Font, new SolidBrush
                            (cell.InheritedStyle.ForeColor), new RectangleF(x + cenderWidth, y + cenderHeight, columnWidth, rowHeight));

                        }
                        x += columnWidth;

                    }
                    x = e.MarginBounds.Left;
                    y += rowHeight;
                    if (page == 1) rowsPerPage++;

                    //打印下一页
                    if (y + rowHeight > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        rowIndex++;

                        break;
                    }
                }
                rowIndex++;
            }
            //页脚
            string footer = " 第 " + page + " 页，共 " + Math.Ceiling(((double)dgv.Rows.Count / rowsPerPage)).ToString() + " 页";

            //画页脚
            e.Graphics.DrawString(footer, dgv.Font, Brushes.Black, x + 800, Ycount);
            e.Graphics.DrawString(footer, dgv.Font, Brushes.Black, x + (pagerWidth - e.Graphics.MeasureString
            (footer, dgv.Font).Width) / 2, e.MarginBounds.Bottom);
            page++;


        }

    }

}
