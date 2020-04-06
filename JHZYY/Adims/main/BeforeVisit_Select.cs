using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using main.CGG;

namespace main
{
    public partial class BeforeVisit_Select : Form
    {
        adims_DAL.AdimsProvider apro =  new  adims_DAL.AdimsProvider();
        adims_BLL.AdimsController acon = new adims_BLL.AdimsController();
      
        public BeforeVisit_Select()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                string patID = dataGridView1.CurrentRow.Cells["住院号"].Value.ToString();
                this.Close();
                //MZsqFsd f2 = new MZsqFsd(patID);
                BeforeVisit_JH f2 = new BeforeVisit_JH(patID);
                f2.ShowDialog();              
            }
            else MessageBox.Show("请选择病人！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DataBind()
        {
            DataTable dt1 = acon.Sqfs1(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt1.DefaultView;
        }

        private void BeforeVisit_Select_Load(object sender, EventArgs e)
        {
            DataBind();
            cmbFSHS.DataSource = acon.GetMZYS(2);
            cmbFSHS.DisplayMember = "user_name";
            groupBox1.Visible = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
            if (dataGridView1.Rows.Count > 0)
                groupBox1.Visible = true;
            else
                groupBox1.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells["住院号"].Value.ToString();
                Zffyzqshu f2 = new Zffyzqshu(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
            }
            else MessageBox.Show("请选择病人！");
        }
        /// <summary>
        /// 打印全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells["住院号"].Value.ToString();
                mazuigyishuzhiqingtongyishu f2 = new mazuigyishuzhiqingtongyishu(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
            }
            else MessageBox.Show("请选择病人！");
            //if (dataGridView1.Rows.Count>0)
            //{
            //    groupBox1.Visible = true;
            //}
           
        }
        /// <summary>
        /// 全部打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnprint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printDocument1.DefaultPageSettings.PaperSize =
                     new System.Drawing.Printing.PaperSize("A4", 820, 1160); 
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize = 
            //    new PaperSize("16K", 737, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
               new PaperSize("A4", 820, 1160);
           
        }
        int i = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }
        int rowIndex = 0;
        private void printDocument1_BeginPrint_1(object sender, PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("K16", 740, 1020);  
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            rowIndex = 0;
 
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            int dgvRowCount = dataGridView1.Rows.Count;
            Font zt9 = new Font(new FontFamily("宋体"), 10);
            Font ht13 = new Font(new FontFamily("黑体"), 15);
            //Font ht9 = new Font(new FontFamily("黑体"), 9);
            Pen ptp = new Pen(Brushes.Black);
            Pen pb2 = new Pen(Brushes.Black, 2);
            for (i = rowIndex; i < dgvRowCount;i++ )
            {
                int x = 50, y = 80, y1 = y + 15;
                e.Graphics.DrawString("昌吉州人名医院洁净手术部围手术期病人护理访视单", ht13, Brushes.Black, x + 100, y);
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 30));
                e.Graphics.DrawString("科 别", zt9, Brushes.Black, x + 10, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 80, y), new Point(x + 80, y + 30));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["病区"].Value.ToString(), zt9, Brushes.Black, x + 90, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 250, y), new Point(x + 250, y + 30));
                e.Graphics.DrawString("姓 名", zt9, Brushes.Black, x + 260, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 340, y), new Point(x + 340, y + 30));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["姓名"].Value.ToString(), zt9, Brushes.Black, x + 350, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 520, y), new Point(x + 520, y + 30));
                e.Graphics.DrawString("住院号", zt9, Brushes.Black, x + 530, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 600, y), new Point(x + 600, y + 30));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["住院号"].Value.ToString(), zt9, Brushes.Black, x + 610, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 30));
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 30));
                e.Graphics.DrawString("床 号", zt9, Brushes.Black, x + 10, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 80, y), new Point(x + 80, y + 30));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["床号"].Value.ToString(), zt9, Brushes.Black, x + 90, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 250, y), new Point(x + 250, y + 30));
                e.Graphics.DrawString("诊 断", zt9, Brushes.Black, x + 260, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 340, y), new Point(x + 340, y + 30));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["主要诊断"].Value.ToString(), zt9, Brushes.Black, x + 350, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 520, y), new Point(x + 520, y + 30));
                e.Graphics.DrawString("年 龄", zt9, Brushes.Black, x + 530, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 600, y), new Point(x + 600, y + 30));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells["年龄"].Value.ToString() + "岁", zt9, Brushes.Black, x + 610, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 30));
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x + 720, y));//画横线
                e.Graphics.DrawString("手", zt9, Brushes.Black, x + 10, y + 100);
                e.Graphics.DrawString("术", zt9, Brushes.Black, x + 10, y + 160);
                e.Graphics.DrawString("前", zt9, Brushes.Black, x + 10, y + 220);
                e.Graphics.DrawLine(ptp, new Point(x + 40, y), new Point(x + 40, y + 370));
                e.Graphics.DrawString("术", zt9, Brushes.Black, x + 50, y + 10);
                e.Graphics.DrawString("前", zt9, Brushes.Black, x + 50, y + 30);
                e.Graphics.DrawString("宣", zt9, Brushes.Black, x + 50, y + 50);
                e.Graphics.DrawString("教", zt9, Brushes.Black, x + 50, y + 70);
                e.Graphics.DrawLine(ptp, new Point(x + 80, y), new Point(x + 80, y + 370));
                e.Graphics.DrawString("目的：对病人的健康状况、心理状况进行评估，安抚病人紧张情绪，增强对手术治疗的信心。", zt9, Brushes.Black, x + 90, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 80, y + 30), new Point(x + 720, y + 30));//画横线
                e.Graphics.DrawString("内容：", zt9, Brushes.Black, x + 90, y + 40);
                e.Graphics.DrawString("1.自我介绍。 2.术前精神放松与配合。3.介绍手术室环境、手术部位、麻醉手术体位及配合。", zt9, Brushes.Black, x + 90, y + 60);
                e.Graphics.DrawString("4.前禁食水、清洁卫生等注意事项。 5.术中注意事项。 6.心理护理。", zt9, Brushes.Black, x + 90, y + 80);          
                e.Graphics.DrawLine(ptp, new Point(x + 40, y + 100), new Point(x + 720, y + 100));//画横线
                e.Graphics.DrawString("观", zt9, Brushes.Black, x + 50, y + 170);
                e.Graphics.DrawString("察", zt9, Brushes.Black, x + 50, y + 200);
                e.Graphics.DrawString("病", zt9, Brushes.Black, x + 50, y + 230);
                e.Graphics.DrawString("人", zt9, Brushes.Black, x + 50, y + 260);
                e.Graphics.DrawString("身体状况：", zt9, Brushes.Black, x + 90, y + 110);
                e.Graphics.DrawString("好", zt9, Brushes.Black, x + 220, y + 110);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 110, 12, 12);
                //if (cmbSQstzk.Text == "好")
                //{
                //    e.Graphics.DrawLine(pb2, x + 245, y + 110, x + 255, y + 110 + 12);
                //    e.Graphics.DrawLine(pb2, x + 255, y + 110 + 12, x + 270, y + 110 - 3);
                //}
                e.Graphics.DrawString("一般", zt9, Brushes.Black, x + 320, y + 110);
                e.Graphics.DrawRectangle(Pens.Black, x + 360, y + 110, 12, 12);
                //if (cmbSQstzk.Text == "一般")
                //{
                //    e.Graphics.DrawLine(pb2, x + 355, y + 110, x + 365, y + 110 + 12);
                //    e.Graphics.DrawLine(pb2, x + 365, y + 110 + 12, x + 380, y + 110 - 3);
                //}
                e.Graphics.DrawString("差", zt9, Brushes.Black, x + 430, y + 110);
                e.Graphics.DrawRectangle(Pens.Black, x + 460, y + 110, 12, 12);
                //if (cmbSQstzk.Text == "差")
                //{
                //    e.Graphics.DrawLine(pb2, x + 455, y + 110, x + 465, y + 110 + 12);
                //    e.Graphics.DrawLine(pb2, x + 465, y + 110 + 12, x + 480, y + 110 - 3);
                //}
                e.Graphics.DrawString("危重", zt9, Brushes.Black, x + 530, y + 110);
                e.Graphics.DrawRectangle(Pens.Black, x + 570, y + 110, 12, 12);
                //if (cmbSQstzk.Text == "危重")
                //{
                //    e.Graphics.DrawLine(pb2, x + 565, y + 110, x + 575, y + 110 + 12);
                //    e.Graphics.DrawLine(pb2, x + 575, y + 110 + 12, x + 590, y + 110 - 3);
                //}
                e.Graphics.DrawLine(ptp, new Point(x + 80, y + 130), new Point(x + 720, y + 130));//画横线
                e.Graphics.DrawString("体    型：", zt9, Brushes.Black, x + 90, y + 140);
                e.Graphics.DrawString("正常", zt9, Brushes.Black, x + 210, y + 140);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 140, 12, 12);
                //if (cmbSQtx.Text == "正常")
                //{
                //    e.Graphics.DrawLine(pb2, x + 245, y + 140, x + 255, y + 140 + 12);
                //    e.Graphics.DrawLine(pb2, x + 255, y + 140 + 12, x + 270, y + 140 - 3);
                //}
                e.Graphics.DrawString("瘦弱", zt9, Brushes.Black, x + 320, y + 140);
                e.Graphics.DrawRectangle(Pens.Black, x + 360, y + 140, 12, 12);
                //if (cmbSQtx.Text == "瘦弱")
                //{
                //    e.Graphics.DrawLine(pb2, x + 355, y + 140, x + 365, y + 140 + 12);
                //    e.Graphics.DrawLine(pb2, x + 365, y + 140 + 12, x + 380, y + 140 - 3);
                //}
                e.Graphics.DrawString("恶病质", zt9, Brushes.Black, x + 410, y + 140);
                e.Graphics.DrawRectangle(Pens.Black, x + 460, y + 140, 12, 12);
                //if (cmbSQtx.Text == "恶病质")
                //{
                //    e.Graphics.DrawLine(pb2, x + 455, y + 140, x + 465, y + 140 + 12);
                //    e.Graphics.DrawLine(pb2, x + 465, y + 140 + 12, x + 480, y + 140 - 3);
                //}
                e.Graphics.DrawString("肥胖", zt9, Brushes.Black, x + 530, y + 140);
                e.Graphics.DrawRectangle(Pens.Black, x + 570, y + 140, 12, 12);
                //if (cmbSQtx.Text == "肥胖")
                //{
                //    e.Graphics.DrawLine(pb2, x + 565, y + 140, x + 575, y + 140 + 12);
                //    e.Graphics.DrawLine(pb2, x + 575, y + 140 + 12, x + 590, y + 140 - 3);
                //}
                e.Graphics.DrawLine(ptp, new Point(x + 80, y + 160), new Point(x + 720, y + 160));//画横线
                e.Graphics.DrawString("心理状态：", zt9, Brushes.Black, x + 90, y + 170);
                e.Graphics.DrawString("乐观", zt9, Brushes.Black, x + 210, y + 170);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 170, 12, 12);
                //if (cmbSQxlzt.Text == "乐观")
                //{
                //    e.Graphics.DrawLine(pb2, x + 245, y + 170, x + 255, y + 170 + 12);
                //    e.Graphics.DrawLine(pb2, x + 255, y + 170 + 12, x + 270, y + 170 - 3);
                //}
                e.Graphics.DrawString("平静", zt9, Brushes.Black, x + 320, y + 170);
                e.Graphics.DrawRectangle(Pens.Black, x + 360, y + 170, 12, 12);
                //if (cmbSQxlzt.Text == "平静")
                //{
                //    e.Graphics.DrawLine(pb2, x + 355, y + 170, x + 365, y + 170 + 12);
                //    e.Graphics.DrawLine(pb2, x + 365, y + 170 + 12, x + 380, y + 170 - 3);
                //}
                e.Graphics.DrawString("焦虑", zt9, Brushes.Black, x + 420, y + 170);
                e.Graphics.DrawRectangle(Pens.Black, x + 460, y + 170, 12, 12);
                //if (cmbSQxlzt.Text == "焦虑")
                //{
                //    e.Graphics.DrawLine(pb2, x + 455, y + 170, x + 465, y + 170 + 12);
                //    e.Graphics.DrawLine(pb2, x + 465, y + 170 + 12, x + 480, y + 170 - 3);
                //}
                e.Graphics.DrawString("紧张", zt9, Brushes.Black, x + 530, y + 170);
                e.Graphics.DrawRectangle(Pens.Black, x + 570, y + 170, 12, 12);
                //if (cmbSQxlzt.Text == "紧张")
                //{
                //    e.Graphics.DrawLine(pb2, x + 565, y + 170, x + 575, y + 170 + 12);
                //    e.Graphics.DrawLine(pb2, x + 575, y + 170 + 12, x + 590, y + 170 - 3);
                //}
                e.Graphics.DrawLine(ptp, new Point(x + 80, y + 190), new Point(x + 720, y + 190));//画横线
                e.Graphics.DrawString("肢体运动：", zt9, Brushes.Black, x + 90, y + 200);
                e.Graphics.DrawString("正常", zt9, Brushes.Black, x + 210, y + 200);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 200, 12, 12);
                //if (cmbSQztyd.Text == "正常")
                //{
                //    e.Graphics.DrawLine(pb2, x + 245, y + 200, x + 255, y + 200 + 12);
                //    e.Graphics.DrawLine(pb2, x + 255, y + 200 + 12, x + 270, y + 200 - 3);
                //}
                e.Graphics.DrawString("偏瘫", zt9, Brushes.Black, x + 320, y + 200);
                e.Graphics.DrawRectangle(Pens.Black, x + 360, y + 200, 12, 12);
                //if (cmbSQztyd.Text == "偏瘫")
                //{
                //    e.Graphics.DrawLine(pb2, x + 355, y + 200, x + 365, y + 200 + 12);
                //    e.Graphics.DrawLine(pb2, x + 365, y + 200 + 12, x + 380, y + 200 - 3);
                //}
                e.Graphics.DrawString("全瘫", zt9, Brushes.Black, x + 420, y + 200);
                e.Graphics.DrawRectangle(Pens.Black, x + 460, y + 200, 12, 12);
                //if (cmbSQztyd.Text == "全瘫")
                //{
                //    e.Graphics.DrawLine(pb2, x + 455, y + 200, x + 465, y + 200 + 12);
                //    e.Graphics.DrawLine(pb2, x + 465, y + 200 + 12, x + 480, y + 200 - 3);
                //}
                e.Graphics.DrawString("活动受限", zt9, Brushes.Black, x + 500, y + 200);
                e.Graphics.DrawRectangle(Pens.Black, x + 570, y + 200, 12, 12);
                //if (cmbSQztyd.Text == "活动受限")
                //{
                //    e.Graphics.DrawLine(pb2, x + 565, y + 200, x + 575, y + 200 + 12);
                //    e.Graphics.DrawLine(pb2, x + 575, y + 200 + 12, x + 590, y + 200 - 3);
                //}
                e.Graphics.DrawLine(ptp, new Point(x + 80, y + 220), new Point(x + 720, y + 220));//画横线
                e.Graphics.DrawString("四肢血管：", zt9, Brushes.Black, x + 90, y + 230);
                e.Graphics.DrawString("充盈", zt9, Brushes.Black, x + 210, y + 230);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 230, 12, 12);
                //if (cmbSQszxg.Text == "充盈")
                //{
                //    e.Graphics.DrawLine(pb2, x + 245, y + 230, x + 255, y + 230 + 12);
                //    e.Graphics.DrawLine(pb2, x + 255, y + 230 + 12, x + 270, y + 230 - 3);
                //}
                e.Graphics.DrawString("不充盈", zt9, Brushes.Black, x + 310, y + 230);
                e.Graphics.DrawRectangle(Pens.Black, x + 360, y + 230, 12, 12);
                //if (cmbSQszxg.Text == "不充盈")
                //{
                //    e.Graphics.DrawLine(pb2, x + 355, y + 230, x + 365, y + 230 + 12);
                //    e.Graphics.DrawLine(pb2, x + 365, y + 230 + 12, x + 380, y + 230 - 3);
                //}
                e.Graphics.DrawLine(ptp, new Point(x + 80, y + 250), new Point(x + 720, y + 250));//画横线
                e.Graphics.DrawString("皮肤完整性：", zt9, Brushes.Black, x + 90, y + 260);
                e.Graphics.DrawString("完整", zt9, Brushes.Black, x + 210, y + 260);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 260, 12, 12);
                //if (cmbSQpfwzx.Text == "完整")
                //{
                //    e.Graphics.DrawLine(pb2, x + 245, y + 260, x + 255, y + 260 + 12);
                //    e.Graphics.DrawLine(pb2, x + 255, y + 260 + 12, x + 270, y + 260 - 3);
                //}
                e.Graphics.DrawString("划痕", zt9, Brushes.Black, x + 320, y + 260);
                e.Graphics.DrawRectangle(Pens.Black, x + 360, y + 260, 12, 12);
                //if (cmbSQpfwzx.Text == "划痕")
                //{
                //    e.Graphics.DrawLine(pb2, x + 355, y + 260, x + 365, y + 260 + 12);
                //    e.Graphics.DrawLine(pb2, x + 365, y + 260 + 12, x + 380, y + 260 - 3);
                //}
                e.Graphics.DrawString("皮疹", zt9, Brushes.Black, x + 420, y + 260);
                e.Graphics.DrawRectangle(Pens.Black, x + 460, y + 260, 12, 12);
                //if (cmbSQpfwzx.Text == "皮疹")
                //{
                //    e.Graphics.DrawLine(pb2, x + 455, y + 260, x + 465, y + 260 + 12);
                //    e.Graphics.DrawLine(pb2, x + 465, y + 260 + 12, x + 480, y + 260 - 3);
                //}
                e.Graphics.DrawString("破溃", zt9, Brushes.Black, x + 530, y + 260);
                e.Graphics.DrawRectangle(Pens.Black, x + 570, y + 260, 12, 12);
                //if (cmbSQpfwzx.Text == "破溃")
                //{
                //    e.Graphics.DrawLine(pb2, x + 565, y + 260, x + 575, y + 260 + 12);
                //    e.Graphics.DrawLine(pb2, x + 575, y + 260 + 12, x + 590, y + 260 - 3);
                //}
                e.Graphics.DrawString("水肿", zt9, Brushes.Black, x + 630, y + 260);
                e.Graphics.DrawRectangle(Pens.Black, x + 670, y + 260, 12, 12);
                //if (cmbSQpfwzx.Text == "水肿")
                //{
                //    e.Graphics.DrawLine(pb2, x + 665, y + 260, x + 675, y + 260 + 12);
                //    e.Graphics.DrawLine(pb2, x + 675, y + 260 + 12, x + 690, y + 260 - 3);
                //}
                e.Graphics.DrawLine(ptp, new Point(x + 80, y + 280), new Point(x + 720, y + 280));//画横线
                e.Graphics.DrawString("感染情况：", zt9, Brushes.Black, x + 90, y + 290);
                e.Graphics.DrawString("无", zt9, Brushes.Black, x + 220, y + 290);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 290, 12, 12);
                //if (cmbSQgrqk.Text == "无")
                //{
                //    e.Graphics.DrawLine(pb2, x + 245, y + 290, x + 255, y + 290 + 12);
                //    e.Graphics.DrawLine(pb2, x + 255, y + 290 + 12, x + 270, y + 290 - 3);
                //}
                e.Graphics.DrawString("有", zt9, Brushes.Black, x + 330, y + 290);
                e.Graphics.DrawRectangle(Pens.Black, x + 360, y + 290, 12, 12);
                //if (cmbSQgrqk.Text == "有")
                //{
                //    e.Graphics.DrawLine(pb2, x + 355, y + 290, x + 365, y + 290 + 12);
                //    e.Graphics.DrawLine(pb2, x + 365, y + 290 + 12, x + 380, y + 290 - 3);
                //}
                e.Graphics.DrawLine(ptp, new Point(x + 80, y + 310), new Point(x + 720, y + 310));//画横线
                e.Graphics.DrawString("过敏史：", zt9, Brushes.Black, x + 90, y + 320);
                e.Graphics.DrawString("无", zt9, Brushes.Black, x + 220, y + 320);
                e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 320, 12, 12);
                //if (cmbSQgms.Text == "无")
                //{
                //    e.Graphics.DrawLine(pb2, x + 245, y + 320, x + 255, y + 320 + 12);
                //    e.Graphics.DrawLine(pb2, x + 255, y + 320 + 12, x + 270, y + 320 - 3);
                //}
                e.Graphics.DrawString("青霉素", zt9, Brushes.Black, x + 290, y + 320);
                e.Graphics.DrawRectangle(Pens.Black, x + 340, y + 320, 12, 12);
                //if (cmbSQgms.Text == "青霉素")
                //{
                //    e.Graphics.DrawLine(pb2, x + 335, y + 320, x + 345, y + 320 + 12);
                //    e.Graphics.DrawLine(pb2, x + 345, y + 320 + 12, x + 360, y + 320 - 3);
                //}
                e.Graphics.DrawString("先锋霉素", zt9, Brushes.Black, x + 380, y + 320);
                e.Graphics.DrawRectangle(Pens.Black, x + 450, y + 320, 12, 12);
                //if (cmbSQgms.Text == "先锋霉素")
                //{
                //    e.Graphics.DrawLine(pb2, x + 445, y + 320, x + 455, y + 320 + 12);
                //    e.Graphics.DrawLine(pb2, x + 455, y + 320 + 12, x + 470, y + 320 - 3);
                //}
                e.Graphics.DrawString("磺胺类", zt9, Brushes.Black, x + 490, y + 320);
                e.Graphics.DrawRectangle(Pens.Black, x + 540, y + 320, 12, 12);
                //if (cmbSQgms.Text == "磺胺类")
                //{
                //    e.Graphics.DrawLine(pb2, x + 535, y + 320, x + 545, y + 320 + 12);
                //    e.Graphics.DrawLine(pb2, x + 545, y + 320 + 12, x + 560, y + 320 - 3);
                //}
                e.Graphics.DrawLine(ptp, new Point(x + 80, y + 340), new Point(x + 720, y + 340));//画横线
                e.Graphics.DrawString("其他:", zt9, Brushes.Black, x + 90, y + 350);
                e.Graphics.DrawLine(ptp, new Point(x, y + 370), new Point(x + 720, y + 370));//画横线
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 370));
                e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 370));
                y = y + 370; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 30));
                e.Graphics.DrawString("患者或家属签名:", zt9, Brushes.Black, x + 10, y + 10);
                e.Graphics.DrawString("术前访视护士签名:" + cmbFSHS.Text, zt9, Brushes.Black, x + 300, y + 10);
                e.Graphics.DrawString(Convert.ToDateTime(dateTimePicker1.Text).AddDays(-1).ToString("yyyy年MM月dd日"), zt9, Brushes.Black, x + 600, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 30));
                e.Graphics.DrawLine(ptp, new Point(x, y + 30), new Point(x + 720, y + 30));//画横线
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 140));
                e.Graphics.DrawString("手", zt9, Brushes.Black, x + 10, y + 40);
                e.Graphics.DrawString("术", zt9, Brushes.Black, x + 10, y + 70);
                e.Graphics.DrawString("中", zt9, Brushes.Black, x + 10, y + 100);
                e.Graphics.DrawLine(ptp, new Point(x + 40, y), new Point(x + 40, y + 140));
                e.Graphics.DrawString("手术人员： 主刀医生：", zt9, Brushes.Black, x + 50, y + 10);
                e.Graphics.DrawString("麻醉医师：", zt9, Brushes.Black, x + 250, y + 10);
                e.Graphics.DrawString("洗手护士：", zt9, Brushes.Black, x + 420, y + 10);
                e.Graphics.DrawString("巡回护士：", zt9, Brushes.Black, x + 570, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 40, y + 30), new Point(x + 720, y + 30));//画横线
                e.Graphics.DrawString("目的：落实优质护理服务措施，及时发现病人不适，防止并发症的发生，确保病人的安全。", zt9, Brushes.Black, x + 50, y + 40);
                e.Graphics.DrawString("内容：1.了解术前一日饮食和睡眠情况，保护隐私，缓解紧张。 2.保温，保持手术间温度。", zt9, Brushes.Black, x + 50, y + 60);
                e.Graphics.DrawString("      3.建立静脉通道，讲解术前输液目的，协助麻醉师实施麻醉。 4.落实查对制度。", zt9, Brushes.Black, x + 50, y + 80);
                e.Graphics.DrawString("      5.规程执行各项操作。 6.保持各管路通畅。 7.密切观察手术情况，配合医师", zt9, Brushes.Black, x + 50, y + 100);
                e.Graphics.DrawString("      手术。", zt9, Brushes.Black, x + 50, y + 120);
                e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 140));
                e.Graphics.DrawLine(ptp, new Point(x, y + 140), new Point(x + 720, y + 140));//画横线
                y = y + 140; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 150));
                e.Graphics.DrawString("手", zt9, Brushes.Black, x + 10, y + 40);
                e.Graphics.DrawString("术", zt9, Brushes.Black, x + 10, y + 70);
                e.Graphics.DrawString("后", zt9, Brushes.Black, x + 10, y + 100);
                e.Graphics.DrawLine(ptp, new Point(x + 40, y), new Point(x + 40, y + 150));
                e.Graphics.DrawString("目的：心理疏导，减轻痛苦，促早日健康。", zt9, Brushes.Black, x + 50, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 40, y + 30), new Point(x + 720, y + 30));//画横线
                e.Graphics.DrawString("精神状态：", zt9, Brushes.Black, x + 50, y + 40);
                e.Graphics.DrawString("好", zt9, Brushes.Black, x + 150, y + 40);
                e.Graphics.DrawRectangle(Pens.Black, x + 180, y + 40, 12, 12);
                //if (cmbSHjszt.Text == "好")
                //{
                //    e.Graphics.DrawLine(pb2, x + 175, y + 40, x + 185, y + 40 + 12);
                //    e.Graphics.DrawLine(pb2, x + 185, y + 40 + 12, x + 200, y + 40 - 3);
                //}
                e.Graphics.DrawString("一般", zt9, Brushes.Black, x + 260, y + 40);
                e.Graphics.DrawRectangle(Pens.Black, x + 300, y + 40, 12, 12);
                //if (cmbSHjszt.Text == "一般")
                //{
                //    e.Graphics.DrawLine(pb2, x + 295, y + 40, x + 305, y + 40 + 12);
                //    e.Graphics.DrawLine(pb2, x + 305, y + 40 + 12, x + 320, y + 40 - 3);
                //}
                e.Graphics.DrawString("差", zt9, Brushes.Black, x + 380, y + 40);
                e.Graphics.DrawRectangle(Pens.Black, x + 410, y + 40, 12, 12);
                //if (cmbSHjszt.Text == "差")
                //{
                //    e.Graphics.DrawLine(pb2, x + 405, y + 40, x + 415, y + 40 + 12);
                //    e.Graphics.DrawLine(pb2, x + 415, y + 40 + 12, x + 430, y + 40 - 3);
                //}
                e.Graphics.DrawString("极差", zt9, Brushes.Black, x + 490, y + 40);
                e.Graphics.DrawRectangle(Pens.Black, x + 530, y + 40, 12, 12);
                //if (cmbSHjszt.Text == "极差")
                //{
                //    e.Graphics.DrawLine(pb2, x + 525, y + 40, x + 535, y + 40 + 12);
                //    e.Graphics.DrawLine(pb2, x + 535, y + 40 + 12, x + 550, y + 40 - 3);
                //}
                e.Graphics.DrawString("皮肤完整性：", zt9, Brushes.Black, x + 50, y + 60);
                e.Graphics.DrawString("完整", zt9, Brushes.Black, x + 160, y + 60);
                e.Graphics.DrawRectangle(Pens.Black, x + 210, y + 60, 12, 12);
                //if (cmbSHpfps.Text == "完整")
                //{
                //    e.Graphics.DrawLine(pb2, x + 195, y + 60, x + 205, y + 60 + 12);
                //    e.Graphics.DrawLine(pb2, x + 205, y + 60 + 12, x + 220, y + 60 - 3);
                //}
                e.Graphics.DrawString("划痕", zt9, Brushes.Black, x + 270, y + 60);
                e.Graphics.DrawRectangle(Pens.Black, x + 310, y + 60, 12, 12);
                //if (cmbSHpfps.Text == "划痕")
                //{
                //    e.Graphics.DrawLine(pb2, x + 305, y + 60, x + 315, y + 60 + 12);
                //    e.Graphics.DrawLine(pb2, x + 315, y + 60 + 12, x + 330, y + 60 - 3);
                //}
                e.Graphics.DrawString("皮疹", zt9, Brushes.Black, x + 370, y + 60);
                e.Graphics.DrawRectangle(Pens.Black, x + 410, y + 60, 12, 12);
                //if (cmbSHpfps.Text == "皮疹")
                //{
                //    e.Graphics.DrawLine(pb2, x + 405, y + 60, x + 415, y + 60 + 12);
                //    e.Graphics.DrawLine(pb2, x + 415, y + 60 + 12, x + 430, y + 60 - 3);
                //}
                e.Graphics.DrawString("破溃", zt9, Brushes.Black, x + 480, y + 60);
                e.Graphics.DrawRectangle(Pens.Black, x + 520, y + 60, 12, 12);
                //if (cmbSHpfps.Text == "破溃")
                //{
                //    e.Graphics.DrawLine(pb2, x + 515, y + 60, x + 525, y + 60 + 12);
                //    e.Graphics.DrawLine(pb2, x + 525, y + 60 + 12, x + 540, y + 60 - 3);
                //}
                e.Graphics.DrawString("水肿", zt9, Brushes.Black, x + 580, y + 60);
                e.Graphics.DrawRectangle(Pens.Black, x + 620, y + 60, 12, 12);
                //if (cmbSHpfps.Text == "水肿")
                //{
                //    e.Graphics.DrawLine(pb2, x + 615, y + 60, x + 625, y + 60 + 12);
                //    e.Graphics.DrawLine(pb2, x + 625, y + 60 + 12, x + 640, y + 60 - 3);
                //}
                //e.Graphics.DrawLine(ptp, new Point(x + 40, y + 90), new Point(x + 720, y + 90));//画横线
                e.Graphics.DrawString("穿刺部位有无静脉炎：", zt9, Brushes.Black, x + 50, y + 80);

                e.Graphics.DrawString("无", zt9, Brushes.Black, x + 190, y + 80);
                e.Graphics.DrawRectangle(Pens.Black, x + 220, y + 80, 12, 12);
                //if (cmbSHccbw.Text == "无")
                //{
                //    e.Graphics.DrawLine(pb2, x + 215, y + 80, x + 225, y + 80 + 12);
                //    e.Graphics.DrawLine(pb2, x + 225, y + 80 + 12, x + 240, y + 80 - 3);
                //}
                e.Graphics.DrawString("有", zt9, Brushes.Black, x + 270, y + 80);
                e.Graphics.DrawRectangle(Pens.Black, x + 300, y + 80, 12, 12);
                //if (cmbSHccbw.Text == "有")
                //{
                //    e.Graphics.DrawLine(pb2, x + 295, y + 80, x + 305, y + 80 + 12);
                //    e.Graphics.DrawLine(pb2, x + 305, y + 80 + 12, x + 320, y + 80 - 3);
                //}
                //e.Graphics.DrawLine(ptp, new Point(x + 40, y + 120), new Point(x + 720, y + 120));//画横线
                e.Graphics.DrawString("其他异常情况：", zt9, Brushes.Black, x + 50, y + 100);

                e.Graphics.DrawString("无", zt9, Brushes.Black, x + 190, y + 100);
                e.Graphics.DrawRectangle(Pens.Black, x + 220, y + 100, 12, 12);
                //if (cmbSHqt.Text == "无")
                //{
                //    e.Graphics.DrawLine(pb2, x + 215, y + 100, x + 225, y + 100 + 12);
                //    e.Graphics.DrawLine(pb2, x + 225, y + 100 + 12, x + 240, y + 100 - 3);
                //}
                e.Graphics.DrawString("有", zt9, Brushes.Black, x + 270, y + 100);
                e.Graphics.DrawRectangle(Pens.Black, x + 300, y + 100, 12, 12);
                //if (cmbSHqt.Text == "有")
                //{
                //    e.Graphics.DrawLine(pb2, x + 295, y + 100, x + 305, y + 100 + 12);
                //    e.Graphics.DrawLine(pb2, x + 305, y + 100 + 12, x + 320, y + 100 - 3);
                //}
                e.Graphics.DrawString("异常详细情况：", zt9, Brushes.Black, x + 350, y + 100);
                e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 150));
                e.Graphics.DrawLine(ptp, new Point(x, y + 150), new Point(x + 720, y + 150));//画横线
                y = y + 150; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 30));
                e.Graphics.DrawString("您对手术室护理服务的评价：", zt9, Brushes.Black, x + 10, y + 10);
                e.Graphics.DrawString("非常满意", zt9, Brushes.Black, x + 220, y + 10);
                e.Graphics.DrawRectangle(Pens.Black, x + 290, y + 10, 12, 12);
                //if (cmbSssPJ.Text == "非常满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 285, y + 10, x + 295, y + 10 + 12);
                //    e.Graphics.DrawLine(pb2, x + 295, y + 10 + 12, x + 310, y + 10 - 3);
                //}
                e.Graphics.DrawString("满意", zt9, Brushes.Black, x + 350, y + 10);
                e.Graphics.DrawRectangle(Pens.Black, x + 390, y + 10, 12, 12);
                //if (cmbSssPJ.Text == "满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 385, y + 10, x + 395, y + 10 + 12);
                //    e.Graphics.DrawLine(pb2, x + 395, y + 10 + 12, x + 410, y + 10 - 3);
                //}
                e.Graphics.DrawString("一般", zt9, Brushes.Black, x + 450, y + 10);
                e.Graphics.DrawRectangle(Pens.Black, x + 490, y + 10, 12, 12);
                //if (cmbSssPJ.Text == "一般")
                //{
                //    e.Graphics.DrawLine(pb2, x + 485, y + 10, x + 495, y + 10 + 12);
                //    e.Graphics.DrawLine(pb2, x + 495, y + 10 + 12, x + 510, y + 10 - 3);
                //}
                e.Graphics.DrawString("不满意", zt9, Brushes.Black, x + 540, y + 10);
                e.Graphics.DrawRectangle(Pens.Black, x + 600, y + 10, 12, 12);
                //if (cmbSssPJ.Text == "不满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 595, y + 10, x + 605, y + 10 + 12);
                //    e.Graphics.DrawLine(pb2, x + 605, y + 10 + 12, x + 620, y + 10 - 3);
                //}
                e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 30));
                e.Graphics.DrawLine(ptp, new Point(x, y + 30), new Point(x + 720, y + 30));//画横线
                y = y + 30; y1 = y + 15;
                e.Graphics.DrawString("1.手术室护士服务态度：", zt9, Brushes.Black, x + 10, y + 10);
                e.Graphics.DrawString("非常满意", zt9, Brushes.Black, x + 270, y + 10);
                e.Graphics.DrawRectangle(Pens.Black, x + 340, y + 10, 12, 12);
                //if (cmbSssfuwu.Text == "非常满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 335, y + 10, x + 345, y + 10 + 12);
                //    e.Graphics.DrawLine(pb2, x + 345, y + 10 + 12, x + 360, y + 10 - 3);
                //}
                e.Graphics.DrawString("一般", zt9, Brushes.Black, x + 410, y + 10);
                e.Graphics.DrawRectangle(Pens.Black, x + 450, y + 10, 12, 12);
                //if (cmbSssfuwu.Text == "一般")
                //{
                //    e.Graphics.DrawLine(pb2, x + 445, y + 10, x + 455, y + 10 + 12);
                //    e.Graphics.DrawLine(pb2, x + 455, y + 10 + 12, x + 470, y + 10 - 3);
                //}
                e.Graphics.DrawString("不满意", zt9, Brushes.Black, x + 510, y + 10);
                e.Graphics.DrawRectangle(Pens.Black, x + 560, y + 10, 12, 12);
                //if (cmbSssfuwu.Text == "不满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 555, y + 10, x + 565, y + 10 + 12);
                //    e.Graphics.DrawLine(pb2, x + 565, y + 10 + 12, x + 580, y + 10 - 3);
                //}
                e.Graphics.DrawString("2.手术室护士解答您的疑问：", zt9, Brushes.Black, x + 10, y + 30);
                e.Graphics.DrawString("非常耐心", zt9, Brushes.Black, x + 270, y + 30);
                e.Graphics.DrawRectangle(Pens.Black, x + 340, y + 30, 12, 12);
                //if (cmbSssjdyw.Text == "非常耐心")
                //{
                //    e.Graphics.DrawLine(pb2, x + 335, y + 30, x + 345, y + 30 + 12);
                //    e.Graphics.DrawLine(pb2, x + 345, y + 30 + 12, x + 360, y + 30 - 3);
                //}
                e.Graphics.DrawString("一般", zt9, Brushes.Black, x + 410, y + 30);
                e.Graphics.DrawRectangle(Pens.Black, x + 450, y + 30, 12, 12);
                //if (cmbSssjdyw.Text == "一般")
                //{
                //    e.Graphics.DrawLine(pb2, x + 445, y + 30, x + 455, y + 30 + 12);
                //    e.Graphics.DrawLine(pb2, x + 455, y + 30 + 12, x + 470, y + 30 - 3);
                //}
                e.Graphics.DrawString("不满意", zt9, Brushes.Black, x + 510, y + 30);
                e.Graphics.DrawRectangle(Pens.Black, x + 560, y + 30, 12, 12);
                //if (cmbSssjdyw.Text == "不满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 555, y + 30, x + 565, y + 30 + 12);
                //    e.Graphics.DrawLine(pb2, x + 565, y + 30 + 12, x + 580, y + 30 - 3);
                //}
                e.Graphics.DrawString("3.手术室护士是否保护您的隐私：", zt9, Brushes.Black, x + 10, y + 50);
                e.Graphics.DrawString("非常满意", zt9, Brushes.Black, x + 270, y + 50);
                e.Graphics.DrawRectangle(Pens.Black, x + 340, y + 50, 12, 12);
                //if (cmbSssSfbf.Text == "非常满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 335, y + 50, x + 345, y + 50 + 12);
                //    e.Graphics.DrawLine(pb2, x + 345, y + 50 + 12, x + 360, y + 50 - 3);
                //}
                e.Graphics.DrawString("一般", zt9, Brushes.Black, x + 410, y + 50);
                e.Graphics.DrawRectangle(Pens.Black, x + 450, y + 50, 12, 12);
                //if (cmbSssSfbf.Text == "一般")
                //{
                //    e.Graphics.DrawLine(pb2, x + 445, y + 50, x + 455, y + 50 + 12);
                //    e.Graphics.DrawLine(pb2, x + 455, y + 50 + 12, x + 470, y + 50 - 3);
                //}
                e.Graphics.DrawString("不满意", zt9, Brushes.Black, x + 510, y + 50);
                e.Graphics.DrawRectangle(Pens.Black, x + 560, y + 50, 12, 12);
                //if (cmbSssSfbf.Text == "不满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 555, y + 50, x + 565, y + 50 + 12);
                //    e.Graphics.DrawLine(pb2, x + 565, y + 50 + 12, x + 580, y + 50 - 3);
                //}
                e.Graphics.DrawString("4.手术室护士环境是否满意：", zt9, Brushes.Black, x + 10, y + 70);
                e.Graphics.DrawString("非常满意", zt9, Brushes.Black, x + 270, y + 70);
                e.Graphics.DrawRectangle(Pens.Black, x + 340, y + 70, 12, 12);
                //if (cmbSssHJ.Text == "非常满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 335, y + 70, x + 345, y + 70 + 12);
                //    e.Graphics.DrawLine(pb2, x + 345, y + 70 + 12, x + 360, y + 70 - 3);
                //}
                e.Graphics.DrawString("一般", zt9, Brushes.Black, x + 410, y + 70);
                e.Graphics.DrawRectangle(Pens.Black, x + 450, y + 70, 12, 12);
                //if (cmbSssHJ.Text == "一般")
                //{
                //    e.Graphics.DrawLine(pb2, x + 445, y + 70, x + 455, y + 70 + 12);
                //    e.Graphics.DrawLine(pb2, x + 455, y + 70 + 12, x + 470, y + 70 - 3);
                //}
                e.Graphics.DrawString("不满意", zt9, Brushes.Black, x + 510, y + 70);
                e.Graphics.DrawRectangle(Pens.Black, x + 560, y + 70, 12, 12);
                //if (cmbSssHJ.Text == "不满意")
                //{
                //    e.Graphics.DrawLine(pb2, x + 555, y + 70, x + 565, y + 70 + 12);
                //    e.Graphics.DrawLine(pb2, x + 565, y + 70 + 12, x + 580, y + 70 - 3);
                //}
                e.Graphics.DrawString("5.接送您入手术室是否核对您的信息：", zt9, Brushes.Black, x + 10, y + 90);
                e.Graphics.DrawString("是", zt9, Brushes.Black, x + 310, y + 90);
                e.Graphics.DrawRectangle(Pens.Black, x + 340, y + 90, 12, 12);
                //if (cmbSssSfHD.Text == "是")
                //{
                //    e.Graphics.DrawLine(pb2, x + 335, y + 90, x + 345, y + 90 + 12);
                //    e.Graphics.DrawLine(pb2, x + 345, y + 90 + 12, x + 360, y + 90 - 3);
                //}
                e.Graphics.DrawString("否", zt9, Brushes.Black, x + 420, y + 90);
                e.Graphics.DrawRectangle(Pens.Black, x + 450, y + 90, 12, 12);
                //if (cmbSssSfHD.Text == "否")
                //{
                //    e.Graphics.DrawLine(pb2, x + 445, y + 90, x + 455, y + 90 + 12);
                //    e.Graphics.DrawLine(pb2, x + 455, y + 90 + 12, x + 470, y + 90 - 3);
                //}
                ///计算换行
                //int YY = y + 120;
                //YY = YY + 10;
                //string str1YY = "";
                //int StrLengthYY = txtTcJy.Text.Trim().Length;
                //int rowYY = StrLengthYY / 50;
                e.Graphics.DrawString("请留下您的宝贵意见和建议：", zt9, Brushes.Black, x + 10, y + 110);
                //for (int i = 0; i <= rowYY; i++)//20个字符就换行
                //{
                //    if (i < rowYY)
                //        str1YY = txtTcJy.Text.Trim().ToString().Substring(i * 50, 50); //从第i*20个开始，截取20个字符串
                //    else
                //        str1YY = txtTcJy.Text.Trim().ToString().Substring(i * 50);
                //    e.Graphics.DrawString(str1YY, zt9, Brushes.Black, x + 10, YY);
                //    YY = YY + 20;
                //}
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 190));
                e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 190));
                e.Graphics.DrawLine(ptp, new Point(x, y + 190), new Point(x + 720, y + 190));//画横线
                y = y + 190; y1 = y + 15;
                e.Graphics.DrawLine(ptp, new Point(x, y), new Point(x, y + 30));
                e.Graphics.DrawString("患者或家属签名:", zt9, Brushes.Black, x + 10, y + 10);
                e.Graphics.DrawString("术后访视护士签名:" + cmbFSHS.Text, zt9, Brushes.Black, x + 300, y + 10);
                e.Graphics.DrawString(Convert.ToDateTime(dateTimePicker1.Text).AddDays(1).ToString("yyyy年MM月dd日"), zt9, Brushes.Black, x + 610, y + 10);
                e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 30));
                e.Graphics.DrawLine(ptp, new Point(x, y + 30), new Point(x + 720, y + 30));//画横线
                y = y + 20;               
                e.Graphics.DrawString("制定时间：2012年10月", zt9, Brushes.Black, x + 570, y + 10);
                y = y + 15;
                e.Graphics.DrawString("修改时间：2015年01月", zt9, Brushes.Black, x + 570, y + 10);
                rowIndex++;
                if (rowIndex <dgvRowCount)
                {
                    e.HasMorePages = true;
                    y = 0; y1 = 0;
                    break;
                }
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells["住院号"].Value.ToString();
                SQSHFSD f2 = new SQSHFSD(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                f2.ShowDialog();
            }
            else MessageBox.Show("请选择病人！");
        }
    }
}
