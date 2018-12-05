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
    public partial class ShoushuYYTJ : Form
    {
        adims_DAL.Flows.MzjldDal MzjldDal = new adims_DAL.Flows.MzjldDal();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        StringReader myReader;
        public ShoushuYYTJ()
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
            if (dgvPatInfo.SelectedRows.Count==0)
            {
                MessageBox.Show("请选择麻醉单号");
                return;
            }
            else
            {
                int mzid = Convert.ToInt32(dgvPatInfo.SelectedRows[0].Cells[0].Value);
                mzjldID = dgvPatInfo.CurrentRow.Cells["Column1"].Value.ToString();
                patName = dgvPatInfo.CurrentRow.Cells["Column3"].Value.ToString();
                patNO = dgvPatInfo.CurrentRow.Cells["dataGridViewTextBoxColumn7"].Value.ToString();
                patSEX = dgvPatInfo.CurrentRow.Cells["dataGridViewTextBoxColumn8"].Value.ToString();
                patAGE = dgvPatInfo.CurrentRow.Cells["dataGridViewTextBoxColumn9"].Value.ToString();
                DataTable dt1 = bll.Select_YongYaoList(mzid, 1);//气体查询
                DataTable dt2 = bll.Select_YongYaoList(mzid,2);//全麻
                //DataTable dt3 = bll.selectJumaYao(mzid);//局麻
                DataTable dt4 = bll.Select_YongYaoList(mzid,6);// 特殊用药 
                DataTable dt5 = bll.SQYY(mzid);//术前
                DataTable dt6 = bll.Select_YongYaoList(mzid,5);//输血查询
                DataTable dt7 = bll.Select_YongYaoList(mzid,4);//输液查询
                //dt2 = this.UniteDataTable(dt1, dt2);
                //dt3 = this.UniteDataTable(dt2, dt3);
                //dt4 = this.UniteDataTable(dt3, dt4);
                //dt5 = this.UniteDataTable(dt4, dt5);
                dataGridView1.Rows.Clear();
                int i=0;
                //术前用药统计
                foreach (DataRow dr in dt5.Rows)
                {
                    i++;
                    dataGridView1.Rows.Add(i.ToString(), dr["ypName"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                }
                //全麻药统计
                foreach (DataRow dr in dt2.Rows)
                {
                    if (Convert.ToInt32(dr["Cxyy"]) == 1)
                    {
                        i++;
                        TimeSpan t = new TimeSpan();
                        DateTime dtS = Convert.ToDateTime(dr["sytime"]);
                        DateTime dtE = Convert.ToDateTime(dr["jstime"]);
                        t = dtE - dtS;
                        double sjd = (t.Days * 24 * 60 + t.Hours * 60 + t.Minutes * 60) / 60;
                        double zongl = Convert.ToDouble(dr["yl"]) * sjd;
                        dataGridView1.Rows.Add(i.ToString(), dr["ydyname"].ToString(), zongl.ToString(), dr["dw"].ToString());

                    }
                    else
                    {
                        i++;
                        dataGridView1.Rows.Add(i.ToString(), dr["ydyname"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                    }
                    
                }        
               
                ////氧气统计
                //foreach (DataRow dr in dt1.Rows)
                //{
                //    i++;
                //    dataGridView1.Rows.Add(i.ToString(), dr["qtname"].ToString(), dr["YL"].ToString(), dr["dw"].ToString());

                //}

                //局麻药统计
                //foreach (DataRow dr in dt3.Rows)
                //{
                //    i++;
                //    dataGridView1.Rows.Add(i.ToString(), dr["name"].ToString(), dr["jl"].ToString(), dr["dw"].ToString());
                  
                //}

                //输液统计
                foreach (DataRow dr in dt7.Rows)
                {
                    i++;
                    dataGridView1.Rows.Add(i.ToString(), dr["shuyename"].ToString(), dr["jl"].ToString(), dr["dw"].ToString());

                }

                //输血统计
                foreach (DataRow dr in dt6.Rows)
                {
                    i++;
                    dataGridView1.Rows.Add(i.ToString(), dr["shuxuename"].ToString(), dr["jl"].ToString(), dr["dw"].ToString());

                }

                //其他用药统计               
                foreach (DataRow dr in dt4.Rows)
                {
                    i++;
                    dataGridView1.Rows.Add(i.ToString(), dr["name"].ToString(), dr["yl"].ToString(), dr["dw"].ToString());
                }

            }

        }
       
        private void PatBind()
        {
            DataTable dt1 = MzjldDal.GetMzjldByOtime(dtpStart.Value.Date.ToString("yyyy-MM-dd"));
            dgvPatInfo.DataSource = dt1.DefaultView;
        }

        private void SsYytj_Load(object sender, EventArgs e)
        {
            PatBind();
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.GreenYellow;
        }
        public string patName, mzjldID, patNO, patSEX, patAGE;//传值对象

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    myReader = new StringReader(patName);
                    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                    printPreviewDialog.Document = document;
                    printPreviewDialog.FormBorderStyle = FormBorderStyle.Fixed3D;
                    printPreviewDialog.PrintPreviewControl.Zoom = 1.0;
                    if (printPreviewDialog.ShowDialog() != DialogResult.OK) return;
                    document.Print();
                }
                else
                {
                    MessageBox.Show("请先统计在打印！");
                }
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
            int x3 = 100, y3 = 160;
            int  y4 = 180;
            int div = 0;
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
            e.Graphics.DrawString("姓名：", new Font("新宋体", 10, FontStyle.Bold), Brushes.Black, new Point(x2 + 280, y2));
            e.Graphics.DrawString(patName, new Font("新宋体", 10, FontStyle.Underline), Brushes.Black, new Point(x2 + 340, y2));
            e.Graphics.DrawString("性别：", new Font("新宋体", 10, FontStyle.Bold), Brushes.Black, new Point(x2 + 420, y2));
            e.Graphics.DrawString(patSEX, new Font("新宋体", 10, FontStyle.Underline), Brushes.Black, new Point(x2 + 460, y2));
            e.Graphics.DrawString("年龄：", new Font("新宋体", 10, FontStyle.Bold), Brushes.Black, new Point(x2 + 520, y2));
            e.Graphics.DrawString(patAGE, new Font("新宋体", 10, FontStyle.Underline), Brushes.Black, new Point(x2 + 560, y2));
            

           ////画药品标题栏
            e.Graphics.DrawLine(Pens.Black, new Point(x2, y3), new Point(x2+600, y3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2, y3), new Point(x2, y3 + 20));
            e.Graphics.DrawString("序号", dgvFrontTag, Brushes.Black, new Point(x2 + 15, y3+3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 100, y3), new Point(x2 + 100, y3 + 20));
            e.Graphics.DrawString("药品名称", dgvFrontTag, Brushes.Black, new Point(x2 + 170, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 300, y3), new Point(x2 + 300, y3 + 20));
            e.Graphics.DrawString("用量", dgvFrontTag, Brushes.Black, new Point(x2 + 330, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 400, y3), new Point(x2 + 400, y3 + 20));
            e.Graphics.DrawString("单位", dgvFrontTag, Brushes.Black, new Point(x2 + 480, y3 + 3));
            e.Graphics.DrawLine(Pens.Black, new Point(x2 + 600, y3), new Point(x2 + 600, y3 + 20));

            e.Graphics.DrawLine(Pens.Black, new Point(x3, y4), new Point(x3 + 600, y4));//药品开始横线
            for (; div< dataGridView1.Rows.Count; div++)
            {
                e.Graphics.DrawLine(Pens.Black,new Point(x3,y4),new Point(x3,y4+20));
                e.Graphics.DrawString(dataGridView1[0,div].Value.ToString(),dgvFront,Brushes.Black,new Point(x3+15,y4+3));
                e.Graphics.DrawLine(Pens.Black,new Point(x3+100,y4),new Point(x3+100,y4+20));
                e.Graphics.DrawString(dataGridView1[1,div].Value.ToString(),dgvFront,Brushes.Black,new Point(x3+170,y4+3));
                e.Graphics.DrawLine(Pens.Black, new Point(x3 + 300, y4), new Point(x3 + 300, y4 + 20));
                e.Graphics.DrawString(dataGridView1[2, div].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 330, y4 + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x3 + 400, y4), new Point(x3 + 400, y4 + 20));
                e.Graphics.DrawString(dataGridView1[3, div].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 480, y4 + 3));
                e.Graphics.DrawLine(Pens.Black, new Point(x3 + 600, y4), new Point(x3 + 600, y4 + 20));
                e.Graphics.DrawLine(Pens.Black, new Point(x3, y4 + 20), new Point(x3 + 600, y4 + 20));//画底部截止线
                y4 = y4 + 20;
            }
            //e.Graphics.DrawString("数量", dgvFrontTag, Brushes.Black, new Point(x2 + 301, y3 + 3));
            //e.Graphics.DrawLine(Pens.Black, new Point(x2 + 360, y3), new Point(x2 + 360, y3 + 20));
            //e.Graphics.DrawString("单位", dgvFrontTag, Brushes.Black, new Point(x2 + 361, y3 + 3));
            //e.Graphics.DrawLine(Pens.Black, new Point(x2 + 400, y3), new Point(x2 + 400, y3 + 20));
            //e.Graphics.DrawString("药品名称", dgvFrontTag, Brushes.Black, new Point(x2 + 401, y3 + 3));
            //e.Graphics.DrawLine(Pens.Black, new Point(x2 + 500, y3), new Point(x2 + 500, y3 + 20));
            //e.Graphics.DrawString("数量", dgvFrontTag, Brushes.Black, new Point(x2 + 501, y3 + 3));
            //e.Graphics.DrawLine(Pens.Black, new Point(x2 + 560, y3), new Point(x2 + 560, y3 + 20));
            //e.Graphics.DrawString("单位", dgvFrontTag, Brushes.Black, new Point(x2 + 561, y3 + 3));
            //e.Graphics.DrawLine(Pens.Black, new Point(x2 + 600, y3), new Point(x2 + 600, y3 + 20));
            ////////
            //e.Graphics.DrawLine(Pens.Black, new Point(x3, y4), new Point(x3 + 600, y4));//药品开始横线
           
            //for (; dyi < dataGridView1.Rows.Count; dyi++)
            //{
            //    if (dataGridView1[0, dyi].Value!= null)
            //    {
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3, y4), new Point(x3, y4+20));
            //        e.Graphics.DrawString(dataGridView1[0, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 1, y4 + 3));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 100, y4), new Point(x3 + 100, y4 + 20));
            //        e.Graphics.DrawString(dataGridView1[1, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 101, y4 + 3));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 160, y4), new Point(x3 + 160, y4 + 20));
            //        e.Graphics.DrawString(dataGridView1[2, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 161, y4 + 3));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 200, y4), new Point(x3 + 200, y4 + 20));

            //    }
            //    else
            //    {
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3, y4), new Point(x3, y4 + 20));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 100, y4), new Point(x3 + 100, y4 + 20));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 160, y4), new Point(x3 + 160, y4 + 20));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 200, y4), new Point(x3 + 200, y4 + 20));

            //    }
                
            //    if (dataGridView1[3, dyi].Value != null)
            //    {
            //        e.Graphics.DrawString(dataGridView1[3, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 201, y4 + 3));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 300, y4), new Point(x3 + 300, y4 + 20));
            //        e.Graphics.DrawString(dataGridView1[4, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 301, y4 + 3));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 360, y4), new Point(x3 + 360, y4 + 20));
            //        e.Graphics.DrawString(dataGridView1[5, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 361, y4 + 3));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 400, y4), new Point(x3 + 400, y4 + 20));

            //    }
            //    else
            //    {
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 300, y4), new Point(x3 + 300, y4 + 20));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 360, y4), new Point(x3 + 360, y4 + 20));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 400, y4), new Point(x3 + 400, y4 + 20));


            //    }
                
            //    if (dataGridView1[6, dyi].Value!= null)
            //    {
            //        e.Graphics.DrawString(dataGridView1[6, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 401, y4 + 3));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 500, y4), new Point(x3 + 500, y4 + 20));
            //        e.Graphics.DrawString(dataGridView1[7, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 501, y4 + 3));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 560, y4), new Point(x3 + 560, y4 + 20));
            //        e.Graphics.DrawString(dataGridView1[8, dyi].Value.ToString(), dgvFront, Brushes.Black, new Point(x3 + 561, y4 + 3));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 600, y4), new Point(x3 + 600, y4 + 20));
            //    }
            //    else
            //    {
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 500, y4), new Point(x3 + 500, y4 + 20));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 560, y4), new Point(x3 + 560, y4 + 20));
            //        e.Graphics.DrawLine(Pens.Black, new Point(x3 + 600, y4), new Point(x3 + 600, y4 + 20));
                
            //    }
                
            //    e.Graphics.DrawLine(Pens.Black, new Point(x3, y4 + 20), new Point(x3 + 600, y4 + 20));//画底部截止线
               
            //    y4 = y4 + 20;
            //}
            
            myBrush.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxMzjldID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbxpatNo.SelectedIndex = cbxMzjldID.SelectedIndex;
            //cbxpatName.SelectedIndex = cbxMzjldID.SelectedIndex;
            //cbxMzjldID.SelectedIndex = cbxMzjldID.SelectedIndex;
           
        }

        private void cbxpatNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbxMzjldID.SelectedIndex = cbxpatNo.SelectedIndex;
            //cbxpatName.SelectedIndex = cbxpatNo.SelectedIndex;
            //cbxMzjldID.SelectedIndex = cbxpatNo.SelectedIndex;
           
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            PatBind();
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            PatBind();
        }

        private void dgvPatInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

  

       

       
       
    }
}
