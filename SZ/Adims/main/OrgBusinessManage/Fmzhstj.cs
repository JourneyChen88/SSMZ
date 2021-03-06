﻿using System;
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

namespace main.OrgBusinessManage
{
    public partial class Fmzhstj : Form
    {
        public Fmzhstj()
        {
            InitializeComponent();
        }
        AdimsController adimsController = new AdimsController();
        AdimsProvider apro = new AdimsProvider();
        StringReader myReader;
        private void Fmzhstj_Load(object sender, EventArgs e)
        {
            ////button1_Click(null, null);
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            dateTimePicker1.Text = year + "/1/1";
            //初始化月份第一天
            dateTimePicker1.Text = year + "/" + month + "/1";
            banding();
            BT();
        }
        public void banding()
        {

            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            dataGridView1.Rows.Clear();
            DataTable dt = apro.GetAll_hushi();
            DataTable dt1 = apro.GetAllMZLbyTime(date1, date2);
            int SumAll = Convert.ToInt32(dt1.Rows[0][0].ToString());
            if (SumAll != 0)
            {
                dataGridView1.Rows.Add(dt.Rows.Count - 1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string HSName = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    DataTable dt2 = apro.GetHSMZLbyTime(SumAll, date1, date2, HSName, Program.Customer.yiyuanType);
                    if (dt2.Rows[0][0] == null)
                    {
                        dataGridView1.Rows[i].Cells[1].Value = 0;
                    }
                    if (dt2.Rows[0][1] == null)
                    {
                        dataGridView1.Rows[i].Cells[2].Value = 0;
                    }
                    if (dt2.Rows[0][2] == null)
                    {
                        dataGridView1.Rows[i].Cells[3].Value = 0;
                    }

                    dataGridView1.Rows[i].Cells[1].Value = dt2.Rows[0][0].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dt2.Rows[0][1].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = dt2.Rows[0][2].ToString();
                }
            }
            else
            {
                MessageBox.Show("当前时间段没有数据！！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //myReader = new StringReader(patName);//传值
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = document;
                printPreviewDialog.FormBorderStyle = FormBorderStyle.Fixed3D;
                printPreviewDialog.PrintPreviewControl.Zoom = 1.5;
                if (printPreviewDialog.ShowDialog() != DialogResult.OK) return;
                document.Print();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public void BT()
        {
            this.打印ToolStripMenuItem.Enabled = true;
            if (dataGridView1.Rows.Count > 1)
            {
                #region 柱状图
                List<string> lst = new List<string>();
                List<double> ltnum = new List<double>();
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    lst.Add(dr.Cells["Column1"].Value.ToString());
                    string[] SS = dr.Cells["Column3"].Value.ToString().Split('例');
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
            else
            {
                this.打印ToolStripMenuItem.Enabled = false;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            banding();
            BT();

        }
        int dyi = 0;
        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 50, y = 50;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;

            Font printFont = new Font("新宋体", 12);//rtbBook.Font;
            Font dgvFront = new Font("新宋体", 11);//打印dataGridView
            SolidBrush myBrush = new SolidBrush(Color.Black);

            e.Graphics.DrawString("手术间使用情况统计报表", new Font("新宋体", 16, FontStyle.Bold), Brushes.Black, new Point(250, 50));
            //e.Graphics.DrawString("麻醉单号：", new Font("新宋体", 9, FontStyle.Bold), Brushes.Black, new Point(x1, y1));
            //e.Graphics.DrawString(mzjldID, new Font("新宋体", 9, FontStyle.Underline), Brushes.Black, new Point(x1 + 60, y1));

            int x1 = 50, y1 = x + 50;
            int x2 = 650, y2 = y1;
            int x3 = 50, y3 = y1 + 30;
            ////画药品标题栏
            e.Graphics.DrawLine(Pens.Black, new Point(x1, y1), new Point(x2, y2));
            e.Graphics.DrawLine(Pens.Black, new Point(x1, y1), new Point(x1, y1 + 30));
            e.Graphics.DrawString("护士名称", dgvFront, Brushes.Black, new Point(x1 + 1, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 150, y1), new Point(x1 + 150, y1 + 30));
            e.Graphics.DrawString("麻醉时间(分)", dgvFront, Brushes.Black, new Point(x1 + 151, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 300, y1), new Point(x1 + 300, y1 + 30));
            e.Graphics.DrawString("手术台数", dgvFront, Brushes.Black, new Point(x1 + 301, y1 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x1 + 450, y1), new Point(x1 + 450, y1 + 30));
            e.Graphics.DrawString("工作率", dgvFront, Brushes.Black, new Point(x1 + 451, y1 + 3));
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

                //}
                //else
                //{
                //    e.Graphics.DrawLine(Pens.Black, new Point(x3, y3), new Point(x3, y3 + 30));
                //    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 150, y3), new Point(x3 + 150, y3 + 20));
                //    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 180, y3), new Point(x3 + 180, y3 + 20));
                //    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 280, y3), new Point(x3 + 280, y3 + 20));

                //}

                e.Graphics.DrawLine(Pens.Black, new Point(x3, y3 + 30), new Point(x3 + 600, y3 + 30));//画底部截止线
                y3 = y3 + 30;
            }

            myBrush.Dispose();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
