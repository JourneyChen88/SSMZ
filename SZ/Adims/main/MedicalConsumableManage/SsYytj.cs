using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace main.MedicalConsumableManage
{
    public partial class SsYytj : Form
    {
        string yiyuanType;
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        StringReader myReader;
        public SsYytj()
        {
            InitializeComponent();
        }

        private DataTable UniteDataTable(DataTable dt1, DataTable dt2)//合并table方法
        {

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt2.Rows.Add(dt1.Rows[i][0], dt1.Rows[i][1], dt1.Rows[i][2], dt1.Rows[i][3]);
            }
            return dt2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvPat.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一位病人！");
                return;
            }
            dgvYyList.Columns[0].DefaultCellStyle.BackColor = Color.GreenYellow;
            dgvYyList.Rows.Clear();
            int mzid = int.Parse(dgvPat.CurrentRow.Cells["id"].Value.ToString());
            Name = dgvPat.CurrentRow.Cells["patname"].Value.ToString();
            Bed = dgvPat.CurrentRow.Cells["Patbedno"].Value.ToString();
            Zhuyuan = dgvPat.CurrentRow.Cells["patzhuyuanid"].Value.ToString();
            Sex = dgvPat.CurrentRow.Cells["patsex"].Value.ToString();
            Age = dgvPat.CurrentRow.Cells["patage"].Value.ToString();
            int i = 0;
            DataTable dtQT = bll.select_qt(mzid);
            if (dtQT.Rows.Count > 0)
            {

                foreach (DataRow dr in dtQT.Rows)
                {
                    TimeSpan t = new TimeSpan();
                    DateTime dtS = Convert.ToDateTime(dr["sytime"]);
                    DateTime dtE = Convert.ToDateTime(dr["jstime"]);
                    t = dtE - dtS;
                    double sjd = (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes * 60) / 60;
                    double zongl = Convert.ToDouble(dr["yl"]) * sjd;
                    dgvYyList.Rows.Add(dr["qtname"].ToString(), zongl.ToString(), dr["dw"].ToString());
                    i++;
                }
            }
            DataTable dtYDY = bll.selectYDY(mzid);
            if (dtYDY.Rows.Count > 0)
            {
                foreach (DataRow dr in dtYDY.Rows)
                {
                    if (dr["Cxyy"].ToString() == "1")
                    {
                        TimeSpan t = new TimeSpan();
                        DateTime dtS = Convert.ToDateTime(dr["sytime"]);
                        DateTime dtE = Convert.ToDateTime(dr["jstime"]);
                        t = dtE - dtS;
                        double sjd = (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes * 60) / 60;
                        double zongl = Convert.ToDouble(dr["yl"]) * sjd;
                        dgvYyList.Rows.Add(dr["ydyname"].ToString(), zongl.ToString(), dr["dw"].ToString());
                        i++;
                    }
                    else
                    {
                        dgvYyList.Rows.Add(dr["ydyname"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                        i++;
                    }
                }
            }
            DataTable dtJMY = bll.selectJumaYao(mzid);
            if (dtJMY.Rows.Count > 0)
            {
                foreach (DataRow dr in dtJMY.Rows)
                {
                    dgvYyList.Rows.Add(dr["name"].ToString(), dr["jl"].ToString(), dr["dw"].ToString());
                    i++;
                }
            }
            DataTable dtZTY = bll.getMZZTY(mzid);
            if (dtZTY.Rows.Count > 0)
            {
                foreach (DataRow dr in dtZTY.Rows)
                {
                    dgvYyList.Rows.Add(dr["name"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                    i++;
                }
            }
            DataTable dtTSYY = bll.getTSYY(mzid);
            if (dtTSYY.Rows.Count > 0)
            {

                foreach (DataRow dr in dtTSYY.Rows)
                {
                    dgvYyList.Rows.Add(dr["name"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                    i++;
                }
            }

            DataTable dtSQYY = bll.GetsqyyUse(mzid);
            if (dtSQYY.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSQYY.Rows)
                {
                    dgvYyList.Rows.Add(dr["ypname"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                    i++;
                }
            }
            //foreach (DataRow dr in dtAll.Rows)
            //{
            //    dgvYyList.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            //}

            //int dtRows = dtAll.Rows.Count;
            //int dgvROWS = dtRows / 2 +1;
            //dgvYyList.Rows.Add(dgvROWS);
        }
        private void SsYytj_Load(object sender, EventArgs e)
        {
            //    int month = DateTime.Now.Month;
            //    int year = DateTime.Now.Year;
            //    //初始化年第一天
            //    dtKStime.Text = year + "/1/1";
            //    //初始化月份第一天
            //    dtKStime.Text = year + "/" + month + "/1";
            yiyuanType = Program.Customer.yiyuanType;
            BindPat();

        }

        public string Name, Zhuyuan, Bed, Sex, Age;//传值对象

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //myReader = new StringReader(patName);
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
        int dyi = 0;
        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            int x1 = 100, y1 = 50;

            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;

            Font printFont = new Font("新宋体", 13);//rtbBook.Font;
            Font dgvFront = new Font("新宋体", 9);//打印dataGridView
            Font dgvFrontTag = new Font("新宋体", 10, FontStyle.Bold);//打印dataGridView标题格式
            SolidBrush myBrush = new SolidBrush(Color.Black);


            //打印标题栏
            e.Graphics.DrawString("药品使用清单", new Font("新宋体", 15, FontStyle.Bold), Brushes.Black, new Point(x1 + 200, y1));


            //打印病人信息栏
            int x2 = 100, y2 = 110;
            int y2L = y2 + 15;
            e.Graphics.DrawString("住院号：" + Zhuyuan, new Font("新宋体", 10), Brushes.Black, new Point(x2, y2));
            e.Graphics.DrawLine(Pens.Black, x2 + 60, y2L, x2 + 130, y2L);
            e.Graphics.DrawString("床号：" + Bed, new Font("新宋体", 10), Brushes.Black, new Point(x2 + 140, y2));
            e.Graphics.DrawLine(Pens.Black, x2 + 170, y2L, x2 + 220, y2L);
            e.Graphics.DrawString("姓名：" + Name, new Font("新宋体", 10), Brushes.Black, new Point(x2 + 230, y2));
            e.Graphics.DrawLine(Pens.Black, x2 + 260, y2L, x2 + 330, y2L);
            e.Graphics.DrawString("性别：" + Sex, new Font("新宋体", 10), Brushes.Black, new Point(x2 + 340, y2));
            e.Graphics.DrawLine(Pens.Black, x2 + 370, y2L, x2 + 420, y2L);
            e.Graphics.DrawString("年龄：" + Age, new Font("新宋体", 10), Brushes.Black, new Point(x2 + 430, y2));
            e.Graphics.DrawLine(Pens.Black, x2 + 460, y2L, x2 + 510, y2L);

            y2 = y2 + 40;
            ////画药品标题栏
            e.Graphics.DrawRectangle(Pens.Black, x2, y2, 130, 30);
            e.Graphics.DrawString("药品名称", dgvFrontTag, Brushes.Black, new Point(x2 + 10, y2 + 3));

            e.Graphics.DrawRectangle(Pens.Black, x2 + 130, y2, 70, 30);
            e.Graphics.DrawString("用量", dgvFrontTag, Brushes.Black, new Point(x2 + 150, y2 + 3));

            e.Graphics.DrawRectangle(Pens.Black, x2 + 200, y2, 70, 30);
            e.Graphics.DrawString("单位", dgvFrontTag, Brushes.Black, new Point(x2 + 220, y2 + 3));

            e.Graphics.DrawRectangle(Pens.Black, x2 + 270, y2, 130, 30);
            e.Graphics.DrawString("药品名称", dgvFrontTag, Brushes.Black, new Point(x2 + 280, y2 + 3));

            e.Graphics.DrawRectangle(Pens.Black, x2 + 400, y2, 70, 30);
            e.Graphics.DrawString("用量", dgvFrontTag, Brushes.Black, new Point(x2 + 420, y2 + 3));

            e.Graphics.DrawRectangle(Pens.Black, x2 + 470, y2, 70, 30);
            e.Graphics.DrawString("单位", dgvFrontTag, Brushes.Black, new Point(x2 + 490, y2 + 3));

            y2 = y2 + 30;
            int j = 0;
            for (int i = 0; i < dgvYyList.Rows.Count; i++)
            {
                if (j % 2 == 0)
                {
                    e.Graphics.DrawRectangle(Pens.Black, x2, y2, 130, 30);
                    e.Graphics.DrawString(dgvYyList[0, i].Value.ToString(), dgvFront, Brushes.Black, new Point(x2 + 10, y2 + 3));

                    e.Graphics.DrawRectangle(Pens.Black, x2 + 130, y2, 70, 30);
                    e.Graphics.DrawString(dgvYyList[1, i].Value.ToString(), dgvFront, Brushes.Black, new Point(x2 + 150, y2 + 3));

                    e.Graphics.DrawRectangle(Pens.Black, x2 + 200, y2, 70, 30);
                    e.Graphics.DrawString(dgvYyList[2, i].Value.ToString(), dgvFront, Brushes.Black, new Point(x2 + 220, y2 + 3));
                    j++;
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Black, x2 + 270, y2, 130, 30);
                    e.Graphics.DrawString(dgvYyList[0, i].Value.ToString(), dgvFront, Brushes.Black, new Point(x2 + 280, y2 + 3));

                    e.Graphics.DrawRectangle(Pens.Black, x2 + 400, y2, 70, 30);
                    e.Graphics.DrawString(dgvYyList[1, i].Value.ToString(), dgvFront, Brushes.Black, new Point(x2 + 420, y2 + 3));

                    e.Graphics.DrawRectangle(Pens.Black, x2 + 470, y2, 70, 30);
                    e.Graphics.DrawString(dgvYyList[2, i].Value.ToString(), dgvFront, Brushes.Black, new Point(x2 + 490, y2 + 3));
                    y2 = y2 + 30;
                    j++;
                }
            }
            //for (; dyi < dgvYyList.Rows.Count; dyi++)
            // {
            //     if (dgvYyList[0, dyi].Value != null)
            //     {
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3, y4), new Point(x3, y4 + 20));
            //         e.Graphics.DrawString(dgvYyList[0, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 1, y4 + 3));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 100, y4), new Point(x3 + 100, y4 + 20));
            //         e.Graphics.DrawString(dgvYyList[1, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 101, y4 + 3));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 160, y4), new Point(x3 + 160, y4 + 20));
            //         e.Graphics.DrawString(dgvYyList[2, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 161, y4 + 3));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 200, y4), new Point(x3 + 200, y4 + 20));

            //     }
            //     else
            //     {
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3, y4), new Point(x3, y4 + 20));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 100, y4), new Point(x3 + 100, y4 + 20));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 160, y4), new Point(x3 + 160, y4 + 20));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 200, y4), new Point(x3 + 200, y4 + 20));

            //     }

            //     if (dgvYyList[3, dyi].Value != null)
            //     {
            //         e.Graphics.DrawString(dgvYyList[3, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 201, y4 + 3));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 300, y4), new Point(x3 + 300, y4 + 20));
            //         e.Graphics.DrawString(dgvYyList[4, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 301, y4 + 3));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 360, y4), new Point(x3 + 360, y4 + 20));
            //         e.Graphics.DrawString(dgvYyList[5, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 361, y4 + 3));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 400, y4), new Point(x3 + 400, y4 + 20));

            //     }
            //     else
            //     {
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 300, y4), new Point(x3 + 300, y4 + 20));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 360, y4), new Point(x3 + 360, y4 + 20));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 400, y4), new Point(x3 + 400, y4 + 20));


            //     }

            //     if (dgvYyList[6, dyi].Value != null)
            //     {
            //         e.Graphics.DrawString(dgvYyList[6, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 401, y4 + 3));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 500, y4), new Point(x3 + 500, y4 + 20));
            //         e.Graphics.DrawString(dgvYyList[7, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 501, y4 + 3));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 560, y4), new Point(x3 + 560, y4 + 20));
            //         e.Graphics.DrawString(dgvYyList[8, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 561, y4 + 3));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 600, y4), new Point(x3 + 600, y4 + 20));
            //     }
            //     else
            //     {
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 500, y4), new Point(x3 + 500, y4 + 20));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 560, y4), new Point(x3 + 560, y4 + 20));
            //         e.Graphics.DrawLine(Pens.Black, new Point(x3 + 600, y4), new Point(x3 + 600, y4 + 20));

            //     }

            //     e.Graphics.DrawLine(Pens.Black, new Point(x3, y4 + 20), new Point(x3 + 600, y4 + 20));//画底部截止线

            //     y4 = y4 + 20;
            // }

            myBrush.Dispose();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void dtKStime_ValueChanged(object sender, EventArgs e)
        {
            BindPat();
        }
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        private void BindPat()
        {
            DataTable dt1 = dal.GetmazuizongjieList(dtKStime.Value.Date.ToString("yyyy-MM-dd"));
            dgvPat.DataSource = dt1.DefaultView;
        }




    }
}
