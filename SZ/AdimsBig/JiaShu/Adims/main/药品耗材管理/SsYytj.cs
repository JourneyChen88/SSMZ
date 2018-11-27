using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace main.药品耗材管理
{
    public partial class SsYytj : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        StringReader myReader;
        public SsYytj()
        {
            InitializeComponent();
        }






        private DataTable UniteDataTable(DataTable dt1, DataTable dt2 )//合并table方法
        {

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt2.Rows.Add(dt1.Rows[i][0], dt1.Rows[i][1], dt1.Rows[i][2], dt1.Rows[i][3]);
            }         
            return dt2;
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbxMzjldID.SelectedIndex == -1)
            {
                MessageBox.Show("请选择麻醉单号");
                return;
            }
            else
            {
                int mzid = Convert.ToInt32(cbxMzjldID.Text);
                DataTable dt1 = bll.select_qt(mzid);
                DataTable dt2 = bll.selectYDY(mzid);
                DataTable dt3 = bll.selectJumaYao(mzid);
                DataTable dt4 = bll.getMZZTY(mzid);
                DataTable dt5 = bll.getTSYY(mzid);
                DataTable dt6 = bll.GetsqyyUse(mzid);
                //dt2 = this.UniteDataTable(dt1, dt2);
                //dt3 = this.UniteDataTable(dt2, dt3);
                //dt4 = this.UniteDataTable(dt3, dt4);
                //dt5 = this.UniteDataTable(dt4, dt5);
                //dt6 = this.UniteDataTable(dt5, dt6);
                dataGridView1.Rows.Clear();
                int i=0;
                foreach (DataRow dr in dt1.Rows)
                {
                    TimeSpan t = new TimeSpan();
                    DateTime dtS = Convert.ToDateTime(dr["sytime"]);
                    DateTime dtE = Convert.ToDateTime(dr["jstime"]);
                    t = dtE - dtS;
                    double sjd = (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes * 60) / 60;
                    double zongl = Convert.ToDouble(dr["yl"]) * sjd;
                    dataGridView1.Rows.Add(dr["qtname"].ToString(), zongl.ToString(), dr["dw"].ToString());
                    i++;
                }
                foreach (DataRow dr in dt2.Rows)
                {
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                    {
                        TimeSpan t = new TimeSpan();
                        DateTime dtS = Convert.ToDateTime(dr["sytime"]);
                        DateTime dtE = Convert.ToDateTime(dr["jstime"]);
                        t = dtE - dtS;
                        double sjd = (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes * 60) / 60;
                        double zongl = Convert.ToDouble(dr["yl"]) * sjd;
                        dataGridView1.Rows.Add(dr["ydyname"].ToString(), zongl.ToString(), dr["dw"].ToString());
                        i++;
                    }
                    else
                    {
                        dataGridView1.Rows.Add(dr["ydyname"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                        i++;

                    }

                    
                }
                foreach (DataRow dr in dt3.Rows)
                {
                    dataGridView2.Rows.Add(dr["name"].ToString(), dr["jl"].ToString(), dr["dw"].ToString());
                    i++;
                }
                foreach (DataRow dr in dt4.Rows)
                {
                    dataGridView2.Rows.Add(dr["name"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                    i++;
                }
                foreach (DataRow dr in dt5.Rows)
                {
                    dataGridView3.Rows.Add(dr["name"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                    i++;
                }
                foreach (DataRow dr in dt6.Rows)
                {
                    dataGridView3.Rows.Add(dr["ypname"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                    i++;
                }

                //if (dt6.Rows.Count > 0)
                //{
                //    dataGridView1.Rows.Add(dt6.Rows.Count / 3);
                //    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                //    {
                //        dataGridView1.Columns[i].ReadOnly = false;
                //    }
                //    for (int i = 0; i < dt6.Rows.Count; i++)
                //    {
                //        if (i % 3 == 0)
                //        {
                //            dataGridView1.Rows[i / 3].Cells[0].Value = dt6.Rows[i][1];
                //            dataGridView1.Rows[i / 3].Cells[1].Value = dt6.Rows[i][2];
                //            dataGridView1.Rows[i / 3].Cells[2].Value = dt6.Rows[i][3];

                //        }
                //        else if (i % 3 == 1)
                //        {
                //            dataGridView1.Rows[i / 3].Cells[3].Value = dt6.Rows[i][1];
                //            dataGridView1.Rows[i / 3].Cells[4].Value = dt6.Rows[i][2];
                //            dataGridView1.Rows[i / 3].Cells[5].Value = dt6.Rows[i][3];
                //        }
                //        else if (i % 3 == 2)
                //        {
                //            dataGridView1.Rows[i / 3].Cells[6].Value = dt6.Rows[i][1];
                //            dataGridView1.Rows[i / 3].Cells[7].Value = dt6.Rows[i][2];
                //            dataGridView1.Rows[i / 3].Cells[8].Value = dt6.Rows[i][3];
                //        }
                //    }
                //}
                //else
                //    MessageBox.Show("并无用药记录，请检查");
            }

        }

        private void SsYytj_Load(object sender, EventArgs e)
        {

            DataTable dt = bll.SSpatInfo_table();
            for (int i = 0; i < dt.Rows.Count; i++)
			{
                cbxpatNo.Items.Add(dt.Rows[i][0]);
                cbxpatName.Items.Add(dt.Rows[i][1]);
                cbxMzjldID.Items.Add(dt.Rows[i][2]);
                cbxpatSex.Items.Add(dt.Rows[i][3]);
                cbxAge.Items.Add(dt.Rows[i][4]);
			}
            cbxpatNo.SelectedIndex = -1;
            cbxpatName.SelectedIndex = -1;
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView2.Columns[0].DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView3.Columns[0].DefaultCellStyle.BackColor = Color.GreenYellow;
        }
        public string patName, mzjldID, patNO, patSEX, patAGE;//传值对象
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                 patName = cbxpatName.Text;
                 mzjldID = cbxMzjldID.Text;
                 patNO = cbxpatNo.Text;
                 patSEX = cbxpatSex.Text;
                 patAGE = cbxAge.Text;
                
                myReader = new StringReader(patName);
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = document;
                printPreviewDialog.FormBorderStyle = FormBorderStyle.Fixed3D;
                printPreviewDialog.PrintPreviewControl.Zoom =1.0 ;
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
            int x2 = 100, y2 = 130;
            int x3 = 100, y3 = 180;
            int  y4 = 200;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            
            Font printFont = new Font("新宋体", 13);//rtbBook.Font;
            Font dgvFront = new Font("新宋体", 9);//打印dataGridView
            Font dgvFrontTag = new Font("新宋体", 10, FontStyle.Bold);//打印dataGridView标题格式
            SolidBrush myBrush = new SolidBrush(Color.Black);


            //打印标题栏
            e.Graphics.DrawString("药品使用清单", new Font("新宋体", 15, FontStyle.Bold), Brushes.Black, new Point(x1+200, y1));


            //打印病人信息栏
            e.Graphics.DrawString("麻醉单号：", new Font("新宋体", 10, FontStyle.Bold), Brushes.Black, new Point(x2, y2));
            e.Graphics.DrawString(mzjldID, new Font("新宋体", 10, FontStyle.Underline), Brushes.Black, new Point(x2 + 80, y2));
            e.Graphics.DrawString("病人编号：", new Font("新宋体", 10, FontStyle.Bold), Brushes.Black, new Point(x2 + 120, y2));
            e.Graphics.DrawString(patNO, new Font("新宋体", 10, FontStyle.Underline), Brushes.Black, new Point(x2 + 200, y2));
            e.Graphics.DrawString("姓名：", new Font("新宋体", 10, FontStyle.Bold), Brushes.Black, new Point(x2 + 240, y2));
            e.Graphics.DrawString(patName, new Font("新宋体", 10, FontStyle.Underline), Brushes.Black, new Point(x2 + 300, y2));
            e.Graphics.DrawString("性别：", new Font("新宋体", 10, FontStyle.Bold), Brushes.Black, new Point(x2 + 380, y2));
            e.Graphics.DrawString(patSEX, new Font("新宋体", 10, FontStyle.Underline), Brushes.Black, new Point(x2 + 420, y2));
            e.Graphics.DrawString("年龄：", new Font("新宋体", 10, FontStyle.Bold), Brushes.Black, new Point(x2 + 480, y2));
            e.Graphics.DrawString(patAGE, new Font("新宋体", 10, FontStyle.Underline), Brushes.Black, new Point(x2 + 520, y2));
            

           ////画药品标题栏
            e.Graphics.DrawLine(Pens.Black, new Point(x2, y3), new Point(x2+600, y3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2, y3), new Point(x2, y3 + 20));
            e.Graphics.DrawString("药品名称", dgvFrontTag, Brushes.Black, new Point(x2 + 3, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 100, y3), new Point(x2 + 100, y3 + 20));
            e.Graphics.DrawString("数量", dgvFrontTag, Brushes.Black, new Point(x2 + 101, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 160, y3), new Point(x2 + 160, y3 + 20));
            e.Graphics.DrawString("单位", dgvFrontTag, Brushes.Black, new Point(x2 + 161, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 200, y3), new Point(x2 + 200, y3 + 20));
            e.Graphics.DrawString("药品名称", dgvFrontTag, Brushes.Black, new Point(x2 + 201, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 300, y3), new Point(x2 + 300, y3 + 20));
            e.Graphics.DrawString("数量", dgvFrontTag, Brushes.Black, new Point(x2 + 301, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 360, y3), new Point(x2 + 360, y3 + 20));
            e.Graphics.DrawString("单位", dgvFrontTag, Brushes.Black, new Point(x2 + 361, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 400, y3), new Point(x2 + 400, y3 + 20));
            e.Graphics.DrawString("药品名称", dgvFrontTag, Brushes.Black, new Point(x2 + 401, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 500, y3), new Point(x2 + 500, y3 + 20));
            e.Graphics.DrawString("数量", dgvFrontTag, Brushes.Black, new Point(x2 + 501, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 560, y3), new Point(x2 + 560, y3 + 20));
            e.Graphics.DrawString("单位", dgvFrontTag, Brushes.Black, new Point(x2 + 561, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 600, y3), new Point(x2 + 600, y3 + 20));
            ////////
            e.Graphics.DrawLine(Pens.Black, new Point(x3, y4), new Point(x3 + 600, y4));//药品开始横线
            for (; dyi < dataGridView1.Rows.Count; dyi++)
            {
                if (dataGridView1[0, dyi].Value!= null)
                {
                    e.Graphics.DrawLine(Pens.Black, new Point(x3, y4), new Point(x3, y4+20));
                    e.Graphics.DrawString(dataGridView1[0, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 1, y4 + 3));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 100, y4), new Point(x3 + 100, y4 + 20));
                    e.Graphics.DrawString(dataGridView1[1, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 101, y4 + 3));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 160, y4), new Point(x3 + 160, y4 + 20));
                    e.Graphics.DrawString(dataGridView1[2, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 161, y4 + 3));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 200, y4), new Point(x3 + 200, y4 + 20));

                }
                else
                {
                    e.Graphics.DrawLine(Pens.Black, new Point(x3, y4), new Point(x3, y4 + 20));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 100, y4), new Point(x3 + 100, y4 + 20));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 160, y4), new Point(x3 + 160, y4 + 20));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 200, y4), new Point(x3 + 200, y4 + 20));

                }
                
                if (dataGridView1[3, dyi].Value != null)
                {
                    e.Graphics.DrawString(dataGridView1[3, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 201, y4 + 3));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 300, y4), new Point(x3 + 300, y4 + 20));
                    e.Graphics.DrawString(dataGridView1[4, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 301, y4 + 3));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 360, y4), new Point(x3 + 360, y4 + 20));
                    e.Graphics.DrawString(dataGridView1[5, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 361, y4 + 3));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 400, y4), new Point(x3 + 400, y4 + 20));

                }
                else
                {
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 300, y4), new Point(x3 + 300, y4 + 20));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 360, y4), new Point(x3 + 360, y4 + 20));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 400, y4), new Point(x3 + 400, y4 + 20));


                }
                
                if (dataGridView1[6, dyi].Value!= null)
                {
                    e.Graphics.DrawString(dataGridView1[6, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 401, y4 + 3));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 500, y4), new Point(x3 + 500, y4 + 20));
                    e.Graphics.DrawString(dataGridView1[7, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 501, y4 + 3));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 560, y4), new Point(x3 + 560, y4 + 20));
                    e.Graphics.DrawString(dataGridView1[8, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 561, y4 + 3));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 600, y4), new Point(x3 + 600, y4 + 20));
                }
                else
                {
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 500, y4), new Point(x3 + 500, y4 + 20));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 560, y4), new Point(x3 + 560, y4 + 20));
                    e.Graphics.DrawLine(Pens.Black, new Point(x3 + 600, y4), new Point(x3 + 600, y4 + 20));
                
                }
                
                e.Graphics.DrawLine(Pens.Black, new Point(x3, y4 + 20), new Point(x3 + 600, y4 + 20));//画底部截止线
               
                y4 = y4 + 20;
            }
            
            myBrush.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cbxMzjldID.Text != "")
            {
                int mzjldid = Convert.ToInt32(cbxMzjldID.Text);
                //YytjReport yy = new YytjReport(mzjldid, cbxpatName.Text, cbxpatNo.Text, cbxAge.Text,cbxpatSex.Text);
                //yy.ShowDialog();
            }
            else
                MessageBox.Show("请先选择麻醉记录单号");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxMzjldID_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxpatNo.SelectedIndex = cbxMzjldID.SelectedIndex;
            cbxpatName.SelectedIndex = cbxMzjldID.SelectedIndex;
            cbxMzjldID.SelectedIndex = cbxMzjldID.SelectedIndex;
            cbxAge.SelectedIndex = cbxMzjldID.SelectedIndex;
            cbxpatSex.SelectedIndex = cbxMzjldID.SelectedIndex;

        }

        private void cbxpatNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxMzjldID.SelectedIndex = cbxpatNo.SelectedIndex;
            cbxpatName.SelectedIndex = cbxpatNo.SelectedIndex;
            cbxMzjldID.SelectedIndex = cbxpatNo.SelectedIndex;
            cbxAge.SelectedIndex = cbxpatNo.SelectedIndex;
            cbxpatSex.SelectedIndex = cbxpatNo.SelectedIndex;
        }

       

       
       
    }
}
