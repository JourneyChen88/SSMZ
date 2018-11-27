using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_MODEL;

namespace main.Statistics
{
    public partial class ConsumablesUseQueryByOrg : Form
    {
        ConsumablesUseDal dal = new ConsumablesUseDal();
        //int MzjldId;
        //string PatId;
        public ConsumablesUseQueryByOrg()
        {
            InitializeComponent();

        }


        private void BindGridView()
        {
            ConsumablesUseGetInput input = new ConsumablesUseGetInput();
            input.BeginTime = dtStart.Value.Date;
            input.EndTime = dtEnd.Value.Date.AddDays(1);
            DataTable dt = dal.GetConsumablesUseGoupBy(input);
            this.dgvUseList.DataSource = dt;

            double sum = 0.0;
            foreach (DataRow dr in dt.Rows)
            {
                sum += Convert.ToDouble(dr["SumPrice"].ToString());
            }
            tbSum.Text = String.Format("{0:F}", sum);
        }


        private void ConsumablesUseForm_Load(object sender, EventArgs e)
        {
            BindGridView();

        }



        private void tbDosage_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }


        private void btnQuery_Click(object sender, EventArgs e)
        {

            BindGridView();

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvUseList.Rows.Count > 0)
            {
                PrintDataGridView.Print(dgvUseList, "按科室统计耗材使用情况", "打印日期：" + DateTime.Now, 1);
            }

            //this.printPreviewDialog1.Document = printDocument1;
            //if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            //    printDocument1.Print();
            //printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("K16", 737, 1020);
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);

            Font ptzt = new Font("新宋体", 9);//普通字体
            Font ptzt2 = new Font("新宋体", 12);//普通字体
            Font ptzt3 = new Font("新宋体", 13);//普通字体
            int y = 20; int x = 40;
            string title1 = "按科室统计耗材使用情况";
            int row = 0;
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 200, y));

            y += 30;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 660, y);
            row++;
            //第一行

            // e.Graphics.DrawString("姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));


        }
    }
}
